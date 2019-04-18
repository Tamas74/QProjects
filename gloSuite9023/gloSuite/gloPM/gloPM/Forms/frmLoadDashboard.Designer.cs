namespace gloPM
{
    partial class frmLoadDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadDashboard));
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblLastModifiedTime = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCopyrghTag = new System.Windows.Forms.Label();
            this.lblLastModifiedDate = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(77)))));
            this.lblDescription.Location = new System.Drawing.Point(419, 18);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(406, 16);
            this.lblDescription.TabIndex = 21;
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLastModifiedTime
            // 
            this.lblLastModifiedTime.AutoSize = true;
            this.lblLastModifiedTime.BackColor = System.Drawing.Color.Transparent;
            this.lblLastModifiedTime.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastModifiedTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(114)))), ((int)(((byte)(175)))));
            this.lblLastModifiedTime.Location = new System.Drawing.Point(459, 416);
            this.lblLastModifiedTime.Name = "lblLastModifiedTime";
            this.lblLastModifiedTime.Size = new System.Drawing.Size(28, 14);
            this.lblLastModifiedTime.TabIndex = 22;
            this.lblLastModifiedTime.Text = "Mar";
            this.lblLastModifiedTime.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 21.05F, System.Drawing.FontStyle.Bold);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(114)))), ((int)(((byte)(175)))));
            this.lblVersion.Location = new System.Drawing.Point(431, 381);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 34);
            this.lblVersion.TabIndex = 25;
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblVersion.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(416, 379);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(18, 11);
            this.panel1.TabIndex = 26;
            this.panel1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 5F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(114)))), ((int)(((byte)(175)))));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 7);
            this.label2.TabIndex = 18;
            this.label2.Text = "TM";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.Controls.Add(this.label1);
            this.Panel2.Controls.Add(this.lblCopyrghTag);
            this.Panel2.Location = new System.Drawing.Point(24, 520);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(800, 75);
            this.Panel2.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial", 8.05F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(221)))), ((int)(((byte)(227)))));
            this.label1.Location = new System.Drawing.Point(0, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 72);
            this.label1.TabIndex = 9;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // lblCopyrghTag
            // 
            this.lblCopyrghTag.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyrghTag.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCopyrghTag.Font = new System.Drawing.Font("Arial", 8.05F);
            this.lblCopyrghTag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(221)))), ((int)(((byte)(227)))));
            this.lblCopyrghTag.Location = new System.Drawing.Point(0, 0);
            this.lblCopyrghTag.Name = "lblCopyrghTag";
            this.lblCopyrghTag.Size = new System.Drawing.Size(800, 15);
            this.lblCopyrghTag.TabIndex = 8;
            this.lblCopyrghTag.Text = "CPT® copyright 2015 American Medical Association. All rights reserved.";
            // 
            // lblLastModifiedDate
            // 
            this.lblLastModifiedDate.AutoSize = true;
            this.lblLastModifiedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblLastModifiedDate.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblLastModifiedDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(221)))), ((int)(((byte)(227)))));
            this.lblLastModifiedDate.Location = new System.Drawing.Point(23, 502);
            this.lblLastModifiedDate.Name = "lblLastModifiedDate";
            this.lblLastModifiedDate.Size = new System.Drawing.Size(70, 14);
            this.lblLastModifiedDate.TabIndex = 27;
            this.lblLastModifiedDate.Text = "Mar 05, 2016";
            this.lblLastModifiedDate.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // frmLoadDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(837, 605);
            this.Controls.Add(this.lblLastModifiedDate);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblLastModifiedTime);
            this.Controls.Add(this.lblDescription);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLoadDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gloPM..People...Places...Things";
            this.Load += new System.EventHandler(this.frmLoadDashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblDescription;
        internal System.Windows.Forms.Label lblLastModifiedTime;
        internal System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lblCopyrghTag;
        internal System.Windows.Forms.Label lblLastModifiedDate;
    }
}