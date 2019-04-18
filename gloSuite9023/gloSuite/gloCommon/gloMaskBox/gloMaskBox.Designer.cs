namespace gloMaskControl
{
    partial class gloMaskBox
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMaskBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // txtMaskBox
            // 
            this.txtMaskBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaskBox.Location = new System.Drawing.Point(0, 0);
            this.txtMaskBox.Name = "txtMaskBox";
            this.txtMaskBox.Size = new System.Drawing.Size(131, 20);
            this.txtMaskBox.TabIndex = 0;
            this.txtMaskBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtMaskBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            this.txtMaskBox.Leave += new System.EventHandler(this.txtMaskBox_Leave);
            // 
            // gloMaskBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMaskBox);
            this.Name = "gloMaskBox";
            this.Size = new System.Drawing.Size(131, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txtMaskBox;

    }
}
