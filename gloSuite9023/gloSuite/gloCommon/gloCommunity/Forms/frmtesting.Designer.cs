namespace gloCommunity.Forms
{
    partial class frmtesting
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
            this.btnclinic = new System.Windows.Forms.Button();
            this.btnglobal = new System.Windows.Forms.Button();
            this.btndownload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnclinic
            // 
            this.btnclinic.Location = new System.Drawing.Point(72, 483);
            this.btnclinic.Name = "btnclinic";
            this.btnclinic.Size = new System.Drawing.Size(97, 23);
            this.btnclinic.TabIndex = 0;
            this.btnclinic.Text = "Upload Clinic";
            this.btnclinic.UseVisualStyleBackColor = true;
            this.btnclinic.Click += new System.EventHandler(this.btnclinic_Click);
            // 
            // btnglobal
            // 
            this.btnglobal.Location = new System.Drawing.Point(208, 483);
            this.btnglobal.Name = "btnglobal";
            this.btnglobal.Size = new System.Drawing.Size(152, 23);
            this.btnglobal.TabIndex = 1;
            this.btnglobal.Text = "Upload Global Repository";
            this.btnglobal.UseVisualStyleBackColor = true;
            this.btnglobal.Click += new System.EventHandler(this.btnglobal_Click);
            // 
            // btndownload
            // 
            this.btndownload.Location = new System.Drawing.Point(94, 529);
            this.btndownload.Name = "btndownload";
            this.btndownload.Size = new System.Drawing.Size(75, 23);
            this.btndownload.TabIndex = 2;
            this.btndownload.Text = "Download";
            this.btndownload.UseVisualStyleBackColor = true;
            this.btndownload.Click += new System.EventHandler(this.btndownload_Click);
            // 
            // frmtesting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 573);
            this.Controls.Add(this.btndownload);
            this.Controls.Add(this.btnglobal);
            this.Controls.Add(this.btnclinic);
            this.Name = "frmtesting";
            this.Text = "frmtesting";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnclinic;
        private System.Windows.Forms.Button btnglobal;
        private System.Windows.Forms.Button btndownload;
    }
}