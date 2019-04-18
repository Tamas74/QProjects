namespace gloEmdeonInterface.Forms
{
    partial class frmCnfrmLabFlow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCnfrmLabFlow));
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtn_save = new System.Windows.Forms.ToolStripButton();
            this.tlbbtn_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_LabStatus = new System.Windows.Forms.CheckBox();
            this.rbTask = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rbRecordResults = new System.Windows.Forms.RadioButton();
            this.rbLabOrder = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chk_perform = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_RecordResults = new System.Windows.Forms.Button();
            this.btn_LabOrder = new System.Windows.Forms.Button();
            this.btn_task = new System.Windows.Forms.Button();
            this.ts_LabMain.SuspendLayout();
            this.pnlToolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackColor = System.Drawing.Color.Transparent;
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtn_save,
            this.tlbbtn_Close});
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(368, 53);
            this.ts_LabMain.TabIndex = 0;
            this.ts_LabMain.Visible = false;
            // 
            // tlbbtn_save
            // 
            this.tlbbtn_save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_save.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_save.Image")));
            this.tlbbtn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_save.Name = "tlbbtn_save";
            this.tlbbtn_save.Size = new System.Drawing.Size(36, 50);
            this.tlbbtn_save.Tag = "OK";
            this.tlbbtn_save.Text = "&OK";
            this.tlbbtn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_save.Click += new System.EventHandler(this.tlbbtn_save_Click);
            // 
            // tlbbtn_Close
            // 
            this.tlbbtn_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtn_Close.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtn_Close.Image")));
            this.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtn_Close.Name = "tlbbtn_Close";
            this.tlbbtn_Close.Size = new System.Drawing.Size(43, 50);
            this.tlbbtn_Close.Tag = "Close";
            this.tlbbtn_Close.Text = "&Close";
            this.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtn_Close.Visible = false;
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Controls.Add(this.panel1);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(434, 154);
            this.pnlToolStrip.TabIndex = 33;
            this.pnlToolStrip.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chk_LabStatus);
            this.panel1.Controls.Add(this.rbTask);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rbRecordResults);
            this.panel1.Controls.Add(this.rbLabOrder);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(21, 81);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(382, 135);
            this.panel1.TabIndex = 37;
            this.panel1.Visible = false;
            // 
            // chk_LabStatus
            // 
            this.chk_LabStatus.AutoSize = true;
            this.chk_LabStatus.Location = new System.Drawing.Point(25, 89);
            this.chk_LabStatus.Name = "chk_LabStatus";
            this.chk_LabStatus.Size = new System.Drawing.Size(350, 18);
            this.chk_LabStatus.TabIndex = 83;
            this.chk_LabStatus.Text = "Always do my lab orders this way. Do not ask me anymore.";
            this.chk_LabStatus.UseVisualStyleBackColor = true;
            // 
            // rbTask
            // 
            this.rbTask.AutoSize = true;
            this.rbTask.Checked = true;
            this.rbTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTask.Location = new System.Drawing.Point(59, 59);
            this.rbTask.Name = "rbTask";
            this.rbTask.Size = new System.Drawing.Size(50, 18);
            this.rbTask.TabIndex = 0;
            this.rbTask.TabStop = true;
            this.rbTask.Tag = "Task";
            this.rbTask.Text = "Task";
            this.rbTask.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 1);
            this.label3.TabIndex = 39;
            // 
            // rbRecordResults
            // 
            this.rbRecordResults.AutoSize = true;
            this.rbRecordResults.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRecordResults.Location = new System.Drawing.Point(236, 59);
            this.rbRecordResults.Name = "rbRecordResults";
            this.rbRecordResults.Size = new System.Drawing.Size(105, 18);
            this.rbRecordResults.TabIndex = 2;
            this.rbRecordResults.Tag = "RecordResults";
            this.rbRecordResults.Text = "Record Results";
            this.rbRecordResults.UseVisualStyleBackColor = true;
            // 
            // rbLabOrder
            // 
            this.rbLabOrder.AutoSize = true;
            this.rbLabOrder.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLabOrder.Location = new System.Drawing.Point(117, 59);
            this.rbLabOrder.Name = "rbLabOrder";
            this.rbLabOrder.Size = new System.Drawing.Size(108, 18);
            this.rbLabOrder.TabIndex = 1;
            this.rbLabOrder.Tag = "LabOrder";
            this.rbLabOrder.Text = "New Lab Order";
            this.rbLabOrder.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(378, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 128);
            this.label5.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 128);
            this.label4.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 1);
            this.label2.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(56, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 14);
            this.label6.TabIndex = 82;
            this.label6.Text = "Do you want to proceed with action below?";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.chk_perform);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_RecordResults);
            this.panel2.Controls.Add(this.btn_LabOrder);
            this.panel2.Controls.Add(this.btn_task);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(434, 154);
            this.panel2.TabIndex = 34;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel7.BackgroundImage")));
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(3, 151);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(428, 3);
            this.panel7.TabIndex = 9;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(431, 26);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(3, 128);
            this.panel6.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 26);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(3, 128);
            this.panel5.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(434, 26);
            this.panel3.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(23, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 14);
            this.label7.TabIndex = 4;
            this.label7.Text = "Order Options";
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(27, 26);
            this.panel4.TabIndex = 0;
            // 
            // chk_perform
            // 
            this.chk_perform.AutoSize = true;
            this.chk_perform.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chk_perform.Location = new System.Drawing.Point(75, 75);
            this.chk_perform.Name = "chk_perform";
            this.chk_perform.Size = new System.Drawing.Size(350, 18);
            this.chk_perform.TabIndex = 4;
            this.chk_perform.Text = "Always do my lab orders this way. Do not ask me anymore.";
            this.chk_perform.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(72, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Do you want to proceed with action below?";
            // 
            // btn_RecordResults
            // 
            this.btn_RecordResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RecordResults.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_RecordResults.Location = new System.Drawing.Point(258, 111);
            this.btn_RecordResults.Name = "btn_RecordResults";
            this.btn_RecordResults.Size = new System.Drawing.Size(121, 24);
            this.btn_RecordResults.TabIndex = 2;
            this.btn_RecordResults.Tag = "RecordResults";
            this.btn_RecordResults.Text = "Orders and Results";
            this.btn_RecordResults.UseVisualStyleBackColor = true;
            this.btn_RecordResults.Click += new System.EventHandler(this.btn_RecordResults_Click);
            // 
            // btn_LabOrder
            // 
            this.btn_LabOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LabOrder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_LabOrder.Location = new System.Drawing.Point(142, 111);
            this.btn_LabOrder.Name = "btn_LabOrder";
            this.btn_LabOrder.Size = new System.Drawing.Size(110, 24);
            this.btn_LabOrder.TabIndex = 1;
            this.btn_LabOrder.Tag = "LabOrder";
            this.btn_LabOrder.Text = "Lab Order";
            this.btn_LabOrder.UseVisualStyleBackColor = true;
            this.btn_LabOrder.Click += new System.EventHandler(this.btn_LabOrder_Click);
            // 
            // btn_task
            // 
            this.btn_task.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_task.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_task.Location = new System.Drawing.Point(75, 111);
            this.btn_task.Name = "btn_task";
            this.btn_task.Size = new System.Drawing.Size(61, 24);
            this.btn_task.TabIndex = 0;
            this.btn_task.Tag = "Task";
            this.btn_task.Text = "Task";
            this.btn_task.UseVisualStyleBackColor = true;
            this.btn_task.Click += new System.EventHandler(this.btn_task_Click);
            // 
            // frmCnfrmLabFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(434, 154);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCnfrmLabFlow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Order Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCnfrmLabFlow_FormClosing);
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        internal System.Windows.Forms.ToolStripButton tlbbtn_save;
        internal System.Windows.Forms.ToolStripButton tlbbtn_Close;
        internal System.Windows.Forms.Panel pnlToolStrip;
        private System.Windows.Forms.RadioButton rbTask;
        private System.Windows.Forms.RadioButton rbLabOrder;
        private System.Windows.Forms.RadioButton rbRecordResults;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chk_LabStatus;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_RecordResults;
        private System.Windows.Forms.Button btn_LabOrder;
        private System.Windows.Forms.Button btn_task;
        private System.Windows.Forms.CheckBox chk_perform;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label7;
    }
}