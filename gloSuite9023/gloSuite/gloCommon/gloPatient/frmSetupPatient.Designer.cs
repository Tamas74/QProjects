namespace gloPatient
{
    partial class frmSetupPatient
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
                //try
                //{
                //    if (PrintDialog1 != null)
                //    {
                //        PrintDialog1.Dispose();
                //        PrintDialog1 = null;
                //    }
                //}
                //catch
                //{
                //}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupPatient));
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnDemoHx = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_ScanPatient = new System.Windows.Forms.ToolStripButton();
            this.tsb_Email = new System.Windows.Forms.ToolStripButton();
            this.tsb_Fax = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Help = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveAsCopy = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlTOP = new System.Windows.Forms.Panel();
            //this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
            this.pnlLast = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ts_Commands.SuspendLayout();
            this.pnlTOP.SuspendLayout();
            this.pnlLast.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlContainer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(784, 736);
            this.pnlContainer.TabIndex = 18;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloPatient.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnDemoHx,
            this.tsb_Print,
            this.tsb_ScanPatient,
            this.tsb_Email,
            this.tsb_Fax,
            this.tsb_Delete,
            this.tsb_Help,
            this.tsb_SaveAsCopy,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(778, 53);
            this.ts_Commands.TabIndex = 21;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // ts_btnDemoHx
            // 
            this.ts_btnDemoHx.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnDemoHx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnDemoHx.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnDemoHx.Image")));
            this.ts_btnDemoHx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnDemoHx.Name = "ts_btnDemoHx";
            this.ts_btnDemoHx.Size = new System.Drawing.Size(66, 50);
            this.ts_btnDemoHx.Tag = "DemoHx";
            this.ts_btnDemoHx.Text = "&Demo Hx";
            this.ts_btnDemoHx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnDemoHx.ToolTipText = "Patient Record Change History";
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(53, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = " &Print  ";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_ScanPatient
            // 
            this.tsb_ScanPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ScanPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ScanPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ScanPatient.Image")));
            this.tsb_ScanPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ScanPatient.Name = "tsb_ScanPatient";
            this.tsb_ScanPatient.Size = new System.Drawing.Size(83, 50);
            this.tsb_ScanPatient.Tag = "ScanPatient";
            this.tsb_ScanPatient.Text = "Sc&an Pat.ID";
            this.tsb_ScanPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ScanPatient.ToolTipText = "Scan Patient ID";
            // 
            // tsb_Email
            // 
            this.tsb_Email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Email.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Email.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Email.Image")));
            this.tsb_Email.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Email.Name = "tsb_Email";
            this.tsb_Email.Size = new System.Drawing.Size(58, 50);
            this.tsb_Email.Text = "  &Email  ";
            this.tsb_Email.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Email.Visible = false;
            // 
            // tsb_Fax
            // 
            this.tsb_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Fax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Fax.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Fax.Image")));
            this.tsb_Fax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Fax.Name = "tsb_Fax";
            this.tsb_Fax.Size = new System.Drawing.Size(47, 50);
            this.tsb_Fax.Text = "  &Fax  ";
            this.tsb_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Fax.Visible = false;
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(66, 50);
            this.tsb_Delete.Text = "  &Delete  ";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.Visible = false;
            // 
            // tsb_Help
            // 
            this.tsb_Help.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Help.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Help.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Help.Image")));
            this.tsb_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Help.Name = "tsb_Help";
            this.tsb_Help.Size = new System.Drawing.Size(54, 50);
            this.tsb_Help.Text = "  &Help  ";
            this.tsb_Help.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Help.Visible = false;
            // 
            // tsb_SaveAsCopy
            // 
            this.tsb_SaveAsCopy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveAsCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_SaveAsCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveAsCopy.Image")));
            this.tsb_SaveAsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveAsCopy.Name = "tsb_SaveAsCopy";
            this.tsb_SaveAsCopy.Size = new System.Drawing.Size(91, 50);
            this.tsb_SaveAsCopy.Tag = "SaveAsCopy";
            this.tsb_SaveAsCopy.Text = "C&opy Patient";
            this.tsb_SaveAsCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveAsCopy.ToolTipText = "Copy Patient";
            this.tsb_SaveAsCopy.Visible = false;
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // pnlTOP
            // 
            this.pnlTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTOP.Controls.Add(this.ts_Commands);
            this.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTOP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTOP.Location = new System.Drawing.Point(0, 0);
            this.pnlTOP.Name = "pnlTOP";
            this.pnlTOP.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlTOP.Size = new System.Drawing.Size(784, 56);
            this.pnlTOP.TabIndex = 22;
            // 
            // PrintDialog1
            // 
            //this.PrintDialog1.UseEXDialog = true;
            // 
            // pnlLast
            // 
            this.pnlLast.AutoScroll = true;
            this.pnlLast.BackColor = System.Drawing.Color.Transparent;
            this.pnlLast.Controls.Add(this.panel1);
            this.pnlLast.Controls.Add(this.pnlContainer);
            this.pnlLast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLast.Location = new System.Drawing.Point(0, 56);
            this.pnlLast.Name = "pnlLast";
            this.pnlLast.Size = new System.Drawing.Size(784, 736);
            this.pnlLast.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 736);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 0);
            this.panel1.TabIndex = 0;
            // 
            // frmSetupPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(784, 792);
            this.Controls.Add(this.pnlLast);
            this.Controls.Add(this.pnlTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupPatient";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Patient Registration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSetupPatient_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSetupPatient_FormClosed);
            this.Load += new System.EventHandler(this.frmSetupPatient_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlTOP.ResumeLayout(false);
            this.pnlTOP.PerformLayout();
            this.pnlLast.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Email;
        internal System.Windows.Forms.ToolStripButton tsb_Fax;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        internal System.Windows.Forms.ToolStripButton tsb_Help;
        private System.Windows.Forms.Panel pnlTOP;
        internal System.Windows.Forms.ToolStripButton ts_btnDemoHx;
        //private System.Windows.Forms.PrintDialog PrintDialog1;
        internal System.Windows.Forms.ToolStripButton tsb_ScanPatient;
        internal System.Windows.Forms.ToolStripButton tsb_SaveAsCopy;
        private System.Windows.Forms.Panel pnlLast;
        private System.Windows.Forms.Panel panel1;
    }
}