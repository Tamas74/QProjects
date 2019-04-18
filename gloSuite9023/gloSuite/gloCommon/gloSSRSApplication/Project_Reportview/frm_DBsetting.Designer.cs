namespace Project_Reportview
{
    partial class frm_DBsetting
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
            this.btn_Save = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.lblServerName = new System.Windows.Forms.Label();
            this.Txt_ServerName = new System.Windows.Forms.TextBox();
            this.Txt_ReportFolderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(103, 180);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(87, 26);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Cancel.Location = new System.Drawing.Point(196, 180);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(87, 26);
            this.Btn_Cancel.TabIndex = 1;
            this.Btn_Cancel.Text = "&Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.BackColor = System.Drawing.Color.Transparent;
            this.lblServerName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerName.Location = new System.Drawing.Point(68, 34);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(100, 13);
            this.lblServerName.TabIndex = 2;
            this.lblServerName.Text = "Server Name :";
            // 
            // Txt_ServerName
            // 
            this.Txt_ServerName.Location = new System.Drawing.Point(174, 30);
            this.Txt_ServerName.MaxLength = 25;
            this.Txt_ServerName.Name = "Txt_ServerName";
            this.Txt_ServerName.Size = new System.Drawing.Size(166, 20);
            this.Txt_ServerName.TabIndex = 3;
            // 
            // Txt_ReportFolderName
            // 
            this.Txt_ReportFolderName.Location = new System.Drawing.Point(174, 66);
            this.Txt_ReportFolderName.MaxLength = 25;
            this.Txt_ReportFolderName.Name = "Txt_ReportFolderName";
            this.Txt_ReportFolderName.Size = new System.Drawing.Size(166, 20);
            this.Txt_ReportFolderName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Report Folder Name :";
            // 
            // frm_DBsetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::gloSSRSApplication.Properties.Resources.btn_img;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(386, 229);
            this.Controls.Add(this.Txt_ReportFolderName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_ServerName);
            this.Controls.Add(this.lblServerName);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.MaximizeBox = false;
            this.Name = "frm_DBsetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.TextBox Txt_ServerName;
        private System.Windows.Forms.TextBox Txt_ReportFolderName;
        private System.Windows.Forms.Label label1;
    }
}