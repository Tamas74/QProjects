namespace gloCardScanning
{
    partial class frmUpdatePatientImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdatePatientImage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Chk_OldImage = new System.Windows.Forms.CheckBox();
            this.Chk_NewImage = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbNewFaceImage = new System.Windows.Forms.PictureBox();
            this.pbOldImage = new System.Windows.Forms.PictureBox();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewFaceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOldImage)).BeginInit();
            this.ts_Commands.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ts_Commands);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 54);
            this.panel1.TabIndex = 110;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Chk_OldImage);
            this.panel2.Controls.Add(this.Chk_NewImage);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pbNewFaceImage);
            this.panel2.Controls.Add(this.pbOldImage);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(500, 284);
            this.panel2.TabIndex = 112;
            // 
            // Chk_OldImage
            // 
            this.Chk_OldImage.AutoSize = true;
            this.Chk_OldImage.Location = new System.Drawing.Point(66, 230);
            this.Chk_OldImage.Name = "Chk_OldImage";
            this.Chk_OldImage.Size = new System.Drawing.Size(120, 18);
            this.Chk_OldImage.TabIndex = 120;
            this.Chk_OldImage.Text = "Select Old Image";
            this.Chk_OldImage.UseVisualStyleBackColor = true;
            this.Chk_OldImage.Visible = false;
            // 
            // Chk_NewImage
            // 
            this.Chk_NewImage.AutoSize = true;
            this.Chk_NewImage.Location = new System.Drawing.Point(311, 230);
            this.Chk_NewImage.Name = "Chk_NewImage";
            this.Chk_NewImage.Size = new System.Drawing.Size(127, 18);
            this.Chk_NewImage.TabIndex = 119;
            this.Chk_NewImage.Text = "Select New Image";
            this.Chk_NewImage.UseVisualStyleBackColor = true;
            this.Chk_NewImage.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(336, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 14);
            this.label5.TabIndex = 118;
            this.label5.Text = "New Photo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(94, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 14);
            this.label6.TabIndex = 117;
            this.label6.Text = "Old Photo";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(4, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(492, 1);
            this.label4.TabIndex = 113;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(492, 1);
            this.label3.TabIndex = 112;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(496, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 278);
            this.label2.TabIndex = 111;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 278);
            this.label1.TabIndex = 110;
            // 
            // pbNewFaceImage
            // 
            this.pbNewFaceImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbNewFaceImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbNewFaceImage.Image = global::gloCardScanning.Properties.Resources.HumanNew;
            this.pbNewFaceImage.Location = new System.Drawing.Point(302, 55);
            this.pbNewFaceImage.Name = "pbNewFaceImage";
            this.pbNewFaceImage.Size = new System.Drawing.Size(142, 159);
            this.pbNewFaceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbNewFaceImage.TabIndex = 111;
            this.pbNewFaceImage.TabStop = false;
            // 
            // pbOldImage
            // 
            this.pbOldImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbOldImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOldImage.Image = global::gloCardScanning.Properties.Resources.HumanNew;
            this.pbOldImage.Location = new System.Drawing.Point(57, 55);
            this.pbOldImage.Name = "pbOldImage";
            this.pbOldImage.Size = new System.Drawing.Size(142, 159);
            this.pbOldImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbOldImage.TabIndex = 111;
            this.pbOldImage.TabStop = false;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloCardScanning.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Save,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(500, 53);
            this.ts_Commands.TabIndex = 109;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // frmUpdatePatientImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(500, 338);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdatePatientImage";
            this.ShowInTaskbar = false;
            this.Text = "Update Patient Photo";
            this.Load += new System.EventHandler(this.frmUpdatePatientImage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewFaceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOldImage)).EndInit();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.PictureBox pbOldImage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbNewFaceImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Chk_OldImage;
        private System.Windows.Forms.CheckBox Chk_NewImage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}