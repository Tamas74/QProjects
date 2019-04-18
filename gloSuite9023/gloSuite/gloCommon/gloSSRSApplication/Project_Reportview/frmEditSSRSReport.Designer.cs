namespace Project_Reportview
{
    partial class frmEditSSRSReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditSSRSReport));
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btn_SaveCls = new System.Windows.Forms.ToolStripButton();
            this.tls_btn_Cancel = new System.Windows.Forms.ToolStripButton();
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtReportName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ClearFile = new System.Windows.Forms.Button();
            this.chk_Submenu = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btn_SaveCls,
            this.tls_btn_Cancel});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(415, 53);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tls_btn_SaveCls
            // 
            this.tls_btn_SaveCls.Image = ((System.Drawing.Image)(resources.GetObject("tls_btn_SaveCls.Image")));
            this.tls_btn_SaveCls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btn_SaveCls.Name = "tls_btn_SaveCls";
            this.tls_btn_SaveCls.Size = new System.Drawing.Size(66, 50);
            this.tls_btn_SaveCls.Text = "&Save&&Cls";
            this.tls_btn_SaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btn_SaveCls.ToolTipText = "Save and Close";
            this.tls_btn_SaveCls.Click += new System.EventHandler(this.tls_btn_SaveCls_Click);
            // 
            // tls_btn_Cancel
            // 
            this.tls_btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btn_Cancel.Image")));
            this.tls_btn_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btn_Cancel.Name = "tls_btn_Cancel";
            this.tls_btn_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btn_Cancel.Text = "&Close";
            this.tls_btn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btn_Cancel.Click += new System.EventHandler(this.tls_btn_Cancel_Click);
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_OpenFile.BackgroundImage")));
            this.btn_OpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_OpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OpenFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btn_OpenFile.Image")));
            this.btn_OpenFile.Location = new System.Drawing.Point(318, 54);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(22, 22);
            this.btn_OpenFile.TabIndex = 3;
            this.btn_OpenFile.UseVisualStyleBackColor = true;
            this.btn_OpenFile.Click += new System.EventHandler(this.btn_OpenFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.White;
            this.txtFileName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(125, 54);
            this.txtFileName.MaxLength = 100;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(189, 22);
            this.txtFileName.TabIndex = 2;
            // 
            // txtReportName
            // 
            this.txtReportName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportName.Location = new System.Drawing.Point(125, 22);
            this.txtReportName.MaxLength = 80;
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Size = new System.Drawing.Size(189, 22);
            this.txtReportName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "Report File Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "Report Name :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_ClearFile);
            this.panel1.Controls.Add(this.chk_Submenu);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtFileName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_OpenFile);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtReportName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(415, 103);
            this.panel1.TabIndex = 0;
            // 
            // btn_ClearFile
            // 
            this.btn_ClearFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ClearFile.BackgroundImage")));
            this.btn_ClearFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ClearFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ClearFile.Image = ((System.Drawing.Image)(resources.GetObject("btn_ClearFile.Image")));
            this.btn_ClearFile.Location = new System.Drawing.Point(344, 54);
            this.btn_ClearFile.Name = "btn_ClearFile";
            this.btn_ClearFile.Size = new System.Drawing.Size(22, 22);
            this.btn_ClearFile.TabIndex = 16;
            this.btn_ClearFile.UseVisualStyleBackColor = true;
            this.btn_ClearFile.Click += new System.EventHandler(this.btn_ClearFile_Click);
            // 
            // chk_Submenu
            // 
            this.chk_Submenu.AutoSize = true;
            this.chk_Submenu.Location = new System.Drawing.Point(320, 23);
            this.chk_Submenu.Name = "chk_Submenu";
            this.chk_Submenu.Size = new System.Drawing.Size(81, 18);
            this.chk_Submenu.TabIndex = 1;
            this.chk_Submenu.Text = "Sub Menu";
            this.chk_Submenu.UseVisualStyleBackColor = true;
            this.chk_Submenu.CheckedChanged += new System.EventHandler(this.chk_Submenu_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(411, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 95);
            this.label6.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 95);
            this.label5.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(409, 1);
            this.label4.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(409, 1);
            this.label3.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 54);
            this.panel2.TabIndex = 1;
            // 
            // frmEditSSRSReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(415, 157);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditSSRSReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SSRS Report";
            this.Load += new System.EventHandler(this.frmEditSSRSReport_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tls_btn_SaveCls;
        private System.Windows.Forms.ToolStripButton tls_btn_Cancel;
        private System.Windows.Forms.Button btn_OpenFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtReportName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chk_Submenu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_ClearFile;
    }
}