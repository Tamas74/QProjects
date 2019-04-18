namespace gloAuditTrail
{
    partial class frmException
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
                //try
                //{
                //    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                //}
                //catch
                //{
                //}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmException));
            this.txterror = new System.Windows.Forms.TextBox();
            this.btndetail = new System.Windows.Forms.Button();
            this.txterrdetail = new System.Windows.Forms.TextBox();
            this.btnok = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txterror
            // 
            this.txterror.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.txterror.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txterror.Location = new System.Drawing.Point(76, 23);
            this.txterror.Multiline = true;
            this.txterror.Name = "txterror";
            this.txterror.Size = new System.Drawing.Size(435, 50);
            this.txterror.TabIndex = 0;
            // 
            // btndetail
            // 
            this.btndetail.FlatAppearance.BorderSize = 0;
            this.btndetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btndetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btndetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndetail.ForeColor = System.Drawing.Color.Black;
            this.btndetail.Image = ((System.Drawing.Image)(resources.GetObject("btndetail.Image")));
            this.btndetail.Location = new System.Drawing.Point(12, 110);
            this.btndetail.Name = "btndetail";
            this.btndetail.Size = new System.Drawing.Size(28, 25);
            this.btndetail.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btndetail, "Show Detail");
            this.btndetail.UseVisualStyleBackColor = true;
            this.btndetail.Click += new System.EventHandler(this.btndetail_Click);
            // 
            // txterrdetail
            // 
            this.txterrdetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.txterrdetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txterrdetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txterrdetail.Location = new System.Drawing.Point(0, 0);
            this.txterrdetail.Multiline = true;
            this.txterrdetail.Name = "txterrdetail";
            this.txterrdetail.Size = new System.Drawing.Size(501, 297);
            this.txterrdetail.TabIndex = 2;
            // 
            // btnok
            // 
            this.btnok.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnok.BackgroundImage")));
            this.btnok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Location = new System.Drawing.Point(426, 110);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(87, 25);
            this.btnok.TabIndex = 3;
            this.btnok.Text = "OK";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 50);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(524, 1);
            this.label1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(524, 1);
            this.label2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txterror);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 100);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.txterrdetail);
            this.panel2.Location = new System.Drawing.Point(12, 146);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(501, 297);
            this.panel2.TabIndex = 8;
            // 
            // frmException
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(524, 452);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.btndetail);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmException";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txterror;
        private System.Windows.Forms.Button btndetail;
        private System.Windows.Forms.TextBox txterrdetail;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}