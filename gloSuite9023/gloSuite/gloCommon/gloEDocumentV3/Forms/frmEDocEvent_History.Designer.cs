
    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_History
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
                    _SelectedDocuments.Dispose();
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_History));
                this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
                this.tlb_Acknowledge = new System.Windows.Forms.ToolStripButton();
                this.tlb_Notes = new System.Windows.Forms.ToolStripButton();
                this.tlb_UserTags = new System.Windows.Forms.ToolStripButton();
                this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
                this.panel1 = new System.Windows.Forms.Panel();
                this.label2 = new System.Windows.Forms.Label();
                this.label10 = new System.Windows.Forms.Label();
                this.label11 = new System.Windows.Forms.Label();
                this.label12 = new System.Windows.Forms.Label();
                this.c1History = new C1.Win.C1FlexGrid.C1FlexGrid();
                this.label3 = new System.Windows.Forms.Label();
                this.label1 = new System.Windows.Forms.Label();
                this.panel3 = new System.Windows.Forms.Panel();
                this.comboBox1 = new System.Windows.Forms.ComboBox();
                this.label5 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.label7 = new System.Windows.Forms.Label();
                this.label8 = new System.Windows.Forms.Label();
                this.label9 = new System.Windows.Forms.Label();
                this.panel2 = new System.Windows.Forms.Panel();
                this.panel4 = new System.Windows.Forms.Panel();
                this.Panel9 = new System.Windows.Forms.Panel();
                this.Panel10 = new System.Windows.Forms.Panel();
                this.lblDescription = new System.Windows.Forms.Label();
                this.label13 = new System.Windows.Forms.Label();
                this.Label28 = new System.Windows.Forms.Label();
                this.Label29 = new System.Windows.Forms.Label();
                this.Label30 = new System.Windows.Forms.Label();
                this.Label31 = new System.Windows.Forms.Label();
                this.tls_MaintainDoc.SuspendLayout();
                this.panel1.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.c1History)).BeginInit();
                this.panel3.SuspendLayout();
                this.panel2.SuspendLayout();
                this.panel4.SuspendLayout();
                this.Panel9.SuspendLayout();
                this.Panel10.SuspendLayout();
                this.SuspendLayout();
                // 
                // tls_MaintainDoc
                // 
                this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
                this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.tls_MaintainDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
                this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Acknowledge,
            this.tlb_Notes,
            this.tlb_UserTags,
            this.tlb_Cancel});
                this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
                this.tls_MaintainDoc.Name = "tls_MaintainDoc";
                this.tls_MaintainDoc.Size = new System.Drawing.Size(594, 53);
                this.tls_MaintainDoc.TabIndex = 3;
                this.tls_MaintainDoc.Text = "toolStrip1";
                // 
                // tlb_Acknowledge
                // 
                this.tlb_Acknowledge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Acknowledge.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Acknowledge.Image")));
                this.tlb_Acknowledge.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_Acknowledge.Name = "tlb_Acknowledge";
                this.tlb_Acknowledge.Size = new System.Drawing.Size(93, 50);
                this.tlb_Acknowledge.Tag = "Acknowledge";
                this.tlb_Acknowledge.Text = "&Acknowledge";
                this.tlb_Acknowledge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_Acknowledge.ToolTipText = "Acknowledge";
                this.tlb_Acknowledge.Click += new System.EventHandler(this.tlb_Acknowledge_Click);
                // 
                // tlb_Notes
                // 
                this.tlb_Notes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Notes.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Notes.Image")));
                this.tlb_Notes.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_Notes.Name = "tlb_Notes";
                this.tlb_Notes.Size = new System.Drawing.Size(46, 50);
                this.tlb_Notes.Tag = "Notes";
                this.tlb_Notes.Text = "&Notes";
                this.tlb_Notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_Notes.ToolTipText = "Notes";
                this.tlb_Notes.Click += new System.EventHandler(this.tlb_Notes_Click);
                // 
                // tlb_UserTags
                // 
                this.tlb_UserTags.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_UserTags.Image = ((System.Drawing.Image)(resources.GetObject("tlb_UserTags.Image")));
                this.tlb_UserTags.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_UserTags.Name = "tlb_UserTags";
                this.tlb_UserTags.Size = new System.Drawing.Size(69, 50);
                this.tlb_UserTags.Tag = "UserTags";
                this.tlb_UserTags.Text = "&User Tags";
                this.tlb_UserTags.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_UserTags.ToolTipText = "User Tags";
                this.tlb_UserTags.Click += new System.EventHandler(this.tlb_UserTags_Click);
                // 
                // tlb_Cancel
                // 
                this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
                this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
                this.tlb_Cancel.Name = "tlb_Cancel";
                this.tlb_Cancel.Size = new System.Drawing.Size(43, 50);
                this.tlb_Cancel.Tag = "Close";
                this.tlb_Cancel.Text = "&Close";
                this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
                this.tlb_Cancel.ToolTipText = "Close";
                this.tlb_Cancel.Click += new System.EventHandler(this.tlb_Cancel_Click);
                // 
                // panel1
                // 
                this.panel1.BackColor = System.Drawing.Color.Transparent;
                this.panel1.Controls.Add(this.label2);
                this.panel1.Controls.Add(this.label10);
                this.panel1.Controls.Add(this.label11);
                this.panel1.Controls.Add(this.label12);
                this.panel1.Controls.Add(this.c1History);
                this.panel1.Controls.Add(this.label3);
                this.panel1.Controls.Add(this.label1);
                this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel1.Location = new System.Drawing.Point(0, 113);
                this.panel1.Margin = new System.Windows.Forms.Padding(2);
                this.panel1.Name = "panel1";
                this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
                this.panel1.Size = new System.Drawing.Size(594, 364);
                this.panel1.TabIndex = 19;
                // 
                // label2
                // 
                this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
                this.label2.Location = new System.Drawing.Point(4, 359);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(585, 1);
                this.label2.TabIndex = 32;
                this.label2.Text = "label2";
                // 
                // label10
                // 
                this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label10.Dock = System.Windows.Forms.DockStyle.Left;
                this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label10.Location = new System.Drawing.Point(3, 1);
                this.label10.Name = "label10";
                this.label10.Size = new System.Drawing.Size(1, 359);
                this.label10.TabIndex = 31;
                this.label10.Text = "label4";
                // 
                // label11
                // 
                this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label11.Dock = System.Windows.Forms.DockStyle.Right;
                this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
                this.label11.Location = new System.Drawing.Point(589, 1);
                this.label11.Name = "label11";
                this.label11.Size = new System.Drawing.Size(1, 359);
                this.label11.TabIndex = 30;
                this.label11.Text = "label3";
                // 
                // label12
                // 
                this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label12.Dock = System.Windows.Forms.DockStyle.Top;
                this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label12.Location = new System.Drawing.Point(3, 0);
                this.label12.Name = "label12";
                this.label12.Size = new System.Drawing.Size(587, 1);
                this.label12.TabIndex = 29;
                this.label12.Text = "label1";
                // 
                // c1History
                // 
                this.c1History.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                this.c1History.AllowEditing = false;
                this.c1History.BackColor = System.Drawing.Color.White;
                this.c1History.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
                this.c1History.ColumnInfo = "9,0,0,0,0,95,Columns:";
                this.c1History.Dock = System.Windows.Forms.DockStyle.Fill;
                this.c1History.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
                this.c1History.Location = new System.Drawing.Point(3, 0);
                this.c1History.Margin = new System.Windows.Forms.Padding(2);
                this.c1History.Name = "c1History";
                this.c1History.Rows.DefaultSize = 19;
                this.c1History.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                this.c1History.Size = new System.Drawing.Size(587, 360);
                this.c1History.StyleInfo = resources.GetString("c1History.StyleInfo");
                this.c1History.TabIndex = 28;
                this.c1History.Tree.NodeImageCollapsed = global::gloEDocumentV3.Properties.Resources.Plus;
                this.c1History.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1History.Tree.NodeImageExpanded")));
                this.c1History.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
                // 
                // label3
                // 
                this.label3.BackColor = System.Drawing.Color.Black;
                this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.label3.Location = new System.Drawing.Point(3, 360);
                this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(587, 1);
                this.label3.TabIndex = 25;
                this.label3.Text = "label3";
                // 
                // label1
                // 
                this.label1.BackColor = System.Drawing.Color.Black;
                this.label1.Dock = System.Windows.Forms.DockStyle.Right;
                this.label1.Location = new System.Drawing.Point(590, 0);
                this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(1, 361);
                this.label1.TabIndex = 23;
                this.label1.Text = "label1";
                // 
                // panel3
                // 
                this.panel3.BackColor = System.Drawing.Color.Transparent;
                this.panel3.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
                this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.panel3.Controls.Add(this.comboBox1);
                this.panel3.Controls.Add(this.label5);
                this.panel3.Controls.Add(this.label4);
                this.panel3.Controls.Add(this.label7);
                this.panel3.Controls.Add(this.label8);
                this.panel3.Controls.Add(this.label9);
                this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel3.Location = new System.Drawing.Point(3, 3);
                this.panel3.Margin = new System.Windows.Forms.Padding(2);
                this.panel3.Name = "panel3";
                this.panel3.Size = new System.Drawing.Size(588, 24);
                this.panel3.TabIndex = 27;
                this.panel3.Visible = false;
                // 
                // comboBox1
                // 
                this.comboBox1.Dock = System.Windows.Forms.DockStyle.Left;
                this.comboBox1.FormattingEnabled = true;
                this.comboBox1.Location = new System.Drawing.Point(64, 1);
                this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
                this.comboBox1.Name = "comboBox1";
                this.comboBox1.Size = new System.Drawing.Size(177, 22);
                this.comboBox1.TabIndex = 4;
                // 
                // label5
                // 
                this.label5.AutoEllipsis = true;
                this.label5.AutoSize = true;
                this.label5.BackColor = System.Drawing.Color.Transparent;
                this.label5.Dock = System.Windows.Forms.DockStyle.Left;
                this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label5.Location = new System.Drawing.Point(1, 1);
                this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label5.Name = "label5";
                this.label5.Padding = new System.Windows.Forms.Padding(2, 3, 2, 2);
                this.label5.Size = new System.Drawing.Size(63, 19);
                this.label5.TabIndex = 2;
                this.label5.Text = "   User :";
                this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                // 
                // label4
                // 
                this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
                this.label4.Location = new System.Drawing.Point(1, 23);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(586, 1);
                this.label4.TabIndex = 12;
                this.label4.Text = "label2";
                // 
                // label7
                // 
                this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label7.Dock = System.Windows.Forms.DockStyle.Left;
                this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label7.Location = new System.Drawing.Point(0, 1);
                this.label7.Name = "label7";
                this.label7.Size = new System.Drawing.Size(1, 23);
                this.label7.TabIndex = 11;
                this.label7.Text = "label4";
                // 
                // label8
                // 
                this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label8.Dock = System.Windows.Forms.DockStyle.Right;
                this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
                this.label8.Location = new System.Drawing.Point(587, 1);
                this.label8.Name = "label8";
                this.label8.Size = new System.Drawing.Size(1, 23);
                this.label8.TabIndex = 10;
                this.label8.Text = "label3";
                // 
                // label9
                // 
                this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.label9.Dock = System.Windows.Forms.DockStyle.Top;
                this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label9.Location = new System.Drawing.Point(0, 0);
                this.label9.Name = "label9";
                this.label9.Size = new System.Drawing.Size(588, 1);
                this.label9.TabIndex = 9;
                this.label9.Text = "label1";
                // 
                // panel2
                // 
                this.panel2.Controls.Add(this.panel3);
                this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
                this.panel2.Location = new System.Drawing.Point(0, 54);
                this.panel2.Name = "panel2";
                this.panel2.Padding = new System.Windows.Forms.Padding(3);
                this.panel2.Size = new System.Drawing.Size(594, 30);
                this.panel2.TabIndex = 28;
                this.panel2.Visible = false;
                // 
                // panel4
                // 
                this.panel4.Controls.Add(this.tls_MaintainDoc);
                this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
                this.panel4.Location = new System.Drawing.Point(0, 0);
                this.panel4.Name = "panel4";
                this.panel4.Size = new System.Drawing.Size(594, 54);
                this.panel4.TabIndex = 29;
                // 
                // Panel9
                // 
                this.Panel9.Controls.Add(this.Panel10);
                this.Panel9.Dock = System.Windows.Forms.DockStyle.Top;
                this.Panel9.Location = new System.Drawing.Point(0, 84);
                this.Panel9.Name = "Panel9";
                this.Panel9.Padding = new System.Windows.Forms.Padding(3);
                this.Panel9.Size = new System.Drawing.Size(594, 29);
                this.Panel9.TabIndex = 30;
                // 
                // Panel10
                // 
                this.Panel10.BackColor = System.Drawing.Color.Transparent;
                this.Panel10.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
                this.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.Panel10.Controls.Add(this.lblDescription);
                this.Panel10.Controls.Add(this.label13);
                this.Panel10.Controls.Add(this.Label28);
                this.Panel10.Controls.Add(this.Label29);
                this.Panel10.Controls.Add(this.Label30);
                this.Panel10.Controls.Add(this.Label31);
                this.Panel10.Dock = System.Windows.Forms.DockStyle.Fill;
                this.Panel10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.Panel10.Location = new System.Drawing.Point(3, 3);
                this.Panel10.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
                this.Panel10.Name = "Panel10";
                this.Panel10.Size = new System.Drawing.Size(588, 23);
                this.Panel10.TabIndex = 19;
                // 
                // lblDescription
                // 
                this.lblDescription.AutoSize = true;
                this.lblDescription.BackColor = System.Drawing.Color.Transparent;
                this.lblDescription.Dock = System.Windows.Forms.DockStyle.Left;
                this.lblDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblDescription.Location = new System.Drawing.Point(97, 1);
                this.lblDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.lblDescription.Name = "lblDescription";
                this.lblDescription.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
                this.lblDescription.Size = new System.Drawing.Size(4, 20);
                this.lblDescription.TabIndex = 25;
                // 
                // label13
                // 
                this.label13.AutoSize = true;
                this.label13.BackColor = System.Drawing.Color.Transparent;
                this.label13.Dock = System.Windows.Forms.DockStyle.Left;
                this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label13.Location = new System.Drawing.Point(1, 1);
                this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                this.label13.Name = "label13";
                this.label13.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
                this.label13.Size = new System.Drawing.Size(96, 20);
                this.label13.TabIndex = 24;
                this.label13.Text = "  Description :";
                // 
                // Label28
                // 
                this.Label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.Label28.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.Label28.Font = new System.Drawing.Font("Tahoma", 9F);
                this.Label28.Location = new System.Drawing.Point(1, 22);
                this.Label28.Name = "Label28";
                this.Label28.Size = new System.Drawing.Size(586, 1);
                this.Label28.TabIndex = 8;
                this.Label28.Text = "label2";
                // 
                // Label29
                // 
                this.Label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.Label29.Dock = System.Windows.Forms.DockStyle.Left;
                this.Label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.Label29.Location = new System.Drawing.Point(0, 1);
                this.Label29.Name = "Label29";
                this.Label29.Size = new System.Drawing.Size(1, 22);
                this.Label29.TabIndex = 7;
                this.Label29.Text = "label4";
                // 
                // Label30
                // 
                this.Label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.Label30.Dock = System.Windows.Forms.DockStyle.Right;
                this.Label30.Font = new System.Drawing.Font("Tahoma", 9F);
                this.Label30.Location = new System.Drawing.Point(587, 1);
                this.Label30.Name = "Label30";
                this.Label30.Size = new System.Drawing.Size(1, 22);
                this.Label30.TabIndex = 6;
                this.Label30.Text = "label3";
                // 
                // Label31
                // 
                this.Label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.Label31.Dock = System.Windows.Forms.DockStyle.Top;
                this.Label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.Label31.Location = new System.Drawing.Point(0, 0);
                this.Label31.Name = "Label31";
                this.Label31.Size = new System.Drawing.Size(588, 1);
                this.Label31.TabIndex = 5;
                this.Label31.Text = "label1";
                // 
                // frmEDocEvent_History
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                this.AutoSize = true;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
                this.ClientSize = new System.Drawing.Size(594, 477);
                this.Controls.Add(this.panel1);
                this.Controls.Add(this.Panel9);
                this.Controls.Add(this.panel2);
                this.Controls.Add(this.panel4);
                this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "frmEDocEvent_History";
                this.ShowInTaskbar = false;
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Amendments";
                this.Load += new System.EventHandler(this.frmEDocEvent_History_Load);
                this.tls_MaintainDoc.ResumeLayout(false);
                this.tls_MaintainDoc.PerformLayout();
                this.panel1.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.c1History)).EndInit();
                this.panel3.ResumeLayout(false);
                this.panel3.PerformLayout();
                this.panel2.ResumeLayout(false);
                this.panel4.ResumeLayout(false);
                this.panel4.PerformLayout();
                this.Panel9.ResumeLayout(false);
                this.Panel10.ResumeLayout(false);
                this.Panel10.PerformLayout();
                this.ResumeLayout(false);

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ToolStripButton tlb_Acknowledge;
            private System.Windows.Forms.ToolStripButton tlb_UserTags;
            private System.Windows.Forms.ToolStripButton tlb_Notes;
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Panel panel3;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.ComboBox comboBox1;
            private C1.Win.C1FlexGrid.C1FlexGrid c1History;
            private System.Windows.Forms.Panel panel2;
            private System.Windows.Forms.Panel panel4;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label10;
            private System.Windows.Forms.Label label11;
            private System.Windows.Forms.Label label12;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label7;
            private System.Windows.Forms.Label label8;
            private System.Windows.Forms.Label label9;
            internal System.Windows.Forms.Panel Panel9;
            internal System.Windows.Forms.Panel Panel10;
            private System.Windows.Forms.Label lblDescription;
            private System.Windows.Forms.Label label13;
            private System.Windows.Forms.Label Label28;
            private System.Windows.Forms.Label Label29;
            private System.Windows.Forms.Label Label30;
            private System.Windows.Forms.Label Label31;
        }
    }
