
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_Rename
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
                    _SelectedDocuments.Dispose();
                    _EventParameter.Dispose();
                    //_ActionDocument.Dispose();
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_Rename));
                this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
                this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
                this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
                this.txtSourceDocument = new System.Windows.Forms.TextBox();
                this.label1 = new System.Windows.Forms.Label();
                this.panel1 = new System.Windows.Forms.Panel();
                this.label3 = new System.Windows.Forms.Label();
                this.label2 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.label59 = new System.Windows.Forms.Label();
                this.panel2 = new System.Windows.Forms.Panel();
                this.tls_MaintainDoc.SuspendLayout();
                this.panel1.SuspendLayout();
                this.panel2.SuspendLayout();
                this.SuspendLayout();
                // 
                // tls_MaintainDoc
                // 
                this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
                this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
                this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_Cancel});
                this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
                this.tls_MaintainDoc.Name = "tls_MaintainDoc";
                this.tls_MaintainDoc.Size = new System.Drawing.Size(315, 53);
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
                this.tlb_Cancel.Size = new System.Drawing.Size(47, 50);
                this.tlb_Cancel.Tag = "Cancel";
                this.tlb_Cancel.Text = " &Close";
                this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_Cancel.ToolTipText = "Close";
                this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
                // 
                // txtSourceDocument
                // 
                this.txtSourceDocument.Location = new System.Drawing.Point(82, 49);
                this.txtSourceDocument.Margin = new System.Windows.Forms.Padding(2);
                this.txtSourceDocument.MaxLength = 150;
                this.txtSourceDocument.Name = "txtSourceDocument";
                this.txtSourceDocument.Size = new System.Drawing.Size(206, 22);
                this.txtSourceDocument.TabIndex = 16;
                this.txtSourceDocument.TextChanged += new System.EventHandler(this.txtSourceDocument_TextChanged);
                // 
                // label1
                // 
                this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                            | System.Windows.Forms.AnchorStyles.Left)
                            | System.Windows.Forms.AnchorStyles.Right)));
                this.label1.AutoEllipsis = true;
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(19, 53);
                this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(59, 14);
                this.label1.TabIndex = 15;
                this.label1.Text = "Rename :";
                this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                // 
                // panel1
                // 
                this.panel1.BackColor = System.Drawing.Color.Transparent;
                this.panel1.Controls.Add(this.label3);
                this.panel1.Controls.Add(this.label2);
                this.panel1.Controls.Add(this.label4);
                this.panel1.Controls.Add(this.label59);
                this.panel1.Controls.Add(this.txtSourceDocument);
                this.panel1.Controls.Add(this.label1);
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Location = new System.Drawing.Point(0, 54);
                this.panel1.Margin = new System.Windows.Forms.Padding(2);
                this.panel1.Name = "panel1";
                this.panel1.Padding = new System.Windows.Forms.Padding(3);
                this.panel1.Size = new System.Drawing.Size(315, 160);
                this.panel1.TabIndex = 19;
                // 
                // label3
                // 
                this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.label3.Location = new System.Drawing.Point(4, 156);
                this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(307, 1);
                this.label3.TabIndex = 25;
                this.label3.Text = "label3";
                // 
                // label2
                // 
                this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label2.Dock = System.Windows.Forms.DockStyle.Top;
                this.label2.Location = new System.Drawing.Point(4, 3);
                this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(307, 1);
                this.label2.TabIndex = 24;
                this.label2.Text = "label2";
                // 
                // label4
                // 
                this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label4.Dock = System.Windows.Forms.DockStyle.Right;
                this.label4.Location = new System.Drawing.Point(311, 3);
                this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(1, 154);
                this.label4.TabIndex = 23;
                this.label4.Text = "label4";
                // 
                // label59
                // 
                this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label59.Dock = System.Windows.Forms.DockStyle.Left;
                this.label59.Location = new System.Drawing.Point(3, 3);
                this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label59.Name = "label59";
                this.label59.Size = new System.Drawing.Size(1, 154);
                this.label59.TabIndex = 22;
                this.label59.Text = "label59";
                // 
                // panel2
                // 
                this.panel2.Controls.Add(this.tls_MaintainDoc);
                this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
                this.panel2.Location = new System.Drawing.Point(0, 0);
                this.panel2.Name = "panel2";
                this.panel2.Size = new System.Drawing.Size(315, 54);
                this.panel2.TabIndex = 26;
                // 
                // frmEDocEvent_Rename
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                this.AutoSize = true;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.ClientSize = new System.Drawing.Size(315, 214);
                this.Controls.Add(this.panel1);
                this.Controls.Add(this.panel2);
                this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "frmEDocEvent_Rename";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Rename";
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
            private System.Windows.Forms.TextBox txtSourceDocument;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label59;
            private System.Windows.Forms.Panel panel2;
        }
    }
