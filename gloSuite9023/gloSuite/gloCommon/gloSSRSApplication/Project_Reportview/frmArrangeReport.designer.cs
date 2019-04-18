namespace Project_Reportview
{
    partial class frmArrangeReport
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
                    components.Dispose();
                    base.Dispose(disposing);
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {

                    System.Windows.Forms.ContextMenuStrip[] cntmnuControls = { cntmnuEdit };
                    System.Windows.Forms.Control[] cntControls = { cntmnuEdit };
                    if (cntmnuControls != null)
                    {
                        if (cntmnuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmnuControls);

                        }
                    }
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
           
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArrangeReport));
            this.trv_Reports = new System.Windows.Forms.TreeView();
            this.cntmnuEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItem_AddReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_EditReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_DeleteReport = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tls_btn_SaveCls = new System.Windows.Forms.ToolStripButton();
            this.tls_btn_Cancel = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_UP = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cntmnuEdit.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // trv_Reports
            // 
            this.trv_Reports.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trv_Reports.ContextMenuStrip = this.cntmnuEdit;
            this.trv_Reports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trv_Reports.Location = new System.Drawing.Point(8, 8);
            this.trv_Reports.Name = "trv_Reports";
            this.trv_Reports.Size = new System.Drawing.Size(394, 453);
            this.trv_Reports.TabIndex = 1;
            this.trv_Reports.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_Reports_AfterSelect);
            this.trv_Reports.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trv_Reports_NodeMouseClick);
            // 
            // cntmnuEdit
            // 
            this.cntmnuEdit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cntmnuEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItem_AddReport,
            this.mnuItem_EditReport,
            this.mnuItem_DeleteReport});
            this.cntmnuEdit.Name = "cmnu_Appointment";
            this.cntmnuEdit.Size = new System.Drawing.Size(125, 70);
            // 
            // mnuItem_AddReport
            // 
            this.mnuItem_AddReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuItem_AddReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_AddReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_AddReport.Image")));
            this.mnuItem_AddReport.Name = "mnuItem_AddReport";
            this.mnuItem_AddReport.Size = new System.Drawing.Size(124, 22);
            this.mnuItem_AddReport.Text = "Add";
            this.mnuItem_AddReport.Click += new System.EventHandler(this.mnuItem_AddReport_Click);
            // 
            // mnuItem_EditReport
            // 
            this.mnuItem_EditReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuItem_EditReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_EditReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_EditReport.Image")));
            this.mnuItem_EditReport.Name = "mnuItem_EditReport";
            this.mnuItem_EditReport.Size = new System.Drawing.Size(124, 22);
            this.mnuItem_EditReport.Text = "Edit";
            this.mnuItem_EditReport.Click += new System.EventHandler(this.mnuItem_EditReport_Click);
            // 
            // mnuItem_DeleteReport
            // 
            this.mnuItem_DeleteReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuItem_DeleteReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_DeleteReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_DeleteReport.Image")));
            this.mnuItem_DeleteReport.Name = "mnuItem_DeleteReport";
            this.mnuItem_DeleteReport.Size = new System.Drawing.Size(124, 22);
            this.mnuItem_DeleteReport.Text = "Delete";
            this.mnuItem_DeleteReport.Click += new System.EventHandler(this.mnuItem_DeleteReport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(448, 54);
            this.panel2.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_btn_SaveCls,
            this.tls_btn_Cancel});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(448, 53);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tls_btn_SaveCls
            // 
            this.tls_btn_SaveCls.Image = ((System.Drawing.Image)(resources.GetObject("tls_btn_SaveCls.Image")));
            this.tls_btn_SaveCls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btn_SaveCls.Name = "tls_btn_SaveCls";
            this.tls_btn_SaveCls.Size = new System.Drawing.Size(66, 50);
            this.tls_btn_SaveCls.Text = "&Save&&Cls";
            this.tls_btn_SaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btn_SaveCls.ToolTipText = "Save and Close";
            this.tls_btn_SaveCls.Click += new System.EventHandler(this.tls_btn_SaveCls_Click);
            // 
            // tls_btn_Cancel
            // 
            this.tls_btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tls_btn_Cancel.Image")));
            this.tls_btn_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btn_Cancel.Name = "tls_btn_Cancel";
            this.tls_btn_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tls_btn_Cancel.Text = "&Close";
            this.tls_btn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btn_Cancel.Click += new System.EventHandler(this.tls_btn_Cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trv_Reports);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(406, 465);
            this.panel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(394, 4);
            this.label7.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(4, 457);
            this.label2.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(402, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 457);
            this.label6.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 457);
            this.label5.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 461);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(400, 1);
            this.label4.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(400, 1);
            this.label3.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btn_Down);
            this.panel3.Controls.Add(this.btn_UP);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(406, 54);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel3.Size = new System.Drawing.Size(42, 465);
            this.panel3.TabIndex = 2;
            // 
            // btn_Down
            // 
            this.btn_Down.FlatAppearance.BorderSize = 0;
            this.btn_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Down.Image = ((System.Drawing.Image)(resources.GetObject("btn_Down.Image")));
            this.btn_Down.Location = new System.Drawing.Point(6, 234);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(24, 24);
            this.btn_Down.TabIndex = 17;
            this.btn_Down.UseVisualStyleBackColor = true;
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // btn_UP
            // 
            this.btn_UP.FlatAppearance.BorderSize = 0;
            this.btn_UP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_UP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_UP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UP.Image = ((System.Drawing.Image)(resources.GetObject("btn_UP.Image")));
            this.btn_UP.Location = new System.Drawing.Point(6, 195);
            this.btn_UP.Name = "btn_UP";
            this.btn_UP.Size = new System.Drawing.Size(24, 24);
            this.btn_UP.TabIndex = 16;
            this.btn_UP.UseVisualStyleBackColor = true;
            this.btn_UP.Click += new System.EventHandler(this.btn_UP_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(38, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 457);
            this.label10.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 457);
            this.label11.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 461);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 1);
            this.label12.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 1);
            this.label13.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 3, 5, 0);
            this.label1.Size = new System.Drawing.Size(441, 163);
            this.label1.TabIndex = 18;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 519);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel4.Size = new System.Drawing.Size(448, 168);
            this.panel4.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(444, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 163);
            this.label8.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 163);
            this.label9.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 164);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(442, 1);
            this.label14.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(442, 1);
            this.label15.TabIndex = 12;
            // 
            // frmArrangeReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(448, 687);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArrangeReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SSRS Report Settings";
            this.Load += new System.EventHandler(this.frmArrangeReport_Load);
            this.cntmnuEdit.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public  System.Windows.Forms.TreeView trv_Reports;
        private System.Windows.Forms.ContextMenuStrip cntmnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_EditReport;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_AddReport;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_DeleteReport;
        private System.Windows.Forms.Panel panel2;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tls_btn_SaveCls;
        private System.Windows.Forms.ToolStripButton tls_btn_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_Down;
        private System.Windows.Forms.Button btn_UP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}