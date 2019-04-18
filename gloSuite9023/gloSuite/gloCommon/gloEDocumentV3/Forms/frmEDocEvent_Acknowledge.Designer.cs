
namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_Acknowledge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_Acknowledge));
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lvwAcknowledge = new System.Windows.Forms.ListView();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Review = new System.Windows.Forms.ToolStripButton();
            this.tlb_History = new System.Windows.Forms.ToolStripButton();
            this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tlb_AssignTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.pnlMaintainDoc = new System.Windows.Forms.Panel();
            this.tls_MaintainDoc.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMaintainDoc.SuspendLayout();
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
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(89, 42);
            this.txtComment.Margin = new System.Windows.Forms.Padding(2);
            this.txtComment.MaxLength = 255;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(287, 113);
            this.txtComment.TabIndex = 15;
            // 
            // lvwAcknowledge
            // 
            this.lvwAcknowledge.FullRowSelect = true;
            this.lvwAcknowledge.HideSelection = false;
            this.lvwAcknowledge.Location = new System.Drawing.Point(10, 10);
            this.lvwAcknowledge.Margin = new System.Windows.Forms.Padding(2);
            this.lvwAcknowledge.MultiSelect = false;
            this.lvwAcknowledge.Name = "lvwAcknowledge";
            this.lvwAcknowledge.Size = new System.Drawing.Size(366, 151);
            this.lvwAcknowledge.TabIndex = 16;
            this.lvwAcknowledge.UseCompatibleStateImageBehavior = false;
            this.lvwAcknowledge.View = System.Windows.Forms.View.Details;
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Review,
            this.tlb_History,
            this.tlb_Delete,
            this.tlb_AssignTask,
            this.toolStripSeparator1,
            this.tlb_Ok,
            this.tlb_Cancel});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(390, 53);
            this.tls_MaintainDoc.TabIndex = 3;
            this.tls_MaintainDoc.Text = "toolStrip1";
            // 
            // tlb_Review
            // 
            this.tlb_Review.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Review.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Review.Image")));
            this.tlb_Review.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Review.Name = "tlb_Review";
            this.tlb_Review.Size = new System.Drawing.Size(55, 50);
            this.tlb_Review.Tag = "Review";
            this.tlb_Review.Text = "&Review";
            this.tlb_Review.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Review.ToolTipText = "Review";
            this.tlb_Review.Click += new System.EventHandler(this.tlb_Notes_Click);
            // 
            // tlb_History
            // 
            this.tlb_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_History.Image = ((System.Drawing.Image)(resources.GetObject("tlb_History.Image")));
            this.tlb_History.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_History.Name = "tlb_History";
            this.tlb_History.Size = new System.Drawing.Size(55, 50);
            this.tlb_History.Tag = "Histroy";
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
            this.tlb_Delete.Tag = "Delete";
            this.tlb_Delete.Text = "&Delete";
            this.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Delete.ToolTipText = "Delete";
            this.tlb_Delete.Visible = false;
            this.tlb_Delete.Click += new System.EventHandler(this.tlb_Delete_Click);
            // 
            // tlb_AssignTask
            // 
            this.tlb_AssignTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_AssignTask.Image = ((System.Drawing.Image)(resources.GetObject("tlb_AssignTask.Image")));
            this.tlb_AssignTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_AssignTask.Name = "tlb_AssignTask";
            this.tlb_AssignTask.Size = new System.Drawing.Size(82, 50);
            this.tlb_AssignTask.Tag = "Assign Task";
            this.tlb_AssignTask.Text = "&Assign Task";
            this.tlb_AssignTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_AssignTask.Click += new System.EventHandler(this.ts_AssignTask_Click);
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
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.AutoEllipsis = true;
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(47, 14);
            this.lblUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(39, 14);
            this.lblUser.TabIndex = 17;
            this.lblUser.Text = "User :";
            // 
            // cmbUser
            // 
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(89, 10);
            this.cmbUser.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(287, 22);
            this.cmbUser.TabIndex = 18;
            // 
            // lblComment
            // 
            this.lblComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblComment.AutoEllipsis = true;
            this.lblComment.AutoSize = true;
            this.lblComment.BackColor = System.Drawing.Color.Transparent;
            this.lblComment.Location = new System.Drawing.Point(20, 44);
            this.lblComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(68, 14);
            this.lblComment.TabIndex = 19;
            this.lblComment.Text = "Comment :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label62);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Controls.Add(this.lblComment);
            this.panel1.Controls.Add(this.cmbUser);
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Controls.Add(this.txtComment);
            this.panel1.Controls.Add(this.pbDocument);
            this.panel1.Controls.Add(this.lvwAcknowledge);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(390, 196);
            this.panel1.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(4, 192);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(382, 1);
            this.label2.TabIndex = 23;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(386, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 189);
            this.label1.TabIndex = 22;
            this.label1.Text = "label1";
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Top;
            this.label62.Location = new System.Drawing.Point(4, 3);
            this.label62.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(383, 1);
            this.label62.TabIndex = 20;
            this.label62.Text = "label62";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 190);
            this.label59.TabIndex = 21;
            this.label59.Text = "label59";
            // 
            // pnlMaintainDoc
            // 
            this.pnlMaintainDoc.Controls.Add(this.tls_MaintainDoc);
            this.pnlMaintainDoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.pnlMaintainDoc.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMaintainDoc.Name = "pnlMaintainDoc";
            this.pnlMaintainDoc.Size = new System.Drawing.Size(390, 54);
            this.pnlMaintainDoc.TabIndex = 21;
            // 
            // frmEDocEvent_Acknowledge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(390, 250);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMaintainDoc);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocEvent_Acknowledge";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Acknowledge/Review";
            this.Load += new System.EventHandler(this.frmEDocEvent_Acknowledge_Load);
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMaintainDoc.ResumeLayout(false);
            this.pnlMaintainDoc.PerformLayout();
            this.ResumeLayout(false);

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Ok;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ProgressBar pbDocument;
            private System.Windows.Forms.TextBox txtComment;
            private System.Windows.Forms.ListView lvwAcknowledge;
            private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            private System.Windows.Forms.ToolStripButton tlb_History;
            private System.Windows.Forms.ToolStripButton tlb_Delete;
            private System.Windows.Forms.ToolStripButton tlb_Review;
            private System.Windows.Forms.Label lblUser;
            private System.Windows.Forms.ComboBox cmbUser;
            private System.Windows.Forms.Label lblComment;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Panel pnlMaintainDoc;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label62;
            private System.Windows.Forms.Label label59;
            internal System.Windows.Forms.ToolStripButton tlb_AssignTask;
        }
    }
