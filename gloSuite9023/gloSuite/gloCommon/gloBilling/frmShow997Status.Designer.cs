namespace gloBilling
{
    partial class frmShow997Status
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShow997Status));
            this.lbl1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBatchName = new System.Windows.Forms.Label();
            this.lblBatchStatusDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBatchStatusCode = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tsb997BatchStatus = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.lblBatchStatus = new System.Windows.Forms.Label();
            this.tsb_Send_PaperClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_Send_ElectronicClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_Elec_Secondary = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tsb997BatchStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.Color.Transparent;
            this.lbl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(35, 55);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(83, 14);
            this.lbl1.TabIndex = 7;
            this.lbl1.Text = "Batch Name:";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Status:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "Date :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBatchName
            // 
            this.lblBatchName.BackColor = System.Drawing.Color.Transparent;
            this.lblBatchName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchName.Location = new System.Drawing.Point(125, 51);
            this.lblBatchName.Name = "lblBatchName";
            this.lblBatchName.Size = new System.Drawing.Size(323, 22);
            this.lblBatchName.TabIndex = 10;
            this.lblBatchName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBatchName.Click += new System.EventHandler(this.lblBatchName_Click);
            // 
            // lblBatchStatusDate
            // 
            this.lblBatchStatusDate.BackColor = System.Drawing.Color.Transparent;
            this.lblBatchStatusDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchStatusDate.Location = new System.Drawing.Point(125, 138);
            this.lblBatchStatusDate.Name = "lblBatchStatusDate";
            this.lblBatchStatusDate.Size = new System.Drawing.Size(323, 22);
            this.lblBatchStatusDate.TabIndex = 12;
            this.lblBatchStatusDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblBatchStatusCode);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblBatchStatusDate);
            this.panel1.Controls.Add(this.lblBatchStatus);
            this.panel1.Controls.Add(this.lblBatchName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(478, 176);
            this.panel1.TabIndex = 0;
            // 
            // lblBatchStatusCode
            // 
            this.lblBatchStatusCode.BackColor = System.Drawing.Color.Transparent;
            this.lblBatchStatusCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchStatusCode.Location = new System.Drawing.Point(125, 80);
            this.lblBatchStatusCode.Name = "lblBatchStatusCode";
            this.lblBatchStatusCode.Size = new System.Drawing.Size(120, 22);
            this.lblBatchStatusCode.TabIndex = 16;
            this.lblBatchStatusCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBatchStatusCode.Click += new System.EventHandler(this.lblBatchStatusCode_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 14);
            this.label4.TabIndex = 15;
            this.label4.Text = "Status Code:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(164, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "997 Acknowledgment";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsb997BatchStatus
            // 
            this.tsb997BatchStatus.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tsb997BatchStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb997BatchStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb997BatchStatus.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsb997BatchStatus.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsb997BatchStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Close});
            this.tsb997BatchStatus.Location = new System.Drawing.Point(0, 0);
            this.tsb997BatchStatus.Name = "tsb997BatchStatus";
            this.tsb997BatchStatus.Size = new System.Drawing.Size(478, 53);
            this.tsb997BatchStatus.TabIndex = 13;
            this.tsb997BatchStatus.Text = "toolStrip1";
            // 
            // tsb_Close
            // 
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Text = "Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // lblBatchStatus
            // 
            this.lblBatchStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblBatchStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchStatus.Location = new System.Drawing.Point(125, 109);
            this.lblBatchStatus.Name = "lblBatchStatus";
            this.lblBatchStatus.Size = new System.Drawing.Size(323, 22);
            this.lblBatchStatus.TabIndex = 11;
            this.lblBatchStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsb_Send_PaperClaim
            // 
            this.tsb_Send_PaperClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Send_PaperClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Send_PaperClaim.Image")));
            this.tsb_Send_PaperClaim.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Send_PaperClaim.Name = "tsb_Send_PaperClaim";
            this.tsb_Send_PaperClaim.Size = new System.Drawing.Size(159, 22);
            this.tsb_Send_PaperClaim.Text = "Paper Claim";
            // 
            // tsb_Send_ElectronicClaim
            // 
            this.tsb_Send_ElectronicClaim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Send_ElectronicClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Send_ElectronicClaim.Image")));
            this.tsb_Send_ElectronicClaim.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Send_ElectronicClaim.Name = "tsb_Send_ElectronicClaim";
            this.tsb_Send_ElectronicClaim.Size = new System.Drawing.Size(159, 22);
            this.tsb_Send_ElectronicClaim.Text = "Electronic Claim";
            // 
            // tsb_Elec_Secondary
            // 
            this.tsb_Elec_Secondary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Elec_Secondary.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Elec_Secondary.Image")));
            this.tsb_Elec_Secondary.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Elec_Secondary.Name = "tsb_Elec_Secondary";
            this.tsb_Elec_Secondary.Size = new System.Drawing.Size(186, 22);
            this.tsb_Elec_Secondary.Text = "Send Electronic Claim";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 170);
            this.label5.TabIndex = 17;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(474, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 170);
            this.label6.TabIndex = 18;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(470, 1);
            this.label7.TabIndex = 19;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(470, 1);
            this.label8.TabIndex = 20;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmShow997Status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(478, 229);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tsb997BatchStatus);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShow997Status";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Acknowledgment";
            this.Load += new System.EventHandler(this.frmShow997Status_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsb997BatchStatus.ResumeLayout(false);
            this.tsb997BatchStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBatchName;
        private System.Windows.Forms.Label lblBatchStatusDate;
        private System.Windows.Forms.Panel panel1;
        private gloGlobal.gloToolStripIgnoreFocus tsb997BatchStatus;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Label lblBatchStatus;
        private System.Windows.Forms.ToolStripMenuItem tsb_Send_PaperClaim;
        private System.Windows.Forms.ToolStripMenuItem tsb_Send_ElectronicClaim;
        private System.Windows.Forms.ToolStripMenuItem tsb_Elec_Secondary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBatchStatusCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;

    }
}