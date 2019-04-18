namespace gloBilling
{
    partial class frmStandardReasonCodeSetup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStandardReasonCodeSetup));
            this.pnltls_Toolstrip = new System.Windows.Forms.Panel();
            this.tls_Notes = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnAddLine = new System.Windows.Forms.ToolStripButton();
            this.tls_btnRemoveLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_SavenClose = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlReasonCodeHdr = new System.Windows.Forms.Panel();
            this.pnlReasonCodemain = new System.Windows.Forms.Panel();
            this.lblReasonCodemain = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pnlReason = new System.Windows.Forms.Panel();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.ReasonGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnltls_Toolstrip.SuspendLayout();
            this.tls_Notes.SuspendLayout();
            this.pnlReasonCodeHdr.SuspendLayout();
            this.pnlReasonCodemain.SuspendLayout();
            this.pnlReason.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReasonGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pnltls_Toolstrip
            // 
            this.pnltls_Toolstrip.Controls.Add(this.tls_Notes);
            this.pnltls_Toolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltls_Toolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnltls_Toolstrip.Name = "pnltls_Toolstrip";
            this.pnltls_Toolstrip.Size = new System.Drawing.Size(560, 54);
            this.pnltls_Toolstrip.TabIndex = 2157;
            this.pnltls_Toolstrip.TabStop = true;
            // 
            // tls_Notes
            // 
            this.tls_Notes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Notes.BackgroundImage")));
            this.tls_Notes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Notes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Notes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnAddLine,
            this.tls_btnRemoveLine,
            this.toolStripSeparator1,
            this.tlb_SavenClose,
            this.tlb_Cancel});
            this.tls_Notes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Notes.Location = new System.Drawing.Point(0, 0);
            this.tls_Notes.Name = "tls_Notes";
            this.tls_Notes.Size = new System.Drawing.Size(560, 53);
            this.tls_Notes.TabIndex = 0;
            this.tls_Notes.TabStop = true;
            this.tls_Notes.Text = "toolStrip1";
            // 
            // tls_btnAddLine
            // 
            this.tls_btnAddLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnAddLine.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnAddLine.Image")));
            this.tls_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnAddLine.Name = "tls_btnAddLine";
            this.tls_btnAddLine.Size = new System.Drawing.Size(65, 50);
            this.tls_btnAddLine.Tag = "AddLine";
            this.tls_btnAddLine.Text = "&Add Line";
            this.tls_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnAddLine.Click += new System.EventHandler(this.tls_btnAddLine_Click);
            // 
            // tls_btnRemoveLine
            // 
            this.tls_btnRemoveLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnRemoveLine.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnRemoveLine.Image")));
            this.tls_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnRemoveLine.Name = "tls_btnRemoveLine";
            this.tls_btnRemoveLine.Size = new System.Drawing.Size(89, 50);
            this.tls_btnRemoveLine.Tag = "RemoveLine";
            this.tls_btnRemoveLine.Text = "Re&move Line";
            this.tls_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnRemoveLine.Click += new System.EventHandler(this.tls_btnRemoveLine_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            // 
            // tlb_SavenClose
            // 
            this.tlb_SavenClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_SavenClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_SavenClose.Image = ((System.Drawing.Image)(resources.GetObject("tlb_SavenClose.Image")));
            this.tlb_SavenClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_SavenClose.Name = "tlb_SavenClose";
            this.tlb_SavenClose.Size = new System.Drawing.Size(66, 50);
            this.tlb_SavenClose.Tag = "OK";
            this.tlb_SavenClose.Text = "Sa&ve&&Cls";
            this.tlb_SavenClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_SavenClose.ToolTipText = "Save and Close";
            this.tlb_SavenClose.Click += new System.EventHandler(this.tlb_SavenClose_Click);
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
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
            // pnlReasonCodeHdr
            // 
            this.pnlReasonCodeHdr.Controls.Add(this.pnlReasonCodemain);
            this.pnlReasonCodeHdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReasonCodeHdr.Location = new System.Drawing.Point(0, 54);
            this.pnlReasonCodeHdr.Name = "pnlReasonCodeHdr";
            this.pnlReasonCodeHdr.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlReasonCodeHdr.Size = new System.Drawing.Size(560, 25);
            this.pnlReasonCodeHdr.TabIndex = 2158;
            // 
            // pnlReasonCodemain
            // 
            this.pnlReasonCodemain.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlReasonCodemain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlReasonCodemain.Controls.Add(this.lblReasonCodemain);
            this.pnlReasonCodemain.Controls.Add(this.label24);
            this.pnlReasonCodemain.Controls.Add(this.label25);
            this.pnlReasonCodemain.Controls.Add(this.label26);
            this.pnlReasonCodemain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReasonCodemain.Location = new System.Drawing.Point(3, 3);
            this.pnlReasonCodemain.Name = "pnlReasonCodemain";
            this.pnlReasonCodemain.Size = new System.Drawing.Size(554, 22);
            this.pnlReasonCodemain.TabIndex = 1;
            // 
            // lblReasonCodemain
            // 
            this.lblReasonCodemain.BackColor = System.Drawing.Color.Transparent;
            this.lblReasonCodemain.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblReasonCodemain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReasonCodemain.Location = new System.Drawing.Point(1, 1);
            this.lblReasonCodemain.Name = "lblReasonCodemain";
            this.lblReasonCodemain.Size = new System.Drawing.Size(112, 21);
            this.lblReasonCodemain.TabIndex = 46;
            this.lblReasonCodemain.Text = " Reason Code";
            this.lblReasonCodemain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Location = new System.Drawing.Point(1, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(552, 1);
            this.label24.TabIndex = 59;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(553, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 22);
            this.label25.TabIndex = 58;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 22);
            this.label26.TabIndex = 57;
            // 
            // pnlReason
            // 
            this.pnlReason.BackColor = System.Drawing.Color.Transparent;
            this.pnlReason.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlReason.Controls.Add(this.pnlInternalControl);
            this.pnlReason.Controls.Add(this.ReasonGrid);
            this.pnlReason.Controls.Add(this.label23);
            this.pnlReason.Controls.Add(this.label18);
            this.pnlReason.Controls.Add(this.label4);
            this.pnlReason.Controls.Add(this.label6);
            this.pnlReason.Controls.Add(this.label7);
            this.pnlReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlReason.Location = new System.Drawing.Point(0, 79);
            this.pnlReason.Name = "pnlReason";
            this.pnlReason.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlReason.Size = new System.Drawing.Size(560, 218);
            this.pnlReason.TabIndex = 2159;
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.AutoScroll = true;
            this.pnlInternalControl.AutoSize = true;
            this.pnlInternalControl.Location = new System.Drawing.Point(39, 43);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(485, 108);
            this.pnlInternalControl.TabIndex = 27;
            this.pnlInternalControl.Visible = false;
            // 
            // ReasonGrid
            // 
            this.ReasonGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.ReasonGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.ReasonGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ReasonGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.ReasonGrid.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.ReasonGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReasonGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReasonGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ReasonGrid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.ReasonGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.ReasonGrid.Location = new System.Drawing.Point(4, 1);
            this.ReasonGrid.Name = "ReasonGrid";
            this.ReasonGrid.Rows.Count = 1;
            this.ReasonGrid.Rows.DefaultSize = 19;
            this.ReasonGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.ReasonGrid.Size = new System.Drawing.Size(552, 212);
            this.ReasonGrid.StyleInfo = resources.GetString("ReasonGrid.StyleInfo");
            this.ReasonGrid.TabIndex = 0;
            this.ReasonGrid.StartEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.ReasonGrid_StartEdit);
            this.ReasonGrid.ChangeEdit += new System.EventHandler(this.ReasonGrid_ChangeEdit);
            this.ReasonGrid.Enter += new System.EventHandler(this.ReasonGrid_Enter);
            this.ReasonGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ReasonGrid_KeyUp);
            this.ReasonGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ReasonGrid_MouseMove);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Location = new System.Drawing.Point(4, 213);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(552, 1);
            this.label23.TabIndex = 61;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(552, 1);
            this.label18.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(4, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(552, 1);
            this.label4.TabIndex = 25;
            this.label4.Text = "label4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(556, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 215);
            this.label6.TabIndex = 23;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 215);
            this.label7.TabIndex = 22;
            this.label7.Text = "label7";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.C1SuperTooltip1.ShowAlways = true;
            // 
            // frmStandardReasonCodeSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(560, 297);
            this.Controls.Add(this.pnlReason);
            this.Controls.Add(this.pnlReasonCodeHdr);
            this.Controls.Add(this.pnltls_Toolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStandardReasonCodeSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Standard Reason Code";
            this.Load += new System.EventHandler(this.frmStandardReasonCodeSetup_Load);
            this.pnltls_Toolstrip.ResumeLayout(false);
            this.pnltls_Toolstrip.PerformLayout();
            this.tls_Notes.ResumeLayout(false);
            this.tls_Notes.PerformLayout();
            this.pnlReasonCodeHdr.ResumeLayout(false);
            this.pnlReasonCodemain.ResumeLayout(false);
            this.pnlReason.ResumeLayout(false);
            this.pnlReason.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReasonGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltls_Toolstrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Notes;
        private System.Windows.Forms.ToolStripButton tls_btnAddLine;
        private System.Windows.Forms.ToolStripButton tls_btnRemoveLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlb_SavenClose;
        private System.Windows.Forms.ToolStripButton tlb_Cancel;
        private System.Windows.Forms.Panel pnlReasonCodeHdr;
        private System.Windows.Forms.Panel pnlReasonCodemain;
        private System.Windows.Forms.Label lblReasonCodemain;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel pnlReason;
        protected internal C1.Win.C1FlexGrid.C1FlexGrid ReasonGrid;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlInternalControl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}