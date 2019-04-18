
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_Import
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_Import));
                this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
                this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
                this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
                this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
                this.tlb_AddPDF = new System.Windows.Forms.ToolStripButton();
                this.tlb_AddImages = new System.Windows.Forms.ToolStripButton();
                this.tlb_Remove = new System.Windows.Forms.ToolStripButton();
                this.pbDocument = new System.Windows.Forms.ProgressBar();
                this.txtDocumentName = new System.Windows.Forms.TextBox();
                this.label1 = new System.Windows.Forms.Label();
                this.lvwDocuments = new System.Windows.Forms.ListView();
                this.chkIsPDF = new System.Windows.Forms.CheckBox();
                this.panel1 = new System.Windows.Forms.Panel();
                this.Label5 = new System.Windows.Forms.Label();
                this.Label6 = new System.Windows.Forms.Label();
                this.Label7 = new System.Windows.Forms.Label();
                this.Label8 = new System.Windows.Forms.Label();
                this.chkSplitFile = new System.Windows.Forms.CheckBox();
                this.chkUseCompression = new System.Windows.Forms.CheckBox();
                this.panel2 = new System.Windows.Forms.Panel();
                this.tls_MaintainDoc.SuspendLayout();
                this.panel1.SuspendLayout();
                this.panel2.SuspendLayout();
                this.SuspendLayout();
                // 
                // tls_MaintainDoc
                // 
                this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
                this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
                this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_Cancel,
            this.toolStripSeparator1,
            this.tlb_AddPDF,
            this.tlb_AddImages,
            this.tlb_Remove});
                this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
                this.tls_MaintainDoc.Name = "tls_MaintainDoc";
                this.tls_MaintainDoc.Size = new System.Drawing.Size(410, 53);
                this.tls_MaintainDoc.TabIndex = 3;
                this.tls_MaintainDoc.Text = "toolStrip1";
                // 
                // tlb_Ok
                // 
                this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
                this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_Ok.Name = "tlb_Ok";
                this.tlb_Ok.Size = new System.Drawing.Size(66, 50);
                this.tlb_Ok.Tag = "OK";
                this.tlb_Ok.Text = "&Save&&Cls";
                this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_Ok.ToolTipText = "Save and Close";
                this.tlb_Ok.Click += new System.EventHandler(this.tlb_Ok_Click);
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
                // toolStripSeparator1
                // 
                this.toolStripSeparator1.AutoSize = false;
                this.toolStripSeparator1.Name = "toolStripSeparator1";
                this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
                // 
                // tlb_AddPDF
                // 
                this.tlb_AddPDF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_AddPDF.Image = ((System.Drawing.Image)(resources.GetObject("tlb_AddPDF.Image")));
                this.tlb_AddPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_AddPDF.Name = "tlb_AddPDF";
                this.tlb_AddPDF.Size = new System.Drawing.Size(63, 50);
                this.tlb_AddPDF.Tag = "AddPDF";
                this.tlb_AddPDF.Text = "Add &PDF";
                this.tlb_AddPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_AddPDF.ToolTipText = "Add PDF";
                this.tlb_AddPDF.Click += new System.EventHandler(this.tlb_AddPDF_Click);
                // 
                // tlb_AddImages
                // 
                this.tlb_AddImages.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_AddImages.Image = ((System.Drawing.Image)(resources.GetObject("tlb_AddImages.Image")));
                this.tlb_AddImages.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_AddImages.Name = "tlb_AddImages";
                this.tlb_AddImages.Size = new System.Drawing.Size(84, 50);
                this.tlb_AddImages.Tag = "AddImage";
                this.tlb_AddImages.Text = "Add &Images";
                this.tlb_AddImages.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_AddImages.ToolTipText = "Add Images";
                this.tlb_AddImages.Click += new System.EventHandler(this.tlb_AddImages_Click);
                // 
                // tlb_Remove
                // 
                this.tlb_Remove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Remove.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Remove.Image")));
                this.tlb_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_Remove.Name = "tlb_Remove";
                this.tlb_Remove.Size = new System.Drawing.Size(60, 50);
                this.tlb_Remove.Tag = "Remove";
                this.tlb_Remove.Text = "&Remove";
                this.tlb_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_Remove.ToolTipText = "Remove";
                this.tlb_Remove.Click += new System.EventHandler(this.tlb_Remove_Click);
                // 
                // pbDocument
                // 
                this.pbDocument.Location = new System.Drawing.Point(28, 192);
                this.pbDocument.Margin = new System.Windows.Forms.Padding(2);
                this.pbDocument.Name = "pbDocument";
                this.pbDocument.Size = new System.Drawing.Size(358, 18);
                this.pbDocument.TabIndex = 5;
                // 
                // txtDocumentName
                // 
                this.txtDocumentName.Location = new System.Drawing.Point(139, 157);
                this.txtDocumentName.Margin = new System.Windows.Forms.Padding(2);
                this.txtDocumentName.MaxLength = 150;
                this.txtDocumentName.Name = "txtDocumentName";
                this.txtDocumentName.Size = new System.Drawing.Size(247, 22);
                this.txtDocumentName.TabIndex = 15;
                this.txtDocumentName.TextChanged += new System.EventHandler(this.txtDocumentName_TextChanged);
                // 
                // label1
                // 
                this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.label1.AutoEllipsis = true;
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(29, 161);
                this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(107, 14);
                this.label1.TabIndex = 14;
                this.label1.Text = "Document Name :";
                this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                // 
                // lvwDocuments
                // 
                this.lvwDocuments.FullRowSelect = true;
                this.lvwDocuments.HideSelection = false;
                this.lvwDocuments.Location = new System.Drawing.Point(24, 46);
                this.lvwDocuments.Margin = new System.Windows.Forms.Padding(2);
                this.lvwDocuments.MultiSelect = false;
                this.lvwDocuments.Name = "lvwDocuments";
                this.lvwDocuments.Size = new System.Drawing.Size(362, 98);
                this.lvwDocuments.TabIndex = 16;
                this.lvwDocuments.UseCompatibleStateImageBehavior = false;
                this.lvwDocuments.View = System.Windows.Forms.View.Details;
                // 
                // chkIsPDF
                // 
                this.chkIsPDF.AutoSize = true;
                this.chkIsPDF.Location = new System.Drawing.Point(326, 22);
                this.chkIsPDF.Margin = new System.Windows.Forms.Padding(2);
                this.chkIsPDF.Name = "chkIsPDF";
                this.chkIsPDF.Size = new System.Drawing.Size(60, 18);
                this.chkIsPDF.TabIndex = 17;
                this.chkIsPDF.Text = "Is PDF";
                this.chkIsPDF.UseVisualStyleBackColor = true;
                this.chkIsPDF.Visible = false;
                // 
                // panel1
                // 
                this.panel1.BackColor = System.Drawing.Color.Transparent;
                this.panel1.Controls.Add(this.Label5);
                this.panel1.Controls.Add(this.Label6);
                this.panel1.Controls.Add(this.Label7);
                this.panel1.Controls.Add(this.Label8);
                this.panel1.Controls.Add(this.chkSplitFile);
                this.panel1.Controls.Add(this.chkUseCompression);
                this.panel1.Controls.Add(this.chkIsPDF);
                this.panel1.Controls.Add(this.lvwDocuments);
                this.panel1.Controls.Add(this.txtDocumentName);
                this.panel1.Controls.Add(this.label1);
                this.panel1.Controls.Add(this.pbDocument);
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Location = new System.Drawing.Point(0, 54);
                this.panel1.Margin = new System.Windows.Forms.Padding(2);
                this.panel1.Name = "panel1";
                this.panel1.Padding = new System.Windows.Forms.Padding(3);
                this.panel1.Size = new System.Drawing.Size(410, 227);
                this.panel1.TabIndex = 19;
                this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
                // 
                // Label5
                // 
                this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
                this.Label5.Location = new System.Drawing.Point(4, 223);
                this.Label5.Name = "Label5";
                this.Label5.Size = new System.Drawing.Size(402, 1);
                this.Label5.TabIndex = 31;
                this.Label5.Text = "label2";
                // 
                // Label6
                // 
                this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
                this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.Label6.Location = new System.Drawing.Point(3, 4);
                this.Label6.Name = "Label6";
                this.Label6.Size = new System.Drawing.Size(1, 220);
                this.Label6.TabIndex = 30;
                this.Label6.Text = "label4";
                // 
                // Label7
                // 
                this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
                this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
                this.Label7.Location = new System.Drawing.Point(406, 4);
                this.Label7.Name = "Label7";
                this.Label7.Size = new System.Drawing.Size(1, 220);
                this.Label7.TabIndex = 29;
                this.Label7.Text = "label3";
                // 
                // Label8
                // 
                this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
                this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.Label8.Location = new System.Drawing.Point(3, 3);
                this.Label8.Name = "Label8";
                this.Label8.Size = new System.Drawing.Size(404, 1);
                this.Label8.TabIndex = 28;
                this.Label8.Text = "label1";
                // 
                // chkSplitFile
                // 
                this.chkSplitFile.AutoSize = true;
                this.chkSplitFile.Checked = true;
                this.chkSplitFile.CheckState = System.Windows.Forms.CheckState.Checked;
                this.chkSplitFile.Location = new System.Drawing.Point(191, 22);
                this.chkSplitFile.Margin = new System.Windows.Forms.Padding(2);
                this.chkSplitFile.Name = "chkSplitFile";
                this.chkSplitFile.Size = new System.Drawing.Size(49, 18);
                this.chkSplitFile.TabIndex = 26;
                this.chkSplitFile.Text = "Split";
                this.chkSplitFile.UseVisualStyleBackColor = true;
                this.chkSplitFile.Visible = false;
                // 
                // chkUseCompression
                // 
                this.chkUseCompression.AutoSize = true;
                this.chkUseCompression.Location = new System.Drawing.Point(24, 22);
                this.chkUseCompression.Margin = new System.Windows.Forms.Padding(2);
                this.chkUseCompression.Name = "chkUseCompression";
                this.chkUseCompression.Size = new System.Drawing.Size(118, 18);
                this.chkUseCompression.TabIndex = 27;
                this.chkUseCompression.Text = "Use Compression";
                this.chkUseCompression.UseVisualStyleBackColor = true;
                this.chkUseCompression.Visible = false;
                // 
                // panel2
                // 
                this.panel2.Controls.Add(this.tls_MaintainDoc);
                this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
                this.panel2.Location = new System.Drawing.Point(0, 0);
                this.panel2.Name = "panel2";
                this.panel2.Size = new System.Drawing.Size(410, 54);
                this.panel2.TabIndex = 32;
                // 
                // frmEDocEvent_Import
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                this.AutoSize = true;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.ClientSize = new System.Drawing.Size(410, 281);
                this.Controls.Add(this.panel1);
                this.Controls.Add(this.panel2);
                this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "frmEDocEvent_Import";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Import";
                this.Load += new System.EventHandler(this.frmEDocEvent_Import_Load);
                this.tls_MaintainDoc.ResumeLayout(false);
                this.tls_MaintainDoc.PerformLayout();
                this.panel1.ResumeLayout(false);
                this.panel1.PerformLayout();
                this.panel2.ResumeLayout(false);
                this.panel2.PerformLayout();
                this.ResumeLayout(false);

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Ok;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ProgressBar pbDocument;
            private System.Windows.Forms.TextBox txtDocumentName;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.ListView lvwDocuments;
            private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            private System.Windows.Forms.ToolStripButton tlb_AddPDF;
            private System.Windows.Forms.ToolStripButton tlb_Remove;
            private System.Windows.Forms.ToolStripButton tlb_AddImages;
            private System.Windows.Forms.CheckBox chkIsPDF;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.CheckBox chkSplitFile;
            private System.Windows.Forms.CheckBox chkUseCompression;
            private System.Windows.Forms.Label Label5;
            private System.Windows.Forms.Label Label6;
            private System.Windows.Forms.Label Label7;
            private System.Windows.Forms.Label Label8;
            private System.Windows.Forms.Panel panel2;
        }
    }
