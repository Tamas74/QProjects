
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_Migrate
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
                this.components = new System.ComponentModel.Container();
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_Migrate));
                this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
                this.tlb_Migrate = new System.Windows.Forms.ToolStripButton();
                this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
                this.txtProgress = new System.Windows.Forms.TextBox();
                this.tmr_Auto = new System.Windows.Forms.Timer(this.components);
                this.panel1 = new System.Windows.Forms.Panel();
                this.lblMigratingPatient = new System.Windows.Forms.Label();
                this.progressBar1 = new System.Windows.Forms.ProgressBar();
                this.txtWait = new System.Windows.Forms.TextBox();
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
                // tls_MaintainDoc
                // 
                this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
                this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
                this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Migrate,
            this.tlb_Cancel});
                this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
                this.tls_MaintainDoc.Name = "tls_MaintainDoc";
                this.tls_MaintainDoc.Size = new System.Drawing.Size(389, 53);
                this.tls_MaintainDoc.TabIndex = 3;
                this.tls_MaintainDoc.Text = "toolStrip1";
                // 
                // tlb_Migrate
                // 
                this.tlb_Migrate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Migrate.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Migrate.Image")));
                this.tlb_Migrate.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_Migrate.Name = "tlb_Migrate";
                this.tlb_Migrate.Size = new System.Drawing.Size(58, 50);
                this.tlb_Migrate.Tag = "OK";
                this.tlb_Migrate.Text = "&Migrate";
                this.tlb_Migrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_Migrate.ToolTipText = "Migrate";
                this.tlb_Migrate.Click += new System.EventHandler(this.tlb_Migrate_Click);
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
                // txtProgress
                // 
                this.txtProgress.BackColor = System.Drawing.Color.White;
                this.txtProgress.Cursor = System.Windows.Forms.Cursors.Arrow;
                this.txtProgress.Enabled = false;
                this.txtProgress.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.txtProgress.Location = new System.Drawing.Point(10, 12);
                this.txtProgress.Multiline = true;
                this.txtProgress.Name = "txtProgress";
                this.txtProgress.ReadOnly = true;
                this.txtProgress.Size = new System.Drawing.Size(368, 140);
                this.txtProgress.TabIndex = 16;
                this.txtProgress.Visible = false;
                // 
                // tmr_Auto
                // 
                this.tmr_Auto.Interval = 200;
                this.tmr_Auto.Tick += new System.EventHandler(this.tmr_Auto_Tick);
                // 
                // panel1
                // 
                this.panel1.BackColor = System.Drawing.Color.Transparent;
                this.panel1.Controls.Add(this.lblMigratingPatient);
                this.panel1.Controls.Add(this.progressBar1);
                this.panel1.Controls.Add(this.txtWait);
                this.panel1.Controls.Add(this.label3);
                this.panel1.Controls.Add(this.label2);
                this.panel1.Controls.Add(this.label1);
                this.panel1.Controls.Add(this.label59);
                this.panel1.Controls.Add(this.txtProgress);
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Location = new System.Drawing.Point(0, 54);
                this.panel1.Name = "panel1";
                this.panel1.Padding = new System.Windows.Forms.Padding(3);
                this.panel1.Size = new System.Drawing.Size(389, 228);
                this.panel1.TabIndex = 19;
                // 
                // lblMigratingPatient
                // 
                this.lblMigratingPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.lblMigratingPatient.Location = new System.Drawing.Point(12, 156);
                this.lblMigratingPatient.Name = "lblMigratingPatient";
                this.lblMigratingPatient.Size = new System.Drawing.Size(365, 37);
                this.lblMigratingPatient.TabIndex = 28;
                this.lblMigratingPatient.Text = "Migrating Documents . . .";
                // 
                // progressBar1
                // 
                this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.progressBar1.Location = new System.Drawing.Point(10, 197);
                this.progressBar1.Name = "progressBar1";
                this.progressBar1.Size = new System.Drawing.Size(367, 19);
                this.progressBar1.TabIndex = 27;
                // 
                // txtWait
                // 
                this.txtWait.BackColor = System.Drawing.Color.White;
                this.txtWait.Cursor = System.Windows.Forms.Cursors.Arrow;
                this.txtWait.Enabled = false;
                this.txtWait.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.txtWait.ForeColor = System.Drawing.Color.Navy;
                this.txtWait.Location = new System.Drawing.Point(10, 12);
                this.txtWait.Multiline = true;
                this.txtWait.Name = "txtWait";
                this.txtWait.ReadOnly = true;
                this.txtWait.Size = new System.Drawing.Size(367, 140);
                this.txtWait.TabIndex = 26;
                this.txtWait.Text = "\r\n\r\nPlease wait while we migrate the documents...";
                this.txtWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                // 
                // label3
                // 
                this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.label3.Location = new System.Drawing.Point(4, 224);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(381, 1);
                this.label3.TabIndex = 25;
                this.label3.Text = "label3";
                // 
                // label2
                // 
                this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label2.Dock = System.Windows.Forms.DockStyle.Top;
                this.label2.Location = new System.Drawing.Point(4, 3);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(381, 1);
                this.label2.TabIndex = 24;
                this.label2.Text = "label2";
                // 
                // label1
                // 
                this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label1.Dock = System.Windows.Forms.DockStyle.Right;
                this.label1.Location = new System.Drawing.Point(385, 3);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(1, 222);
                this.label1.TabIndex = 23;
                this.label1.Text = "label1";
                // 
                // label59
                // 
                this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label59.Dock = System.Windows.Forms.DockStyle.Left;
                this.label59.Location = new System.Drawing.Point(3, 3);
                this.label59.Name = "label59";
                this.label59.Size = new System.Drawing.Size(1, 222);
                this.label59.TabIndex = 22;
                this.label59.Text = "label59";
                // 
                // panel2
                // 
                this.panel2.Controls.Add(this.tls_MaintainDoc);
                this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
                this.panel2.Location = new System.Drawing.Point(0, 0);
                this.panel2.Name = "panel2";
                this.panel2.Size = new System.Drawing.Size(389, 54);
                this.panel2.TabIndex = 20;
                // 
                // frmEDocEvent_Migrate
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.ClientSize = new System.Drawing.Size(389, 282);
                this.Controls.Add(this.panel1);
                this.Controls.Add(this.panel2);
                this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "frmEDocEvent_Migrate";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Migrate";
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
            private System.Windows.Forms.ToolStripButton tlb_Migrate;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.TextBox txtProgress;
            private System.Windows.Forms.Timer tmr_Auto;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label59;
            private System.Windows.Forms.TextBox txtWait;
            private System.Windows.Forms.ProgressBar progressBar1;
            private System.Windows.Forms.Label lblMigratingPatient;
            private System.Windows.Forms.Panel panel2;
        }
    }
