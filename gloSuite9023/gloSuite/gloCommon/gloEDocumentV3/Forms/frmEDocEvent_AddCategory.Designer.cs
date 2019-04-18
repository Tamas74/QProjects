
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_AddCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_AddCategory));
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lvwCategory = new System.Windows.Forms.ListView();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Add = new System.Windows.Forms.ToolStripButton();
            this.tlb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tlb_View = new System.Windows.Forms.ToolStripButton();
            this.tlb_Save = new System.Windows.Forms.ToolStripButton();
            this.tlb_Close = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.lblCategory = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMandatory = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tls_MaintainDoc.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCategory
            // 
            this.txtCategory.ForeColor = System.Drawing.Color.Black;
            this.txtCategory.Location = new System.Drawing.Point(144, 96);
            this.txtCategory.MaxLength = 50;
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(228, 22);
            this.txtCategory.TabIndex = 15;
            this.txtCategory.Tag = "0";
            this.txtCategory.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCategory_KeyUp);
            // 
            // lvwCategory
            // 
            this.lvwCategory.ForeColor = System.Drawing.Color.Black;
            this.lvwCategory.FullRowSelect = true;
            this.lvwCategory.HideSelection = false;
            this.lvwCategory.Location = new System.Drawing.Point(11, 10);
            this.lvwCategory.MultiSelect = false;
            this.lvwCategory.Name = "lvwCategory";
            this.lvwCategory.Size = new System.Drawing.Size(400, 194);
            this.lvwCategory.TabIndex = 16;
            this.lvwCategory.UseCompatibleStateImageBehavior = false;
            this.lvwCategory.View = System.Windows.Forms.View.Details;
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Add,
            this.tlb_Modify,
            this.tlb_Delete,
            this.tlb_View,
            this.tlb_Save,
            this.tlb_Close,
            this.tlb_Cancel});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(423, 53);
            this.tls_MaintainDoc.TabIndex = 3;
            this.tls_MaintainDoc.Text = "toolStrip1";
            this.tls_MaintainDoc.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_MaintainDoc_ItemClicked);
            // 
            // tlb_Add
            // 
            this.tlb_Add.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Add.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Add.Image")));
            this.tlb_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Add.Name = "tlb_Add";
            this.tlb_Add.Size = new System.Drawing.Size(37, 50);
            this.tlb_Add.Tag = "Add";
            this.tlb_Add.Text = "&New";
            this.tlb_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlb_Modify
            // 
            this.tlb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Modify.Image")));
            this.tlb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Modify.Name = "tlb_Modify";
            this.tlb_Modify.Size = new System.Drawing.Size(53, 50);
            this.tlb_Modify.Tag = "Modify";
            this.tlb_Modify.Text = "&Modify";
            this.tlb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // 
            // tlb_View
            // 
            this.tlb_View.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_View.Image = ((System.Drawing.Image)(resources.GetObject("tlb_View.Image")));
            this.tlb_View.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_View.Name = "tlb_View";
            this.tlb_View.Size = new System.Drawing.Size(40, 50);
            this.tlb_View.Tag = "View";
            this.tlb_View.Text = "&View";
            this.tlb_View.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_View.Visible = false;
            // 
            // tlb_Save
            // 
            this.tlb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Save.Image")));
            this.tlb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Save.Name = "tlb_Save";
            this.tlb_Save.Size = new System.Drawing.Size(66, 50);
            this.tlb_Save.Tag = "Save";
            this.tlb_Save.Text = "&Save&&Cls";
            this.tlb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Save.ToolTipText = "Save and Close";
            this.tlb_Save.Visible = false;
            // 
            // tlb_Close
            // 
            this.tlb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Close.Image")));
            this.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Close.Name = "tlb_Close";
            this.tlb_Close.Size = new System.Drawing.Size(43, 50);
            this.tlb_Close.Tag = "Close";
            this.tlb_Close.Text = "&Close";
            this.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Close.ToolTipText = "Close";
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
            this.tlb_Cancel.Visible = false;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(42, 100);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(99, 14);
            this.lblCategory.TabIndex = 17;
            this.lblCategory.Text = "Category Name :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblMandatory);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label59);
            this.panel1.Controls.Add(this.txtCategory);
            this.panel1.Controls.Add(this.lblCategory);
            this.panel1.Controls.Add(this.lvwCategory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(423, 214);
            this.panel1.TabIndex = 18;
            // 
            // lblMandatory
            // 
            this.lblMandatory.AutoSize = true;
            this.lblMandatory.ForeColor = System.Drawing.Color.Red;
            this.lblMandatory.Location = new System.Drawing.Point(30, 100);
            this.lblMandatory.Name = "lblMandatory";
            this.lblMandatory.Size = new System.Drawing.Size(14, 14);
            this.lblMandatory.TabIndex = 26;
            this.lblMandatory.Text = "*";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(415, 1);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(415, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(419, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 208);
            this.label1.TabIndex = 23;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 208);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tls_MaintainDoc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(423, 54);
            this.panel2.TabIndex = 19;
            // 
            // frmEDocEvent_AddCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(423, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocEvent_AddCategory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " DMS Category";
            this.Load += new System.EventHandler(this.frmEDocEvent_AddCategory_Load);
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
            private System.Windows.Forms.ToolStripButton tlb_Add;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.TextBox txtCategory;
            private System.Windows.Forms.ListView lvwCategory;
            private System.Windows.Forms.ToolStripButton tlb_View;
            private System.Windows.Forms.ToolStripButton tlb_Delete;
            private System.Windows.Forms.ToolStripButton tlb_Modify;
            private System.Windows.Forms.Label lblCategory;
            private System.Windows.Forms.ToolStripButton tlb_Save;
            private System.Windows.Forms.ToolStripButton tlb_Close;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label59;
            private System.Windows.Forms.Panel panel2;
            private System.Windows.Forms.Label lblMandatory;
        }
    }
