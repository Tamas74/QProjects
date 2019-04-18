namespace gloEDocumentV3.Forms
{
    partial class frmEDocEvent_ExportToPDF
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
                components.Dispose();

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
               
                
                try
                {
                    if (dlgFolderPath != null)
                    {

                        dlgFolderPath.Dispose();
                        dlgFolderPath = null;
                    }
                }
                catch
                {
                }
            }
            base.Dispose(disposing);

            System.Windows.Forms.ContextMenu[] cmControls = { cmnuToRename };
            
            if (cmControls != null)
            {
                if (cmControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cmControls);
                    gloGlobal.cEventHelper.DisposeContextMenu(ref cmControls);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_ExportToPDF));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.c1Documents = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnl_ProgressBar = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tls_MaintainDoc = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_ExportMain = new System.Windows.Forms.ToolStripButton();
            this.tlb_RenameMain = new System.Windows.Forms.ToolStripButton();
            this.tlb_ImportReport = new System.Windows.Forms.ToolStripButton();
            this.tlbRemove = new System.Windows.Forms.ToolStripButton();
            this.tlb_CancelMain = new System.Windows.Forms.ToolStripButton();
            this.pnl_Raname = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.txtSourceDocument = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tl_Rename = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_OKRename = new System.Windows.Forms.ToolStripButton();
            this.tsb_CancelRename = new System.Windows.Forms.ToolStripButton();
            this.cmnuToRename = new System.Windows.Forms.ContextMenu();
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.dlgFolderPath = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Documents)).BeginInit();
            this.pnl_ProgressBar.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tls_MaintainDoc.SuspendLayout();
            this.pnl_Raname.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tl_Rename.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.pnl_ProgressBar);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 309);
            this.panel1.TabIndex = 20;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.c1Documents);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 54);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(382, 236);
            this.panel4.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(4, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(374, 1);
            this.label5.TabIndex = 29;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(4, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(374, 1);
            this.label6.TabIndex = 28;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 230);
            this.label7.TabIndex = 26;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(378, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 230);
            this.label8.TabIndex = 27;
            this.label8.Text = "label8";
            // 
            // c1Documents
            // 
            this.c1Documents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Documents.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Documents.ColumnInfo = "5,0,0,0,0,95,Columns:0{Width:72;}\t1{Width:87;}\t";
            this.c1Documents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Documents.ExtendLastCol = true;
            this.c1Documents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Documents.Location = new System.Drawing.Point(3, 3);
            this.c1Documents.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.c1Documents.Name = "c1Documents";
            this.c1Documents.Rows.Count = 1;
            this.c1Documents.Rows.DefaultSize = 19;
            this.c1Documents.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Documents.Size = new System.Drawing.Size(376, 230);
            this.c1Documents.StyleInfo = resources.GetString("c1Documents.StyleInfo");
            this.c1Documents.TabIndex = 10;
            this.c1Documents.Tree.NodeImageCollapsed = global::gloEDocumentV3.Properties.Resources.Plus;
            this.c1Documents.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1Documents.Tree.NodeImageExpanded")));
            this.c1Documents.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1Documents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Documents_MouseDown);
            // 
            // pnl_ProgressBar
            // 
            this.pnl_ProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.pnl_ProgressBar.Controls.Add(this.label9);
            this.pnl_ProgressBar.Controls.Add(this.label10);
            this.pnl_ProgressBar.Controls.Add(this.label11);
            this.pnl_ProgressBar.Controls.Add(this.label12);
            this.pnl_ProgressBar.Controls.Add(this.pbDocument);
            this.pnl_ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_ProgressBar.Location = new System.Drawing.Point(0, 290);
            this.pnl_ProgressBar.Name = "pnl_ProgressBar";
            this.pnl_ProgressBar.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnl_ProgressBar.Size = new System.Drawing.Size(382, 19);
            this.pnl_ProgressBar.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(4, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(374, 1);
            this.label9.TabIndex = 29;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(374, 1);
            this.label10.TabIndex = 28;
            this.label10.Text = "label10";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 16);
            this.label11.TabIndex = 26;
            this.label11.Text = "label11";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(378, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 16);
            this.label12.TabIndex = 27;
            this.label12.Text = "label12";
            // 
            // pbDocument
            // 
            this.pbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbDocument.Location = new System.Drawing.Point(3, 0);
            this.pbDocument.Margin = new System.Windows.Forms.Padding(2);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(376, 16);
            this.pbDocument.TabIndex = 15;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel5.Controls.Add(this.tls_MaintainDoc);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(382, 54);
            this.panel5.TabIndex = 12;
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.BackColor = System.Drawing.Color.Transparent;
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_ExportMain,
            this.tlb_RenameMain,
            this.tlb_ImportReport,
            this.tlbRemove,
            this.tlb_CancelMain});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(382, 53);
            this.tls_MaintainDoc.TabIndex = 11;
            this.tls_MaintainDoc.Text = "toolStrip1";
            this.tls_MaintainDoc.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_MaintainDoc_ItemClicked);
            // 
            // tlb_ExportMain
            // 
            this.tlb_ExportMain.BackColor = System.Drawing.Color.Transparent;
            this.tlb_ExportMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_ExportMain.Image = ((System.Drawing.Image)(resources.GetObject("tlb_ExportMain.Image")));
            this.tlb_ExportMain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_ExportMain.Name = "tlb_ExportMain";
            this.tlb_ExportMain.Size = new System.Drawing.Size(56, 50);
            this.tlb_ExportMain.Tag = "OK";
            this.tlb_ExportMain.Text = " &Export";
            this.tlb_ExportMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_ExportMain.ToolTipText = "Export";
            this.tlb_ExportMain.Click += new System.EventHandler(this.tlb_ExportMain_Click);
            // 
            // tlb_RenameMain
            // 
            this.tlb_RenameMain.BackColor = System.Drawing.Color.Transparent;
            this.tlb_RenameMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_RenameMain.Image = ((System.Drawing.Image)(resources.GetObject("tlb_RenameMain.Image")));
            this.tlb_RenameMain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_RenameMain.Name = "tlb_RenameMain";
            this.tlb_RenameMain.Size = new System.Drawing.Size(60, 50);
            this.tlb_RenameMain.Tag = "Rename";
            this.tlb_RenameMain.Text = "Re&name";
            this.tlb_RenameMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_RenameMain.ToolTipText = "Rename";
            this.tlb_RenameMain.Click += new System.EventHandler(this.tlb_RenameMain_Click);
            // 
            // tlb_ImportReport
            // 
            this.tlb_ImportReport.BackColor = System.Drawing.Color.Transparent;
            this.tlb_ImportReport.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_ImportReport.Image = ((System.Drawing.Image)(resources.GetObject("tlb_ImportReport.Image")));
            this.tlb_ImportReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_ImportReport.Name = "tlb_ImportReport";
            this.tlb_ImportReport.Size = new System.Drawing.Size(83, 50);
            this.tlb_ImportReport.Tag = "ImportReport";
            this.tlb_ImportReport.Text = "&ViewReport";
            this.tlb_ImportReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_ImportReport.Click += new System.EventHandler(this.tlb_ImportReport_Click);
            // 
            // tlbRemove
            // 
            this.tlbRemove.BackColor = System.Drawing.Color.Transparent;
            this.tlbRemove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbRemove.Image = ((System.Drawing.Image)(resources.GetObject("tlbRemove.Image")));
            this.tlbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbRemove.Name = "tlbRemove";
            this.tlbRemove.Size = new System.Drawing.Size(60, 50);
            this.tlbRemove.Tag = "Remove";
            this.tlbRemove.Text = "&Remove";
            this.tlbRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbRemove.ToolTipText = "Remove";
            this.tlbRemove.Click += new System.EventHandler(this.tlbRemove_Click);
            // 
            // tlb_CancelMain
            // 
            this.tlb_CancelMain.BackColor = System.Drawing.Color.Transparent;
            this.tlb_CancelMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_CancelMain.Image = ((System.Drawing.Image)(resources.GetObject("tlb_CancelMain.Image")));
            this.tlb_CancelMain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_CancelMain.Name = "tlb_CancelMain";
            this.tlb_CancelMain.Size = new System.Drawing.Size(47, 50);
            this.tlb_CancelMain.Tag = "Cancel";
            this.tlb_CancelMain.Text = " &Close";
            this.tlb_CancelMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_CancelMain.ToolTipText = "Close";
            this.tlb_CancelMain.Click += new System.EventHandler(this.tlb_CancelMain_Click);
            // 
            // pnl_Raname
            // 
            this.pnl_Raname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_Raname.Controls.Add(this.panel2);
            this.pnl_Raname.Controls.Add(this.panel3);
            this.pnl_Raname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Raname.Location = new System.Drawing.Point(0, 0);
            this.pnl_Raname.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnl_Raname.Name = "pnl_Raname";
            this.pnl_Raname.Size = new System.Drawing.Size(382, 309);
            this.pnl_Raname.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label59);
            this.panel2.Controls.Add(this.txtSourceDocument);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(382, 255);
            this.panel2.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 1);
            this.label3.TabIndex = 25;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(374, 1);
            this.label2.TabIndex = 24;
            this.label2.Text = "label2";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Left;
            this.label59.Location = new System.Drawing.Point(3, 3);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 249);
            this.label59.TabIndex = 22;
            this.label59.Text = "label59";
            // 
            // txtSourceDocument
            // 
            this.txtSourceDocument.ForeColor = System.Drawing.Color.Black;
            this.txtSourceDocument.Location = new System.Drawing.Point(113, 86);
            this.txtSourceDocument.Margin = new System.Windows.Forms.Padding(0);
            this.txtSourceDocument.MaxLength = 150;
            this.txtSourceDocument.Name = "txtSourceDocument";
            this.txtSourceDocument.Size = new System.Drawing.Size(203, 22);
            this.txtSourceDocument.TabIndex = 16;
            this.txtSourceDocument.TextChanged += new System.EventHandler(this.txtSourceDocument_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(52, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 15;
            this.label1.Text = "Rename :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(378, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 249);
            this.label4.TabIndex = 23;
            this.label4.Text = "label4";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel3.Controls.Add(this.tl_Rename);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(382, 54);
            this.panel3.TabIndex = 26;
            // 
            // tl_Rename
            // 
            this.tl_Rename.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tl_Rename.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tl_Rename.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tl_Rename.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_OKRename,
            this.tsb_CancelRename});
            this.tl_Rename.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tl_Rename.Location = new System.Drawing.Point(0, 0);
            this.tl_Rename.Name = "tl_Rename";
            this.tl_Rename.Size = new System.Drawing.Size(382, 53);
            this.tl_Rename.TabIndex = 12;
            this.tl_Rename.Text = "toolStrip1";
            // 
            // tsb_OKRename
            // 
            this.tsb_OKRename.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OKRename.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OKRename.Image")));
            this.tsb_OKRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OKRename.Name = "tsb_OKRename";
            this.tsb_OKRename.Size = new System.Drawing.Size(40, 50);
            this.tsb_OKRename.Tag = "OK";
            this.tsb_OKRename.Text = "&Save";
            this.tsb_OKRename.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OKRename.ToolTipText = "Save";
            this.tsb_OKRename.Click += new System.EventHandler(this.tsb_OK_Click);
            // 
            // tsb_CancelRename
            // 
            this.tsb_CancelRename.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_CancelRename.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CancelRename.Image")));
            this.tsb_CancelRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CancelRename.Name = "tsb_CancelRename";
            this.tsb_CancelRename.Size = new System.Drawing.Size(54, 50);
            this.tsb_CancelRename.Tag = "Cancel";
            this.tsb_CancelRename.Text = " &Cancel";
            this.tsb_CancelRename.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_CancelRename.ToolTipText = "Cancel";
            this.tsb_CancelRename.Click += new System.EventHandler(this.tsb_CancelRename_Click);
            // 
            // cmnuToRename
            // 
            this.cmnuToRename.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem1,
            this.menuItem2});
            this.cmnuToRename.Tag = "Rename";
            // 
            // MenuItem1
            // 
            this.MenuItem1.Index = 0;
            this.MenuItem1.Text = "Rename";
            this.MenuItem1.Click += new System.EventHandler(this.MenuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Remove";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // frmEDocEvent_ExportToPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(382, 309);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_Raname);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEDocEvent_ExportToPDF";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Document";
            this.Load += new System.EventHandler(this.frmEDocEvent_ExportToPDF_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Documents)).EndInit();
            this.pnl_ProgressBar.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.pnl_Raname.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tl_Rename.ResumeLayout(false);
            this.tl_Rename.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Documents;
        private gloGlobal.gloToolStripIgnoreFocus tls_MaintainDoc;
        private System.Windows.Forms.ToolStripButton tlb_ExportMain;
        private System.Windows.Forms.ToolStripButton tlb_RenameMain;
        private System.Windows.Forms.ToolStripButton tlb_CancelMain;
        private System.Windows.Forms.Panel pnl_Raname;
        private gloGlobal.gloToolStripIgnoreFocus tl_Rename;
        private System.Windows.Forms.ToolStripButton tsb_OKRename;
        private System.Windows.Forms.ToolStripButton tsb_CancelRename;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtSourceDocument;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ContextMenu cmnuToRename;
        internal System.Windows.Forms.MenuItem MenuItem1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderPath;
        private System.Windows.Forms.ToolStripButton tlb_ImportReport;
        private System.Windows.Forms.Panel pnl_ProgressBar;
        private System.Windows.Forms.ProgressBar pbDocument;
        private System.Windows.Forms.ToolStripButton tlbRemove;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}