namespace gloPrintDialog
{
    partial class gloPrintProgressController
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
            if (disposing)
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                if (_IsOnPagePDFDocCreated)
                {
                    if (_PDFDoc != null)
                    {
                        _PDFDoc.Dispose();
                        _PDFDoc = null;
                    }
                }
                if (components != null)
                {
                    components.Dispose();
                }
                try
                {
                    if (_oPrintDocument != null)
                    {
                        _oPrintDocument.Dispose();
                        _oPrintDocument = null;
                    }
                    if (_PrinterSetting != null)
                    {

                        _PrinterSetting = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (_ExtendedPrinterSettings != null)
                    {
                        _ExtendedPrinterSettings.Dispose();
                        _ExtendedPrinterSettings = null;
                    }

                }
                catch
                {
                }
                try
                {
                    if (_ExtendedPrinterSettingsFooterFontBy != null)
                    {
                        for (int scaling = 1; scaling <= 3; scaling++)
                        {
                            if (_ExtendedPrinterSettingsFooterFontBy[scaling-1] != null)
                            {
                                _ExtendedPrinterSettingsFooterFontBy[scaling-1].Dispose();
                                _ExtendedPrinterSettingsFooterFontBy[scaling-1] = null;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            thisform = null;
            base.Dispose(disposing);

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPrintProgressController));
            this.lblPrinterName = new System.Windows.Forms.Label();
            this.lblPrinterNameValue = new System.Windows.Forms.Label();
            this.lblPages = new System.Windows.Forms.Label();
            this.gbPrintingBox = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCopies = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblPageNoOfDocument = new System.Windows.Forms.Label();
            this.lblDocumentName = new System.Windows.Forms.Label();
            this.gbPrintingBox.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPrinterName
            // 
            this.lblPrinterName.AutoSize = true;
            this.lblPrinterName.Location = new System.Drawing.Point(13, 14);
            this.lblPrinterName.Name = "lblPrinterName";
            this.lblPrinterName.Size = new System.Drawing.Size(67, 13);
            this.lblPrinterName.TabIndex = 0;
            this.lblPrinterName.Text = "Printing To : ";
            // 
            // lblPrinterNameValue
            // 
            this.lblPrinterNameValue.AutoEllipsis = true;
            this.lblPrinterNameValue.AutoSize = true;
            this.lblPrinterNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterNameValue.Location = new System.Drawing.Point(75, 14);
            this.lblPrinterNameValue.Name = "lblPrinterNameValue";
            this.lblPrinterNameValue.Size = new System.Drawing.Size(77, 13);
            this.lblPrinterNameValue.TabIndex = 1;
            this.lblPrinterNameValue.Text = "printer name";
            // 
            // lblPages
            // 
            this.lblPages.AutoSize = true;
            this.lblPages.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPages.Location = new System.Drawing.Point(0, 0);
            this.lblPages.Name = "lblPages";
            this.lblPages.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lblPages.Size = new System.Drawing.Size(70, 13);
            this.lblPages.TabIndex = 2;
            this.lblPages.Text = "n of N pages";
            // 
            // gbPrintingBox
            // 
            this.gbPrintingBox.Controls.Add(this.panel3);
            this.gbPrintingBox.Controls.Add(this.panel1);
            this.gbPrintingBox.Controls.Add(this.panel4);
            this.gbPrintingBox.Location = new System.Drawing.Point(10, 37);
            this.gbPrintingBox.Name = "gbPrintingBox";
            this.gbPrintingBox.Size = new System.Drawing.Size(351, 148);
            this.gbPrintingBox.TabIndex = 3;
            this.gbPrintingBox.TabStop = false;
            this.gbPrintingBox.Text = "Printing";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.pbDocument);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(345, 58);
            this.panel3.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRestart);
            this.panel2.Controls.Add(this.btnPlay);
            this.panel2.Controls.Add(this.btnPause);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 23);
            this.panel2.TabIndex = 11;
            // 
            // btnRestart
            // 
            this.btnRestart.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRestart.Enabled = false;
            this.btnRestart.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Image = ((System.Drawing.Image)(resources.GetObject("btnRestart.Image")));
            this.btnRestart.Location = new System.Drawing.Point(216, 0);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(31, 23);
            this.btnRestart.TabIndex = 7;
            this.btnRestart.Tag = "Resume";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(247, 0);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(33, 23);
            this.btnPlay.TabIndex = 10;
            this.btnPlay.Tag = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(280, 0);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(33, 23);
            this.btnPause.TabIndex = 8;
            this.btnPause.Tag = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(313, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(32, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Tag = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbDocument
            // 
            this.pbDocument.Location = new System.Drawing.Point(3, 8);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(337, 18);
            this.pbDocument.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCopies);
            this.panel1.Controls.Add(this.lblPages);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 18);
            this.panel1.TabIndex = 4;
            // 
            // lblCopies
            // 
            this.lblCopies.AutoEllipsis = true;
            this.lblCopies.AutoSize = true;
            this.lblCopies.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCopies.Location = new System.Drawing.Point(269, 0);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblCopies.Size = new System.Drawing.Size(76, 13);
            this.lblCopies.TabIndex = 3;
            this.lblCopies.Text = "m of M copies";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblPageNoOfDocument);
            this.panel4.Controls.Add(this.lblDocumentName);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 16);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(345, 53);
            this.panel4.TabIndex = 4;
            // 
            // lblPageNoOfDocument
            // 
            this.lblPageNoOfDocument.AutoEllipsis = true;
            this.lblPageNoOfDocument.AutoSize = true;
            this.lblPageNoOfDocument.Location = new System.Drawing.Point(10, 30);
            this.lblPageNoOfDocument.Name = "lblPageNoOfDocument";
            this.lblPageNoOfDocument.Size = new System.Drawing.Size(96, 13);
            this.lblPageNoOfDocument.TabIndex = 10;
            this.lblPageNoOfDocument.Text = "Page of Document";
            // 
            // lblDocumentName
            // 
            this.lblDocumentName.AutoEllipsis = true;
            this.lblDocumentName.Location = new System.Drawing.Point(10, 8);
            this.lblDocumentName.Name = "lblDocumentName";
            this.lblDocumentName.Size = new System.Drawing.Size(315, 13);
            this.lblDocumentName.TabIndex = 4;
            this.lblDocumentName.Text = "Document Name";
            // 
            // gloPrintProgressController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(369, 188);
            this.ControlBox = false;
            this.Controls.Add(this.gbPrintingBox);
            this.Controls.Add(this.lblPrinterNameValue);
            this.Controls.Add(this.lblPrinterName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(375, 217);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(375, 217);
            this.Name = "gloPrintProgressController";
            this.ShowIcon = false;
            this.Text = "Print Job Progress";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.gloPrintProgressController_FormClosing);
            this.Load += new System.EventHandler(this.gloPrintProgressController_Load);
            this.Shown += new System.EventHandler(this.gloPrintProgressController_Shown);
            this.gbPrintingBox.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrinterName;
        private System.Windows.Forms.Label lblPrinterNameValue;
        private System.Windows.Forms.Label lblPages;
        private System.Windows.Forms.GroupBox gbPrintingBox;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDocumentName;
        private System.Windows.Forms.Label lblCopies;
        private System.Windows.Forms.ProgressBar pbDocument;
        private System.Windows.Forms.Label lblPageNoOfDocument;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Panel panel2;
    }
}