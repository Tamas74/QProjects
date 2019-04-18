namespace gloPrintDialog
{
    partial class gloPrinterSelector
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
            this.components = new System.ComponentModel.Container();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.btnModifyPrinter = new System.Windows.Forms.Button();
            this.cmbPrinterList = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.lblNote);
            this.grpMain.Controls.Add(this.btnRefreshList);
            this.grpMain.Controls.Add(this.btnModifyPrinter);
            this.grpMain.Controls.Add(this.cmbPrinterList);
            this.grpMain.Controls.Add(this.lblName);
            this.grpMain.Location = new System.Drawing.Point(9, 11);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(417, 105);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Printer";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(57, 80);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(341, 13);
            this.lblNote.TabIndex = 4;
            this.lblNote.Text = "Note: If you are not able to see your printers, click on \'Refresh Printers\'.";
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Location = new System.Drawing.Point(56, 50);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(101, 23);
            this.btnRefreshList.TabIndex = 3;
            this.btnRefreshList.Text = "Refresh Printers";
            this.toolTip1.SetToolTip(this.btnRefreshList, "Refresh Printers");
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // btnModifyPrinter
            // 
            this.btnModifyPrinter.Location = new System.Drawing.Point(329, 22);
            this.btnModifyPrinter.Name = "btnModifyPrinter";
            this.btnModifyPrinter.Size = new System.Drawing.Size(75, 23);
            this.btnModifyPrinter.TabIndex = 2;
            this.btnModifyPrinter.Text = "Modify";
            this.toolTip1.SetToolTip(this.btnModifyPrinter, "Modify");
            this.btnModifyPrinter.UseVisualStyleBackColor = true;
            this.btnModifyPrinter.Click += new System.EventHandler(this.btnModifyPrinter_Click);
            // 
            // cmbPrinterList
            // 
            this.cmbPrinterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinterList.FormattingEnabled = true;
            this.cmbPrinterList.Location = new System.Drawing.Point(57, 23);
            this.cmbPrinterList.Name = "cmbPrinterList";
            this.cmbPrinterList.Size = new System.Drawing.Size(268, 21);
            this.cmbPrinterList.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(16, 26);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(272, 122);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(351, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gloPrinterSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 165);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(451, 171);
            this.Name = "gloPrinterSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Printer Profiles";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.gloPrinterSelector_FormClosed);
            this.Load += new System.EventHandler(this.gloPrinterSelector_Load);
            this.Shown += new System.EventHandler(this.gloPrinterSelector_Shown);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.ComboBox cmbPrinterList;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnModifyPrinter;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblNote;
    }
}