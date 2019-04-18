
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_Print
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
                    if (_SelectedDocuments != null)
                    {
                        _SelectedDocuments.Dispose();
                        _SelectedDocuments = null;
                    }
                 
                    components.Dispose();
                    //try
                    //{
                    //    if (PrintDialog1 != null)
                    //    {
                    //        PrintDialog1.Dispose();
                    //        PrintDialog1 = null;
                    //    }
                    //}
                    //catch
                    //{
                    //}
                    //try
                    //{
                    //    if (printDocument1 != null)
                    //    {
                    //        //try
                    //        //{
                    //        //    printDocument1.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
                    //        //    printDocument1.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
                    //        //}
                    //        //catch
                    //        //{
                    //        //}

                    //        printDocument1.Dispose();
                    //        printDocument1 = null;
                    //    }
                    //}
                    //catch
                    //{
                    //}
                    try
                    {
                        if (ogloSettings != null)
                        {
                            ogloSettings.Dispose();
                            ogloSettings = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (oSourceDocSelectedPages != null)
                        {
                            oSourceDocSelectedPages.Clear();
                            oSourceDocSelectedPages = null;
                        }
                    }
                    catch
                    {
                    }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_Print));
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.txtPrintStatus = new System.Windows.Forms.TextBox();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Print = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            //this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            //this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlDPISetting = new System.Windows.Forms.Panel();
            this.Label6 = new System.Windows.Forms.Label();
            this.numCustomDPIValue = new System.Windows.Forms.NumericUpDown();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.rbDefaultDPI = new System.Windows.Forms.RadioButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.rbCustomDPI = new System.Windows.Forms.RadioButton();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.tls_MaintainDoc.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlDPISetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomDPIValue)).BeginInit();
            this.SuspendLayout();
            // 
            // pbDocument
            // 
            this.pbDocument.Location = new System.Drawing.Point(10, 165);
            this.pbDocument.Margin = new System.Windows.Forms.Padding(2);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(365, 18);
            this.pbDocument.TabIndex = 5;
            // 
            // txtPrintStatus
            // 
            this.txtPrintStatus.BackColor = System.Drawing.Color.White;
            this.txtPrintStatus.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtPrintStatus.Enabled = false;
            this.txtPrintStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrintStatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPrintStatus.Location = new System.Drawing.Point(10, 12);
            this.txtPrintStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrintStatus.Multiline = true;
            this.txtPrintStatus.Name = "txtPrintStatus";
            this.txtPrintStatus.ReadOnly = true;
            this.txtPrintStatus.Size = new System.Drawing.Size(366, 145);
            this.txtPrintStatus.TabIndex = 15;
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Print,
            this.tlb_Cancel});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(385, 53);
            this.tls_MaintainDoc.TabIndex = 3;
            this.tls_MaintainDoc.Text = "toolStrip1";
            // 
            // tlb_Print
            // 
            this.tlb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Print.Image")));
            this.tlb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Print.Name = "tlb_Print";
            this.tlb_Print.Size = new System.Drawing.Size(41, 50);
            this.tlb_Print.Tag = "OK";
            this.tlb_Print.Text = "&Print";
            this.tlb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Print.ToolTipText = "Print";
            this.tlb_Print.Click += new System.EventHandler(this.tlb_Ok_Click);
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
            this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Cancel.Name = "tlb_Cancel";
            this.tlb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tlb_Cancel.Tag = "Cancel";
            this.tlb_Cancel.Text = "&Close";
            this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Cancel.ToolTipText = "Close";
            this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
            // 
            // printDocument1
            // 
            //this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            //this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Controls.Add(this.pbDocument);
            this.panel1.Controls.Add(this.txtPrintStatus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 107);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(385, 187);
            this.panel1.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 183);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(377, 1);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(377, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(381, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 181);
            this.label1.TabIndex = 23;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.Black;
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 181);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // PrintDialog1
            // 
            //this.PrintDialog1.UseEXDialog = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tls_MaintainDoc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(385, 54);
            this.panel2.TabIndex = 26;
            // 
            // pnlDPISetting
            // 
            this.pnlDPISetting.Controls.Add(this.Label6);
            this.pnlDPISetting.Controls.Add(this.numCustomDPIValue);
            this.pnlDPISetting.Controls.Add(this.ProgressBar1);
            this.pnlDPISetting.Controls.Add(this.rbDefaultDPI);
            this.pnlDPISetting.Controls.Add(this.Label5);
            this.pnlDPISetting.Controls.Add(this.rbCustomDPI);
            this.pnlDPISetting.Controls.Add(this.Label7);
            this.pnlDPISetting.Controls.Add(this.Label8);
            this.pnlDPISetting.Controls.Add(this.Label9);
            this.pnlDPISetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDPISetting.Location = new System.Drawing.Point(0, 54);
            this.pnlDPISetting.Name = "pnlDPISetting";
            this.pnlDPISetting.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlDPISetting.Size = new System.Drawing.Size(385, 53);
            this.pnlDPISetting.TabIndex = 27;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(8, 8);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(75, 14);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "Printer DPI";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numCustomDPIValue
            // 
            this.numCustomDPIValue.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numCustomDPIValue.Location = new System.Drawing.Point(266, 25);
            this.numCustomDPIValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCustomDPIValue.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numCustomDPIValue.Name = "numCustomDPIValue";
            this.numCustomDPIValue.Size = new System.Drawing.Size(78, 22);
            this.numCustomDPIValue.TabIndex = 14;
            this.numCustomDPIValue.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.BackColor = System.Drawing.Color.White;
            this.ProgressBar1.ForeColor = System.Drawing.Color.LimeGreen;
            this.ProgressBar1.Location = new System.Drawing.Point(11, 129);
            this.ProgressBar1.Maximum = 250;
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(352, 14);
            this.ProgressBar1.Step = 25;
            this.ProgressBar1.TabIndex = 11;
            // 
            // rbDefaultDPI
            // 
            this.rbDefaultDPI.AutoSize = true;
            this.rbDefaultDPI.Location = new System.Drawing.Point(27, 27);
            this.rbDefaultDPI.Name = "rbDefaultDPI";
            this.rbDefaultDPI.Size = new System.Drawing.Size(87, 18);
            this.rbDefaultDPI.TabIndex = 13;
            this.rbDefaultDPI.TabStop = true;
            this.rbDefaultDPI.Text = "Default DPI";
            this.rbDefaultDPI.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Location = new System.Drawing.Point(4, 52);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(377, 1);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Label5";
            // 
            // rbCustomDPI
            // 
            this.rbCustomDPI.AutoSize = true;
            this.rbCustomDPI.Location = new System.Drawing.Point(174, 27);
            this.rbCustomDPI.Name = "rbCustomDPI";
            this.rbCustomDPI.Size = new System.Drawing.Size(89, 18);
            this.rbCustomDPI.TabIndex = 12;
            this.rbCustomDPI.TabStop = true;
            this.rbCustomDPI.Text = "Custom DPI";
            this.rbCustomDPI.UseVisualStyleBackColor = true;
            this.rbCustomDPI.CheckedChanged += new System.EventHandler(this.rbCustomDPI_CheckedChanged);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label7.Location = new System.Drawing.Point(4, 3);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(377, 1);
            this.Label7.TabIndex = 2;
            this.Label7.Text = "Label7";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label8.Location = new System.Drawing.Point(381, 3);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(1, 50);
            this.Label8.TabIndex = 1;
            this.Label8.Text = "Label8";
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label9.Location = new System.Drawing.Point(3, 3);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(1, 50);
            this.Label9.TabIndex = 0;
            this.Label9.Text = "Label9";
            // 
            // frmEDocEvent_Print
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(385, 294);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDPISetting);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocEvent_Print";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print";
            this.Load += new System.EventHandler(this.frmEDocEvent_Print_Load);
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlDPISetting.ResumeLayout(false);
            this.pnlDPISetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomDPIValue)).EndInit();
            this.ResumeLayout(false);

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Print;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ProgressBar pbDocument;
            private System.Windows.Forms.TextBox txtPrintStatus;
            //private System.Drawing.Printing.PrintDocument printDocument1;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label59;
            //internal System.Windows.Forms.PrintDialog PrintDialog1;
            private System.Windows.Forms.Panel panel2;
            internal System.Windows.Forms.Panel pnlDPISetting;
            internal System.Windows.Forms.Label Label6;
            internal System.Windows.Forms.NumericUpDown numCustomDPIValue;
            internal System.Windows.Forms.ProgressBar ProgressBar1;
            internal System.Windows.Forms.RadioButton rbDefaultDPI;
            internal System.Windows.Forms.Label Label5;
            internal System.Windows.Forms.RadioButton rbCustomDPI;
            internal System.Windows.Forms.Label Label7;
            internal System.Windows.Forms.Label Label8;
            internal System.Windows.Forms.Label Label9;
        }
    }
