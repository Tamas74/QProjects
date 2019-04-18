namespace gloPatient
{
    partial class gloPatientOtherInfoControl
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpRegDate, dtpSignatureDate };
            System.Windows.Forms.Control[] cntControls = { dtpRegDate, dtpSignatureDate };
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientOtherInfoControl));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnl_OtherDemogrphicDetails = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlBirthOrder = new System.Windows.Forms.Panel();
            this.cmbBirthOrder = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlMultipleBirthIndicator = new System.Windows.Forms.Panel();
            this.cmbMultipleBirthIndicator = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlOtherGenderIdentity = new System.Windows.Forms.Panel();
            this.txtOtherGenderIdentity = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlGenderIdentity = new System.Windows.Forms.Panel();
            this.cmbGenderIdentity = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlOtherSexualOrientation = new System.Windows.Forms.Panel();
            this.txtOtherSexualOrientation = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlSexualOrientation = new System.Windows.Forms.Panel();
            this.cmbSexualOrientation = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlPreviousName = new System.Windows.Forms.Panel();
            this.txtPatPRVSuffix = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.txtPatPRVMName = new System.Windows.Forms.TextBox();
            this.txtPatPRVLName = new System.Windows.Forms.TextBox();
            this.txtPatPRVFname = new System.Windows.Forms.TextBox();
            this.lblPALName = new System.Windows.Forms.Label();
            this.lblPAMName = new System.Windows.Forms.Label();
            this.lblPAFName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlCMS1500Box13 = new System.Windows.Forms.Panel();
            this.cmbCMS1500Box13 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dtpSignatureDate = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlBirthSex = new System.Windows.Forms.Panel();
            this.cmbBirthSex = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlSexualOrientationHeader = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbImRegStatus = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblPrimaryInsuDetails = new System.Windows.Forms.Label();
            this.txtLawyerName = new System.Windows.Forms.TextBox();
            this.lblLawyerName = new System.Windows.Forms.Label();
            this.cmbPatientStatus = new System.Windows.Forms.ComboBox();
            this.lblInsuInfoRegistDate = new System.Windows.Forms.Label();
            this.lblInsuInfoPatientStatus = new System.Windows.Forms.Label();
            this.lblInsuInfoSpousePhone = new System.Windows.Forms.Label();
            this.lblSpouseName = new System.Windows.Forms.Label();
            this.chkBadDebtPatient = new System.Windows.Forms.CheckBox();
            this.txtSpouseName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpRegDate = new System.Windows.Forms.DateTimePicker();
            this.btn_MedicalCategoryDel = new System.Windows.Forms.Button();
            this.mtxtSpousePhone = new gloMaskControl.gloMaskBox();
            this.btn_MedicalCategory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPAMedicalCategory = new System.Windows.Forms.ComboBox();
            this.chkReminder = new System.Windows.Forms.CheckBox();
            this.lbMedicalCategory = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTOP = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlInsuInfoHeader = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.lblInsuInfoHeader = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_AddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_RemoveLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.c1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.label18 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnl_OtherDemogrphicDetails.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlBirthOrder.SuspendLayout();
            this.pnlMultipleBirthIndicator.SuspendLayout();
            this.pnlOtherGenderIdentity.SuspendLayout();
            this.pnlGenderIdentity.SuspendLayout();
            this.pnlOtherSexualOrientation.SuspendLayout();
            this.pnlSexualOrientation.SuspendLayout();
            this.pnlPreviousName.SuspendLayout();
            this.pnlCMS1500Box13.SuspendLayout();
            this.pnlBirthSex.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlInsuInfoHeader.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.pnl_OtherDemogrphicDetails);
            this.pnlMain.Controls.Add(this.pnlTOP);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.menuStrip1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(755, 649);
            this.pnlMain.TabIndex = 0;
            // 
            // pnl_OtherDemogrphicDetails
            // 
            this.pnl_OtherDemogrphicDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnl_OtherDemogrphicDetails.Controls.Add(this.panel4);
            this.pnl_OtherDemogrphicDetails.Controls.Add(this.panel3);
            this.pnl_OtherDemogrphicDetails.Controls.Add(this.label4);
            this.pnl_OtherDemogrphicDetails.Controls.Add(this.label3);
            this.pnl_OtherDemogrphicDetails.Controls.Add(this.label5);
            this.pnl_OtherDemogrphicDetails.Controls.Add(this.label2);
            this.pnl_OtherDemogrphicDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_OtherDemogrphicDetails.Location = new System.Drawing.Point(0, 82);
            this.pnl_OtherDemogrphicDetails.Name = "pnl_OtherDemogrphicDetails";
            this.pnl_OtherDemogrphicDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_OtherDemogrphicDetails.Size = new System.Drawing.Size(755, 567);
            this.pnl_OtherDemogrphicDetails.TabIndex = 0;
            this.pnl_OtherDemogrphicDetails.TabStop = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlBirthOrder);
            this.panel4.Controls.Add(this.pnlMultipleBirthIndicator);
            this.panel4.Controls.Add(this.pnlOtherGenderIdentity);
            this.panel4.Controls.Add(this.pnlGenderIdentity);
            this.panel4.Controls.Add(this.pnlOtherSexualOrientation);
            this.panel4.Controls.Add(this.pnlSexualOrientation);
            this.panel4.Controls.Add(this.pnlPreviousName);
            this.panel4.Controls.Add(this.pnlCMS1500Box13);
            this.panel4.Controls.Add(this.pnlBirthSex);
            this.panel4.Controls.Add(this.pnlSexualOrientationHeader);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 258);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(747, 305);
            this.panel4.TabIndex = 49;
            // 
            // pnlBirthOrder
            // 
            this.pnlBirthOrder.Controls.Add(this.cmbBirthOrder);
            this.pnlBirthOrder.Controls.Add(this.label12);
            this.pnlBirthOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBirthOrder.Location = new System.Drawing.Point(0, 251);
            this.pnlBirthOrder.Name = "pnlBirthOrder";
            this.pnlBirthOrder.Size = new System.Drawing.Size(747, 28);
            this.pnlBirthOrder.TabIndex = 50;
            // 
            // cmbBirthOrder
            // 
            this.cmbBirthOrder.BackColor = System.Drawing.Color.White;
            this.cmbBirthOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBirthOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBirthOrder.ForeColor = System.Drawing.Color.Black;
            this.cmbBirthOrder.FormattingEnabled = true;
            this.cmbBirthOrder.Items.AddRange(new object[] {
            ""});
            this.cmbBirthOrder.Location = new System.Drawing.Point(218, 3);
            this.cmbBirthOrder.Name = "cmbBirthOrder";
            this.cmbBirthOrder.Size = new System.Drawing.Size(218, 22);
            this.cmbBirthOrder.TabIndex = 46;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Location = new System.Drawing.Point(139, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 14);
            this.label12.TabIndex = 45;
            this.label12.Text = "Birth Order :";
            // 
            // pnlMultipleBirthIndicator
            // 
            this.pnlMultipleBirthIndicator.Controls.Add(this.label18);
            this.pnlMultipleBirthIndicator.Controls.Add(this.cmbMultipleBirthIndicator);
            this.pnlMultipleBirthIndicator.Controls.Add(this.label13);
            this.pnlMultipleBirthIndicator.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMultipleBirthIndicator.Location = new System.Drawing.Point(0, 224);
            this.pnlMultipleBirthIndicator.Name = "pnlMultipleBirthIndicator";
            this.pnlMultipleBirthIndicator.Size = new System.Drawing.Size(747, 27);
            this.pnlMultipleBirthIndicator.TabIndex = 51;
            // 
            // cmbMultipleBirthIndicator
            // 
            this.cmbMultipleBirthIndicator.BackColor = System.Drawing.Color.White;
            this.cmbMultipleBirthIndicator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMultipleBirthIndicator.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMultipleBirthIndicator.ForeColor = System.Drawing.Color.Black;
            this.cmbMultipleBirthIndicator.FormattingEnabled = true;
            this.cmbMultipleBirthIndicator.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cmbMultipleBirthIndicator.Location = new System.Drawing.Point(218, 3);
            this.cmbMultipleBirthIndicator.Name = "cmbMultipleBirthIndicator";
            this.cmbMultipleBirthIndicator.Size = new System.Drawing.Size(218, 22);
            this.cmbMultipleBirthIndicator.TabIndex = 40;
            this.cmbMultipleBirthIndicator.SelectedIndexChanged += new System.EventHandler(this.cmbMultipleBirthIndicator_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoEllipsis = true;
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Location = new System.Drawing.Point(78, 7);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 14);
            this.label13.TabIndex = 41;
            this.label13.Text = "Multiple Birth Indicator :";
            // 
            // pnlOtherGenderIdentity
            // 
            this.pnlOtherGenderIdentity.Controls.Add(this.txtOtherGenderIdentity);
            this.pnlOtherGenderIdentity.Controls.Add(this.label10);
            this.pnlOtherGenderIdentity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOtherGenderIdentity.Location = new System.Drawing.Point(0, 196);
            this.pnlOtherGenderIdentity.Name = "pnlOtherGenderIdentity";
            this.pnlOtherGenderIdentity.Size = new System.Drawing.Size(747, 28);
            this.pnlOtherGenderIdentity.TabIndex = 48;
            this.pnlOtherGenderIdentity.Visible = false;
            // 
            // txtOtherGenderIdentity
            // 
            this.txtOtherGenderIdentity.BackColor = System.Drawing.Color.White;
            this.txtOtherGenderIdentity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherGenderIdentity.ForeColor = System.Drawing.Color.Black;
            this.txtOtherGenderIdentity.Location = new System.Drawing.Point(218, 2);
            this.txtOtherGenderIdentity.Name = "txtOtherGenderIdentity";
            this.txtOtherGenderIdentity.Size = new System.Drawing.Size(358, 22);
            this.txtOtherGenderIdentity.TabIndex = 44;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoEllipsis = true;
            this.label10.AutoSize = true;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Location = new System.Drawing.Point(123, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 14);
            this.label10.TabIndex = 45;
            this.label10.Text = "Please Specify :";
            // 
            // pnlGenderIdentity
            // 
            this.pnlGenderIdentity.Controls.Add(this.cmbGenderIdentity);
            this.pnlGenderIdentity.Controls.Add(this.label8);
            this.pnlGenderIdentity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGenderIdentity.Location = new System.Drawing.Point(0, 169);
            this.pnlGenderIdentity.Name = "pnlGenderIdentity";
            this.pnlGenderIdentity.Size = new System.Drawing.Size(747, 27);
            this.pnlGenderIdentity.TabIndex = 49;
            // 
            // cmbGenderIdentity
            // 
            this.cmbGenderIdentity.BackColor = System.Drawing.Color.White;
            this.cmbGenderIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenderIdentity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGenderIdentity.ForeColor = System.Drawing.Color.Black;
            this.cmbGenderIdentity.FormattingEnabled = true;
            this.cmbGenderIdentity.Items.AddRange(new object[] {
            "",
            "Active",
            "Deceased",
            "Legal Pending",
            "Lock Charts",
            "Default",
            "Erroneous",
            "Non-Active"});
            this.cmbGenderIdentity.Location = new System.Drawing.Point(218, 3);
            this.cmbGenderIdentity.Name = "cmbGenderIdentity";
            this.cmbGenderIdentity.Size = new System.Drawing.Size(358, 22);
            this.cmbGenderIdentity.TabIndex = 40;
            this.cmbGenderIdentity.SelectedIndexChanged += new System.EventHandler(this.cmbGenderIdentity_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(112, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 14);
            this.label8.TabIndex = 41;
            this.label8.Text = "Gender Identity :";
            // 
            // pnlOtherSexualOrientation
            // 
            this.pnlOtherSexualOrientation.Controls.Add(this.txtOtherSexualOrientation);
            this.pnlOtherSexualOrientation.Controls.Add(this.label9);
            this.pnlOtherSexualOrientation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOtherSexualOrientation.Location = new System.Drawing.Point(0, 143);
            this.pnlOtherSexualOrientation.Name = "pnlOtherSexualOrientation";
            this.pnlOtherSexualOrientation.Size = new System.Drawing.Size(747, 26);
            this.pnlOtherSexualOrientation.TabIndex = 47;
            this.pnlOtherSexualOrientation.Visible = false;
            // 
            // txtOtherSexualOrientation
            // 
            this.txtOtherSexualOrientation.BackColor = System.Drawing.Color.White;
            this.txtOtherSexualOrientation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherSexualOrientation.ForeColor = System.Drawing.Color.Black;
            this.txtOtherSexualOrientation.Location = new System.Drawing.Point(218, 2);
            this.txtOtherSexualOrientation.Name = "txtOtherSexualOrientation";
            this.txtOtherSexualOrientation.Size = new System.Drawing.Size(358, 22);
            this.txtOtherSexualOrientation.TabIndex = 44;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.AutoSize = true;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(122, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 14);
            this.label9.TabIndex = 45;
            this.label9.Text = "Please Specify :";
            // 
            // pnlSexualOrientation
            // 
            this.pnlSexualOrientation.Controls.Add(this.cmbSexualOrientation);
            this.pnlSexualOrientation.Controls.Add(this.label7);
            this.pnlSexualOrientation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSexualOrientation.Location = new System.Drawing.Point(0, 118);
            this.pnlSexualOrientation.Name = "pnlSexualOrientation";
            this.pnlSexualOrientation.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlSexualOrientation.Size = new System.Drawing.Size(747, 25);
            this.pnlSexualOrientation.TabIndex = 49;
            // 
            // cmbSexualOrientation
            // 
            this.cmbSexualOrientation.BackColor = System.Drawing.Color.White;
            this.cmbSexualOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSexualOrientation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSexualOrientation.ForeColor = System.Drawing.Color.Black;
            this.cmbSexualOrientation.FormattingEnabled = true;
            this.cmbSexualOrientation.Items.AddRange(new object[] {
            "",
            "Active",
            "Deceased",
            "Legal Pending",
            "Lock Charts",
            "Default",
            "Erroneous",
            "Non-Active"});
            this.cmbSexualOrientation.Location = new System.Drawing.Point(218, 1);
            this.cmbSexualOrientation.Name = "cmbSexualOrientation";
            this.cmbSexualOrientation.Size = new System.Drawing.Size(358, 22);
            this.cmbSexualOrientation.TabIndex = 38;
            this.cmbSexualOrientation.SelectedIndexChanged += new System.EventHandler(this.cmbSexualOrientation_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(99, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 14);
            this.label7.TabIndex = 39;
            this.label7.Text = "Sexual Orientation :";
            // 
            // pnlPreviousName
            // 
            this.pnlPreviousName.Controls.Add(this.txtPatPRVSuffix);
            this.pnlPreviousName.Controls.Add(this.label62);
            this.pnlPreviousName.Controls.Add(this.txtPatPRVMName);
            this.pnlPreviousName.Controls.Add(this.txtPatPRVLName);
            this.pnlPreviousName.Controls.Add(this.txtPatPRVFname);
            this.pnlPreviousName.Controls.Add(this.lblPALName);
            this.pnlPreviousName.Controls.Add(this.lblPAMName);
            this.pnlPreviousName.Controls.Add(this.lblPAFName);
            this.pnlPreviousName.Controls.Add(this.label11);
            this.pnlPreviousName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPreviousName.Location = new System.Drawing.Point(0, 73);
            this.pnlPreviousName.Name = "pnlPreviousName";
            this.pnlPreviousName.Size = new System.Drawing.Size(747, 45);
            this.pnlPreviousName.TabIndex = 48;
            // 
            // txtPatPRVSuffix
            // 
            this.txtPatPRVSuffix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPatPRVSuffix.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatPRVSuffix.Location = new System.Drawing.Point(580, 3);
            this.txtPatPRVSuffix.MaxLength = 10;
            this.txtPatPRVSuffix.Name = "txtPatPRVSuffix";
            this.txtPatPRVSuffix.Size = new System.Drawing.Size(46, 22);
            this.txtPatPRVSuffix.TabIndex = 1012;
            this.txtPatPRVSuffix.Visible = false;
            // 
            // label62
            // 
            this.label62.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label62.AutoEllipsis = true;
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label62.Location = new System.Drawing.Point(584, 26);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(38, 12);
            this.label62.TabIndex = 1017;
            this.label62.Text = "(Suffix)";
            this.label62.Visible = false;
            // 
            // txtPatPRVMName
            // 
            this.txtPatPRVMName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPatPRVMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatPRVMName.Location = new System.Drawing.Point(363, 3);
            this.txtPatPRVMName.MaxLength = 35;
            this.txtPatPRVMName.Name = "txtPatPRVMName";
            this.txtPatPRVMName.Size = new System.Drawing.Size(73, 22);
            this.txtPatPRVMName.TabIndex = 1010;
            // 
            // txtPatPRVLName
            // 
            this.txtPatPRVLName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPatPRVLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatPRVLName.Location = new System.Drawing.Point(437, 3);
            this.txtPatPRVLName.MaxLength = 50;
            this.txtPatPRVLName.Name = "txtPatPRVLName";
            this.txtPatPRVLName.Size = new System.Drawing.Size(142, 22);
            this.txtPatPRVLName.TabIndex = 1011;
            // 
            // txtPatPRVFname
            // 
            this.txtPatPRVFname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPatPRVFname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatPRVFname.Location = new System.Drawing.Point(220, 3);
            this.txtPatPRVFname.MaxLength = 50;
            this.txtPatPRVFname.Name = "txtPatPRVFname";
            this.txtPatPRVFname.Size = new System.Drawing.Size(142, 22);
            this.txtPatPRVFname.TabIndex = 1009;
            // 
            // lblPALName
            // 
            this.lblPALName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPALName.AutoEllipsis = true;
            this.lblPALName.AutoSize = true;
            this.lblPALName.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.lblPALName.Location = new System.Drawing.Point(486, 26);
            this.lblPALName.Name = "lblPALName";
            this.lblPALName.Size = new System.Drawing.Size(58, 12);
            this.lblPALName.TabIndex = 1015;
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
            this.lblPAMName.Location = new System.Drawing.Point(389, 26);
            this.lblPAMName.Name = "lblPAMName";
            this.lblPAMName.Size = new System.Drawing.Size(25, 12);
            this.lblPAMName.TabIndex = 1014;
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
            this.lblPAFName.Location = new System.Drawing.Point(268, 26);
            this.lblPAFName.Name = "lblPAFName";
            this.lblPAFName.Size = new System.Drawing.Size(60, 12);
            this.lblPAFName.TabIndex = 1013;
            this.lblPAFName.Text = "(First Name)";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoEllipsis = true;
            this.label11.AutoSize = true;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(119, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 14);
            this.label11.TabIndex = 45;
            this.label11.Text = "Previous Name :";
            // 
            // pnlCMS1500Box13
            // 
            this.pnlCMS1500Box13.Controls.Add(this.cmbCMS1500Box13);
            this.pnlCMS1500Box13.Controls.Add(this.label14);
            this.pnlCMS1500Box13.Controls.Add(this.dtpSignatureDate);
            this.pnlCMS1500Box13.Controls.Add(this.label15);
            this.pnlCMS1500Box13.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCMS1500Box13.Location = new System.Drawing.Point(0, 45);
            this.pnlCMS1500Box13.Name = "pnlCMS1500Box13";
            this.pnlCMS1500Box13.Size = new System.Drawing.Size(747, 28);
            this.pnlCMS1500Box13.TabIndex = 49;
            this.pnlCMS1500Box13.Visible = false;
            // 
            // cmbCMS1500Box13
            // 
            this.cmbCMS1500Box13.BackColor = System.Drawing.Color.White;
            this.cmbCMS1500Box13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCMS1500Box13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCMS1500Box13.ForeColor = System.Drawing.Color.Black;
            this.cmbCMS1500Box13.FormattingEnabled = true;
            this.cmbCMS1500Box13.Items.AddRange(new object[] {
            "",
            "Pay to Provider",
            "Pay to Subscriber"});
            this.cmbCMS1500Box13.Location = new System.Drawing.Point(218, 3);
            this.cmbCMS1500Box13.Name = "cmbCMS1500Box13";
            this.cmbCMS1500Box13.Size = new System.Drawing.Size(218, 22);
            this.cmbCMS1500Box13.TabIndex = 0;
            this.cmbCMS1500Box13.TabStop = false;
            this.cmbCMS1500Box13.Visible = false;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoEllipsis = true;
            this.label14.AutoSize = true;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(464, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 14);
            this.label14.TabIndex = 0;
            this.label14.Text = "Signature On File Date :";
            this.label14.Visible = false;
            // 
            // dtpSignatureDate
            // 
            this.dtpSignatureDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpSignatureDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpSignatureDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpSignatureDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpSignatureDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpSignatureDate.CustomFormat = "MM/dd/yyy";
            this.dtpSignatureDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSignatureDate.Location = new System.Drawing.Point(604, 3);
            this.dtpSignatureDate.Name = "dtpSignatureDate";
            this.dtpSignatureDate.Size = new System.Drawing.Size(87, 22);
            this.dtpSignatureDate.TabIndex = 0;
            this.dtpSignatureDate.TabStop = false;
            this.dtpSignatureDate.Visible = false;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoEllipsis = true;
            this.label15.AutoSize = true;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Location = new System.Drawing.Point(101, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 14);
            this.label15.TabIndex = 0;
            this.label15.Text = "CMS 1500 Box 13 :";
            this.label15.Visible = false;
            // 
            // pnlBirthSex
            // 
            this.pnlBirthSex.Controls.Add(this.cmbBirthSex);
            this.pnlBirthSex.Controls.Add(this.label16);
            this.pnlBirthSex.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBirthSex.Location = new System.Drawing.Point(0, 17);
            this.pnlBirthSex.Name = "pnlBirthSex";
            this.pnlBirthSex.Size = new System.Drawing.Size(747, 28);
            this.pnlBirthSex.TabIndex = 50;
            this.pnlBirthSex.Visible = false;
            // 
            // cmbBirthSex
            // 
            this.cmbBirthSex.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbBirthSex.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBirthSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbBirthSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBirthSex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBirthSex.FormattingEnabled = true;
            this.cmbBirthSex.Location = new System.Drawing.Point(219, 3);
            this.cmbBirthSex.Name = "cmbBirthSex";
            this.cmbBirthSex.Size = new System.Drawing.Size(218, 22);
            this.cmbBirthSex.TabIndex = 38;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoEllipsis = true;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(150, 7);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 14);
            this.label16.TabIndex = 39;
            this.label16.Text = "Birth Sex :";
            // 
            // pnlSexualOrientationHeader
            // 
            this.pnlSexualOrientationHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSexualOrientationHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSexualOrientationHeader.Name = "pnlSexualOrientationHeader";
            this.pnlSexualOrientationHeader.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlSexualOrientationHeader.Size = new System.Drawing.Size(747, 17);
            this.pnlSexualOrientationHeader.TabIndex = 50;
            this.pnlSexualOrientationHeader.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbImRegStatus);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.lblPrimaryInsuDetails);
            this.panel3.Controls.Add(this.txtLawyerName);
            this.panel3.Controls.Add(this.lblLawyerName);
            this.panel3.Controls.Add(this.cmbPatientStatus);
            this.panel3.Controls.Add(this.lblInsuInfoRegistDate);
            this.panel3.Controls.Add(this.lblInsuInfoPatientStatus);
            this.panel3.Controls.Add(this.lblInsuInfoSpousePhone);
            this.panel3.Controls.Add(this.lblSpouseName);
            this.panel3.Controls.Add(this.chkBadDebtPatient);
            this.panel3.Controls.Add(this.txtSpouseName);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.dtpRegDate);
            this.panel3.Controls.Add(this.btn_MedicalCategoryDel);
            this.panel3.Controls.Add(this.mtxtSpousePhone);
            this.panel3.Controls.Add(this.btn_MedicalCategory);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cmbPAMedicalCategory);
            this.panel3.Controls.Add(this.chkReminder);
            this.panel3.Controls.Add(this.lbMedicalCategory);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(747, 254);
            this.panel3.TabIndex = 49;
            // 
            // cmbImRegStatus
            // 
            this.cmbImRegStatus.BackColor = System.Drawing.Color.White;
            this.cmbImRegStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImRegStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbImRegStatus.ForeColor = System.Drawing.Color.Black;
            this.cmbImRegStatus.FormattingEnabled = true;
            this.cmbImRegStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive",
            "Inactive-Lost to follow-up",
            "Inactive-Moved or gone elsewhere",
            "Inactive-Permanently inactive",
            "Unknown"});
            this.cmbImRegStatus.Location = new System.Drawing.Point(218, 155);
            this.cmbImRegStatus.Name = "cmbImRegStatus";
            this.cmbImRegStatus.Size = new System.Drawing.Size(218, 22);
            this.cmbImRegStatus.TabIndex = 41;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoEllipsis = true;
            this.label17.AutoSize = true;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Location = new System.Drawing.Point(46, 159);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(168, 14);
            this.label17.TabIndex = 38;
            this.label17.Text = "Immunization registry status :";
            // 
            // lblPrimaryInsuDetails
            // 
            this.lblPrimaryInsuDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPrimaryInsuDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimaryInsuDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPrimaryInsuDetails.Location = new System.Drawing.Point(0, 0);
            this.lblPrimaryInsuDetails.Name = "lblPrimaryInsuDetails";
            this.lblPrimaryInsuDetails.Size = new System.Drawing.Size(747, 27);
            this.lblPrimaryInsuDetails.TabIndex = 0;
            this.lblPrimaryInsuDetails.Text = "  Patient Other Demographic  Details : ";
            this.lblPrimaryInsuDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLawyerName
            // 
            this.txtLawyerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLawyerName.ForeColor = System.Drawing.Color.Black;
            this.txtLawyerName.Location = new System.Drawing.Point(218, 28);
            this.txtLawyerName.Name = "txtLawyerName";
            this.txtLawyerName.Size = new System.Drawing.Size(358, 22);
            this.txtLawyerName.TabIndex = 3;
            this.txtLawyerName.Text = "1";
            // 
            // lblLawyerName
            // 
            this.lblLawyerName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLawyerName.AutoEllipsis = true;
            this.lblLawyerName.AutoSize = true;
            this.lblLawyerName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLawyerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLawyerName.Location = new System.Drawing.Point(82, 33);
            this.lblLawyerName.Name = "lblLawyerName";
            this.lblLawyerName.Size = new System.Drawing.Size(132, 14);
            this.lblLawyerName.TabIndex = 2;
            this.lblLawyerName.Text = "Patient Lawyer Name :";
            // 
            // cmbPatientStatus
            // 
            this.cmbPatientStatus.BackColor = System.Drawing.Color.White;
            this.cmbPatientStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatientStatus.ForeColor = System.Drawing.Color.Black;
            this.cmbPatientStatus.FormattingEnabled = true;
            this.cmbPatientStatus.Items.AddRange(new object[] {
            "",
            "Active",
            "Deceased",
            "Legal Pending",
            "Lock Charts",
            "Default",
            "Erroneous",
            "Non-Active"});
            this.cmbPatientStatus.Location = new System.Drawing.Point(218, 109);
            this.cmbPatientStatus.Name = "cmbPatientStatus";
            this.cmbPatientStatus.Size = new System.Drawing.Size(218, 22);
            this.cmbPatientStatus.TabIndex = 4;
            // 
            // lblInsuInfoRegistDate
            // 
            this.lblInsuInfoRegistDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsuInfoRegistDate.AutoEllipsis = true;
            this.lblInsuInfoRegistDate.AutoSize = true;
            this.lblInsuInfoRegistDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuInfoRegistDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuInfoRegistDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsuInfoRegistDate.Location = new System.Drawing.Point(105, 186);
            this.lblInsuInfoRegistDate.Name = "lblInsuInfoRegistDate";
            this.lblInsuInfoRegistDate.Size = new System.Drawing.Size(109, 14);
            this.lblInsuInfoRegistDate.TabIndex = 10;
            this.lblInsuInfoRegistDate.Text = "Registration Date :";
            // 
            // lblInsuInfoPatientStatus
            // 
            this.lblInsuInfoPatientStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsuInfoPatientStatus.AutoEllipsis = true;
            this.lblInsuInfoPatientStatus.AutoSize = true;
            this.lblInsuInfoPatientStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuInfoPatientStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuInfoPatientStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsuInfoPatientStatus.Location = new System.Drawing.Point(121, 112);
            this.lblInsuInfoPatientStatus.Name = "lblInsuInfoPatientStatus";
            this.lblInsuInfoPatientStatus.Size = new System.Drawing.Size(93, 14);
            this.lblInsuInfoPatientStatus.TabIndex = 6;
            this.lblInsuInfoPatientStatus.Text = "Patient Status :";
            // 
            // lblInsuInfoSpousePhone
            // 
            this.lblInsuInfoSpousePhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsuInfoSpousePhone.AutoEllipsis = true;
            this.lblInsuInfoSpousePhone.AutoSize = true;
            this.lblInsuInfoSpousePhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuInfoSpousePhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuInfoSpousePhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblInsuInfoSpousePhone.Location = new System.Drawing.Point(120, 86);
            this.lblInsuInfoSpousePhone.Name = "lblInsuInfoSpousePhone";
            this.lblInsuInfoSpousePhone.Size = new System.Drawing.Size(94, 14);
            this.lblInsuInfoSpousePhone.TabIndex = 8;
            this.lblInsuInfoSpousePhone.Text = "Spouse Phone :";
            // 
            // lblSpouseName
            // 
            this.lblSpouseName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpouseName.AutoEllipsis = true;
            this.lblSpouseName.AutoSize = true;
            this.lblSpouseName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSpouseName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpouseName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSpouseName.Location = new System.Drawing.Point(124, 60);
            this.lblSpouseName.Name = "lblSpouseName";
            this.lblSpouseName.Size = new System.Drawing.Size(90, 14);
            this.lblSpouseName.TabIndex = 4;
            this.lblSpouseName.Text = "Spouse Name :";
            // 
            // chkBadDebtPatient
            // 
            this.chkBadDebtPatient.AutoSize = true;
            this.chkBadDebtPatient.Location = new System.Drawing.Point(218, 136);
            this.chkBadDebtPatient.Name = "chkBadDebtPatient";
            this.chkBadDebtPatient.Size = new System.Drawing.Size(15, 14);
            this.chkBadDebtPatient.TabIndex = 5;
            this.chkBadDebtPatient.UseVisualStyleBackColor = true;
            // 
            // txtSpouseName
            // 
            this.txtSpouseName.BackColor = System.Drawing.Color.White;
            this.txtSpouseName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpouseName.ForeColor = System.Drawing.Color.Black;
            this.txtSpouseName.Location = new System.Drawing.Point(218, 55);
            this.txtSpouseName.Name = "txtSpouseName";
            this.txtSpouseName.Size = new System.Drawing.Size(358, 22);
            this.txtSpouseName.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(92, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 14);
            this.label6.TabIndex = 37;
            this.label6.Text = "Is Bad Debt Patient :";
            // 
            // dtpRegDate
            // 
            this.dtpRegDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpRegDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpRegDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpRegDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpRegDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpRegDate.CustomFormat = "MM/dd/yyy";
            this.dtpRegDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRegDate.Location = new System.Drawing.Point(218, 182);
            this.dtpRegDate.Name = "dtpRegDate";
            this.dtpRegDate.Size = new System.Drawing.Size(218, 22);
            this.dtpRegDate.TabIndex = 6;
            // 
            // btn_MedicalCategoryDel
            // 
            this.btn_MedicalCategoryDel.AutoEllipsis = true;
            this.btn_MedicalCategoryDel.BackColor = System.Drawing.Color.Transparent;
            this.btn_MedicalCategoryDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_MedicalCategoryDel.BackgroundImage")));
            this.btn_MedicalCategoryDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_MedicalCategoryDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MedicalCategoryDel.Image = ((System.Drawing.Image)(resources.GetObject("btn_MedicalCategoryDel.Image")));
            this.btn_MedicalCategoryDel.Location = new System.Drawing.Point(415, 228);
            this.btn_MedicalCategoryDel.Name = "btn_MedicalCategoryDel";
            this.btn_MedicalCategoryDel.Size = new System.Drawing.Size(22, 22);
            this.btn_MedicalCategoryDel.TabIndex = 34;
            this.btn_MedicalCategoryDel.UseVisualStyleBackColor = false;
            this.btn_MedicalCategoryDel.Click += new System.EventHandler(this.btn_MedicalCategoryDel_Click);
            this.btn_MedicalCategoryDel.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_MedicalCategoryDel.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // mtxtSpousePhone
            // 
            this.mtxtSpousePhone.AllowValidate = true;
            this.mtxtSpousePhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtSpousePhone.IncludeLiteralsAndPrompts = false;
            this.mtxtSpousePhone.Location = new System.Drawing.Point(218, 82);
            this.mtxtSpousePhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxtSpousePhone.Name = "mtxtSpousePhone";
            this.mtxtSpousePhone.ReadOnly = false;
            this.mtxtSpousePhone.Size = new System.Drawing.Size(218, 22);
            this.mtxtSpousePhone.TabIndex = 3;
            // 
            // btn_MedicalCategory
            // 
            this.btn_MedicalCategory.AutoEllipsis = true;
            this.btn_MedicalCategory.BackColor = System.Drawing.Color.Transparent;
            this.btn_MedicalCategory.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btn_MedicalCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_MedicalCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MedicalCategory.Image = ((System.Drawing.Image)(resources.GetObject("btn_MedicalCategory.Image")));
            this.btn_MedicalCategory.Location = new System.Drawing.Point(391, 228);
            this.btn_MedicalCategory.Name = "btn_MedicalCategory";
            this.btn_MedicalCategory.Size = new System.Drawing.Size(22, 22);
            this.btn_MedicalCategory.TabIndex = 33;
            this.btn_MedicalCategory.UseVisualStyleBackColor = false;
            this.btn_MedicalCategory.Click += new System.EventHandler(this.btn_MedicalCategory_Click);
            this.btn_MedicalCategory.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_MedicalCategory.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(50, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 14);
            this.label1.TabIndex = 27;
            this.label1.Text = "Patient Reminders Declined :";
            // 
            // cmbPAMedicalCategory
            // 
            this.cmbPAMedicalCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbPAMedicalCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPAMedicalCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPAMedicalCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPAMedicalCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPAMedicalCategory.FormattingEnabled = true;
            this.cmbPAMedicalCategory.Location = new System.Drawing.Point(218, 228);
            this.cmbPAMedicalCategory.Name = "cmbPAMedicalCategory";
            this.cmbPAMedicalCategory.Size = new System.Drawing.Size(169, 22);
            this.cmbPAMedicalCategory.TabIndex = 32;
            this.cmbPAMedicalCategory.SelectedIndexChanged += new System.EventHandler(this.cmbPAMedicalCategory_SelectedIndexChanged);
            // 
            // chkReminder
            // 
            this.chkReminder.AutoSize = true;
            this.chkReminder.Location = new System.Drawing.Point(218, 209);
            this.chkReminder.Name = "chkReminder";
            this.chkReminder.Size = new System.Drawing.Size(15, 14);
            this.chkReminder.TabIndex = 7;
            this.chkReminder.UseVisualStyleBackColor = true;
            // 
            // lbMedicalCategory
            // 
            this.lbMedicalCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMedicalCategory.AutoEllipsis = true;
            this.lbMedicalCategory.AutoSize = true;
            this.lbMedicalCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMedicalCategory.Location = new System.Drawing.Point(107, 232);
            this.lbMedicalCategory.Name = "lbMedicalCategory";
            this.lbMedicalCategory.Size = new System.Drawing.Size(107, 14);
            this.lbMedicalCategory.TabIndex = 35;
            this.lbMedicalCategory.Text = "Medical Category :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 563);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(747, 1);
            this.label4.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(747, 1);
            this.label3.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(751, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 561);
            this.label5.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 561);
            this.label2.TabIndex = 24;
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTOP.Controls.Add(this.ts_Commands);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTOP.Location = new System.Drawing.Point(0, 28);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlTOP.Size = new System.Drawing.Size(755, 54);
            this.pnlTOP.TabIndex = 27;
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
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(749, 53);
            this.ts_Commands.TabIndex = 12;
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
            this.panel1.Controls.Add(this.pnlInsuInfoHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(755, 28);
            this.panel1.TabIndex = 28;
            this.panel1.Visible = false;
            // 
            // pnlInsuInfoHeader
            // 
            this.pnlInsuInfoHeader.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlInsuInfoHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlInsuInfoHeader.Controls.Add(this.lbl_BottomBrd);
            this.pnlInsuInfoHeader.Controls.Add(this.lbl_LeftBrd);
            this.pnlInsuInfoHeader.Controls.Add(this.lbl_RightBrd);
            this.pnlInsuInfoHeader.Controls.Add(this.lbl_TopBrd);
            this.pnlInsuInfoHeader.Controls.Add(this.lblInsuInfoHeader);
            this.pnlInsuInfoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInsuInfoHeader.Location = new System.Drawing.Point(3, 3);
            this.pnlInsuInfoHeader.Name = "pnlInsuInfoHeader";
            this.pnlInsuInfoHeader.Size = new System.Drawing.Size(749, 22);
            this.pnlInsuInfoHeader.TabIndex = 1;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 21);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(747, 1);
            this.lbl_BottomBrd.TabIndex = 0;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 21);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(748, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 21);
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
            this.lbl_TopBrd.Size = new System.Drawing.Size(749, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // lblInsuInfoHeader
            // 
            this.lblInsuInfoHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblInsuInfoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInsuInfoHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsuInfoHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsuInfoHeader.ForeColor = System.Drawing.Color.White;
            this.lblInsuInfoHeader.Location = new System.Drawing.Point(0, 0);
            this.lblInsuInfoHeader.Name = "lblInsuInfoHeader";
            this.lblInsuInfoHeader.Size = new System.Drawing.Size(749, 22);
            this.lblInsuInfoHeader.TabIndex = 0;
            this.lblInsuInfoHeader.Text = "  Other Details";
            this.lblInsuInfoHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(755, 24);
            this.menuStrip1.TabIndex = 218;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // mnuBilling
            // 
            this.mnuBilling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling_AddLine,
            this.mnuBilling_RemoveLine});
            this.mnuBilling.Name = "mnuBilling";
            this.mnuBilling.Size = new System.Drawing.Size(22, 20);
            this.mnuBilling.Text = " ";
            // 
            // mnuBilling_AddLine
            // 
            this.mnuBilling_AddLine.Name = "mnuBilling_AddLine";
            this.mnuBilling_AddLine.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuBilling_AddLine.Size = new System.Drawing.Size(161, 22);
            this.mnuBilling_AddLine.Text = "Add Line";
            // 
            // mnuBilling_RemoveLine
            // 
            this.mnuBilling_RemoveLine.Name = "mnuBilling_RemoveLine";
            this.mnuBilling_RemoveLine.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuBilling_RemoveLine.Size = new System.Drawing.Size(161, 22);
            this.mnuBilling_RemoveLine.Text = "Remove Line";
            // 
            // c1SuperTooltip1
            // 
            this.c1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoEllipsis = true;
            this.label18.AutoSize = true;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Location = new System.Drawing.Point(442, 7);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(122, 14);
            this.label18.TabIndex = 46;
            this.label18.Text = "1 - No, 2 to 16 - Yes";
            // 
            // gloPatientOtherInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPatientOtherInfoControl";
            this.Size = new System.Drawing.Size(755, 649);
            this.Load += new System.EventHandler(this.gloPatientOtherInfoControl_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnl_OtherDemogrphicDetails.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlBirthOrder.ResumeLayout(false);
            this.pnlBirthOrder.PerformLayout();
            this.pnlMultipleBirthIndicator.ResumeLayout(false);
            this.pnlMultipleBirthIndicator.PerformLayout();
            this.pnlOtherGenderIdentity.ResumeLayout(false);
            this.pnlOtherGenderIdentity.PerformLayout();
            this.pnlGenderIdentity.ResumeLayout(false);
            this.pnlGenderIdentity.PerformLayout();
            this.pnlOtherSexualOrientation.ResumeLayout(false);
            this.pnlOtherSexualOrientation.PerformLayout();
            this.pnlSexualOrientation.ResumeLayout(false);
            this.pnlSexualOrientation.PerformLayout();
            this.pnlPreviousName.ResumeLayout(false);
            this.pnlPreviousName.PerformLayout();
            this.pnlCMS1500Box13.ResumeLayout(false);
            this.pnlCMS1500Box13.PerformLayout();
            this.pnlBirthSex.ResumeLayout(false);
            this.pnlBirthSex.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlTOP.ResumeLayout(false);
            this.pnlTOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlInsuInfoHeader.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlInsuInfoHeader;
        private System.Windows.Forms.Label lblInsuInfoHeader;
        private System.Windows.Forms.Panel pnlTOP;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnl_OtherDemogrphicDetails;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPrimaryInsuDetails;
        private System.Windows.Forms.Label lblLawyerName;
        private System.Windows.Forms.TextBox txtLawyerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.DateTimePicker dtpRegDate;
        private System.Windows.Forms.TextBox txtSpouseName;
        private System.Windows.Forms.Label lblSpouseName;
        private System.Windows.Forms.Label lblInsuInfoSpousePhone;
        private System.Windows.Forms.Label lblInsuInfoPatientStatus;
        private System.Windows.Forms.Label lblInsuInfoRegistDate;
        private System.Windows.Forms.ComboBox cmbPatientStatus;
        private gloMaskControl.gloMaskBox mtxtSpousePhone;
        private System.Windows.Forms.DateTimePicker dtpSignatureDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbCMS1500Box13;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_AddLine;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_RemoveLine;
        private System.Windows.Forms.ToolTip toolTip1;
        private C1.Win.C1SuperTooltip.C1SuperTooltip c1SuperTooltip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkReminder;
        private System.Windows.Forms.Button btn_MedicalCategoryDel;
        private System.Windows.Forms.Button btn_MedicalCategory;
        private System.Windows.Forms.ComboBox cmbPAMedicalCategory;
        private System.Windows.Forms.Label lbMedicalCategory;
        private System.Windows.Forms.CheckBox chkBadDebtPatient;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbGenderIdentity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSexualOrientation;
        private System.Windows.Forms.Panel pnlOtherSexualOrientation;
        private System.Windows.Forms.TextBox txtOtherSexualOrientation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnlOtherGenderIdentity;
        private System.Windows.Forms.TextBox txtOtherGenderIdentity;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlGenderIdentity;
        private System.Windows.Forms.Panel pnlSexualOrientation;
        private System.Windows.Forms.Panel pnlCMS1500Box13;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlPreviousName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPatPRVSuffix;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txtPatPRVMName;
        private System.Windows.Forms.TextBox txtPatPRVLName;
        private System.Windows.Forms.TextBox txtPatPRVFname;
        private System.Windows.Forms.Label lblPALName;
        private System.Windows.Forms.Label lblPAMName;
        private System.Windows.Forms.Label lblPAFName;
        private System.Windows.Forms.Panel pnlBirthOrder;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlMultipleBirthIndicator;
        private System.Windows.Forms.ComboBox cmbMultipleBirthIndicator;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbBirthSex;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbBirthOrder;
        private System.Windows.Forms.Panel pnlSexualOrientationHeader;
        private System.Windows.Forms.Panel pnlBirthSex;
        private System.Windows.Forms.ComboBox cmbImRegStatus;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
    }
}
