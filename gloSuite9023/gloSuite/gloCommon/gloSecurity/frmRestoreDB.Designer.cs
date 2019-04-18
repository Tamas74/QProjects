namespace gloSecurity
{
    partial class frmRestoreDB
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
                try
                {
                    if (dlgBackupFile != null)
                    {

                        dlgBackupFile.Dispose();
                        dlgBackupFile = null;
                    }
                }
                catch
                {
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRestoreDB));
            this.pnt_tlStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnl_RestoreInfo = new System.Windows.Forms.Panel();
            this.rbtn_NewDatabse = new System.Windows.Forms.RadioButton();
            this.rbtn_Overwrite = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDatabases = new System.Windows.Forms.ComboBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtBackupFile = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.dlgBackupFile = new System.Windows.Forms.OpenFileDialog();
            this.pnt_tlStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnl_RestoreInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnt_tlStrip
            // 
            this.pnt_tlStrip.Controls.Add(this.ts_Commands);
            this.pnt_tlStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnt_tlStrip.Location = new System.Drawing.Point(0, 0);
            this.pnt_tlStrip.Name = "pnt_tlStrip";
            this.pnt_tlStrip.Size = new System.Drawing.Size(393, 53);
            this.pnt_tlStrip.TabIndex = 0;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(393, 52);
            this.ts_Commands.TabIndex = 33;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_OK
            // 
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(36, 49);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "  OK";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(46, 49);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = " Cancel";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnl_RestoreInfo
            // 
            this.pnl_RestoreInfo.Controls.Add(this.rbtn_NewDatabse);
            this.pnl_RestoreInfo.Controls.Add(this.rbtn_Overwrite);
            this.pnl_RestoreInfo.Controls.Add(this.label1);
            this.pnl_RestoreInfo.Controls.Add(this.cmbDatabases);
            this.pnl_RestoreInfo.Controls.Add(this.btnBrowse);
            this.pnl_RestoreInfo.Controls.Add(this.txtBackupFile);
            this.pnl_RestoreInfo.Controls.Add(this.txtDatabase);
            this.pnl_RestoreInfo.Controls.Add(this.Label33);
            this.pnl_RestoreInfo.Controls.Add(this.Label32);
            this.pnl_RestoreInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_RestoreInfo.Location = new System.Drawing.Point(0, 53);
            this.pnl_RestoreInfo.Name = "pnl_RestoreInfo";
            this.pnl_RestoreInfo.Size = new System.Drawing.Size(393, 229);
            this.pnl_RestoreInfo.TabIndex = 1;
            // 
            // rbtn_NewDatabse
            // 
            this.rbtn_NewDatabse.AutoSize = true;
            this.rbtn_NewDatabse.Location = new System.Drawing.Point(122, 27);
            this.rbtn_NewDatabse.Name = "rbtn_NewDatabse";
            this.rbtn_NewDatabse.Size = new System.Drawing.Size(82, 17);
            this.rbtn_NewDatabse.TabIndex = 87;
            this.rbtn_NewDatabse.Text = "Create New";
            this.rbtn_NewDatabse.UseVisualStyleBackColor = true;
            // 
            // rbtn_Overwrite
            // 
            this.rbtn_Overwrite.AutoSize = true;
            this.rbtn_Overwrite.Checked = true;
            this.rbtn_Overwrite.Location = new System.Drawing.Point(231, 27);
            this.rbtn_Overwrite.Name = "rbtn_Overwrite";
            this.rbtn_Overwrite.Size = new System.Drawing.Size(113, 17);
            this.rbtn_Overwrite.TabIndex = 88;
            this.rbtn_Overwrite.TabStop = true;
            this.rbtn_Overwrite.Text = "Overwrite Existing";
            this.rbtn_Overwrite.UseVisualStyleBackColor = true;
            this.rbtn_Overwrite.CheckedChanged += new System.EventHandler(this.rbtn_Overwrite_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label1.Location = new System.Drawing.Point(21, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 86;
            this.label1.Text = "Select Database";
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(122, 61);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(167, 21);
            this.cmbDatabases.TabIndex = 85;
            this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnBrowse.Location = new System.Drawing.Point(305, 145);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(72, 23);
            this.btnBrowse.TabIndex = 84;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtBackupFile
            // 
            this.txtBackupFile.Location = new System.Drawing.Point(122, 145);
            this.txtBackupFile.Name = "txtBackupFile";
            this.txtBackupFile.Size = new System.Drawing.Size(167, 21);
            this.txtBackupFile.TabIndex = 81;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(122, 105);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(167, 21);
            this.txtDatabase.TabIndex = 80;
            this.txtDatabase.TextChanged += new System.EventHandler(this.txtDatabase_TextChanged);
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.BackColor = System.Drawing.Color.Transparent;
            this.Label33.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Label33.Location = new System.Drawing.Point(44, 108);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(62, 13);
            this.Label33.TabIndex = 82;
            this.Label33.Text = "To Databse";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.BackColor = System.Drawing.Color.Transparent;
            this.Label32.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Label32.Location = new System.Drawing.Point(56, 147);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(50, 13);
            this.Label32.TabIndex = 83;
            this.Label32.Text = "From File";
            // 
            // dlgBackupFile
            // 
            this.dlgBackupFile.FileName = "openFileDialog1";
            // 
            // frmRestoreDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(393, 282);
            this.Controls.Add(this.pnl_RestoreInfo);
            this.Controls.Add(this.pnt_tlStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRestoreDB";
            this.Text = "Restore Database";
            this.Load += new System.EventHandler(this.frmRestoreDB_Load);
            this.pnt_tlStrip.ResumeLayout(false);
            this.pnt_tlStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnl_RestoreInfo.ResumeLayout(false);
            this.pnl_RestoreInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnt_tlStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnl_RestoreInfo;
        private System.Windows.Forms.TextBox txtBackupFile;
        private System.Windows.Forms.TextBox txtDatabase;
        internal System.Windows.Forms.Label Label33;
        internal System.Windows.Forms.Label Label32;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog dlgBackupFile;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDatabases;
        private System.Windows.Forms.RadioButton rbtn_NewDatabse;
        private System.Windows.Forms.RadioButton rbtn_Overwrite;
    }
}