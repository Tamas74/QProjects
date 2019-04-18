namespace gloICDAnalysis
{
    partial class frmDBSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBSettings));
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tls = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnOk = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txt_PMSDB_Password
            // 
            this.txt_PMSDB_Password.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PMSDB_Password.ForeColor = System.Drawing.Color.Black;
            this.txt_PMSDB_Password.Location = new System.Drawing.Point(134, 163);
            this.txt_PMSDB_Password.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PMSDB_Password.Name = "txt_PMSDB_Password";
            this.txt_PMSDB_Password.PasswordChar = '*';
            this.txt_PMSDB_Password.Size = new System.Drawing.Size(235, 22);
            this.txt_PMSDB_Password.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(64, 167);
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
            this.txt_PMSDB_UserName.Location = new System.Drawing.Point(134, 126);
            this.txt_PMSDB_UserName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PMSDB_UserName.Name = "txt_PMSDB_UserName";
            this.txt_PMSDB_UserName.Size = new System.Drawing.Size(235, 22);
            this.txt_PMSDB_UserName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(56, 130);
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
            this.cmb_PMSDB_Authentication.Location = new System.Drawing.Point(134, 89);
            this.cmb_PMSDB_Authentication.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_PMSDB_Authentication.Name = "cmb_PMSDB_Authentication";
            this.cmb_PMSDB_Authentication.Size = new System.Drawing.Size(235, 22);
            this.cmb_PMSDB_Authentication.TabIndex = 2;
            this.cmb_PMSDB_Authentication.SelectedIndexChanged += new System.EventHandler(this.cmbAuthentication_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 93);
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
            this.txt_PMSDB_DatabaseName.Location = new System.Drawing.Point(134, 52);
            this.txt_PMSDB_DatabaseName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PMSDB_DatabaseName.Name = "txt_PMSDB_DatabaseName";
            this.txt_PMSDB_DatabaseName.Size = new System.Drawing.Size(235, 22);
            this.txt_PMSDB_DatabaseName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 56);
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
            this.txt_PMSDB_ServerName.Location = new System.Drawing.Point(134, 15);
            this.txt_PMSDB_ServerName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PMSDB_ServerName.Name = "txt_PMSDB_ServerName";
            this.txt_PMSDB_ServerName.Size = new System.Drawing.Size(235, 22);
            this.txt_PMSDB_ServerName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 19);
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
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_PMSDB_ServerName);
            this.panel1.Controls.Add(this.txt_PMSDB_UserName);
            this.panel1.Controls.Add(this.txt_PMSDB_Password);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmb_PMSDB_Authentication);
            this.panel1.Controls.Add(this.txt_PMSDB_DatabaseName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(405, 204);
            this.panel1.TabIndex = 22;
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
            this.pnl_tlspTOP.Size = new System.Drawing.Size(405, 53);
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
            this.tls.Size = new System.Drawing.Size(405, 53);
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
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(399, 1);
            this.label7.TabIndex = 23;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 200);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(399, 1);
            this.label8.TabIndex = 24;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 196);
            this.label9.TabIndex = 25;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(401, 4);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 196);
            this.label10.TabIndex = 26;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmDatabaseSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(405, 257);
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
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel pnl_tlspTOP;
        private gloGlobal.gloToolStripIgnoreFocus tls;
        private System.Windows.Forms.ToolStripButton ts_btnOk;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}