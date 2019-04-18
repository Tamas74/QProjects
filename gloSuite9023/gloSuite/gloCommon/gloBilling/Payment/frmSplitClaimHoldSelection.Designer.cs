namespace gloBilling.Payment
{
    partial class frmSplitClaimHoldSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplitClaimHoldSelection));
            this.pnlInsurace = new System.Windows.Forms.Panel();
            this.label53 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.c1SplitClaims = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnSaveClose = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlInsurace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitClaims)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInsurace
            // 
            this.pnlInsurace.Controls.Add(this.label53);
            this.pnlInsurace.Controls.Add(this.label51);
            this.pnlInsurace.Controls.Add(this.label50);
            this.pnlInsurace.Controls.Add(this.label38);
            this.pnlInsurace.Controls.Add(this.c1SplitClaims);
            this.pnlInsurace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInsurace.Location = new System.Drawing.Point(0, 138);
            this.pnlInsurace.Name = "pnlInsurace";
            this.pnlInsurace.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlInsurace.Size = new System.Drawing.Size(681, 362);
            this.pnlInsurace.TabIndex = 22;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Top;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Location = new System.Drawing.Point(4, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(673, 1);
            this.label53.TabIndex = 65;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Location = new System.Drawing.Point(4, 358);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(673, 1);
            this.label51.TabIndex = 64;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Right;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Location = new System.Drawing.Point(677, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 359);
            this.label50.TabIndex = 62;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Location = new System.Drawing.Point(3, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 359);
            this.label38.TabIndex = 61;
            // 
            // c1SplitClaims
            // 
            this.c1SplitClaims.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1SplitClaims.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1SplitClaims.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1SplitClaims.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1SplitClaims.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1SplitClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1SplitClaims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1SplitClaims.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1SplitClaims.Location = new System.Drawing.Point(3, 0);
            this.c1SplitClaims.Name = "c1SplitClaims";
            this.c1SplitClaims.Padding = new System.Windows.Forms.Padding(3);
            this.c1SplitClaims.Rows.Count = 1;
            this.c1SplitClaims.Rows.DefaultSize = 19;
            this.c1SplitClaims.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1SplitClaims.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1SplitClaims.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.None;
            this.c1SplitClaims.Size = new System.Drawing.Size(675, 359);
            this.c1SplitClaims.StyleInfo = resources.GetString("c1SplitClaims.StyleInfo");
            this.c1SplitClaims.TabIndex = 60;
            this.c1SplitClaims.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SplitClaims_AfterEdit);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(681, 54);
            this.pnlToolStrip.TabIndex = 23;
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnSaveClose,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(681, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnSaveClose
            // 
            this.tls_btnSaveClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnSaveClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnSaveClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnSaveClose.Image")));
            this.tls_btnSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnSaveClose.Name = "tls_btnSaveClose";
            this.tls_btnSaveClose.Size = new System.Drawing.Size(66, 50);
            this.tls_btnSaveClose.Tag = "SaveClose";
            this.tls_btnSaveClose.Text = "Sa&ve&&Cls";
            this.tls_btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnSaveClose.ToolTipText = "Save and Close";
            this.tls_btnSaveClose.Click += new System.EventHandler(this.tls_btnSaveClose_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Cancel";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(681, 84);
            this.panel1.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lblUser);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(673, 53);
            this.panel3.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(72, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 14);
            this.label5.TabIndex = 69;
            this.label5.Text = "User :";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(114, 6);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(0, 14);
            this.lblUser.TabIndex = 68;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(18, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(232, 14);
            this.label6.TabIndex = 70;
            this.label6.Text = "Which claims should remain on hold?";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblNote);
            this.panel2.Controls.Add(this.lblMessage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(673, 23);
            this.panel2.TabIndex = 71;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.BackColor = System.Drawing.Color.Transparent;
            this.lblNote.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(111, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Padding = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.lblNote.Size = new System.Drawing.Size(4, 21);
            this.lblNote.TabIndex = 66;
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(111, 23);
            this.lblMessage.TabIndex = 67;
            this.lblMessage.Text = "Hold Note :";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(673, 1);
            this.label1.TabIndex = 65;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Location = new System.Drawing.Point(4, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(673, 1);
            this.label2.TabIndex = 64;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Location = new System.Drawing.Point(677, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 78);
            this.label3.TabIndex = 62;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 78);
            this.label4.TabIndex = 61;
            // 
            // frmSplitClaimHoldSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(681, 500);
            this.Controls.Add(this.pnlInsurace);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSplitClaimHoldSelection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hold Split Claims";
            this.Load += new System.EventHandler(this.frmSplitClaimHoldSelection_Load);
            this.pnlInsurace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1SplitClaims)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInsurace;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label38;
        private C1.Win.C1FlexGrid.C1FlexGrid c1SplitClaims;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnSaveClose;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}