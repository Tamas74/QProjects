namespace gloAppointmentScheduling
{
    partial class frmSearchModDelCriteria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchModDelCriteria));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Email = new System.Windows.Forms.ToolStripButton();
            this.tsb_Fax = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsb_Help = new System.Windows.Forms.ToolStripButton();
            this.rbThisOccurence = new System.Windows.Forms.RadioButton();
            this.rbSeries = new System.Windows.Forms.RadioButton();
            this.pnlSearchModDelCriteria = new System.Windows.Forms.Panel();
            this.lbl_BottomBorder = new System.Windows.Forms.Label();
            this.lbl_LeftBorder = new System.Windows.Forms.Label();
            this.lbl_RightBorder = new System.Windows.Forms.Label();
            this.lbl_TopBorder = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands.SuspendLayout();
            this.pnlSearchModDelCriteria.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentScheduling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Print,
            this.tsb_Email,
            this.tsb_Fax,
            this.tsb_Delete,
            this.tsb_Modify,
            this.tsb_Cancel,
            this.tsb_Help});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(392, 53);
            this.ts_Commands.TabIndex = 11;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.ToolTipText = "Print";
            this.tsb_Print.Visible = false;
            // 
            // tsb_Email
            // 
            this.tsb_Email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Email.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Email.Image")));
            this.tsb_Email.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Email.Name = "tsb_Email";
            this.tsb_Email.Size = new System.Drawing.Size(42, 50);
            this.tsb_Email.Text = "&Email";
            this.tsb_Email.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Email.ToolTipText = "Email";
            this.tsb_Email.Visible = false;
            // 
            // tsb_Fax
            // 
            this.tsb_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Fax.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Fax.Image")));
            this.tsb_Fax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Fax.Name = "tsb_Fax";
            this.tsb_Fax.Size = new System.Drawing.Size(36, 50);
            this.tsb_Fax.Text = "&Fax";
            this.tsb_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Fax.ToolTipText = "Fax";
            this.tsb_Fax.Visible = false;
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Tag = "OK";
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.ToolTipText = "Delete";
            this.tsb_Delete.Visible = false;
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(53, 50);
            this.tsb_Modify.Tag = "OK";
            this.tsb_Modify.Text = "&Modify";
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.ToolTipText = "Modify";
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
            this.tsb_Cancel.ToolTipText = "Close";
            // 
            // tsb_Help
            // 
            this.tsb_Help.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Help.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Help.Image")));
            this.tsb_Help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Help.Name = "tsb_Help";
            this.tsb_Help.Size = new System.Drawing.Size(38, 50);
            this.tsb_Help.Tag = "Help";
            this.tsb_Help.Text = "&Help";
            this.tsb_Help.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Help.ToolTipText = "Help";
            this.tsb_Help.Visible = false;
            // 
            // rbThisOccurence
            // 
            this.rbThisOccurence.AutoSize = true;
            this.rbThisOccurence.BackColor = System.Drawing.Color.Transparent;
            this.rbThisOccurence.Location = new System.Drawing.Point(21, 12);
            this.rbThisOccurence.Name = "rbThisOccurence";
            this.rbThisOccurence.Size = new System.Drawing.Size(96, 18);
            this.rbThisOccurence.TabIndex = 1;
            this.rbThisOccurence.TabStop = true;
            this.rbThisOccurence.Text = "radioButton1";
            this.rbThisOccurence.UseVisualStyleBackColor = false;
            this.rbThisOccurence.CheckedChanged += new System.EventHandler(this.rbThisOccurence_CheckedChanged);
            // 
            // rbSeries
            // 
            this.rbSeries.AutoSize = true;
            this.rbSeries.BackColor = System.Drawing.Color.Transparent;
            this.rbSeries.Location = new System.Drawing.Point(21, 39);
            this.rbSeries.Name = "rbSeries";
            this.rbSeries.Size = new System.Drawing.Size(96, 18);
            this.rbSeries.TabIndex = 2;
            this.rbSeries.TabStop = true;
            this.rbSeries.Text = "radioButton2";
            this.rbSeries.UseVisualStyleBackColor = false;
            this.rbSeries.CheckedChanged += new System.EventHandler(this.rbSeries_CheckedChanged);
            // 
            // pnlSearchModDelCriteria
            // 
            this.pnlSearchModDelCriteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearchModDelCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchModDelCriteria.Controls.Add(this.lbl_BottomBorder);
            this.pnlSearchModDelCriteria.Controls.Add(this.lbl_LeftBorder);
            this.pnlSearchModDelCriteria.Controls.Add(this.lbl_RightBorder);
            this.pnlSearchModDelCriteria.Controls.Add(this.lbl_TopBorder);
            this.pnlSearchModDelCriteria.Controls.Add(this.rbThisOccurence);
            this.pnlSearchModDelCriteria.Controls.Add(this.rbSeries);
            this.pnlSearchModDelCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchModDelCriteria.Location = new System.Drawing.Point(0, 55);
            this.pnlSearchModDelCriteria.Name = "pnlSearchModDelCriteria";
            this.pnlSearchModDelCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearchModDelCriteria.Size = new System.Drawing.Size(392, 71);
            this.pnlSearchModDelCriteria.TabIndex = 0;
            // 
            // lbl_BottomBorder
            // 
            this.lbl_BottomBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBorder.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBorder.Location = new System.Drawing.Point(4, 67);
            this.lbl_BottomBorder.Name = "lbl_BottomBorder";
            this.lbl_BottomBorder.Size = new System.Drawing.Size(384, 1);
            this.lbl_BottomBorder.TabIndex = 17;
            this.lbl_BottomBorder.Text = "label2";
            // 
            // lbl_LeftBorder
            // 
            this.lbl_LeftBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBorder.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBorder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBorder.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBorder.Name = "lbl_LeftBorder";
            this.lbl_LeftBorder.Size = new System.Drawing.Size(1, 64);
            this.lbl_LeftBorder.TabIndex = 16;
            this.lbl_LeftBorder.Text = "label4";
            // 
            // lbl_RightBorder
            // 
            this.lbl_RightBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBorder.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBorder.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBorder.Location = new System.Drawing.Point(388, 4);
            this.lbl_RightBorder.Name = "lbl_RightBorder";
            this.lbl_RightBorder.Size = new System.Drawing.Size(1, 64);
            this.lbl_RightBorder.TabIndex = 15;
            this.lbl_RightBorder.Text = "label3";
            // 
            // lbl_TopBorder
            // 
            this.lbl_TopBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBorder.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBorder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBorder.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBorder.Name = "lbl_TopBorder";
            this.lbl_TopBorder.Size = new System.Drawing.Size(386, 1);
            this.lbl_TopBorder.TabIndex = 14;
            this.lbl_TopBorder.Text = "label1";
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(392, 55);
            this.pnlToolStrip.TabIndex = 1;
            // 
            // frmSearchModDelCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(392, 126);
            this.Controls.Add(this.pnlSearchModDelCriteria);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchModDelCriteria";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open\\Delete";
            this.Load += new System.EventHandler(this.frmSearchModDelCriteria_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlSearchModDelCriteria.ResumeLayout(false);
            this.pnlSearchModDelCriteria.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Modify;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Email;
        internal System.Windows.Forms.ToolStripButton tsb_Fax;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        internal System.Windows.Forms.ToolStripButton tsb_Help;
        private System.Windows.Forms.RadioButton rbThisOccurence;
        private System.Windows.Forms.RadioButton rbSeries;
        private System.Windows.Forms.Panel pnlSearchModDelCriteria;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Label lbl_BottomBorder;
        private System.Windows.Forms.Label lbl_LeftBorder;
        private System.Windows.Forms.Label lbl_RightBorder;
        private System.Windows.Forms.Label lbl_TopBorder;
    }
}