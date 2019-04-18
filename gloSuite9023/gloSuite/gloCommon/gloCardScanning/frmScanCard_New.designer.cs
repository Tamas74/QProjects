namespace gloCardScanning
{
    partial class frmScanCard_New
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
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip = null;
                }
                //try
                //{
                //    if (printDialog1 != null)
                //    {
                //        printDialog1.Dispose();
                //        printDialog1 = null;
                //    }
                //}
                //catch
                //{
                //}
                //try
                //{
                //    if (printDoc != null)
                //    {
                //        gloGlobal.cEventHelper.RemoveAllEventHandlers(printDoc);
                //        printDoc.Dispose();
                //        printDoc = null;
                //    }
                //}
                //catch
                //{
                //}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScanCard_New));
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
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
            this.lbl_ScanMessage = new System.Windows.Forms.Label();
            this.btnInsBrowse = new System.Windows.Forms.Button();
            this.cmbIns_PlanProvider = new System.Windows.Forms.ComboBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.txtCopay = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbInsOther = new System.Windows.Forms.RadioButton();
            this.rbInsFemale = new System.Windows.Forms.RadioButton();
            this.rbInsMale = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIns_MemberID = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtIns_GroupNo = new System.Windows.Forms.TextBox();
            this.txtIns_MemberName = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.PnlNewPatScan = new System.Windows.Forms.Panel();
            this.pnlNewPatDetails = new System.Windows.Forms.Panel();
            this.txtNewCode = new System.Windows.Forms.MaskedTextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.pnlInternalControl_New = new System.Windows.Forms.Panel();
            this.Pic_NewPatient = new System.Windows.Forms.PictureBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.lblNew_County = new System.Windows.Forms.Label();
            this.cmbNew_State = new System.Windows.Forms.ComboBox();
            this.txtNew_County = new System.Windows.Forms.TextBox();
            this.label91 = new System.Windows.Forms.Label();
            this.txtNew_Address2 = new System.Windows.Forms.TextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.txtNew_MIName = new System.Windows.Forms.TextBox();
            this.txtNew_FirstName = new System.Windows.Forms.TextBox();
            this.txtNew_LastName = new System.Windows.Forms.TextBox();
            this.cmbNew_Country = new System.Windows.Forms.ComboBox();
            this.cmbNew_Gender = new System.Windows.Forms.ComboBox();
            this.txtNew_DOB = new System.Windows.Forms.MaskedTextBox();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.txtNew_Address1 = new System.Windows.Forms.TextBox();
            this.label105 = new System.Windows.Forms.Label();
            this.txtNew_City = new System.Windows.Forms.TextBox();
            this.txtNew_Zip = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.pnlLicenseDetails = new System.Windows.Forms.Panel();
            this.pnlNewDetails = new System.Windows.Forms.Panel();
            this.chk_Photo = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlTemplateDetailsHeader = new System.Windows.Forms.Panel();
            this.lblCardDetails = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbl_NewCounty = new System.Windows.Forms.Label();
            this.cmbNewState = new System.Windows.Forms.ComboBox();
            this.txt_NewCounty = new System.Windows.Forms.TextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.txt_NewAddress2 = new System.Windows.Forms.TextBox();
            this.chk_Address = new System.Windows.Forms.CheckBox();
            this.chk_Sex = new System.Windows.Forms.CheckBox();
            this.chk_DOB = new System.Windows.Forms.CheckBox();
            this.chk_Patientname = new System.Windows.Forms.CheckBox();
            this.label79 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.txt_NewMIname = new System.Windows.Forms.TextBox();
            this.txt_NewFname = new System.Windows.Forms.TextBox();
            this.txt_NewLastname = new System.Windows.Forms.TextBox();
            this.cmbNewCountry = new System.Windows.Forms.ComboBox();
            this.cmbNewGender = new System.Windows.Forms.ComboBox();
            this.txt_NewDOB = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.lbl_NewState = new System.Windows.Forms.Label();
            this.txt_NewPatientcode = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.txt_NewAddress1 = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.txt_NewCity = new System.Windows.Forms.TextBox();
            this.txt_NewZip = new System.Windows.Forms.TextBox();
            this.PicNew = new System.Windows.Forms.PictureBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlPreviousDetails = new System.Windows.Forms.Panel();
            this.txtLic_PatientCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblLic_County = new System.Windows.Forms.Label();
            this.txtLic_County = new System.Windows.Forms.TextBox();
            this.cmbLicState = new System.Windows.Forms.ComboBox();
            this.label83 = new System.Windows.Forms.Label();
            this.txtLicAddress2 = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.cmbLicCountry = new System.Windows.Forms.ComboBox();
            this.cmbLicGender = new System.Windows.Forms.ComboBox();
            this.txtLicMiddleName = new System.Windows.Forms.TextBox();
            this.txtLicDOB = new System.Windows.Forms.MaskedTextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lbl_PrevState = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.txtLicFirstName = new System.Windows.Forms.TextBox();
            this.txtLicLastName = new System.Windows.Forms.TextBox();
            this.txtLicAddress = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.txtLicCity = new System.Windows.Forms.TextBox();
            this.txtLicZip = new System.Windows.Forms.TextBox();
            this.pbFaceImage = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
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
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            //this.printDialog1 = new System.Windows.Forms.PrintDialog();
            //this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.toolTipInsurance = new System.Windows.Forms.ToolTip(this.components);
            this.tooltipZip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlInsuranceDetails.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.PnlNewPatScan.SuspendLayout();
            this.pnlNewPatDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_NewPatient)).BeginInit();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.pnlLicenseDetails.SuspendLayout();
            this.pnlNewDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlTemplateDetailsHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicNew)).BeginInit();
            this.pnlPreviousDetails.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFaceImage)).BeginInit();
            this.pnlCardLeft.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FrontSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BackSide)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.ForeColor = System.Drawing.Color.Black;
            this.txtFilePath.Location = new System.Drawing.Point(326, 24);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(277, 22);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.TabStop = false;
            this.txtFilePath.Text = "F:\\Developer Working Folder\\Temp\\Sample.bmp";
            this.txtFilePath.Visible = false;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Controls.Add(this.txtFilePath);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(813, 54);
            this.pnlToolStrip.TabIndex = 120;
            this.pnlToolStrip.TabStop = true;
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
            this.ts_Commands.Size = new System.Drawing.Size(813, 53);
            this.ts_Commands.TabIndex = 4;
            this.ts_Commands.TabStop = true;
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
            this.tsb_Cheque.Visible = false;
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
            this.tsb_Print.Visible = false;
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
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 310);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(813, 392);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.pnlInsuranceDetails);
            this.pnlDetails.Controls.Add(this.PnlNewPatScan);
            this.pnlDetails.Controls.Add(this.pnlLicenseDetails);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(3, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(807, 389);
            this.pnlDetails.TabIndex = 0;
            // 
            // pnlInsuranceDetails
            // 
            this.pnlInsuranceDetails.Controls.Add(this.lbl_ScanMessage);
            this.pnlInsuranceDetails.Controls.Add(this.btnInsBrowse);
            this.pnlInsuranceDetails.Controls.Add(this.cmbIns_PlanProvider);
            this.pnlInsuranceDetails.Controls.Add(this.panel10);
            this.pnlInsuranceDetails.Controls.Add(this.txtCopay);
            this.pnlInsuranceDetails.Controls.Add(this.groupBox1);
            this.pnlInsuranceDetails.Controls.Add(this.label2);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_MemberID);
            this.pnlInsuranceDetails.Controls.Add(this.label27);
            this.pnlInsuranceDetails.Controls.Add(this.label14);
            this.pnlInsuranceDetails.Controls.Add(this.label15);
            this.pnlInsuranceDetails.Controls.Add(this.label16);
            this.pnlInsuranceDetails.Controls.Add(this.label17);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_GroupNo);
            this.pnlInsuranceDetails.Controls.Add(this.txtIns_MemberName);
            this.pnlInsuranceDetails.Controls.Add(this.label37);
            this.pnlInsuranceDetails.Controls.Add(this.label39);
            this.pnlInsuranceDetails.Controls.Add(this.label40);
            this.pnlInsuranceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInsuranceDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlInsuranceDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlInsuranceDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlInsuranceDetails.Name = "pnlInsuranceDetails";
            this.pnlInsuranceDetails.Size = new System.Drawing.Size(807, 389);
            this.pnlInsuranceDetails.TabIndex = 1;
            // 
            // lbl_ScanMessage
            // 
            this.lbl_ScanMessage.AutoSize = true;
            this.lbl_ScanMessage.ForeColor = System.Drawing.Color.Red;
            this.lbl_ScanMessage.Location = new System.Drawing.Point(478, 61);
            this.lbl_ScanMessage.Name = "lbl_ScanMessage";
            this.lbl_ScanMessage.Size = new System.Drawing.Size(18, 14);
            this.lbl_ScanMessage.TabIndex = 159;
            this.lbl_ScanMessage.Text = "lbl";
            this.lbl_ScanMessage.Visible = false;
            // 
            // btnInsBrowse
            // 
            this.btnInsBrowse.AutoEllipsis = true;
            this.btnInsBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnInsBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsBrowse.BackgroundImage")));
            this.btnInsBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnInsBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnInsBrowse.Image")));
            this.btnInsBrowse.Location = new System.Drawing.Point(449, 57);
            this.btnInsBrowse.Name = "btnInsBrowse";
            this.btnInsBrowse.Size = new System.Drawing.Size(22, 22);
            this.btnInsBrowse.TabIndex = 1;
            this.toolTipInsurance.SetToolTip(this.btnInsBrowse, "Browse Insurance Plan");
            this.btnInsBrowse.UseVisualStyleBackColor = false;
            this.btnInsBrowse.Click += new System.EventHandler(this.btnInsBrowse_Click);
            this.btnInsBrowse.MouseLeave += new System.EventHandler(this.btnInsBrowse_MouseLeave);
            this.btnInsBrowse.MouseHover += new System.EventHandler(this.btnInsBrowse_MouseHover);
            // 
            // cmbIns_PlanProvider
            // 
            this.cmbIns_PlanProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIns_PlanProvider.FormattingEnabled = true;
            this.cmbIns_PlanProvider.Location = new System.Drawing.Point(213, 57);
            this.cmbIns_PlanProvider.Name = "cmbIns_PlanProvider";
            this.cmbIns_PlanProvider.Size = new System.Drawing.Size(230, 22);
            this.cmbIns_PlanProvider.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(1, 1);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(805, 26);
            this.panel10.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Controls.Add(this.label11);
            this.panel11.Controls.Add(this.label81);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(805, 26);
            this.panel11.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(805, 25);
            this.label11.TabIndex = 1;
            this.label11.Text = "  Card Details";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label81
            // 
            this.label81.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label81.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label81.Location = new System.Drawing.Point(0, 25);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(805, 1);
            this.label81.TabIndex = 2;
            // 
            // txtCopay
            // 
            this.txtCopay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCopay.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCopay.ForeColor = System.Drawing.Color.Black;
            this.txtCopay.Location = new System.Drawing.Point(1, 1);
            this.txtCopay.Name = "txtCopay";
            this.txtCopay.Size = new System.Drawing.Size(805, 22);
            this.txtCopay.TabIndex = 15;
            this.txtCopay.TabStop = false;
            this.txtCopay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCopay.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbInsOther);
            this.groupBox1.Controls.Add(this.rbInsFemale);
            this.groupBox1.Controls.Add(this.rbInsMale);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.groupBox1.Location = new System.Drawing.Point(103, 386);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 30);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
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
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(65, 394);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 125;
            this.label2.Text = "Sex :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // txtIns_MemberID
            // 
            this.txtIns_MemberID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_MemberID.ForeColor = System.Drawing.Color.Black;
            this.txtIns_MemberID.Location = new System.Drawing.Point(213, 86);
            this.txtIns_MemberID.MaxLength = 25;
            this.txtIns_MemberID.Name = "txtIns_MemberID";
            this.txtIns_MemberID.Size = new System.Drawing.Size(230, 22);
            this.txtIns_MemberID.TabIndex = 2;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Location = new System.Drawing.Point(136, 90);
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
            this.label14.Location = new System.Drawing.Point(1, 388);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(805, 1);
            this.label14.TabIndex = 118;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(1, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(805, 1);
            this.label15.TabIndex = 117;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Location = new System.Drawing.Point(806, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 389);
            this.label16.TabIndex = 116;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 389);
            this.label17.TabIndex = 115;
            // 
            // txtIns_GroupNo
            // 
            this.txtIns_GroupNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_GroupNo.ForeColor = System.Drawing.Color.Black;
            this.txtIns_GroupNo.Location = new System.Drawing.Point(213, 144);
            this.txtIns_GroupNo.MaxLength = 50;
            this.txtIns_GroupNo.Name = "txtIns_GroupNo";
            this.txtIns_GroupNo.Size = new System.Drawing.Size(230, 22);
            this.txtIns_GroupNo.TabIndex = 4;
            // 
            // txtIns_MemberName
            // 
            this.txtIns_MemberName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIns_MemberName.ForeColor = System.Drawing.Color.Black;
            this.txtIns_MemberName.Location = new System.Drawing.Point(213, 115);
            this.txtIns_MemberName.MaxLength = 150;
            this.txtIns_MemberName.Name = "txtIns_MemberName";
            this.txtIns_MemberName.Size = new System.Drawing.Size(230, 22);
            this.txtIns_MemberName.TabIndex = 3;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Location = new System.Drawing.Point(144, 148);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(67, 14);
            this.label37.TabIndex = 3;
            this.label37.Text = "Group No :";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Location = new System.Drawing.Point(117, 119);
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
            this.label40.Location = new System.Drawing.Point(126, 61);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(85, 14);
            this.label40.TabIndex = 0;
            this.label40.Text = "Plan Provider :";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PnlNewPatScan
            // 
            this.PnlNewPatScan.Controls.Add(this.pnlNewPatDetails);
            this.PnlNewPatScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlNewPatScan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PnlNewPatScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.PnlNewPatScan.Location = new System.Drawing.Point(0, 0);
            this.PnlNewPatScan.Name = "PnlNewPatScan";
            this.PnlNewPatScan.Size = new System.Drawing.Size(807, 389);
            this.PnlNewPatScan.TabIndex = 2;
            // 
            // pnlNewPatDetails
            // 
            this.pnlNewPatDetails.Controls.Add(this.txtNewCode);
            this.pnlNewPatDetails.Controls.Add(this.label53);
            this.pnlNewPatDetails.Controls.Add(this.pnlInternalControl_New);
            this.pnlNewPatDetails.Controls.Add(this.Pic_NewPatient);
            this.pnlNewPatDetails.Controls.Add(this.panel13);
            this.pnlNewPatDetails.Controls.Add(this.lblNew_County);
            this.pnlNewPatDetails.Controls.Add(this.cmbNew_State);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_County);
            this.pnlNewPatDetails.Controls.Add(this.label91);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_Address2);
            this.pnlNewPatDetails.Controls.Add(this.label92);
            this.pnlNewPatDetails.Controls.Add(this.label93);
            this.pnlNewPatDetails.Controls.Add(this.label94);
            this.pnlNewPatDetails.Controls.Add(this.label95);
            this.pnlNewPatDetails.Controls.Add(this.label96);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_MIName);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_FirstName);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_LastName);
            this.pnlNewPatDetails.Controls.Add(this.cmbNew_Country);
            this.pnlNewPatDetails.Controls.Add(this.cmbNew_Gender);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_DOB);
            this.pnlNewPatDetails.Controls.Add(this.label98);
            this.pnlNewPatDetails.Controls.Add(this.label99);
            this.pnlNewPatDetails.Controls.Add(this.label100);
            this.pnlNewPatDetails.Controls.Add(this.lblState);
            this.pnlNewPatDetails.Controls.Add(this.label102);
            this.pnlNewPatDetails.Controls.Add(this.label103);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_Address1);
            this.pnlNewPatDetails.Controls.Add(this.label105);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_City);
            this.pnlNewPatDetails.Controls.Add(this.txtNew_Zip);
            this.pnlNewPatDetails.Controls.Add(this.label107);
            this.pnlNewPatDetails.Controls.Add(this.label109);
            this.pnlNewPatDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNewPatDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlNewPatDetails.Name = "pnlNewPatDetails";
            this.pnlNewPatDetails.Size = new System.Drawing.Size(807, 389);
            this.pnlNewPatDetails.TabIndex = 0;
            // 
            // txtNewCode
            // 
            this.txtNewCode.BackColor = System.Drawing.SystemColors.Control;
            this.txtNewCode.Enabled = false;
            this.txtNewCode.Location = new System.Drawing.Point(276, 37);
            this.txtNewCode.Mask = "AAA-AAAAAAAAAA";
            this.txtNewCode.Name = "txtNewCode";
            this.txtNewCode.PromptChar = ' ';
            this.txtNewCode.ResetOnSpace = false;
            this.txtNewCode.ShortcutsEnabled = false;
            this.txtNewCode.Size = new System.Drawing.Size(156, 22);
            this.txtNewCode.TabIndex = 500004;
            this.txtNewCode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label53.Location = new System.Drawing.Point(1, 388);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(805, 1);
            this.label53.TabIndex = 179;
            // 
            // pnlInternalControl_New
            // 
            this.pnlInternalControl_New.Location = new System.Drawing.Point(308, 132);
            this.pnlInternalControl_New.Name = "pnlInternalControl_New";
            this.pnlInternalControl_New.Size = new System.Drawing.Size(157, 98);
            this.pnlInternalControl_New.TabIndex = 176;
            this.pnlInternalControl_New.Visible = false;
            // 
            // Pic_NewPatient
            // 
            this.Pic_NewPatient.BackgroundImage = global::gloCardScanning.Properties.Resources.HumanNew;
            this.Pic_NewPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Pic_NewPatient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pic_NewPatient.Location = new System.Drawing.Point(25, 73);
            this.Pic_NewPatient.Name = "Pic_NewPatient";
            this.Pic_NewPatient.Size = new System.Drawing.Size(121, 132);
            this.Pic_NewPatient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Pic_NewPatient.TabIndex = 150;
            this.Pic_NewPatient.TabStop = false;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.panel14);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(1, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(805, 26);
            this.panel13.TabIndex = 0;
            // 
            // panel14
            // 
            this.panel14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel14.BackgroundImage")));
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel14.Controls.Add(this.label87);
            this.panel14.Controls.Add(this.label88);
            this.panel14.Controls.Add(this.label89);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(805, 26);
            this.panel14.TabIndex = 0;
            // 
            // label87
            // 
            this.label87.BackColor = System.Drawing.Color.Transparent;
            this.label87.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label87.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.ForeColor = System.Drawing.Color.White;
            this.label87.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label87.Location = new System.Drawing.Point(0, 1);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(805, 24);
            this.label87.TabIndex = 0;
            this.label87.Text = "   New Details";
            this.label87.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label88
            // 
            this.label88.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label88.Dock = System.Windows.Forms.DockStyle.Top;
            this.label88.Location = new System.Drawing.Point(0, 0);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(805, 1);
            this.label88.TabIndex = 1;
            // 
            // label89
            // 
            this.label89.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label89.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label89.Location = new System.Drawing.Point(0, 25);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(805, 1);
            this.label89.TabIndex = 2;
            // 
            // lblNew_County
            // 
            this.lblNew_County.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNew_County.AutoEllipsis = true;
            this.lblNew_County.AutoSize = true;
            this.lblNew_County.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNew_County.Location = new System.Drawing.Point(219, 238);
            this.lblNew_County.Name = "lblNew_County";
            this.lblNew_County.Size = new System.Drawing.Size(54, 14);
            this.lblNew_County.TabIndex = 177;
            this.lblNew_County.Text = "County :";
            // 
            // cmbNew_State
            // 
            this.cmbNew_State.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbNew_State.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNew_State.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNew_State.ForeColor = System.Drawing.Color.Black;
            this.cmbNew_State.FormattingEnabled = true;
            this.cmbNew_State.Location = new System.Drawing.Point(473, 182);
            this.cmbNew_State.Name = "cmbNew_State";
            this.cmbNew_State.Size = new System.Drawing.Size(107, 22);
            this.cmbNew_State.TabIndex = 9;
            // 
            // txtNew_County
            // 
            this.txtNew_County.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtNew_County.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew_County.Location = new System.Drawing.Point(276, 234);
            this.txtNew_County.MaxLength = 50;
            this.txtNew_County.Name = "txtNew_County";
            this.txtNew_County.Size = new System.Drawing.Size(123, 22);
            this.txtNew_County.TabIndex = 11;
            this.txtNew_County.Tag = "";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label91.Location = new System.Drawing.Point(204, 160);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(69, 14);
            this.label91.TabIndex = 171;
            this.label91.Text = "Address 2 :";
            this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNew_Address2
            // 
            this.txtNew_Address2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew_Address2.ForeColor = System.Drawing.Color.Black;
            this.txtNew_Address2.Location = new System.Drawing.Point(276, 156);
            this.txtNew_Address2.MaxLength = 50;
            this.txtNew_Address2.Name = "txtNew_Address2";
            this.txtNew_Address2.Size = new System.Drawing.Size(354, 22);
            this.txtNew_Address2.TabIndex = 6;
            // 
            // label92
            // 
            this.label92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label92.Dock = System.Windows.Forms.DockStyle.Left;
            this.label92.Location = new System.Drawing.Point(0, 0);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(1, 389);
            this.label92.TabIndex = 165;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label93.Location = new System.Drawing.Point(184, 66);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(89, 14);
            this.label93.TabIndex = 164;
            this.label93.Text = "Patient Name :";
            this.label93.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label94.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label94.Location = new System.Drawing.Point(440, 86);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(25, 12);
            this.label94.TabIndex = 163;
            this.label94.Text = "(MI)";
            this.label94.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label95.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label95.Location = new System.Drawing.Point(522, 86);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(58, 12);
            this.label95.TabIndex = 162;
            this.label95.Text = "(Last Name)";
            this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label96.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label96.Location = new System.Drawing.Point(324, 88);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(60, 12);
            this.label96.TabIndex = 161;
            this.label96.Text = "(First Name)";
            this.label96.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNew_MIName
            // 
            this.txtNew_MIName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew_MIName.ForeColor = System.Drawing.Color.Black;
            this.txtNew_MIName.Location = new System.Drawing.Point(435, 62);
            this.txtNew_MIName.MaxLength = 50;
            this.txtNew_MIName.Name = "txtNew_MIName";
            this.txtNew_MIName.Size = new System.Drawing.Size(35, 22);
            this.txtNew_MIName.TabIndex = 1;
            // 
            // txtNew_FirstName
            // 
            this.txtNew_FirstName.AcceptsReturn = true;
            this.txtNew_FirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew_FirstName.ForeColor = System.Drawing.Color.Black;
            this.txtNew_FirstName.Location = new System.Drawing.Point(276, 62);
            this.txtNew_FirstName.MaxLength = 50;
            this.txtNew_FirstName.Name = "txtNew_FirstName";
            this.txtNew_FirstName.Size = new System.Drawing.Size(156, 22);
            this.txtNew_FirstName.TabIndex = 0;
            // 
            // txtNew_LastName
            // 
            this.txtNew_LastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew_LastName.ForeColor = System.Drawing.Color.Black;
            this.txtNew_LastName.Location = new System.Drawing.Point(473, 62);
            this.txtNew_LastName.MaxLength = 50;
            this.txtNew_LastName.Name = "txtNew_LastName";
            this.txtNew_LastName.Size = new System.Drawing.Size(156, 22);
            this.txtNew_LastName.TabIndex = 2;
            // 
            // cmbNew_Country
            // 
            this.cmbNew_Country.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbNew_Country.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNew_Country.ForeColor = System.Drawing.Color.Black;
            this.cmbNew_Country.FormattingEnabled = true;
            this.cmbNew_Country.Location = new System.Drawing.Point(473, 208);
            this.cmbNew_Country.Name = "cmbNew_Country";
            this.cmbNew_Country.Size = new System.Drawing.Size(107, 22);
            this.cmbNew_Country.TabIndex = 10;
            this.cmbNew_Country.SelectedIndexChanged += new System.EventHandler(this.cmbNew_Country_SelectedIndexChanged);
            // 
            // cmbNew_Gender
            // 
            this.cmbNew_Gender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbNew_Gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNew_Gender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNew_Gender.ForeColor = System.Drawing.Color.Black;
            this.cmbNew_Gender.FormattingEnabled = true;
            this.cmbNew_Gender.Items.AddRange(new object[] {
            "",
            "Female",
            "Male",
            "Other"});
            this.cmbNew_Gender.Location = new System.Drawing.Point(473, 104);
            this.cmbNew_Gender.Name = "cmbNew_Gender";
            this.cmbNew_Gender.Size = new System.Drawing.Size(107, 22);
            this.cmbNew_Gender.TabIndex = 4;
            // 
            // txtNew_DOB
            // 
            this.txtNew_DOB.Location = new System.Drawing.Point(276, 104);
            this.txtNew_DOB.Mask = "00/00/0000";
            this.txtNew_DOB.Name = "txtNew_DOB";
            this.txtNew_DOB.Size = new System.Drawing.Size(123, 22);
            this.txtNew_DOB.TabIndex = 3;
            this.txtNew_DOB.ValidatingType = typeof(System.DateTime);
            this.txtNew_DOB.Validating += new System.ComponentModel.CancelEventHandler(this.txtNew_DOB_Validating);
            // 
            // label98
            // 
            this.label98.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label98.AutoEllipsis = true;
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(412, 212);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(58, 14);
            this.label98.TabIndex = 156;
            this.label98.Text = "Country :";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label99.Location = new System.Drawing.Point(204, 134);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(69, 14);
            this.label99.TabIndex = 133;
            this.label99.Text = "Address 1 :";
            this.label99.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label100.Location = new System.Drawing.Point(238, 186);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(35, 14);
            this.label100.TabIndex = 135;
            this.label100.Text = "City :";
            this.label100.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblState
            // 
            this.lblState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblState.Location = new System.Drawing.Point(401, 186);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(69, 14);
            this.lblState.TabIndex = 138;
            this.lblState.Text = "State :";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label102.Location = new System.Drawing.Point(242, 212);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(31, 14);
            this.label102.TabIndex = 126;
            this.label102.Text = "Zip :";
            this.label102.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label103.Location = new System.Drawing.Point(230, 40);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(43, 14);
            this.label103.TabIndex = 154;
            this.label103.Text = "Code :";
            this.label103.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNew_Address1
            // 
            this.txtNew_Address1.AcceptsReturn = true;
            this.txtNew_Address1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew_Address1.ForeColor = System.Drawing.Color.Black;
            this.txtNew_Address1.Location = new System.Drawing.Point(276, 130);
            this.txtNew_Address1.MaxLength = 255;
            this.txtNew_Address1.Name = "txtNew_Address1";
            this.txtNew_Address1.Size = new System.Drawing.Size(354, 22);
            this.txtNew_Address1.TabIndex = 5;
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label105.Location = new System.Drawing.Point(188, 108);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(85, 14);
            this.label105.TabIndex = 153;
            this.label105.Text = "Date of birth :";
            this.label105.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNew_City
            // 
            this.txtNew_City.AcceptsReturn = true;
            this.txtNew_City.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew_City.ForeColor = System.Drawing.Color.Black;
            this.txtNew_City.Location = new System.Drawing.Point(276, 182);
            this.txtNew_City.MaxLength = 255;
            this.txtNew_City.Name = "txtNew_City";
            this.txtNew_City.Size = new System.Drawing.Size(123, 22);
            this.txtNew_City.TabIndex = 8;
            // 
            // txtNew_Zip
            // 
            this.txtNew_Zip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNew_Zip.ForeColor = System.Drawing.Color.Black;
            this.txtNew_Zip.Location = new System.Drawing.Point(276, 208);
            this.txtNew_Zip.MaxLength = 5;
            this.txtNew_Zip.Name = "txtNew_Zip";
            this.txtNew_Zip.Size = new System.Drawing.Size(123, 22);
            this.txtNew_Zip.TabIndex = 7;
            this.txtNew_Zip.TextChanged += new System.EventHandler(this.txtNewZip_TextChanged);
            this.txtNew_Zip.Enter += new System.EventHandler(this.txtNewZip_GotFocus);
            this.txtNew_Zip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewZip_KeyDown);
            this.txtNew_Zip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewZip_KeyPress);
            this.txtNew_Zip.Leave += new System.EventHandler(this.txtNewZip_LostFocus);
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label107.Location = new System.Drawing.Point(415, 108);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(55, 14);
            this.label107.TabIndex = 152;
            this.label107.Text = "Gender :";
            this.label107.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label109
            // 
            this.label109.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label109.Dock = System.Windows.Forms.DockStyle.Right;
            this.label109.Location = new System.Drawing.Point(806, 0);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(1, 389);
            this.label109.TabIndex = 178;
            // 
            // pnlLicenseDetails
            // 
            this.pnlLicenseDetails.Controls.Add(this.pnlNewDetails);
            this.pnlLicenseDetails.Controls.Add(this.pnlPreviousDetails);
            this.pnlLicenseDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLicenseDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlLicenseDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlLicenseDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlLicenseDetails.Name = "pnlLicenseDetails";
            this.pnlLicenseDetails.Size = new System.Drawing.Size(807, 389);
            this.pnlLicenseDetails.TabIndex = 0;
            // 
            // pnlNewDetails
            // 
            this.pnlNewDetails.Controls.Add(this.chk_Photo);
            this.pnlNewDetails.Controls.Add(this.label18);
            this.pnlNewDetails.Controls.Add(this.pnlInternalControl);
            this.pnlNewDetails.Controls.Add(this.panel2);
            this.pnlNewDetails.Controls.Add(this.lbl_NewCounty);
            this.pnlNewDetails.Controls.Add(this.cmbNewState);
            this.pnlNewDetails.Controls.Add(this.txt_NewCounty);
            this.pnlNewDetails.Controls.Add(this.label84);
            this.pnlNewDetails.Controls.Add(this.txt_NewAddress2);
            this.pnlNewDetails.Controls.Add(this.chk_Address);
            this.pnlNewDetails.Controls.Add(this.chk_Sex);
            this.pnlNewDetails.Controls.Add(this.chk_DOB);
            this.pnlNewDetails.Controls.Add(this.chk_Patientname);
            this.pnlNewDetails.Controls.Add(this.label79);
            this.pnlNewDetails.Controls.Add(this.label6);
            this.pnlNewDetails.Controls.Add(this.label7);
            this.pnlNewDetails.Controls.Add(this.label68);
            this.pnlNewDetails.Controls.Add(this.label78);
            this.pnlNewDetails.Controls.Add(this.txt_NewMIname);
            this.pnlNewDetails.Controls.Add(this.txt_NewFname);
            this.pnlNewDetails.Controls.Add(this.txt_NewLastname);
            this.pnlNewDetails.Controls.Add(this.cmbNewCountry);
            this.pnlNewDetails.Controls.Add(this.cmbNewGender);
            this.pnlNewDetails.Controls.Add(this.txt_NewDOB);
            this.pnlNewDetails.Controls.Add(this.label8);
            this.pnlNewDetails.Controls.Add(this.label65);
            this.pnlNewDetails.Controls.Add(this.label66);
            this.pnlNewDetails.Controls.Add(this.lbl_NewState);
            this.pnlNewDetails.Controls.Add(this.txt_NewPatientcode);
            this.pnlNewDetails.Controls.Add(this.label69);
            this.pnlNewDetails.Controls.Add(this.label70);
            this.pnlNewDetails.Controls.Add(this.txt_NewAddress1);
            this.pnlNewDetails.Controls.Add(this.label72);
            this.pnlNewDetails.Controls.Add(this.txt_NewCity);
            this.pnlNewDetails.Controls.Add(this.txt_NewZip);
            this.pnlNewDetails.Controls.Add(this.PicNew);
            this.pnlNewDetails.Controls.Add(this.label74);
            this.pnlNewDetails.Controls.Add(this.label12);
            this.pnlNewDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNewDetails.Location = new System.Drawing.Point(395, 0);
            this.pnlNewDetails.Name = "pnlNewDetails";
            this.pnlNewDetails.Size = new System.Drawing.Size(412, 389);
            this.pnlNewDetails.TabIndex = 0;
            // 
            // chk_Photo
            // 
            this.chk_Photo.AutoSize = true;
            this.chk_Photo.Location = new System.Drawing.Point(270, 98);
            this.chk_Photo.Name = "chk_Photo";
            this.chk_Photo.Size = new System.Drawing.Size(15, 14);
            this.chk_Photo.TabIndex = 1;
            this.chk_Photo.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(1, 388);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(410, 1);
            this.label18.TabIndex = 179;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.Location = new System.Drawing.Point(132, 258);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(157, 98);
            this.pnlInternalControl.TabIndex = 176;
            this.pnlInternalControl.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlTemplateDetailsHeader);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(410, 26);
            this.panel2.TabIndex = 0;
            // 
            // pnlTemplateDetailsHeader
            // 
            this.pnlTemplateDetailsHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTemplateDetailsHeader.BackgroundImage")));
            this.pnlTemplateDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateDetailsHeader.Controls.Add(this.lblCardDetails);
            this.pnlTemplateDetailsHeader.Controls.Add(this.label21);
            this.pnlTemplateDetailsHeader.Controls.Add(this.label20);
            this.pnlTemplateDetailsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplateDetailsHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTemplateDetailsHeader.Name = "pnlTemplateDetailsHeader";
            this.pnlTemplateDetailsHeader.Size = new System.Drawing.Size(410, 26);
            this.pnlTemplateDetailsHeader.TabIndex = 1;
            // 
            // lblCardDetails
            // 
            this.lblCardDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblCardDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardDetails.ForeColor = System.Drawing.Color.White;
            this.lblCardDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCardDetails.Location = new System.Drawing.Point(0, 1);
            this.lblCardDetails.Name = "lblCardDetails";
            this.lblCardDetails.Size = new System.Drawing.Size(410, 24);
            this.lblCardDetails.TabIndex = 117;
            this.lblCardDetails.Text = "   New Details";
            this.lblCardDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(410, 1);
            this.label21.TabIndex = 116;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Location = new System.Drawing.Point(0, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(410, 1);
            this.label20.TabIndex = 115;
            // 
            // lbl_NewCounty
            // 
            this.lbl_NewCounty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_NewCounty.AutoEllipsis = true;
            this.lbl_NewCounty.AutoSize = true;
            this.lbl_NewCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NewCounty.Location = new System.Drawing.Point(41, 363);
            this.lbl_NewCounty.Name = "lbl_NewCounty";
            this.lbl_NewCounty.Size = new System.Drawing.Size(54, 14);
            this.lbl_NewCounty.TabIndex = 177;
            this.lbl_NewCounty.Text = "County :";
            // 
            // cmbNewState
            // 
            this.cmbNewState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbNewState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNewState.ForeColor = System.Drawing.Color.Black;
            this.cmbNewState.FormattingEnabled = true;
            this.cmbNewState.Location = new System.Drawing.Point(290, 309);
            this.cmbNewState.Name = "cmbNewState";
            this.cmbNewState.Size = new System.Drawing.Size(92, 22);
            this.cmbNewState.TabIndex = 15;
            // 
            // txt_NewCounty
            // 
            this.txt_NewCounty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txt_NewCounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewCounty.Location = new System.Drawing.Point(99, 359);
            this.txt_NewCounty.MaxLength = 50;
            this.txt_NewCounty.Name = "txt_NewCounty";
            this.txt_NewCounty.Size = new System.Drawing.Size(84, 22);
            this.txt_NewCounty.TabIndex = 17;
            this.txt_NewCounty.Tag = "";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Location = new System.Drawing.Point(26, 288);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(69, 14);
            this.label84.TabIndex = 171;
            this.label84.Text = "Address 2 :";
            this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_NewAddress2
            // 
            this.txt_NewAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewAddress2.ForeColor = System.Drawing.Color.Black;
            this.txt_NewAddress2.Location = new System.Drawing.Point(99, 284);
            this.txt_NewAddress2.MaxLength = 50;
            this.txt_NewAddress2.Name = "txt_NewAddress2";
            this.txt_NewAddress2.Size = new System.Drawing.Size(283, 22);
            this.txt_NewAddress2.TabIndex = 12;
            // 
            // chk_Address
            // 
            this.chk_Address.AutoSize = true;
            this.chk_Address.Location = new System.Drawing.Point(388, 263);
            this.chk_Address.Name = "chk_Address";
            this.chk_Address.Size = new System.Drawing.Size(15, 14);
            this.chk_Address.TabIndex = 11;
            this.chk_Address.UseVisualStyleBackColor = true;
            // 
            // chk_Sex
            // 
            this.chk_Sex.AutoSize = true;
            this.chk_Sex.Location = new System.Drawing.Point(388, 238);
            this.chk_Sex.Name = "chk_Sex";
            this.chk_Sex.Size = new System.Drawing.Size(15, 14);
            this.chk_Sex.TabIndex = 9;
            this.chk_Sex.UseVisualStyleBackColor = true;
            // 
            // chk_DOB
            // 
            this.chk_DOB.AutoSize = true;
            this.chk_DOB.Location = new System.Drawing.Point(190, 239);
            this.chk_DOB.Name = "chk_DOB";
            this.chk_DOB.Size = new System.Drawing.Size(15, 14);
            this.chk_DOB.TabIndex = 7;
            this.chk_DOB.UseVisualStyleBackColor = true;
            // 
            // chk_Patientname
            // 
            this.chk_Patientname.AutoSize = true;
            this.chk_Patientname.Location = new System.Drawing.Point(388, 200);
            this.chk_Patientname.Name = "chk_Patientname";
            this.chk_Patientname.Size = new System.Drawing.Size(15, 14);
            this.chk_Patientname.TabIndex = 5;
            this.chk_Patientname.UseVisualStyleBackColor = true;
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Dock = System.Windows.Forms.DockStyle.Left;
            this.label79.Location = new System.Drawing.Point(0, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(1, 389);
            this.label79.TabIndex = 165;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(6, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 14);
            this.label6.TabIndex = 164;
            this.label6.Text = "Patient Name :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(229, 220);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 12);
            this.label7.TabIndex = 163;
            this.label7.Text = "(MI)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label68.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label68.Location = new System.Drawing.Point(291, 220);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(58, 12);
            this.label68.TabIndex = 162;
            this.label68.Text = "(Last Name)";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label78.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label78.Location = new System.Drawing.Point(127, 220);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(60, 12);
            this.label78.TabIndex = 161;
            this.label78.Text = "(First Name)";
            this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_NewMIname
            // 
            this.txt_NewMIname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewMIname.ForeColor = System.Drawing.Color.Black;
            this.txt_NewMIname.Location = new System.Drawing.Point(225, 196);
            this.txt_NewMIname.MaxLength = 50;
            this.txt_NewMIname.Name = "txt_NewMIname";
            this.txt_NewMIname.Size = new System.Drawing.Size(35, 22);
            this.txt_NewMIname.TabIndex = 3;
            // 
            // txt_NewFname
            // 
            this.txt_NewFname.AcceptsReturn = true;
            this.txt_NewFname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewFname.ForeColor = System.Drawing.Color.Black;
            this.txt_NewFname.Location = new System.Drawing.Point(99, 196);
            this.txt_NewFname.MaxLength = 50;
            this.txt_NewFname.Name = "txt_NewFname";
            this.txt_NewFname.Size = new System.Drawing.Size(123, 22);
            this.txt_NewFname.TabIndex = 2;
            // 
            // txt_NewLastname
            // 
            this.txt_NewLastname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewLastname.ForeColor = System.Drawing.Color.Black;
            this.txt_NewLastname.Location = new System.Drawing.Point(263, 196);
            this.txt_NewLastname.MaxLength = 50;
            this.txt_NewLastname.Name = "txt_NewLastname";
            this.txt_NewLastname.Size = new System.Drawing.Size(119, 22);
            this.txt_NewLastname.TabIndex = 4;
            // 
            // cmbNewCountry
            // 
            this.cmbNewCountry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbNewCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewCountry.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNewCountry.ForeColor = System.Drawing.Color.Black;
            this.cmbNewCountry.FormattingEnabled = true;
            this.cmbNewCountry.Location = new System.Drawing.Point(290, 334);
            this.cmbNewCountry.Name = "cmbNewCountry";
            this.cmbNewCountry.Size = new System.Drawing.Size(92, 22);
            this.cmbNewCountry.TabIndex = 16;
            this.cmbNewCountry.SelectedIndexChanged += new System.EventHandler(this.cmbNewCountry_SelectedIndexChanged);
            // 
            // cmbNewGender
            // 
            this.cmbNewGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbNewGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNewGender.ForeColor = System.Drawing.Color.Black;
            this.cmbNewGender.FormattingEnabled = true;
            this.cmbNewGender.Items.AddRange(new object[] {
            "",
            "Female",
            "Male",
            "Other"});
            this.cmbNewGender.Location = new System.Drawing.Point(290, 234);
            this.cmbNewGender.Name = "cmbNewGender";
            this.cmbNewGender.Size = new System.Drawing.Size(92, 22);
            this.cmbNewGender.TabIndex = 8;
            // 
            // txt_NewDOB
            // 
            this.txt_NewDOB.Location = new System.Drawing.Point(99, 234);
            this.txt_NewDOB.Mask = "00/00/0000";
            this.txt_NewDOB.Name = "txt_NewDOB";
            this.txt_NewDOB.Size = new System.Drawing.Size(84, 22);
            this.txt_NewDOB.TabIndex = 6;
            this.txt_NewDOB.ValidatingType = typeof(System.DateTime);
            this.txt_NewDOB.Validating += new System.ComponentModel.CancelEventHandler(this.txt_NewDOB_Validating);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(227, 338);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 14);
            this.label8.TabIndex = 156;
            this.label8.Text = "Country :";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label65.Location = new System.Drawing.Point(26, 263);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(69, 14);
            this.label65.TabIndex = 133;
            this.label65.Text = "Address 1 :";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label66.Location = new System.Drawing.Point(60, 313);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(35, 14);
            this.label66.TabIndex = 135;
            this.label66.Text = "City :";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_NewState
            // 
            this.lbl_NewState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NewState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_NewState.Location = new System.Drawing.Point(210, 313);
            this.lbl_NewState.Name = "lbl_NewState";
            this.lbl_NewState.Size = new System.Drawing.Size(75, 14);
            this.lbl_NewState.TabIndex = 138;
            this.lbl_NewState.Text = "State :";
            this.lbl_NewState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_NewPatientcode
            // 
            this.txt_NewPatientcode.Enabled = false;
            this.txt_NewPatientcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewPatientcode.ForeColor = System.Drawing.Color.Black;
            this.txt_NewPatientcode.Location = new System.Drawing.Point(99, 170);
            this.txt_NewPatientcode.Name = "txt_NewPatientcode";
            this.txt_NewPatientcode.Size = new System.Drawing.Size(123, 22);
            this.txt_NewPatientcode.TabIndex = 0;
            this.txt_NewPatientcode.TabStop = false;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label69.Location = new System.Drawing.Point(64, 338);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(31, 14);
            this.label69.TabIndex = 126;
            this.label69.Text = "Zip :";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Location = new System.Drawing.Point(52, 174);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(43, 14);
            this.label70.TabIndex = 154;
            this.label70.Text = "Code :";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_NewAddress1
            // 
            this.txt_NewAddress1.AcceptsReturn = true;
            this.txt_NewAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewAddress1.ForeColor = System.Drawing.Color.Black;
            this.txt_NewAddress1.Location = new System.Drawing.Point(99, 259);
            this.txt_NewAddress1.MaxLength = 255;
            this.txt_NewAddress1.Name = "txt_NewAddress1";
            this.txt_NewAddress1.Size = new System.Drawing.Size(283, 22);
            this.txt_NewAddress1.TabIndex = 10;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Location = new System.Drawing.Point(10, 238);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(85, 14);
            this.label72.TabIndex = 153;
            this.label72.Text = "Date of birth :";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_NewCity
            // 
            this.txt_NewCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewCity.ForeColor = System.Drawing.Color.Black;
            this.txt_NewCity.Location = new System.Drawing.Point(99, 309);
            this.txt_NewCity.MaxLength = 255;
            this.txt_NewCity.Name = "txt_NewCity";
            this.txt_NewCity.Size = new System.Drawing.Size(84, 22);
            this.txt_NewCity.TabIndex = 14;
            // 
            // txt_NewZip
            // 
            this.txt_NewZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NewZip.ForeColor = System.Drawing.Color.Black;
            this.txt_NewZip.Location = new System.Drawing.Point(99, 334);
            this.txt_NewZip.MaxLength = 5;
            this.txt_NewZip.Name = "txt_NewZip";
            this.txt_NewZip.Size = new System.Drawing.Size(84, 22);
            this.txt_NewZip.TabIndex = 13;
            this.txt_NewZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
            this.txt_NewZip.Enter += new System.EventHandler(this.txtZip_GotFocus);
            this.txt_NewZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZip_KeyDown);
            this.txt_NewZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZip_KeyPress);
            this.txt_NewZip.Leave += new System.EventHandler(this.txtZip_LostFocus);
            // 
            // PicNew
            // 
            this.PicNew.BackgroundImage = global::gloCardScanning.Properties.Resources.HumanNew;
            this.PicNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PicNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicNew.Location = new System.Drawing.Point(132, 29);
            this.PicNew.Name = "PicNew";
            this.PicNew.Size = new System.Drawing.Size(121, 132);
            this.PicNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicNew.TabIndex = 150;
            this.PicNew.TabStop = false;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Location = new System.Drawing.Point(232, 238);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(55, 14);
            this.label74.TabIndex = 152;
            this.label74.Text = "Gender :";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(411, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 389);
            this.label12.TabIndex = 178;
            // 
            // pnlPreviousDetails
            // 
            this.pnlPreviousDetails.Controls.Add(this.txtLic_PatientCode);
            this.pnlPreviousDetails.Controls.Add(this.label10);
            this.pnlPreviousDetails.Controls.Add(this.lblLic_County);
            this.pnlPreviousDetails.Controls.Add(this.txtLic_County);
            this.pnlPreviousDetails.Controls.Add(this.cmbLicState);
            this.pnlPreviousDetails.Controls.Add(this.label83);
            this.pnlPreviousDetails.Controls.Add(this.txtLicAddress2);
            this.pnlPreviousDetails.Controls.Add(this.panel8);
            this.pnlPreviousDetails.Controls.Add(this.label4);
            this.pnlPreviousDetails.Controls.Add(this.label77);
            this.pnlPreviousDetails.Controls.Add(this.label59);
            this.pnlPreviousDetails.Controls.Add(this.label32);
            this.pnlPreviousDetails.Controls.Add(this.cmbLicCountry);
            this.pnlPreviousDetails.Controls.Add(this.cmbLicGender);
            this.pnlPreviousDetails.Controls.Add(this.txtLicMiddleName);
            this.pnlPreviousDetails.Controls.Add(this.txtLicDOB);
            this.pnlPreviousDetails.Controls.Add(this.label33);
            this.pnlPreviousDetails.Controls.Add(this.lblCountry);
            this.pnlPreviousDetails.Controls.Add(this.label31);
            this.pnlPreviousDetails.Controls.Add(this.label30);
            this.pnlPreviousDetails.Controls.Add(this.lbl_PrevState);
            this.pnlPreviousDetails.Controls.Add(this.label9);
            this.pnlPreviousDetails.Controls.Add(this.label56);
            this.pnlPreviousDetails.Controls.Add(this.txtLicFirstName);
            this.pnlPreviousDetails.Controls.Add(this.txtLicLastName);
            this.pnlPreviousDetails.Controls.Add(this.txtLicAddress);
            this.pnlPreviousDetails.Controls.Add(this.label50);
            this.pnlPreviousDetails.Controls.Add(this.txtLicCity);
            this.pnlPreviousDetails.Controls.Add(this.txtLicZip);
            this.pnlPreviousDetails.Controls.Add(this.pbFaceImage);
            this.pnlPreviousDetails.Controls.Add(this.label13);
            this.pnlPreviousDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPreviousDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlPreviousDetails.Name = "pnlPreviousDetails";
            this.pnlPreviousDetails.Size = new System.Drawing.Size(395, 389);
            this.pnlPreviousDetails.TabIndex = 1;
            // 
            // txtLic_PatientCode
            // 
            this.txtLic_PatientCode.BackColor = System.Drawing.SystemColors.Control;
            this.txtLic_PatientCode.Enabled = false;
            this.txtLic_PatientCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLic_PatientCode.ForeColor = System.Drawing.Color.Black;
            this.txtLic_PatientCode.Location = new System.Drawing.Point(93, 171);
            this.txtLic_PatientCode.Name = "txtLic_PatientCode";
            this.txtLic_PatientCode.Size = new System.Drawing.Size(123, 22);
            this.txtLic_PatientCode.TabIndex = 500006;
            this.txtLic_PatientCode.TabStop = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(1, 388);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(394, 1);
            this.label10.TabIndex = 181;
            // 
            // lblLic_County
            // 
            this.lblLic_County.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLic_County.AutoEllipsis = true;
            this.lblLic_County.AutoSize = true;
            this.lblLic_County.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLic_County.Location = new System.Drawing.Point(37, 363);
            this.lblLic_County.Name = "lblLic_County";
            this.lblLic_County.Size = new System.Drawing.Size(54, 14);
            this.lblLic_County.TabIndex = 179;
            this.lblLic_County.Text = "County :";
            // 
            // txtLic_County
            // 
            this.txtLic_County.BackColor = System.Drawing.SystemColors.Control;
            this.txtLic_County.Enabled = false;
            this.txtLic_County.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLic_County.Location = new System.Drawing.Point(93, 359);
            this.txtLic_County.Name = "txtLic_County";
            this.txtLic_County.Size = new System.Drawing.Size(78, 22);
            this.txtLic_County.TabIndex = 178;
            this.txtLic_County.TabStop = false;
            this.txtLic_County.Tag = "";
            // 
            // cmbLicState
            // 
            this.cmbLicState.BackColor = System.Drawing.SystemColors.Control;
            this.cmbLicState.CausesValidation = false;
            this.cmbLicState.Enabled = false;
            this.cmbLicState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLicState.ForeColor = System.Drawing.Color.Black;
            this.cmbLicState.FormattingEnabled = true;
            this.cmbLicState.Location = new System.Drawing.Point(289, 309);
            this.cmbLicState.Name = "cmbLicState";
            this.cmbLicState.Size = new System.Drawing.Size(91, 22);
            this.cmbLicState.TabIndex = 176;
            this.cmbLicState.TabStop = false;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label83.Location = new System.Drawing.Point(22, 288);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(69, 14);
            this.label83.TabIndex = 155;
            this.label83.Text = "Address 2 :";
            this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLicAddress2
            // 
            this.txtLicAddress2.BackColor = System.Drawing.SystemColors.Control;
            this.txtLicAddress2.CausesValidation = false;
            this.txtLicAddress2.Enabled = false;
            this.txtLicAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicAddress2.ForeColor = System.Drawing.Color.Black;
            this.txtLicAddress2.Location = new System.Drawing.Point(93, 284);
            this.txtLicAddress2.Name = "txtLicAddress2";
            this.txtLicAddress2.Size = new System.Drawing.Size(287, 22);
            this.txtLicAddress2.TabIndex = 156;
            this.txtLicAddress2.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(1, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(394, 26);
            this.panel8.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel9.Controls.Add(this.label19);
            this.panel9.Controls.Add(this.label80);
            this.panel9.Controls.Add(this.label82);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(394, 26);
            this.panel9.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(394, 1);
            this.label19.TabIndex = 0;
            // 
            // label80
            // 
            this.label80.BackColor = System.Drawing.Color.Transparent;
            this.label80.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label80.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.ForeColor = System.Drawing.Color.White;
            this.label80.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label80.Location = new System.Drawing.Point(0, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(394, 25);
            this.label80.TabIndex = 1;
            this.label80.Text = "   Previous Details";
            this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label82
            // 
            this.label82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label82.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label82.Location = new System.Drawing.Point(0, 25);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(394, 1);
            this.label82.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(232, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 153;
            this.label4.Text = "Gender :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label77.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label77.Location = new System.Drawing.Point(224, 220);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(25, 12);
            this.label77.TabIndex = 130;
            this.label77.Text = "(MI)";
            this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label59.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Location = new System.Drawing.Point(287, 220);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(58, 12);
            this.label59.TabIndex = 129;
            this.label59.Text = "(Last Name)";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Location = new System.Drawing.Point(121, 218);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(60, 12);
            this.label32.TabIndex = 128;
            this.label32.Text = "(First Name)";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLicCountry
            // 
            this.cmbLicCountry.BackColor = System.Drawing.SystemColors.Control;
            this.cmbLicCountry.CausesValidation = false;
            this.cmbLicCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLicCountry.Enabled = false;
            this.cmbLicCountry.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLicCountry.ForeColor = System.Drawing.Color.Black;
            this.cmbLicCountry.FormattingEnabled = true;
            this.cmbLicCountry.Location = new System.Drawing.Point(289, 334);
            this.cmbLicCountry.Name = "cmbLicCountry";
            this.cmbLicCountry.Size = new System.Drawing.Size(91, 22);
            this.cmbLicCountry.TabIndex = 127;
            this.cmbLicCountry.TabStop = false;
            this.cmbLicCountry.SelectedIndexChanged += new System.EventHandler(this.cmbLicCountry_SelectedIndexChanged);
            // 
            // cmbLicGender
            // 
            this.cmbLicGender.BackColor = System.Drawing.SystemColors.Control;
            this.cmbLicGender.CausesValidation = false;
            this.cmbLicGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLicGender.Enabled = false;
            this.cmbLicGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLicGender.ForeColor = System.Drawing.Color.Black;
            this.cmbLicGender.FormattingEnabled = true;
            this.cmbLicGender.Items.AddRange(new object[] {
            "",
            "Female",
            "Male",
            "Other"});
            this.cmbLicGender.Location = new System.Drawing.Point(289, 234);
            this.cmbLicGender.Name = "cmbLicGender";
            this.cmbLicGender.Size = new System.Drawing.Size(91, 22);
            this.cmbLicGender.TabIndex = 126;
            // 
            // txtLicMiddleName
            // 
            this.txtLicMiddleName.BackColor = System.Drawing.SystemColors.Control;
            this.txtLicMiddleName.CausesValidation = false;
            this.txtLicMiddleName.Enabled = false;
            this.txtLicMiddleName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicMiddleName.ForeColor = System.Drawing.Color.Black;
            this.txtLicMiddleName.Location = new System.Drawing.Point(220, 196);
            this.txtLicMiddleName.Name = "txtLicMiddleName";
            this.txtLicMiddleName.Size = new System.Drawing.Size(35, 22);
            this.txtLicMiddleName.TabIndex = 5;
            this.txtLicMiddleName.TabStop = false;
            // 
            // txtLicDOB
            // 
            this.txtLicDOB.BackColor = System.Drawing.SystemColors.Control;
            this.txtLicDOB.CausesValidation = false;
            this.txtLicDOB.Enabled = false;
            this.txtLicDOB.Location = new System.Drawing.Point(93, 234);
            this.txtLicDOB.Mask = "00/00/0000";
            this.txtLicDOB.Name = "txtLicDOB";
            this.txtLicDOB.Size = new System.Drawing.Size(78, 22);
            this.txtLicDOB.TabIndex = 7;
            this.txtLicDOB.TabStop = false;
            this.txtLicDOB.ValidatingType = typeof(System.DateTime);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Location = new System.Drawing.Point(2, 200);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(89, 14);
            this.label33.TabIndex = 1;
            this.label33.Text = "Patient Name :";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountry.AutoEllipsis = true;
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(229, 338);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(58, 14);
            this.lblCountry.TabIndex = 125;
            this.lblCountry.Text = "Country :";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Location = new System.Drawing.Point(22, 263);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(69, 14);
            this.label31.TabIndex = 3;
            this.label31.Text = "Address 1 :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Location = new System.Drawing.Point(56, 313);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(35, 14);
            this.label30.TabIndex = 4;
            this.label30.Text = "City :";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_PrevState
            // 
            this.lbl_PrevState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PrevState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_PrevState.Location = new System.Drawing.Point(218, 313);
            this.lbl_PrevState.Name = "lbl_PrevState";
            this.lbl_PrevState.Size = new System.Drawing.Size(69, 14);
            this.lbl_PrevState.TabIndex = 5;
            this.lbl_PrevState.Text = "State :";
            this.lbl_PrevState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(60, 338);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "Zip :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Location = new System.Drawing.Point(48, 174);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(43, 14);
            this.label56.TabIndex = 121;
            this.label56.Text = "Code :";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLicFirstName
            // 
            this.txtLicFirstName.BackColor = System.Drawing.SystemColors.Control;
            this.txtLicFirstName.CausesValidation = false;
            this.txtLicFirstName.Enabled = false;
            this.txtLicFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicFirstName.ForeColor = System.Drawing.Color.Black;
            this.txtLicFirstName.Location = new System.Drawing.Point(93, 196);
            this.txtLicFirstName.Name = "txtLicFirstName";
            this.txtLicFirstName.Size = new System.Drawing.Size(123, 22);
            this.txtLicFirstName.TabIndex = 4;
            this.txtLicFirstName.TabStop = false;
            // 
            // txtLicLastName
            // 
            this.txtLicLastName.BackColor = System.Drawing.SystemColors.Control;
            this.txtLicLastName.CausesValidation = false;
            this.txtLicLastName.Enabled = false;
            this.txtLicLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicLastName.ForeColor = System.Drawing.Color.Black;
            this.txtLicLastName.Location = new System.Drawing.Point(259, 196);
            this.txtLicLastName.Name = "txtLicLastName";
            this.txtLicLastName.Size = new System.Drawing.Size(121, 22);
            this.txtLicLastName.TabIndex = 6;
            this.txtLicLastName.TabStop = false;
            // 
            // txtLicAddress
            // 
            this.txtLicAddress.BackColor = System.Drawing.SystemColors.Control;
            this.txtLicAddress.CausesValidation = false;
            this.txtLicAddress.Enabled = false;
            this.txtLicAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicAddress.ForeColor = System.Drawing.Color.Black;
            this.txtLicAddress.Location = new System.Drawing.Point(93, 259);
            this.txtLicAddress.Name = "txtLicAddress";
            this.txtLicAddress.Size = new System.Drawing.Size(287, 22);
            this.txtLicAddress.TabIndex = 9;
            this.txtLicAddress.TabStop = false;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(6, 238);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(85, 14);
            this.label50.TabIndex = 119;
            this.label50.Text = "Date of birth :";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLicCity
            // 
            this.txtLicCity.BackColor = System.Drawing.SystemColors.Control;
            this.txtLicCity.CausesValidation = false;
            this.txtLicCity.Enabled = false;
            this.txtLicCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicCity.ForeColor = System.Drawing.Color.Black;
            this.txtLicCity.Location = new System.Drawing.Point(93, 309);
            this.txtLicCity.Name = "txtLicCity";
            this.txtLicCity.Size = new System.Drawing.Size(78, 22);
            this.txtLicCity.TabIndex = 11;
            this.txtLicCity.TabStop = false;
            // 
            // txtLicZip
            // 
            this.txtLicZip.BackColor = System.Drawing.SystemColors.Control;
            this.txtLicZip.CausesValidation = false;
            this.txtLicZip.Enabled = false;
            this.txtLicZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicZip.ForeColor = System.Drawing.Color.Black;
            this.txtLicZip.Location = new System.Drawing.Point(93, 334);
            this.txtLicZip.Name = "txtLicZip";
            this.txtLicZip.Size = new System.Drawing.Size(78, 22);
            this.txtLicZip.TabIndex = 10;
            this.txtLicZip.TabStop = false;
            // 
            // pbFaceImage
            // 
            this.pbFaceImage.BackgroundImage = global::gloCardScanning.Properties.Resources.HumanNew;
            this.pbFaceImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbFaceImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFaceImage.Location = new System.Drawing.Point(134, 30);
            this.pbFaceImage.Name = "pbFaceImage";
            this.pbFaceImage.Size = new System.Drawing.Size(121, 132);
            this.pbFaceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbFaceImage.TabIndex = 108;
            this.pbFaceImage.TabStop = false;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 389);
            this.label13.TabIndex = 180;
            // 
            // pnlCardLeft
            // 
            this.pnlCardLeft.Controls.Add(this.panel3);
            this.pnlCardLeft.Controls.Add(this.panel5);
            this.pnlCardLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCardLeft.Location = new System.Drawing.Point(0, 54);
            this.pnlCardLeft.Name = "pnlCardLeft";
            this.pnlCardLeft.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCardLeft.Size = new System.Drawing.Size(813, 256);
            this.pnlCardLeft.TabIndex = 1;
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
            this.panel3.Location = new System.Drawing.Point(3, 29);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(807, 224);
            this.panel3.TabIndex = 0;
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label63.Location = new System.Drawing.Point(1, 223);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(805, 1);
            this.label63.TabIndex = 0;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Top;
            this.label62.Location = new System.Drawing.Point(1, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(805, 1);
            this.label62.TabIndex = 1;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.label61.Location = new System.Drawing.Point(806, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1, 224);
            this.label61.TabIndex = 2;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Location = new System.Drawing.Point(0, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 224);
            this.label60.TabIndex = 3;
            // 
            // pb_FrontSide
            // 
            this.pb_FrontSide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_FrontSide.BackgroundImage")));
            this.pb_FrontSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_FrontSide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_FrontSide.Location = new System.Drawing.Point(13, 9);
            this.pb_FrontSide.Name = "pb_FrontSide";
            this.pb_FrontSide.Size = new System.Drawing.Size(379, 207);
            this.pb_FrontSide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_FrontSide.TabIndex = 107;
            this.pb_FrontSide.TabStop = false;
            // 
            // pb_BackSide
            // 
            this.pb_BackSide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_BackSide.BackgroundImage")));
            this.pb_BackSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_BackSide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_BackSide.Location = new System.Drawing.Point(413, 9);
            this.pb_BackSide.Name = "pb_BackSide";
            this.pb_BackSide.Size = new System.Drawing.Size(379, 207);
            this.pb_BackSide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_BackSide.TabIndex = 109;
            this.pb_BackSide.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(807, 26);
            this.panel5.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label51);
            this.panel6.Controls.Add(this.label52);
            this.panel6.Controls.Add(this.label54);
            this.panel6.Controls.Add(this.label55);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(807, 26);
            this.panel6.TabIndex = 0;
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
            this.label51.Size = new System.Drawing.Size(805, 25);
            this.label51.TabIndex = 0;
            this.label51.Text = "  Card Images";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Location = new System.Drawing.Point(1, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(805, 1);
            this.label52.TabIndex = 1;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Right;
            this.label54.Location = new System.Drawing.Point(806, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 26);
            this.label54.TabIndex = 2;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Location = new System.Drawing.Point(0, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 26);
            this.label55.TabIndex = 3;
            // 
            // printDialog1
            // 
            //this.printDialog1.UseEXDialog = true;
            // 
            // frmScanCard_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(813, 702);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlCardLeft);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmScanCard_New";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan Patient ID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScanCard_New_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmScanCard_New_FormClosed);
            this.Load += new System.EventHandler(this.frmCardScanning_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.pnlInsuranceDetails.ResumeLayout(false);
            this.pnlInsuranceDetails.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.PnlNewPatScan.ResumeLayout(false);
            this.pnlNewPatDetails.ResumeLayout(false);
            this.pnlNewPatDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_NewPatient)).EndInit();
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.pnlLicenseDetails.ResumeLayout(false);
            this.pnlNewDetails.ResumeLayout(false);
            this.pnlNewDetails.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlTemplateDetailsHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicNew)).EndInit();
            this.pnlPreviousDetails.ResumeLayout(false);
            this.pnlPreviousDetails.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFaceImage)).EndInit();
            this.pnlCardLeft.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_FrontSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BackSide)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal System.Windows.Forms.ToolStripButton tsb_InsuranceCard;
        internal System.Windows.Forms.ToolStripButton tsb_DriversLicense;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.PictureBox pb_BackSide;
        private System.Windows.Forms.PictureBox pbFaceImage;
        private System.Windows.Forms.PictureBox pb_FrontSide;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Panel pnlInsuranceDetails;
        private System.Windows.Forms.TextBox txtIns_GroupNo;
        private System.Windows.Forms.TextBox txtIns_MemberName;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Panel pnlLicenseDetails;
        private System.Windows.Forms.TextBox txtLicCity;
        private System.Windows.Forms.TextBox txtLicAddress;
        private System.Windows.Forms.TextBox txtLicLastName;
        private System.Windows.Forms.TextBox txtLicFirstName;
        private System.Windows.Forms.Label lbl_PrevState;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel pnlCardLeft;
        private System.Windows.Forms.TextBox txtLicZip;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtIns_MemberID;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        internal System.Windows.Forms.ToolStripButton tsb_ClearData;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox txtLicMiddleName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbInsOther;
        private System.Windows.Forms.RadioButton rbInsFemale;
        private System.Windows.Forms.RadioButton rbInsMale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label lblCountry;
        internal System.Windows.Forms.ToolStripButton tsb_Cheque;
        private System.Windows.Forms.MaskedTextBox txtLicDOB;
        private System.Windows.Forms.TextBox txtCopay;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        //private System.Windows.Forms.PrintDialog printDialog1;
        //private System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.Panel pnlNewDetails;
        private System.Windows.Forms.Panel pnlPreviousDetails;
        private System.Windows.Forms.MaskedTextBox txt_NewDOB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label lbl_NewState;
        private System.Windows.Forms.TextBox txt_NewPatientcode;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.TextBox txt_NewAddress1;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txt_NewCity;
        private System.Windows.Forms.TextBox txt_NewZip;
        private System.Windows.Forms.PictureBox PicNew;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.ComboBox cmbNewGender;
        private System.Windows.Forms.ComboBox cmbLicGender;
        private System.Windows.Forms.ComboBox cmbNewCountry;
        private System.Windows.Forms.ComboBox cmbLicCountry;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.TextBox txt_NewMIname;
        private System.Windows.Forms.TextBox txt_NewFname;
        private System.Windows.Forms.TextBox txt_NewLastname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.CheckBox chk_Address;
        private System.Windows.Forms.CheckBox chk_Sex;
        private System.Windows.Forms.CheckBox chk_DOB;
        private System.Windows.Forms.CheckBox chk_Patientname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlTemplateDetailsHeader;
        private System.Windows.Forms.Label lblCardDetails;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.TextBox txtLicAddress2;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.TextBox txt_NewAddress2;
        private System.Windows.Forms.TextBox txt_NewCounty;
        private System.Windows.Forms.ComboBox cmbNewState;
        private System.Windows.Forms.ComboBox cmbLicState;
        internal System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Label lbl_NewCounty;
        private System.Windows.Forms.Label lblLic_County;
        private System.Windows.Forms.TextBox txtLic_County;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel PnlNewPatScan;
        private System.Windows.Forms.Panel pnlNewPatDetails;
        private System.Windows.Forms.Label label53;
        internal System.Windows.Forms.Panel pnlInternalControl_New;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label lblNew_County;
        private System.Windows.Forms.ComboBox cmbNew_State;
        private System.Windows.Forms.TextBox txtNew_County;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.TextBox txtNew_Address2;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.TextBox txtNew_MIName;
        private System.Windows.Forms.TextBox txtNew_FirstName;
        private System.Windows.Forms.TextBox txtNew_LastName;
        private System.Windows.Forms.ComboBox cmbNew_Country;
        private System.Windows.Forms.ComboBox cmbNew_Gender;
        private System.Windows.Forms.MaskedTextBox txtNew_DOB;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.TextBox txtNew_Address1;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.TextBox txtNew_City;
        private System.Windows.Forms.TextBox txtNew_Zip;
        private System.Windows.Forms.PictureBox Pic_NewPatient;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.ComboBox cmbIns_PlanProvider;
        internal System.Windows.Forms.MaskedTextBox txtNewCode;
        private System.Windows.Forms.TextBox txtLic_PatientCode;
        private System.Windows.Forms.CheckBox chk_Photo;
        private System.Windows.Forms.Label lbl_ScanMessage;
        private System.Windows.Forms.Button btnInsBrowse;
        private System.Windows.Forms.ToolTip toolTipInsurance;
        private System.Windows.Forms.ToolTip tooltipZip;


    }
}