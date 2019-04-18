namespace gloBilling
{
    partial class frmSetupActionCodes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupActionCodes));
            this.tls_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Saveclose = new System.Windows.Forms.ToolStripButton();
            this.tsb_close = new System.Windows.Forms.ToolStripButton();
            this.pnlText = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsBlock = new System.Windows.Forms.CheckBox();
            this.chkIsSystem = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tls_SetupResource.SuspendLayout();
            this.pnlText.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_SetupResource
            // 
            this.tls_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tls_SetupResource.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_SetupResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_Saveclose,
            this.tsb_close});
            this.tls_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tls_SetupResource.Name = "tls_SetupResource";
            this.tls_SetupResource.Size = new System.Drawing.Size(349, 53);
            this.tls_SetupResource.TabIndex = 2;
            this.tls_SetupResource.TabStop = true;
            this.tls_SetupResource.Text = "toolStrip1";
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_Saveclose
            // 
            this.tsb_Saveclose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Saveclose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Saveclose.Image")));
            this.tsb_Saveclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Saveclose.Name = "tsb_Saveclose";
            this.tsb_Saveclose.Size = new System.Drawing.Size(66, 50);
            this.tsb_Saveclose.Tag = "OK";
            this.tsb_Saveclose.Text = "Sa&ve&&Cls";
            this.tsb_Saveclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Saveclose.ToolTipText = "Save and Close";
            this.tsb_Saveclose.Click += new System.EventHandler(this.tsb_Saveclose_Click);
            // 
            // tsb_close
            // 
            this.tsb_close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_close.Image")));
            this.tsb_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_close.Name = "tsb_close";
            this.tsb_close.Size = new System.Drawing.Size(43, 50);
            this.tsb_close.Tag = "Cancel";
            this.tsb_close.Text = "&Close";
            this.tsb_close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_close.Click += new System.EventHandler(this.tsb_close_Click);
            // 
            // pnlText
            // 
            this.pnlText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlText.Controls.Add(this.label4);
            this.pnlText.Controls.Add(this.chkIsBlock);
            this.pnlText.Controls.Add(this.chkIsSystem);
            this.pnlText.Controls.Add(this.label3);
            this.pnlText.Controls.Add(this.label2);
            this.pnlText.Controls.Add(this.label1);
            this.pnlText.Controls.Add(this.label59);
            this.pnlText.Controls.Add(this.txtDescription);
            this.pnlText.Controls.Add(this.lblDescription);
            this.pnlText.Controls.Add(this.txtCode);
            this.pnlText.Controls.Add(this.lblCode);
            this.pnlText.Controls.Add(this.label19);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlText.Location = new System.Drawing.Point(0, 53);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(3);
            this.pnlText.Size = new System.Drawing.Size(349, 108);
            this.pnlText.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(5, 41);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 113;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkIsBlock
            // 
            this.chkIsBlock.AutoSize = true;
            this.chkIsBlock.Location = new System.Drawing.Point(186, 70);
            this.chkIsBlock.Name = "chkIsBlock";
            this.chkIsBlock.Size = new System.Drawing.Size(81, 18);
            this.chkIsBlock.TabIndex = 112;
            this.chkIsBlock.Text = "Is blocked";
            this.chkIsBlock.UseVisualStyleBackColor = true;
            this.chkIsBlock.Visible = false;
            // 
            // chkIsSystem
            // 
            this.chkIsSystem.AutoSize = true;
            this.chkIsSystem.Location = new System.Drawing.Point(93, 70);
            this.chkIsSystem.Name = "chkIsSystem";
            this.chkIsSystem.Size = new System.Drawing.Size(77, 18);
            this.chkIsSystem.TabIndex = 111;
            this.chkIsSystem.Text = "Is system";
            this.chkIsSystem.UseVisualStyleBackColor = true;
            this.chkIsSystem.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(341, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(341, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(345, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 102);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 102);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // txtDescription
            // 
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(97, 40);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(225, 22);
            this.txtDescription.TabIndex = 4;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(17, 42);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(75, 14);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description :";
            // 
            // txtCode
            // 
            this.txtCode.ForeColor = System.Drawing.Color.Black;
            this.txtCode.Location = new System.Drawing.Point(97, 12);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(106, 22);
            this.txtCode.TabIndex = 1;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(49, 16);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(43, 14);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Code :";
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(37, 16);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmSetupActionCodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(349, 161);
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.tls_SetupResource);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupActionCodes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup Action Codes";
            this.Load += new System.EventHandler(this.frmSetupActionCodes_Load);
            this.tls_SetupResource.ResumeLayout(false);
            this.tls_SetupResource.PerformLayout();
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_SetupResource;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton tsb_Saveclose;
        private System.Windows.Forms.ToolStripButton tsb_close;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.CheckBox chkIsBlock;
        private System.Windows.Forms.CheckBox chkIsSystem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        internal System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label4;
    }
}