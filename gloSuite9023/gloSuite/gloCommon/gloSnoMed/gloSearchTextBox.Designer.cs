namespace gloSnoMed
{
    partial class gloSearchTextBox
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
                try
                {
                    if (oTimer != null)
                    {
                        oTimer.Dispose();
                        oTimer = null;
                    }
                }
                catch
                {
                }
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
            this.components = new System.ComponentModel.Container();
            this.oTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // oTimer
            // 
            this.oTimer.Tick += new System.EventHandler(this.oTimer_Tick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer oTimer;

    }
}
