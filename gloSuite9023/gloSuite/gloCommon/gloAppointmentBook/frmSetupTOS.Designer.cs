namespace gloAppointmentBook
{
    partial class frmSetupTOS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupTOS));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tlsp_SetupResource = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_BottomBorder = new System.Windows.Forms.Label();
            this.lbl_TopBorder = new System.Windows.Forms.Label();
            this.lbl_RightBorder = new System.Windows.Forms.Label();
            this.lbl_LeftBorder = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tlsp_SetupResource.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlToolStrip.Controls.Add(this.tlsp_SetupResource);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(394, 53);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // tlsp_SetupResource
            // 
            this.tlsp_SetupResource.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_SetupResource.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.tlsp_SetupResource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_SetupResource.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_SetupResource.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsp_SetupResource.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_SetupResource.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnSave,
            this.tlsp_btnOK,
            this.tlsp_btnCancel});
            this.tlsp_SetupResource.Location = new System.Drawing.Point(0, 0);
            this.tlsp_SetupResource.Name = "tlsp_SetupResource";
            this.tlsp_SetupResource.Size = new System.Drawing.Size(394, 53);
            this.tlsp_SetupResource.TabIndex = 0;
            this.tlsp_SetupResource.Text = "toolStrip1";
            this.tlsp_SetupResource.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_SetupResource_ItemClicked);
            // 
            // tlsp_btnSave
            // 
            this.tlsp_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnSave.Image")));
            this.tlsp_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnSave.Name = "tlsp_btnSave";
            this.tlsp_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tlsp_btnSave.Tag = "Save";
            this.tlsp_btnSave.Text = "Sa&ve";
            this.tlsp_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnSave.ToolTipText = "Save";
            // 
            // tlsp_btnOK
            // 
            this.tlsp_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnOK.Image")));
            this.tlsp_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnOK.Name = "tlsp_btnOK";
            this.tlsp_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tlsp_btnOK.Tag = "OK";
            this.tlsp_btnOK.Text = "&Save&&Cls";
            this.tlsp_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnOK.ToolTipText = "Save and Close";
            // 
            // tlsp_btnCancel
            // 
            this.tlsp_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnCancel.Image")));
            this.tlsp_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnCancel.Name = "tlsp_btnCancel";
            this.tlsp_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tlsp_btnCancel.Tag = "Cancel";
            this.tlsp_btnCancel.Text = "&Close";
            this.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // txtName
            // 
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(78, 26);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(283, 22);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(29, 30);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(46, 14);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "Name :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.lbl_BottomBorder);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lbl_TopBorder);
            this.panel1.Controls.Add(this.lbl_RightBorder);
            this.panel1.Controls.Add(this.lbl_LeftBorder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(394, 75);
            this.panel1.TabIndex = 0;
            // 
            // lbl_BottomBorder
            // 
            this.lbl_BottomBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBorder.Location = new System.Drawing.Point(4, 71);
            this.lbl_BottomBorder.Name = "lbl_BottomBorder";
            this.lbl_BottomBorder.Size = new System.Drawing.Size(386, 1);
            this.lbl_BottomBorder.TabIndex = 3;
            // 
            // lbl_TopBorder
            // 
            this.lbl_TopBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBorder.Location = new System.Drawing.Point(4, 3);
            this.lbl_TopBorder.Name = "lbl_TopBorder";
            this.lbl_TopBorder.Size = new System.Drawing.Size(386, 1);
            this.lbl_TopBorder.TabIndex = 2;
            // 
            // lbl_RightBorder
            // 
            this.lbl_RightBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBorder.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBorder.Location = new System.Drawing.Point(390, 3);
            this.lbl_RightBorder.Name = "lbl_RightBorder";
            this.lbl_RightBorder.Size = new System.Drawing.Size(1, 69);
            this.lbl_RightBorder.TabIndex = 1;
            // 
            // lbl_LeftBorder
            // 
            this.lbl_LeftBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBorder.Location = new System.Drawing.Point(3, 3);
            this.lbl_LeftBorder.Name = "lbl_LeftBorder";
            this.lbl_LeftBorder.Size = new System.Drawing.Size(1, 69);
            this.lbl_LeftBorder.TabIndex = 0;
            // 
            // frmSetupTOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(394, 128);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupTOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup - Type of Service";
            this.Load += new System.EventHandler(this.frmSetupTOS_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tlsp_SetupResource.ResumeLayout(false);
            this.tlsp_SetupResource.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_SetupResource;
        private System.Windows.Forms.ToolStripButton tlsp_btnOK;
        private System.Windows.Forms.ToolStripButton tlsp_btnCancel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_BottomBorder;
        private System.Windows.Forms.Label lbl_TopBorder;
        private System.Windows.Forms.Label lbl_RightBorder;
        private System.Windows.Forms.Label lbl_LeftBorder;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
    }
}