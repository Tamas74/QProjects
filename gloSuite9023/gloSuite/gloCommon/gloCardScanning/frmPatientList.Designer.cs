namespace gloCardScanningPatientList
{
    partial class frmPatientList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientList));
            this.pnl_ToolStrip = new System.Windows.Forms.Panel();
            this.tlsp_Location = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsp_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.c1PatientList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnl_ToolStrip.SuspendLayout();
            this.tlsp_Location.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).BeginInit();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ToolStrip
            // 
            this.pnl_ToolStrip.Controls.Add(this.tlsp_Location);
            this.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnl_ToolStrip.Name = "pnl_ToolStrip";
            this.pnl_ToolStrip.Size = new System.Drawing.Size(422, 54);
            this.pnl_ToolStrip.TabIndex = 1;
            // 
            // tlsp_Location
            // 
            this.tlsp_Location.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_Location.BackgroundImage")));
            this.tlsp_Location.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_Location.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_Location.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_Location.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsp_btnOK,
            this.tlsp_btnCancel});
            this.tlsp_Location.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_Location.Location = new System.Drawing.Point(0, 0);
            this.tlsp_Location.Name = "tlsp_Location";
            this.tlsp_Location.Size = new System.Drawing.Size(422, 53);
            this.tlsp_Location.TabIndex = 11;
            this.tlsp_Location.Text = "ToolStrip1";
            // 
            // tlsp_btnOK
            // 
            this.tlsp_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsp_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnOK.Image")));
            this.tlsp_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnOK.Name = "tlsp_btnOK";
            this.tlsp_btnOK.Size = new System.Drawing.Size(66, 50);
            this.tlsp_btnOK.Tag = "OK";
            this.tlsp_btnOK.Text = "&Save&&Cls";
            this.tlsp_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnOK.ToolTipText = "Save and Close";
            this.tlsp_btnOK.Click += new System.EventHandler(this.tlsp_btnOK_Click);
            // 
            // tlsp_btnCancel
            // 
            this.tlsp_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlsp_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnCancel.Image")));
            this.tlsp_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnCancel.Name = "tlsp_btnCancel";
            this.tlsp_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tlsp_btnCancel.Tag = "Cancel";
            this.tlsp_btnCancel.Text = "&Close";
            this.tlsp_btnCancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnCancel.ToolTipText = "Close";
            this.tlsp_btnCancel.Click += new System.EventHandler(this.tlsp_btnCancel_Click);
            // 
            // c1PatientList
            // 
            this.c1PatientList.AllowEditing = false;
            this.c1PatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientList.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1PatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientList.Location = new System.Drawing.Point(4, 4);
            this.c1PatientList.Name = "c1PatientList";
            this.c1PatientList.Rows.Count = 1;
            this.c1PatientList.Rows.DefaultSize = 19;
            this.c1PatientList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientList.ShowCellLabels = true;
            this.c1PatientList.Size = new System.Drawing.Size(414, 259);
            this.c1PatientList.StyleInfo = resources.GetString("c1PatientList.StyleInfo");
            this.c1PatientList.TabIndex = 0;
            this.c1PatientList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PatientList_MouseMove);
            this.c1PatientList.DoubleClick += new System.EventHandler(this.c1PatientList_DoubleClick);
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel2.Controls.Add(this.c1PatientList);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Controls.Add(this.Label7);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.Location = new System.Drawing.Point(0, 54);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.Panel2.Size = new System.Drawing.Size(422, 267);
            this.Panel2.TabIndex = 0;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 263);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(414, 1);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 4);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 260);
            this.Label6.TabIndex = 7;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(418, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 260);
            this.Label7.TabIndex = 6;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 3);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(416, 1);
            this.Label8.TabIndex = 5;
            this.Label8.Text = "label1";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmPatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(422, 321);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.pnl_ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientList";
            this.ShowInTaskbar = false;
            this.Text = "Patient List";
            this.Load += new System.EventHandler(this.frmPatientList_Load);
            this.pnl_ToolStrip.ResumeLayout(false);
            this.pnl_ToolStrip.PerformLayout();
            this.tlsp_Location.ResumeLayout(false);
            this.tlsp_Location.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientList)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus tlsp_Location;
        internal System.Windows.Forms.ToolStripButton tlsp_btnOK;
        internal System.Windows.Forms.ToolStripButton tlsp_btnCancel;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientList;
        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}