namespace gloQRDAImport
{
    partial class frmQRDAImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQRDAImport));
            this.gbDatabase = new System.Windows.Forms.GroupBox();
            this.chkSQLAuthentication = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.bllDatabase = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.blbServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.chkregprov = new System.Windows.Forms.CheckBox();
            this.chkGetPathFromClipboard = new System.Windows.Forms.CheckBox();
            this.lblProcessingfilename = new System.Windows.Forms.Label();
            this.lblTotalQRDAfiles = new System.Windows.Forms.Label();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cmbProviders = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.chkImportCQMData = new System.Windows.Forms.CheckBox();
            this.chkShowJson = new System.Windows.Forms.CheckBox();
            this.tls_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.cmdImport = new System.Windows.Forms.ToolStripButton();
            this.btnCompareQRDA = new System.Windows.Forms.ToolStripButton();
            this.btnjson = new System.Windows.Forms.ToolStripButton();
            this.tsb_ImportCQM = new System.Windows.Forms.ToolStripButton();
            this.btnExportProvider = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlflexgrid = new System.Windows.Forms.Panel();
            this.lblGridBottom = new System.Windows.Forms.Label();
            this.lblGridLeft = new System.Windows.Forms.Label();
            this.lblGridRight = new System.Windows.Forms.Label();
            this.lblGridTop = new System.Windows.Forms.Label();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.gbDatabase.SuspendLayout();
            this.pnlToolstrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.pnlflexgrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDatabase
            // 
            this.gbDatabase.Controls.Add(this.chkSQLAuthentication);
            this.gbDatabase.Controls.Add(this.txtPassword);
            this.gbDatabase.Controls.Add(this.lblPassword);
            this.gbDatabase.Controls.Add(this.txtUser);
            this.gbDatabase.Controls.Add(this.lblUser);
            this.gbDatabase.Controls.Add(this.bllDatabase);
            this.gbDatabase.Controls.Add(this.txtDatabase);
            this.gbDatabase.Controls.Add(this.btnConnect);
            this.gbDatabase.Controls.Add(this.blbServer);
            this.gbDatabase.Controls.Add(this.txtServer);
            this.gbDatabase.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatabase.ForeColor = System.Drawing.Color.Black;
            this.gbDatabase.Location = new System.Drawing.Point(468, 576);
            this.gbDatabase.Name = "gbDatabase";
            this.gbDatabase.Size = new System.Drawing.Size(119, 96);
            this.gbDatabase.TabIndex = 12;
            this.gbDatabase.TabStop = false;
            this.gbDatabase.Text = "Database Credentials";
            this.gbDatabase.Visible = false;
            // 
            // chkSQLAuthentication
            // 
            this.chkSQLAuthentication.AutoSize = true;
            this.chkSQLAuthentication.Checked = true;
            this.chkSQLAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSQLAuthentication.ForeColor = System.Drawing.Color.Black;
            this.chkSQLAuthentication.Location = new System.Drawing.Point(101, 52);
            this.chkSQLAuthentication.Name = "chkSQLAuthentication";
            this.chkSQLAuthentication.Size = new System.Drawing.Size(147, 18);
            this.chkSQLAuthentication.TabIndex = 12;
            this.chkSQLAuthentication.Text = "SQL Authentication";
            this.chkSQLAuthentication.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(313, 75);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(128, 22);
            this.txtPassword.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(245, 79);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(66, 14);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password :";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.Black;
            this.txtUser.Location = new System.Drawing.Point(101, 75);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(128, 22);
            this.txtUser.TabIndex = 3;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.Black;
            this.lblUser.Location = new System.Drawing.Point(32, 79);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(65, 14);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "SQL User :";
            // 
            // bllDatabase
            // 
            this.bllDatabase.AutoSize = true;
            this.bllDatabase.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bllDatabase.ForeColor = System.Drawing.Color.Black;
            this.bllDatabase.Location = new System.Drawing.Point(246, 28);
            this.bllDatabase.Name = "bllDatabase";
            this.bllDatabase.Size = new System.Drawing.Size(65, 14);
            this.bllDatabase.TabIndex = 7;
            this.bllDatabase.Text = "Database :";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabase.ForeColor = System.Drawing.Color.Black;
            this.txtDatabase.Location = new System.Drawing.Point(313, 24);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(128, 22);
            this.txtDatabase.TabIndex = 2;
            // 
            // btnConnect
            // 
            this.btnConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConnect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(447, 73);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(128, 25);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Test Connection";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // blbServer
            // 
            this.blbServer.AutoSize = true;
            this.blbServer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blbServer.ForeColor = System.Drawing.Color.Black;
            this.blbServer.Location = new System.Drawing.Point(21, 28);
            this.blbServer.Name = "blbServer";
            this.blbServer.Size = new System.Drawing.Size(76, 14);
            this.blbServer.TabIndex = 11;
            this.blbServer.Text = "SQL Server :";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.ForeColor = System.Drawing.Color.Black;
            this.txtServer.Location = new System.Drawing.Point(101, 24);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(128, 22);
            this.txtServer.TabIndex = 1;
            // 
            // chkregprov
            // 
            this.chkregprov.AutoSize = true;
            this.chkregprov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkregprov.Location = new System.Drawing.Point(21, 21);
            this.chkregprov.Name = "chkregprov";
            this.chkregprov.Size = new System.Drawing.Size(118, 18);
            this.chkregprov.TabIndex = 75;
            this.chkregprov.Text = "Register Provider";
            this.chkregprov.UseVisualStyleBackColor = true;
            // 
            // chkGetPathFromClipboard
            // 
            this.chkGetPathFromClipboard.AutoSize = true;
            this.chkGetPathFromClipboard.Checked = true;
            this.chkGetPathFromClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGetPathFromClipboard.ForeColor = System.Drawing.Color.Black;
            this.chkGetPathFromClipboard.Location = new System.Drawing.Point(266, 586);
            this.chkGetPathFromClipboard.Name = "chkGetPathFromClipboard";
            this.chkGetPathFromClipboard.Size = new System.Drawing.Size(159, 18);
            this.chkGetPathFromClipboard.TabIndex = 74;
            this.chkGetPathFromClipboard.Text = "Get Path From Cilpboard";
            this.chkGetPathFromClipboard.UseVisualStyleBackColor = true;
            this.chkGetPathFromClipboard.Visible = false;
            // 
            // lblProcessingfilename
            // 
            this.lblProcessingfilename.AutoSize = true;
            this.lblProcessingfilename.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessingfilename.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProcessingfilename.Location = new System.Drawing.Point(18, 176);
            this.lblProcessingfilename.Name = "lblProcessingfilename";
            this.lblProcessingfilename.Size = new System.Drawing.Size(85, 14);
            this.lblProcessingfilename.TabIndex = 73;
            this.lblProcessingfilename.Tag = "";
            this.lblProcessingfilename.Text = "Processing File";
            this.lblProcessingfilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalQRDAfiles
            // 
            this.lblTotalQRDAfiles.AutoSize = true;
            this.lblTotalQRDAfiles.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQRDAfiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTotalQRDAfiles.Location = new System.Drawing.Point(18, 153);
            this.lblTotalQRDAfiles.Name = "lblTotalQRDAfiles";
            this.lblTotalQRDAfiles.Size = new System.Drawing.Size(172, 14);
            this.lblTotalQRDAfiles.TabIndex = 72;
            this.lblTotalQRDAfiles.Text = "Total QRDA Files to Import";
            this.lblTotalQRDAfiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProcessStatus
            // 
            this.lblProcessStatus.AutoSize = true;
            this.lblProcessStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProcessStatus.Location = new System.Drawing.Point(18, 199);
            this.lblProcessStatus.Name = "lblProcessStatus";
            this.lblProcessStatus.Size = new System.Drawing.Size(133, 14);
            this.lblProcessStatus.TabIndex = 71;
            this.lblProcessStatus.Text = "Importing QRDA 0 of 0";
            this.lblProcessStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Black;
            this.progressBar1.Location = new System.Drawing.Point(18, 231);
            this.progressBar1.MarqueeAnimationSpeed = 500;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(626, 19);
            this.progressBar1.TabIndex = 70;
            // 
            // cmbProviders
            // 
            this.cmbProviders.ForeColor = System.Drawing.Color.Black;
            this.cmbProviders.FormattingEnabled = true;
            this.cmbProviders.Location = new System.Drawing.Point(117, 110);
            this.cmbProviders.Name = "cmbProviders";
            this.cmbProviders.Size = new System.Drawing.Size(488, 22);
            this.cmbProviders.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(17, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "Select Provider :";
            // 
            // txtPath
            // 
            this.txtPath.ForeColor = System.Drawing.Color.Black;
            this.txtPath.Location = new System.Drawing.Point(117, 82);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(488, 22);
            this.txtPath.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(28, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "Select Folder :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(18, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "New Patient Register from File Information";
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.Controls.Add(this.chkImportCQMData);
            this.pnlToolstrip.Controls.Add(this.chkShowJson);
            this.pnlToolstrip.Controls.Add(this.tls_Main);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(663, 59);
            this.pnlToolstrip.TabIndex = 27;
            // 
            // chkImportCQMData
            // 
            this.chkImportCQMData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkImportCQMData.AutoSize = true;
            this.chkImportCQMData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(217)))), ((int)(((byte)(244)))));
            this.chkImportCQMData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkImportCQMData.Location = new System.Drawing.Point(573, 37);
            this.chkImportCQMData.Name = "chkImportCQMData";
            this.chkImportCQMData.Size = new System.Drawing.Size(80, 18);
            this.chkImportCQMData.TabIndex = 77;
            this.chkImportCQMData.Text = "CQM Data";
            this.chkImportCQMData.UseVisualStyleBackColor = false;
            this.chkImportCQMData.CheckedChanged += new System.EventHandler(this.cmbImportCQMData_CheckedChanged);
            // 
            // chkShowJson
            // 
            this.chkShowJson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowJson.AutoSize = true;
            this.chkShowJson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(217)))), ((int)(((byte)(244)))));
            this.chkShowJson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkShowJson.Location = new System.Drawing.Point(553, 16);
            this.chkShowJson.Name = "chkShowJson";
            this.chkShowJson.Size = new System.Drawing.Size(100, 18);
            this.chkShowJson.TabIndex = 76;
            this.chkShowJson.Text = "Compare Files";
            this.chkShowJson.UseVisualStyleBackColor = false;
            this.chkShowJson.CheckedChanged += new System.EventHandler(this.chkShowJson_CheckedChanged);
            // 
            // tls_Main
            // 
            this.tls_Main.AutoSize = false;
            this.tls_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Main.BackgroundImage")));
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdImport,
            this.btnCompareQRDA,
            this.btnjson,
            this.tsb_ImportCQM,
            this.btnExportProvider,
            this.tlbbtn_Close});
            this.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(663, 56);
            this.tls_Main.TabIndex = 3;
            this.tls_Main.Text = "ToolStrip1";
            // 
            // cmdImport
            // 
            this.cmdImport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdImport.Image = ((System.Drawing.Image)(resources.GetObject("cmdImport.Image")));
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(54, 53);
            this.cmdImport.Tag = "Import";
            this.cmdImport.Text = "&Import";
            this.cmdImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdImport.ToolTipText = "Import";
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // btnCompareQRDA
            // 
            this.btnCompareQRDA.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompareQRDA.Image = ((System.Drawing.Image)(resources.GetObject("btnCompareQRDA.Image")));
            this.btnCompareQRDA.Name = "btnCompareQRDA";
            this.btnCompareQRDA.Size = new System.Drawing.Size(105, 53);
            this.btnCompareQRDA.Tag = "Compare QRDA";
            this.btnCompareQRDA.Text = "Compare &QRDA";
            this.btnCompareQRDA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCompareQRDA.ToolTipText = "Compare QRDA";
            this.btnCompareQRDA.Visible = false;
            this.btnCompareQRDA.Click += new System.EventHandler(this.btnCompareQRDA_Click);
            // 
            // btnjson
            // 
            this.btnjson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnjson.Image = ((System.Drawing.Image)(resources.GetObject("btnjson.Image")));
            this.btnjson.Name = "btnjson";
            this.btnjson.Size = new System.Drawing.Size(89, 53);
            this.btnjson.Tag = "Import JSON";
            this.btnjson.Text = "&Import JSON";
            this.btnjson.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnjson.ToolTipText = "Import JSON";
            this.btnjson.Visible = false;
            this.btnjson.Click += new System.EventHandler(this.btnjson_Click);
            // 
            // tsb_ImportCQM
            // 
            this.tsb_ImportCQM.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ImportCQM.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ImportCQM.Image")));
            this.tsb_ImportCQM.Name = "tsb_ImportCQM";
            this.tsb_ImportCQM.Size = new System.Drawing.Size(82, 53);
            this.tsb_ImportCQM.Tag = "ImportCQM";
            this.tsb_ImportCQM.Text = "&ImportCQM";
            this.tsb_ImportCQM.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ImportCQM.ToolTipText = "ImportCQM";
            this.tsb_ImportCQM.Visible = false;
            this.tsb_ImportCQM.Click += new System.EventHandler(this.tsb_ImportCQM_Click);
            // 
            // btnExportProvider
            // 
            this.btnExportProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnExportProvider.Image")));
            this.btnExportProvider.Name = "btnExportProvider";
            this.btnExportProvider.Size = new System.Drawing.Size(113, 53);
            this.btnExportProvider.Tag = "Export Provider";
            this.btnExportProvider.Text = "&Export Providers";
            this.btnExportProvider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportProvider.ToolTipText = "Export Provider";
            this.btnExportProvider.Visible = false;
            this.btnExportProvider.Click += new System.EventHandler(this.btnExportProvider_Click);
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 53);
            this.tlbbtn_Close.Tag = "Close";
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Close.Click += new System.EventHandler(this.tlbbtn_Close_Click);
            // 
            // pnlflexgrid
            // 
            this.pnlflexgrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlflexgrid.Controls.Add(this.lblGridBottom);
            this.pnlflexgrid.Controls.Add(this.gbDatabase);
            this.pnlflexgrid.Controls.Add(this.lblGridLeft);
            this.pnlflexgrid.Controls.Add(this.chkregprov);
            this.pnlflexgrid.Controls.Add(this.lblGridRight);
            this.pnlflexgrid.Controls.Add(this.lblGridTop);
            this.pnlflexgrid.Controls.Add(this.chkGetPathFromClipboard);
            this.pnlflexgrid.Controls.Add(this.lblProcessingfilename);
            this.pnlflexgrid.Controls.Add(this.label1);
            this.pnlflexgrid.Controls.Add(this.lblTotalQRDAfiles);
            this.pnlflexgrid.Controls.Add(this.label2);
            this.pnlflexgrid.Controls.Add(this.lblProcessStatus);
            this.pnlflexgrid.Controls.Add(this.txtPath);
            this.pnlflexgrid.Controls.Add(this.progressBar1);
            this.pnlflexgrid.Controls.Add(this.cmdBrowse);
            this.pnlflexgrid.Controls.Add(this.cmbProviders);
            this.pnlflexgrid.Controls.Add(this.label3);
            this.pnlflexgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlflexgrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlflexgrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlflexgrid.Location = new System.Drawing.Point(0, 59);
            this.pnlflexgrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlflexgrid.Name = "pnlflexgrid";
            this.pnlflexgrid.Padding = new System.Windows.Forms.Padding(3);
            this.pnlflexgrid.Size = new System.Drawing.Size(663, 266);
            this.pnlflexgrid.TabIndex = 28;
            // 
            // lblGridBottom
            // 
            this.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGridBottom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGridBottom.Location = new System.Drawing.Point(4, 262);
            this.lblGridBottom.Name = "lblGridBottom";
            this.lblGridBottom.Size = new System.Drawing.Size(655, 1);
            this.lblGridBottom.TabIndex = 10;
            this.lblGridBottom.Text = "label2";
            // 
            // lblGridLeft
            // 
            this.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGridLeft.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridLeft.Location = new System.Drawing.Point(3, 4);
            this.lblGridLeft.Name = "lblGridLeft";
            this.lblGridLeft.Size = new System.Drawing.Size(1, 259);
            this.lblGridLeft.TabIndex = 9;
            this.lblGridLeft.Text = "label4";
            // 
            // lblGridRight
            // 
            this.lblGridRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGridRight.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGridRight.Location = new System.Drawing.Point(659, 4);
            this.lblGridRight.Name = "lblGridRight";
            this.lblGridRight.Size = new System.Drawing.Size(1, 259);
            this.lblGridRight.TabIndex = 8;
            this.lblGridRight.Text = "label3";
            // 
            // lblGridTop
            // 
            this.lblGridTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGridTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTop.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGridTop.Location = new System.Drawing.Point(3, 3);
            this.lblGridTop.Name = "lblGridTop";
            this.lblGridTop.Size = new System.Drawing.Size(657, 1);
            this.lblGridTop.TabIndex = 7;
            this.lblGridTop.Text = "label1";
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.BackgroundImage")));
            this.cmdBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdBrowse.ForeColor = System.Drawing.Color.Black;
            this.cmdBrowse.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.Image")));
            this.cmdBrowse.Location = new System.Drawing.Point(611, 81);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(24, 24);
            this.cmdBrowse.TabIndex = 16;
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            this.cmdBrowse.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            this.cmdBrowse.MouseHover += new System.EventHandler(this.btnMouseHover);
            // 
            // frmQRDAImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(663, 325);
            this.Controls.Add(this.pnlflexgrid);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmQRDAImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QRDA Import Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbDatabase.ResumeLayout(false);
            this.gbDatabase.PerformLayout();
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.pnlflexgrid.ResumeLayout(false);
            this.pnlflexgrid.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbDatabase;
        private System.Windows.Forms.CheckBox chkSQLAuthentication;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Label lblPassword;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.Label lblUser;
        internal System.Windows.Forms.Label bllDatabase;
        internal System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Button btnConnect;
        internal System.Windows.Forms.Label blbServer;
        internal System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.ComboBox cmbProviders;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lblTotalQRDAfiles;
        internal System.Windows.Forms.Label lblProcessStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        internal System.Windows.Forms.Label lblProcessingfilename;
        private System.Windows.Forms.CheckBox chkGetPathFromClipboard;
        private System.Windows.Forms.CheckBox chkregprov;
        internal System.Windows.Forms.Panel pnlToolstrip;
        internal gloGlobal.gloToolStripIgnoreFocus tls_Main;
        internal System.Windows.Forms.ToolStripButton cmdImport;
        internal System.Windows.Forms.ToolStripButton btnCompareQRDA;
        internal System.Windows.Forms.ToolStripButton btnjson;
        internal System.Windows.Forms.Panel pnlflexgrid;
        private System.Windows.Forms.Label lblGridBottom;
        private System.Windows.Forms.Label lblGridLeft;
        private System.Windows.Forms.Label lblGridRight;
        private System.Windows.Forms.Label lblGridTop;
        private System.Windows.Forms.CheckBox chkShowJson;
        private System.Windows.Forms.CheckBox chkImportCQMData;
        internal System.Windows.Forms.ToolStripButton tsb_ImportCQM;
        internal System.Windows.Forms.ToolStripButton btnExportProvider;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;

    }
}

