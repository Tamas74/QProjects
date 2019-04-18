namespace gloAppointmentBook
{
    partial class frmSetupResourceType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupResourceType));
            this.tlsp_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lbl_Bottom = new System.Windows.Forms.Label();
            this.lbl_Left = new System.Windows.Forms.Label();
            this.lbl_Right = new System.Windows.Forms.Label();
            this.lbl_Top = new System.Windows.Forms.Label();
            this.txtResourceType = new System.Windows.Forms.TextBox();
            this.lblResourceType = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tlsp_Commands.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlsp_Commands
            // 
            this.tlsp_Commands.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_Commands.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.tlsp_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnSave,
            this.tlsp_btnOK,
            this.tlsp_btnCancel});
            this.tlsp_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_Commands.Location = new System.Drawing.Point(0, 0);
            this.tlsp_Commands.Name = "tlsp_Commands";
            this.tlsp_Commands.Size = new System.Drawing.Size(394, 53);
            this.tlsp_Commands.TabIndex = 9;
            this.tlsp_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
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
            this.tlsp_btnCancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnCancel.ToolTipText = "Close";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.lbl_Bottom);
            this.pnlSearch.Controls.Add(this.lbl_Left);
            this.pnlSearch.Controls.Add(this.lbl_Right);
            this.pnlSearch.Controls.Add(this.lbl_Top);
            this.pnlSearch.Controls.Add(this.txtResourceType);
            this.pnlSearch.Controls.Add(this.lblResourceType);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 53);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearch.Size = new System.Drawing.Size(394, 75);
            this.pnlSearch.TabIndex = 0;
            // 
            // lbl_Bottom
            // 
            this.lbl_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_Bottom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_Bottom.Location = new System.Drawing.Point(4, 71);
            this.lbl_Bottom.Name = "lbl_Bottom";
            this.lbl_Bottom.Size = new System.Drawing.Size(386, 1);
            this.lbl_Bottom.TabIndex = 21;
            this.lbl_Bottom.Text = "label2";
            // 
            // lbl_Left
            // 
            this.lbl_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Left.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Left.Location = new System.Drawing.Point(3, 4);
            this.lbl_Left.Name = "lbl_Left";
            this.lbl_Left.Size = new System.Drawing.Size(1, 68);
            this.lbl_Left.TabIndex = 20;
            this.lbl_Left.Text = "label4";
            // 
            // lbl_Right
            // 
            this.lbl_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_Right.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_Right.Location = new System.Drawing.Point(390, 4);
            this.lbl_Right.Name = "lbl_Right";
            this.lbl_Right.Size = new System.Drawing.Size(1, 68);
            this.lbl_Right.TabIndex = 19;
            this.lbl_Right.Text = "label3";
            // 
            // lbl_Top
            // 
            this.lbl_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Top.Location = new System.Drawing.Point(3, 3);
            this.lbl_Top.Name = "lbl_Top";
            this.lbl_Top.Size = new System.Drawing.Size(388, 1);
            this.lbl_Top.TabIndex = 18;
            this.lbl_Top.Text = "label1";
            // 
            // txtResourceType
            // 
            this.txtResourceType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResourceType.ForeColor = System.Drawing.Color.Black;
            this.txtResourceType.Location = new System.Drawing.Point(109, 26);
            this.txtResourceType.Name = "txtResourceType";
            this.txtResourceType.Size = new System.Drawing.Size(273, 22);
            this.txtResourceType.TabIndex = 1;
            this.txtResourceType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResourceType_KeyPress);
            // 
            // lblResourceType
            // 
            this.lblResourceType.AutoSize = true;
            this.lblResourceType.BackColor = System.Drawing.Color.Transparent;
            this.lblResourceType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResourceType.Location = new System.Drawing.Point(10, 30);
            this.lblResourceType.Name = "lblResourceType";
            this.lblResourceType.Size = new System.Drawing.Size(97, 14);
            this.lblResourceType.TabIndex = 6;
            this.lblResourceType.Text = "Resource Type :";
            this.lblResourceType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tlsp_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(394, 53);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // frmSetupResourceType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(394, 128);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupResourceType";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resource Type";
            this.Load += new System.EventHandler(this.frmSetupResourceType_Load);
            this.tlsp_Commands.ResumeLayout(false);
            this.tlsp_Commands.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus tlsp_Commands;
        internal System.Windows.Forms.ToolStripButton tlsp_btnOK;
        internal System.Windows.Forms.ToolStripButton tlsp_btnCancel;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblResourceType;
        private System.Windows.Forms.TextBox txtResourceType;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Label lbl_Bottom;
        private System.Windows.Forms.Label lbl_Left;
        private System.Windows.Forms.Label lbl_Right;
        private System.Windows.Forms.Label lbl_Top;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
    }
}