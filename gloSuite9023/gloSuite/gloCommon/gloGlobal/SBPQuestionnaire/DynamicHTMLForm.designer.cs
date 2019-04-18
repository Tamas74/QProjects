namespace gloGlobal.Common//DynamicHTMLFormSBP
{
    partial class frmDynamicHTMLForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDynamicHTMLForm));
            this.wbBrowserCtrl = new System.Windows.Forms.WebBrowser();
            this.pnlSBPToolBar = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlstrpBtnSaveClose = new System.Windows.Forms.ToolStripButton();
            this.tlstrpbtnSBPHistory = new System.Windows.Forms.ToolStripButton();
            this.tlstrpBtnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlWebbrowserCtrl = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.pnlSBPToolBar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlWebbrowserCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbBrowserCtrl
            // 
            this.wbBrowserCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbBrowserCtrl.Location = new System.Drawing.Point(3, 3);
            this.wbBrowserCtrl.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbBrowserCtrl.Name = "wbBrowserCtrl";
            this.wbBrowserCtrl.Size = new System.Drawing.Size(738, 409);
            this.wbBrowserCtrl.TabIndex = 0;
            this.wbBrowserCtrl.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbBrowserCtrl_DocumentCompleted);
            this.wbBrowserCtrl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.wbBrowserCtrl_PreviewKeyDown);
            // 
            // pnlSBPToolBar
            // 
            this.pnlSBPToolBar.AutoSize = true;
            this.pnlSBPToolBar.Controls.Add(this.toolStrip1);
            this.pnlSBPToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSBPToolBar.Location = new System.Drawing.Point(0, 0);
            this.pnlSBPToolBar.Name = "pnlSBPToolBar";
            this.pnlSBPToolBar.Size = new System.Drawing.Size(744, 53);
            this.pnlSBPToolBar.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlstrpbtnSBPHistory,
            this.tlstrpBtnSaveClose,
            this.tlstrpBtnClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(744, 53);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlstrpBtnSaveClose
            // 
            this.tlstrpBtnSaveClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlstrpBtnSaveClose.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpBtnSaveClose.Image")));
            this.tlstrpBtnSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpBtnSaveClose.Name = "tlstrpBtnSaveClose";
            this.tlstrpBtnSaveClose.Size = new System.Drawing.Size(66, 50);
            this.tlstrpBtnSaveClose.Text = "&Save&&Cls";
            this.tlstrpBtnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlstrpBtnSaveClose.ToolTipText = "Save and Close";
            this.tlstrpBtnSaveClose.Click += new System.EventHandler(this.tlstrpBtnSaveClose_Click);
            // 
            // tlstrpbtnSBPHistory
            // 
            this.tlstrpbtnSBPHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlstrpbtnSBPHistory.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpbtnSBPHistory.Image")));
            this.tlstrpbtnSBPHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpbtnSBPHistory.Name = "tlstrpbtnSBPHistory";
            this.tlstrpbtnSBPHistory.Size = new System.Drawing.Size(93, 50);
            this.tlstrpbtnSBPHistory.Text = "Audit History";
            this.tlstrpbtnSBPHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlstrpbtnSBPHistory.Click += new System.EventHandler(this.tlstrpbtnSBPHistory_Click);
            // 
            // tlstrpBtnClose
            // 
            this.tlstrpBtnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlstrpBtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tlstrpBtnClose.Image")));
            this.tlstrpBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlstrpBtnClose.Name = "tlstrpBtnClose";
            this.tlstrpBtnClose.Size = new System.Drawing.Size(43, 50);
            this.tlstrpBtnClose.Text = "&Close";
            this.tlstrpBtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlstrpBtnClose.Click += new System.EventHandler(this.tlstrpBtnClose_Click);
            // 
            // pnlWebbrowserCtrl
            // 
            this.pnlWebbrowserCtrl.Controls.Add(this.label2);
            this.pnlWebbrowserCtrl.Controls.Add(this.lbl_pnlTop);
            this.pnlWebbrowserCtrl.Controls.Add(this.label1);
            this.pnlWebbrowserCtrl.Controls.Add(this.lbl_pnlLeft);
            this.pnlWebbrowserCtrl.Controls.Add(this.wbBrowserCtrl);
            this.pnlWebbrowserCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWebbrowserCtrl.Location = new System.Drawing.Point(0, 53);
            this.pnlWebbrowserCtrl.Name = "pnlWebbrowserCtrl";
            this.pnlWebbrowserCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.pnlWebbrowserCtrl.Size = new System.Drawing.Size(744, 415);
            this.pnlWebbrowserCtrl.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(4, 411);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(736, 1);
            this.label2.TabIndex = 7;
            this.label2.Text = "label1";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(736, 1);
            this.lbl_pnlTop.TabIndex = 6;
            this.lbl_pnlTop.Text = "label1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(740, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 409);
            this.label1.TabIndex = 5;
            this.label1.Text = "label4";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 409);
            this.lbl_pnlLeft.TabIndex = 4;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // frmDynamicHTMLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(744, 468);
            this.Controls.Add(this.pnlWebbrowserCtrl);
            this.Controls.Add(this.pnlSBPToolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDynamicHTMLForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dynamic HTML Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDynamicHTMLForm_FormClosed);
            this.Load += new System.EventHandler(this.frmDynamicHTMLForm_Load);
            this.pnlSBPToolBar.ResumeLayout(false);
            this.pnlSBPToolBar.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlWebbrowserCtrl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbBrowserCtrl;
        private System.Windows.Forms.Panel pnlSBPToolBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel pnlWebbrowserCtrl;
        private System.Windows.Forms.ToolStripButton tlstrpbtnSBPHistory;
        private System.Windows.Forms.ToolStripButton tlstrpBtnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_pnlTop;
        private System.Windows.Forms.ToolStripButton tlstrpBtnSaveClose;
    }
}

