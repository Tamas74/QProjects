namespace gloQRDAImport
{
    partial class frmImportCQMData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportCQMData));
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddSourcePath = new System.Windows.Forms.Button();
            this.tls_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.tls_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtPath.Location = new System.Drawing.Point(116, 119);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(501, 22);
            this.txtPath.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(25, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select Folder :";
            // 
            // btnAddSourcePath
            // 
            this.btnAddSourcePath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddSourcePath.BackgroundImage")));
            this.btnAddSourcePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddSourcePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddSourcePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSourcePath.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnAddSourcePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddSourcePath.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSourcePath.Image")));
            this.btnAddSourcePath.Location = new System.Drawing.Point(622, 118);
            this.btnAddSourcePath.Name = "btnAddSourcePath";
            this.btnAddSourcePath.Size = new System.Drawing.Size(24, 24);
            this.btnAddSourcePath.TabIndex = 9;
            this.btnAddSourcePath.UseVisualStyleBackColor = true;
            this.btnAddSourcePath.Click += new System.EventHandler(this.btnAddSourcePath_Click);
            // 
            // tls_Main
            // 
            this.tls_Main.AutoSize = false;
            this.tls_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Main.BackgroundImage")));
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImport});
            this.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(786, 56);
            this.tls_Main.TabIndex = 10;
            this.tls_Main.TabStop = true;
            this.tls_Main.Text = "ToolStrip1";
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(54, 53);
            this.btnImport.Tag = "Import";
            this.btnImport.Text = "&Import";
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImport.ToolTipText = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // frmImportCQMData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 368);
            this.Controls.Add(this.tls_Main);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddSourcePath);
            this.Controls.Add(this.txtPath);
            this.Name = "frmImportCQMData";
            this.Text = "frmImportCQMData";
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddSourcePath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        internal gloGlobal.gloToolStripIgnoreFocus tls_Main;
        internal System.Windows.Forms.ToolStripButton btnImport;
    }
}