namespace gloPMGeneral
{
    partial class frmShowPriorAuthorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowPriorAuthorization));
            this.pnlText = new System.Windows.Forms.Panel();
            this.c1ProirAuthorization = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.lblPatientName1 = new System.Windows.Forms.Label();
            this.lblInPutDate = new System.Windows.Forms.Label();
            this.lblPtN = new System.Windows.Forms.Label();
            this.pnltlsStrip = new System.Windows.Forms.Panel();
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_ADD = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ProirAuthorization)).BeginInit();
            this.pnltlsStrip.SuspendLayout();
            this.tls_SetupResource.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.c1ProirAuthorization);
            this.pnlText.Controls.Add(this.label3);
            this.pnlText.Controls.Add(this.label2);
            this.pnlText.Controls.Add(this.label1);
            this.pnlText.Controls.Add(this.label59);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.Location = new System.Drawing.Point(0, 98);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlText.Size = new System.Drawing.Size(756, 355);
            this.pnlText.TabIndex = 0;
            // 
            // c1ProirAuthorization
            // 
            this.c1ProirAuthorization.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ProirAuthorization.AutoResize = false;
            this.c1ProirAuthorization.BackColor = System.Drawing.Color.White;
            this.c1ProirAuthorization.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ProirAuthorization.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1ProirAuthorization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ProirAuthorization.ExtendLastCol = true;
            this.c1ProirAuthorization.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ProirAuthorization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ProirAuthorization.Location = new System.Drawing.Point(4, 1);
            this.c1ProirAuthorization.Name = "c1ProirAuthorization";
            this.c1ProirAuthorization.Padding = new System.Windows.Forms.Padding(3);
            this.c1ProirAuthorization.Rows.Count = 1;
            this.c1ProirAuthorization.Rows.DefaultSize = 19;
            this.c1ProirAuthorization.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ProirAuthorization.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1ProirAuthorization.ShowCellLabels = true;
            this.c1ProirAuthorization.Size = new System.Drawing.Size(748, 350);
            this.c1ProirAuthorization.StyleInfo = resources.GetString("c1ProirAuthorization.StyleInfo");
            this.c1ProirAuthorization.TabIndex = 31;
            this.c1ProirAuthorization.DoubleClick += new System.EventHandler(this.c1ProirAuthorization_DoubleClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(748, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(748, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(752, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 352);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 352);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // lblPatientName1
            // 
            this.lblPatientName1.AutoSize = true;
            this.lblPatientName1.Location = new System.Drawing.Point(82, 14);
            this.lblPatientName1.Name = "lblPatientName1";
            this.lblPatientName1.Size = new System.Drawing.Size(0, 14);
            this.lblPatientName1.TabIndex = 30;
            // 
            // lblInPutDate
            // 
            this.lblInPutDate.AutoSize = true;
            this.lblInPutDate.Location = new System.Drawing.Point(511, 15);
            this.lblInPutDate.Name = "lblInPutDate";
            this.lblInPutDate.Size = new System.Drawing.Size(0, 14);
            this.lblInPutDate.TabIndex = 2;
            this.lblInPutDate.Visible = false;
            // 
            // lblPtN
            // 
            this.lblPtN.AutoSize = true;
            this.lblPtN.Location = new System.Drawing.Point(23, 14);
            this.lblPtN.Name = "lblPtN";
            this.lblPtN.Size = new System.Drawing.Size(54, 14);
            this.lblPtN.TabIndex = 0;
            this.lblPtN.Text = "Patient :";
            // 
            // pnltlsStrip
            // 
            this.pnltlsStrip.BackgroundImage = global::gloPMGeneral.Properties.Resources.Img_Toolstrip;
            this.pnltlsStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltlsStrip.Controls.Add(this.tls_SetupResource);
            this.pnltlsStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltlsStrip.Location = new System.Drawing.Point(0, 0);
            this.pnltlsStrip.Name = "pnltlsStrip";
            this.pnltlsStrip.Size = new System.Drawing.Size(756, 54);
            this.pnltlsStrip.TabIndex = 1;
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ADD,
            this.tsb_Modify,
            this.tsb_Save,
            this.toolStripButton2});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(756, 53);
            this.tls_SetupResource.TabIndex = 0;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // tsb_ADD
            // 
            this.tsb_ADD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ADD.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ADD.Image")));
            this.tsb_ADD.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_ADD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ADD.Name = "tsb_ADD";
            this.tsb_ADD.Size = new System.Drawing.Size(37, 50);
            this.tsb_ADD.Tag = "Add";
            this.tsb_ADD.Text = "&New";
            this.tsb_ADD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ADD.Click += new System.EventHandler(this.tsb_ADD_Click);
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(53, 50);
            this.tsb_Modify.Tag = "Modify";
            this.tsb_Modify.Text = "&Modify";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "OK";
            this.tsb_Save.Text = "Sa&ve&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(43, 50);
            this.toolStripButton2.Tag = "Cancel";
            this.toolStripButton2.Text = "&Close";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblPtN);
            this.panel1.Controls.Add(this.lblPatientName1);
            this.panel1.Controls.Add(this.lblInPutDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(756, 44);
            this.panel1.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(466, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 14);
            this.label9.TabIndex = 36;
            this.label9.Text = "As of :";
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(610, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 14);
            this.label8.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(4, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(748, 1);
            this.label7.TabIndex = 34;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(4, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(748, 1);
            this.label6.TabIndex = 33;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(752, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 38);
            this.label5.TabIndex = 32;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 38);
            this.label4.TabIndex = 31;
            this.label4.Text = "label4";
            // 
            // frmShowPriorAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(756, 453);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnltlsStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowPriorAuthorization";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prior Authorization";
            this.Load += new System.EventHandler(this.frmShowPriorAuthorization_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmShowPriorAuthorization_FormClosing);
            this.pnlText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ProirAuthorization)).EndInit();
            this.pnltlsStrip.ResumeLayout(false);
            this.pnltlsStrip.PerformLayout();
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.Panel pnltlsStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        private System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label lblInPutDate;
        private System.Windows.Forms.Label lblPtN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        internal System.Windows.Forms.ToolStripButton tsb_ADD;
        internal System.Windows.Forms.ToolStripButton tsb_Modify;
        private System.Windows.Forms.Label lblPatientName1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ProirAuthorization;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}