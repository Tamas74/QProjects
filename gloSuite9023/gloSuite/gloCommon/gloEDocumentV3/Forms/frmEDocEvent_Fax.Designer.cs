
    namespace gloEDocumentV3.Forms
    {
        public partial class frmEDocEvent_Fax
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

                System.Windows.Forms.ContextMenu[] cmControls = { cmnuDeleteFaxTo, cmnuAddFaxTo };

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocEvent_Fax));
            this.tls_Fax = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlb_Update = new System.Windows.Forms.ToolStripButton();
            this.tlb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tlb_Ok = new System.Windows.Forms.ToolStripButton();
            this.tlb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pbDocument = new System.Windows.Forms.ProgressBar();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnldsoFAXPreview = new System.Windows.Forms.Panel();
            this.pnlCoverPage = new System.Windows.Forms.Panel();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.dsoFAXPreview = new AxDSOFramer.AxFramerControl();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.optHighPriority = new System.Windows.Forms.RadioButton();
            this.optNormalPriority = new System.Windows.Forms.RadioButton();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.pnlc1FaxListHeader = new System.Windows.Forms.Panel();
            this.pnlC1FaxList = new System.Windows.Forms.Panel();
            this.c1FaxList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlSearchDocument = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearchDocument = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.Label77 = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSearchOn = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlFaxProgressBar = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlFaxNoandDetail = new System.Windows.Forms.Panel();
            this.lblFaxDetails = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.mskFaxNo = new gloMaskControl.gloMaskBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCoverPage = new System.Windows.Forms.ComboBox();
            this.btnDown1 = new System.Windows.Forms.Button();
            this.btnUp1 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wdFaxCoverpage = new AxDSOFramer.AxFramerControl();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.pnlFaxTo = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.trvFaxTo = new System.Windows.Forms.TreeView();
            this.imgFax = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel7 = new System.Windows.Forms.Panel();
            this.trvContactType = new System.Windows.Forms.TreeView();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.txtFAXNo = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cmnuAddFaxTo = new System.Windows.Forms.ContextMenu();
            this.mnuFaxTo = new System.Windows.Forms.MenuItem();
            this.cmnuDeleteFaxTo = new System.Windows.Forms.ContextMenu();
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.tls_Fax.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnldsoFAXPreview.SuspendLayout();
            this.pnlCoverPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsoFAXPreview)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlc1FaxListHeader.SuspendLayout();
            this.pnlC1FaxList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FaxList)).BeginInit();
            this.panel4.SuspendLayout();
            this.pnlSearchDocument.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlFaxProgressBar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlFaxNoandDetail.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdFaxCoverpage)).BeginInit();
            this.pnlFaxTo.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tls_Fax
            // 
            this.tls_Fax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_Fax.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_Fax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Fax.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Fax.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlb_Update,
            this.tlb_Refresh,
            this.tlb_Ok,
            this.tlb_Cancel});
            this.tls_Fax.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_Fax.Location = new System.Drawing.Point(0, 0);
            this.tls_Fax.Name = "tls_Fax";
            this.tls_Fax.Size = new System.Drawing.Size(917, 53);
            this.tls_Fax.TabIndex = 3;
            this.tls_Fax.Text = "toolStrip1";
            this.tls_Fax.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tls_Fax_ItemClicked);
            // 
            // tlb_Update
            // 
            this.tlb_Update.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Update.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Update.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Update.Image")));
            this.tlb_Update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Update.Name = "tlb_Update";
            this.tlb_Update.Size = new System.Drawing.Size(103, 50);
            this.tlb_Update.Tag = "Update";
            this.tlb_Update.Text = "&Update Fax No.";
            this.tlb_Update.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Update.ToolTipText = "Update Fax Number";
            this.tlb_Update.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tlb_Refresh
            // 
            this.tlb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Refresh.Image")));
            this.tlb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Refresh.Name = "tlb_Refresh";
            this.tlb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tlb_Refresh.Tag = "Refresh";
            this.tlb_Refresh.Text = "&Refresh";
            this.tlb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Refresh.ToolTipText = "Refresh";
            // 
            // tlb_Ok
            // 
            this.tlb_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Ok.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Ok.Image")));
            this.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Ok.Name = "tlb_Ok";
            this.tlb_Ok.Size = new System.Drawing.Size(57, 50);
            this.tlb_Ok.Tag = "OK";
            this.tlb_Ok.Text = "&Fax&&Cls";
            this.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Ok.ToolTipText = "Fax and Close";
            // 
            // tlb_Cancel
            // 
            this.tlb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tlb_Cancel.Image")));
            this.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_Cancel.Name = "tlb_Cancel";
            this.tlb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tlb_Cancel.Tag = "Cancel";
            this.tlb_Cancel.Text = "&Close";
            this.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlb_Cancel.ToolTipText = "Close";
            // 
            // pbDocument
            // 
            this.pbDocument.Location = new System.Drawing.Point(6, 3);
            this.pbDocument.Margin = new System.Windows.Forms.Padding(2);
            this.pbDocument.Name = "pbDocument";
            this.pbDocument.Size = new System.Drawing.Size(695, 16);
            this.pbDocument.TabIndex = 5;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnldsoFAXPreview);
            this.pnlMain.Controls.Add(this.panel5);
            this.pnlMain.Controls.Add(this.pnlc1FaxListHeader);
            this.pnlMain.Controls.Add(this.pnlFaxProgressBar);
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.txtNotes);
            this.pnlMain.Controls.Add(this.pnlFaxTo);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(917, 594);
            this.pnlMain.TabIndex = 19;
            // 
            // pnldsoFAXPreview
            // 
            this.pnldsoFAXPreview.Controls.Add(this.pnlCoverPage);
            this.pnldsoFAXPreview.Controls.Add(this.panel9);
            this.pnldsoFAXPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnldsoFAXPreview.Location = new System.Drawing.Point(208, 253);
            this.pnldsoFAXPreview.Name = "pnldsoFAXPreview";
            this.pnldsoFAXPreview.Size = new System.Drawing.Size(709, 316);
            this.pnldsoFAXPreview.TabIndex = 215;
            // 
            // pnlCoverPage
            // 
            this.pnlCoverPage.Controls.Add(this.label46);
            this.pnlCoverPage.Controls.Add(this.label47);
            this.pnlCoverPage.Controls.Add(this.label48);
            this.pnlCoverPage.Controls.Add(this.label49);
            this.pnlCoverPage.Controls.Add(this.dsoFAXPreview);
            this.pnlCoverPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCoverPage.Location = new System.Drawing.Point(0, 27);
            this.pnlCoverPage.Name = "pnlCoverPage";
            this.pnlCoverPage.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlCoverPage.Size = new System.Drawing.Size(709, 289);
            this.pnlCoverPage.TabIndex = 213;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label46.Location = new System.Drawing.Point(1, 285);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(704, 1);
            this.label46.TabIndex = 8;
            this.label46.Text = "label2";
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(0, 1);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1, 285);
            this.label47.TabIndex = 7;
            this.label47.Text = "label4";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label48.Location = new System.Drawing.Point(705, 1);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1, 285);
            this.label48.TabIndex = 6;
            this.label48.Text = "label3";
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Top;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(0, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(706, 1);
            this.label49.TabIndex = 5;
            this.label49.Text = "label1";
            // 
            // dsoFAXPreview
            // 
            this.dsoFAXPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dsoFAXPreview.Enabled = true;
            this.dsoFAXPreview.Location = new System.Drawing.Point(0, 0);
            this.dsoFAXPreview.Name = "dsoFAXPreview";
            this.dsoFAXPreview.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("dsoFAXPreview.OcxState")));
            this.dsoFAXPreview.Size = new System.Drawing.Size(706, 286);
            this.dsoFAXPreview.TabIndex = 4;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel9.Size = new System.Drawing.Size(709, 27);
            this.panel9.TabIndex = 212;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel10.Controls.Add(this.label27);
            this.panel10.Controls.Add(this.optHighPriority);
            this.panel10.Controls.Add(this.optNormalPriority);
            this.panel10.Controls.Add(this.cmbTemplate);
            this.panel10.Controls.Add(this.label28);
            this.panel10.Controls.Add(this.label42);
            this.panel10.Controls.Add(this.label43);
            this.panel10.Controls.Add(this.label44);
            this.panel10.Controls.Add(this.label45);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(706, 24);
            this.panel10.TabIndex = 0;
            this.panel10.TabStop = true;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(399, 1);
            this.label27.Name = "label27";
            this.label27.Padding = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.label27.Size = new System.Drawing.Size(140, 22);
            this.label27.TabIndex = 2;
            this.label27.Text = "Select Template :";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // optHighPriority
            // 
            this.optHighPriority.BackColor = System.Drawing.Color.Transparent;
            this.optHighPriority.Dock = System.Windows.Forms.DockStyle.Left;
            this.optHighPriority.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optHighPriority.Location = new System.Drawing.Point(243, 1);
            this.optHighPriority.Name = "optHighPriority";
            this.optHighPriority.Size = new System.Drawing.Size(156, 22);
            this.optHighPriority.TabIndex = 1;
            this.optHighPriority.Text = "Send Immediately";
            this.optHighPriority.UseVisualStyleBackColor = false;
            this.optHighPriority.CheckedChanged += new System.EventHandler(this.optHighPriority_CheckedChanged);
            // 
            // optNormalPriority
            // 
            this.optNormalPriority.BackColor = System.Drawing.Color.Transparent;
            this.optNormalPriority.Checked = true;
            this.optNormalPriority.Dock = System.Windows.Forms.DockStyle.Left;
            this.optNormalPriority.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNormalPriority.Location = new System.Drawing.Point(112, 1);
            this.optNormalPriority.Name = "optNormalPriority";
            this.optNormalPriority.Size = new System.Drawing.Size(131, 22);
            this.optNormalPriority.TabIndex = 0;
            this.optNormalPriority.TabStop = true;
            this.optNormalPriority.Text = "Normal Priority";
            this.optNormalPriority.UseVisualStyleBackColor = false;
            this.optNormalPriority.CheckedChanged += new System.EventHandler(this.optNormalPriority_CheckedChanged);
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTemplate.ForeColor = System.Drawing.Color.Black;
            this.cmbTemplate.Location = new System.Drawing.Point(539, 1);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(166, 22);
            this.cmbTemplate.TabIndex = 2;
            this.cmbTemplate.SelectionChangeCommitted += new System.EventHandler(this.cmbTemplate_SelectionChangeCommitted);
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Dock = System.Windows.Forms.DockStyle.Left;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(1, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(111, 22);
            this.label28.TabIndex = 8;
            this.label28.Text = "   FAX Priority :";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label42.Location = new System.Drawing.Point(1, 23);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(704, 1);
            this.label42.TabIndex = 14;
            this.label42.Text = "label2";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(0, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 23);
            this.label43.TabIndex = 13;
            this.label43.Text = "label4";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label44.Location = new System.Drawing.Point(705, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 23);
            this.label44.TabIndex = 12;
            this.label44.Text = "label3";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(0, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(706, 1);
            this.label45.TabIndex = 11;
            this.label45.Text = "label1";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(208, 225);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel5.Size = new System.Drawing.Size(709, 28);
            this.panel5.TabIndex = 211;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.btnUp);
            this.panel8.Controls.Add(this.btnDown);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.label5);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.label26);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(706, 25);
            this.panel8.TabIndex = 19;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Location = new System.Drawing.Point(657, 1);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(24, 23);
            this.btnUp.TabIndex = 9;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            this.btnUp.MouseLeave += new System.EventHandler(this.btnUp_MouseLeave);
            this.btnUp.MouseHover += new System.EventHandler(this.btnUp_MouseHover);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Location = new System.Drawing.Point(681, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(24, 23);
            this.btnDown.TabIndex = 10;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseLeave += new System.EventHandler(this.btnDown_MouseLeave);
            this.btnDown.MouseHover += new System.EventHandler(this.btnDown_MouseHover);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(1, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(704, 1);
            this.label4.TabIndex = 8;
            this.label4.Text = "label2";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "label4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label6.Location = new System.Drawing.Point(705, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 24);
            this.label6.TabIndex = 6;
            this.label6.Text = "label3";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(706, 1);
            this.label8.TabIndex = 5;
            this.label8.Text = "label1";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(706, 25);
            this.label26.TabIndex = 1;
            this.label26.Text = "    FAX Cover Page Preview";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlc1FaxListHeader
            // 
            this.pnlc1FaxListHeader.Controls.Add(this.pnlC1FaxList);
            this.pnlc1FaxListHeader.Controls.Add(this.panel4);
            this.pnlc1FaxListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlc1FaxListHeader.Location = new System.Drawing.Point(208, 30);
            this.pnlc1FaxListHeader.Name = "pnlc1FaxListHeader";
            this.pnlc1FaxListHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlc1FaxListHeader.Size = new System.Drawing.Size(709, 195);
            this.pnlc1FaxListHeader.TabIndex = 214;
            // 
            // pnlC1FaxList
            // 
            this.pnlC1FaxList.BackColor = System.Drawing.Color.Transparent;
            this.pnlC1FaxList.Controls.Add(this.c1FaxList);
            this.pnlC1FaxList.Controls.Add(this.label24);
            this.pnlC1FaxList.Controls.Add(this.label23);
            this.pnlC1FaxList.Controls.Add(this.label22);
            this.pnlC1FaxList.Controls.Add(this.label21);
            this.pnlC1FaxList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlC1FaxList.Location = new System.Drawing.Point(0, 27);
            this.pnlC1FaxList.Margin = new System.Windows.Forms.Padding(2);
            this.pnlC1FaxList.Name = "pnlC1FaxList";
            this.pnlC1FaxList.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlC1FaxList.Size = new System.Drawing.Size(709, 165);
            this.pnlC1FaxList.TabIndex = 67;
            // 
            // c1FaxList
            // 
            this.c1FaxList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1FaxList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1FaxList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1FaxList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1FaxList.ColumnInfo = resources.GetString("c1FaxList.ColumnInfo");
            this.c1FaxList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FaxList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1FaxList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1FaxList.Location = new System.Drawing.Point(1, 1);
            this.c1FaxList.Margin = new System.Windows.Forms.Padding(2);
            this.c1FaxList.Name = "c1FaxList";
            this.c1FaxList.Rows.Count = 10;
            this.c1FaxList.Rows.DefaultSize = 19;
            this.c1FaxList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FaxList.ShowCellLabels = true;
            this.c1FaxList.Size = new System.Drawing.Size(704, 163);
            this.c1FaxList.StyleInfo = resources.GetString("c1FaxList.StyleInfo");
            this.c1FaxList.TabIndex = 58;
            this.c1FaxList.Tree.Column = 2;
            this.c1FaxList.Tree.Indent = 72;
            this.c1FaxList.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.c1FaxList.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1FaxList.Tree.NodeImageCollapsed")));
            this.c1FaxList.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1FaxList.Tree.NodeImageExpanded")));
            this.c1FaxList.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1FaxList.EnterCell += new System.EventHandler(this.c1FaxList_EnterCell);
            this.c1FaxList.DoubleClick += new System.EventHandler(this.c1FaxList_DoubleClick);
            this.c1FaxList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1FaxList_MouseDown);
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Location = new System.Drawing.Point(705, 1);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 163);
            this.label24.TabIndex = 43;
            this.label24.Text = "label24";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Location = new System.Drawing.Point(0, 1);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 163);
            this.label23.TabIndex = 42;
            this.label23.Text = "label23";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.Location = new System.Drawing.Point(0, 164);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(706, 1);
            this.label22.TabIndex = 41;
            this.label22.Text = "label22";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(706, 1);
            this.label21.TabIndex = 40;
            this.label21.Text = "label21";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.pnlSearchDocument);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel4.Size = new System.Drawing.Size(709, 27);
            this.panel4.TabIndex = 69;
            // 
            // pnlSearchDocument
            // 
            this.pnlSearchDocument.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearchDocument.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.pnlSearchDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchDocument.Controls.Add(this.pnlSearch);
            this.pnlSearchDocument.Controls.Add(this.label3);
            this.pnlSearchDocument.Controls.Add(this.label12);
            this.pnlSearchDocument.Controls.Add(this.lblSearchOn);
            this.pnlSearchDocument.Controls.Add(this.label9);
            this.pnlSearchDocument.Controls.Add(this.label10);
            this.pnlSearchDocument.Controls.Add(this.label11);
            this.pnlSearchDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchDocument.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchDocument.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSearchDocument.Name = "pnlSearchDocument";
            this.pnlSearchDocument.Size = new System.Drawing.Size(706, 24);
            this.pnlSearchDocument.TabIndex = 60;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtSearchDocument);
            this.pnlSearch.Controls.Add(this.label50);
            this.pnlSearch.Controls.Add(this.Label77);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSearch.Location = new System.Drawing.Point(63, 1);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(241, 22);
            this.pnlSearch.TabIndex = 44;
            // 
            // txtSearchDocument
            // 
            this.txtSearchDocument.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchDocument.ForeColor = System.Drawing.Color.Black;
            this.txtSearchDocument.Location = new System.Drawing.Point(5, 3);
            this.txtSearchDocument.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchDocument.Name = "txtSearchDocument";
            this.txtSearchDocument.Size = new System.Drawing.Size(214, 15);
            this.txtSearchDocument.TabIndex = 0;
            this.txtSearchDocument.TextChanged += new System.EventHandler(this.txtSearchDocument_TextChanged);
            this.txtSearchDocument.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchDocument_KeyPress);
            this.txtSearchDocument.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchDocument_KeyUp);
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.White;
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Location = new System.Drawing.Point(1, 3);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(4, 14);
            this.label50.TabIndex = 44;
            // 
            // Label77
            // 
            this.Label77.BackColor = System.Drawing.Color.White;
            this.Label77.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label77.Location = new System.Drawing.Point(1, 17);
            this.Label77.Name = "Label77";
            this.Label77.Size = new System.Drawing.Size(218, 5);
            this.Label77.TabIndex = 43;
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(1, 0);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(218, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(1, 1);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(4, 21);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(219, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(21, 22);
            this.btnClear.TabIndex = 41;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(240, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.label3.Size = new System.Drawing.Size(1, 22);
            this.label3.TabIndex = 0;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(705, 1);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 22);
            this.label12.TabIndex = 31;
            this.label12.Text = "label12";
            // 
            // lblSearchOn
            // 
            this.lblSearchOn.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchOn.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearchOn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSearchOn.Location = new System.Drawing.Point(1, 1);
            this.lblSearchOn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearchOn.Name = "lblSearchOn";
            this.lblSearchOn.Size = new System.Drawing.Size(61, 22);
            this.lblSearchOn.TabIndex = 19;
            this.lblSearchOn.Text = "Search :";
            this.lblSearchOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(1, 23);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(705, 1);
            this.label9.TabIndex = 28;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(1, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(705, 1);
            this.label10.TabIndex = 29;
            this.label10.Text = "label10";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 24);
            this.label11.TabIndex = 30;
            this.label11.Text = "label11";
            // 
            // pnlFaxProgressBar
            // 
            this.pnlFaxProgressBar.Controls.Add(this.panel2);
            this.pnlFaxProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFaxProgressBar.Location = new System.Drawing.Point(208, 569);
            this.pnlFaxProgressBar.Name = "pnlFaxProgressBar";
            this.pnlFaxProgressBar.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlFaxProgressBar.Size = new System.Drawing.Size(709, 25);
            this.pnlFaxProgressBar.TabIndex = 70;
            this.pnlFaxProgressBar.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label34);
            this.panel2.Controls.Add(this.label33);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.pbDocument);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(706, 22);
            this.panel2.TabIndex = 51;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Right;
            this.label34.Location = new System.Drawing.Point(705, 1);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 20);
            this.label34.TabIndex = 47;
            this.label34.Text = "label34";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Left;
            this.label33.Location = new System.Drawing.Point(0, 1);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1, 20);
            this.label33.TabIndex = 46;
            this.label33.Text = "label33";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label32.Location = new System.Drawing.Point(0, 21);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(706, 1);
            this.label32.TabIndex = 41;
            this.label32.Text = "label32";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Top;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(706, 1);
            this.label25.TabIndex = 40;
            this.label25.Text = "label25";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlFaxNoandDetail);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(208, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel3.Size = new System.Drawing.Size(709, 30);
            this.panel3.TabIndex = 68;
            // 
            // pnlFaxNoandDetail
            // 
            this.pnlFaxNoandDetail.BackColor = System.Drawing.Color.Transparent;
            this.pnlFaxNoandDetail.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Blue2007;
            this.pnlFaxNoandDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFaxNoandDetail.Controls.Add(this.lblFaxDetails);
            this.pnlFaxNoandDetail.Controls.Add(this.label18);
            this.pnlFaxNoandDetail.Controls.Add(this.mskFaxNo);
            this.pnlFaxNoandDetail.Controls.Add(this.label1);
            this.pnlFaxNoandDetail.Controls.Add(this.cmbCoverPage);
            this.pnlFaxNoandDetail.Controls.Add(this.btnDown1);
            this.pnlFaxNoandDetail.Controls.Add(this.btnUp1);
            this.pnlFaxNoandDetail.Controls.Add(this.label20);
            this.pnlFaxNoandDetail.Controls.Add(this.label14);
            this.pnlFaxNoandDetail.Controls.Add(this.label15);
            this.pnlFaxNoandDetail.Controls.Add(this.label13);
            this.pnlFaxNoandDetail.Controls.Add(this.label16);
            this.pnlFaxNoandDetail.Controls.Add(this.label19);
            this.pnlFaxNoandDetail.Controls.Add(this.label17);
            this.pnlFaxNoandDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFaxNoandDetail.Location = new System.Drawing.Point(0, 3);
            this.pnlFaxNoandDetail.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFaxNoandDetail.Name = "pnlFaxNoandDetail";
            this.pnlFaxNoandDetail.Size = new System.Drawing.Size(706, 24);
            this.pnlFaxNoandDetail.TabIndex = 66;
            // 
            // lblFaxDetails
            // 
            this.lblFaxDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblFaxDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFaxDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaxDetails.ForeColor = System.Drawing.Color.White;
            this.lblFaxDetails.Location = new System.Drawing.Point(121, 1);
            this.lblFaxDetails.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFaxDetails.Name = "lblFaxDetails";
            this.lblFaxDetails.Size = new System.Drawing.Size(122, 22);
            this.lblFaxDetails.TabIndex = 36;
            this.lblFaxDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(243, 1);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 22);
            this.label18.TabIndex = 32;
            this.label18.Text = "Fax No. : ";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskFaxNo
            // 
            this.mskFaxNo.AllowValidate = true;
            this.mskFaxNo.Dock = System.Windows.Forms.DockStyle.Right;
            this.mskFaxNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskFaxNo.IncludeLiteralsAndPrompts = false;
            this.mskFaxNo.Location = new System.Drawing.Point(314, 1);
            this.mskFaxNo.MaskType = gloMaskControl.gloMaskType.Fax;
            this.mskFaxNo.Name = "mskFaxNo";
            this.mskFaxNo.ReadOnly = false;
            this.mskFaxNo.Size = new System.Drawing.Size(100, 22);
            this.mskFaxNo.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(414, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 22);
            this.label1.TabIndex = 42;
            this.label1.Text = "Fax Cover Page : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // cmbCoverPage
            // 
            this.cmbCoverPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbCoverPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCoverPage.Location = new System.Drawing.Point(530, 1);
            this.cmbCoverPage.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCoverPage.Name = "cmbCoverPage";
            this.cmbCoverPage.Size = new System.Drawing.Size(127, 22);
            this.cmbCoverPage.TabIndex = 50;
            this.cmbCoverPage.Visible = false;
            this.cmbCoverPage.SelectionChangeCommitted += new System.EventHandler(this.cmbCoverPage_SelectionChangeCommitted);
            // 
            // btnDown1
            // 
            this.btnDown1.BackColor = System.Drawing.Color.Transparent;
            this.btnDown1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDown1.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown1.FlatAppearance.BorderSize = 0;
            this.btnDown1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown1.Location = new System.Drawing.Point(657, 1);
            this.btnDown1.Name = "btnDown1";
            this.btnDown1.Size = new System.Drawing.Size(24, 22);
            this.btnDown1.TabIndex = 44;
            this.btnDown1.UseVisualStyleBackColor = false;
            this.btnDown1.Click += new System.EventHandler(this.btnDown1_Click_1);
            this.btnDown1.MouseLeave += new System.EventHandler(this.btnDown1_MouseLeave);
            this.btnDown1.MouseHover += new System.EventHandler(this.btnDown1_MouseHover);
            // 
            // btnUp1
            // 
            this.btnUp1.BackColor = System.Drawing.Color.Transparent;
            this.btnUp1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnUp1.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUp1.FlatAppearance.BorderSize = 0;
            this.btnUp1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUp1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUp1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp1.Location = new System.Drawing.Point(681, 1);
            this.btnUp1.Name = "btnUp1";
            this.btnUp1.Size = new System.Drawing.Size(24, 22);
            this.btnUp1.TabIndex = 43;
            this.btnUp1.UseVisualStyleBackColor = false;
            this.btnUp1.Click += new System.EventHandler(this.btnUp1_Click_1);
            this.btnUp1.MouseLeave += new System.EventHandler(this.btnUp1_MouseLeave);
            this.btnUp1.MouseHover += new System.EventHandler(this.btnUp1_MouseHover);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(705, 1);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 22);
            this.label20.TabIndex = 41;
            this.label20.Text = "label20";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(6, 1);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 22);
            this.label14.TabIndex = 35;
            this.label14.Text = "Contact Person : ";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(3, 1);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(3, 22);
            this.label15.TabIndex = 37;
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(1, 1);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(2, 22);
            this.label13.TabIndex = 35;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Location = new System.Drawing.Point(1, 23);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(705, 1);
            this.label16.TabIndex = 38;
            this.label16.Text = "label16";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(0, 1);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 23);
            this.label19.TabIndex = 40;
            this.label19.Text = "label19";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(706, 1);
            this.label17.TabIndex = 39;
            this.label17.Text = "label17";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter1.Location = new System.Drawing.Point(205, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 594);
            this.splitter1.TabIndex = 71;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.wdFaxCoverpage);
            this.panel1.Location = new System.Drawing.Point(211, 349);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 80);
            this.panel1.TabIndex = 72;
            this.panel1.Visible = false;
            // 
            // wdFaxCoverpage
            // 
            this.wdFaxCoverpage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdFaxCoverpage.Enabled = true;
            this.wdFaxCoverpage.Location = new System.Drawing.Point(0, 0);
            this.wdFaxCoverpage.Name = "wdFaxCoverpage";
            this.wdFaxCoverpage.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdFaxCoverpage.OcxState")));
            this.wdFaxCoverpage.Size = new System.Drawing.Size(604, 80);
            this.wdFaxCoverpage.TabIndex = 60;
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(209, 372);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(2);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(49, 29);
            this.txtNotes.TabIndex = 26;
            this.txtNotes.Visible = false;
            // 
            // pnlFaxTo
            // 
            this.pnlFaxTo.BackColor = System.Drawing.Color.Transparent;
            this.pnlFaxTo.Controls.Add(this.panel6);
            this.pnlFaxTo.Controls.Add(this.splitter2);
            this.pnlFaxTo.Controls.Add(this.panel7);
            this.pnlFaxTo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFaxTo.Location = new System.Drawing.Point(0, 0);
            this.pnlFaxTo.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFaxTo.Name = "pnlFaxTo";
            this.pnlFaxTo.Size = new System.Drawing.Size(205, 594);
            this.pnlFaxTo.TabIndex = 65;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Controls.Add(this.trvFaxTo);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label29);
            this.panel6.Controls.Add(this.label30);
            this.panel6.Controls.Add(this.label31);
            this.panel6.Controls.Add(this.label35);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 225);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel6.Size = new System.Drawing.Size(205, 369);
            this.panel6.TabIndex = 73;
            // 
            // trvFaxTo
            // 
            this.trvFaxTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvFaxTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvFaxTo.ForeColor = System.Drawing.Color.Black;
            this.trvFaxTo.ImageIndex = 0;
            this.trvFaxTo.ImageList = this.imgFax;
            this.trvFaxTo.Indent = 20;
            this.trvFaxTo.ItemHeight = 20;
            this.trvFaxTo.Location = new System.Drawing.Point(8, 5);
            this.trvFaxTo.Margin = new System.Windows.Forms.Padding(2);
            this.trvFaxTo.Name = "trvFaxTo";
            this.trvFaxTo.SelectedImageIndex = 0;
            this.trvFaxTo.ShowLines = false;
            this.trvFaxTo.Size = new System.Drawing.Size(196, 360);
            this.trvFaxTo.TabIndex = 0;
            this.trvFaxTo.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvFaxTo_NodeMouseClick);
            this.trvFaxTo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvFaxTo_MouseDown);
            // 
            // imgFax
            // 
            this.imgFax.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgFax.ImageStream")));
            this.imgFax.TransparentColor = System.Drawing.Color.Transparent;
            this.imgFax.Images.SetKeyName(0, "Contact.ico");
            this.imgFax.Images.SetKeyName(1, "Hospital.ico");
            this.imgFax.Images.SetKeyName(2, "Insurance01.ico");
            this.imgFax.Images.SetKeyName(3, "Pharmacy.ico");
            this.imgFax.Images.SetKeyName(4, "Img_Status_ReSend01.ico");
            this.imgFax.Images.SetKeyName(5, "Physician01.ico");
            this.imgFax.Images.SetKeyName(6, "epharmacy.ico");
            this.imgFax.Images.SetKeyName(7, "Bullet06.ico");
            this.imgFax.Images.SetKeyName(8, "Contact.ico");
            this.imgFax.Images.SetKeyName(9, "Other.ico");
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(4, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(4, 360);
            this.label2.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(4, 1);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 4);
            this.label7.TabIndex = 41;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.label29.Location = new System.Drawing.Point(4, 0);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(200, 1);
            this.label29.TabIndex = 43;
            this.label29.Text = "label29";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Location = new System.Drawing.Point(3, 0);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 365);
            this.label30.TabIndex = 45;
            this.label30.Text = "label30";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Right;
            this.label31.Location = new System.Drawing.Point(204, 0);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 365);
            this.label31.TabIndex = 46;
            this.label31.Text = "label31";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label35.Location = new System.Drawing.Point(3, 365);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(202, 1);
            this.label35.TabIndex = 44;
            this.label35.Text = "label35";
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 222);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(205, 3);
            this.splitter2.TabIndex = 75;
            this.splitter2.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.trvContactType);
            this.panel7.Controls.Add(this.label36);
            this.panel7.Controls.Add(this.label37);
            this.panel7.Controls.Add(this.label38);
            this.panel7.Controls.Add(this.label39);
            this.panel7.Controls.Add(this.label40);
            this.panel7.Controls.Add(this.label41);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.panel7.Size = new System.Drawing.Size(205, 222);
            this.panel7.TabIndex = 74;
            // 
            // trvContactType
            // 
            this.trvContactType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvContactType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvContactType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvContactType.ForeColor = System.Drawing.Color.Black;
            this.trvContactType.HideSelection = false;
            this.trvContactType.ImageIndex = 0;
            this.trvContactType.ImageList = this.imgFax;
            this.trvContactType.ItemHeight = 20;
            this.trvContactType.Location = new System.Drawing.Point(8, 8);
            this.trvContactType.Name = "trvContactType";
            this.trvContactType.SelectedImageIndex = 0;
            this.trvContactType.Size = new System.Drawing.Size(196, 213);
            this.trvContactType.TabIndex = 47;
            this.trvContactType.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvContactType_NodeMouseClick);
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.White;
            this.label36.Dock = System.Windows.Forms.DockStyle.Left;
            this.label36.Location = new System.Drawing.Point(4, 8);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(4, 213);
            this.label36.TabIndex = 42;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.White;
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Location = new System.Drawing.Point(4, 4);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(200, 4);
            this.label37.TabIndex = 41;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.Location = new System.Drawing.Point(4, 3);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(200, 1);
            this.label38.TabIndex = 43;
            this.label38.Text = "label38";
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Left;
            this.label39.Location = new System.Drawing.Point(3, 3);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(1, 218);
            this.label39.TabIndex = 45;
            this.label39.Text = "label39";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Right;
            this.label40.Location = new System.Drawing.Point(204, 3);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 218);
            this.label40.TabIndex = 46;
            this.label40.Text = "label40";
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(3, 221);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(202, 1);
            this.label41.TabIndex = 44;
            this.label41.Text = "label41";
            // 
            // txtFAXNo
            // 
            this.txtFAXNo.ForeColor = System.Drawing.Color.Black;
            this.txtFAXNo.Location = new System.Drawing.Point(520, 11);
            this.txtFAXNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtFAXNo.Multiline = true;
            this.txtFAXNo.Name = "txtFAXNo";
            this.txtFAXNo.ShortcutsEnabled = false;
            this.txtFAXNo.Size = new System.Drawing.Size(128, 22);
            this.txtFAXNo.TabIndex = 33;
            this.txtFAXNo.Visible = false;
            this.txtFAXNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFAXNo_KeyPress);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AutoEllipsis = true;
            this.btnUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(300, 11);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 25);
            this.btnUpdate.TabIndex = 34;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cmnuAddFaxTo
            // 
            this.cmnuAddFaxTo.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFaxTo});
            // 
            // mnuFaxTo
            // 
            this.mnuFaxTo.Index = 0;
            this.mnuFaxTo.Text = "Add To Selected Contacts";
            this.mnuFaxTo.Click += new System.EventHandler(this.mnuFaxTo_Click);
            // 
            // cmnuDeleteFaxTo
            // 
            this.cmnuDeleteFaxTo.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem1});
            // 
            // MenuItem1
            // 
            this.MenuItem1.Index = 0;
            this.MenuItem1.Text = "Delete Selected Contact";
            this.MenuItem1.Click += new System.EventHandler(this.MenuItem1_Click);
            // 
            // frmEDocEvent_Fax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(917, 647);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtFAXNo);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tls_Fax);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmEDocEvent_Fax";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fax";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEDocEvent_Fax_Load);
            this.tls_Fax.ResumeLayout(false);
            this.tls_Fax.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnldsoFAXPreview.ResumeLayout(false);
            this.pnlCoverPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsoFAXPreview)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.pnlc1FaxListHeader.ResumeLayout(false);
            this.pnlC1FaxList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FaxList)).EndInit();
            this.panel4.ResumeLayout(false);
            this.pnlSearchDocument.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlFaxProgressBar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlFaxNoandDetail.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wdFaxCoverpage)).EndInit();
            this.pnlFaxTo.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion

            private gloGlobal.gloToolStripIgnoreFocus tls_Fax;
            private System.Windows.Forms.ToolStripButton tlb_Ok;
            private System.Windows.Forms.ToolStripButton tlb_Cancel;
            private System.Windows.Forms.ProgressBar pbDocument;
            private System.Windows.Forms.Panel pnlMain;
            private System.Windows.Forms.TextBox txtNotes;
            internal System.Windows.Forms.Panel panel2;
            internal C1.Win.C1FlexGrid.C1FlexGrid c1FaxList;
            internal System.Windows.Forms.Panel pnlFaxTo;
            internal System.Windows.Forms.TreeView trvFaxTo;
            internal System.Windows.Forms.Panel pnlSearchDocument;
            internal System.Windows.Forms.TextBox txtSearchDocument;
            internal System.Windows.Forms.Label lblSearchOn;
            internal System.Windows.Forms.Panel pnlFaxNoandDetail;
            private System.Windows.Forms.Label label12;
            private System.Windows.Forms.Label label9;
            private System.Windows.Forms.Label label10;
            private System.Windows.Forms.Label label11;
            internal System.Windows.Forms.Label lblFaxDetails;
            internal System.Windows.Forms.Label label14;
            internal System.Windows.Forms.Button btnUpdate;
            internal System.Windows.Forms.TextBox txtFAXNo;
            internal System.Windows.Forms.Label label18;
            private System.Windows.Forms.Label label20;
            internal System.Windows.Forms.Label label15;
            internal System.Windows.Forms.Label label13;
            private System.Windows.Forms.Label label16;
            private System.Windows.Forms.Label label17;
            private System.Windows.Forms.Label label19;
            private System.Windows.Forms.Panel pnlC1FaxList;
            private System.Windows.Forms.Label label24;
            private System.Windows.Forms.Label label23;
            private System.Windows.Forms.Label label22;
            private System.Windows.Forms.Label label21;
            private System.Windows.Forms.ToolStripButton tlb_Update;
            private System.Windows.Forms.Label label25;
            internal System.Windows.Forms.ContextMenu cmnuAddFaxTo;
            internal System.Windows.Forms.MenuItem mnuFaxTo;
            internal System.Windows.Forms.ContextMenu cmnuDeleteFaxTo;
            internal System.Windows.Forms.MenuItem MenuItem1;
            private System.Windows.Forms.Panel panel4;
            private System.Windows.Forms.Panel panel3;
            private System.Windows.Forms.Label label32;
            private System.Windows.Forms.Panel pnlFaxProgressBar;
            private System.Windows.Forms.Label label34;
            private System.Windows.Forms.Label label33;
            internal System.Windows.Forms.Label label3;
            private System.Windows.Forms.Splitter splitter1;
            private System.Windows.Forms.ImageList imgFax;
            internal System.Windows.Forms.ComboBox cmbCoverPage;
            internal System.Windows.Forms.Label label1;
            private System.Windows.Forms.Panel panel1;
            private AxDSOFramer.AxFramerControl wdFaxCoverpage;
            internal System.Windows.Forms.TreeView trvContactType;
            private System.Windows.Forms.ToolStripButton tlb_Refresh;
            internal System.Windows.Forms.Panel panel6;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label7;
            private System.Windows.Forms.Label label29;
            private System.Windows.Forms.Label label30;
            private System.Windows.Forms.Label label31;
            private System.Windows.Forms.Label label35;
            private System.Windows.Forms.Splitter splitter2;
            internal System.Windows.Forms.Panel panel7;
            private System.Windows.Forms.Label label36;
            private System.Windows.Forms.Label label37;
            private System.Windows.Forms.Label label38;
            private System.Windows.Forms.Label label39;
            private System.Windows.Forms.Label label40;
            private System.Windows.Forms.Label label41;
            internal System.Windows.Forms.Panel panel5;
            internal System.Windows.Forms.Panel panel8;
            internal System.Windows.Forms.Button btnUp;
            internal System.Windows.Forms.Button btnDown;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.Label label6;
            private System.Windows.Forms.Label label8;
            internal System.Windows.Forms.Label label26;
            internal System.Windows.Forms.Panel panel9;
            internal System.Windows.Forms.Panel panel10;
            internal System.Windows.Forms.Label label27;
            internal System.Windows.Forms.RadioButton optHighPriority;
            internal System.Windows.Forms.RadioButton optNormalPriority;
            internal System.Windows.Forms.ComboBox cmbTemplate;
            internal System.Windows.Forms.Label label28;
            private System.Windows.Forms.Label label42;
            private System.Windows.Forms.Label label43;
            private System.Windows.Forms.Label label44;
            private System.Windows.Forms.Label label45;
            internal System.Windows.Forms.Panel pnlCoverPage;
            private System.Windows.Forms.Label label46;
            private System.Windows.Forms.Label label47;
            private System.Windows.Forms.Label label48;
            private System.Windows.Forms.Label label49;
            internal AxDSOFramer.AxFramerControl dsoFAXPreview;
            private System.Windows.Forms.Panel pnlc1FaxListHeader;
            private System.Windows.Forms.Panel pnldsoFAXPreview;
            internal System.Windows.Forms.Button btnDown1;
            internal System.Windows.Forms.Button btnUp1;
            internal gloMaskControl.gloMaskBox mskFaxNo;
            internal System.Windows.Forms.Panel pnlSearch;
            internal System.Windows.Forms.Label Label77;
            internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
            internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
            internal System.Windows.Forms.Button btnClear;
            private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
            private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
            internal System.Windows.Forms.Label label50;
        }
    }
