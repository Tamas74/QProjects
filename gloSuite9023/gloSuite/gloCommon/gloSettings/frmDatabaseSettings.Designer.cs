namespace gloSettings
{
    partial class frmDatabaseSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatabaseSettings));
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txt_PMSDB_Password = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_PMSDB_UserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_PMSDB_Authentication = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_PMSDB_DatabaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_PMSDB_ServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_Settings = new System.Windows.Forms.TabControl();
            this.tbpg_PMSDBSettings = new System.Windows.Forms.TabPage();
            this.grPMSAuthentication = new System.Windows.Forms.GroupBox();
            this.tbpg_ReportingDBSettings = new System.Windows.Forms.TabPage();
            this.grReportingServiceAuthentication = new System.Windows.Forms.GroupBox();
            this.chk_RPT_SameAsPMSDBSettings = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_RPTDB_ServerName = new System.Windows.Forms.TextBox();
            this.cmb_RPTDB_Authentication = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_RPTDB_DataBaseName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_RPTDB_UserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_RPTDB_Password = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnOk = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.tb_Settings.SuspendLayout();
            this.tbpg_PMSDBSettings.SuspendLayout();
            this.grPMSAuthentication.SuspendLayout();
            this.tbpg_ReportingDBSettings.SuspendLayout();
            this.grReportingServiceAuthentication.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_tlspTOP.SuspendLayout();
            this.tls.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(125)))), ((int)(((byte)(146)))));
            this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Location = new System.Drawing.Point(82, 0);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(50, 25);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.MouseLeave += new System.EventHandler(this.btnHelp_MouseLeave);
            this.btnHelp.MouseHover += new System.EventHandler(this.btnHelp_MouseHover);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(125)))), ((int)(((byte)(146)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(28, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(54, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseHover += new System.EventHandler(this.btnCancel_MouseHover);
            // 
            // btnOK
            // 
            this.btnOK.AutoSize = true;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(125)))), ((int)(((byte)(146)))));
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(-22, 0);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(50, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.MouseLeave += new System.EventHandler(this.btnOK_MouseLeave);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnOK.MouseHover += new System.EventHandler(this.btnOK_MouseHover);
            // 
            // txt_PMSDB_Password
            // 
            this.txt_PMSDB_Password.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PMSDB_Password.ForeColor = System.Drawing.Color.Black;
            this.txt_PMSDB_Password.Location = new System.Drawing.Point(131, 175);
            this.txt_PMSDB_Password.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PMSDB_Password.Name = "txt_PMSDB_Password";
            this.txt_PMSDB_Password.PasswordChar = '*';
            this.txt_PMSDB_Password.Size = new System.Drawing.Size(203, 22);
            this.txt_PMSDB_Password.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(59, 179);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 14);
            this.label5.TabIndex = 21;
            this.label5.Text = "Password :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_PMSDB_UserName
            // 
            this.txt_PMSDB_UserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PMSDB_UserName.ForeColor = System.Drawing.Color.Black;
            this.txt_PMSDB_UserName.Location = new System.Drawing.Point(131, 138);
            this.txt_PMSDB_UserName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PMSDB_UserName.Name = "txt_PMSDB_UserName";
            this.txt_PMSDB_UserName.Size = new System.Drawing.Size(203, 22);
            this.txt_PMSDB_UserName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(51, 142);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 14);
            this.label4.TabIndex = 19;
            this.label4.Text = "User Name :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_PMSDB_Authentication
            // 
            this.cmb_PMSDB_Authentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_PMSDB_Authentication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_PMSDB_Authentication.ForeColor = System.Drawing.Color.Black;
            this.cmb_PMSDB_Authentication.FormattingEnabled = true;
            this.cmb_PMSDB_Authentication.Location = new System.Drawing.Point(131, 101);
            this.cmb_PMSDB_Authentication.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_PMSDB_Authentication.Name = "cmb_PMSDB_Authentication";
            this.cmb_PMSDB_Authentication.Size = new System.Drawing.Size(203, 22);
            this.cmb_PMSDB_Authentication.TabIndex = 2;
            this.cmb_PMSDB_Authentication.SelectedIndexChanged += new System.EventHandler(this.cmbAuthentication_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "Authentication :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_PMSDB_DatabaseName
            // 
            this.txt_PMSDB_DatabaseName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PMSDB_DatabaseName.ForeColor = System.Drawing.Color.Black;
            this.txt_PMSDB_DatabaseName.Location = new System.Drawing.Point(131, 64);
            this.txt_PMSDB_DatabaseName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PMSDB_DatabaseName.Name = "txt_PMSDB_DatabaseName";
            this.txt_PMSDB_DatabaseName.Size = new System.Drawing.Size(203, 22);
            this.txt_PMSDB_DatabaseName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "Database Name :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_PMSDB_ServerName
            // 
            this.txt_PMSDB_ServerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PMSDB_ServerName.ForeColor = System.Drawing.Color.Black;
            this.txt_PMSDB_ServerName.Location = new System.Drawing.Point(131, 27);
            this.txt_PMSDB_ServerName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PMSDB_ServerName.Name = "txt_PMSDB_ServerName";
            this.txt_PMSDB_ServerName.Size = new System.Drawing.Size(203, 22);
            this.txt_PMSDB_ServerName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "SQL Server Name :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.tb_Settings);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.panel1.Size = new System.Drawing.Size(378, 249);
            this.panel1.TabIndex = 22;
            // 
            // tb_Settings
            // 
            this.tb_Settings.Controls.Add(this.tbpg_PMSDBSettings);
            this.tb_Settings.Controls.Add(this.tbpg_ReportingDBSettings);
            this.tb_Settings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Settings.Location = new System.Drawing.Point(0, 2);
            this.tb_Settings.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Settings.Name = "tb_Settings";
            this.tb_Settings.SelectedIndex = 0;
            this.tb_Settings.Size = new System.Drawing.Size(378, 247);
            this.tb_Settings.TabIndex = 2;
            // 
            // tbpg_PMSDBSettings
            // 
            this.tbpg_PMSDBSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_PMSDBSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbpg_PMSDBSettings.Controls.Add(this.grPMSAuthentication);
            this.tbpg_PMSDBSettings.Location = new System.Drawing.Point(4, 23);
            this.tbpg_PMSDBSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tbpg_PMSDBSettings.Name = "tbpg_PMSDBSettings";
            this.tbpg_PMSDBSettings.Padding = new System.Windows.Forms.Padding(2);
            this.tbpg_PMSDBSettings.Size = new System.Drawing.Size(370, 220);
            this.tbpg_PMSDBSettings.TabIndex = 0;
            this.tbpg_PMSDBSettings.Text = "gloPM DB Settings";
            this.tbpg_PMSDBSettings.UseVisualStyleBackColor = true;
            // 
            // grPMSAuthentication
            // 
            this.grPMSAuthentication.Controls.Add(this.label1);
            this.grPMSAuthentication.Controls.Add(this.label4);
            this.grPMSAuthentication.Controls.Add(this.txt_PMSDB_UserName);
            this.grPMSAuthentication.Controls.Add(this.txt_PMSDB_ServerName);
            this.grPMSAuthentication.Controls.Add(this.cmb_PMSDB_Authentication);
            this.grPMSAuthentication.Controls.Add(this.label2);
            this.grPMSAuthentication.Controls.Add(this.label5);
            this.grPMSAuthentication.Controls.Add(this.txt_PMSDB_DatabaseName);
            this.grPMSAuthentication.Controls.Add(this.label3);
            this.grPMSAuthentication.Controls.Add(this.txt_PMSDB_Password);
            this.grPMSAuthentication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grPMSAuthentication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grPMSAuthentication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grPMSAuthentication.Location = new System.Drawing.Point(2, 2);
            this.grPMSAuthentication.Margin = new System.Windows.Forms.Padding(2);
            this.grPMSAuthentication.Name = "grPMSAuthentication";
            this.grPMSAuthentication.Padding = new System.Windows.Forms.Padding(2);
            this.grPMSAuthentication.Size = new System.Drawing.Size(366, 216);
            this.grPMSAuthentication.TabIndex = 0;
            this.grPMSAuthentication.TabStop = false;
            this.grPMSAuthentication.Text = "gloPM DB Settings";
            // 
            // tbpg_ReportingDBSettings
            // 
            this.tbpg_ReportingDBSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_ReportingDBSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tbpg_ReportingDBSettings.Controls.Add(this.grReportingServiceAuthentication);
            this.tbpg_ReportingDBSettings.Location = new System.Drawing.Point(4, 23);
            this.tbpg_ReportingDBSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tbpg_ReportingDBSettings.Name = "tbpg_ReportingDBSettings";
            this.tbpg_ReportingDBSettings.Padding = new System.Windows.Forms.Padding(2);
            this.tbpg_ReportingDBSettings.Size = new System.Drawing.Size(370, 220);
            this.tbpg_ReportingDBSettings.TabIndex = 1;
            this.tbpg_ReportingDBSettings.Text = "Reporting DB Settings";
            // 
            // grReportingServiceAuthentication
            // 
            this.grReportingServiceAuthentication.BackColor = System.Drawing.Color.Transparent;
            this.grReportingServiceAuthentication.Controls.Add(this.chk_RPT_SameAsPMSDBSettings);
            this.grReportingServiceAuthentication.Controls.Add(this.label8);
            this.grReportingServiceAuthentication.Controls.Add(this.txt_RPTDB_ServerName);
            this.grReportingServiceAuthentication.Controls.Add(this.cmb_RPTDB_Authentication);
            this.grReportingServiceAuthentication.Controls.Add(this.label9);
            this.grReportingServiceAuthentication.Controls.Add(this.txt_RPTDB_DataBaseName);
            this.grReportingServiceAuthentication.Controls.Add(this.label10);
            this.grReportingServiceAuthentication.Controls.Add(this.label6);
            this.grReportingServiceAuthentication.Controls.Add(this.txt_RPTDB_UserName);
            this.grReportingServiceAuthentication.Controls.Add(this.label7);
            this.grReportingServiceAuthentication.Controls.Add(this.txt_RPTDB_Password);
            this.grReportingServiceAuthentication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grReportingServiceAuthentication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grReportingServiceAuthentication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grReportingServiceAuthentication.Location = new System.Drawing.Point(2, 2);
            this.grReportingServiceAuthentication.Margin = new System.Windows.Forms.Padding(2);
            this.grReportingServiceAuthentication.Name = "grReportingServiceAuthentication";
            this.grReportingServiceAuthentication.Padding = new System.Windows.Forms.Padding(2);
            this.grReportingServiceAuthentication.Size = new System.Drawing.Size(366, 216);
            this.grReportingServiceAuthentication.TabIndex = 1;
            this.grReportingServiceAuthentication.TabStop = false;
            this.grReportingServiceAuthentication.Text = "SQL Authentication For gloReporting ";
            // 
            // chk_RPT_SameAsPMSDBSettings
            // 
            this.chk_RPT_SameAsPMSDBSettings.AutoSize = true;
            this.chk_RPT_SameAsPMSDBSettings.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_RPT_SameAsPMSDBSettings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_RPT_SameAsPMSDBSettings.Location = new System.Drawing.Point(5, 19);
            this.chk_RPT_SameAsPMSDBSettings.Margin = new System.Windows.Forms.Padding(2);
            this.chk_RPT_SameAsPMSDBSettings.Name = "chk_RPT_SameAsPMSDBSettings";
            this.chk_RPT_SameAsPMSDBSettings.Size = new System.Drawing.Size(177, 18);
            this.chk_RPT_SameAsPMSDBSettings.TabIndex = 32;
            this.chk_RPT_SameAsPMSDBSettings.Text = "Same As gloPM DB Settings";
            this.chk_RPT_SameAsPMSDBSettings.UseVisualStyleBackColor = true;
            this.chk_RPT_SameAsPMSDBSettings.CheckedChanged += new System.EventHandler(this.chk_RPT_SameAsPMSDBSettings_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 53);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 14);
            this.label8.TabIndex = 29;
            this.label8.Text = "SQL Server Name :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_RPTDB_ServerName
            // 
            this.txt_RPTDB_ServerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RPTDB_ServerName.ForeColor = System.Drawing.Color.Black;
            this.txt_RPTDB_ServerName.Location = new System.Drawing.Point(139, 50);
            this.txt_RPTDB_ServerName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_RPTDB_ServerName.Name = "txt_RPTDB_ServerName";
            this.txt_RPTDB_ServerName.Size = new System.Drawing.Size(195, 22);
            this.txt_RPTDB_ServerName.TabIndex = 26;
            // 
            // cmb_RPTDB_Authentication
            // 
            this.cmb_RPTDB_Authentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_RPTDB_Authentication.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_RPTDB_Authentication.ForeColor = System.Drawing.Color.Black;
            this.cmb_RPTDB_Authentication.FormattingEnabled = true;
            this.cmb_RPTDB_Authentication.Location = new System.Drawing.Point(139, 112);
            this.cmb_RPTDB_Authentication.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_RPTDB_Authentication.Name = "cmb_RPTDB_Authentication";
            this.cmb_RPTDB_Authentication.Size = new System.Drawing.Size(195, 22);
            this.cmb_RPTDB_Authentication.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(26, 84);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 14);
            this.label9.TabIndex = 30;
            this.label9.Text = "Database Name :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_RPTDB_DataBaseName
            // 
            this.txt_RPTDB_DataBaseName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RPTDB_DataBaseName.ForeColor = System.Drawing.Color.Black;
            this.txt_RPTDB_DataBaseName.Location = new System.Drawing.Point(139, 81);
            this.txt_RPTDB_DataBaseName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_RPTDB_DataBaseName.Name = "txt_RPTDB_DataBaseName";
            this.txt_RPTDB_DataBaseName.Size = new System.Drawing.Size(195, 22);
            this.txt_RPTDB_DataBaseName.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(30, 115);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 14);
            this.label10.TabIndex = 31;
            this.label10.Text = "Authentication :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(52, 146);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 14);
            this.label6.TabIndex = 24;
            this.label6.Text = "User Name :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_RPTDB_UserName
            // 
            this.txt_RPTDB_UserName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RPTDB_UserName.ForeColor = System.Drawing.Color.Black;
            this.txt_RPTDB_UserName.Location = new System.Drawing.Point(139, 143);
            this.txt_RPTDB_UserName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_RPTDB_UserName.Name = "txt_RPTDB_UserName";
            this.txt_RPTDB_UserName.Size = new System.Drawing.Size(195, 22);
            this.txt_RPTDB_UserName.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(60, 177);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 14);
            this.label7.TabIndex = 25;
            this.label7.Text = "Password :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_RPTDB_Password
            // 
            this.txt_RPTDB_Password.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_RPTDB_Password.ForeColor = System.Drawing.Color.Black;
            this.txt_RPTDB_Password.Location = new System.Drawing.Point(139, 174);
            this.txt_RPTDB_Password.Margin = new System.Windows.Forms.Padding(2);
            this.txt_RPTDB_Password.Name = "txt_RPTDB_Password";
            this.txt_RPTDB_Password.PasswordChar = '*';
            this.txt_RPTDB_Password.Size = new System.Drawing.Size(195, 22);
            this.txt_RPTDB_Password.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnHelp);
            this.panel2.Location = new System.Drawing.Point(375, 342);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(132, 25);
            this.panel2.TabIndex = 0;
            this.panel2.Visible = false;
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlspTOP.Controls.Add(this.tls);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlspTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(378, 53);
            this.pnl_tlspTOP.TabIndex = 23;
            // 
            // tls
            // 
            this.tls.BackColor = System.Drawing.Color.Transparent;
            this.tls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls.BackgroundImage")));
            this.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.tls.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnOk,
            this.ts_btnCancel,
            this.toolStripButton1});
            this.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls.Location = new System.Drawing.Point(0, 0);
            this.tls.Name = "tls";
            this.tls.Size = new System.Drawing.Size(378, 53);
            this.tls.TabIndex = 0;
            this.tls.Text = "toolStrip1";
            // 
            // ts_btnOk
            // 
            this.ts_btnOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnOk.Image")));
            this.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnOk.Name = "ts_btnOk";
            this.ts_btnOk.Size = new System.Drawing.Size(66, 50);
            this.ts_btnOk.Tag = "OK";
            this.ts_btnOk.Text = "&Save&&Cls";
            this.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnOk.ToolTipText = "Save and Close";
            this.ts_btnOk.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ts_btnCancel
            // 
            this.ts_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCancel.Image")));
            this.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCancel.Name = "ts_btnCancel";
            this.ts_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.ts_btnCancel.Tag = "Cancel";
            this.ts_btnCancel.Text = "&Close";
            this.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(38, 50);
            this.toolStripButton1.Tag = "Help";
            this.toolStripButton1.Text = "&Help";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Visible = false;
            // 
            // frmDatabaseSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(378, 302);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDatabaseSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Settings";
            this.Load += new System.EventHandler(this.frmDatabaseSettings_Load);
            this.panel1.ResumeLayout(false);
            this.tb_Settings.ResumeLayout(false);
            this.tbpg_PMSDBSettings.ResumeLayout(false);
            this.grPMSAuthentication.ResumeLayout(false);
            this.grPMSAuthentication.PerformLayout();
            this.tbpg_ReportingDBSettings.ResumeLayout(false);
            this.grReportingServiceAuthentication.ResumeLayout(false);
            this.grReportingServiceAuthentication.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.tls.ResumeLayout(false);
            this.tls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txt_PMSDB_Password;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_PMSDB_UserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_PMSDB_Authentication;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_PMSDB_DatabaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_PMSDB_ServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grReportingServiceAuthentication;
        private System.Windows.Forms.GroupBox grPMSAuthentication;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_RPTDB_UserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_RPTDB_Password;
        private System.Windows.Forms.TabControl tb_Settings;
        private System.Windows.Forms.TabPage tbpg_PMSDBSettings;
        private System.Windows.Forms.TabPage tbpg_ReportingDBSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_RPTDB_ServerName;
        private System.Windows.Forms.ComboBox cmb_RPTDB_Authentication;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_RPTDB_DataBaseName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chk_RPT_SameAsPMSDBSettings;
        private System.Windows.Forms.Panel pnl_tlspTOP;
        private gloGlobal.gloToolStripIgnoreFocus tls;
        private System.Windows.Forms.ToolStripButton ts_btnOk;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}