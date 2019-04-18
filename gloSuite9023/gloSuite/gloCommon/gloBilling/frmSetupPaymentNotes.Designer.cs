namespace gloBilling
{
    partial class frmSetupPaymentNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupPaymentNotes));
            this.tls_Notes = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Notes = new System.Windows.Forms.ToolStripButton();
            this.tlb_History = new System.Windows.Forms.ToolStripButton();
            this.tlb_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lvwNotes = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tls_Notes.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_Notes
            // 
            this.tls_Notes.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Notes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Notes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Notes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Notes,
            this.tlb_History,
            this.tlb_Delete,
            this.toolStripSeparator1,
            this.ToolStripButton1,
            this.tlb_Ok,
            this.tlb_Cancel});
            this.tls_Notes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Notes.Location = new System.Drawing.Point(0, 0);
            this.tls_Notes.Name = "tls_Notes";
            this.tls_Notes.Size = new System.Drawing.Size(485, 53);
            this.tls_Notes.TabIndex = 0;
            this.tls_Notes.TabStop = true;
            this.tls_Notes.Text = "toolStrip1";
            // 
            // tlb_Notes
            // 
            this.tlb_Notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Notes.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Notes.Image")));
            this.tlb_Notes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Notes.Name = "tlb_Notes";
            this.tlb_Notes.Size = new System.Drawing.Size(46, 50);
            this.tlb_Notes.Tag = "Notes";
            this.tlb_Notes.Text = "&Notes";
            this.tlb_Notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Notes.ToolTipText = "Notes";
            this.tlb_Notes.Click += new System.EventHandler(this.tlb_Notes_Click);
            // 
            // tlb_History
            // 
            this.tlb_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_History.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_History.Image = ((System.Drawing.Image)(resources.GetObject("tlb_History.Image")));
            this.tlb_History.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_History.Name = "tlb_History";
            this.tlb_History.Size = new System.Drawing.Size(55, 50);
            this.tlb_History.Tag = "History";
            this.tlb_History.Text = "&History";
            this.tlb_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_History.Click += new System.EventHandler(this.tlb_History_Click);
            // 
            // tlb_Delete
            // 
            this.tlb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Delete.Image")));
            this.tlb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Delete.Name = "tlb_Delete";
            this.tlb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tlb_Delete.Tag = " Delete";
            this.tlb_Delete.Text = "&Delete";
            this.tlb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Delete.Click += new System.EventHandler(this.tlb_Delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(40, 50);
            this.ToolStripButton1.Text = "Sa&ve";
            this.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolStripButton1.ToolTipText = "Save";
            // 
            // tlb_Ok
            // 
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
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.label59);
            this.pnlMain.Controls.Add(this.txtNotes);
            this.pnlMain.Controls.Add(this.lvwNotes);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(485, 272);
            this.pnlMain.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(477, 1);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(477, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(481, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 266);
            this.label1.TabIndex = 23;
            this.label1.Text = "label1";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 266);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(10, 15);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(460, 242);
            this.txtNotes.TabIndex = 1;
            // 
            // lvwNotes
            // 
            this.lvwNotes.FullRowSelect = true;
            this.lvwNotes.HideSelection = false;
            this.lvwNotes.Location = new System.Drawing.Point(10, 15);
            this.lvwNotes.MultiSelect = false;
            this.lvwNotes.Name = "lvwNotes";
            this.lvwNotes.Size = new System.Drawing.Size(461, 242);
            this.lvwNotes.TabIndex = 2;
            this.lvwNotes.UseCompatibleStateImageBehavior = false;
            this.lvwNotes.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tls_Notes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 54);
            this.panel1.TabIndex = 1;
            // 
            // frmSetupPaymentNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(485, 326);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupPaymentNotes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payment Notes";
            this.Load += new System.EventHandler(this.frmSetupPaymentNotes_Load);
            this.tls_Notes.ResumeLayout(false);
            this.tls_Notes.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gloGlobal.gloToolStripIgnoreFocus tls_Notes;
        private System.Windows.Forms.ToolStripButton tlb_Ok;
        private System.Windows.Forms.ToolStripButton tlb_Cancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlb_Notes;
        private System.Windows.Forms.ToolStripButton tlb_History;
        private System.Windows.Forms.ToolStripButton tlb_Delete;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.ListView lvwNotes;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ToolStripButton ToolStripButton1;
    }
}