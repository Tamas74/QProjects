namespace WordToolStrip
{
    partial class gloWordToolStrip
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
                if (dtInput != null)
                {
                    dtInput.Dispose();
                    dtInput = null;
                }
                try
                {
                    if (tlsProviderSignatureButtonItem != null)
                    {
                        
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(tlsProviderSignatureButtonItem);
                        }
                        catch
                        {
                        }
                        if (MyToolStrip != null)
                        {
                            try
                            {
                                if (MyToolStrip.Items.Contains(tlsProviderSignatureButtonItem))
                                {
                                    MyToolStrip.Items.Remove(tlsProviderSignatureButtonItem);
                                }
                            }
                            catch
                            {
                            }
                        }
                        tlsProviderSignatureButtonItem.Dispose();
                        tlsProviderSignatureButtonItem = null;
                    }
                }
                catch
                {
                }
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MyToolStrip = new gloToolStrip.gloToolStrip();
            this.SuspendLayout();
            // 
            // MyToolStrip
            // 
            this.MyToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.MyToolStrip.BackgroundImage = global::WordToolStrip.Properties.Resources.Img_Toolstrip;
            this.MyToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MyToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyToolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MyToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MyToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MyToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MyToolStrip.Name = "MyToolStrip";
            this.MyToolStrip.Size = new System.Drawing.Size(175, 58);
            this.MyToolStrip.TabIndex = 0;
            this.MyToolStrip.Text = "toolStrip1";
            this.MyToolStrip.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowTagText;
            this.MyToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MyToolStrip_ItemClicked);
            // 
            // gloWordToolStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.MyToolStrip);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.Name = "gloWordToolStrip";
            this.Size = new System.Drawing.Size(175, 58);
            this.Load += new System.EventHandler(this.gloWordToolStrip_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public gloToolStrip.gloToolStrip MyToolStrip;
    }
}
