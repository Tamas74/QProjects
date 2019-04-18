namespace gloBilling.gloERA
{
    partial class frmCloseDate
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
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCloseDate));
            this.ClosedDate = new System.Windows.Forms.Label();
            this.lbBillingtray = new System.Windows.Forms.Label();
            this.mskCloseDate = new System.Windows.Forms.MaskedTextBox();
            this.lblpayementtray = new System.Windows.Forms.Label();
            this.btnTraySelection = new System.Windows.Forms.Button();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pmlMain = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands.SuspendLayout();
            this.pmlMain.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClosedDate
            // 
            this.ClosedDate.AutoSize = true;
            this.ClosedDate.Location = new System.Drawing.Point(41, 41);
            this.ClosedDate.Name = "ClosedDate";
            this.ClosedDate.Size = new System.Drawing.Size(73, 14);
            this.ClosedDate.TabIndex = 0;
            this.ClosedDate.Text = "Close Date :";
            // 
            // lbBillingtray
            // 
            this.lbBillingtray.AutoEllipsis = true;
            this.lbBillingtray.BackColor = System.Drawing.Color.Transparent;
            this.lbBillingtray.Location = new System.Drawing.Point(121, 69);
            this.lbBillingtray.Name = "lbBillingtray";
            this.lbBillingtray.Size = new System.Drawing.Size(251, 14);
            this.lbBillingtray.TabIndex = 1;
            // 
            // mskCloseDate
            // 
            this.mskCloseDate.Location = new System.Drawing.Point(124, 37);
            this.mskCloseDate.Mask = "00/00/0000";
            this.mskCloseDate.Name = "mskCloseDate";
            this.mskCloseDate.Size = new System.Drawing.Size(95, 22);
            this.mskCloseDate.TabIndex = 0;
            this.mskCloseDate.ValidatingType = typeof(System.DateTime);
            // 
            // lblpayementtray
            // 
            this.lblpayementtray.AutoSize = true;
            this.lblpayementtray.Location = new System.Drawing.Point(23, 69);
            this.lblpayementtray.Name = "lblpayementtray";
            this.lblpayementtray.Size = new System.Drawing.Size(91, 14);
            this.lblpayementtray.TabIndex = 5;
            this.lblpayementtray.Text = "Payment Tray :";
            // 
            // btnTraySelection
            // 
            this.btnTraySelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTraySelection.AutoEllipsis = true;
            this.btnTraySelection.BackColor = System.Drawing.Color.Transparent;
            this.btnTraySelection.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.btnTraySelection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTraySelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraySelection.Image = ((System.Drawing.Image)(resources.GetObject("btnTraySelection.Image")));
            this.btnTraySelection.Location = new System.Drawing.Point(390, 66);
            this.btnTraySelection.Name = "btnTraySelection";
            this.btnTraySelection.Size = new System.Drawing.Size(21, 21);
            this.btnTraySelection.TabIndex = 1;
            this.btnTraySelection.TabStop = false;
            this.btnTraySelection.UseVisualStyleBackColor = false;
            this.btnTraySelection.Click += new System.EventHandler(this.btnTraySelection_Click);
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(445, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            this.tsb_OK.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // pmlMain
            // 
            this.pmlMain.Controls.Add(this.label4);
            this.pmlMain.Controls.Add(this.label3);
            this.pmlMain.Controls.Add(this.btnTraySelection);
            this.pmlMain.Controls.Add(this.lblpayementtray);
            this.pmlMain.Controls.Add(this.label2);
            this.pmlMain.Controls.Add(this.mskCloseDate);
            this.pmlMain.Controls.Add(this.label1);
            this.pmlMain.Controls.Add(this.lbBillingtray);
            this.pmlMain.Controls.Add(this.ClosedDate);
            this.pmlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pmlMain.Location = new System.Drawing.Point(0, 54);
            this.pmlMain.Name = "pmlMain";
            this.pmlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pmlMain.Size = new System.Drawing.Size(445, 137);
            this.pmlMain.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(441, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 129);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 129);
            this.label3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(439, 1);
            this.label2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(439, 1);
            this.label1.TabIndex = 0;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(445, 54);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // frmCloseDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(445, 191);
            this.Controls.Add(this.pmlMain);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCloseDate";
            this.ShowInTaskbar = false;
            this.Text = "Close Day Tray";
            this.Load += new System.EventHandler(this.frmCloseDate_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCloseDate_FormClosing);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pmlMain.ResumeLayout(false);
            this.pmlMain.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ClosedDate;
        private System.Windows.Forms.Label lbBillingtray;
        private System.Windows.Forms.MaskedTextBox mskCloseDate;
        private System.Windows.Forms.Label lblpayementtray;
        private System.Windows.Forms.Button btnTraySelection;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pmlMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlToolStrip;
    }
}