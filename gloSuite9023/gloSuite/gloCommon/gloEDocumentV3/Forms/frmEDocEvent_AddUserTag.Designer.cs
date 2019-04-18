
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_AddUserTag
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_AddUserTag));
                this.pbDocument = new System.Windows.Forms.ProgressBar();
                this.txtUserTags = new System.Windows.Forms.TextBox();
                this.lvwUserTags = new System.Windows.Forms.ListView();
                this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
                this.tlb_UserTag = new System.Windows.Forms.ToolStripButton();
                this.tlb_History = new System.Windows.Forms.ToolStripButton();
                this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
                this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
                this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
                this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
                this.panel1 = new System.Windows.Forms.Panel();
                this.label3 = new System.Windows.Forms.Label();
                this.label2 = new System.Windows.Forms.Label();
                this.label1 = new System.Windows.Forms.Label();
                this.label59 = new System.Windows.Forms.Label();
                this.tls_MaintainDoc.SuspendLayout();
                this.panel1.SuspendLayout();
                this.SuspendLayout();
                // 
                // pbDocument
                // 
                this.pbDocument.Location = new System.Drawing.Point(11, 167);
                this.pbDocument.Margin = new System.Windows.Forms.Padding(2);
                this.pbDocument.Name = "pbDocument";
                this.pbDocument.Size = new System.Drawing.Size(365, 18);
                this.pbDocument.TabIndex = 5;
                // 
                // txtUserTags
                // 
                this.txtUserTags.Location = new System.Drawing.Point(10, 10);
                this.txtUserTags.Margin = new System.Windows.Forms.Padding(2);
                this.txtUserTags.MaxLength = 255;
                this.txtUserTags.Multiline = true;
                this.txtUserTags.Name = "txtUserTags";
                this.txtUserTags.Size = new System.Drawing.Size(366, 149);
                this.txtUserTags.TabIndex = 15;
                // 
                // lvwUserTags
                // 
                this.lvwUserTags.FullRowSelect = true;
                this.lvwUserTags.HideSelection = false;
                this.lvwUserTags.Location = new System.Drawing.Point(10, 10);
                this.lvwUserTags.Margin = new System.Windows.Forms.Padding(2);
                this.lvwUserTags.MultiSelect = false;
                this.lvwUserTags.Name = "lvwUserTags";
                this.lvwUserTags.Size = new System.Drawing.Size(366, 148);
                this.lvwUserTags.TabIndex = 16;
                this.lvwUserTags.UseCompatibleStateImageBehavior = false;
                this.lvwUserTags.View = System.Windows.Forms.View.Details;
                // 
                // tls_MaintainDoc
                // 
                this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
                this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
                this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_UserTag,
            this.tlb_Ok,
            this.tlb_History,
            this.tlb_Delete,
            this.toolStripSeparator1,
            this.tlb_Cancel});
                this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
                this.tls_MaintainDoc.Name = "tls_MaintainDoc";
                this.tls_MaintainDoc.Size = new System.Drawing.Size(388, 53);
                this.tls_MaintainDoc.TabIndex = 3;
                this.tls_MaintainDoc.Text = "toolStrip1";
                // 
                // tlb_UserTag
                // 
                this.tlb_UserTag.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_UserTag.Image = ((System.Drawing.Image)(resources.GetObject("tlb_UserTag.Image")));
                this.tlb_UserTag.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_UserTag.Name = "tlb_UserTag";
                this.tlb_UserTag.Size = new System.Drawing.Size(68, 50);
                this.tlb_UserTag.Tag = "AddPDF";
                this.tlb_UserTag.Text = "&Add Tags";
                this.tlb_UserTag.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_UserTag.ToolTipText = "Add Tags";
                this.tlb_UserTag.Click += new System.EventHandler(this.tlb_Notes_Click);
                // 
                // tlb_History
                // 
                this.tlb_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_History.Image = ((System.Drawing.Image)(resources.GetObject("tlb_History.Image")));
                this.tlb_History.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_History.Name = "tlb_History";
                this.tlb_History.Size = new System.Drawing.Size(55, 50);
                this.tlb_History.Tag = "AddPDF";
                this.tlb_History.Text = "&History";
                this.tlb_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_History.ToolTipText = "History";
                this.tlb_History.Visible = false;
                this.tlb_History.Click += new System.EventHandler(this.tlb_History_Click);
                // 
                // tlb_Delete
                // 
                this.tlb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Delete.Image")));
                this.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_Delete.Name = "tlb_Delete";
                this.tlb_Delete.Size = new System.Drawing.Size(50, 50);
                this.tlb_Delete.Tag = "AddPDF";
                this.tlb_Delete.Text = "&Delete";
                this.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_Delete.ToolTipText = "Delete";
                this.tlb_Delete.Visible = false;
                this.tlb_Delete.Click += new System.EventHandler(this.tlb_Delete_Click);
                // 
                // toolStripSeparator1
                // 
                this.toolStripSeparator1.AutoSize = false;
                this.toolStripSeparator1.Name = "toolStripSeparator1";
                this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
                this.toolStripSeparator1.Visible = false;
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
                // panel1
                // 
                this.panel1.BackColor = System.Drawing.Color.Transparent;
                this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.panel1.Controls.Add(this.label3);
                this.panel1.Controls.Add(this.label2);
                this.panel1.Controls.Add(this.label1);
                this.panel1.Controls.Add(this.txtUserTags);
                this.panel1.Controls.Add(this.label59);
                this.panel1.Controls.Add(this.pbDocument);
                this.panel1.Controls.Add(this.lvwUserTags);
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Location = new System.Drawing.Point(0, 53);
                this.panel1.Margin = new System.Windows.Forms.Padding(2);
                this.panel1.Name = "panel1";
                this.panel1.Padding = new System.Windows.Forms.Padding(3);
                this.panel1.Size = new System.Drawing.Size(388, 196);
                this.panel1.TabIndex = 19;
                // 
                // label3
                // 
                this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.label3.Location = new System.Drawing.Point(4, 192);
                this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(380, 1);
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
                this.label2.Size = new System.Drawing.Size(380, 1);
                this.label2.TabIndex = 24;
                this.label2.Text = "label2";
                // 
                // label1
                // 
                this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label1.Dock = System.Windows.Forms.DockStyle.Right;
                this.label1.Location = new System.Drawing.Point(384, 3);
                this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(1, 190);
                this.label1.TabIndex = 23;
                this.label1.Text = "label1";
                // 
                // label59
                // 
                this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label59.Dock = System.Windows.Forms.DockStyle.Left;
                this.label59.Location = new System.Drawing.Point(3, 3);
                this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label59.Name = "label59";
                this.label59.Size = new System.Drawing.Size(1, 190);
                this.label59.TabIndex = 22;
                this.label59.Text = "label59";
                // 
                // frmEDocEvent_AddUserTag
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                this.AutoSize = true;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.ClientSize = new System.Drawing.Size(388, 249);
                this.Controls.Add(this.panel1);
                this.Controls.Add(this.tls_MaintainDoc);
                this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "frmEDocEvent_AddUserTag";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Add Tags";
                this.Load += new System.EventHandler(this.frmEDocEvent_AddUserTag_Load);
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
            private System.Windows.Forms.TextBox txtUserTags;
            private System.Windows.Forms.ListView lvwUserTags;
            private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            private System.Windows.Forms.ToolStripButton tlb_History;
            private System.Windows.Forms.ToolStripButton tlb_Delete;
            private System.Windows.Forms.ToolStripButton tlb_UserTag;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label59;
        }
    }
