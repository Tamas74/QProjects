namespace gloPM
{
    partial class frmSplash
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.pnlDataBase = new System.Windows.Forms.Panel();
            this.cmbDatabaseName = new System.Windows.Forms.ComboBox();
            this.lblDataBase = new System.Windows.Forms.Label();
            this.pnlLoginButton = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlPMS = new System.Windows.Forms.Panel();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCopyrghTag = new System.Windows.Forms.Label();
            this.lblLastModifiedTime = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblLastModifiedDate = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblSetup = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tmr_ShowSetting = new System.Windows.Forms.Timer(this.components);
            this.tmr_AUSUpdates = new System.Windows.Forms.Timer(this.components);
            this.cachedrptSummaryOfVisit1 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit2 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit3 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit4 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit5 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit6 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit7 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit8 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit9 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit10 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit11 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.pnlLogin.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.pnlDataBase.SuspendLayout();
            this.pnlLoginButton.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlPMS.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.pnlUser);
            this.pnlLogin.Controls.Add(this.pnlDataBase);
            this.pnlLogin.Controls.Add(this.pnlLoginButton);
            this.pnlLogin.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlLogin.Location = new System.Drawing.Point(566, 22);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(221, 133);
            this.pnlLogin.TabIndex = 11;
            // 
            // pnlUser
            // 
            this.pnlUser.BackColor = System.Drawing.Color.Transparent;
            this.pnlUser.Controls.Add(this.txtPassword);
            this.pnlUser.Controls.Add(this.Label3);
            this.pnlUser.Controls.Add(this.txtUserName);
            this.pnlUser.Controls.Add(this.Label4);
            this.pnlUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUser.Location = new System.Drawing.Point(0, 0);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(221, 68);
            this.pnlUser.TabIndex = 20;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(72, 39);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(141, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(77)))));
            this.Label3.Location = new System.Drawing.Point(-1, 43);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(72, 12);
            this.Label3.TabIndex = 10;
            this.Label3.Text = "PASSWORD:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(72, 8);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(141, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(77)))));
            this.Label4.Location = new System.Drawing.Point(0, 12);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(70, 12);
            this.Label4.TabIndex = 9;
            this.Label4.Text = "USERNAME:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlDataBase
            // 
            this.pnlDataBase.BackColor = System.Drawing.Color.Transparent;
            this.pnlDataBase.Controls.Add(this.cmbDatabaseName);
            this.pnlDataBase.Controls.Add(this.lblDataBase);
            this.pnlDataBase.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDataBase.Location = new System.Drawing.Point(0, 68);
            this.pnlDataBase.Name = "pnlDataBase";
            this.pnlDataBase.Size = new System.Drawing.Size(221, 30);
            this.pnlDataBase.TabIndex = 21;
            // 
            // cmbDatabaseName
            // 
            this.cmbDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabaseName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDatabaseName.FormattingEnabled = true;
            this.cmbDatabaseName.IntegralHeight = false;
            this.cmbDatabaseName.ItemHeight = 14;
            this.cmbDatabaseName.Location = new System.Drawing.Point(72, 2);
            this.cmbDatabaseName.Name = "cmbDatabaseName";
            this.cmbDatabaseName.Size = new System.Drawing.Size(141, 22);
            this.cmbDatabaseName.TabIndex = 2;
            // 
            // lblDataBase
            // 
            this.lblDataBase.AutoSize = true;
            this.lblDataBase.BackColor = System.Drawing.Color.Transparent;
            this.lblDataBase.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(77)))));
            this.lblDataBase.Location = new System.Drawing.Point(1, 8);
            this.lblDataBase.Name = "lblDataBase";
            this.lblDataBase.Size = new System.Drawing.Size(69, 12);
            this.lblDataBase.TabIndex = 14;
            this.lblDataBase.Text = "DATABASE:";
            this.lblDataBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlLoginButton
            // 
            this.pnlLoginButton.BackColor = System.Drawing.Color.Transparent;
            this.pnlLoginButton.Controls.Add(this.btnCancel);
            this.pnlLoginButton.Controls.Add(this.btnLogin);
            this.pnlLoginButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLoginButton.Location = new System.Drawing.Point(0, 98);
            this.pnlLoginButton.Name = "pnlLoginButton";
            this.pnlLoginButton.Size = new System.Drawing.Size(221, 35);
            this.pnlLoginButton.TabIndex = 22;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(170)))), ((int)(((byte)(239)))));
            this.btnCancel.BackgroundImage = global::gloPM.Properties.Resources.Img_BlueBtnSplashScreen;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(170)))), ((int)(((byte)(239)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(170)))), ((int)(((byte)(239)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(170)))), ((int)(((byte)(239)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(113, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            this.btnCancel.MouseHover += new System.EventHandler(this.btnCancel_MouseHover);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(203)))), ((int)(((byte)(49)))));
            this.btnLogin.BackgroundImage = global::gloPM.Properties.Resources.Img_OrangeBtnSplashScreen;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(203)))), ((int)(((byte)(49)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(203)))), ((int)(((byte)(49)))));
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(203)))), ((int)(((byte)(49)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(7, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "&Login";
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseLeave += new System.EventHandler(this.btnLogin_MouseLeave);
            this.btnLogin.MouseHover += new System.EventHandler(this.btnLogin_MouseHover);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 7.95F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(77)))));
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.Location = new System.Drawing.Point(611, 463);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 22);
            this.label5.TabIndex = 156;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlPMS);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(837, 605);
            this.pnlMain.TabIndex = 10;
            // 
            // pnlPMS
            // 
            this.pnlPMS.BackColor = System.Drawing.Color.Transparent;
            this.pnlPMS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPMS.Controls.Add(this.label5);
            this.pnlPMS.Controls.Add(this.lblPleaseWait);
            this.pnlPMS.Controls.Add(this.panel1);
            this.pnlPMS.Controls.Add(this.Panel2);
            this.pnlPMS.Controls.Add(this.lblLastModifiedTime);
            this.pnlPMS.Controls.Add(this.lblVersion);
            this.pnlPMS.Controls.Add(this.lblLastModifiedDate);
            this.pnlPMS.Controls.Add(this.pictureBox1);
            this.pnlPMS.Controls.Add(this.lblSetup);
            this.pnlPMS.Controls.Add(this.lblApplicationDate);
            this.pnlPMS.Controls.Add(this.pnlLogin);
            this.pnlPMS.Controls.Add(this.Label9);
            this.pnlPMS.Controls.Add(this.menuStrip1);
            this.pnlPMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPMS.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPMS.Location = new System.Drawing.Point(0, 0);
            this.pnlPMS.Name = "pnlPMS";
            this.pnlPMS.Size = new System.Drawing.Size(837, 605);
            this.pnlPMS.TabIndex = 1;
            this.pnlPMS.Visible = false;
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.BackColor = System.Drawing.Color.Transparent;
            this.lblPleaseWait.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(77)))));
            this.lblPleaseWait.Location = new System.Drawing.Point(611, 151);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(135, 14);
            this.lblPleaseWait.TabIndex = 29;
            this.lblPleaseWait.Text = "Loading. . .  Please Wait";
            this.lblPleaseWait.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(415, 406);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(18, 11);
            this.panel1.TabIndex = 20;
            this.panel1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 5F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(114)))), ((int)(((byte)(175)))));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 7);
            this.label2.TabIndex = 18;
            this.label2.Text = "TM";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.Controls.Add(this.label1);
            this.Panel2.Controls.Add(this.lblCopyrghTag);
            this.Panel2.Location = new System.Drawing.Point(24, 520);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(800, 75);
            this.Panel2.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial", 8.05F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(221)))), ((int)(((byte)(227)))));
            this.label1.Location = new System.Drawing.Point(0, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 72);
            this.label1.TabIndex = 9;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // lblCopyrghTag
            // 
            this.lblCopyrghTag.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCopyrghTag.Font = new System.Drawing.Font("Arial", 8.05F);
            this.lblCopyrghTag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(221)))), ((int)(((byte)(227)))));
            this.lblCopyrghTag.Location = new System.Drawing.Point(0, 0);
            this.lblCopyrghTag.Name = "lblCopyrghTag";
            this.lblCopyrghTag.Size = new System.Drawing.Size(800, 15);
            this.lblCopyrghTag.TabIndex = 8;
            this.lblCopyrghTag.Text = "CPT® copyright 2015 American Medical Association. All rights reserved.";
            // 
            // lblLastModifiedTime
            // 
            this.lblLastModifiedTime.AutoSize = true;
            this.lblLastModifiedTime.BackColor = System.Drawing.Color.Transparent;
            this.lblLastModifiedTime.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastModifiedTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(114)))), ((int)(((byte)(175)))));
            this.lblLastModifiedTime.Location = new System.Drawing.Point(353, 48);
            this.lblLastModifiedTime.Name = "lblLastModifiedTime";
            this.lblLastModifiedTime.Size = new System.Drawing.Size(0, 14);
            this.lblLastModifiedTime.TabIndex = 19;
            this.lblLastModifiedTime.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 21.05F, System.Drawing.FontStyle.Bold);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(114)))), ((int)(((byte)(175)))));
            this.lblVersion.Location = new System.Drawing.Point(430, 408);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 34);
            this.lblVersion.TabIndex = 18;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVersion.Visible = false;
            // 
            // lblLastModifiedDate
            // 
            this.lblLastModifiedDate.AutoSize = true;
            this.lblLastModifiedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblLastModifiedDate.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblLastModifiedDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(221)))), ((int)(((byte)(227)))));
            this.lblLastModifiedDate.Location = new System.Drawing.Point(23, 502);
            this.lblLastModifiedDate.Name = "lblLastModifiedDate";
            this.lblLastModifiedDate.Size = new System.Drawing.Size(70, 14);
            this.lblLastModifiedDate.TabIndex = 17;
            this.lblLastModifiedDate.Text = "Mar 05, 2016";
            this.lblLastModifiedDate.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(525, 644);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // lblSetup
            // 
            this.lblSetup.AutoSize = true;
            this.lblSetup.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSetup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(77)))));
            this.lblSetup.Location = new System.Drawing.Point(604, 5);
            this.lblSetup.Name = "lblSetup";
            this.lblSetup.Size = new System.Drawing.Size(142, 14);
            this.lblSetup.TabIndex = 14;
            this.lblSetup.Text = "Press F11 to enter setup";
            this.lblSetup.Visible = false;
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblApplicationDate.ForeColor = System.Drawing.Color.White;
            this.lblApplicationDate.Location = new System.Drawing.Point(0, 591);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(10, 14);
            this.lblApplicationDate.TabIndex = 12;
            this.lblApplicationDate.Text = ".";
            this.lblApplicationDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblApplicationDate.Visible = false;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Font = new System.Drawing.Font("Arial", 7.95F, System.Drawing.FontStyle.Bold);
            this.Label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(77)))));
            this.Label9.Image = ((System.Drawing.Image)(resources.GetObject("Label9.Image")));
            this.Label9.Location = new System.Drawing.Point(626, 169);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(101, 40);
            this.Label9.TabIndex = 157;
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::gloPM.Properties.Resources.Img_Toolstrip;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(648, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetting});
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(22, 20);
            this.dToolStripMenuItem.Text = " ";
            // 
            // mnuSetting
            // 
            this.mnuSetting.Name = "mnuSetting";
            this.mnuSetting.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.mnuSetting.Size = new System.Drawing.Size(136, 22);
            this.mnuSetting.Text = "Setting";
            this.mnuSetting.Click += new System.EventHandler(this.mnuSetting_Click);
            // 
            // tmr_ShowSetting
            // 
            this.tmr_ShowSetting.Interval = 2000;
            this.tmr_ShowSetting.Tick += new System.EventHandler(this.tmr_ShowSetting_Tick);
            // 
            // tmr_AUSUpdates
            // 
            this.tmr_AUSUpdates.Interval = 50000;
            this.tmr_AUSUpdates.Tick += new System.EventHandler(this.tmr_AUSUpdates_Tick);
            // 
            // frmSplash
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(837, 605);
            this.Controls.Add(this.pnlMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gloPMS..People...Places...Things";
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlDataBase.ResumeLayout(false);
            this.pnlDataBase.PerformLayout();
            this.pnlLoginButton.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlPMS.ResumeLayout(false);
            this.pnlPMS.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.Button btnLogin;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtUserName;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlLogin;
        internal System.Windows.Forms.Panel pnlPMS;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Timer tmr_ShowSetting;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting;
        private System.Windows.Forms.Label lblSetup;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lblCopyrghTag;
        internal System.Windows.Forms.Label lblVersion;
        internal System.Windows.Forms.Label lblLastModifiedDate;
        internal System.Windows.Forms.Label lblLastModifiedTime;
        private System.Windows.Forms.Timer tmr_AUSUpdates;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit1;
        internal System.Windows.Forms.ComboBox cmbDatabaseName;
        internal System.Windows.Forms.Label lblDataBase;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit2;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit3;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit4;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit5;
        private System.Windows.Forms.Panel pnlDataBase;
        private System.Windows.Forms.Panel pnlLoginButton;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label2;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit6;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit7;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit8;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit9;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit10;
        internal System.Windows.Forms.Label lblPleaseWait;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label Label9;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit11;
    }
}