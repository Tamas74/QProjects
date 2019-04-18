
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocumentArchiveViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocumentArchiveViewer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowserArchiveDocument = new System.Windows.Forms.WebBrowser();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Print = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tls_MaintainDoc.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            this.panel19.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.webBrowserArchiveDocument);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 117);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(804, 571);
            this.panel1.TabIndex = 19;
            // 
            // webBrowserArchiveDocument
            // 
            this.webBrowserArchiveDocument.AllowWebBrowserDrop = false;
            this.webBrowserArchiveDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserArchiveDocument.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserArchiveDocument.Location = new System.Drawing.Point(4, 4);
            this.webBrowserArchiveDocument.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserArchiveDocument.Name = "webBrowserArchiveDocument";
            this.webBrowserArchiveDocument.ScriptErrorsSuppressed = true;
            this.webBrowserArchiveDocument.Size = new System.Drawing.Size(796, 563);
            this.webBrowserArchiveDocument.TabIndex = 26;
            this.webBrowserArchiveDocument.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowserArchiveDocument.WebBrowserShortcutsEnabled = false;
            this.webBrowserArchiveDocument.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserArchiveDocument_DocumentCompleted);
            this.webBrowserArchiveDocument.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserArchiveDocument_Navigated);
            this.webBrowserArchiveDocument.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowserArchiveDocument_Navigating);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 567);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(796, 1);
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
            this.label2.Size = new System.Drawing.Size(796, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(800, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 565);
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
            this.label59.Size = new System.Drawing.Size(1, 565);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tls_MaintainDoc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(804, 54);
            this.panel2.TabIndex = 26;
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
            this.tls_MaintainDoc.Size = new System.Drawing.Size(804, 53);
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
            this.tlb_Print.Click += new System.EventHandler(this.tlb_Print_Click);
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
            // pnlPatients
            // 
            this.pnlPatients.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatients.Controls.Add(this.panel19);
            this.pnlPatients.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatients.Location = new System.Drawing.Point(0, 54);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Padding = new System.Windows.Forms.Padding(3);
            this.pnlPatients.Size = new System.Drawing.Size(804, 63);
            this.pnlPatients.TabIndex = 28;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.label16);
            this.panel19.Controls.Add(this.label15);
            this.panel19.Controls.Add(this.label14);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(3, 3);
            this.panel19.Name = "panel19";
            this.panel19.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.panel19.Size = new System.Drawing.Size(798, 57);
            this.panel19.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 56);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(793, 1);
            this.label16.TabIndex = 10;
            this.label16.Text = "label4";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(794, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 57);
            this.label15.TabIndex = 9;
            this.label15.Text = "label4";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 57);
            this.label14.TabIndex = 8;
            this.label14.Text = "label4";
            // 
            // frmEDocumentArchiveViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(804, 688);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlPatients);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocumentArchiveViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Archived Document Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEDocEvent_Print_Load);
            this.Shown += new System.EventHandler(this.frmEDocumentArchiveViewer_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmEDocumentArchiveViewer_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.pnlPatients.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.ResumeLayout(false);

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Print;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            //private System.Drawing.Printing.PrintDocument printDocument1;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label59;
            //internal System.Windows.Forms.PrintDialog PrintDialog1;
            private System.Windows.Forms.Panel panel2;
            private System.Windows.Forms.WebBrowser webBrowserArchiveDocument;
            private System.Windows.Forms.Panel pnlPatients;
            private System.Windows.Forms.Panel panel19;
            private System.Windows.Forms.Label label16;
            private System.Windows.Forms.Label label15;
            private System.Windows.Forms.Label label14;
        }
    }
