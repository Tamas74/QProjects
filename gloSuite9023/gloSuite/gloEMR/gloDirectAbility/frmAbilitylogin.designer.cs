namespace gloDirectAbility
{
    partial class frmAbilitylogin
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
                  components.Dispose();
                  gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
              }
              catch
              {
              }
            
              try
              {
                  if (saveFileDialog1 != null)
                  {
                      
                      saveFileDialog1.Dispose();
                      saveFileDialog1 = null;
                  }
              }
              catch
              {
              }
          }
          base.Dispose(disposing);
      }

        #region Windows Form Designer generated code



        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbilitylogin));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tblStrip_32 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tblbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlWebbrowser = new System.Windows.Forms.Panel();
            this.wbLogin = new System.Windows.Forms.WebBrowser();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pnlToolStrip.SuspendLayout();
            this.tblStrip_32.SuspendLayout();
            this.pnlWebbrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlToolStrip.Controls.Add(this.tblStrip_32);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1184, 54);
            this.pnlToolStrip.TabIndex = 37;
            // 
            // tblStrip_32
            // 
            this.tblStrip_32.BackColor = System.Drawing.Color.Transparent;
            this.tblStrip_32.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblStrip_32.BackgroundImage")));
            this.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tblStrip_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblStrip_32.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tblStrip_32.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tblbtn_Close});
            this.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tblStrip_32.Location = new System.Drawing.Point(0, 0);
            this.tblStrip_32.Name = "tblStrip_32";
            this.tblStrip_32.Size = new System.Drawing.Size(1184, 53);
            this.tblStrip_32.TabIndex = 6;
            this.tblStrip_32.Text = "ToolStrip1";
            // 
            // tblbtn_Close
            // 
            this.tblbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtn_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Close.Image")));
            this.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Close.Name = "tblbtn_Close";
            this.tblbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tblbtn_Close.Tag = "Close";
            this.tblbtn_Close.Text = "&Close";
            this.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblbtn_Close.Click += new System.EventHandler(this.tblbtn_Close_Click);
            // 
            // pnlWebbrowser
            // 
            this.pnlWebbrowser.Controls.Add(this.wbLogin);
            this.pnlWebbrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWebbrowser.Location = new System.Drawing.Point(0, 54);
            this.pnlWebbrowser.Name = "pnlWebbrowser";
            this.pnlWebbrowser.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlWebbrowser.Size = new System.Drawing.Size(1184, 778);
            this.pnlWebbrowser.TabIndex = 39;
            // 
            // wbLogin
            // 
            this.wbLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbLogin.Location = new System.Drawing.Point(3, 0);
            this.wbLogin.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbLogin.Name = "wbLogin";
            this.wbLogin.ScriptErrorsSuppressed = true;
            this.wbLogin.Size = new System.Drawing.Size(1178, 775);
            this.wbLogin.TabIndex = 0;
            // 
            // frmAbilitylogin
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1184, 832);
            this.Controls.Add(this.pnlWebbrowser);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbilitylogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DIRECT Inbox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAbilitylogin_FormClosing);
            this.Load += new System.EventHandler(this.frmAbilitylogin_Load);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tblStrip_32.ResumeLayout(false);
            this.tblStrip_32.PerformLayout();
            this.pnlWebbrowser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus tblStrip_32;
        internal System.Windows.Forms.ToolStripButton tblbtn_Close;
        internal System.Windows.Forms.Panel pnlWebbrowser;
       // internal System.Windows.Forms.ListBox LstTreatment;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.WebBrowser wbLogin;



    }
}