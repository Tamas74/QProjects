namespace gloBilling
{
    partial class frmSetupClaimCategoryType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupClaimCategoryType));
            this.panel2 = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveCls = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.chkDeactivate = new System.Windows.Forms.CheckBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtClaimCategoryCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAdjustmentType = new System.Windows.Forms.Label();
            this.txtClaimCategoryDesc = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ts_Commands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(373, 55);
            this.panel2.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_SaveCls,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(373, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
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
            // tsb_SaveCls
            // 
            this.tsb_SaveCls.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveCls.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveCls.Image")));
            this.tsb_SaveCls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveCls.Name = "tsb_SaveCls";
            this.tsb_SaveCls.Size = new System.Drawing.Size(66, 50);
            this.tsb_SaveCls.Tag = "OK";
            this.tsb_SaveCls.Text = "Sa&ve&&Cls";
            this.tsb_SaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveCls.ToolTipText = "Save and Close";
            this.tsb_SaveCls.Click += new System.EventHandler(this.tsb_OK_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Cancel";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.chkDeactivate);
            this.pnlSearch.Controls.Add(this.lblCode);
            this.pnlSearch.Controls.Add(this.txtClaimCategoryCode);
            this.pnlSearch.Controls.Add(this.label4);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.lblAdjustmentType);
            this.pnlSearch.Controls.Add(this.txtClaimCategoryDesc);
            this.pnlSearch.Controls.Add(this.label12);
            this.pnlSearch.Controls.Add(this.label19);
            this.pnlSearch.Controls.Add(this.label5);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 55);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSearch.Size = new System.Drawing.Size(373, 142);
            this.pnlSearch.TabIndex = 0;
            // 
            // chkDeactivate
            // 
            this.chkDeactivate.AutoSize = true;
            this.chkDeactivate.Checked = true;
            this.chkDeactivate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeactivate.Location = new System.Drawing.Point(104, 113);
            this.chkDeactivate.Name = "chkDeactivate";
            this.chkDeactivate.Size = new System.Drawing.Size(60, 18);
            this.chkDeactivate.TabIndex = 3;
            this.chkDeactivate.Text = "Active";
            this.chkDeactivate.UseVisualStyleBackColor = true;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(58, 16);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(43, 14);
            this.lblCode.TabIndex = 20;
            this.lblCode.Text = "Code :";
            // 
            // txtClaimCategoryCode
            // 
            this.txtClaimCategoryCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimCategoryCode.ForeColor = System.Drawing.Color.Black;
            this.txtClaimCategoryCode.Location = new System.Drawing.Point(104, 12);
            this.txtClaimCategoryCode.MaxLength = 50;
            this.txtClaimCategoryCode.Name = "txtClaimCategoryCode";
            this.txtClaimCategoryCode.Size = new System.Drawing.Size(257, 22);
            this.txtClaimCategoryCode.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 135);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(369, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 135);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(367, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(367, 1);
            this.label1.TabIndex = 0;
            // 
            // lblAdjustmentType
            // 
            this.lblAdjustmentType.AutoSize = true;
            this.lblAdjustmentType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustmentType.Location = new System.Drawing.Point(26, 43);
            this.lblAdjustmentType.Name = "lblAdjustmentType";
            this.lblAdjustmentType.Size = new System.Drawing.Size(75, 14);
            this.lblAdjustmentType.TabIndex = 18;
            this.lblAdjustmentType.Text = "Description :";
            // 
            // txtClaimCategoryDesc
            // 
            this.txtClaimCategoryDesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimCategoryDesc.ForeColor = System.Drawing.Color.Black;
            this.txtClaimCategoryDesc.Location = new System.Drawing.Point(104, 42);
            this.txtClaimCategoryDesc.MaxLength = 255;
            this.txtClaimCategoryDesc.Multiline = true;
            this.txtClaimCategoryDesc.Name = "txtClaimCategoryDesc";
            this.txtClaimCategoryDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClaimCategoryDesc.Size = new System.Drawing.Size(257, 63);
            this.txtClaimCategoryDesc.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(367, 2);
            this.label12.TabIndex = 14;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(47, 16);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 111;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(14, 43);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(14, 14);
            this.label5.TabIndex = 112;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmSetupClaimCategoryType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(373, 197);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupClaimCategoryType";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Claim Reporting Category";
            this.Load += new System.EventHandler(this.frmSetClaimCategoryType_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_SaveCls;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAdjustmentType;
        private System.Windows.Forms.TextBox txtClaimCategoryDesc;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtClaimCategoryCode;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.CheckBox chkDeactivate;
    }
}