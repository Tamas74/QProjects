namespace gloOffice
{
    partial class frmWd_PatientTemplate
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
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtp_DOS };
            System.Windows.Forms.Control[] cntControls = { dtp_DOS };
 
            if (disposing && (components != null))
            {
                components.Dispose();
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

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWd_PatientTemplate));
            this.pnl_TOP = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_InsertFile = new System.Windows.Forms.ToolStripButton();
            this.tsb_InsertImage = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnl_Left = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.wdTemplate = new AxDSOFramer.AxFramerControl();
            //this.wdTemp = new AxDSOFramer.AxFramerControl();
            this.pnl_DOS = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtp_DOS = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrDocProtect = new System.Windows.Forms.Timer(this.components);
            this.pnl_TOP.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnl_Left.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.wdTemp)).BeginInit();
            this.pnl_DOS.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_TOP
            // 
            this.pnl_TOP.Controls.Add(this.ts_Commands);
            this.pnl_TOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_TOP.Location = new System.Drawing.Point(0, 0);
            this.pnl_TOP.Name = "pnl_TOP";
            this.pnl_TOP.Size = new System.Drawing.Size(1022, 53);
            this.pnl_TOP.TabIndex = 0;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_InsertFile,
            this.tsb_InsertImage,
            this.tsb_Print,
            this.tsb_Save,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1022, 53);
            this.ts_Commands.TabIndex = 11;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_InsertFile
            // 
            this.tsb_InsertFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_InsertFile.Image = ((System.Drawing.Image)(resources.GetObject("tsb_InsertFile.Image")));
            this.tsb_InsertFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_InsertFile.Name = "tsb_InsertFile";
            this.tsb_InsertFile.Size = new System.Drawing.Size(49, 50);
            this.tsb_InsertFile.Tag = "InsertFile";
            this.tsb_InsertFile.Text = "&IntFile";
            this.tsb_InsertFile.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_InsertFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_InsertFile.ToolTipText = "Insert File";
            // 
            // tsb_InsertImage
            // 
            this.tsb_InsertImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_InsertImage.Image = ((System.Drawing.Image)(resources.GetObject("tsb_InsertImage.Image")));
            this.tsb_InsertImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_InsertImage.Name = "tsb_InsertImage";
            this.tsb_InsertImage.Size = new System.Drawing.Size(68, 50);
            this.tsb_InsertImage.Tag = "InsertImage";
            this.tsb_InsertImage.Text = "Int&Image";
            this.tsb_InsertImage.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_InsertImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_InsertImage.ToolTipText = "Insert Image";
            // 
            // tsb_Print
            // 
            this.tsb_Print.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(45, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = " &Print";
            this.tsb_Print.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Save
            // 
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(70, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = " &Save&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            // 
            // tsb_Close
            // 
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(51, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = " &Close ";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnl_Left
            // 
            this.pnl_Left.Controls.Add(this.label12);
            this.pnl_Left.Controls.Add(this.label13);
            this.pnl_Left.Controls.Add(this.label11);
            this.pnl_Left.Controls.Add(this.label10);
            this.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Left.Location = new System.Drawing.Point(0, 85);
            this.pnl_Left.Name = "pnl_Left";
            this.pnl_Left.Padding = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.pnl_Left.Size = new System.Drawing.Size(229, 633);
            this.pnl_Left.TabIndex = 1;
            this.pnl_Left.Visible = false;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(227, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 625);
            this.label12.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(3, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 625);
            this.label13.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(225, 1);
            this.label11.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(3, 629);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(225, 1);
            this.label10.TabIndex = 20;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(229, 85);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 633);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.wdTemplate);
            //this.panel3.Controls.Add(this.wdTemp);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(232, 85);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.panel3.Size = new System.Drawing.Size(790, 633);
            this.panel3.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(786, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 625);
            this.label8.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(1, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 625);
            this.label7.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(1, 629);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(786, 1);
            this.label6.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(1, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(786, 1);
            this.label5.TabIndex = 19;
            // 
            // wdTemplate
            // 
            this.wdTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdTemplate.Enabled = true;
            this.wdTemplate.Location = new System.Drawing.Point(1, 3);
            this.wdTemplate.Name = "wdTemplate";
            this.wdTemplate.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdTemplate.OcxState")));
            this.wdTemplate.Size = new System.Drawing.Size(786, 627);
            this.wdTemplate.TabIndex = 0;
            this.wdTemplate.BeforeDocumentClosed += new AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEventHandler(this.wdTemplate_BeforeDocumentClosed);
            this.wdTemplate.OnDocumentOpened += new AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEventHandler(this.wdTemplate_OnDocumentOpened);
            this.wdTemplate.OnDocumentClosed += new System.EventHandler(this.wdTemplate_OnDocumentClosed);
            // 
            // wdTemp
            // 
            //this.wdTemp.Enabled = true;
            //this.wdTemp.Location = new System.Drawing.Point(8, 289);
            //this.wdTemp.Name = "wdTemp";
            //this.wdTemp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdTemp.OcxState")));
            //this.wdTemp.Size = new System.Drawing.Size(440, 332);
            //this.wdTemp.TabIndex = 23;
            //this.wdTemp.Visible = false;
            // 
            // pnl_DOS
            // 
            this.pnl_DOS.Controls.Add(this.label4);
            this.pnl_DOS.Controls.Add(this.label3);
            this.pnl_DOS.Controls.Add(this.label2);
            this.pnl_DOS.Controls.Add(this.label9);
            this.pnl_DOS.Controls.Add(this.dtp_DOS);
            this.pnl_DOS.Controls.Add(this.label1);
            this.pnl_DOS.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_DOS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_DOS.Location = new System.Drawing.Point(0, 53);
            this.pnl_DOS.Name = "pnl_DOS";
            this.pnl_DOS.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_DOS.Size = new System.Drawing.Size(1022, 32);
            this.pnl_DOS.TabIndex = 0;
            this.pnl_DOS.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(4, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1014, 1);
            this.label4.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(4, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1014, 1);
            this.label3.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(1018, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 26);
            this.label2.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 26);
            this.label9.TabIndex = 16;
            // 
            // dtp_DOS
            // 
            this.dtp_DOS.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_DOS.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtp_DOS.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtp_DOS.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtp_DOS.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtp_DOS.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtp_DOS.CustomFormat = "MM/dd/yyyy";
            this.dtp_DOS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_DOS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DOS.Location = new System.Drawing.Point(137, 5);
            this.dtp_DOS.Name = "dtp_DOS";
            this.dtp_DOS.Size = new System.Drawing.Size(124, 22);
            this.dtp_DOS.TabIndex = 1;
            this.dtp_DOS.Visible = false;
            this.dtp_DOS.ValueChanged += new System.EventHandler(this.dtp_DOS_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date of Service :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrDocProtect
            // 
            this.tmrDocProtect.Tick += new System.EventHandler(this.tmrDocProtect_Tick);
            // 
            // frmWd_PatientTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1022, 718);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnl_Left);
            this.Controls.Add(this.pnl_DOS);
            this.Controls.Add(this.pnl_TOP);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWd_PatientTemplate";
            this.ShowInTaskbar = false;
            this.Text = "Patient Template";
            this.Load += new System.EventHandler(this.frmWd_PatientTemplate_Load);
            this.pnl_TOP.ResumeLayout(false);
            this.pnl_TOP.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnl_Left.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.wdTemp)).EndInit();
            this.pnl_DOS.ResumeLayout(false);
            this.pnl_DOS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_TOP;
        private System.Windows.Forms.Panel pnl_Left;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private AxDSOFramer.AxFramerControl wdTemplate;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnl_DOS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_DOS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        //private AxDSOFramer.AxFramerControl wdTemp;
        internal System.Windows.Forms.ToolStripButton tsb_InsertImage;
        internal System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripButton tsb_InsertFile;
        private System.Windows.Forms.Timer tmrDocProtect;
    }
}