namespace gloEmdeonInterface.Forms
{
    partial class frmGrid
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
            this.dataGridResponce = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResponce)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridResponce
            // 
            this.dataGridResponce.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResponce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridResponce.Location = new System.Drawing.Point(0, 0);
            this.dataGridResponce.Name = "dataGridResponce";
            this.dataGridResponce.Size = new System.Drawing.Size(1149, 266);
            this.dataGridResponce.TabIndex = 0;
            // 
            // frmGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 266);
            this.Controls.Add(this.dataGridResponce);
            this.Name = "frmGrid";
            this.Text = "frmGrid";
            this.Load += new System.EventHandler(this.frmGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResponce)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridResponce;
    }
}