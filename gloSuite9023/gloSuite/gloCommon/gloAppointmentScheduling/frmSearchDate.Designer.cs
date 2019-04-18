namespace gloAppointmentScheduling
{
    partial class frmSearchDate
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpDate };
            System.Windows.Forms.Control[] cntControls = { dtpDate };

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

 
            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                }
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSearchDate));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Email = new System.Windows.Forms.ToolStripButton();
            this.tsb_Fax = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsb_Help = new System.Windows.Forms.ToolStripButton();
            this.lblApp_Patient = new System.Windows.Forms.Label();
            this.cmbView = new System.Windows.Forms.ComboBox();
            this.lblApp_AppointmentType = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.pnlSearchDate = new System.Windows.Forms.Panel();
            this.lbl_BottomBorder = new System.Windows.Forms.Label();
            this.lbl_LeftBorder = new System.Windows.Forms.Label();
            this.lbl_RightBorder = new System.Windows.Forms.Label();
            this.lbl_TopBorder = new System.Windows.Forms.Label();
            this.ts_Commands.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.pnlSearchDate.SuspendLayout();
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
            this.tsb_OK,
            this.tsb_Help,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(392, 53);
            this.ts_Commands.TabIndex = 10;
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
            this.tsb_Fax.Visible = false;
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.ToolTipText = "Delete";
            this.tsb_Delete.Visible = false;
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(46, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Go To";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Go To";
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
            this.tsb_Help.Visible = false;
            // 
            // lblApp_Patient
            // 
            this.lblApp_Patient.AutoSize = true;
            this.lblApp_Patient.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_Patient.Location = new System.Drawing.Point(53, 14);
            this.lblApp_Patient.Name = "lblApp_Patient";
            this.lblApp_Patient.Size = new System.Drawing.Size(41, 14);
            this.lblApp_Patient.TabIndex = 75;
            this.lblApp_Patient.Text = "Date :";
            this.lblApp_Patient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbView
            // 
            this.cmbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbView.ForeColor = System.Drawing.Color.Black;
            this.cmbView.FormattingEnabled = true;
            this.cmbView.Location = new System.Drawing.Point(100, 38);
            this.cmbView.Name = "cmbView";
            this.cmbView.Size = new System.Drawing.Size(259, 22);
            this.cmbView.TabIndex = 2;
            // 
            // lblApp_AppointmentType
            // 
            this.lblApp_AppointmentType.AutoSize = true;
            this.lblApp_AppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.lblApp_AppointmentType.Location = new System.Drawing.Point(33, 42);
            this.lblApp_AppointmentType.Name = "lblApp_AppointmentType";
            this.lblApp_AppointmentType.Size = new System.Drawing.Size(61, 14);
            this.lblApp_AppointmentType.TabIndex = 77;
            this.lblApp_AppointmentType.Text = "Show In :";
            this.lblApp_AppointmentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDate
            // 
            this.dtpDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpDate.CustomFormat = "MM/dd/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(100, 10);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(124, 22);
            this.dtpDate.TabIndex = 1;
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
            // pnlSearchDate
            // 
            this.pnlSearchDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearchDate.Controls.Add(this.lbl_BottomBorder);
            this.pnlSearchDate.Controls.Add(this.lbl_LeftBorder);
            this.pnlSearchDate.Controls.Add(this.lbl_RightBorder);
            this.pnlSearchDate.Controls.Add(this.lbl_TopBorder);
            this.pnlSearchDate.Controls.Add(this.lblApp_Patient);
            this.pnlSearchDate.Controls.Add(this.dtpDate);
            this.pnlSearchDate.Controls.Add(this.lblApp_AppointmentType);
            this.pnlSearchDate.Controls.Add(this.cmbView);
            this.pnlSearchDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchDate.Location = new System.Drawing.Point(0, 55);
            this.pnlSearchDate.Name = "pnlSearchDate";
            this.pnlSearchDate.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearchDate.Size = new System.Drawing.Size(392, 71);
            this.pnlSearchDate.TabIndex = 0;
            // 
            // lbl_BottomBorder
            // 
            this.lbl_BottomBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBorder.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBorder.Location = new System.Drawing.Point(4, 67);
            this.lbl_BottomBorder.Name = "lbl_BottomBorder";
            this.lbl_BottomBorder.Size = new System.Drawing.Size(384, 1);
            this.lbl_BottomBorder.TabIndex = 83;
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
            this.lbl_LeftBorder.TabIndex = 82;
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
            this.lbl_RightBorder.TabIndex = 81;
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
            this.lbl_TopBorder.TabIndex = 80;
            this.lbl_TopBorder.Text = "label1";
            // 
            // frmSearchDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(392, 126);
            this.Controls.Add(this.pnlSearchDate);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchDate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Go To ...";
            this.Load += new System.EventHandler(this.frmSearchDate_Load);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.pnlSearchDate.ResumeLayout(false);
            this.pnlSearchDate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_Email;
        internal System.Windows.Forms.ToolStripButton tsb_Fax;
        internal System.Windows.Forms.ToolStripButton tsb_Delete;
        internal System.Windows.Forms.ToolStripButton tsb_Help;
        internal System.Windows.Forms.Label lblApp_Patient;
        private System.Windows.Forms.ComboBox cmbView;
        internal System.Windows.Forms.Label lblApp_AppointmentType;
        internal System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.Panel pnlSearchDate;
        private System.Windows.Forms.Label lbl_BottomBorder;
        private System.Windows.Forms.Label lbl_LeftBorder;
        private System.Windows.Forms.Label lbl_RightBorder;
        private System.Windows.Forms.Label lbl_TopBorder;
    }
}