namespace gloPatientPortal
{
    partial class frmPrePost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrePost));
            this.txtPrePost = new System.Windows.Forms.TextBox();
            this.tls_DyHealthForm = new System.Windows.Forms.ToolStrip();
            this.ts_Save = new System.Windows.Forms.ToolStripButton();
            this.ts_ShowHide = new System.Windows.Forms.ToolStripButton();
            this.lblPrePost = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tls_DyHealthForm.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPrePost
            // 
            this.txtPrePost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrePost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrePost.Location = new System.Drawing.Point(11, 37);
            this.txtPrePost.MaxLength = 2147483647;
            this.txtPrePost.Multiline = true;
            this.txtPrePost.Name = "txtPrePost";
            this.txtPrePost.Size = new System.Drawing.Size(758, 370);
            this.txtPrePost.TabIndex = 0;
            // 
            // tls_DyHealthForm
            // 
            this.tls_DyHealthForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_DyHealthForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tls_DyHealthForm.BackgroundImage")));
            this.tls_DyHealthForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_DyHealthForm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_DyHealthForm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_DyHealthForm.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_DyHealthForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_Save,
            this.ts_ShowHide});
            this.tls_DyHealthForm.Location = new System.Drawing.Point(0, 0);
            this.tls_DyHealthForm.Name = "tls_DyHealthForm";
            this.tls_DyHealthForm.Size = new System.Drawing.Size(782, 53);
            this.tls_DyHealthForm.TabIndex = 1;
            this.tls_DyHealthForm.Text = "ToolStrip1";
            // 
            // ts_Save
            // 
            this.ts_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_Save.Image = ((System.Drawing.Image)(resources.GetObject("ts_Save.Image")));
            this.ts_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_Save.Name = "ts_Save";
            this.ts_Save.Size = new System.Drawing.Size(66, 50);
            this.ts_Save.Text = "&Save&&Cls";
            this.ts_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_Save.ToolTipText = "Save and Close";
            this.ts_Save.Click += new System.EventHandler(this.ts_Save_Click);
            // 
            // ts_ShowHide
            // 
            this.ts_ShowHide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_ShowHide.Image = ((System.Drawing.Image)(resources.GetObject("ts_ShowHide.Image")));
            this.ts_ShowHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_ShowHide.Name = "ts_ShowHide";
            this.ts_ShowHide.Size = new System.Drawing.Size(43, 50);
            this.ts_ShowHide.Text = "&Close";
            this.ts_ShowHide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_ShowHide.Click += new System.EventHandler(this.ts_ShowHide_Click);
            // 
            // lblPrePost
            // 
            this.lblPrePost.AutoSize = true;
            this.lblPrePost.BackColor = System.Drawing.Color.Transparent;
            this.lblPrePost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrePost.Location = new System.Drawing.Point(15, 11);
            this.lblPrePost.Name = "lblPrePost";
            this.lblPrePost.Size = new System.Drawing.Size(0, 14);
            this.lblPrePost.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblPrePost);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtPrePost);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 419);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(782, 1);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 418);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(782, 1);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 417);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(781, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 417);
            this.label4.TabIndex = 6;
            this.label4.Text = "label4";
            // 
            // frmPrePost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(782, 472);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tls_DyHealthForm);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrePost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pre Post";
            this.Load += new System.EventHandler(this.frmPrePost_Load);
            this.tls_DyHealthForm.ResumeLayout(false);
            this.tls_DyHealthForm.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPrePost;
        internal System.Windows.Forms.ToolStrip tls_DyHealthForm;
        internal System.Windows.Forms.ToolStripButton ts_Save;
        public System.Windows.Forms.ToolStripButton ts_ShowHide;
        private System.Windows.Forms.Label lblPrePost;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}