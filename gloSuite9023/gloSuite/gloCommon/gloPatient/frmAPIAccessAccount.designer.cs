namespace gloPatient
{
    partial class frmAPIAccessAccount
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { mskDOInfoProvided };
            System.Windows.Forms.Control[] cntControls = { mskDOInfoProvided };
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
                    if (oPatientPortalAccount != null)
                    {
                        oPatientPortalAccount.Dispose();
                        oPatientPortalAccount = null;
                    }
                }
                catch
                {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAPIAccessAccount));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnSendInvitation = new System.Windows.Forms.ToolStripButton();
            this.tls_btnQuickActivate = new System.Windows.Forms.ToolStripButton();
            this.tls_btnActivatePatient = new System.Windows.Forms.ToolStripButton();
            this.tls_btnResetTempPassword = new System.Windows.Forms.ToolStripButton();
            this.tls_btnAdd = new System.Windows.Forms.ToolStripButton();
            this.tls_btnRemove = new System.Windows.Forms.ToolStripButton();
            this.tls_btnBrowse = new System.Windows.Forms.ToolStripButton();
            this.tls_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlNotes = new System.Windows.Forms.Panel();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlPortalLoginCredentials = new System.Windows.Forms.Panel();
            this.btnSendLoginCredentialsEmail = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblPortalLoginCredentials = new System.Windows.Forms.Label();
            this.btnPrintLoginCredentials = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPortalUserName = new System.Windows.Forms.Label();
            this.mskDOInfoProvided = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBlockReason1 = new System.Windows.Forms.TextBox();
            this.lblBlockDate = new System.Windows.Forms.Label();
            this.lblBlockDate1 = new System.Windows.Forms.Label();
            this.lblBlockReason1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlGIHeader = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDateOfInvitation = new System.Windows.Forms.Label();
            this.lblDateOfLastLogin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPortalAccountStatus = new System.Windows.Forms.Label();
            this.lblDateOfActivation = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlPatientRepresentative = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbPatientRepresentative = new System.Windows.Forms.ComboBox();
            this.btnPatientRepresentativeBrowse = new System.Windows.Forms.Button();
            this.pnlValidation = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnblockcancel = new System.Windows.Forms.Button();
            this.lblGIHeader = new System.Windows.Forms.Label();
            this.pnlUserName = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnCancelUserName = new System.Windows.Forms.Button();
            this.btnProceedUserName = new System.Windows.Forms.Button();
            this.pnlPatientBlock = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBlockReason = new System.Windows.Forms.TextBox();
            this.btnBlockProceed = new System.Windows.Forms.Button();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.pnlNotes.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlPortalLoginCredentials.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlGIHeader.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlValidation.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlUserName.SuspendLayout();
            this.pnlPatientBlock.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(771, 54);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnSendInvitation,
            this.tls_btnQuickActivate,
            this.tls_btnActivatePatient,
            this.tls_btnResetTempPassword,
            this.tls_btnAdd,
            this.tls_btnRemove,
            this.tls_btnBrowse,
            this.tls_btnSave,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(771, 53);
            this.tls_Top.TabIndex = 1;
            this.tls_Top.Text = "toolStrip1";
            this.tls_Top.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_Top_ItemClicked);
            // 
            // tls_btnSendInvitation
            // 
            this.tls_btnSendInvitation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSendInvitation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSendInvitation.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSendInvitation.Image")));
            this.tls_btnSendInvitation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSendInvitation.Name = "tls_btnSendInvitation";
            this.tls_btnSendInvitation.Size = new System.Drawing.Size(107, 50);
            this.tls_btnSendInvitation.Tag = "";
            this.tls_btnSendInvitation.Text = "Send Invitation";
            this.tls_btnSendInvitation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSendInvitation.Click += new System.EventHandler(this.tls_btnSendInvitation_Click);
            // 
            // tls_btnQuickActivate
            // 
            this.tls_btnQuickActivate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnQuickActivate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnQuickActivate.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnQuickActivate.Image")));
            this.tls_btnQuickActivate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnQuickActivate.Name = "tls_btnQuickActivate";
            this.tls_btnQuickActivate.Size = new System.Drawing.Size(62, 50);
            this.tls_btnQuickActivate.Tag = "";
            this.tls_btnQuickActivate.Text = "Activate";
            this.tls_btnQuickActivate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnQuickActivate.Click += new System.EventHandler(this.tls_btnQuickActivate_Click);
            // 
            // tls_btnActivatePatient
            // 
            this.tls_btnActivatePatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnActivatePatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnActivatePatient.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnActivatePatient.Image")));
            this.tls_btnActivatePatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnActivatePatient.Name = "tls_btnActivatePatient";
            this.tls_btnActivatePatient.Size = new System.Drawing.Size(88, 50);
            this.tls_btnActivatePatient.Tag = "";
            this.tls_btnActivatePatient.Text = "&Activate API";
            this.tls_btnActivatePatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnActivatePatient.Click += new System.EventHandler(this.tls_btnActivatePatient_Click);
            // 
            // tls_btnResetTempPassword
            // 
            this.tls_btnResetTempPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnResetTempPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnResetTempPassword.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnResetTempPassword.Image")));
            this.tls_btnResetTempPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnResetTempPassword.Name = "tls_btnResetTempPassword";
            this.tls_btnResetTempPassword.Size = new System.Drawing.Size(109, 50);
            this.tls_btnResetTempPassword.Tag = "";
            this.tls_btnResetTempPassword.Text = "Reset &Password";
            this.tls_btnResetTempPassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnResetTempPassword.Click += new System.EventHandler(this.tls_btnResetTempPassword_Click);
            // 
            // tls_btnAdd
            // 
            this.tls_btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnAdd.Image")));
            this.tls_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnAdd.Name = "tls_btnAdd";
            this.tls_btnAdd.Size = new System.Drawing.Size(36, 50);
            this.tls_btnAdd.Tag = "Add";
            this.tls_btnAdd.Text = "A&dd";
            this.tls_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnAdd.ToolTipText = "Add";
            // 
            // tls_btnRemove
            // 
            this.tls_btnRemove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnRemove.Image")));
            this.tls_btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnRemove.Name = "tls_btnRemove";
            this.tls_btnRemove.Size = new System.Drawing.Size(60, 50);
            this.tls_btnRemove.Tag = "Remove";
            this.tls_btnRemove.Text = "&Remove";
            this.tls_btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tls_btnBrowse
            // 
            this.tls_btnBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnBrowse.Image")));
            this.tls_btnBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnBrowse.Name = "tls_btnBrowse";
            this.tls_btnBrowse.Size = new System.Drawing.Size(56, 50);
            this.tls_btnBrowse.Tag = "Browse";
            this.tls_btnBrowse.Text = "&Browse";
            this.tls_btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnBrowse.Click += new System.EventHandler(this.tls_btnBrowse_Click);
            // 
            // tls_btnSave
            // 
            this.tls_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSave.Image")));
            this.tls_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSave.Name = "tls_btnSave";
            this.tls_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tls_btnSave.Tag = "Save";
            this.tls_btnSave.Text = "&Save";
            this.tls_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSave.ToolTipText = "Save";
            this.tls_btnSave.Click += new System.EventHandler(this.tls_btnSave_Click);
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
            // pnlNotes
            // 
            this.pnlNotes.Controls.Add(this.pnl_Base);
            this.pnlNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNotes.Location = new System.Drawing.Point(0, 54);
            this.pnlNotes.Name = "pnlNotes";
            this.pnlNotes.Size = new System.Drawing.Size(771, 556);
            this.pnlNotes.TabIndex = 0;
            // 
            // pnl_Base
            // 
            this.pnl_Base.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Base.Controls.Add(this.tabControl1);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Base.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_Base.Location = new System.Drawing.Point(0, 0);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Size = new System.Drawing.Size(771, 556);
            this.pnl_Base.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(6, 4);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(771, 556);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage1.Controls.Add(this.pnlPortalLoginCredentials);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(763, 527);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "API Accounts";
            // 
            // pnlPortalLoginCredentials
            // 
            this.pnlPortalLoginCredentials.Controls.Add(this.btnSendLoginCredentialsEmail);
            this.pnlPortalLoginCredentials.Controls.Add(this.panel2);
            this.pnlPortalLoginCredentials.Controls.Add(this.btnPrintLoginCredentials);
            this.pnlPortalLoginCredentials.Controls.Add(this.label15);
            this.pnlPortalLoginCredentials.Controls.Add(this.lblPassword);
            this.pnlPortalLoginCredentials.Controls.Add(this.label17);
            this.pnlPortalLoginCredentials.Controls.Add(this.lblUserName);
            this.pnlPortalLoginCredentials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPortalLoginCredentials.Location = new System.Drawing.Point(4, 203);
            this.pnlPortalLoginCredentials.Name = "pnlPortalLoginCredentials";
            this.pnlPortalLoginCredentials.Size = new System.Drawing.Size(755, 320);
            this.pnlPortalLoginCredentials.TabIndex = 1022;
            this.pnlPortalLoginCredentials.Visible = false;
            // 
            // btnSendLoginCredentialsEmail
            // 
            this.btnSendLoginCredentialsEmail.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnSendLoginCredentialsEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSendLoginCredentialsEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendLoginCredentialsEmail.Location = new System.Drawing.Point(343, 93);
            this.btnSendLoginCredentialsEmail.Name = "btnSendLoginCredentialsEmail";
            this.btnSendLoginCredentialsEmail.Size = new System.Drawing.Size(84, 23);
            this.btnSendLoginCredentialsEmail.TabIndex = 1028;
            this.btnSendLoginCredentialsEmail.Text = "Send Email";
            this.btnSendLoginCredentialsEmail.UseVisualStyleBackColor = true;
            this.btnSendLoginCredentialsEmail.Visible = false;
            this.btnSendLoginCredentialsEmail.Click += new System.EventHandler(this.btnSendLoginCredentialsEmail_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.lblPortalLoginCredentials);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(755, 25);
            this.panel2.TabIndex = 1027;
            // 
            // label27
            // 
            this.label27.AutoEllipsis = true;
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(0, 24);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(755, 1);
            this.label27.TabIndex = 1026;
            // 
            // label26
            // 
            this.label26.AutoEllipsis = true;
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(755, 1);
            this.label26.TabIndex = 1025;
            // 
            // lblPortalLoginCredentials
            // 
            this.lblPortalLoginCredentials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPortalLoginCredentials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPortalLoginCredentials.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortalLoginCredentials.ForeColor = System.Drawing.Color.White;
            this.lblPortalLoginCredentials.Location = new System.Drawing.Point(0, 0);
            this.lblPortalLoginCredentials.Name = "lblPortalLoginCredentials";
            this.lblPortalLoginCredentials.Size = new System.Drawing.Size(755, 25);
            this.lblPortalLoginCredentials.TabIndex = 1021;
            this.lblPortalLoginCredentials.Text = "   Login Credentials";
            this.lblPortalLoginCredentials.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPortalLoginCredentials.Visible = false;
            // 
            // btnPrintLoginCredentials
            // 
            this.btnPrintLoginCredentials.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnPrintLoginCredentials.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintLoginCredentials.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintLoginCredentials.Location = new System.Drawing.Point(253, 93);
            this.btnPrintLoginCredentials.Name = "btnPrintLoginCredentials";
            this.btnPrintLoginCredentials.Size = new System.Drawing.Size(84, 23);
            this.btnPrintLoginCredentials.TabIndex = 1021;
            this.btnPrintLoginCredentials.Text = "Print";
            this.btnPrintLoginCredentials.UseVisualStyleBackColor = true;
            this.btnPrintLoginCredentials.Visible = false;
            this.btnPrintLoginCredentials.Click += new System.EventHandler(this.btnPrintLoginCredentials_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoEllipsis = true;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(177, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 14);
            this.label15.TabIndex = 1017;
            this.label15.Text = "User Name :";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(253, 65);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(66, 14);
            this.lblPassword.TabIndex = 1020;
            this.lblPassword.Text = "Password";
            this.lblPassword.UseMnemonic = false;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoEllipsis = true;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(94, 64);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(157, 14);
            this.label17.TabIndex = 1019;
            this.label17.Text = "Password (Case Sensitive) :";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(253, 37);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(66, 14);
            this.lblUserName.TabIndex = 1018;
            this.lblUserName.Text = "Username";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblPortalUserName);
            this.panel1.Controls.Add(this.mskDOInfoProvided);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtBlockReason1);
            this.panel1.Controls.Add(this.lblBlockDate);
            this.panel1.Controls.Add(this.lblBlockDate1);
            this.panel1.Controls.Add(this.lblBlockReason1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pnlGIHeader);
            this.panel1.Controls.Add(this.lblDateOfInvitation);
            this.panel1.Controls.Add(this.lblDateOfLastLogin);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblPortalAccountStatus);
            this.panel1.Controls.Add(this.lblDateOfActivation);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 199);
            this.panel1.TabIndex = 1021;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(145, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 14);
            this.label8.TabIndex = 1042;
            this.label8.Text = "API Username :";
            // 
            // lblPortalUserName
            // 
            this.lblPortalUserName.AutoSize = true;
            this.lblPortalUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPortalUserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortalUserName.Location = new System.Drawing.Point(254, 45);
            this.lblPortalUserName.Name = "lblPortalUserName";
            this.lblPortalUserName.Size = new System.Drawing.Size(44, 14);
            this.lblPortalUserName.TabIndex = 1043;
            this.lblPortalUserName.Text = "API";
            // 
            // mskDOInfoProvided
            // 
            this.mskDOInfoProvided.CalendarForeColor = System.Drawing.Color.Maroon;
            this.mskDOInfoProvided.CalendarMonthBackground = System.Drawing.Color.White;
            this.mskDOInfoProvided.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.mskDOInfoProvided.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.mskDOInfoProvided.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.mskDOInfoProvided.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.mskDOInfoProvided.Location = new System.Drawing.Point(254, 150);
            this.mskDOInfoProvided.Name = "mskDOInfoProvided";
            this.mskDOInfoProvided.ShowCheckBox = true;
            this.mskDOInfoProvided.Size = new System.Drawing.Size(125, 22);
            this.mskDOInfoProvided.TabIndex = 1041;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(238, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 14);
            this.label12.TabIndex = 1040;
            this.label12.Text = ":";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoEllipsis = true;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(239, 14);
            this.label9.TabIndex = 1019;
            this.label9.Text = "Patient has no email address but “access\'\' \r\n";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBlockReason1
            // 
            this.txtBlockReason1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.txtBlockReason1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBlockReason1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtBlockReason1.Location = new System.Drawing.Point(454, 68);
            this.txtBlockReason1.Multiline = true;
            this.txtBlockReason1.Name = "txtBlockReason1";
            this.txtBlockReason1.ReadOnly = true;
            this.txtBlockReason1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBlockReason1.Size = new System.Drawing.Size(293, 45);
            this.txtBlockReason1.TabIndex = 1035;
            // 
            // lblBlockDate
            // 
            this.lblBlockDate.AutoSize = true;
            this.lblBlockDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBlockDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlockDate.Location = new System.Drawing.Point(747, 82);
            this.lblBlockDate.Name = "lblBlockDate";
            this.lblBlockDate.Size = new System.Drawing.Size(0, 14);
            this.lblBlockDate.TabIndex = 1034;
            this.lblBlockDate.Visible = false;
            // 
            // lblBlockDate1
            // 
            this.lblBlockDate1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBlockDate1.AutoEllipsis = true;
            this.lblBlockDate1.AutoSize = true;
            this.lblBlockDate1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlockDate1.Location = new System.Drawing.Point(644, 72);
            this.lblBlockDate1.Name = "lblBlockDate1";
            this.lblBlockDate1.Size = new System.Drawing.Size(73, 14);
            this.lblBlockDate1.TabIndex = 1033;
            this.lblBlockDate1.Text = "Block Date :";
            this.lblBlockDate1.Visible = false;
            // 
            // lblBlockReason1
            // 
            this.lblBlockReason1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBlockReason1.AutoEllipsis = true;
            this.lblBlockReason1.AutoSize = true;
            this.lblBlockReason1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlockReason1.Location = new System.Drawing.Point(362, 68);
            this.lblBlockReason1.Name = "lblBlockReason1";
            this.lblBlockReason1.Size = new System.Drawing.Size(86, 14);
            this.lblBlockReason1.TabIndex = 1031;
            this.lblBlockReason1.Text = "Block Reason :";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(114, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 14);
            this.label1.TabIndex = 1013;
            this.label1.Text = "API Account Status :";
            // 
            // pnlGIHeader
            // 
            this.pnlGIHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlGIHeader.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlGIHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGIHeader.Controls.Add(this.label7);
            this.pnlGIHeader.Controls.Add(this.label11);
            this.pnlGIHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGIHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGIHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlGIHeader.Name = "pnlGIHeader";
            this.pnlGIHeader.Size = new System.Drawing.Size(755, 25);
            this.pnlGIHeader.TabIndex = 1030;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(755, 24);
            this.label7.TabIndex = 1022;
            this.label7.Text = "   Account Details";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(0, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(755, 1);
            this.label11.TabIndex = 8;
            this.label11.Text = "label2";
            // 
            // lblDateOfInvitation
            // 
            this.lblDateOfInvitation.AutoSize = true;
            this.lblDateOfInvitation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDateOfInvitation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfInvitation.Location = new System.Drawing.Point(661, 210);
            this.lblDateOfInvitation.Name = "lblDateOfInvitation";
            this.lblDateOfInvitation.Size = new System.Drawing.Size(85, 14);
            this.lblDateOfInvitation.TabIndex = 1016;
            this.lblDateOfInvitation.Text = "01/01/2000";
            this.lblDateOfInvitation.Visible = false;
            // 
            // lblDateOfLastLogin
            // 
            this.lblDateOfLastLogin.AutoSize = true;
            this.lblDateOfLastLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDateOfLastLogin.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfLastLogin.Location = new System.Drawing.Point(254, 117);
            this.lblDateOfLastLogin.Name = "lblDateOfLastLogin";
            this.lblDateOfLastLogin.Size = new System.Drawing.Size(85, 14);
            this.lblDateOfLastLogin.TabIndex = 1020;
            this.lblDateOfLastLogin.Text = "01/01/2000";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(547, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 14);
            this.label3.TabIndex = 1015;
            this.label3.Text = "Date of Invitation :";
            this.label3.Visible = false;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoEllipsis = true;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(134, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 14);
            this.label10.TabIndex = 1019;
            this.label10.Text = "Date of Last Login :";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(135, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 14);
            this.label5.TabIndex = 1017;
            this.label5.Text = "Date of Activation :";
            // 
            // lblPortalAccountStatus
            // 
            this.lblPortalAccountStatus.AutoSize = true;
            this.lblPortalAccountStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPortalAccountStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortalAccountStatus.Location = new System.Drawing.Point(254, 68);
            this.lblPortalAccountStatus.Name = "lblPortalAccountStatus";
            this.lblPortalAccountStatus.Size = new System.Drawing.Size(66, 14);
            this.lblPortalAccountStatus.TabIndex = 1014;
            this.lblPortalAccountStatus.Text = "Activated";
            // 
            // lblDateOfActivation
            // 
            this.lblDateOfActivation.AutoSize = true;
            this.lblDateOfActivation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDateOfActivation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOfActivation.Location = new System.Drawing.Point(254, 93);
            this.lblDateOfActivation.Name = "lblDateOfActivation";
            this.lblDateOfActivation.Size = new System.Drawing.Size(85, 14);
            this.lblDateOfActivation.TabIndex = 1018;
            this.lblDateOfActivation.Text = "01/01/2000";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoEllipsis = true;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(56, 163);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(180, 14);
            this.label14.TabIndex = 1019;
            this.label14.Text = "has been provided per MU rules";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.AutoEllipsis = true;
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(759, 4);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 519);
            this.label24.TabIndex = 1026;
            // 
            // label23
            // 
            this.label23.AutoEllipsis = true;
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(4, 523);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(756, 1);
            this.label23.TabIndex = 1025;
            // 
            // label20
            // 
            this.label20.AutoEllipsis = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Left;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 4);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 520);
            this.label20.TabIndex = 1024;
            // 
            // label22
            // 
            this.label22.AutoEllipsis = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(3, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(757, 1);
            this.label22.TabIndex = 1023;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Controls.Add(this.pnlPatientRepresentative);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label21);
            this.tabPage2.Controls.Add(this.cmbPatientRepresentative);
            this.tabPage2.Controls.Add(this.btnPatientRepresentativeBrowse);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(763, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Patient Representative";
            // 
            // label25
            // 
            this.label25.AutoEllipsis = true;
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(4, 3);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(755, 1);
            this.label25.TabIndex = 1017;
            // 
            // pnlPatientRepresentative
            // 
            this.pnlPatientRepresentative.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPatientRepresentative.Location = new System.Drawing.Point(4, 4);
            this.pnlPatientRepresentative.Name = "pnlPatientRepresentative";
            this.pnlPatientRepresentative.Size = new System.Drawing.Size(755, 519);
            this.pnlPatientRepresentative.TabIndex = 2;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(759, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 519);
            this.label19.TabIndex = 1016;
            // 
            // label18
            // 
            this.label18.AutoEllipsis = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 519);
            this.label18.TabIndex = 1015;
            // 
            // label16
            // 
            this.label16.AutoEllipsis = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 523);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(757, 1);
            this.label16.TabIndex = 1014;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(757, 1);
            this.label4.TabIndex = 1013;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoEllipsis = true;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(31, 12);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(140, 14);
            this.label21.TabIndex = 1012;
            this.label21.Text = "Patient Representative :";
            this.label21.Visible = false;
            // 
            // cmbPatientRepresentative
            // 
            this.cmbPatientRepresentative.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPatientRepresentative.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientRepresentative.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPatientRepresentative.FormattingEnabled = true;
            this.cmbPatientRepresentative.Location = new System.Drawing.Point(174, 8);
            this.cmbPatientRepresentative.Name = "cmbPatientRepresentative";
            this.cmbPatientRepresentative.Size = new System.Drawing.Size(412, 22);
            this.cmbPatientRepresentative.TabIndex = 77;
            this.cmbPatientRepresentative.Visible = false;
            // 
            // btnPatientRepresentativeBrowse
            // 
            this.btnPatientRepresentativeBrowse.AutoEllipsis = true;
            this.btnPatientRepresentativeBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnPatientRepresentativeBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPatientRepresentativeBrowse.BackgroundImage")));
            this.btnPatientRepresentativeBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPatientRepresentativeBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientRepresentativeBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientRepresentativeBrowse.Image")));
            this.btnPatientRepresentativeBrowse.Location = new System.Drawing.Point(592, 9);
            this.btnPatientRepresentativeBrowse.Name = "btnPatientRepresentativeBrowse";
            this.btnPatientRepresentativeBrowse.Size = new System.Drawing.Size(22, 21);
            this.btnPatientRepresentativeBrowse.TabIndex = 78;
            this.btnPatientRepresentativeBrowse.UseVisualStyleBackColor = false;
            this.btnPatientRepresentativeBrowse.Visible = false;
            this.btnPatientRepresentativeBrowse.Click += new System.EventHandler(this.btnPatientRepresentativeBrowse_Click);
            // 
            // pnlValidation
            // 
            this.pnlValidation.BackColor = System.Drawing.Color.White;
            this.pnlValidation.Controls.Add(this.pnlUserName);
            this.pnlValidation.Controls.Add(this.panel6);
            this.pnlValidation.Controls.Add(this.panel4);
            this.pnlValidation.Controls.Add(this.label6);
            this.pnlValidation.Controls.Add(this.label13);
            this.pnlValidation.Controls.Add(this.panel3);
            this.pnlValidation.Controls.Add(this.panel5);
            this.pnlValidation.Controls.Add(this.pnlPatientBlock);
            this.pnlValidation.Location = new System.Drawing.Point(183, 226);
            this.pnlValidation.Name = "pnlValidation";
            this.pnlValidation.Size = new System.Drawing.Size(404, 158);
            this.pnlValidation.TabIndex = 1021;
            this.pnlValidation.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(3, 154);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(397, 3);
            this.panel6.TabIndex = 1037;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(400, 27);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 130);
            this.panel4.TabIndex = 1035;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(3, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(400, 1);
            this.label6.TabIndex = 1033;
            this.label6.Text = "label2";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label13.Location = new System.Drawing.Point(403, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 131);
            this.label13.TabIndex = 1032;
            this.label13.Text = "label3";
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(3, 131);
            this.panel3.TabIndex = 1034;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.btnblockcancel);
            this.panel5.Controls.Add(this.lblGIHeader);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(404, 27);
            this.panel5.TabIndex = 1036;
            // 
            // btnblockcancel
            // 
            this.btnblockcancel.BackColor = System.Drawing.Color.Transparent;
            this.btnblockcancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnblockcancel.BackgroundImage")));
            this.btnblockcancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnblockcancel.FlatAppearance.BorderSize = 0;
            this.btnblockcancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnblockcancel.Location = new System.Drawing.Point(380, 4);
            this.btnblockcancel.Name = "btnblockcancel";
            this.btnblockcancel.Size = new System.Drawing.Size(18, 18);
            this.btnblockcancel.TabIndex = 1025;
            this.btnblockcancel.UseVisualStyleBackColor = false;
            this.btnblockcancel.Click += new System.EventHandler(this.btnblockcancel_Click);
            // 
            // lblGIHeader
            // 
            this.lblGIHeader.AutoSize = true;
            this.lblGIHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblGIHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGIHeader.ForeColor = System.Drawing.Color.White;
            this.lblGIHeader.Location = new System.Drawing.Point(3, 7);
            this.lblGIHeader.Name = "lblGIHeader";
            this.lblGIHeader.Size = new System.Drawing.Size(0, 14);
            this.lblGIHeader.TabIndex = 0;
            this.lblGIHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlUserName
            // 
            this.pnlUserName.Controls.Add(this.label29);
            this.pnlUserName.Controls.Add(this.txtUserName);
            this.pnlUserName.Controls.Add(this.btnCancelUserName);
            this.pnlUserName.Controls.Add(this.btnProceedUserName);
            this.pnlUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUserName.Location = new System.Drawing.Point(3, 27);
            this.pnlUserName.Name = "pnlUserName";
            this.pnlUserName.Size = new System.Drawing.Size(397, 127);
            this.pnlUserName.TabIndex = 1039;
            // 
            // label29
            // 
            this.label29.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label29.AutoEllipsis = true;
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(24, 29);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(69, 14);
            this.label29.TabIndex = 1027;
            this.label29.Text = "Username :";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(96, 25);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(271, 22);
            this.txtUserName.TabIndex = 1026;
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserName_KeyPress);
            // 
            // btnCancelUserName
            // 
            this.btnCancelUserName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancelUserName.BackgroundImage")));
            this.btnCancelUserName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancelUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelUserName.Location = new System.Drawing.Point(212, 62);
            this.btnCancelUserName.Name = "btnCancelUserName";
            this.btnCancelUserName.Size = new System.Drawing.Size(80, 25);
            this.btnCancelUserName.TabIndex = 1025;
            this.btnCancelUserName.Text = "Cancel";
            this.btnCancelUserName.UseVisualStyleBackColor = true;
            this.btnCancelUserName.Click += new System.EventHandler(this.btnCancelUserName_Click);
            // 
            // btnProceedUserName
            // 
            this.btnProceedUserName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProceedUserName.BackgroundImage")));
            this.btnProceedUserName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProceedUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProceedUserName.Location = new System.Drawing.Point(126, 62);
            this.btnProceedUserName.Name = "btnProceedUserName";
            this.btnProceedUserName.Size = new System.Drawing.Size(80, 25);
            this.btnProceedUserName.TabIndex = 1024;
            this.btnProceedUserName.Text = "Proceed";
            this.btnProceedUserName.UseVisualStyleBackColor = true;
            this.btnProceedUserName.Click += new System.EventHandler(this.btnProceedUserName_Click);
            // 
            // pnlPatientBlock
            // 
            this.pnlPatientBlock.Controls.Add(this.label2);
            this.pnlPatientBlock.Controls.Add(this.txtBlockReason);
            this.pnlPatientBlock.Controls.Add(this.btnBlockProceed);
            this.pnlPatientBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientBlock.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientBlock.Name = "pnlPatientBlock";
            this.pnlPatientBlock.Size = new System.Drawing.Size(404, 158);
            this.pnlPatientBlock.TabIndex = 1038;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 14);
            this.label2.TabIndex = 1021;
            this.label2.Text = "Enter reason for Block/Inactivation\r\n";
            // 
            // txtBlockReason
            // 
            this.txtBlockReason.Location = new System.Drawing.Point(11, 55);
            this.txtBlockReason.Multiline = true;
            this.txtBlockReason.Name = "txtBlockReason";
            this.txtBlockReason.Size = new System.Drawing.Size(374, 53);
            this.txtBlockReason.TabIndex = 1022;
            // 
            // btnBlockProceed
            // 
            this.btnBlockProceed.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBlockProceed.BackgroundImage")));
            this.btnBlockProceed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBlockProceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlockProceed.Location = new System.Drawing.Point(145, 118);
            this.btnBlockProceed.Name = "btnBlockProceed";
            this.btnBlockProceed.Size = new System.Drawing.Size(80, 25);
            this.btnBlockProceed.TabIndex = 1024;
            this.btnBlockProceed.Text = "Proceed";
            this.btnBlockProceed.UseVisualStyleBackColor = true;
            this.btnBlockProceed.Click += new System.EventHandler(this.btnBlockProceed_Click);
            // 
            // frmAPIAccessAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(771, 610);
            this.Controls.Add(this.pnlValidation);
            this.Controls.Add(this.pnlNotes);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAPIAccessAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "API Access Account";
            this.Load += new System.EventHandler(this.frmAddNotes_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlNotes.ResumeLayout(false);
            this.pnl_Base.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnlPortalLoginCredentials.ResumeLayout(false);
            this.pnlPortalLoginCredentials.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlGIHeader.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.pnlValidation.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlUserName.ResumeLayout(false);
            this.pnlUserName.PerformLayout();
            this.pnlPatientBlock.ResumeLayout(false);
            this.pnlPatientBlock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlNotes;
        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.ToolStripButton tls_btnActivatePatient;
        private System.Windows.Forms.ToolStripButton tls_btnSendInvitation;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbPatientRepresentative;
        private System.Windows.Forms.Button btnPatientRepresentativeBrowse;
        private System.Windows.Forms.Label lblDateOfLastLogin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDateOfActivation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDateOfInvitation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPortalAccountStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel pnlPatientRepresentative;
        private System.Windows.Forms.Panel pnlValidation;
        private System.Windows.Forms.Button btnBlockProceed;
        private System.Windows.Forms.TextBox txtBlockReason;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnblockcancel;
        private System.Windows.Forms.Panel pnlGIHeader;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblGIHeader;
        private System.Windows.Forms.ToolStripButton tls_btnQuickActivate;
        private System.Windows.Forms.Label lblPortalLoginCredentials;
        private System.Windows.Forms.Panel pnlPortalLoginCredentials;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnPrintLoginCredentials;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton tls_btnResetTempPassword;
        internal System.Windows.Forms.ToolStripButton tls_btnAdd;
        internal System.Windows.Forms.ToolStripButton tls_btnRemove;
        internal System.Windows.Forms.ToolStripButton tls_btnSave;
        internal System.Windows.Forms.ToolStripButton tls_btnBrowse;
        private System.Windows.Forms.Label lblBlockDate;
        private System.Windows.Forms.Label lblBlockDate1;
        private System.Windows.Forms.Label lblBlockReason1;
        private System.Windows.Forms.TextBox txtBlockReason1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker mskDOInfoProvided;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPortalUserName;
        private System.Windows.Forms.Panel pnlPatientBlock;
        private System.Windows.Forms.Panel pnlUserName;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnCancelUserName;
        private System.Windows.Forms.Button btnProceedUserName;
        private System.Windows.Forms.Button btnSendLoginCredentialsEmail;
       // private Janus.Windows.UI.Dock.UIPanelManager uiPanelManager1;
    }
}