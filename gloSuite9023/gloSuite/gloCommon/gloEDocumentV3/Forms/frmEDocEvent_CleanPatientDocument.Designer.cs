namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_CleanPatientDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_CleanPatientDocument));
            this.lblPatientCodelbl = new System.Windows.Forms.Label();
            this.lblPatientNamelbl = new System.Windows.Forms.Label();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkListYears = new System.Windows.Forms.CheckedListBox();
            this.chkYear2008 = new System.Windows.Forms.CheckBox();
            this.chkYear2007 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tls_MaintainDoc.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPatientCodelbl
            // 
            this.lblPatientCodelbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientCodelbl.AutoEllipsis = true;
            this.lblPatientCodelbl.AutoSize = true;
            this.lblPatientCodelbl.Location = new System.Drawing.Point(18, 23);
            this.lblPatientCodelbl.Name = "lblPatientCodelbl";
            this.lblPatientCodelbl.Size = new System.Drawing.Size(86, 14);
            this.lblPatientCodelbl.TabIndex = 0;
            this.lblPatientCodelbl.Text = "Patient Code :";
            // 
            // lblPatientNamelbl
            // 
            this.lblPatientNamelbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientNamelbl.AutoEllipsis = true;
            this.lblPatientNamelbl.AutoSize = true;
            this.lblPatientNamelbl.Location = new System.Drawing.Point(15, 53);
            this.lblPatientNamelbl.Name = "lblPatientNamelbl";
            this.lblPatientNamelbl.Size = new System.Drawing.Size(89, 14);
            this.lblPatientNamelbl.TabIndex = 1;
            this.lblPatientNamelbl.Text = "Patient Name :";
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.BackColor = System.Drawing.Color.White;
            this.lblPatientCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPatientCode.Location = new System.Drawing.Point(108, 20);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Size = new System.Drawing.Size(208, 20);
            this.lblPatientCode.TabIndex = 2;
            this.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatientName
            // 
            this.lblPatientName.BackColor = System.Drawing.Color.White;
            this.lblPatientName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPatientName.Location = new System.Drawing.Point(108, 50);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(208, 20);
            this.lblPatientName.TabIndex = 3;
            this.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_Cancel});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(338, 53);
            this.tls_MaintainDoc.TabIndex = 4;
            this.tls_MaintainDoc.Text = "toolStrip1";
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(66, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "&Save&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Save and Close";
            this.tlb_Ok.Click += new System.EventHandler(this.tlb_Ok_Click);
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
            this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Cancel.Name = "tlb_Cancel";
            this.tlb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tlb_Cancel.Tag = "Cancel";
            this.tlb_Cancel.Text = "&Close";
            this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Cancel.ToolTipText = "Close";
            this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.chkListYears);
            this.panel1.Controls.Add(this.chkYear2008);
            this.panel1.Controls.Add(this.chkYear2007);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblPatientCode);
            this.panel1.Controls.Add(this.lblPatientNamelbl);
            this.panel1.Controls.Add(this.lblPatientCodelbl);
            this.panel1.Controls.Add(this.lblPatientName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(338, 187);
            this.panel1.TabIndex = 20;
            // 
            // chkListYears
            // 
            this.chkListYears.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkListYears.CheckOnClick = true;
            this.chkListYears.FormattingEnabled = true;
            this.chkListYears.Location = new System.Drawing.Point(108, 83);
            this.chkListYears.Name = "chkListYears";
            this.chkListYears.Size = new System.Drawing.Size(208, 85);
            this.chkListYears.TabIndex = 28;
            // 
            // chkYear2008
            // 
            this.chkYear2008.AutoSize = true;
            this.chkYear2008.Location = new System.Drawing.Point(21, 111);
            this.chkYear2008.Margin = new System.Windows.Forms.Padding(2);
            this.chkYear2008.Name = "chkYear2008";
            this.chkYear2008.Size = new System.Drawing.Size(54, 18);
            this.chkYear2008.TabIndex = 27;
            this.chkYear2008.Text = "2008";
            this.chkYear2008.UseVisualStyleBackColor = true;
            this.chkYear2008.Visible = false;
            // 
            // chkYear2007
            // 
            this.chkYear2007.AutoSize = true;
            this.chkYear2007.Location = new System.Drawing.Point(21, 133);
            this.chkYear2007.Margin = new System.Windows.Forms.Padding(2);
            this.chkYear2007.Name = "chkYear2007";
            this.chkYear2007.Size = new System.Drawing.Size(54, 18);
            this.chkYear2007.TabIndex = 27;
            this.chkYear2007.Text = "2007";
            this.chkYear2007.UseVisualStyleBackColor = true;
            this.chkYear2007.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 14);
            this.label4.TabIndex = 26;
            this.label4.Text = "Years :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(330, 1);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(334, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 181);
            this.label1.TabIndex = 23;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 181);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tls_MaintainDoc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(338, 54);
            this.panel2.TabIndex = 28;
            // 
            // frmEDocEvent_CleanPatientDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(338, 241);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocEvent_CleanPatientDocument";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clear Patient Documents";
            this.Load += new System.EventHandler(this.frmEDocEvent_CleanPatientDocument_Load);
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPatientCodelbl;
        private System.Windows.Forms.Label lblPatientNamelbl;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Label lblPatientName;
        private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
        private System.Windows.Forms.ToolStripButton tlb_Ok;
        private System.Windows.Forms.ToolStripButton tlb_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkYear2008;
        private System.Windows.Forms.CheckBox chkYear2007;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckedListBox chkListYears;
    }
}