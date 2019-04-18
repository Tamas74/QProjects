namespace gloAccountsV2
{
    partial class frmPatientClaimsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientClaimsList));
            this.pnlPatientClaimGrid = new System.Windows.Forms.Panel();
            this.c1PatientClaimGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlToolstrip = new System.Windows.Forms.Panel();
            this.tlsMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_btnTransfer = new System.Windows.Forms.ToolStripButton();
            this.tsb_btnClose = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPatientInsurances = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlPatientClaimGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientClaimGrid)).BeginInit();
            this.pnlToolstrip.SuspendLayout();
            this.tlsMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPatientClaimGrid
            // 
            this.pnlPatientClaimGrid.Controls.Add(this.c1PatientClaimGrid);
            this.pnlPatientClaimGrid.Controls.Add(this.label1);
            this.pnlPatientClaimGrid.Controls.Add(this.label2);
            this.pnlPatientClaimGrid.Controls.Add(this.label59);
            this.pnlPatientClaimGrid.Controls.Add(this.label3);
            this.pnlPatientClaimGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientClaimGrid.Location = new System.Drawing.Point(0, 95);
            this.pnlPatientClaimGrid.Name = "pnlPatientClaimGrid";
            this.pnlPatientClaimGrid.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPatientClaimGrid.Size = new System.Drawing.Size(795, 364);
            this.pnlPatientClaimGrid.TabIndex = 1;
            // 
            // c1PatientClaimGrid
            // 
            this.c1PatientClaimGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientClaimGrid.AllowMergingFixed = C1.Win.C1FlexGrid.AllowMergingEnum.None;
            this.c1PatientClaimGrid.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1PatientClaimGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1PatientClaimGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientClaimGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientClaimGrid.ColumnInfo = "1,0,0,0,0,105,Columns:0{Name:\"bIsSelect\";Caption:\"Select\";Visible:False;AllowMerg" +
    "ing:True;Style:\"DataType:System.Boolean;TextAlign:LeftCenter;ImageAlign:CenterCe" +
    "nter;\";}\t";
            this.c1PatientClaimGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientClaimGrid.ExtendLastCol = true;
            this.c1PatientClaimGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientClaimGrid.Location = new System.Drawing.Point(4, 4);
            this.c1PatientClaimGrid.Name = "c1PatientClaimGrid";
            this.c1PatientClaimGrid.Padding = new System.Windows.Forms.Padding(3);
            this.c1PatientClaimGrid.Rows.Count = 1;
            this.c1PatientClaimGrid.Rows.DefaultSize = 21;
            this.c1PatientClaimGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientClaimGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1PatientClaimGrid.Size = new System.Drawing.Size(787, 356);
            this.c1PatientClaimGrid.StyleInfo = resources.GetString("c1PatientClaimGrid.StyleInfo");
            this.c1PatientClaimGrid.TabIndex = 0;
            this.c1PatientClaimGrid.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientClaimGrid_AfterEdit);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(791, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 356);
            this.label1.TabIndex = 27;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(788, 1);
            this.label2.TabIndex = 28;
            this.label2.Text = "label2";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 357);
            this.label59.TabIndex = 26;
            this.label59.Text = "label59";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(789, 1);
            this.label3.TabIndex = 29;
            this.label3.Text = "label3";
            // 
            // pnlToolstrip
            // 
            this.pnlToolstrip.AutoSize = true;
            this.pnlToolstrip.Controls.Add(this.tlsMain);
            this.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolstrip.Name = "pnlToolstrip";
            this.pnlToolstrip.Size = new System.Drawing.Size(795, 53);
            this.pnlToolstrip.TabIndex = 2;
            // 
            // tlsMain
            // 
            this.tlsMain.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tlsMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_btnTransfer,
            this.tsb_btnClose});
            this.tlsMain.Location = new System.Drawing.Point(0, 0);
            this.tlsMain.Name = "tlsMain";
            this.tlsMain.Size = new System.Drawing.Size(795, 53);
            this.tlsMain.TabIndex = 0;
            this.tlsMain.TabStop = true;
            this.tlsMain.Text = "toolStrip1";
            // 
            // tsb_btnTransfer
            // 
            this.tsb_btnTransfer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnTransfer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnTransfer.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnTransfer.Image")));
            this.tsb_btnTransfer.Name = "tsb_btnTransfer";
            this.tsb_btnTransfer.Size = new System.Drawing.Size(61, 50);
            this.tsb_btnTransfer.Tag = "Transfer";
            this.tsb_btnTransfer.Text = "&Transfer";
            this.tsb_btnTransfer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnTransfer.ToolTipText = "Transfer";
            this.tsb_btnTransfer.Click += new System.EventHandler(this.tsb_btnTransfer_Click);
            // 
            // tsb_btnClose
            // 
            this.tsb_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_btnClose.Image")));
            this.tsb_btnClose.Name = "tsb_btnClose";
            this.tsb_btnClose.Size = new System.Drawing.Size(43, 50);
            this.tsb_btnClose.Tag = "Close";
            this.tsb_btnClose.Text = "&Close";
            this.tsb_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_btnClose.ToolTipText = "Close";
            this.tsb_btnClose.Click += new System.EventHandler(this.tsb_btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.cmbPatientInsurances);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel2.Size = new System.Drawing.Size(795, 42);
            this.panel2.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Location = new System.Drawing.Point(13, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 14);
            this.label8.TabIndex = 31;
            this.label8.Text = "Patient Insurance(s) :";
            // 
            // cmbPatientInsurances
            // 
            this.cmbPatientInsurances.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatientInsurances.FormattingEnabled = true;
            this.cmbPatientInsurances.Location = new System.Drawing.Point(144, 10);
            this.cmbPatientInsurances.Name = "cmbPatientInsurances";
            this.cmbPatientInsurances.Size = new System.Drawing.Size(395, 22);
            this.cmbPatientInsurances.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(791, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 37);
            this.label4.TabIndex = 27;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(4, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(788, 1);
            this.label5.TabIndex = 28;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 38);
            this.label6.TabIndex = 26;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(789, 1);
            this.label7.TabIndex = 29;
            this.label7.Text = "label7";
            // 
            // frmPatientClaimsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(795, 459);
            this.Controls.Add(this.pnlPatientClaimGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPatientClaimsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Responsibility";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPatientClaimsList_FormClosed);
            this.Load += new System.EventHandler(this.frmPatientClaimsList_Load);
            this.pnlPatientClaimGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientClaimGrid)).EndInit();
            this.pnlToolstrip.ResumeLayout(false);
            this.pnlToolstrip.PerformLayout();
            this.tlsMain.ResumeLayout(false);
            this.tlsMain.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlPatientClaimGrid;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientClaimGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlToolstrip;
        private gloGlobal.gloToolStripIgnoreFocus tlsMain;
        internal System.Windows.Forms.ToolStripButton tsb_btnTransfer;
        internal System.Windows.Forms.ToolStripButton tsb_btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPatientInsurances;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}