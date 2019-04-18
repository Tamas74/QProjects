namespace gloPrintDialog
{
    partial class frmgloPrintQueueController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmgloPrintQueueController));
            this.btnRestart = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPages = new System.Windows.Forms.Label();
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.Label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPageNoOfDocument = new System.Windows.Forms.Label();
            this.lblCopies = new System.Windows.Forms.Label();
            this.lblPrinterNameValue = new System.Windows.Forms.Label();
            this.lblPrinterName = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRestart
            // 
            this.btnRestart.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRestart.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRestart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRestart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Image = ((System.Drawing.Image)(resources.GetObject("btnRestart.Image")));
            this.btnRestart.Location = new System.Drawing.Point(205, 23);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(31, 29);
            this.btnRestart.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.btnRestart, "Restart");
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRestart);
            this.panel3.Controls.Add(this.btnPause);
            this.panel3.Controls.Add(this.btnPlay);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.lblPages);
            this.panel3.Controls.Add(this.pbDocument);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 37);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(339, 80);
            this.panel3.TabIndex = 11;
            // 
            // btnPause
            // 
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPause.Enabled = false;
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(236, 23);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(31, 29);
            this.btnPause.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.btnPause, "Pause");
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPlay.Enabled = false;
            this.btnPlay.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(267, 23);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(31, 29);
            this.btnPlay.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.btnPlay, "Play");
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Visible = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(298, 23);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(31, 29);
            this.btnCancel.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.btnCancel, "Close");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPages
            // 
            this.lblPages.AutoEllipsis = true;
            this.lblPages.AutoSize = true;
            this.lblPages.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPages.Location = new System.Drawing.Point(10, 10);
            this.lblPages.Name = "lblPages";
            this.lblPages.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lblPages.Size = new System.Drawing.Size(78, 13);
            this.lblPages.TabIndex = 2;
            this.lblPages.Text = "Please Wait... ";
            // 
            // pbDocument
            // 
            this.pbDocument.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbDocument.Location = new System.Drawing.Point(10, 52);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(319, 18);
            this.pbDocument.TabIndex = 9;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Silver;
            this.Label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label5.Location = new System.Drawing.Point(1, 36);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(339, 1);
            this.Label5.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPageNoOfDocument);
            this.panel1.Controls.Add(this.lblCopies);
            this.panel1.Controls.Add(this.lblPrinterNameValue);
            this.panel1.Controls.Add(this.lblPrinterName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(339, 35);
            this.panel1.TabIndex = 12;
            // 
            // lblPageNoOfDocument
            // 
            this.lblPageNoOfDocument.AutoSize = true;
            this.lblPageNoOfDocument.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPageNoOfDocument.Location = new System.Drawing.Point(326, 10);
            this.lblPageNoOfDocument.Name = "lblPageNoOfDocument";
            this.lblPageNoOfDocument.Size = new System.Drawing.Size(0, 13);
            this.lblPageNoOfDocument.TabIndex = 4;
            // 
            // lblCopies
            // 
            this.lblCopies.AutoEllipsis = true;
            this.lblCopies.AutoSize = true;
            this.lblCopies.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCopies.Location = new System.Drawing.Point(326, 10);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblCopies.Size = new System.Drawing.Size(3, 13);
            this.lblCopies.TabIndex = 3;
            // 
            // lblPrinterNameValue
            // 
            this.lblPrinterNameValue.AutoEllipsis = true;
            this.lblPrinterNameValue.AutoSize = true;
            this.lblPrinterNameValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPrinterNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinterNameValue.Location = new System.Drawing.Point(74, 10);
            this.lblPrinterNameValue.Name = "lblPrinterNameValue";
            this.lblPrinterNameValue.Size = new System.Drawing.Size(77, 13);
            this.lblPrinterNameValue.TabIndex = 8;
            this.lblPrinterNameValue.Text = "printer name";
            // 
            // lblPrinterName
            // 
            this.lblPrinterName.AutoSize = true;
            this.lblPrinterName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPrinterName.Location = new System.Drawing.Point(10, 10);
            this.lblPrinterName.Name = "lblPrinterName";
            this.lblPrinterName.Size = new System.Drawing.Size(64, 13);
            this.lblPrinterName.TabIndex = 7;
            this.lblPrinterName.Text = "Printing To :";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Silver;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label1.Location = new System.Drawing.Point(340, 1);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(1, 116);
            this.Label1.TabIndex = 13;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Silver;
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Location = new System.Drawing.Point(0, 1);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 116);
            this.Label2.TabIndex = 14;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Silver;
            this.Label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label3.Location = new System.Drawing.Point(0, 117);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(341, 1);
            this.Label3.TabIndex = 15;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Silver;
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Location = new System.Drawing.Point(0, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(341, 1);
            this.Label4.TabIndex = 16;
            // 
            // frmobjgloPrintQueueController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 118);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label4);
            this.Name = "frmobjgloPrintQueueController";
            this.Text = "frmPrintQueueController";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmgloPrintQueueController_FormClosed);
            this.Load += new System.EventHandler(this.frmobjgloPrintQueueController_Load);
            this.Shown += new System.EventHandler(this.frmgloPrintQueueController_Shown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRestart;
        internal System.Windows.Forms.ToolTip ToolTip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPages;
        private System.Windows.Forms.ProgressBar pbDocument;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label lblPageNoOfDocument;
        private System.Windows.Forms.Label lblCopies;
        private System.Windows.Forms.Label lblPrinterNameValue;
        private System.Windows.Forms.Label lblPrinterName;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label4;
    }
}