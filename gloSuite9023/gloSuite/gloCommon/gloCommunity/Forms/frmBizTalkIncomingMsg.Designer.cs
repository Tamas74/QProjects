namespace gloCommunity.Forms
{
    partial class frmBizTalkIncomingMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBizTalkIncomingMsg));
            this.cfgCCD = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.tsEjectionFraction = new System.Windows.Forms.ToolStrip();
            this.tlbbtn_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.cfgCCD)).BeginInit();
            this.Panel1.SuspendLayout();
            this.tsEjectionFraction.SuspendLayout();
            this.SuspendLayout();
            // 
            // cfgCCD
            // 
            this.cfgCCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.cfgCCD.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.cfgCCD.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.cfgCCD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgCCD.ExtendLastCol = true;
            this.cfgCCD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cfgCCD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.cfgCCD.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.cfgCCD.Location = new System.Drawing.Point(0, 0);
            this.cfgCCD.Name = "cfgCCD";
            this.cfgCCD.Rows.DefaultSize = 19;
            this.cfgCCD.ShowCellLabels = true;
            this.cfgCCD.Size = new System.Drawing.Size(292, 213);
            this.cfgCCD.StyleInfo = resources.GetString("cfgCCD.StyleInfo");
            this.cfgCCD.TabIndex = 24;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.cfgCCD);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 53);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(292, 213);
            this.Panel1.TabIndex = 28;
            // 
            // tsEjectionFraction
            // 
            this.tsEjectionFraction.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsEjectionFraction.BackgroundImage")));
            this.tsEjectionFraction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsEjectionFraction.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsEjectionFraction.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsEjectionFraction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtn_Refresh,
            this.tlbbtn_Close});
            this.tsEjectionFraction.Location = new System.Drawing.Point(0, 0);
            this.tsEjectionFraction.Name = "tsEjectionFraction";
            this.tsEjectionFraction.Size = new System.Drawing.Size(292, 53);
            this.tsEjectionFraction.TabIndex = 27;
            this.tsEjectionFraction.Text = "ToolStrip1";
            // 
            // tlbbtn_Refresh
            // 
            this.tlbbtn_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Refresh.Image")));
            this.tlbbtn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Refresh.Name = "tlbbtn_Refresh";
            this.tlbbtn_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tlbbtn_Refresh.Tag = "Refresh";
            this.tlbbtn_Refresh.Text = "&Refresh";
            this.tlbbtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Tag = "Close";
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // frmBizTalkIncomingMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.tsEjectionFraction);
            this.Name = "frmBizTalkIncomingMsg";
            this.Text = "View Incoming Documents";
            ((System.ComponentModel.ISupportInitialize)(this.cfgCCD)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.tsEjectionFraction.ResumeLayout(false);
            this.tsEjectionFraction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        internal C1.Win.C1FlexGrid.C1FlexGrid cfgCCD;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Refresh;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.ToolStrip tsEjectionFraction;
    }
}