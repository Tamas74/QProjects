namespace gloEmdeonInterface.Forms
{
    partial class FrmSelectECGDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectECGDevice));
            this.ts_ViewButtons = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbHealthCentrix = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.RdoMidmarkECGDevice = new System.Windows.Forms.RadioButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.RdoWelchAllynECGDevice = new System.Windows.Forms.RadioButton();
            this.RdoCardiacScienseECGDevice = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.ts_ViewButtons.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_ViewButtons
            // 
            this.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent;
            this.ts_ViewButtons.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_ViewButtons.BackgroundImage")));
            this.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_ViewButtons.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_ViewButtons.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_ViewButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbHealthCentrix,
            this.ts_btnClose});
            this.ts_ViewButtons.Location = new System.Drawing.Point(0, 0);
            this.ts_ViewButtons.Name = "ts_ViewButtons";
            this.ts_ViewButtons.Size = new System.Drawing.Size(341, 53);
            this.ts_ViewButtons.TabIndex = 2;
            this.ts_ViewButtons.Text = "ToolStrip1";
            this.ts_ViewButtons.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_ViewButtons_ItemClicked);
            // 
            // tlbHealthCentrix
            // 
            this.tlbHealthCentrix.BackColor = System.Drawing.Color.Transparent;
            this.tlbHealthCentrix.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbHealthCentrix.Image = ((System.Drawing.Image)(resources.GetObject("tlbHealthCentrix.Image")));
            this.tlbHealthCentrix.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbHealthCentrix.Name = "tlbHealthCentrix";
            this.tlbHealthCentrix.Size = new System.Drawing.Size(100, 50);
            this.tlbHealthCentrix.Tag = "StartECGDevice";
            this.tlbHealthCentrix.Text = "Start ECG Test";
            this.tlbHealthCentrix.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.BackColor = System.Drawing.Color.Transparent;
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.RdoMidmarkECGDevice);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.RdoWelchAllynECGDevice);
            this.Panel1.Controls.Add(this.RdoCardiacScienseECGDevice);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel1.Location = new System.Drawing.Point(0, 53);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(341, 166);
            this.Panel1.TabIndex = 3;
            // 
            // RdoMidmarkECGDevice
            // 
            this.RdoMidmarkECGDevice.AutoSize = true;
            this.RdoMidmarkECGDevice.Location = new System.Drawing.Point(63, 86);
            this.RdoMidmarkECGDevice.Name = "RdoMidmarkECGDevice";
            this.RdoMidmarkECGDevice.Size = new System.Drawing.Size(151, 18);
            this.RdoMidmarkECGDevice.TabIndex = 11;
            this.RdoMidmarkECGDevice.TabStop = true;
            this.RdoMidmarkECGDevice.Text = "MidMark IQ ECG Device";
            this.RdoMidmarkECGDevice.UseVisualStyleBackColor = true;
            this.RdoMidmarkECGDevice.CheckedChanged += new System.EventHandler(this.RdoMidmarkECGDevice_CheckedChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(15, 18);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(224, 14);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "Select ECG Device To Perform Test :";
            // 
            // RdoWelchAllynECGDevice
            // 
            this.RdoWelchAllynECGDevice.AutoSize = true;
            this.RdoWelchAllynECGDevice.Location = new System.Drawing.Point(63, 120);
            this.RdoWelchAllynECGDevice.Name = "RdoWelchAllynECGDevice";
            this.RdoWelchAllynECGDevice.Size = new System.Drawing.Size(154, 18);
            this.RdoWelchAllynECGDevice.TabIndex = 8;
            this.RdoWelchAllynECGDevice.TabStop = true;
            this.RdoWelchAllynECGDevice.Text = "Welch Allyn ECG Device";
            this.RdoWelchAllynECGDevice.UseVisualStyleBackColor = true;
            this.RdoWelchAllynECGDevice.CheckedChanged += new System.EventHandler(this.RdoWelchAllynECGDevice_CheckedChanged);
            // 
            // RdoCardiacScienseECGDevice
            // 
            this.RdoCardiacScienseECGDevice.AutoSize = true;
            this.RdoCardiacScienseECGDevice.Location = new System.Drawing.Point(63, 51);
            this.RdoCardiacScienseECGDevice.Name = "RdoCardiacScienseECGDevice";
            this.RdoCardiacScienseECGDevice.Size = new System.Drawing.Size(159, 18);
            this.RdoCardiacScienseECGDevice.TabIndex = 7;
            this.RdoCardiacScienseECGDevice.TabStop = true;
            this.RdoCardiacScienseECGDevice.Text = "HeartCentrix ECG Device";
            this.RdoCardiacScienseECGDevice.UseVisualStyleBackColor = true;
            this.RdoCardiacScienseECGDevice.CheckedChanged += new System.EventHandler(this.RdoCardiacScienseECGDevice_CheckedChanged);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.Location = new System.Drawing.Point(4, 3);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(333, 1);
            this.Label1.TabIndex = 3;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label3.Location = new System.Drawing.Point(4, 162);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(333, 1);
            this.Label3.TabIndex = 5;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label4.Location = new System.Drawing.Point(337, 3);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(1, 160);
            this.Label4.TabIndex = 6;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Location = new System.Drawing.Point(3, 3);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 160);
            this.Label2.TabIndex = 4;
            // 
            // FrmSelectECGDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(341, 219);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.ts_ViewButtons);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectECGDevice";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Device";
            this.Load += new System.EventHandler(this.FrmSelectECGDevice_Load);
            this.ts_ViewButtons.ResumeLayout(false);
            this.ts_ViewButtons.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_ViewButtons;
        internal System.Windows.Forms.ToolStripButton tlbHealthCentrix;
        internal System.Windows.Forms.ToolStripButton ts_btnClose;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.RadioButton RdoWelchAllynECGDevice;
        internal System.Windows.Forms.RadioButton RdoCardiacScienseECGDevice;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.RadioButton RdoMidmarkECGDevice;
    }
}