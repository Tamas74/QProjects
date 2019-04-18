namespace gloPMGeneral
{
    partial class frmEligibilityResponse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEligibilityResponse));
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnCheckResponse = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.c1Response = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.rtfeligibilityinfo = new System.Windows.Forms.RichTextBox();
            this.rtfError = new System.Windows.Forms.RichTextBox();
            this.tls_Top.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Response)).BeginInit();
            this.SuspendLayout();
            // 
            // tls_Top
            // 
            this.tls_Top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_Top.BackgroundImage")));
            this.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Top.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Top.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Top.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btnCheckResponse,
            this.tls_btnCancel});
            this.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Top.Location = new System.Drawing.Point(0, 0);
            this.tls_Top.Name = "tls_Top";
            this.tls_Top.Size = new System.Drawing.Size(798, 53);
            this.tls_Top.TabIndex = 11;
            this.tls_Top.Text = "toolStrip1";
            // 
            // tls_btnCheckResponse
            // 
            this.tls_btnCheckResponse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnCheckResponse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tls_btnCheckResponse.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnCheckResponse.Image")));
            this.tls_btnCheckResponse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnCheckResponse.Name = "tls_btnCheckResponse";
            this.tls_btnCheckResponse.Size = new System.Drawing.Size(70, 50);
            this.tls_btnCheckResponse.Tag = "Response";
            this.tls_btnCheckResponse.Text = "&Response";
            this.tls_btnCheckResponse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnCheckResponse.Visible = false;
            this.tls_btnCheckResponse.Click += new System.EventHandler(this.tls_btnCheckResponse_Click);
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
            this.tls_btnCancel.ToolTipText = "Close";
            this.tls_btnCancel.Click += new System.EventHandler(this.tls_btnCancel_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.rtfeligibilityinfo);
            this.pnlTop.Controls.Add(this.label10);
            this.pnlTop.Controls.Add(this.label11);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 53);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTop.Size = new System.Drawing.Size(798, 337);
            this.pnlTop.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(794, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 330);
            this.label10.TabIndex = 10;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(792, 1);
            this.label11.TabIndex = 9;
            this.label11.Text = "label1";
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // c1Response
            // 
            this.c1Response.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Response.AllowEditing = false;
            this.c1Response.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Response.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Response.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Response.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Response.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Response.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Response.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Response.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Response.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Response.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Response.Location = new System.Drawing.Point(0, 390);
            this.c1Response.Name = "c1Response";
            this.c1Response.Rows.Count = 1;
            this.c1Response.Rows.DefaultSize = 19;
            this.c1Response.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Response.Size = new System.Drawing.Size(798, 148);
            this.c1Response.StyleInfo = resources.GetString("c1Response.StyleInfo");
            this.c1Response.TabIndex = 13;
            // 
            // rtfeligibilityinfo
            // 
            this.rtfeligibilityinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfeligibilityinfo.Location = new System.Drawing.Point(3, 4);
            this.rtfeligibilityinfo.Name = "rtfeligibilityinfo";
            this.rtfeligibilityinfo.Size = new System.Drawing.Size(791, 330);
            this.rtfeligibilityinfo.TabIndex = 17;
            this.rtfeligibilityinfo.Text = "";
            // 
            // rtfError
            // 
            this.rtfError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfError.Location = new System.Drawing.Point(0, 390);
            this.rtfError.Name = "rtfError";
            this.rtfError.Size = new System.Drawing.Size(798, 148);
            this.rtfError.TabIndex = 16;
            this.rtfError.Text = "";
            this.rtfError.Visible = false;
            // 
            // frmEligibilityResponse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(798, 538);
            this.Controls.Add(this.rtfError);
            this.Controls.Add(this.c1Response);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.tls_Top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEligibilityResponse";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eligibility Response";
            this.Load += new System.EventHandler(this.frmEligibilityResponse_Load);
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Response)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        private System.Windows.Forms.ToolStripButton tls_btnCheckResponse;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.RichTextBox rtfeligibilityinfo;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Response;
        private System.Windows.Forms.RichTextBox rtfError;
    }
}