namespace gloSurescriptSecureMessage
{
    partial class frmCCDA
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
                    if (dtFromPtCCD != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtFromPtCCD);
                        }
                        catch
                        {
                        }
                        dtFromPtCCD.Dispose();
                        dtFromPtCCD = null;
                    }
                }
                catch
                {
                }



                try
                {
                    if (dtToPtCCD != null)
                    {
                        try
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtToPtCCD);
                        }
                        catch
                        {
                        }
                        dtToPtCCD.Dispose();
                        dtToPtCCD = null;
                    }
                }
                catch
                {
                }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCCDA));
            this.btnPtCCDA = new System.Windows.Forms.Button();
            this.dtFromPtCCD = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtToPtCCD = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDateRange = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPtCCDA
            // 
            this.btnPtCCDA.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPtCCDA.BackgroundImage")));
            this.btnPtCCDA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPtCCDA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPtCCDA.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPtCCDA.Location = new System.Drawing.Point(374, 23);
            this.btnPtCCDA.Name = "btnPtCCDA";
            this.btnPtCCDA.Size = new System.Drawing.Size(56, 23);
            this.btnPtCCDA.TabIndex = 3;
            this.btnPtCCDA.Text = "CDA";
            this.btnPtCCDA.UseVisualStyleBackColor = true;
            this.btnPtCCDA.Click += new System.EventHandler(this.btnPtCCD_Click);
            // 
            // dtFromPtCCD
            // 
            this.dtFromPtCCD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFromPtCCD.Location = new System.Drawing.Point(164, 23);
            this.dtFromPtCCD.Name = "dtFromPtCCD";
            this.dtFromPtCCD.Size = new System.Drawing.Size(95, 22);
            this.dtFromPtCCD.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Patient Record From :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "to";
            // 
            // dtToPtCCD
            // 
            this.dtToPtCCD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtToPtCCD.Location = new System.Drawing.Point(285, 23);
            this.dtToPtCCD.Name = "dtToPtCCD";
            this.dtToPtCCD.Size = new System.Drawing.Size(85, 22);
            this.dtToPtCCD.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkDateRange);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtToPtCCD);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnPtCCDA);
            this.panel1.Controls.Add(this.dtFromPtCCD);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(447, 69);
            this.panel1.TabIndex = 9;
            // 
            // chkDateRange
            // 
            this.chkDateRange.AutoSize = true;
            this.chkDateRange.Location = new System.Drawing.Point(145, 27);
            this.chkDateRange.Name = "chkDateRange";
            this.chkDateRange.Size = new System.Drawing.Size(15, 14);
            this.chkDateRange.TabIndex = 0;
            this.chkDateRange.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(443, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 61);
            this.label7.TabIndex = 4;
            this.label7.Text = "Viisit Summary For :";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 61);
            this.label6.TabIndex = 3;
            this.label6.Text = "Viisit Summary For :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(3, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(441, 1);
            this.label5.TabIndex = 2;
            this.label5.Text = "Viisit Summary For :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(441, 1);
            this.label4.TabIndex = 1;
            this.label4.Text = "Viisit Summary For :";
            // 
            // frmCCDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(447, 69);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCCDA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CDA";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPtCCDA;
        private System.Windows.Forms.DateTimePicker dtFromPtCCD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtToPtCCD;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkDateRange;
    }
}