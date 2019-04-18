namespace gloReports
{
    partial class frmHL7MessageDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHL7MessageDetails));
            this.rtbHL7Message = new System.Windows.Forms.RichTextBox();
            this.pnl_tlspTOP = new System.Windows.Forms.Panel();
            this.tlsp_Settings = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnWrap = new System.Windows.Forms.ToolStripButton();
            this.ts_btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.ts_btnCancel = new System.Windows.Forms.ToolStripButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.lblMessageID = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.pnl_tlspTOP.SuspendLayout();
            this.tlsp_Settings.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbHL7Message
            // 
            this.rtbHL7Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHL7Message.Location = new System.Drawing.Point(0, 0);
            this.rtbHL7Message.Name = "rtbHL7Message";
            this.rtbHL7Message.Size = new System.Drawing.Size(704, 326);
            this.rtbHL7Message.TabIndex = 0;
            this.rtbHL7Message.Text = "";
            // 
            // pnl_tlspTOP
            // 
            this.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            this.pnl_tlspTOP.Controls.Add(this.tlsp_Settings);
            this.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlspTOP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnl_tlspTOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlspTOP.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnl_tlspTOP.Name = "pnl_tlspTOP";
            this.pnl_tlspTOP.Size = new System.Drawing.Size(712, 58);
            this.pnl_tlspTOP.TabIndex = 11;
            // 
            // tlsp_Settings
            // 
            this.tlsp_Settings.BackColor = System.Drawing.Color.Transparent;
            this.tlsp_Settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsp_Settings.BackgroundImage")));
            this.tlsp_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsp_Settings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_Settings.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsp_Settings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnWrap,
            this.ts_btnRefresh,
            this.ts_btnCancel});
            this.tlsp_Settings.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsp_Settings.Location = new System.Drawing.Point(0, 0);
            this.tlsp_Settings.Name = "tlsp_Settings";
            this.tlsp_Settings.Size = new System.Drawing.Size(712, 53);
            this.tlsp_Settings.TabIndex = 0;
            this.tlsp_Settings.Text = "toolStrip1";
            // 
            // ts_btnWrap
            // 
            this.ts_btnWrap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnWrap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnWrap.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnWrap.Image")));
            this.ts_btnWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnWrap.Name = "ts_btnWrap";
            this.ts_btnWrap.Size = new System.Drawing.Size(43, 50);
            this.ts_btnWrap.Tag = "";
            this.ts_btnWrap.Text = "&Wrap";
            this.ts_btnWrap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnWrap.ToolTipText = "Wrap Message";
            this.ts_btnWrap.Click += new System.EventHandler(this.ts_btnWrap_Click);
            // 
            // ts_btnRefresh
            // 
            this.ts_btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnRefresh.Image")));
            this.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnRefresh.Name = "ts_btnRefresh";
            this.ts_btnRefresh.Size = new System.Drawing.Size(58, 50);
            this.ts_btnRefresh.Tag = "Refresh";
            this.ts_btnRefresh.Text = "&Refresh";
            this.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnRefresh.ToolTipText = "Refresh";
            this.ts_btnRefresh.Visible = false;
            // 
            // ts_btnCancel
            // 
            this.ts_btnCancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCancel.Image")));
            this.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCancel.Name = "ts_btnCancel";
            this.ts_btnCancel.Size = new System.Drawing.Size(43, 50);
            this.ts_btnCancel.Tag = "Cancel";
            this.ts_btnCancel.Text = "&Close";
            this.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnCancel.Click += new System.EventHandler(this.ts_btnCancel_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Panel3);
            this.Panel1.Controls.Add(this.Panel2);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 58);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(712, 365);
            this.Panel1.TabIndex = 12;
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.rtbHL7Message);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel3.Location = new System.Drawing.Point(4, 35);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(704, 326);
            this.Panel3.TabIndex = 22;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.lblMessageID);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(4, 4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(704, 31);
            this.Panel2.TabIndex = 21;
            // 
            // lblMessageID
            // 
            this.lblMessageID.AutoSize = true;
            this.lblMessageID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageID.Location = new System.Drawing.Point(3, 6);
            this.lblMessageID.Name = "lblMessageID";
            this.lblMessageID.Size = new System.Drawing.Size(85, 14);
            this.lblMessageID.TabIndex = 20;
            this.lblMessageID.Text = "Message ID :";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label4.Location = new System.Drawing.Point(3, 4);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(1, 357);
            this.Label4.TabIndex = 3;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label3.Location = new System.Drawing.Point(708, 4);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 357);
            this.Label3.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label2.Location = new System.Drawing.Point(3, 361);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(706, 1);
            this.Label2.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Location = new System.Drawing.Point(3, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(706, 1);
            this.Label1.TabIndex = 0;
            // 
            // frmHL7MessageDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(712, 423);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pnl_tlspTOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHL7MessageDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HL7 Message Details";
            this.pnl_tlspTOP.ResumeLayout(false);
            this.pnl_tlspTOP.PerformLayout();
            this.tlsp_Settings.ResumeLayout(false);
            this.tlsp_Settings.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbHL7Message;
        private System.Windows.Forms.Panel pnl_tlspTOP;
        private gloGlobal.gloToolStripIgnoreFocus tlsp_Settings;
        private System.Windows.Forms.ToolStripButton ts_btnWrap;
        private System.Windows.Forms.ToolStripButton ts_btnRefresh;
        private System.Windows.Forms.ToolStripButton ts_btnCancel;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label lblMessageID;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
    }
}