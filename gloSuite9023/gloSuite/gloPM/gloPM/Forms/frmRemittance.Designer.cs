namespace gloPM
{
    partial class frmRemittance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRemittance));
            this.pnlPaymentDetails = new System.Windows.Forms.Panel();
            this.c1Transaction = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lbl_pnlPaymentDetailsBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlPaymentDetailsTopBrd = new System.Windows.Forms.Label();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnOK = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlPaymentDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Transaction)).BeginInit();
            this.tls_Top.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPaymentDetails
            // 
            this.pnlPaymentDetails.Controls.Add(this.c1Transaction);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsBottomBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsLeftBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsRightBrd);
            this.pnlPaymentDetails.Controls.Add(this.lbl_pnlPaymentDetailsTopBrd);
            this.pnlPaymentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPaymentDetails.Location = new System.Drawing.Point(0, 54);
            this.pnlPaymentDetails.Name = "pnlPaymentDetails";
            this.pnlPaymentDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPaymentDetails.Size = new System.Drawing.Size(834, 641);
            this.pnlPaymentDetails.TabIndex = 17;
            // 
            // c1Transaction
            // 
            this.c1Transaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Transaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Transaction.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Transaction.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.c1Transaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Transaction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Transaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Transaction.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Transaction.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Transaction.Location = new System.Drawing.Point(4, 4);
            this.c1Transaction.Name = "c1Transaction";
            this.c1Transaction.Rows.DefaultSize = 19;
            this.c1Transaction.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Transaction.Size = new System.Drawing.Size(826, 633);
            this.c1Transaction.StyleInfo = resources.GetString("c1Transaction.StyleInfo");
            this.c1Transaction.TabIndex = 120;
            this.c1Transaction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Transaction_MouseMove);
            // 
            // lbl_pnlPaymentDetailsBottomBrd
            // 
            this.lbl_pnlPaymentDetailsBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlPaymentDetailsBottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsBottomBrd.Location = new System.Drawing.Point(4, 637);
            this.lbl_pnlPaymentDetailsBottomBrd.Name = "lbl_pnlPaymentDetailsBottomBrd";
            this.lbl_pnlPaymentDetailsBottomBrd.Size = new System.Drawing.Size(826, 1);
            this.lbl_pnlPaymentDetailsBottomBrd.TabIndex = 119;
            this.lbl_pnlPaymentDetailsBottomBrd.Text = "label2";
            // 
            // lbl_pnlPaymentDetailsLeftBrd
            // 
            this.lbl_pnlPaymentDetailsLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlPaymentDetailsLeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlPaymentDetailsLeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_pnlPaymentDetailsLeftBrd.Name = "lbl_pnlPaymentDetailsLeftBrd";
            this.lbl_pnlPaymentDetailsLeftBrd.Size = new System.Drawing.Size(1, 634);
            this.lbl_pnlPaymentDetailsLeftBrd.TabIndex = 118;
            this.lbl_pnlPaymentDetailsLeftBrd.Text = "label4";
            // 
            // lbl_pnlPaymentDetailsRightBrd
            // 
            this.lbl_pnlPaymentDetailsRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlPaymentDetailsRightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlPaymentDetailsRightBrd.Location = new System.Drawing.Point(830, 4);
            this.lbl_pnlPaymentDetailsRightBrd.Name = "lbl_pnlPaymentDetailsRightBrd";
            this.lbl_pnlPaymentDetailsRightBrd.Size = new System.Drawing.Size(1, 634);
            this.lbl_pnlPaymentDetailsRightBrd.TabIndex = 117;
            this.lbl_pnlPaymentDetailsRightBrd.Text = "label3";
            // 
            // lbl_pnlPaymentDetailsTopBrd
            // 
            this.lbl_pnlPaymentDetailsTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlPaymentDetailsTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlPaymentDetailsTopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlPaymentDetailsTopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlPaymentDetailsTopBrd.Name = "lbl_pnlPaymentDetailsTopBrd";
            this.lbl_pnlPaymentDetailsTopBrd.Size = new System.Drawing.Size(828, 1);
            this.lbl_pnlPaymentDetailsTopBrd.TabIndex = 116;
            this.lbl_pnlPaymentDetailsTopBrd.Text = "label1";
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnOK,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(834, 53);
            this.tls_Top.TabIndex = 10;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnOK
            // 
            this.tls_btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnOK.Image")));
            this.tls_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnOK.Name = "tls_btnOK";
            this.tls_btnOK.Size = new System.Drawing.Size(43, 50);
            this.tls_btnOK.Tag = "Open";
            this.tls_btnOK.Text = "&Open";
            this.tls_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnOK.Click += new System.EventHandler(this.tls_btnOK_Click);
            // 
            // tls_btnCancel
            // 
            this.tls_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCancel.Image")));
            this.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCancel.Name = "tls_btnCancel";
            this.tls_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btnCancel.Tag = "Close";
            this.tls_btnCancel.Text = "&Close";
            this.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(834, 54);
            this.pnlToolStrip.TabIndex = 16;
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmRemittance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(834, 695);
            this.Controls.Add(this.pnlPaymentDetails);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRemittance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remittance";
            this.Load += new System.EventHandler(this.frmRemittance_Load);
            this.pnlPaymentDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Transaction)).EndInit();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPaymentDetails;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsBottomBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsLeftBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsRightBrd;
        private System.Windows.Forms.Label lbl_pnlPaymentDetailsTopBrd;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnOK;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.Panel pnlToolStrip;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Transaction;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}