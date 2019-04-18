namespace gloGlobal.SBPQuestionnaire
{
    partial class usrCtrlDynamicSBP
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlWebbrowserCtrl = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_pnlTop = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_pnlLeft = new System.Windows.Forms.Label();
            this.wbBrowserCtrl = new System.Windows.Forms.WebBrowser();
            this.pnlWebbrowserCtrl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWebbrowserCtrl
            // 
            this.pnlWebbrowserCtrl.Controls.Add(this.label2);
            this.pnlWebbrowserCtrl.Controls.Add(this.lbl_pnlTop);
            this.pnlWebbrowserCtrl.Controls.Add(this.label1);
            this.pnlWebbrowserCtrl.Controls.Add(this.lbl_pnlLeft);
            this.pnlWebbrowserCtrl.Controls.Add(this.wbBrowserCtrl);
            this.pnlWebbrowserCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWebbrowserCtrl.Location = new System.Drawing.Point(0, 0);
            this.pnlWebbrowserCtrl.Name = "pnlWebbrowserCtrl";
            this.pnlWebbrowserCtrl.Padding = new System.Windows.Forms.Padding(3);
            this.pnlWebbrowserCtrl.Size = new System.Drawing.Size(686, 493);
            this.pnlWebbrowserCtrl.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(4, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(678, 1);
            this.label2.TabIndex = 7;
            this.label2.Text = "label1";
            // 
            // lbl_pnlTop
            // 
            this.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTop.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnlTop.Name = "lbl_pnlTop";
            this.lbl_pnlTop.Size = new System.Drawing.Size(678, 1);
            this.lbl_pnlTop.TabIndex = 6;
            this.lbl_pnlTop.Text = "label1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(682, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 487);
            this.label1.TabIndex = 5;
            this.label1.Text = "label4";
            // 
            // lbl_pnlLeft
            // 
            this.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlLeft.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnlLeft.Name = "lbl_pnlLeft";
            this.lbl_pnlLeft.Size = new System.Drawing.Size(1, 487);
            this.lbl_pnlLeft.TabIndex = 4;
            this.lbl_pnlLeft.Text = "label4";
            // 
            // wbBrowserCtrl
            // 
            this.wbBrowserCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbBrowserCtrl.Location = new System.Drawing.Point(3, 3);
            this.wbBrowserCtrl.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbBrowserCtrl.Name = "wbBrowserCtrl";
            this.wbBrowserCtrl.Size = new System.Drawing.Size(680, 487);
            this.wbBrowserCtrl.TabIndex = 0;
            this.wbBrowserCtrl.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbBrowserCtrl_DocumentCompleted);
            this.wbBrowserCtrl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.wbBrowserCtrl_PreviewKeyDown);
            // 
            // usrCtrlDynamicSBP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlWebbrowserCtrl);
            this.Name = "usrCtrlDynamicSBP";
            this.Size = new System.Drawing.Size(686, 493);
            this.pnlWebbrowserCtrl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWebbrowserCtrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_pnlTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_pnlLeft;
        private System.Windows.Forms.WebBrowser wbBrowserCtrl;
    }
}
