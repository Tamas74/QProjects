namespace gloPatient
{
    partial class frmEligibilityResponse_5010
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEligibilityResponse_5010));
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtfError = new System.Windows.Forms.RichTextBox();
            this.rtfeligibilityinfo = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1Response = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Top = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btnCheckResponse = new System.Windows.Forms.ToolStripButton();
            this.tls_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Response)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtfError);
            this.panel2.Controls.Add(this.rtfeligibilityinfo);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel2.Size = new System.Drawing.Size(1158, 382);
            this.panel2.TabIndex = 20;
            // 
            // rtfError
            // 
            this.rtfError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtfError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfError.Location = new System.Drawing.Point(4, 1);
            this.rtfError.Name = "rtfError";
            this.rtfError.Size = new System.Drawing.Size(1150, 380);
            this.rtfError.TabIndex = 21;
            this.rtfError.Text = "";
            this.rtfError.Visible = false;
            // 
            // rtfeligibilityinfo
            // 
            this.rtfeligibilityinfo.BackColor = System.Drawing.Color.White;
            this.rtfeligibilityinfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtfeligibilityinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfeligibilityinfo.Location = new System.Drawing.Point(4, 1);
            this.rtfeligibilityinfo.Name = "rtfeligibilityinfo";
            this.rtfeligibilityinfo.ReadOnly = true;
            this.rtfeligibilityinfo.Size = new System.Drawing.Size(1150, 380);
            this.rtfeligibilityinfo.TabIndex = 20;
            this.rtfeligibilityinfo.Text = "";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(4, 381);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1150, 1);
            this.label8.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1150, 1);
            this.label7.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 382);
            this.label6.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(1154, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 382);
            this.label5.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1Response);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 441);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(1158, 273);
            this.panel1.TabIndex = 21;
            // 
            // c1Response
            // 
            this.c1Response.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Response.AllowEditing = false;
            this.c1Response.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this.c1Response.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Response.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Response.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1Response.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Response.ExtendLastCol = true;
            this.c1Response.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Response.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1Response.Location = new System.Drawing.Point(4, 1);
            this.c1Response.Name = "c1Response";
            this.c1Response.Rows.Count = 1;
            this.c1Response.Rows.DefaultSize = 19;
            this.c1Response.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Response.Size = new System.Drawing.Size(1150, 268);
            this.c1Response.StyleInfo = resources.GetString("c1Response.StyleInfo");
            this.c1Response.TabIndex = 60;
            this.c1Response.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Response_MouseMove);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1150, 1);
            this.label1.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1154, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 269);
            this.label4.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 269);
            this.label3.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1152, 1);
            this.label2.TabIndex = 20;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Top);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1158, 56);
            this.pnlToolStrip.TabIndex = 22;
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
            this.tls_Top.Size = new System.Drawing.Size(1158, 53);
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
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.C1SuperTooltip1.ShowAlways = true;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 438);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1158, 3);
            this.splitter1.TabIndex = 23;
            this.splitter1.TabStop = false;
            this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
            // 
            // frmEligibilityResponse_5010
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1158, 714);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEligibilityResponse_5010";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eligibility Response";
            this.Load += new System.EventHandler(this.frmEligibilityResponse_5010_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEligibilityResponse_5010_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Response)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Top.ResumeLayout(false);
            this.tls_Top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtfError;
        private System.Windows.Forms.RichTextBox rtfeligibilityinfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Top;
        private System.Windows.Forms.ToolStripButton tls_btnCheckResponse;
        private System.Windows.Forms.ToolStripButton tls_btnCancel;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Response;
        private System.Windows.Forms.Splitter splitter1;
    }
}