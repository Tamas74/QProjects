namespace gloBilling
{
    partial class frmEDI276Generation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDI276Generation));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnGenerateEDI = new System.Windows.Forms.ToolStripButton();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tb_EDIDataDetails = new System.Windows.Forms.TabControl();
            this.tbpg_EDIDetails = new System.Windows.Forms.TabPage();
            this.grp_BHT = new System.Windows.Forms.GroupBox();
            this.cmbTypeOfTransaction = new System.Windows.Forms.ComboBox();
            this.txtBHTTime = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBHTDate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBHTRefIdentification = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.lblBHT_transactionType = new System.Windows.Forms.Label();
            this.txtBHT_HerarchicalStrCode = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.grp_TransactionSet = new System.Windows.Forms.GroupBox();
            this.txtTSControlNumber = new System.Windows.Forms.TextBox();
            this.lblTSControlNumber = new System.Windows.Forms.Label();
            this.txtTSIdCode = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.grp_FunctionalGroup = new System.Windows.Forms.GroupBox();
            this.txtFuncGroupTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFunGroupDate = new System.Windows.Forms.TextBox();
            this.txtFunctionID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReceiverDept = new System.Windows.Forms.TextBox();
            this.lblSenderDept = new System.Windows.Forms.Label();
            this.lblFunctionID = new System.Windows.Forms.Label();
            this.txtSenderDept = new System.Windows.Forms.TextBox();
            this.grp_ISASegment = new System.Windows.Forms.GroupBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSenderID1 = new System.Windows.Forms.TextBox();
            this.txtReferenceID1 = new System.Windows.Forms.TextBox();
            this.txtInquiryDate = new System.Windows.Forms.TextBox();
            this.lblSenderID = new System.Windows.Forms.Label();
            this.lblReferenceID = new System.Windows.Forms.Label();
            this.txtReceiverID1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInquiryTime = new System.Windows.Forms.TextBox();
            this.lblInquiryTime = new System.Windows.Forms.Label();
            this.txtControlNo = new System.Windows.Forms.TextBox();
            this.lblControlNo = new System.Windows.Forms.Label();
            this.tbpg_SourceAndReceiver = new System.Windows.Forms.TabPage();
            this.grp_InformationReceiver = new System.Windows.Forms.GroupBox();
            this.txtRecMiddleName = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.txtRecFirstName = new System.Windows.Forms.TextBox();
            this.label116 = new System.Windows.Forms.Label();
            this.cmbInfoReceiverEntityQualifier = new System.Windows.Forms.ComboBox();
            this.cmbRecIDCodeQualifier = new System.Windows.Forms.ComboBox();
            this.label93 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.txtInfoRecEntityQualifier = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.txtRecIDQualifier = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.txtRecLastName = new System.Windows.Forms.TextBox();
            this.label110 = new System.Windows.Forms.Label();
            this.txtInfoReceiverLevel = new System.Windows.Forms.TextBox();
            this.label111 = new System.Windows.Forms.Label();
            this.grp_InformationSource = new System.Windows.Forms.GroupBox();
            this.cmbInfoSourCommQualCode2 = new System.Windows.Forms.ComboBox();
            this.label86 = new System.Windows.Forms.Label();
            this.txtCommNoFaxNo = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.cmbInfoSourCommQualCode = new System.Windows.Forms.ComboBox();
            this.cmbInfoSourceEntityQualifierCode = new System.Windows.Forms.ComboBox();
            this.cmbInfoSourceContactQual = new System.Windows.Forms.ComboBox();
            this.cmbInfoSourceIDCodeQualifier = new System.Windows.Forms.ComboBox();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.txtInfoSourceQualifier = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.txtInfoSourceIDCodeQualifierPayerID = new System.Windows.Forms.TextBox();
            this.label104 = new System.Windows.Forms.Label();
            this.txtInfoSourceName = new System.Windows.Forms.TextBox();
            this.label109 = new System.Windows.Forms.Label();
            this.txtInfoSourceLevel = new System.Windows.Forms.TextBox();
            this.lblInfoSourceLevel = new System.Windows.Forms.Label();
            this.textBox51 = new System.Windows.Forms.TextBox();
            this.label112 = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this.label113 = new System.Windows.Forms.Label();
            this.tbpg_SubscriberDetails = new System.Windows.Forms.TabPage();
            this.grp_Provider = new System.Windows.Forms.GroupBox();
            this.label80 = new System.Windows.Forms.Label();
            this.txtProviderLevelCode = new System.Windows.Forms.TextBox();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.cmbProvEntityQualCode = new System.Windows.Forms.ComboBox();
            this.cmbProvIdentificationCode = new System.Windows.Forms.ComboBox();
            this.label101 = new System.Windows.Forms.Label();
            this.txtProviderIDNo = new System.Windows.Forms.TextBox();
            this.txtProviderMName = new System.Windows.Forms.TextBox();
            this.txtProviderLName = new System.Windows.Forms.TextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.txtProviderFName = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.txtProvEntityQualifier = new System.Windows.Forms.TextBox();
            this.label108 = new System.Windows.Forms.Label();
            this.grp_Subscriber = new System.Windows.Forms.GroupBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.txtSubscriberTraceType = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cmbReferenceIDQualifier = new System.Windows.Forms.ComboBox();
            this.cmbServiceLineDateQualifier = new System.Windows.Forms.ComboBox();
            this.cmbServiceLineItemQualifier = new System.Windows.Forms.ComboBox();
            this.cmbServiceIDQualifierCode = new System.Windows.Forms.ComboBox();
            this.cmbDateQualifierCode = new System.Windows.Forms.ComboBox();
            this.cmbAmountQualifierCode = new System.Windows.Forms.ComboBox();
            this.cmbMedicalIDQualifier = new System.Windows.Forms.ComboBox();
            this.cmbBillTypeIDQualifier = new System.Windows.Forms.ComboBox();
            this.cmbSubscriberEntityQualifierCode = new System.Windows.Forms.ComboBox();
            this.label122 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.cmbSubscriberIDQualifier = new System.Windows.Forms.ComboBox();
            this.label118 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txtSubscriberEntityQualifier = new System.Windows.Forms.TextBox();
            this.txtServiceAmount = new System.Windows.Forms.TextBox();
            this.txtServiceLineDate = new System.Windows.Forms.TextBox();
            this.txtLineItemControlNo = new System.Windows.Forms.TextBox();
            this.txtServiceIDQualifier = new System.Windows.Forms.TextBox();
            this.label121 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.txtSubscriberGender = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtServiceDate = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtSubscriberDOB = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lblIdentifierCode = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.txtSubscriberFName = new System.Windows.Forms.TextBox();
            this.txtSubscriberLName = new System.Windows.Forms.TextBox();
            this.lblSubscriberName = new System.Windows.Forms.Label();
            this.txtSubscriberIDQualifier = new System.Windows.Forms.TextBox();
            this.txtSubscriberChildCode = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtSubscriberMName = new System.Windows.Forms.TextBox();
            this.txtAmountQualifier = new System.Windows.Forms.TextBox();
            this.txtMedicalIDQual = new System.Windows.Forms.TextBox();
            this.txtBillTypeID = new System.Windows.Forms.TextBox();
            this.txtPayerClaimNo = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.txtSubscriberLevelCode = new System.Windows.Forms.TextBox();
            this.txtSubscriberTraceNo = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtSenderID = new System.Windows.Forms.TextBox();
            this.txtReferenceID = new System.Windows.Forms.TextBox();
            this.txtEnquiryDate = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtReceiverID = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtEnquiryTime = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tb_EDIDataDetails.SuspendLayout();
            this.tbpg_EDIDetails.SuspendLayout();
            this.grp_BHT.SuspendLayout();
            this.grp_TransactionSet.SuspendLayout();
            this.grp_FunctionalGroup.SuspendLayout();
            this.grp_ISASegment.SuspendLayout();
            this.tbpg_SourceAndReceiver.SuspendLayout();
            this.grp_InformationReceiver.SuspendLayout();
            this.grp_InformationSource.SuspendLayout();
            this.tbpg_SubscriberDetails.SuspendLayout();
            this.grp_Provider.SuspendLayout();
            this.grp_Subscriber.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1077, 54);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnGenerateEDI,
            this.tls_btnOK,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(1077, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.Text = "toolStrip1";
            // 
            // ts_btnGenerateEDI
            // 
            this.ts_btnGenerateEDI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnGenerateEDI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnGenerateEDI.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnGenerateEDI.Image")));
            this.ts_btnGenerateEDI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnGenerateEDI.Name = "ts_btnGenerateEDI";
            this.ts_btnGenerateEDI.Size = new System.Drawing.Size(91, 50);
            this.ts_btnGenerateEDI.Tag = "GenerateEDI";
            this.ts_btnGenerateEDI.Text = "&Generate EDI";
            this.ts_btnGenerateEDI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnGenerateEDI.ToolTipText = "Generate EDI";
            this.ts_btnGenerateEDI.Click += new System.EventHandler(this.ts_btnGenerateEDI_Click);
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tls_btnOK.Tag = "OK";
            this.tls_btnOK.Text = "&Save&&Cls";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.ToolTipText = "Save and Close";
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tb_EDIDataDetails);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(1077, 652);
            this.pnlMain.TabIndex = 0;
            // 
            // tb_EDIDataDetails
            // 
            this.tb_EDIDataDetails.Controls.Add(this.tbpg_EDIDetails);
            this.tb_EDIDataDetails.Controls.Add(this.tbpg_SourceAndReceiver);
            this.tb_EDIDataDetails.Controls.Add(this.tbpg_SubscriberDetails);
            this.tb_EDIDataDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_EDIDataDetails.Location = new System.Drawing.Point(3, 3);
            this.tb_EDIDataDetails.Name = "tb_EDIDataDetails";
            this.tb_EDIDataDetails.SelectedIndex = 0;
            this.tb_EDIDataDetails.Size = new System.Drawing.Size(1071, 646);
            this.tb_EDIDataDetails.TabIndex = 0;
            // 
            // tbpg_EDIDetails
            // 
            this.tbpg_EDIDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_EDIDetails.Controls.Add(this.grp_BHT);
            this.tbpg_EDIDetails.Controls.Add(this.grp_TransactionSet);
            this.tbpg_EDIDetails.Controls.Add(this.grp_FunctionalGroup);
            this.tbpg_EDIDetails.Controls.Add(this.grp_ISASegment);
            this.tbpg_EDIDetails.Location = new System.Drawing.Point(4, 23);
            this.tbpg_EDIDetails.Name = "tbpg_EDIDetails";
            this.tbpg_EDIDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_EDIDetails.Size = new System.Drawing.Size(1063, 619);
            this.tbpg_EDIDetails.TabIndex = 4;
            this.tbpg_EDIDetails.Tag = "Segment";
            this.tbpg_EDIDetails.Text = "Segment Details ";
            this.tbpg_EDIDetails.UseVisualStyleBackColor = true;
            // 
            // grp_BHT
            // 
            this.grp_BHT.Controls.Add(this.cmbTypeOfTransaction);
            this.grp_BHT.Controls.Add(this.txtBHTTime);
            this.grp_BHT.Controls.Add(this.label30);
            this.grp_BHT.Controls.Add(this.label9);
            this.grp_BHT.Controls.Add(this.txtBHTDate);
            this.grp_BHT.Controls.Add(this.label8);
            this.grp_BHT.Controls.Add(this.txtBHTRefIdentification);
            this.grp_BHT.Controls.Add(this.label52);
            this.grp_BHT.Controls.Add(this.lblBHT_transactionType);
            this.grp_BHT.Controls.Add(this.txtBHT_HerarchicalStrCode);
            this.grp_BHT.Controls.Add(this.label54);
            this.grp_BHT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_BHT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_BHT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_BHT.Location = new System.Drawing.Point(3, 284);
            this.grp_BHT.Name = "grp_BHT";
            this.grp_BHT.Size = new System.Drawing.Size(1057, 332);
            this.grp_BHT.TabIndex = 3;
            this.grp_BHT.TabStop = false;
            this.grp_BHT.Text = "BHT";
            // 
            // cmbTypeOfTransaction
            // 
            this.cmbTypeOfTransaction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeOfTransaction.ForeColor = System.Drawing.Color.Black;
            this.cmbTypeOfTransaction.FormattingEnabled = true;
            this.cmbTypeOfTransaction.Items.AddRange(new object[] {
            "13-Request",
            "01-Cancellation",
            "36- Authority to Deduct(Reply)"});
            this.cmbTypeOfTransaction.Location = new System.Drawing.Point(183, 104);
            this.cmbTypeOfTransaction.Name = "cmbTypeOfTransaction";
            this.cmbTypeOfTransaction.Size = new System.Drawing.Size(155, 22);
            this.cmbTypeOfTransaction.TabIndex = 4;
            // 
            // txtBHTTime
            // 
            this.txtBHTTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBHTTime.ForeColor = System.Drawing.Color.Black;
            this.txtBHTTime.Location = new System.Drawing.Point(488, 69);
            this.txtBHTTime.MaxLength = 80;
            this.txtBHTTime.Name = "txtBHTTime";
            this.txtBHTTime.Size = new System.Drawing.Size(73, 22);
            this.txtBHTTime.TabIndex = 3;
            this.txtBHTTime.Text = "1050";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(338, 108);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(55, 14);
            this.label30.TabIndex = 19;
            this.label30.Text = "(Use 13)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(444, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 14);
            this.label9.TabIndex = 19;
            this.label9.Text = "Time :";
            // 
            // txtBHTDate
            // 
            this.txtBHTDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBHTDate.ForeColor = System.Drawing.Color.Black;
            this.txtBHTDate.Location = new System.Drawing.Point(183, 69);
            this.txtBHTDate.MaxLength = 80;
            this.txtBHTDate.Name = "txtBHTDate";
            this.txtBHTDate.Size = new System.Drawing.Size(122, 22);
            this.txtBHTDate.TabIndex = 2;
            this.txtBHTDate.Text = "20080916";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(61, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 14);
            this.label8.TabIndex = 17;
            this.label8.Text = "Date [CC]YYMMDD :";
            // 
            // txtBHTRefIdentification
            // 
            this.txtBHTRefIdentification.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBHTRefIdentification.ForeColor = System.Drawing.Color.Black;
            this.txtBHTRefIdentification.Location = new System.Drawing.Point(488, 34);
            this.txtBHTRefIdentification.MaxLength = 80;
            this.txtBHTRefIdentification.Name = "txtBHTRefIdentification";
            this.txtBHTRefIdentification.Size = new System.Drawing.Size(73, 22);
            this.txtBHTRefIdentification.TabIndex = 1;
            this.txtBHTRefIdentification.Text = "1234";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(346, 38);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(140, 14);
            this.label52.TabIndex = 15;
            this.label52.Text = "Reference Identication :";
            // 
            // lblBHT_transactionType
            // 
            this.lblBHT_transactionType.AutoSize = true;
            this.lblBHT_transactionType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBHT_transactionType.Location = new System.Drawing.Point(55, 108);
            this.lblBHT_transactionType.Name = "lblBHT_transactionType";
            this.lblBHT_transactionType.Size = new System.Drawing.Size(125, 14);
            this.lblBHT_transactionType.TabIndex = 13;
            this.lblBHT_transactionType.Text = "Type of Transaction :";
            // 
            // txtBHT_HerarchicalStrCode
            // 
            this.txtBHT_HerarchicalStrCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBHT_HerarchicalStrCode.ForeColor = System.Drawing.Color.Black;
            this.txtBHT_HerarchicalStrCode.Location = new System.Drawing.Point(183, 34);
            this.txtBHT_HerarchicalStrCode.MaxLength = 35;
            this.txtBHT_HerarchicalStrCode.Name = "txtBHT_HerarchicalStrCode";
            this.txtBHT_HerarchicalStrCode.Size = new System.Drawing.Size(122, 22);
            this.txtBHT_HerarchicalStrCode.TabIndex = 0;
            this.txtBHT_HerarchicalStrCode.Text = "0022";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(19, 38);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(161, 14);
            this.label54.TabIndex = 13;
            this.label54.Text = "Herarchical Structure Code :";
            // 
            // grp_TransactionSet
            // 
            this.grp_TransactionSet.Controls.Add(this.txtTSControlNumber);
            this.grp_TransactionSet.Controls.Add(this.lblTSControlNumber);
            this.grp_TransactionSet.Controls.Add(this.txtTSIdCode);
            this.grp_TransactionSet.Controls.Add(this.label53);
            this.grp_TransactionSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_TransactionSet.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_TransactionSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_TransactionSet.Location = new System.Drawing.Point(3, 215);
            this.grp_TransactionSet.Name = "grp_TransactionSet";
            this.grp_TransactionSet.Size = new System.Drawing.Size(1057, 69);
            this.grp_TransactionSet.TabIndex = 2;
            this.grp_TransactionSet.TabStop = false;
            this.grp_TransactionSet.Text = "Transaction Set";
            // 
            // txtTSControlNumber
            // 
            this.txtTSControlNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTSControlNumber.ForeColor = System.Drawing.Color.Black;
            this.txtTSControlNumber.Location = new System.Drawing.Point(440, 27);
            this.txtTSControlNumber.MaxLength = 80;
            this.txtTSControlNumber.Name = "txtTSControlNumber";
            this.txtTSControlNumber.Size = new System.Drawing.Size(144, 22);
            this.txtTSControlNumber.TabIndex = 1;
            this.txtTSControlNumber.Text = "100001";
            // 
            // lblTSControlNumber
            // 
            this.lblTSControlNumber.AutoSize = true;
            this.lblTSControlNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTSControlNumber.Location = new System.Drawing.Point(317, 31);
            this.lblTSControlNumber.Name = "lblTSControlNumber";
            this.lblTSControlNumber.Size = new System.Drawing.Size(120, 14);
            this.lblTSControlNumber.TabIndex = 15;
            this.lblTSControlNumber.Text = "TS Control Number :";
            // 
            // txtTSIdCode
            // 
            this.txtTSIdCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTSIdCode.ForeColor = System.Drawing.Color.Black;
            this.txtTSIdCode.Location = new System.Drawing.Point(158, 27);
            this.txtTSIdCode.MaxLength = 35;
            this.txtTSIdCode.Name = "txtTSIdCode";
            this.txtTSIdCode.Size = new System.Drawing.Size(122, 22);
            this.txtTSIdCode.TabIndex = 0;
            this.txtTSIdCode.Text = "270";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(18, 31);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(137, 14);
            this.label53.TabIndex = 13;
            this.label53.Text = "TS Identification Code :";
            // 
            // grp_FunctionalGroup
            // 
            this.grp_FunctionalGroup.Controls.Add(this.txtFuncGroupTime);
            this.grp_FunctionalGroup.Controls.Add(this.label6);
            this.grp_FunctionalGroup.Controls.Add(this.label5);
            this.grp_FunctionalGroup.Controls.Add(this.txtFunGroupDate);
            this.grp_FunctionalGroup.Controls.Add(this.txtFunctionID);
            this.grp_FunctionalGroup.Controls.Add(this.label4);
            this.grp_FunctionalGroup.Controls.Add(this.txtReceiverDept);
            this.grp_FunctionalGroup.Controls.Add(this.lblSenderDept);
            this.grp_FunctionalGroup.Controls.Add(this.lblFunctionID);
            this.grp_FunctionalGroup.Controls.Add(this.txtSenderDept);
            this.grp_FunctionalGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_FunctionalGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_FunctionalGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_FunctionalGroup.Location = new System.Drawing.Point(3, 112);
            this.grp_FunctionalGroup.Name = "grp_FunctionalGroup";
            this.grp_FunctionalGroup.Size = new System.Drawing.Size(1057, 103);
            this.grp_FunctionalGroup.TabIndex = 1;
            this.grp_FunctionalGroup.TabStop = false;
            this.grp_FunctionalGroup.Text = "Functional Group";
            // 
            // txtFuncGroupTime
            // 
            this.txtFuncGroupTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFuncGroupTime.ForeColor = System.Drawing.Color.Black;
            this.txtFuncGroupTime.Location = new System.Drawing.Point(101, 58);
            this.txtFuncGroupTime.MaxLength = 4;
            this.txtFuncGroupTime.Name = "txtFuncGroupTime";
            this.txtFuncGroupTime.Size = new System.Drawing.Size(139, 22);
            this.txtFuncGroupTime.TabIndex = 3;
            this.txtFuncGroupTime.Text = "1300";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(56, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 64;
            this.label6.Text = "Time :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(300, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 14);
            this.label5.TabIndex = 62;
            this.label5.Text = "Date [CC]YYMMDD :";
            // 
            // txtFunGroupDate
            // 
            this.txtFunGroupDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFunGroupDate.ForeColor = System.Drawing.Color.Black;
            this.txtFunGroupDate.Location = new System.Drawing.Point(422, 58);
            this.txtFunGroupDate.MaxLength = 6;
            this.txtFunGroupDate.Name = "txtFunGroupDate";
            this.txtFunGroupDate.Size = new System.Drawing.Size(139, 22);
            this.txtFunGroupDate.TabIndex = 4;
            this.txtFunGroupDate.Text = "080916";
            // 
            // txtFunctionID
            // 
            this.txtFunctionID.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtFunctionID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFunctionID.ForeColor = System.Drawing.Color.Black;
            this.txtFunctionID.Location = new System.Drawing.Point(101, 28);
            this.txtFunctionID.MaxLength = 2;
            this.txtFunctionID.Name = "txtFunctionID";
            this.txtFunctionID.ReadOnly = true;
            this.txtFunctionID.Size = new System.Drawing.Size(139, 22);
            this.txtFunctionID.TabIndex = 0;
            this.txtFunctionID.Text = "HS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(595, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Receiver Dept :";
            // 
            // txtReceiverDept
            // 
            this.txtReceiverDept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiverDept.ForeColor = System.Drawing.Color.Black;
            this.txtReceiverDept.Location = new System.Drawing.Point(690, 28);
            this.txtReceiverDept.MaxLength = 15;
            this.txtReceiverDept.Name = "txtReceiverDept";
            this.txtReceiverDept.Size = new System.Drawing.Size(139, 22);
            this.txtReceiverDept.TabIndex = 2;
            this.txtReceiverDept.Text = "Rec. Department";
            // 
            // lblSenderDept
            // 
            this.lblSenderDept.AutoSize = true;
            this.lblSenderDept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderDept.Location = new System.Drawing.Point(334, 32);
            this.lblSenderDept.Name = "lblSenderDept";
            this.lblSenderDept.Size = new System.Drawing.Size(85, 14);
            this.lblSenderDept.TabIndex = 5;
            this.lblSenderDept.Text = "Sender Dept :";
            // 
            // lblFunctionID
            // 
            this.lblFunctionID.AutoSize = true;
            this.lblFunctionID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFunctionID.Location = new System.Drawing.Point(20, 32);
            this.lblFunctionID.Name = "lblFunctionID";
            this.lblFunctionID.Size = new System.Drawing.Size(78, 14);
            this.lblFunctionID.TabIndex = 58;
            this.lblFunctionID.Text = "Function ID :";
            // 
            // txtSenderDept
            // 
            this.txtSenderDept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenderDept.ForeColor = System.Drawing.Color.Black;
            this.txtSenderDept.Location = new System.Drawing.Point(422, 28);
            this.txtSenderDept.MaxLength = 15;
            this.txtSenderDept.Name = "txtSenderDept";
            this.txtSenderDept.Size = new System.Drawing.Size(139, 22);
            this.txtSenderDept.TabIndex = 1;
            this.txtSenderDept.Text = "Demo Department";
            // 
            // grp_ISASegment
            // 
            this.grp_ISASegment.Controls.Add(this.label48);
            this.grp_ISASegment.Controls.Add(this.label3);
            this.grp_ISASegment.Controls.Add(this.txtSenderID1);
            this.grp_ISASegment.Controls.Add(this.txtReferenceID1);
            this.grp_ISASegment.Controls.Add(this.txtInquiryDate);
            this.grp_ISASegment.Controls.Add(this.lblSenderID);
            this.grp_ISASegment.Controls.Add(this.lblReferenceID);
            this.grp_ISASegment.Controls.Add(this.txtReceiverID1);
            this.grp_ISASegment.Controls.Add(this.label2);
            this.grp_ISASegment.Controls.Add(this.txtInquiryTime);
            this.grp_ISASegment.Controls.Add(this.lblInquiryTime);
            this.grp_ISASegment.Controls.Add(this.txtControlNo);
            this.grp_ISASegment.Controls.Add(this.lblControlNo);
            this.grp_ISASegment.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_ISASegment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_ISASegment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_ISASegment.Location = new System.Drawing.Point(3, 3);
            this.grp_ISASegment.Name = "grp_ISASegment";
            this.grp_ISASegment.Size = new System.Drawing.Size(1057, 109);
            this.grp_ISASegment.TabIndex = 0;
            this.grp_ISASegment.TabStop = false;
            this.grp_ISASegment.Text = "ISA Segment";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(807, 31);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(0, 14);
            this.label48.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(502, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 14);
            this.label3.TabIndex = 60;
            this.label3.Text = "Inquiry Date [CC]YYMMDD :";
            // 
            // txtSenderID1
            // 
            this.txtSenderID1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenderID1.ForeColor = System.Drawing.Color.Black;
            this.txtSenderID1.Location = new System.Drawing.Point(97, 27);
            this.txtSenderID1.MaxLength = 15;
            this.txtSenderID1.Name = "txtSenderID1";
            this.txtSenderID1.Size = new System.Drawing.Size(139, 22);
            this.txtSenderID1.TabIndex = 0;
            this.txtSenderID1.Text = "12345";
            // 
            // txtReferenceID1
            // 
            this.txtReferenceID1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferenceID1.ForeColor = System.Drawing.Color.Black;
            this.txtReferenceID1.Location = new System.Drawing.Point(339, 61);
            this.txtReferenceID1.Name = "txtReferenceID1";
            this.txtReferenceID1.Size = new System.Drawing.Size(139, 22);
            this.txtReferenceID1.TabIndex = 4;
            this.txtReferenceID1.Text = "12121";
            // 
            // txtInquiryDate
            // 
            this.txtInquiryDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInquiryDate.ForeColor = System.Drawing.Color.Black;
            this.txtInquiryDate.Location = new System.Drawing.Point(665, 27);
            this.txtInquiryDate.MaxLength = 6;
            this.txtInquiryDate.Name = "txtInquiryDate";
            this.txtInquiryDate.Size = new System.Drawing.Size(139, 22);
            this.txtInquiryDate.TabIndex = 2;
            this.txtInquiryDate.Text = "080916";
            // 
            // lblSenderID
            // 
            this.lblSenderID.AutoSize = true;
            this.lblSenderID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderID.Location = new System.Drawing.Point(24, 31);
            this.lblSenderID.Name = "lblSenderID";
            this.lblSenderID.Size = new System.Drawing.Size(70, 14);
            this.lblSenderID.TabIndex = 1;
            this.lblSenderID.Text = "Sender ID :";
            // 
            // lblReferenceID
            // 
            this.lblReferenceID.AutoSize = true;
            this.lblReferenceID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferenceID.Location = new System.Drawing.Point(249, 65);
            this.lblReferenceID.Name = "lblReferenceID";
            this.lblReferenceID.Size = new System.Drawing.Size(87, 14);
            this.lblReferenceID.TabIndex = 32;
            this.lblReferenceID.Text = "Reference ID :";
            // 
            // txtReceiverID1
            // 
            this.txtReceiverID1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiverID1.ForeColor = System.Drawing.Color.Black;
            this.txtReceiverID1.Location = new System.Drawing.Point(339, 27);
            this.txtReceiverID1.MaxLength = 15;
            this.txtReceiverID1.Name = "txtReceiverID1";
            this.txtReceiverID1.Size = new System.Drawing.Size(139, 22);
            this.txtReceiverID1.TabIndex = 1;
            this.txtReceiverID1.Text = "REC1001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(259, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Receiver ID :";
            // 
            // txtInquiryTime
            // 
            this.txtInquiryTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInquiryTime.ForeColor = System.Drawing.Color.Black;
            this.txtInquiryTime.Location = new System.Drawing.Point(665, 61);
            this.txtInquiryTime.MaxLength = 4;
            this.txtInquiryTime.Name = "txtInquiryTime";
            this.txtInquiryTime.Size = new System.Drawing.Size(139, 22);
            this.txtInquiryTime.TabIndex = 5;
            this.txtInquiryTime.Text = "1300";
            // 
            // lblInquiryTime
            // 
            this.lblInquiryTime.AutoSize = true;
            this.lblInquiryTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInquiryTime.Location = new System.Drawing.Point(579, 65);
            this.lblInquiryTime.Name = "lblInquiryTime";
            this.lblInquiryTime.Size = new System.Drawing.Size(83, 14);
            this.lblInquiryTime.TabIndex = 26;
            this.lblInquiryTime.Text = "Inquiry Time :";
            // 
            // txtControlNo
            // 
            this.txtControlNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtControlNo.ForeColor = System.Drawing.Color.Black;
            this.txtControlNo.Location = new System.Drawing.Point(97, 61);
            this.txtControlNo.MaxLength = 9;
            this.txtControlNo.Name = "txtControlNo";
            this.txtControlNo.Size = new System.Drawing.Size(139, 22);
            this.txtControlNo.TabIndex = 3;
            this.txtControlNo.Text = "000021";
            // 
            // lblControlNo
            // 
            this.lblControlNo.AutoSize = true;
            this.lblControlNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlNo.Location = new System.Drawing.Point(21, 65);
            this.lblControlNo.Name = "lblControlNo";
            this.lblControlNo.Size = new System.Drawing.Size(73, 14);
            this.lblControlNo.TabIndex = 28;
            this.lblControlNo.Text = "Control No :";
            // 
            // tbpg_SourceAndReceiver
            // 
            this.tbpg_SourceAndReceiver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_SourceAndReceiver.Controls.Add(this.grp_InformationReceiver);
            this.tbpg_SourceAndReceiver.Controls.Add(this.grp_InformationSource);
            this.tbpg_SourceAndReceiver.Location = new System.Drawing.Point(4, 23);
            this.tbpg_SourceAndReceiver.Name = "tbpg_SourceAndReceiver";
            this.tbpg_SourceAndReceiver.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_SourceAndReceiver.Size = new System.Drawing.Size(1063, 619);
            this.tbpg_SourceAndReceiver.TabIndex = 3;
            this.tbpg_SourceAndReceiver.Tag = "Source And Receiver";
            this.tbpg_SourceAndReceiver.Text = "Source And Receiver";
            this.tbpg_SourceAndReceiver.UseVisualStyleBackColor = true;
            // 
            // grp_InformationReceiver
            // 
            this.grp_InformationReceiver.Controls.Add(this.txtRecMiddleName);
            this.grp_InformationReceiver.Controls.Add(this.label117);
            this.grp_InformationReceiver.Controls.Add(this.txtRecFirstName);
            this.grp_InformationReceiver.Controls.Add(this.label116);
            this.grp_InformationReceiver.Controls.Add(this.cmbInfoReceiverEntityQualifier);
            this.grp_InformationReceiver.Controls.Add(this.cmbRecIDCodeQualifier);
            this.grp_InformationReceiver.Controls.Add(this.label93);
            this.grp_InformationReceiver.Controls.Add(this.label95);
            this.grp_InformationReceiver.Controls.Add(this.txtInfoRecEntityQualifier);
            this.grp_InformationReceiver.Controls.Add(this.label96);
            this.grp_InformationReceiver.Controls.Add(this.label97);
            this.grp_InformationReceiver.Controls.Add(this.txtRecIDQualifier);
            this.grp_InformationReceiver.Controls.Add(this.label102);
            this.grp_InformationReceiver.Controls.Add(this.txtRecLastName);
            this.grp_InformationReceiver.Controls.Add(this.label110);
            this.grp_InformationReceiver.Controls.Add(this.txtInfoReceiverLevel);
            this.grp_InformationReceiver.Controls.Add(this.label111);
            this.grp_InformationReceiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_InformationReceiver.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_InformationReceiver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_InformationReceiver.Location = new System.Drawing.Point(3, 254);
            this.grp_InformationReceiver.Name = "grp_InformationReceiver";
            this.grp_InformationReceiver.Size = new System.Drawing.Size(1057, 362);
            this.grp_InformationReceiver.TabIndex = 1;
            this.grp_InformationReceiver.TabStop = false;
            this.grp_InformationReceiver.Text = "Information Receiver";
            // 
            // txtRecMiddleName
            // 
            this.txtRecMiddleName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecMiddleName.ForeColor = System.Drawing.Color.Black;
            this.txtRecMiddleName.Location = new System.Drawing.Point(840, 84);
            this.txtRecMiddleName.MaxLength = 60;
            this.txtRecMiddleName.Name = "txtRecMiddleName";
            this.txtRecMiddleName.Size = new System.Drawing.Size(137, 22);
            this.txtRecMiddleName.TabIndex = 5;
            this.txtRecMiddleName.Text = "M";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label117.Location = new System.Drawing.Point(753, 88);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(84, 14);
            this.label117.TabIndex = 70;
            this.label117.Text = "Middle Name :";
            // 
            // txtRecFirstName
            // 
            this.txtRecFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecFirstName.ForeColor = System.Drawing.Color.Black;
            this.txtRecFirstName.Location = new System.Drawing.Point(566, 84);
            this.txtRecFirstName.MaxLength = 60;
            this.txtRecFirstName.Name = "txtRecFirstName";
            this.txtRecFirstName.Size = new System.Drawing.Size(185, 22);
            this.txtRecFirstName.TabIndex = 4;
            this.txtRecFirstName.Text = "Receiver First Name";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label116.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label116.Location = new System.Drawing.Point(437, 88);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(122, 14);
            this.label116.TabIndex = 68;
            this.label116.Text = "Info Rec First Name :";
            // 
            // cmbInfoReceiverEntityQualifier
            // 
            this.cmbInfoReceiverEntityQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInfoReceiverEntityQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmbInfoReceiverEntityQualifier.FormattingEnabled = true;
            this.cmbInfoReceiverEntityQualifier.Items.AddRange(new object[] {
            "41-Submitter"});
            this.cmbInfoReceiverEntityQualifier.Location = new System.Drawing.Point(201, 53);
            this.cmbInfoReceiverEntityQualifier.Name = "cmbInfoReceiverEntityQualifier";
            this.cmbInfoReceiverEntityQualifier.Size = new System.Drawing.Size(194, 22);
            this.cmbInfoReceiverEntityQualifier.TabIndex = 1;
            // 
            // cmbRecIDCodeQualifier
            // 
            this.cmbRecIDCodeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRecIDCodeQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmbRecIDCodeQualifier.FormattingEnabled = true;
            this.cmbRecIDCodeQualifier.Items.AddRange(new object[] {
            "46- Electronic Transmitter Identification Number (ETIN)",
            "FI- Federal Taxpayer’s Identification Number",
            "XX- Health Care Financing Administration National Provider Identifier"});
            this.cmbRecIDCodeQualifier.Location = new System.Drawing.Point(201, 115);
            this.cmbRecIDCodeQualifier.Name = "cmbRecIDCodeQualifier";
            this.cmbRecIDCodeQualifier.Size = new System.Drawing.Size(194, 22);
            this.cmbRecIDCodeQualifier.TabIndex = 6;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label93.Location = new System.Drawing.Point(7, 57);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(191, 14);
            this.label93.TabIndex = 13;
            this.label93.Text = "Entity Qualifier For Info Receiver :";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label95.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label95.Location = new System.Drawing.Point(12, 119);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(186, 14);
            this.label95.TabIndex = 13;
            this.label95.Text = "Info Receiver ID Code  Qualifier :";
            // 
            // txtInfoRecEntityQualifier
            // 
            this.txtInfoRecEntityQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoRecEntityQualifier.ForeColor = System.Drawing.Color.Black;
            this.txtInfoRecEntityQualifier.Location = new System.Drawing.Point(566, 53);
            this.txtInfoRecEntityQualifier.Name = "txtInfoRecEntityQualifier";
            this.txtInfoRecEntityQualifier.Size = new System.Drawing.Size(42, 22);
            this.txtInfoRecEntityQualifier.TabIndex = 2;
            this.txtInfoRecEntityQualifier.Tag = "";
            this.txtInfoRecEntityQualifier.Text = "1";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label96.Location = new System.Drawing.Point(611, 57);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(147, 14);
            this.label96.TabIndex = 13;
            this.label96.Text = "(1-Person, 2-Non Person)";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label97.Location = new System.Drawing.Point(425, 57);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(134, 14);
            this.label97.TabIndex = 13;
            this.label97.Text = "Info Receiver Qualifier :";
            // 
            // txtRecIDQualifier
            // 
            this.txtRecIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecIDQualifier.ForeColor = System.Drawing.Color.Black;
            this.txtRecIDQualifier.Location = new System.Drawing.Point(566, 115);
            this.txtRecIDQualifier.MaxLength = 80;
            this.txtRecIDQualifier.Name = "txtRecIDQualifier";
            this.txtRecIDQualifier.Size = new System.Drawing.Size(185, 22);
            this.txtRecIDQualifier.TabIndex = 7;
            this.txtRecIDQualifier.Text = "203250";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label102.Location = new System.Drawing.Point(482, 119);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(77, 14);
            this.label102.TabIndex = 34;
            this.label102.Text = "Receiver ID :";
            // 
            // txtRecLastName
            // 
            this.txtRecLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecLastName.ForeColor = System.Drawing.Color.Black;
            this.txtRecLastName.Location = new System.Drawing.Point(201, 84);
            this.txtRecLastName.MaxLength = 60;
            this.txtRecLastName.Name = "txtRecLastName";
            this.txtRecLastName.Size = new System.Drawing.Size(194, 22);
            this.txtRecLastName.TabIndex = 3;
            this.txtRecLastName.Text = "Receiver Last Name";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label110.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label110.Location = new System.Drawing.Point(21, 88);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(177, 14);
            this.label110.TabIndex = 13;
            this.label110.Text = "Info  Receiver Last/Org Name :";
            // 
            // txtInfoReceiverLevel
            // 
            this.txtInfoReceiverLevel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoReceiverLevel.ForeColor = System.Drawing.Color.Black;
            this.txtInfoReceiverLevel.Location = new System.Drawing.Point(201, 22);
            this.txtInfoReceiverLevel.Name = "txtInfoReceiverLevel";
            this.txtInfoReceiverLevel.Size = new System.Drawing.Size(194, 22);
            this.txtInfoReceiverLevel.TabIndex = 0;
            this.txtInfoReceiverLevel.Text = "21";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label111.Location = new System.Drawing.Point(79, 26);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(119, 14);
            this.label111.TabIndex = 15;
            this.label111.Text = "Info Receiver Level :";
            // 
            // grp_InformationSource
            // 
            this.grp_InformationSource.Controls.Add(this.cmbInfoSourCommQualCode2);
            this.grp_InformationSource.Controls.Add(this.label86);
            this.grp_InformationSource.Controls.Add(this.txtCommNoFaxNo);
            this.grp_InformationSource.Controls.Add(this.label89);
            this.grp_InformationSource.Controls.Add(this.textBox38);
            this.grp_InformationSource.Controls.Add(this.label81);
            this.grp_InformationSource.Controls.Add(this.cmbInfoSourCommQualCode);
            this.grp_InformationSource.Controls.Add(this.cmbInfoSourceEntityQualifierCode);
            this.grp_InformationSource.Controls.Add(this.cmbInfoSourceContactQual);
            this.grp_InformationSource.Controls.Add(this.cmbInfoSourceIDCodeQualifier);
            this.grp_InformationSource.Controls.Add(this.label82);
            this.grp_InformationSource.Controls.Add(this.label83);
            this.grp_InformationSource.Controls.Add(this.label84);
            this.grp_InformationSource.Controls.Add(this.label85);
            this.grp_InformationSource.Controls.Add(this.txtInfoSourceQualifier);
            this.grp_InformationSource.Controls.Add(this.label88);
            this.grp_InformationSource.Controls.Add(this.label92);
            this.grp_InformationSource.Controls.Add(this.txtInfoSourceIDCodeQualifierPayerID);
            this.grp_InformationSource.Controls.Add(this.label104);
            this.grp_InformationSource.Controls.Add(this.txtInfoSourceName);
            this.grp_InformationSource.Controls.Add(this.label109);
            this.grp_InformationSource.Controls.Add(this.txtInfoSourceLevel);
            this.grp_InformationSource.Controls.Add(this.lblInfoSourceLevel);
            this.grp_InformationSource.Controls.Add(this.textBox51);
            this.grp_InformationSource.Controls.Add(this.label112);
            this.grp_InformationSource.Controls.Add(this.txtContactName);
            this.grp_InformationSource.Controls.Add(this.label113);
            this.grp_InformationSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_InformationSource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_InformationSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_InformationSource.Location = new System.Drawing.Point(3, 3);
            this.grp_InformationSource.Name = "grp_InformationSource";
            this.grp_InformationSource.Size = new System.Drawing.Size(1057, 251);
            this.grp_InformationSource.TabIndex = 0;
            this.grp_InformationSource.TabStop = false;
            this.grp_InformationSource.Text = "Information Source";
            // 
            // cmbInfoSourCommQualCode2
            // 
            this.cmbInfoSourCommQualCode2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInfoSourCommQualCode2.ForeColor = System.Drawing.Color.Black;
            this.cmbInfoSourCommQualCode2.FormattingEnabled = true;
            this.cmbInfoSourCommQualCode2.Items.AddRange(new object[] {
            "EX- Telephone Extension",
            "FX- Facsimile"});
            this.cmbInfoSourCommQualCode2.Location = new System.Drawing.Point(200, 208);
            this.cmbInfoSourCommQualCode2.Name = "cmbInfoSourCommQualCode2";
            this.cmbInfoSourCommQualCode2.Size = new System.Drawing.Size(194, 22);
            this.cmbInfoSourCommQualCode2.TabIndex = 11;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(23, 211);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(174, 14);
            this.label86.TabIndex = 65;
            this.label86.Text = "Comm Qualifier Code (FX/EX) :";
            // 
            // txtCommNoFaxNo
            // 
            this.txtCommNoFaxNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommNoFaxNo.ForeColor = System.Drawing.Color.Black;
            this.txtCommNoFaxNo.Location = new System.Drawing.Point(563, 208);
            this.txtCommNoFaxNo.MaxLength = 10;
            this.txtCommNoFaxNo.Name = "txtCommNoFaxNo";
            this.txtCommNoFaxNo.Size = new System.Drawing.Size(185, 22);
            this.txtCommNoFaxNo.TabIndex = 12;
            this.txtCommNoFaxNo.Text = "2356455822";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(442, 212);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(118, 14);
            this.label89.TabIndex = 66;
            this.label89.Text = "Comm (Fax/Ex) No :";
            // 
            // textBox38
            // 
            this.textBox38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox38.ForeColor = System.Drawing.Color.Black;
            this.textBox38.Location = new System.Drawing.Point(840, 177);
            this.textBox38.MaxLength = 4;
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(194, 22);
            this.textBox38.TabIndex = 10;
            this.textBox38.Text = "8334";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(778, 181);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(59, 14);
            this.label81.TabIndex = 63;
            this.label81.Text = "Extn No :";
            // 
            // cmbInfoSourCommQualCode
            // 
            this.cmbInfoSourCommQualCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInfoSourCommQualCode.ForeColor = System.Drawing.Color.Black;
            this.cmbInfoSourCommQualCode.FormattingEnabled = true;
            this.cmbInfoSourCommQualCode.Items.AddRange(new object[] {
            "ED- Electronic Data Interchange Access Number",
            "EM- Electronic Mail",
            "TE- Telephone"});
            this.cmbInfoSourCommQualCode.Location = new System.Drawing.Point(200, 177);
            this.cmbInfoSourCommQualCode.Name = "cmbInfoSourCommQualCode";
            this.cmbInfoSourCommQualCode.Size = new System.Drawing.Size(194, 22);
            this.cmbInfoSourCommQualCode.TabIndex = 8;
            // 
            // cmbInfoSourceEntityQualifierCode
            // 
            this.cmbInfoSourceEntityQualifierCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInfoSourceEntityQualifierCode.ForeColor = System.Drawing.Color.Black;
            this.cmbInfoSourceEntityQualifierCode.FormattingEnabled = true;
            this.cmbInfoSourceEntityQualifierCode.Items.AddRange(new object[] {
            "PR-Payer"});
            this.cmbInfoSourceEntityQualifierCode.Location = new System.Drawing.Point(200, 53);
            this.cmbInfoSourceEntityQualifierCode.Name = "cmbInfoSourceEntityQualifierCode";
            this.cmbInfoSourceEntityQualifierCode.Size = new System.Drawing.Size(194, 22);
            this.cmbInfoSourceEntityQualifierCode.TabIndex = 1;
            // 
            // cmbInfoSourceContactQual
            // 
            this.cmbInfoSourceContactQual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInfoSourceContactQual.ForeColor = System.Drawing.Color.Black;
            this.cmbInfoSourceContactQual.FormattingEnabled = true;
            this.cmbInfoSourceContactQual.Items.AddRange(new object[] {
            "IL-Insured or Subscriber"});
            this.cmbInfoSourceContactQual.Location = new System.Drawing.Point(200, 146);
            this.cmbInfoSourceContactQual.Name = "cmbInfoSourceContactQual";
            this.cmbInfoSourceContactQual.Size = new System.Drawing.Size(194, 22);
            this.cmbInfoSourceContactQual.TabIndex = 6;
            // 
            // cmbInfoSourceIDCodeQualifier
            // 
            this.cmbInfoSourceIDCodeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInfoSourceIDCodeQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmbInfoSourceIDCodeQualifier.FormattingEnabled = true;
            this.cmbInfoSourceIDCodeQualifier.Items.AddRange(new object[] {
            "21- Health Industry Number (HIN)",
            "AD- Blue Cross Blue Shield Association Plan Code",
            "FI- Federal Taxpayer’s Identification Number",
            "NI- National Association of Insurance Commissioners (NAIC) Identification",
            "PI- Payor Identification",
            "PP- Pharmacy Processor Number",
            "XV- Health Care Financing Administration National PlanID "});
            this.cmbInfoSourceIDCodeQualifier.Location = new System.Drawing.Point(200, 115);
            this.cmbInfoSourceIDCodeQualifier.Name = "cmbInfoSourceIDCodeQualifier";
            this.cmbInfoSourceIDCodeQualifier.Size = new System.Drawing.Size(194, 22);
            this.cmbInfoSourceIDCodeQualifier.TabIndex = 4;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(20, 180);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(177, 14);
            this.label82.TabIndex = 13;
            this.label82.Text = "Communication Qualifier Code :";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.Location = new System.Drawing.Point(14, 56);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(183, 14);
            this.label83.TabIndex = 13;
            this.label83.Text = "Entity Qualifier For Info Source :";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(39, 149);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(158, 14);
            this.label84.TabIndex = 13;
            this.label84.Text = "Info Source Contact Code :";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(19, 118);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(178, 14);
            this.label85.TabIndex = 13;
            this.label85.Text = "Info Source ID Code  Qualifier :";
            // 
            // txtInfoSourceQualifier
            // 
            this.txtInfoSourceQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoSourceQualifier.ForeColor = System.Drawing.Color.Black;
            this.txtInfoSourceQualifier.Location = new System.Drawing.Point(563, 53);
            this.txtInfoSourceQualifier.Name = "txtInfoSourceQualifier";
            this.txtInfoSourceQualifier.Size = new System.Drawing.Size(42, 22);
            this.txtInfoSourceQualifier.TabIndex = 2;
            this.txtInfoSourceQualifier.Tag = "";
            this.txtInfoSourceQualifier.Text = "2";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(608, 57);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(147, 14);
            this.label88.TabIndex = 13;
            this.label88.Text = "(1-Person, 2-Non Person)";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(434, 57);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(126, 14);
            this.label92.TabIndex = 13;
            this.label92.Text = "Info Source Qualifier :";
            // 
            // txtInfoSourceIDCodeQualifierPayerID
            // 
            this.txtInfoSourceIDCodeQualifierPayerID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoSourceIDCodeQualifierPayerID.ForeColor = System.Drawing.Color.Black;
            this.txtInfoSourceIDCodeQualifierPayerID.Location = new System.Drawing.Point(563, 115);
            this.txtInfoSourceIDCodeQualifierPayerID.MaxLength = 80;
            this.txtInfoSourceIDCodeQualifierPayerID.Name = "txtInfoSourceIDCodeQualifierPayerID";
            this.txtInfoSourceIDCodeQualifierPayerID.Size = new System.Drawing.Size(185, 22);
            this.txtInfoSourceIDCodeQualifierPayerID.TabIndex = 5;
            this.txtInfoSourceIDCodeQualifierPayerID.Text = "12545";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(499, 119);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(61, 14);
            this.label104.TabIndex = 34;
            this.label104.Text = "Payer ID :";
            // 
            // txtInfoSourceName
            // 
            this.txtInfoSourceName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoSourceName.ForeColor = System.Drawing.Color.Black;
            this.txtInfoSourceName.Location = new System.Drawing.Point(200, 84);
            this.txtInfoSourceName.MaxLength = 60;
            this.txtInfoSourceName.Name = "txtInfoSourceName";
            this.txtInfoSourceName.Size = new System.Drawing.Size(194, 22);
            this.txtInfoSourceName.TabIndex = 3;
            this.txtInfoSourceName.Text = "Payer Organisation";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.Location = new System.Drawing.Point(42, 87);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(155, 14);
            this.label109.TabIndex = 13;
            this.label109.Text = "Information Source Name :";
            // 
            // txtInfoSourceLevel
            // 
            this.txtInfoSourceLevel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoSourceLevel.ForeColor = System.Drawing.Color.Black;
            this.txtInfoSourceLevel.Location = new System.Drawing.Point(200, 22);
            this.txtInfoSourceLevel.Name = "txtInfoSourceLevel";
            this.txtInfoSourceLevel.Size = new System.Drawing.Size(194, 22);
            this.txtInfoSourceLevel.TabIndex = 0;
            this.txtInfoSourceLevel.Text = "20";
            // 
            // lblInfoSourceLevel
            // 
            this.lblInfoSourceLevel.AutoSize = true;
            this.lblInfoSourceLevel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoSourceLevel.Location = new System.Drawing.Point(86, 25);
            this.lblInfoSourceLevel.Name = "lblInfoSourceLevel";
            this.lblInfoSourceLevel.Size = new System.Drawing.Size(111, 14);
            this.lblInfoSourceLevel.TabIndex = 15;
            this.lblInfoSourceLevel.Text = "Info Source Level :";
            // 
            // textBox51
            // 
            this.textBox51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox51.ForeColor = System.Drawing.Color.Black;
            this.textBox51.Location = new System.Drawing.Point(563, 177);
            this.textBox51.MaxLength = 10;
            this.textBox51.Name = "textBox51";
            this.textBox51.Size = new System.Drawing.Size(185, 22);
            this.textBox51.TabIndex = 9;
            this.textBox51.Text = "9967833434";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(443, 181);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(117, 14);
            this.label112.TabIndex = 18;
            this.label112.Text = "Communication No :";
            // 
            // txtContactName
            // 
            this.txtContactName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactName.ForeColor = System.Drawing.Color.Black;
            this.txtContactName.Location = new System.Drawing.Point(563, 146);
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(185, 22);
            this.txtContactName.TabIndex = 7;
            this.txtContactName.Text = "MI";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label113.Location = new System.Drawing.Point(467, 150);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(93, 14);
            this.label113.TabIndex = 18;
            this.label113.Text = "Contact Name :";
            // 
            // tbpg_SubscriberDetails
            // 
            this.tbpg_SubscriberDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_SubscriberDetails.Controls.Add(this.grp_Provider);
            this.tbpg_SubscriberDetails.Controls.Add(this.grp_Subscriber);
            this.tbpg_SubscriberDetails.Location = new System.Drawing.Point(4, 23);
            this.tbpg_SubscriberDetails.Name = "tbpg_SubscriberDetails";
            this.tbpg_SubscriberDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpg_SubscriberDetails.Size = new System.Drawing.Size(1063, 619);
            this.tbpg_SubscriberDetails.TabIndex = 2;
            this.tbpg_SubscriberDetails.Tag = "Provider And Subscriber";
            this.tbpg_SubscriberDetails.Text = "Provider And Subscriber";
            this.tbpg_SubscriberDetails.UseVisualStyleBackColor = true;
            // 
            // grp_Provider
            // 
            this.grp_Provider.Controls.Add(this.label80);
            this.grp_Provider.Controls.Add(this.txtProviderLevelCode);
            this.grp_Provider.Controls.Add(this.label98);
            this.grp_Provider.Controls.Add(this.label99);
            this.grp_Provider.Controls.Add(this.label100);
            this.grp_Provider.Controls.Add(this.cmbProvEntityQualCode);
            this.grp_Provider.Controls.Add(this.cmbProvIdentificationCode);
            this.grp_Provider.Controls.Add(this.label101);
            this.grp_Provider.Controls.Add(this.txtProviderIDNo);
            this.grp_Provider.Controls.Add(this.txtProviderMName);
            this.grp_Provider.Controls.Add(this.txtProviderLName);
            this.grp_Provider.Controls.Add(this.label103);
            this.grp_Provider.Controls.Add(this.label105);
            this.grp_Provider.Controls.Add(this.txtProviderFName);
            this.grp_Provider.Controls.Add(this.label106);
            this.grp_Provider.Controls.Add(this.txtProvEntityQualifier);
            this.grp_Provider.Controls.Add(this.label108);
            this.grp_Provider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Provider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Provider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_Provider.Location = new System.Drawing.Point(3, 462);
            this.grp_Provider.Name = "grp_Provider";
            this.grp_Provider.Size = new System.Drawing.Size(1057, 154);
            this.grp_Provider.TabIndex = 1;
            this.grp_Provider.TabStop = false;
            this.grp_Provider.Text = "Provider Level";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(91, 27);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(123, 14);
            this.label80.TabIndex = 63;
            this.label80.Text = "Provider Level Code :";
            // 
            // txtProviderLevelCode
            // 
            this.txtProviderLevelCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProviderLevelCode.Location = new System.Drawing.Point(217, 24);
            this.txtProviderLevelCode.MaxLength = 3;
            this.txtProviderLevelCode.Name = "txtProviderLevelCode";
            this.txtProviderLevelCode.Size = new System.Drawing.Size(93, 22);
            this.txtProviderLevelCode.TabIndex = 0;
            this.txtProviderLevelCode.Text = "19";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(611, 56);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(147, 14);
            this.label98.TabIndex = 61;
            this.label98.Text = "(1-Person, 2-Non Person)";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(40, 58);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(174, 14);
            this.label99.TabIndex = 43;
            this.label99.Text = "Provider Entity Qualifier Code :";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.Location = new System.Drawing.Point(48, 120);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(166, 14);
            this.label100.TabIndex = 43;
            this.label100.Text = "Provider Identification Code :";
            // 
            // cmbProvEntityQualCode
            // 
            this.cmbProvEntityQualCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvEntityQualCode.FormattingEnabled = true;
            this.cmbProvEntityQualCode.Items.AddRange(new object[] {
            "1P-Provider"});
            this.cmbProvEntityQualCode.Location = new System.Drawing.Point(217, 55);
            this.cmbProvEntityQualCode.Name = "cmbProvEntityQualCode";
            this.cmbProvEntityQualCode.Size = new System.Drawing.Size(195, 22);
            this.cmbProvEntityQualCode.TabIndex = 1;
            // 
            // cmbProvIdentificationCode
            // 
            this.cmbProvIdentificationCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvIdentificationCode.FormattingEnabled = true;
            this.cmbProvIdentificationCode.Items.AddRange(new object[] {
            "FI-Federal Taxpayer\'s Identification Number",
            "SV-Service Provider Number",
            "XX-Health Care Financing Administration National Identifier"});
            this.cmbProvIdentificationCode.Location = new System.Drawing.Point(217, 117);
            this.cmbProvIdentificationCode.Name = "cmbProvIdentificationCode";
            this.cmbProvIdentificationCode.Size = new System.Drawing.Size(195, 22);
            this.cmbProvIdentificationCode.TabIndex = 6;
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label101.Location = new System.Drawing.Point(94, 89);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(120, 14);
            this.label101.TabIndex = 43;
            this.label101.Text = "Provider First Name :";
            // 
            // txtProviderIDNo
            // 
            this.txtProviderIDNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProviderIDNo.Location = new System.Drawing.Point(555, 116);
            this.txtProviderIDNo.MaxLength = 80;
            this.txtProviderIDNo.Name = "txtProviderIDNo";
            this.txtProviderIDNo.Size = new System.Drawing.Size(137, 22);
            this.txtProviderIDNo.TabIndex = 7;
            this.txtProviderIDNo.Text = "125351";
            // 
            // txtProviderMName
            // 
            this.txtProviderMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProviderMName.Location = new System.Drawing.Point(555, 83);
            this.txtProviderMName.MaxLength = 80;
            this.txtProviderMName.Name = "txtProviderMName";
            this.txtProviderMName.Size = new System.Drawing.Size(137, 22);
            this.txtProviderMName.TabIndex = 4;
            this.txtProviderMName.Text = "M";
            // 
            // txtProviderLName
            // 
            this.txtProviderLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProviderLName.Location = new System.Drawing.Point(821, 83);
            this.txtProviderLName.MaxLength = 80;
            this.txtProviderLName.Name = "txtProviderLName";
            this.txtProviderLName.Size = new System.Drawing.Size(137, 22);
            this.txtProviderLName.TabIndex = 5;
            this.txtProviderLName.Text = "Picasi";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.Location = new System.Drawing.Point(430, 120);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(122, 14);
            this.label103.TabIndex = 42;
            this.label103.Text = "Provider ID Number :";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(420, 86);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(132, 14);
            this.label105.TabIndex = 42;
            this.label105.Text = "Provider Middle Name :";
            // 
            // txtProviderFName
            // 
            this.txtProviderFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProviderFName.Location = new System.Drawing.Point(217, 86);
            this.txtProviderFName.MaxLength = 35;
            this.txtProviderFName.Name = "txtProviderFName";
            this.txtProviderFName.Size = new System.Drawing.Size(195, 22);
            this.txtProviderFName.TabIndex = 3;
            this.txtProviderFName.Text = "Alexander";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.Location = new System.Drawing.Point(697, 86);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(120, 14);
            this.label106.TabIndex = 42;
            this.label106.Text = "Provider Last Name :";
            // 
            // txtProvEntityQualifier
            // 
            this.txtProvEntityQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProvEntityQualifier.Location = new System.Drawing.Point(555, 53);
            this.txtProvEntityQualifier.MaxLength = 1;
            this.txtProvEntityQualifier.Name = "txtProvEntityQualifier";
            this.txtProvEntityQualifier.Size = new System.Drawing.Size(52, 22);
            this.txtProvEntityQualifier.TabIndex = 2;
            this.txtProvEntityQualifier.Tag = "";
            this.txtProvEntityQualifier.Text = "1";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.Location = new System.Drawing.Point(494, 56);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(58, 14);
            this.label108.TabIndex = 13;
            this.label108.Text = "Qualifier :";
            // 
            // grp_Subscriber
            // 
            this.grp_Subscriber.Controls.Add(this.label115);
            this.grp_Subscriber.Controls.Add(this.label107);
            this.grp_Subscriber.Controls.Add(this.label91);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberTraceType);
            this.grp_Subscriber.Controls.Add(this.label90);
            this.grp_Subscriber.Controls.Add(this.label33);
            this.grp_Subscriber.Controls.Add(this.cmbReferenceIDQualifier);
            this.grp_Subscriber.Controls.Add(this.cmbServiceLineDateQualifier);
            this.grp_Subscriber.Controls.Add(this.cmbServiceLineItemQualifier);
            this.grp_Subscriber.Controls.Add(this.cmbServiceIDQualifierCode);
            this.grp_Subscriber.Controls.Add(this.cmbDateQualifierCode);
            this.grp_Subscriber.Controls.Add(this.cmbAmountQualifierCode);
            this.grp_Subscriber.Controls.Add(this.cmbMedicalIDQualifier);
            this.grp_Subscriber.Controls.Add(this.cmbBillTypeIDQualifier);
            this.grp_Subscriber.Controls.Add(this.cmbSubscriberEntityQualifierCode);
            this.grp_Subscriber.Controls.Add(this.label122);
            this.grp_Subscriber.Controls.Add(this.label120);
            this.grp_Subscriber.Controls.Add(this.label38);
            this.grp_Subscriber.Controls.Add(this.cmbSubscriberIDQualifier);
            this.grp_Subscriber.Controls.Add(this.label118);
            this.grp_Subscriber.Controls.Add(this.label31);
            this.grp_Subscriber.Controls.Add(this.label114);
            this.grp_Subscriber.Controls.Add(this.label94);
            this.grp_Subscriber.Controls.Add(this.label32);
            this.grp_Subscriber.Controls.Add(this.label34);
            this.grp_Subscriber.Controls.Add(this.label35);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberEntityQualifier);
            this.grp_Subscriber.Controls.Add(this.txtServiceAmount);
            this.grp_Subscriber.Controls.Add(this.txtServiceLineDate);
            this.grp_Subscriber.Controls.Add(this.txtLineItemControlNo);
            this.grp_Subscriber.Controls.Add(this.txtServiceIDQualifier);
            this.grp_Subscriber.Controls.Add(this.label121);
            this.grp_Subscriber.Controls.Add(this.label41);
            this.grp_Subscriber.Controls.Add(this.label119);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberGender);
            this.grp_Subscriber.Controls.Add(this.label29);
            this.grp_Subscriber.Controls.Add(this.txtServiceDate);
            this.grp_Subscriber.Controls.Add(this.label36);
            this.grp_Subscriber.Controls.Add(this.label37);
            this.grp_Subscriber.Controls.Add(this.label39);
            this.grp_Subscriber.Controls.Add(this.label40);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberDOB);
            this.grp_Subscriber.Controls.Add(this.label42);
            this.grp_Subscriber.Controls.Add(this.label43);
            this.grp_Subscriber.Controls.Add(this.lblIdentifierCode);
            this.grp_Subscriber.Controls.Add(this.label44);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberFName);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberLName);
            this.grp_Subscriber.Controls.Add(this.lblSubscriberName);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberIDQualifier);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberChildCode);
            this.grp_Subscriber.Controls.Add(this.label45);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberMName);
            this.grp_Subscriber.Controls.Add(this.txtAmountQualifier);
            this.grp_Subscriber.Controls.Add(this.txtMedicalIDQual);
            this.grp_Subscriber.Controls.Add(this.txtBillTypeID);
            this.grp_Subscriber.Controls.Add(this.txtPayerClaimNo);
            this.grp_Subscriber.Controls.Add(this.label46);
            this.grp_Subscriber.Controls.Add(this.label58);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberLevelCode);
            this.grp_Subscriber.Controls.Add(this.txtSubscriberTraceNo);
            this.grp_Subscriber.Controls.Add(this.label57);
            this.grp_Subscriber.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_Subscriber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Subscriber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_Subscriber.Location = new System.Drawing.Point(3, 3);
            this.grp_Subscriber.Name = "grp_Subscriber";
            this.grp_Subscriber.Size = new System.Drawing.Size(1057, 459);
            this.grp_Subscriber.TabIndex = 0;
            this.grp_Subscriber.TabStop = false;
            this.grp_Subscriber.Text = "Subscriber Details";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label115.Location = new System.Drawing.Point(469, 304);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(108, 14);
            this.label115.TabIndex = 64;
            this.label115.Text = "Charged Amount :";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(460, 273);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(117, 14);
            this.label107.TabIndex = 64;
            this.label107.Text = "Medical ID Qualifier :";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(433, 242);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(144, 14);
            this.label91.TabIndex = 64;
            this.label91.Text = "Institutional Bill Type ID :";
            // 
            // txtSubscriberTraceType
            // 
            this.txtSubscriberTraceType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberTraceType.Location = new System.Drawing.Point(219, 177);
            this.txtSubscriberTraceType.MaxLength = 3;
            this.txtSubscriberTraceType.Name = "txtSubscriberTraceType";
            this.txtSubscriberTraceType.Size = new System.Drawing.Size(174, 22);
            this.txtSubscriberTraceType.TabIndex = 11;
            this.txtSubscriberTraceType.Text = "1";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(78, 180);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(138, 14);
            this.label90.TabIndex = 63;
            this.label90.Text = "Subscriber Trace Type :";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(778, 88);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(147, 14);
            this.label33.TabIndex = 61;
            this.label33.Text = "(1-Person, 2-Non Person)";
            // 
            // cmbReferenceIDQualifier
            // 
            this.cmbReferenceIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReferenceIDQualifier.FormattingEnabled = true;
            this.cmbReferenceIDQualifier.Items.AddRange(new object[] {
            "1K- Payor\'s Claim Number"});
            this.cmbReferenceIDQualifier.Location = new System.Drawing.Point(219, 208);
            this.cmbReferenceIDQualifier.Name = "cmbReferenceIDQualifier";
            this.cmbReferenceIDQualifier.Size = new System.Drawing.Size(174, 22);
            this.cmbReferenceIDQualifier.TabIndex = 13;
            // 
            // cmbServiceLineDateQualifier
            // 
            this.cmbServiceLineDateQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceLineDateQualifier.FormattingEnabled = true;
            this.cmbServiceLineDateQualifier.Items.AddRange(new object[] {
            "472- Service"});
            this.cmbServiceLineDateQualifier.Location = new System.Drawing.Point(219, 425);
            this.cmbServiceLineDateQualifier.Name = "cmbServiceLineDateQualifier";
            this.cmbServiceLineDateQualifier.Size = new System.Drawing.Size(174, 22);
            this.cmbServiceLineDateQualifier.TabIndex = 27;
            // 
            // cmbServiceLineItemQualifier
            // 
            this.cmbServiceLineItemQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceLineItemQualifier.FormattingEnabled = true;
            this.cmbServiceLineItemQualifier.Items.AddRange(new object[] {
            "FJ- Line Item Control Number"});
            this.cmbServiceLineItemQualifier.Location = new System.Drawing.Point(219, 394);
            this.cmbServiceLineItemQualifier.Name = "cmbServiceLineItemQualifier";
            this.cmbServiceLineItemQualifier.Size = new System.Drawing.Size(174, 22);
            this.cmbServiceLineItemQualifier.TabIndex = 25;
            // 
            // cmbServiceIDQualifierCode
            // 
            this.cmbServiceIDQualifierCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceIDQualifierCode.FormattingEnabled = true;
            this.cmbServiceIDQualifierCode.Items.AddRange(new object[] {
            "AD- American Dental Association Codes",
            "CI- Common Language Equipment Identifier (CLEI)",
            "HC- Health Care Financing Administration Common Procedural Coding System (HCPCS) " +
                "Codes",
            "ID- International Classification of Diseases Clinical Modification (ICD-9-CM) - P" +
                "rocedure",
            "IV- Home Infusion EDI Coalition (HIEC) Product/Service Code",
            "N1- National Drug Code in 4-4-2 Format",
            "N2- National Drug Code in 5-3-2 Format",
            "N3- National Drug Code in 5-4-1 Format",
            "N4- National Drug Code in 5-4-2 Format",
            "ND- National Drug Code (NDC)",
            "NH- National Health Related Item Code",
            "NU- National Uniform Billing Committee (NUBC) UB92 Codes",
            "RB- National Uniform Billing Committee (NUBC) UB82 Codes"});
            this.cmbServiceIDQualifierCode.Location = new System.Drawing.Point(219, 363);
            this.cmbServiceIDQualifierCode.Name = "cmbServiceIDQualifierCode";
            this.cmbServiceIDQualifierCode.Size = new System.Drawing.Size(174, 22);
            this.cmbServiceIDQualifierCode.TabIndex = 22;
            // 
            // cmbDateQualifierCode
            // 
            this.cmbDateQualifierCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDateQualifierCode.FormattingEnabled = true;
            this.cmbDateQualifierCode.Items.AddRange(new object[] {
            "232- Claim Statement Period Start"});
            this.cmbDateQualifierCode.Location = new System.Drawing.Point(219, 332);
            this.cmbDateQualifierCode.Name = "cmbDateQualifierCode";
            this.cmbDateQualifierCode.Size = new System.Drawing.Size(174, 22);
            this.cmbDateQualifierCode.TabIndex = 20;
            // 
            // cmbAmountQualifierCode
            // 
            this.cmbAmountQualifierCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAmountQualifierCode.FormattingEnabled = true;
            this.cmbAmountQualifierCode.Items.AddRange(new object[] {
            "T3- Total Submitted Charges"});
            this.cmbAmountQualifierCode.Location = new System.Drawing.Point(219, 301);
            this.cmbAmountQualifierCode.Name = "cmbAmountQualifierCode";
            this.cmbAmountQualifierCode.Size = new System.Drawing.Size(174, 22);
            this.cmbAmountQualifierCode.TabIndex = 18;
            // 
            // cmbMedicalIDQualifier
            // 
            this.cmbMedicalIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMedicalIDQualifier.FormattingEnabled = true;
            this.cmbMedicalIDQualifier.Items.AddRange(new object[] {
            "EA- Medical Record Identification Number"});
            this.cmbMedicalIDQualifier.Location = new System.Drawing.Point(219, 270);
            this.cmbMedicalIDQualifier.Name = "cmbMedicalIDQualifier";
            this.cmbMedicalIDQualifier.Size = new System.Drawing.Size(174, 22);
            this.cmbMedicalIDQualifier.TabIndex = 16;
            // 
            // cmbBillTypeIDQualifier
            // 
            this.cmbBillTypeIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBillTypeIDQualifier.FormattingEnabled = true;
            this.cmbBillTypeIDQualifier.Items.AddRange(new object[] {
            "BLT- Billing Type"});
            this.cmbBillTypeIDQualifier.Location = new System.Drawing.Point(219, 239);
            this.cmbBillTypeIDQualifier.Name = "cmbBillTypeIDQualifier";
            this.cmbBillTypeIDQualifier.Size = new System.Drawing.Size(174, 22);
            this.cmbBillTypeIDQualifier.TabIndex = 15;
            // 
            // cmbSubscriberEntityQualifierCode
            // 
            this.cmbSubscriberEntityQualifierCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubscriberEntityQualifierCode.FormattingEnabled = true;
            this.cmbSubscriberEntityQualifierCode.Items.AddRange(new object[] {
            "IL- Insured or Subscriber",
            "QC- Patient"});
            this.cmbSubscriberEntityQualifierCode.Location = new System.Drawing.Point(219, 84);
            this.cmbSubscriberEntityQualifierCode.Name = "cmbSubscriberEntityQualifierCode";
            this.cmbSubscriberEntityQualifierCode.Size = new System.Drawing.Size(174, 22);
            this.cmbSubscriberEntityQualifierCode.TabIndex = 4;
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label122.Location = new System.Drawing.Point(28, 428);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(188, 14);
            this.label122.TabIndex = 13;
            this.label122.Text = "Service Line Date Time Qualifier :";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label120.Location = new System.Drawing.Point(11, 397);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(205, 14);
            this.label120.TabIndex = 13;
            this.label120.Text = "Service Line Item ID Qualifier Code :";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(71, 366);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(145, 14);
            this.label38.TabIndex = 13;
            this.label38.Text = "ServiceID Qualifier Code :";
            // 
            // cmbSubscriberIDQualifier
            // 
            this.cmbSubscriberIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubscriberIDQualifier.FormattingEnabled = true;
            this.cmbSubscriberIDQualifier.Items.AddRange(new object[] {
            "24- Employer’s Identification Number",
            "MI- Member Identification Number",
            "ZZ- Mutually Defined"});
            this.cmbSubscriberIDQualifier.Location = new System.Drawing.Point(219, 146);
            this.cmbSubscriberIDQualifier.Name = "cmbSubscriberIDQualifier";
            this.cmbSubscriberIDQualifier.Size = new System.Drawing.Size(174, 22);
            this.cmbSubscriberIDQualifier.TabIndex = 9;
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label118.Location = new System.Drawing.Point(65, 335);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(151, 14);
            this.label118.TabIndex = 13;
            this.label118.Text = "Claim Date Qualifier Code :";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(50, 211);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(166, 14);
            this.label31.TabIndex = 13;
            this.label31.Text = "Reference ID Qualifier Code :";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.Location = new System.Drawing.Point(78, 304);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(138, 14);
            this.label114.TabIndex = 13;
            this.label114.Text = "Amount Qualifier Code :";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(67, 273);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(149, 14);
            this.label94.TabIndex = 13;
            this.label94.Text = "Medical ID Qualifier Code :";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(44, 242);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(172, 14);
            this.label32.TabIndex = 13;
            this.label32.Text = "Institutional Bill Type ID Qual :";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(30, 87);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(186, 14);
            this.label34.TabIndex = 13;
            this.label34.Text = "Subscriber Entity Qualifier Code :";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(50, 149);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(166, 14);
            this.label35.TabIndex = 13;
            this.label35.Text = "Subscriber Qualifier ID Code :";
            // 
            // txtSubscriberEntityQualifier
            // 
            this.txtSubscriberEntityQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberEntityQualifier.Location = new System.Drawing.Point(581, 84);
            this.txtSubscriberEntityQualifier.Name = "txtSubscriberEntityQualifier";
            this.txtSubscriberEntityQualifier.Size = new System.Drawing.Size(194, 22);
            this.txtSubscriberEntityQualifier.TabIndex = 5;
            this.txtSubscriberEntityQualifier.Tag = "";
            this.txtSubscriberEntityQualifier.Text = "1";
            // 
            // txtServiceAmount
            // 
            this.txtServiceAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceAmount.Location = new System.Drawing.Point(884, 362);
            this.txtServiceAmount.Name = "txtServiceAmount";
            this.txtServiceAmount.Size = new System.Drawing.Size(133, 22);
            this.txtServiceAmount.TabIndex = 24;
            this.txtServiceAmount.Text = "500";
            // 
            // txtServiceLineDate
            // 
            this.txtServiceLineDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceLineDate.Location = new System.Drawing.Point(581, 425);
            this.txtServiceLineDate.Name = "txtServiceLineDate";
            this.txtServiceLineDate.Size = new System.Drawing.Size(194, 22);
            this.txtServiceLineDate.TabIndex = 28;
            this.txtServiceLineDate.Text = "20080915";
            // 
            // txtLineItemControlNo
            // 
            this.txtLineItemControlNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLineItemControlNo.Location = new System.Drawing.Point(581, 394);
            this.txtLineItemControlNo.Name = "txtLineItemControlNo";
            this.txtLineItemControlNo.Size = new System.Drawing.Size(194, 22);
            this.txtLineItemControlNo.TabIndex = 26;
            this.txtLineItemControlNo.Text = "212";
            // 
            // txtServiceIDQualifier
            // 
            this.txtServiceIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceIDQualifier.Location = new System.Drawing.Point(581, 363);
            this.txtServiceIDQualifier.Name = "txtServiceIDQualifier";
            this.txtServiceIDQualifier.Size = new System.Drawing.Size(194, 22);
            this.txtServiceIDQualifier.TabIndex = 23;
            this.txtServiceIDQualifier.Text = "3434";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label121.Location = new System.Drawing.Point(480, 428);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(97, 13);
            this.label121.TabIndex = 11;
            this.label121.Text = "Service Line Date :";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(778, 366);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(102, 14);
            this.label41.TabIndex = 11;
            this.label41.Text = "Service Amount :";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(448, 397);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(129, 14);
            this.label119.TabIndex = 11;
            this.label119.Text = "Line Item Control No :";
            // 
            // txtSubscriberGender
            // 
            this.txtSubscriberGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberGender.Location = new System.Drawing.Point(581, 53);
            this.txtSubscriberGender.MaxLength = 1;
            this.txtSubscriberGender.Name = "txtSubscriberGender";
            this.txtSubscriberGender.Size = new System.Drawing.Size(194, 22);
            this.txtSubscriberGender.TabIndex = 3;
            this.txtSubscriberGender.Text = "M";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(507, 366);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(70, 14);
            this.label29.TabIndex = 11;
            this.label29.Text = "Service ID :";
            // 
            // txtServiceDate
            // 
            this.txtServiceDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceDate.Location = new System.Drawing.Point(581, 332);
            this.txtServiceDate.Name = "txtServiceDate";
            this.txtServiceDate.Size = new System.Drawing.Size(194, 22);
            this.txtServiceDate.TabIndex = 21;
            this.txtServiceDate.Text = "20080915";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(505, 335);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(72, 14);
            this.label36.TabIndex = 11;
            this.label36.Text = "Claim Date :";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(483, 87);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(94, 14);
            this.label37.TabIndex = 13;
            this.label37.Text = "Entity Qualifier :";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(775, 55);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(50, 14);
            this.label39.TabIndex = 59;
            this.label39.Text = "(M/F/U)";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(462, 56);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(115, 14);
            this.label40.TabIndex = 59;
            this.label40.Text = "Subscriber Gender :";
            // 
            // txtSubscriberDOB
            // 
            this.txtSubscriberDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberDOB.Location = new System.Drawing.Point(219, 53);
            this.txtSubscriberDOB.Name = "txtSubscriberDOB";
            this.txtSubscriberDOB.Size = new System.Drawing.Size(174, 22);
            this.txtSubscriberDOB.TabIndex = 2;
            this.txtSubscriberDOB.Text = "19850612";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(117, 56);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(99, 14);
            this.label42.TabIndex = 59;
            this.label42.Text = "Subscriber DOB :";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(84, 118);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(132, 14);
            this.label43.TabIndex = 57;
            this.label43.Text = "Subscriber Last Name :";
            // 
            // lblIdentifierCode
            // 
            this.lblIdentifierCode.AutoSize = true;
            this.lblIdentifierCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdentifierCode.Location = new System.Drawing.Point(81, 25);
            this.lblIdentifierCode.Name = "lblIdentifierCode";
            this.lblIdentifierCode.Size = new System.Drawing.Size(135, 14);
            this.lblIdentifierCode.TabIndex = 50;
            this.lblIdentifierCode.Text = "Subscriber Level Code :";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(505, 118);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(72, 14);
            this.label44.TabIndex = 15;
            this.label44.Text = "First Name :";
            // 
            // txtSubscriberFName
            // 
            this.txtSubscriberFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberFName.Location = new System.Drawing.Point(581, 115);
            this.txtSubscriberFName.MaxLength = 80;
            this.txtSubscriberFName.Name = "txtSubscriberFName";
            this.txtSubscriberFName.Size = new System.Drawing.Size(194, 22);
            this.txtSubscriberFName.TabIndex = 7;
            this.txtSubscriberFName.Text = "Ethan";
            // 
            // txtSubscriberLName
            // 
            this.txtSubscriberLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberLName.Location = new System.Drawing.Point(219, 115);
            this.txtSubscriberLName.MaxLength = 4;
            this.txtSubscriberLName.Name = "txtSubscriberLName";
            this.txtSubscriberLName.Size = new System.Drawing.Size(174, 22);
            this.txtSubscriberLName.TabIndex = 6;
            this.txtSubscriberLName.Text = "Hunt";
            // 
            // lblSubscriberName
            // 
            this.lblSubscriberName.AutoSize = true;
            this.lblSubscriberName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriberName.Location = new System.Drawing.Point(796, 119);
            this.lblSubscriberName.Name = "lblSubscriberName";
            this.lblSubscriberName.Size = new System.Drawing.Size(84, 14);
            this.lblSubscriberName.TabIndex = 11;
            this.lblSubscriberName.Text = "Middle Name :";
            // 
            // txtSubscriberIDQualifier
            // 
            this.txtSubscriberIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberIDQualifier.Location = new System.Drawing.Point(581, 146);
            this.txtSubscriberIDQualifier.MaxLength = 80;
            this.txtSubscriberIDQualifier.Name = "txtSubscriberIDQualifier";
            this.txtSubscriberIDQualifier.Size = new System.Drawing.Size(194, 22);
            this.txtSubscriberIDQualifier.TabIndex = 10;
            this.txtSubscriberIDQualifier.Text = "11122333301";
            // 
            // txtSubscriberChildCode
            // 
            this.txtSubscriberChildCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberChildCode.Location = new System.Drawing.Point(581, 22);
            this.txtSubscriberChildCode.MaxLength = 80;
            this.txtSubscriberChildCode.Name = "txtSubscriberChildCode";
            this.txtSubscriberChildCode.Size = new System.Drawing.Size(194, 22);
            this.txtSubscriberChildCode.TabIndex = 1;
            this.txtSubscriberChildCode.Text = "12545";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(505, 25);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(72, 14);
            this.label45.TabIndex = 34;
            this.label45.Text = "Child Code :";
            // 
            // txtSubscriberMName
            // 
            this.txtSubscriberMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberMName.Location = new System.Drawing.Point(884, 115);
            this.txtSubscriberMName.Name = "txtSubscriberMName";
            this.txtSubscriberMName.Size = new System.Drawing.Size(133, 22);
            this.txtSubscriberMName.TabIndex = 8;
            this.txtSubscriberMName.Text = "J";
            // 
            // txtAmountQualifier
            // 
            this.txtAmountQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountQualifier.Location = new System.Drawing.Point(581, 301);
            this.txtAmountQualifier.Name = "txtAmountQualifier";
            this.txtAmountQualifier.Size = new System.Drawing.Size(194, 22);
            this.txtAmountQualifier.TabIndex = 19;
            this.txtAmountQualifier.Text = "500";
            // 
            // txtMedicalIDQual
            // 
            this.txtMedicalIDQual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMedicalIDQual.Location = new System.Drawing.Point(581, 270);
            this.txtMedicalIDQual.Name = "txtMedicalIDQual";
            this.txtMedicalIDQual.Size = new System.Drawing.Size(194, 22);
            this.txtMedicalIDQual.TabIndex = 17;
            this.txtMedicalIDQual.Text = "333";
            // 
            // txtBillTypeID
            // 
            this.txtBillTypeID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillTypeID.Location = new System.Drawing.Point(581, 239);
            this.txtBillTypeID.Name = "txtBillTypeID";
            this.txtBillTypeID.Size = new System.Drawing.Size(194, 22);
            this.txtBillTypeID.TabIndex = 16;
            this.txtBillTypeID.Text = "10203";
            // 
            // txtPayerClaimNo
            // 
            this.txtPayerClaimNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayerClaimNo.Location = new System.Drawing.Point(581, 208);
            this.txtPayerClaimNo.Name = "txtPayerClaimNo";
            this.txtPayerClaimNo.Size = new System.Drawing.Size(194, 22);
            this.txtPayerClaimNo.TabIndex = 14;
            this.txtPayerClaimNo.Text = "20389";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(414, 211);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(163, 14);
            this.label46.TabIndex = 18;
            this.label46.Text = "Payer Claim/Patient Acc No :";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(490, 149);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(87, 14);
            this.label58.TabIndex = 20;
            this.label58.Text = "Subscriber ID :";
            // 
            // txtSubscriberLevelCode
            // 
            this.txtSubscriberLevelCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberLevelCode.Location = new System.Drawing.Point(219, 22);
            this.txtSubscriberLevelCode.MaxLength = 2;
            this.txtSubscriberLevelCode.Name = "txtSubscriberLevelCode";
            this.txtSubscriberLevelCode.ReadOnly = true;
            this.txtSubscriberLevelCode.Size = new System.Drawing.Size(174, 22);
            this.txtSubscriberLevelCode.TabIndex = 0;
            this.txtSubscriberLevelCode.Text = "22";
            // 
            // txtSubscriberTraceNo
            // 
            this.txtSubscriberTraceNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberTraceNo.Location = new System.Drawing.Point(581, 177);
            this.txtSubscriberTraceNo.Name = "txtSubscriberTraceNo";
            this.txtSubscriberTraceNo.Size = new System.Drawing.Size(194, 22);
            this.txtSubscriberTraceNo.TabIndex = 12;
            this.txtSubscriberTraceNo.Text = "93175-012547";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(424, 180);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(153, 14);
            this.label57.TabIndex = 22;
            this.label57.Text = "Subscriber Trace Number :";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 0;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 0;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 23);
            this.label15.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(0, 0);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 23);
            this.label16.TabIndex = 0;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(0, 0);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 23);
            this.label17.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 23);
            this.label18.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 23);
            this.label19.TabIndex = 0;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(0, 0);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 20);
            this.textBox8.TabIndex = 0;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(0, 0);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(100, 23);
            this.label20.TabIndex = 0;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(0, 0);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 20);
            this.textBox10.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(100, 23);
            this.label21.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(100, 23);
            this.label22.TabIndex = 0;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(0, 0);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(100, 20);
            this.textBox11.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(100, 23);
            this.label23.TabIndex = 0;
            // 
            // txtSenderID
            // 
            this.txtSenderID.Location = new System.Drawing.Point(0, 0);
            this.txtSenderID.Name = "txtSenderID";
            this.txtSenderID.Size = new System.Drawing.Size(100, 20);
            this.txtSenderID.TabIndex = 0;
            // 
            // txtReferenceID
            // 
            this.txtReferenceID.Location = new System.Drawing.Point(0, 0);
            this.txtReferenceID.Name = "txtReferenceID";
            this.txtReferenceID.Size = new System.Drawing.Size(100, 20);
            this.txtReferenceID.TabIndex = 0;
            // 
            // txtEnquiryDate
            // 
            this.txtEnquiryDate.Location = new System.Drawing.Point(0, 0);
            this.txtEnquiryDate.Name = "txtEnquiryDate";
            this.txtEnquiryDate.Size = new System.Drawing.Size(100, 20);
            this.txtEnquiryDate.TabIndex = 0;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(100, 23);
            this.label24.TabIndex = 0;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(100, 23);
            this.label25.TabIndex = 0;
            // 
            // txtReceiverID
            // 
            this.txtReceiverID.Location = new System.Drawing.Point(0, 0);
            this.txtReceiverID.Name = "txtReceiverID";
            this.txtReceiverID.Size = new System.Drawing.Size(100, 20);
            this.txtReceiverID.TabIndex = 0;
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(100, 23);
            this.label26.TabIndex = 0;
            // 
            // txtEnquiryTime
            // 
            this.txtEnquiryTime.Location = new System.Drawing.Point(0, 0);
            this.txtEnquiryTime.Name = "txtEnquiryTime";
            this.txtEnquiryTime.Size = new System.Drawing.Size(100, 20);
            this.txtEnquiryTime.TabIndex = 0;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(0, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(100, 23);
            this.label27.TabIndex = 0;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(0, 0);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 20);
            this.textBox12.TabIndex = 0;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(100, 23);
            this.label28.TabIndex = 0;
            // 
            // frmEDI276Generation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1077, 706);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEDI276Generation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EDI276 Generation";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEDI276Generation_FormClosed);
            this.Load += new System.EventHandler(this.frmEDI276Generation_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tb_EDIDataDetails.ResumeLayout(false);
            this.tbpg_EDIDetails.ResumeLayout(false);
            this.grp_BHT.ResumeLayout(false);
            this.grp_BHT.PerformLayout();
            this.grp_TransactionSet.ResumeLayout(false);
            this.grp_TransactionSet.PerformLayout();
            this.grp_FunctionalGroup.ResumeLayout(false);
            this.grp_FunctionalGroup.PerformLayout();
            this.grp_ISASegment.ResumeLayout(false);
            this.grp_ISASegment.PerformLayout();
            this.tbpg_SourceAndReceiver.ResumeLayout(false);
            this.grp_InformationReceiver.ResumeLayout(false);
            this.grp_InformationReceiver.PerformLayout();
            this.grp_InformationSource.ResumeLayout(false);
            this.grp_InformationSource.PerformLayout();
            this.tbpg_SubscriberDetails.ResumeLayout(false);
            this.grp_Provider.ResumeLayout(false);
            this.grp_Provider.PerformLayout();
            this.grp_Subscriber.ResumeLayout(false);
            this.grp_Subscriber.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlMain;
        //private System.Windows.Forms.GroupBox grp_BHT;
        //private System.Windows.Forms.ComboBox cmbTypeOfTransaction;
        //internal System.Windows.Forms.TextBox txtBHTTime;
        //private System.Windows.Forms.Label label12;
        //private System.Windows.Forms.Label label30;
        //private System.Windows.Forms.Label label9;
        //internal System.Windows.Forms.TextBox txtBHTDate;
        //private System.Windows.Forms.Label label8;
        //internal System.Windows.Forms.TextBox txtBHTRefIdentification;
        //private System.Windows.Forms.Label label52;
        //private System.Windows.Forms.Label lblBHT_transactionType;
        //internal System.Windows.Forms.TextBox txtBHT_HerarchicalStrCode;
        //private System.Windows.Forms.Label label54;
        //private System.Windows.Forms.GroupBox grp_TransactionSet;
        //internal System.Windows.Forms.TextBox txtTSControlNumber;
        //private System.Windows.Forms.Label lblTSControlNumber;
        //internal System.Windows.Forms.TextBox txtTSIdCode;
        //private System.Windows.Forms.Label label53;
        //private System.Windows.Forms.GroupBox grp_FunctionalGroup;
        //internal System.Windows.Forms.TextBox txtFuncGroupTime;
        //private System.Windows.Forms.Label label6;
        //private System.Windows.Forms.Label label5;
        //internal System.Windows.Forms.TextBox txtFunGroupDate;
        //internal System.Windows.Forms.TextBox txtFunctionID;
        //private System.Windows.Forms.Label label4;
        //internal System.Windows.Forms.TextBox txtReceiverDept;
        //private System.Windows.Forms.Label lblSenderDept;
        //private System.Windows.Forms.Label lblFunctionID;
        //internal System.Windows.Forms.TextBox txtSenderDept;
        //private System.Windows.Forms.GroupBox grp_ISASegment;
        //private System.Windows.Forms.Label label3;
        //internal System.Windows.Forms.TextBox txtSenderID1;
        //internal System.Windows.Forms.TextBox txtReferenceID1;
        //internal System.Windows.Forms.TextBox txtEnquiryDate1;
        //private System.Windows.Forms.Label lblSenderID;
        //private System.Windows.Forms.Label lblReferenceID;
        //internal System.Windows.Forms.TextBox txtReceiverID1;
        //private System.Windows.Forms.Label label2;
        //internal System.Windows.Forms.TextBox txtEnquiryTime1;
        //private System.Windows.Forms.Label lblInquiryTime;
        //internal System.Windows.Forms.TextBox txtControlNo;
        //private System.Windows.Forms.Label lblControlNo;
        private System.Windows.Forms.TabControl tb_EDIDataDetails;
        private System.Windows.Forms.TabPage tbpg_SubscriberDetails;
        private System.Windows.Forms.GroupBox grp_Subscriber;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cmbReferenceIDQualifier;
        private System.Windows.Forms.ComboBox cmbBillTypeIDQualifier;
        private System.Windows.Forms.ComboBox cmbSubscriberEntityQualifierCode;
        private System.Windows.Forms.ComboBox cmbSubscriberIDQualifier;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        internal System.Windows.Forms.TextBox txtSubscriberEntityQualifier;
        internal System.Windows.Forms.TextBox txtSubscriberGender;
        internal System.Windows.Forms.TextBox txtServiceDate;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        internal System.Windows.Forms.TextBox txtSubscriberDOB;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label lblIdentifierCode;
        private System.Windows.Forms.Label label44;
        internal System.Windows.Forms.TextBox txtSubscriberFName;
        internal System.Windows.Forms.TextBox txtSubscriberLName;
        private System.Windows.Forms.Label lblSubscriberName;
        internal System.Windows.Forms.TextBox txtSubscriberIDQualifier;
        internal System.Windows.Forms.TextBox txtSubscriberChildCode;
        private System.Windows.Forms.Label label45;
        internal System.Windows.Forms.TextBox txtSubscriberMName;
        internal System.Windows.Forms.TextBox txtPayerClaimNo;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label58;
        internal System.Windows.Forms.TextBox txtSubscriberLevelCode;
        internal System.Windows.Forms.TextBox txtSubscriberTraceNo;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TabPage tbpg_SourceAndReceiver;
        private System.Windows.Forms.GroupBox grp_InformationSource;
        private System.Windows.Forms.ComboBox cmbInfoSourCommQualCode;
        private System.Windows.Forms.ComboBox cmbInfoSourceEntityQualifierCode;
        private System.Windows.Forms.ComboBox cmbInfoSourceContactQual;
        private System.Windows.Forms.ComboBox cmbInfoSourceIDCodeQualifier;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        internal System.Windows.Forms.TextBox txtInfoSourceQualifier;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label92;
        internal System.Windows.Forms.TextBox txtInfoSourceIDCodeQualifierPayerID;
        private System.Windows.Forms.Label label104;
        internal System.Windows.Forms.TextBox txtInfoSourceName;
        private System.Windows.Forms.Label label109;
        internal System.Windows.Forms.TextBox txtInfoSourceLevel;
        private System.Windows.Forms.Label lblInfoSourceLevel;
        internal System.Windows.Forms.TextBox textBox51;
        private System.Windows.Forms.Label label112;
        internal System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.Label label113;
        internal System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.ComboBox cmbInfoSourCommQualCode2;
        private System.Windows.Forms.Label label86;
        internal System.Windows.Forms.TextBox txtCommNoFaxNo;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.GroupBox grp_InformationReceiver;
        private System.Windows.Forms.ComboBox cmbInfoReceiverEntityQualifier;
        private System.Windows.Forms.ComboBox cmbRecIDCodeQualifier;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label95;
        internal System.Windows.Forms.TextBox txtInfoRecEntityQualifier;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label97;
        internal System.Windows.Forms.TextBox txtRecIDQualifier;
        private System.Windows.Forms.Label label102;
        internal System.Windows.Forms.TextBox txtRecLastName;
        private System.Windows.Forms.Label label110;
        internal System.Windows.Forms.TextBox txtInfoReceiverLevel;
        private System.Windows.Forms.Label label111;
        internal System.Windows.Forms.TextBox txtRecFirstName;
        private System.Windows.Forms.Label label116;
        internal System.Windows.Forms.TextBox txtRecMiddleName;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.GroupBox grp_Provider;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.ComboBox cmbProvEntityQualCode;
        private System.Windows.Forms.ComboBox cmbProvIdentificationCode;
        private System.Windows.Forms.Label label101;
        internal System.Windows.Forms.TextBox txtProviderIDNo;
        internal System.Windows.Forms.TextBox txtProviderMName;
        internal System.Windows.Forms.TextBox txtProviderLName;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label105;
        internal System.Windows.Forms.TextBox txtProviderFName;
        private System.Windows.Forms.Label label106;
        internal System.Windows.Forms.TextBox txtProvEntityQualifier;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label80;
        internal System.Windows.Forms.TextBox txtProviderLevelCode;
        internal System.Windows.Forms.TextBox txtSubscriberTraceType;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label91;
        internal System.Windows.Forms.TextBox txtBillTypeID;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.ComboBox cmbMedicalIDQualifier;
        private System.Windows.Forms.Label label94;
        internal System.Windows.Forms.TextBox txtMedicalIDQual;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.ComboBox cmbDateQualifierCode;
        private System.Windows.Forms.ComboBox cmbAmountQualifierCode;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label114;
        internal System.Windows.Forms.TextBox txtAmountQualifier;
        private System.Windows.Forms.ComboBox cmbServiceIDQualifierCode;
        private System.Windows.Forms.Label label38;
        internal System.Windows.Forms.TextBox txtServiceIDQualifier;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbServiceLineItemQualifier;
        private System.Windows.Forms.Label label120;
        internal System.Windows.Forms.TextBox txtServiceAmount;
        internal System.Windows.Forms.TextBox txtLineItemControlNo;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.ComboBox cmbServiceLineDateQualifier;
        private System.Windows.Forms.Label label122;
        internal System.Windows.Forms.TextBox txtServiceLineDate;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.ToolStripButton ts_btnGenerateEDI;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        internal System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.TextBox textBox8;
        internal System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        internal System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label23;
        internal System.Windows.Forms.TextBox txtSenderID;
        internal System.Windows.Forms.TextBox txtReferenceID;
        internal System.Windows.Forms.TextBox txtEnquiryDate;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        internal System.Windows.Forms.TextBox txtReceiverID;
        private System.Windows.Forms.Label label26;
        internal System.Windows.Forms.TextBox txtEnquiryTime;
        private System.Windows.Forms.Label label27;
        internal System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TabPage tbpg_EDIDetails;
        private System.Windows.Forms.GroupBox grp_ISASegment;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtSenderID1;
        internal System.Windows.Forms.TextBox txtReferenceID1;
        internal System.Windows.Forms.TextBox txtInquiryDate;
        private System.Windows.Forms.Label lblSenderID;
        private System.Windows.Forms.Label lblReferenceID;
        internal System.Windows.Forms.TextBox txtReceiverID1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtInquiryTime;
        private System.Windows.Forms.Label lblInquiryTime;
        internal System.Windows.Forms.TextBox txtControlNo;
        private System.Windows.Forms.Label lblControlNo;
        private System.Windows.Forms.GroupBox grp_FunctionalGroup;
        internal System.Windows.Forms.TextBox txtFuncGroupTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtFunGroupDate;
        internal System.Windows.Forms.TextBox txtFunctionID;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtReceiverDept;
        private System.Windows.Forms.Label lblSenderDept;
        private System.Windows.Forms.Label lblFunctionID;
        internal System.Windows.Forms.TextBox txtSenderDept;
        private System.Windows.Forms.GroupBox grp_TransactionSet;
        internal System.Windows.Forms.TextBox txtTSControlNumber;
        private System.Windows.Forms.Label lblTSControlNumber;
        internal System.Windows.Forms.TextBox txtTSIdCode;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.GroupBox grp_BHT;
        private System.Windows.Forms.ComboBox cmbTypeOfTransaction;
        internal System.Windows.Forms.TextBox txtBHTTime;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txtBHTDate;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtBHTRefIdentification;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label lblBHT_transactionType;
        internal System.Windows.Forms.TextBox txtBHT_HerarchicalStrCode;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label48;
    }
}