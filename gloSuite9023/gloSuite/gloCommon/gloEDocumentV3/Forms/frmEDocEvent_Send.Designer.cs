
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_Send
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_Send));
                this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
                this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
                this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
                this.pbDocument = new System.Windows.Forms.ProgressBar();
                this.lblSearch = new System.Windows.Forms.Label();
                this.txtSearch = new System.Windows.Forms.TextBox();
                this.txtDocumentName = new System.Windows.Forms.TextBox();
                this.label2 = new System.Windows.Forms.Label();
                this.lvwDocuments = new System.Windows.Forms.ListView();
                this.panel1 = new System.Windows.Forms.Panel();
                this.label3 = new System.Windows.Forms.Label();
                this.label1 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.label59 = new System.Windows.Forms.Label();
                this.tls_MaintainDoc.SuspendLayout();
                this.panel1.SuspendLayout();
                this.SuspendLayout();
                // 
                // tls_MaintainDoc
                // 
                this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
                this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
                this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Ok,
            this.tlb_Cancel});
                this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
                this.tls_MaintainDoc.Name = "tls_MaintainDoc";
                this.tls_MaintainDoc.Size = new System.Drawing.Size(389, 53);
                this.tls_MaintainDoc.TabIndex = 3;
                this.tls_MaintainDoc.Text = "toolStrip1";
                // 
                // tlb_Ok
                // 
                this.tlb_Ok.BackColor = System.Drawing.Color.Transparent;
                this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
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
                this.tlb_Cancel.BackColor = System.Drawing.Color.Transparent;
                this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
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
                // pbDocument
                // 
                this.pbDocument.Location = new System.Drawing.Point(26, 184);
                this.pbDocument.Name = "pbDocument";
                this.pbDocument.Size = new System.Drawing.Size(331, 19);
                this.pbDocument.TabIndex = 5;
                // 
                // lblSearch
                // 
                this.lblSearch.AutoSize = true;
                this.lblSearch.Location = new System.Drawing.Point(78, 20);
                this.lblSearch.Name = "lblSearch";
                this.lblSearch.Size = new System.Drawing.Size(52, 14);
                this.lblSearch.TabIndex = 6;
                this.lblSearch.Text = "Search :";
                this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                // 
                // txtSearch
                // 
                this.txtSearch.ForeColor = System.Drawing.Color.Black;
                this.txtSearch.Location = new System.Drawing.Point(133, 16);
                this.txtSearch.Name = "txtSearch";
                this.txtSearch.Size = new System.Drawing.Size(224, 22);
                this.txtSearch.TabIndex = 7;
                this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
                // 
                // txtDocumentName
                // 
                this.txtDocumentName.ForeColor = System.Drawing.Color.Black;
                this.txtDocumentName.Location = new System.Drawing.Point(133, 152);
                this.txtDocumentName.Name = "txtDocumentName";
                this.txtDocumentName.Size = new System.Drawing.Size(224, 22);
                this.txtDocumentName.TabIndex = 9;
                this.txtDocumentName.TextChanged += new System.EventHandler(this.txtDocumentName_TextChanged);
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(23, 156);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(107, 14);
                this.label2.TabIndex = 8;
                this.label2.Text = "Document Name :";
                this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                // 
                // lvwDocuments
                // 
                this.lvwDocuments.ForeColor = System.Drawing.Color.Black;
                this.lvwDocuments.FullRowSelect = true;
                this.lvwDocuments.HideSelection = false;
                this.lvwDocuments.Location = new System.Drawing.Point(133, 44);
                this.lvwDocuments.MultiSelect = false;
                this.lvwDocuments.Name = "lvwDocuments";
                this.lvwDocuments.Size = new System.Drawing.Size(224, 97);
                this.lvwDocuments.TabIndex = 10;
                this.lvwDocuments.UseCompatibleStateImageBehavior = false;
                this.lvwDocuments.SelectedIndexChanged += new System.EventHandler(this.lvwDocuments_SelectedIndexChanged);
                // 
                // panel1
                // 
                this.panel1.BackColor = System.Drawing.Color.Transparent;
                this.panel1.Controls.Add(this.label3);
                this.panel1.Controls.Add(this.label1);
                this.panel1.Controls.Add(this.label4);
                this.panel1.Controls.Add(this.label59);
                this.panel1.Controls.Add(this.lvwDocuments);
                this.panel1.Controls.Add(this.txtDocumentName);
                this.panel1.Controls.Add(this.label2);
                this.panel1.Controls.Add(this.txtSearch);
                this.panel1.Controls.Add(this.lblSearch);
                this.panel1.Controls.Add(this.pbDocument);
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Location = new System.Drawing.Point(0, 53);
                this.panel1.Name = "panel1";
                this.panel1.Padding = new System.Windows.Forms.Padding(3);
                this.panel1.Size = new System.Drawing.Size(389, 215);
                this.panel1.TabIndex = 19;
                // 
                // label3
                // 
                this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.label3.Location = new System.Drawing.Point(4, 211);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(381, 1);
                this.label3.TabIndex = 25;
                this.label3.Text = "label3";
                // 
                // label1
                // 
                this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label1.Dock = System.Windows.Forms.DockStyle.Top;
                this.label1.Location = new System.Drawing.Point(4, 3);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(381, 1);
                this.label1.TabIndex = 24;
                this.label1.Text = "label1";
                // 
                // label4
                // 
                this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label4.Dock = System.Windows.Forms.DockStyle.Right;
                this.label4.Location = new System.Drawing.Point(385, 3);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(1, 209);
                this.label4.TabIndex = 23;
                this.label4.Text = "label4";
                // 
                // label59
                // 
                this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label59.Dock = System.Windows.Forms.DockStyle.Left;
                this.label59.Location = new System.Drawing.Point(3, 3);
                this.label59.Name = "label59";
                this.label59.Size = new System.Drawing.Size(1, 209);
                this.label59.TabIndex = 22;
                this.label59.Text = "label59";
                // 
                // frmEDocEvent_Send
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.ClientSize = new System.Drawing.Size(389, 268);
                this.Controls.Add(this.panel1);
                this.Controls.Add(this.tls_MaintainDoc);
                this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "frmEDocEvent_Send";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Send";
                this.Load += new System.EventHandler(this.frmEDocEvent_Send_Load);
                this.tls_MaintainDoc.ResumeLayout(false);
                this.tls_MaintainDoc.PerformLayout();
                this.panel1.ResumeLayout(false);
                this.panel1.PerformLayout();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Ok;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ProgressBar pbDocument;
            private System.Windows.Forms.Label lblSearch;
            private System.Windows.Forms.TextBox txtSearch;
            private System.Windows.Forms.TextBox txtDocumentName;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.ListView lvwDocuments;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label59;
        }
    }
