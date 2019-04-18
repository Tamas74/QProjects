namespace AdminLibrary
{
    partial class DataBaseInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataBaseInfo));
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkIsReplication = new System.Windows.Forms.CheckBox();
            this.lblDBVersion = new System.Windows.Forms.Label();
            this.ddlDBVersion = new System.Windows.Forms.ComboBox();
            this.txtgloDataPath = new System.Windows.Forms.TextBox();
            this.lblgloDataPath = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rbtSQL = new System.Windows.Forms.RadioButton();
            this.rbtWindows = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbtExistingDatabase = new System.Windows.Forms.RadioButton();
            this.rbtNewDataBase = new System.Windows.Forms.RadioButton();
            this.lblDataBaseName = new System.Windows.Forms.Label();
            this.lblSQlServer = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDataBaseName = new System.Windows.Forms.TextBox();
            this.txtPrerequisitePath = new System.Windows.Forms.TextBox();
            this.txtSqlServer = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkIsReplication);
            this.panel2.Controls.Add(this.lblDBVersion);
            this.panel2.Controls.Add(this.ddlDBVersion);
            this.panel2.Controls.Add(this.txtgloDataPath);
            this.panel2.Controls.Add(this.lblgloDataPath);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.lblDataBaseName);
            this.panel2.Controls.Add(this.lblSQlServer);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtDataBaseName);
            this.panel2.Controls.Add(this.txtPrerequisitePath);
            this.panel2.Controls.Add(this.txtSqlServer);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSubmit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(494, 309);
            this.panel2.TabIndex = 27;
            // 
            // chkIsReplication
            // 
            this.chkIsReplication.AutoSize = true;
            this.chkIsReplication.Checked = true;
            this.chkIsReplication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsReplication.Font = new System.Drawing.Font("Arial", 8.25F);
            this.chkIsReplication.Location = new System.Drawing.Point(36, 185);
            this.chkIsReplication.Name = "chkIsReplication";
            this.chkIsReplication.Size = new System.Drawing.Size(152, 18);
            this.chkIsReplication.TabIndex = 39;
            this.chkIsReplication.Text = "Is Database in  Replication";
            this.chkIsReplication.UseVisualStyleBackColor = true;
            this.chkIsReplication.CheckedChanged += new System.EventHandler(this.chkIsReplication_CheckedChanged);
            // 
            // lblDBVersion
            // 
            this.lblDBVersion.AutoSize = true;
            this.lblDBVersion.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblDBVersion.Location = new System.Drawing.Point(33, 212);
            this.lblDBVersion.Name = "lblDBVersion";
            this.lblDBVersion.Size = new System.Drawing.Size(127, 14);
            this.lblDBVersion.TabIndex = 38;
            this.lblDBVersion.Text = "Select Database Version";
            this.lblDBVersion.Visible = false;
            // 
            // ddlDBVersion
            // 
            this.ddlDBVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDBVersion.Font = new System.Drawing.Font("Arial", 8.25F);
            this.ddlDBVersion.FormattingEnabled = true;
            this.ddlDBVersion.Location = new System.Drawing.Point(166, 209);
            this.ddlDBVersion.Name = "ddlDBVersion";
            this.ddlDBVersion.Size = new System.Drawing.Size(58, 22);
            this.ddlDBVersion.TabIndex = 37;
            this.ddlDBVersion.Visible = false;
            this.ddlDBVersion.SelectedIndexChanged += new System.EventHandler(this.ddlDBVersion_SelectedIndexChanged);
            // 
            // txtgloDataPath
            // 
            this.txtgloDataPath.Font = new System.Drawing.Font("Arial", 8.25F);
            this.txtgloDataPath.Location = new System.Drawing.Point(177, 9);
            this.txtgloDataPath.Name = "txtgloDataPath";
            this.txtgloDataPath.Size = new System.Drawing.Size(280, 20);
            this.txtgloDataPath.TabIndex = 0;
            this.txtgloDataPath.Leave += new System.EventHandler(this.txtgloDataPath_Leave);
            // 
            // lblgloDataPath
            // 
            this.lblgloDataPath.AutoSize = true;
            this.lblgloDataPath.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblgloDataPath.Location = new System.Drawing.Point(98, 14);
            this.lblgloDataPath.Name = "lblgloDataPath";
            this.lblgloDataPath.Size = new System.Drawing.Size(73, 14);
            this.lblgloDataPath.TabIndex = 36;
            this.lblgloDataPath.Text = "gloData Path :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(230, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 71);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(84, 45);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(129, 20);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.ForeColor = System.Drawing.Color.Black;
            this.txtUserName.Location = new System.Drawing.Point(84, 20);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(129, 20);
            this.txtUserName.TabIndex = 7;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Enabled = false;
            this.lblUserName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.Black;
            this.lblUserName.Location = new System.Drawing.Point(14, 23);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(66, 14);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = " User Name:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Enabled = false;
            this.lblPassword.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(17, 48);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(63, 14);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = " Password:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(33, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 14);
            this.label8.TabIndex = 31;
            this.label8.Text = "Select Authentication Mode:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rbtSQL);
            this.panel4.Controls.Add(this.rbtWindows);
            this.panel4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(177, 151);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(280, 25);
            this.panel4.TabIndex = 5;
            // 
            // rbtSQL
            // 
            this.rbtSQL.AutoSize = true;
            this.rbtSQL.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSQL.Location = new System.Drawing.Point(144, 3);
            this.rbtSQL.Name = "rbtSQL";
            this.rbtSQL.Size = new System.Drawing.Size(46, 18);
            this.rbtSQL.TabIndex = 5;
            this.rbtSQL.TabStop = true;
            this.rbtSQL.Text = "SQL";
            this.rbtSQL.UseVisualStyleBackColor = true;
            this.rbtSQL.CheckedChanged += new System.EventHandler(this.rbtSQL_CheckedChanged);
            // 
            // rbtWindows
            // 
            this.rbtWindows.AutoSize = true;
            this.rbtWindows.Checked = true;
            this.rbtWindows.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtWindows.Location = new System.Drawing.Point(9, 2);
            this.rbtWindows.Name = "rbtWindows";
            this.rbtWindows.Size = new System.Drawing.Size(71, 18);
            this.rbtWindows.TabIndex = 4;
            this.rbtWindows.TabStop = true;
            this.rbtWindows.Text = "Windows";
            this.rbtWindows.UseVisualStyleBackColor = true;
            this.rbtWindows.CheckedChanged += new System.EventHandler(this.rbtWindows_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbtExistingDatabase);
            this.panel3.Controls.Add(this.rbtNewDataBase);
            this.panel3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(177, 116);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(280, 27);
            this.panel3.TabIndex = 4;
            // 
            // rbtExistingDatabase
            // 
            this.rbtExistingDatabase.AutoSize = true;
            this.rbtExistingDatabase.Checked = true;
            this.rbtExistingDatabase.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtExistingDatabase.Location = new System.Drawing.Point(143, 4);
            this.rbtExistingDatabase.Name = "rbtExistingDatabase";
            this.rbtExistingDatabase.Size = new System.Drawing.Size(111, 18);
            this.rbtExistingDatabase.TabIndex = 1;
            this.rbtExistingDatabase.TabStop = true;
            this.rbtExistingDatabase.Text = "Existing Database";
            this.rbtExistingDatabase.UseVisualStyleBackColor = true;
            this.rbtExistingDatabase.CheckedChanged += new System.EventHandler(this.rbtExistingDatabase_CheckedChanged);
            // 
            // rbtNewDataBase
            // 
            this.rbtNewDataBase.AutoSize = true;
            this.rbtNewDataBase.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtNewDataBase.Location = new System.Drawing.Point(9, 4);
            this.rbtNewDataBase.Name = "rbtNewDataBase";
            this.rbtNewDataBase.Size = new System.Drawing.Size(97, 18);
            this.rbtNewDataBase.TabIndex = 0;
            this.rbtNewDataBase.Text = "New Database";
            this.rbtNewDataBase.UseVisualStyleBackColor = true;
            this.rbtNewDataBase.CheckedChanged += new System.EventHandler(this.rbtNewDataBase_CheckedChanged);
            // 
            // lblDataBaseName
            // 
            this.lblDataBaseName.AutoSize = true;
            this.lblDataBaseName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataBaseName.ForeColor = System.Drawing.Color.Black;
            this.lblDataBaseName.Location = new System.Drawing.Point(112, 96);
            this.lblDataBaseName.Name = "lblDataBaseName";
            this.lblDataBaseName.Size = new System.Drawing.Size(62, 14);
            this.lblDataBaseName.TabIndex = 1;
            this.lblDataBaseName.Text = " Database :";
            // 
            // lblSQlServer
            // 
            this.lblSQlServer.AutoSize = true;
            this.lblSQlServer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSQlServer.ForeColor = System.Drawing.Color.Black;
            this.lblSQlServer.Location = new System.Drawing.Point(101, 70);
            this.lblSQlServer.Name = "lblSQlServer";
            this.lblSQlServer.Size = new System.Drawing.Size(73, 14);
            this.lblSQlServer.TabIndex = 0;
            this.lblSQlServer.Text = " SQL Server :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(-8, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(529, 1);
            this.label5.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 14);
            this.label2.TabIndex = 30;
            this.label2.Text = "Prerequisites Path :";
            // 
            // txtDataBaseName
            // 
            this.txtDataBaseName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataBaseName.ForeColor = System.Drawing.Color.Black;
            this.txtDataBaseName.Location = new System.Drawing.Point(177, 93);
            this.txtDataBaseName.Name = "txtDataBaseName";
            this.txtDataBaseName.Size = new System.Drawing.Size(280, 20);
            this.txtDataBaseName.TabIndex = 3;
            // 
            // txtPrerequisitePath
            // 
            this.txtPrerequisitePath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrerequisitePath.Location = new System.Drawing.Point(177, 41);
            this.txtPrerequisitePath.Name = "txtPrerequisitePath";
            this.txtPrerequisitePath.Size = new System.Drawing.Size(280, 20);
            this.txtPrerequisitePath.TabIndex = 1;
            // 
            // txtSqlServer
            // 
            this.txtSqlServer.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSqlServer.ForeColor = System.Drawing.Color.Black;
            this.txtSqlServer.Location = new System.Drawing.Point(177, 67);
            this.txtSqlServer.Name = "txtSqlServer";
            this.txtSqlServer.Size = new System.Drawing.Size(280, 20);
            this.txtSqlServer.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(402, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(314, 268);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(78, 23);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "&Install";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(322, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(10, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 19);
            this.label4.TabIndex = 30;
            this.label4.Text = "Installing gloEMR Admin";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 70);
            this.panel1.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(494, 1);
            this.label6.TabIndex = 33;
            // 
            // DataBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 379);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DataBaseInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "gloEMR Admin Install Information";
            this.Load += new System.EventHandler(this.DataBaseInfo_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtgloDataPath;
        private System.Windows.Forms.Label lblgloDataPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbtSQL;
        private System.Windows.Forms.RadioButton rbtWindows;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbtExistingDatabase;
        private System.Windows.Forms.RadioButton rbtNewDataBase;
        private System.Windows.Forms.Label lblDataBaseName;
        private System.Windows.Forms.Label lblSQlServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDataBaseName;
        private System.Windows.Forms.TextBox txtPrerequisitePath;
        private System.Windows.Forms.TextBox txtSqlServer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ddlDBVersion;
        private System.Windows.Forms.Label lblDBVersion;
        private System.Windows.Forms.CheckBox chkIsReplication;
    }
}