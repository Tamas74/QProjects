namespace gloPatient
{
    partial class frmvwRecentPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmvwRecentPatient));
            this.C1viewPat = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.panelC1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.C1viewPat)).BeginInit();
            this.panelC1.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // C1viewPat
            // 
            this.C1viewPat.AllowEditing = false;
            this.C1viewPat.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.C1viewPat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.C1viewPat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.C1viewPat.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1viewPat.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.C1viewPat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1viewPat.ExtendLastCol = true;
            this.C1viewPat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1viewPat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1viewPat.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.C1viewPat.Location = new System.Drawing.Point(4, 4);
            this.C1viewPat.Name = "C1viewPat";
            this.C1viewPat.Rows.Count = 1;
            this.C1viewPat.Rows.DefaultSize = 19;
            this.C1viewPat.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1viewPat.Size = new System.Drawing.Size(837, 345);
            this.C1viewPat.StyleInfo = resources.GetString("C1viewPat.StyleInfo");
            this.C1viewPat.TabIndex = 0;
            this.C1viewPat.DoubleClick += new System.EventHandler(this.C1viewPat_DoubleClick);
            this.C1viewPat.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C1viewPat_MouseMove);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // panelC1
            // 
            this.panelC1.BackColor = System.Drawing.Color.Transparent;
            this.panelC1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelC1.Controls.Add(this.C1viewPat);
            this.panelC1.Controls.Add(this.label3);
            this.panelC1.Controls.Add(this.label2);
            this.panelC1.Controls.Add(this.label1);
            this.panelC1.Controls.Add(this.label59);
            this.panelC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelC1.ForeColor = System.Drawing.Color.White;
            this.panelC1.Location = new System.Drawing.Point(0, 53);
            this.panelC1.Name = "panelC1";
            this.panelC1.Padding = new System.Windows.Forms.Padding(3);
            this.panelC1.Size = new System.Drawing.Size(845, 353);
            this.panelC1.TabIndex = 12;
            this.panelC1.TabStop = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(837, 1);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(837, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(841, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 347);
            this.label1.TabIndex = 23;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 347);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnClose});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(845, 53);
            this.tls_Top.TabIndex = 26;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnClose
            // 
            this.tls_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnClose.Image")));
            this.tls_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnClose.Name = "tls_btnClose";
            this.tls_btnClose.Size = new System.Drawing.Size(43, 50);
            this.tls_btnClose.Tag = "Cancel";
            this.tls_btnClose.Text = "&Close";
            this.tls_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnClose.Click += new System.EventHandler(this.tls_btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.tls_Top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 53);
            this.panel1.TabIndex = 27;
            // 
            // frmvwRecentPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(845, 406);
            this.Controls.Add(this.panelC1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmvwRecentPatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recent Patient";
            this.Load += new System.EventHandler(this.frmvwRecentPatient_Load);
            this.Shown += new System.EventHandler(this.frmvwRecentPatient_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.C1viewPat)).EndInit();
            this.panelC1.ResumeLayout(false);
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal C1.Win.C1FlexGrid.C1FlexGrid C1viewPat;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Panel panelC1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.ToolTip toolTip1;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnClose;
        private System.Windows.Forms.Panel panel1;
    }
}