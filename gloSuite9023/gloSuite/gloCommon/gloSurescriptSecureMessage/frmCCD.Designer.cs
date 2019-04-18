namespace gloSurescriptSecureMessage
{
    partial class frmCCD
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
                System.Windows.Forms.DateTimePicker[] cntdtControls = { dtExamCCD, dtFromPtCCD, dtToPtCCD };
                System.Windows.Forms.Control[] cntControls = { dtExamCCD, dtFromPtCCD, dtToPtCCD };

                try
                {
                    try
                    {
                        if (cntdtControls != null)
                        {
                            if (cntdtControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                            }
                        }
                    }
                    catch
                    {
                    }

                    components.Dispose();
                    try
                    {
                        if (cntControls != null)
                        {
                            if (cntControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                            }
                        }
                    }
                    catch
                    {
                    }
                    //if (dtExamCCD != null)
                    //{
                        //try
                        //{
                        //    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtExamCCD);
                        //}
                        //catch
                        //{
                        //}
                        //dtExamCCD.Dispose();
                        //dtExamCCD = null;
                   // }
                }
                catch
                {
                }



                //try
                //{
                //    //if (dtFromPtCCD != null)
                //    //{
                //    //    try
                //    //    {
                //    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtFromPtCCD);
                //    //    }
                //    //    catch
                //    //    {
                //    //    }
                //    //    dtFromPtCCD.Dispose();
                //    //    dtFromPtCCD = null;
                //    //}
                //}
                //catch
                //{
                //}



                //try
                //{
                //    //if (dtToPtCCD != null)
                //    //{
                //    //    try
                //    //    {
                //    //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtToPtCCD);
                //    //    }
                //    //    catch
                //    //    {
                //    //    }
                //    //    dtToPtCCD.Dispose();
                //    //    dtToPtCCD = null;
                //    //}
                //}
                //catch
                //{
                //}


                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
               
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCCD));
            this.label1 = new System.Windows.Forms.Label();
            this.dtExamCCD = new System.Windows.Forms.DateTimePicker();
            this.btnExamCCD = new System.Windows.Forms.Button();
            this.btnPtCCD = new System.Windows.Forms.Button();
            this.dtFromPtCCD = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtToPtCCD = new System.Windows.Forms.DateTimePicker();
            this.chkPerPtRequest = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Viisit Summary For :";
            // 
            // dtExamCCD
            // 
            this.dtExamCCD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtExamCCD.Location = new System.Drawing.Point(155, 16);
            this.dtExamCCD.Name = "dtExamCCD";
            this.dtExamCCD.Size = new System.Drawing.Size(95, 22);
            this.dtExamCCD.TabIndex = 1;
            // 
            // btnExamCCD
            // 
            this.btnExamCCD.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExamCCD.BackgroundImage")));
            this.btnExamCCD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExamCCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExamCCD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExamCCD.Location = new System.Drawing.Point(258, 16);
            this.btnExamCCD.Name = "btnExamCCD";
            this.btnExamCCD.Size = new System.Drawing.Size(56, 23);
            this.btnExamCCD.TabIndex = 2;
            this.btnExamCCD.Text = "CCD";
            this.btnExamCCD.UseVisualStyleBackColor = true;
            this.btnExamCCD.Click += new System.EventHandler(this.btnExamCCD_Click);
            // 
            // btnPtCCD
            // 
            this.btnPtCCD.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPtCCD.BackgroundImage")));
            this.btnPtCCD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPtCCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPtCCD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPtCCD.Location = new System.Drawing.Point(369, 43);
            this.btnPtCCD.Name = "btnPtCCD";
            this.btnPtCCD.Size = new System.Drawing.Size(56, 23);
            this.btnPtCCD.TabIndex = 5;
            this.btnPtCCD.Text = "CCD";
            this.btnPtCCD.UseVisualStyleBackColor = true;
            this.btnPtCCD.Click += new System.EventHandler(this.btnPtCCD_Click);
            // 
            // dtFromPtCCD
            // 
            this.dtFromPtCCD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFromPtCCD.Location = new System.Drawing.Point(155, 43);
            this.dtFromPtCCD.Name = "dtFromPtCCD";
            this.dtFromPtCCD.Size = new System.Drawing.Size(95, 22);
            this.dtFromPtCCD.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Patient Record From :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "to";
            // 
            // dtToPtCCD
            // 
            this.dtToPtCCD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtToPtCCD.Location = new System.Drawing.Point(274, 43);
            this.dtToPtCCD.Name = "dtToPtCCD";
            this.dtToPtCCD.Size = new System.Drawing.Size(85, 22);
            this.dtToPtCCD.TabIndex = 4;
            // 
            // chkPerPtRequest
            // 
            this.chkPerPtRequest.AutoSize = true;
            this.chkPerPtRequest.Checked = true;
            this.chkPerPtRequest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPerPtRequest.Location = new System.Drawing.Point(155, 73);
            this.chkPerPtRequest.Name = "chkPerPtRequest";
            this.chkPerPtRequest.Size = new System.Drawing.Size(130, 18);
            this.chkPerPtRequest.TabIndex = 6;
            this.chkPerPtRequest.Text = "Per Patient Request";
            this.chkPerPtRequest.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.chkPerPtRequest);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dtToPtCCD);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnPtCCD);
            this.panel1.Controls.Add(this.dtFromPtCCD);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtExamCCD);
            this.panel1.Controls.Add(this.btnExamCCD);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(451, 103);
            this.panel1.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(447, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 95);
            this.label7.TabIndex = 4;
            this.label7.Text = "Viisit Summary For :";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 95);
            this.label6.TabIndex = 3;
            this.label6.Text = "Viisit Summary For :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(3, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(445, 1);
            this.label5.TabIndex = 2;
            this.label5.Text = "Viisit Summary For :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(445, 1);
            this.label4.TabIndex = 1;
            this.label4.Text = "Viisit Summary For :";
            // 
            // frmCCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(451, 103);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCCD";
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CCD";
            //this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtExamCCD;
        private System.Windows.Forms.Button btnExamCCD;
        private System.Windows.Forms.Button btnPtCCD;
        private System.Windows.Forms.DateTimePicker dtFromPtCCD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtToPtCCD;
        private System.Windows.Forms.CheckBox chkPerPtRequest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}