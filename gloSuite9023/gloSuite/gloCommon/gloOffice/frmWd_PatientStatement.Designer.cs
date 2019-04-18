namespace gloOffice
{
    partial class frmWd_PatientStatement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWd_PatientStatement));
            this.wdStatement = new AxDSOFramer.AxFramerControl();
            ((System.ComponentModel.ISupportInitialize)(this.wdStatement)).BeginInit();
            this.SuspendLayout();
            // 
            // wdStatement
            // 
            this.wdStatement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdStatement.Enabled = true;
            this.wdStatement.Location = new System.Drawing.Point(0, 0);
            this.wdStatement.Name = "wdStatement";
            this.wdStatement.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdStatement.OcxState")));
            this.wdStatement.Size = new System.Drawing.Size(599, 497);
            this.wdStatement.TabIndex = 1;
            this.wdStatement.OnDocumentOpened += new AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEventHandler(this.wdStatement_OnDocumentOpened);
            this.wdStatement.OnDocumentClosed += new System.EventHandler(this.wdStatement_OnDocumentClosed);
            // 
            // frmWd_PatientStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 497);
            this.Controls.Add(this.wdStatement);
            this.Name = "frmWd_PatientStatement";
            this.Text = "frmWd_PatientStatement";
            ((System.ComponentModel.ISupportInitialize)(this.wdStatement)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxDSOFramer.AxFramerControl wdStatement;
    }
}