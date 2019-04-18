namespace gloCommunity.UserControls
{
    partial class UCTemplates
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTemplates));
            this.pnlMainMain = new System.Windows.Forms.Panel();
            this.pnlSimmary = new System.Windows.Forms.Panel();
            this.wdopenfl = new AxDSOFramer.AxFramerControl();
            this.lbl_pnlSummaryBB = new System.Windows.Forms.Label();
            this.lbl_pnlSummaryLB = new System.Windows.Forms.Label();
            this.lbl_pnlSummaryRB = new System.Windows.Forms.Label();
            this.lbl_pnlSummaryTB = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.pnlTemplates = new System.Windows.Forms.Panel();
            this.lnl_pnlTemplatesBB = new System.Windows.Forms.Label();
            this.lnl_pnlTemplatesLB = new System.Windows.Forms.Label();
            this.lnl_pnlTemplatesRB = new System.Windows.Forms.Label();
            this.lnl_pnlTemplatesTB = new System.Windows.Forms.Label();
            this.flxTemplates = new C1.Win.C1FlexGrid.C1FlexGrid();
     //       this.wdTemplate = new AxDSOFramer.AxFramerControl();
            this.pnlProviderTop = new System.Windows.Forms.Panel();
            this.pnlProvider = new System.Windows.Forms.Panel();
            this.chkdncon = new System.Windows.Forms.CheckBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.lbl_pnlProviderName = new System.Windows.Forms.Label();
            this.lbl_pnlProviderBB = new System.Windows.Forms.Label();
            this.lbl_pnlProviderLB = new System.Windows.Forms.Label();
            this.lbl_pnlProviderRB = new System.Windows.Forms.Label();
            this.lbl_pnlProviderTB = new System.Windows.Forms.Label();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.pnlCategory = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.trvCategories = new System.Windows.Forms.TreeView();
            this.imgTempalte = new System.Windows.Forms.ImageList(this.components);
            this.Label9 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.pnlProcess = new System.Windows.Forms.Panel();
            this.Label61 = new System.Windows.Forms.Label();
            this.Label62 = new System.Windows.Forms.Label();
            this.Label65 = new System.Windows.Forms.Label();
            this.Label66 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblProcess = new System.Windows.Forms.Label();
            this.pnlMainMain.SuspendLayout();
            this.pnlSimmary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdopenfl)).BeginInit();
            this.pnlTemplates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxTemplates)).BeginInit();
         //   ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).BeginInit();
            this.pnlProviderTop.SuspendLayout();
            this.pnlProvider.SuspendLayout();
            this.pnlCategory.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.pnlProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMainMain
            // 
            this.pnlMainMain.Controls.Add(this.pnlSimmary);
            this.pnlMainMain.Controls.Add(this.splitter2);
            this.pnlMainMain.Controls.Add(this.pnlTemplates);
            this.pnlMainMain.Controls.Add(this.pnlProviderTop);
            this.pnlMainMain.Controls.Add(this.Splitter1);
            this.pnlMainMain.Controls.Add(this.pnlCategory);
            this.pnlMainMain.Controls.Add(this.pnltls);
            this.pnlMainMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMainMain.Name = "pnlMainMain";
            this.pnlMainMain.Size = new System.Drawing.Size(959, 738);
            this.pnlMainMain.TabIndex = 3;
            // 
            // pnlSimmary
            // 
            this.pnlSimmary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSimmary.Controls.Add(this.wdopenfl);
            this.pnlSimmary.Controls.Add(this.lbl_pnlSummaryBB);
            this.pnlSimmary.Controls.Add(this.lbl_pnlSummaryLB);
            this.pnlSimmary.Controls.Add(this.lbl_pnlSummaryRB);
            this.pnlSimmary.Controls.Add(this.lbl_pnlSummaryTB);
            this.pnlSimmary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSimmary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSimmary.Location = new System.Drawing.Point(292, 406);
            this.pnlSimmary.Name = "pnlSimmary";
            this.pnlSimmary.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlSimmary.Size = new System.Drawing.Size(667, 332);
            this.pnlSimmary.TabIndex = 1;
            // 
            // wdopenfl
            // 
            this.wdopenfl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdopenfl.Enabled = true;
            this.wdopenfl.Location = new System.Drawing.Point(1, 1);
            this.wdopenfl.Name = "wdopenfl";
            this.wdopenfl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdopenfl.OcxState")));
            this.wdopenfl.Size = new System.Drawing.Size(662, 327);
            this.wdopenfl.TabIndex = 11;
            this.wdopenfl.Visible = false;
            this.wdopenfl.OnFileCommand += new AxDSOFramer._DFramerCtlEvents_OnFileCommandEventHandler(this.wdopenfl_OnFileCommand);
            this.wdopenfl.OnDocumentOpened += new AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEventHandler(this.wdopenfl_OnDocumentOpened);
            this.wdopenfl.OnDocumentClosed += new System.EventHandler(this.wdopenfl_OnDocumentClosed);
            this.wdopenfl.BeforeDocumentClosed += new AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEventHandler(this.wdopenfl_BeforeDocumentClosed);
            // 
            // lbl_pnlSummaryBB
            // 
            this.lbl_pnlSummaryBB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSummaryBB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSummaryBB.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlSummaryBB.Location = new System.Drawing.Point(1, 328);
            this.lbl_pnlSummaryBB.Name = "lbl_pnlSummaryBB";
            this.lbl_pnlSummaryBB.Size = new System.Drawing.Size(662, 1);
            this.lbl_pnlSummaryBB.TabIndex = 8;
            this.lbl_pnlSummaryBB.Text = "label2";
            // 
            // lbl_pnlSummaryLB
            // 
            this.lbl_pnlSummaryLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSummaryLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSummaryLB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlSummaryLB.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlSummaryLB.Name = "lbl_pnlSummaryLB";
            this.lbl_pnlSummaryLB.Size = new System.Drawing.Size(1, 328);
            this.lbl_pnlSummaryLB.TabIndex = 7;
            this.lbl_pnlSummaryLB.Text = "label4";
            // 
            // lbl_pnlSummaryRB
            // 
            this.lbl_pnlSummaryRB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSummaryRB.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSummaryRB.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlSummaryRB.Location = new System.Drawing.Point(663, 1);
            this.lbl_pnlSummaryRB.Name = "lbl_pnlSummaryRB";
            this.lbl_pnlSummaryRB.Size = new System.Drawing.Size(1, 328);
            this.lbl_pnlSummaryRB.TabIndex = 6;
            this.lbl_pnlSummaryRB.Text = "label3";
            // 
            // lbl_pnlSummaryTB
            // 
            this.lbl_pnlSummaryTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSummaryTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSummaryTB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlSummaryTB.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSummaryTB.Name = "lbl_pnlSummaryTB";
            this.lbl_pnlSummaryTB.Size = new System.Drawing.Size(664, 1);
            this.lbl_pnlSummaryTB.TabIndex = 5;
            this.lbl_pnlSummaryTB.Text = "label1";
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(292, 403);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(667, 3);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // pnlTemplates
            // 
            this.pnlTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlTemplates.Controls.Add(this.lnl_pnlTemplatesBB);
            this.pnlTemplates.Controls.Add(this.lnl_pnlTemplatesLB);
            this.pnlTemplates.Controls.Add(this.lnl_pnlTemplatesRB);
            this.pnlTemplates.Controls.Add(this.lnl_pnlTemplatesTB);
            this.pnlTemplates.Controls.Add(this.flxTemplates);
         //   this.pnlTemplates.Controls.Add(this.wdTemplate);
            this.pnlTemplates.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplates.Location = new System.Drawing.Point(292, 27);
            this.pnlTemplates.Name = "pnlTemplates";
            this.pnlTemplates.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlTemplates.Size = new System.Drawing.Size(667, 376);
            this.pnlTemplates.TabIndex = 2;
            // 
            // lnl_pnlTemplatesBB
            // 
            this.lnl_pnlTemplatesBB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lnl_pnlTemplatesBB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lnl_pnlTemplatesBB.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lnl_pnlTemplatesBB.Location = new System.Drawing.Point(1, 375);
            this.lnl_pnlTemplatesBB.Name = "lnl_pnlTemplatesBB";
            this.lnl_pnlTemplatesBB.Size = new System.Drawing.Size(662, 1);
            this.lnl_pnlTemplatesBB.TabIndex = 8;
            this.lnl_pnlTemplatesBB.Text = "label2";
            // 
            // lnl_pnlTemplatesLB
            // 
            this.lnl_pnlTemplatesLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lnl_pnlTemplatesLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.lnl_pnlTemplatesLB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnl_pnlTemplatesLB.Location = new System.Drawing.Point(0, 1);
            this.lnl_pnlTemplatesLB.Name = "lnl_pnlTemplatesLB";
            this.lnl_pnlTemplatesLB.Size = new System.Drawing.Size(1, 375);
            this.lnl_pnlTemplatesLB.TabIndex = 7;
            this.lnl_pnlTemplatesLB.Text = "label4";
            // 
            // lnl_pnlTemplatesRB
            // 
            this.lnl_pnlTemplatesRB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lnl_pnlTemplatesRB.Dock = System.Windows.Forms.DockStyle.Right;
            this.lnl_pnlTemplatesRB.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lnl_pnlTemplatesRB.Location = new System.Drawing.Point(663, 1);
            this.lnl_pnlTemplatesRB.Name = "lnl_pnlTemplatesRB";
            this.lnl_pnlTemplatesRB.Size = new System.Drawing.Size(1, 375);
            this.lnl_pnlTemplatesRB.TabIndex = 6;
            this.lnl_pnlTemplatesRB.Text = "label3";
            // 
            // lnl_pnlTemplatesTB
            // 
            this.lnl_pnlTemplatesTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lnl_pnlTemplatesTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.lnl_pnlTemplatesTB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnl_pnlTemplatesTB.Location = new System.Drawing.Point(0, 0);
            this.lnl_pnlTemplatesTB.Name = "lnl_pnlTemplatesTB";
            this.lnl_pnlTemplatesTB.Size = new System.Drawing.Size(664, 1);
            this.lnl_pnlTemplatesTB.TabIndex = 5;
            this.lnl_pnlTemplatesTB.Text = "label1";
            // 
            // flxTemplates
            // 
            this.flxTemplates.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxTemplates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxTemplates.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxTemplates.ExtendLastCol = true;
            this.flxTemplates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxTemplates.Location = new System.Drawing.Point(0, 0);
            this.flxTemplates.Name = "flxTemplates";
            this.flxTemplates.Rows.Count = 1;
            this.flxTemplates.Rows.DefaultSize = 21;
            this.flxTemplates.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxTemplates.Size = new System.Drawing.Size(664, 376);
            this.flxTemplates.StyleInfo = resources.GetString("flxTemplates.StyleInfo");
            this.flxTemplates.TabIndex = 3;
            this.flxTemplates.EnterCell += new System.EventHandler(this.flxTemplates_EnterCell);
            this.flxTemplates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.flxTemplates_MouseDoubleClick);
            this.flxTemplates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxTemplates_MouseDown);
            //// 
            //// wdTemplate
            //// 
            //this.wdTemplate.Enabled = true;
            //this.wdTemplate.Location = new System.Drawing.Point(186, 63);
            //this.wdTemplate.Name = "wdTemplate";
            //this.wdTemplate.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdTemplate.OcxState")));
            //this.wdTemplate.Size = new System.Drawing.Size(62, 36);
            //this.wdTemplate.TabIndex = 15;
            //this.wdTemplate.Visible = false;
            //// 
            // pnlProviderTop
            // 
            this.pnlProviderTop.Controls.Add(this.pnlProvider);
            this.pnlProviderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProviderTop.Location = new System.Drawing.Point(292, 0);
            this.pnlProviderTop.Name = "pnlProviderTop";
            this.pnlProviderTop.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlProviderTop.Size = new System.Drawing.Size(667, 27);
            this.pnlProviderTop.TabIndex = 4;
            // 
            // pnlProvider
            // 
            this.pnlProvider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlProvider.BackgroundImage")));
            this.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProvider.Controls.Add(this.chkdncon);
            this.pnlProvider.Controls.Add(this.cmbCategory);
            this.pnlProvider.Controls.Add(this.cmbProvider);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderName);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderBB);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderLB);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderRB);
            this.pnlProvider.Controls.Add(this.lbl_pnlProviderTB);
            this.pnlProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProvider.Location = new System.Drawing.Point(0, 0);
            this.pnlProvider.Name = "pnlProvider";
            this.pnlProvider.Size = new System.Drawing.Size(664, 24);
            this.pnlProvider.TabIndex = 0;
            // 
            // chkdncon
            // 
            this.chkdncon.Location = new System.Drawing.Point(473, 1);
            this.chkdncon.Name = "chkdncon";
            this.chkdncon.Size = new System.Drawing.Size(137, 18);
            this.chkdncon.TabIndex = 10;
            this.chkdncon.Text = "Download Content";
            this.chkdncon.Visible = false;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbCategory.Location = new System.Drawing.Point(113, 1);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(321, 22);
            this.cmbCategory.TabIndex = 9;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.Location = new System.Drawing.Point(113, 1);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(321, 22);
            this.cmbProvider.TabIndex = 1;
            this.cmbProvider.SelectionChangeCommitted += new System.EventHandler(this.cmbProvider_SelectionChangeCommitted);
            // 
            // lbl_pnlProviderName
            // 
            this.lbl_pnlProviderName.AutoSize = true;
            this.lbl_pnlProviderName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_pnlProviderName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlProviderName.Location = new System.Drawing.Point(5, 5);
            this.lbl_pnlProviderName.Name = "lbl_pnlProviderName";
            this.lbl_pnlProviderName.Size = new System.Drawing.Size(103, 14);
            this.lbl_pnlProviderName.TabIndex = 0;
            this.lbl_pnlProviderName.Text = "Provider Name :";
            this.lbl_pnlProviderName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_pnlProviderBB
            // 
            this.lbl_pnlProviderBB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderBB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlProviderBB.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlProviderBB.Location = new System.Drawing.Point(1, 23);
            this.lbl_pnlProviderBB.Name = "lbl_pnlProviderBB";
            this.lbl_pnlProviderBB.Size = new System.Drawing.Size(662, 1);
            this.lbl_pnlProviderBB.TabIndex = 8;
            this.lbl_pnlProviderBB.Text = "label2";
            // 
            // lbl_pnlProviderLB
            // 
            this.lbl_pnlProviderLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderLB.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlProviderLB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlProviderLB.Location = new System.Drawing.Point(0, 1);
            this.lbl_pnlProviderLB.Name = "lbl_pnlProviderLB";
            this.lbl_pnlProviderLB.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlProviderLB.TabIndex = 7;
            this.lbl_pnlProviderLB.Text = "label4";
            // 
            // lbl_pnlProviderRB
            // 
            this.lbl_pnlProviderRB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderRB.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlProviderRB.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlProviderRB.Location = new System.Drawing.Point(663, 1);
            this.lbl_pnlProviderRB.Name = "lbl_pnlProviderRB";
            this.lbl_pnlProviderRB.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlProviderRB.TabIndex = 6;
            this.lbl_pnlProviderRB.Text = "label3";
            // 
            // lbl_pnlProviderTB
            // 
            this.lbl_pnlProviderTB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlProviderTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlProviderTB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlProviderTB.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlProviderTB.Name = "lbl_pnlProviderTB";
            this.lbl_pnlProviderTB.Size = new System.Drawing.Size(664, 1);
            this.lbl_pnlProviderTB.TabIndex = 5;
            this.lbl_pnlProviderTB.Text = "label1";
            // 
            // Splitter1
            // 
            this.Splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Splitter1.Location = new System.Drawing.Point(289, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 738);
            this.Splitter1.TabIndex = 3;
            this.Splitter1.TabStop = false;
            // 
            // pnlCategory
            // 
            this.pnlCategory.Controls.Add(this.Panel1);
            this.pnlCategory.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCategory.Location = new System.Drawing.Point(28, 0);
            this.pnlCategory.Name = "pnlCategory";
            this.pnlCategory.Size = new System.Drawing.Size(261, 738);
            this.pnlCategory.TabIndex = 0;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.trvCategories);
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.Label6);
            this.Panel1.Controls.Add(this.Label7);
            this.Panel1.Controls.Add(this.Label8);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.Panel1.Size = new System.Drawing.Size(261, 738);
            this.Panel1.TabIndex = 9;
            // 
            // trvCategories
            // 
            this.trvCategories.BackColor = System.Drawing.Color.White;
            this.trvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCategories.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvCategories.ForeColor = System.Drawing.Color.Black;
            this.trvCategories.HideSelection = false;
            this.trvCategories.ImageIndex = 0;
            this.trvCategories.ImageList = this.imgTempalte;
            this.trvCategories.Indent = 20;
            this.trvCategories.ItemHeight = 20;
            this.trvCategories.Location = new System.Drawing.Point(8, 5);
            this.trvCategories.Name = "trvCategories";
            this.trvCategories.SelectedImageIndex = 0;
            this.trvCategories.ShowLines = false;
            this.trvCategories.ShowPlusMinus = false;
            this.trvCategories.ShowRootLines = false;
            this.trvCategories.Size = new System.Drawing.Size(252, 729);
            this.trvCategories.TabIndex = 2;
            this.trvCategories.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCategories_AfterCheck);
            this.trvCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCategories_AfterSelect);
            // 
            // imgTempalte
            // 
            this.imgTempalte.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTempalte.ImageStream")));
            this.imgTempalte.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTempalte.Images.SetKeyName(0, "Bullet06.ico");
            this.imgTempalte.Images.SetKeyName(1, "Small Arrow.ico");
            this.imgTempalte.Images.SetKeyName(2, "world_link.ico");
            this.imgTempalte.Images.SetKeyName(3, "Tempate Category.ico");
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.White;
            this.Label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(8, 1);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(252, 4);
            this.Label9.TabIndex = 14;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.White;
            this.Label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(4, 1);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(4, 733);
            this.Label4.TabIndex = 13;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 734);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(256, 1);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "label2";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.Location = new System.Drawing.Point(3, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(1, 734);
            this.Label6.TabIndex = 11;
            this.Label6.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label7.Location = new System.Drawing.Point(260, 1);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 734);
            this.Label7.TabIndex = 10;
            this.Label7.Text = "label3";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.Location = new System.Drawing.Point(3, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(258, 1);
            this.Label8.TabIndex = 9;
            this.Label8.Text = "label1";
            // 
            // pnltls
            // 
            this.pnltls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltls.Controls.Add(this.label24);
            this.pnltls.Controls.Add(this.tlsgloCommunity);
            this.pnltls.Controls.Add(this.btn_Right1);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnltls.Controls.Add(this.label53);
            this.pnltls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltls.Location = new System.Drawing.Point(0, 0);
            this.pnltls.Name = "pnltls";
            this.pnltls.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltls.Size = new System.Drawing.Size(28, 738);
            this.pnltls.TabIndex = 106;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(4, 734);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(23, 1);
            this.label24.TabIndex = 144;
            // 
            // tlsgloCommunity
            // 
            this.tlsgloCommunity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tlsgloCommunity.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsgloCommunity.BackgroundImage")));
            this.tlsgloCommunity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsgloCommunity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlsgloCommunity.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsgloCommunity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbClinicRepository,
            this.tlbGlobalRepository});
            this.tlsgloCommunity.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tlsgloCommunity.Location = new System.Drawing.Point(4, 23);
            this.tlsgloCommunity.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.tlsgloCommunity.Name = "tlsgloCommunity";
            this.tlsgloCommunity.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 712);
            this.tlsgloCommunity.TabIndex = 21;
            this.tlsgloCommunity.Text = "toolStrip1";
            this.tlsgloCommunity.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // tlbClinicRepository
            // 
            this.tlbClinicRepository.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbClinicRepository.Image = ((System.Drawing.Image)(resources.GetObject("tlbClinicRepository.Image")));
            this.tlbClinicRepository.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbClinicRepository.Name = "tlbClinicRepository";
            this.tlbClinicRepository.Size = new System.Drawing.Size(21, 154);
            this.tlbClinicRepository.Text = "  Practice Repository";
            this.tlbClinicRepository.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tlbClinicRepository.Click += new System.EventHandler(this.tlbClinicRepository_Click);
            // 
            // tlbGlobalRepository
            // 
            this.tlbGlobalRepository.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbGlobalRepository.Image = ((System.Drawing.Image)(resources.GetObject("tlbGlobalRepository.Image")));
            this.tlbGlobalRepository.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbGlobalRepository.Name = "tlbGlobalRepository";
            this.tlbGlobalRepository.Size = new System.Drawing.Size(21, 143);
            this.tlbGlobalRepository.Text = "  Global Repository";
            this.tlbGlobalRepository.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tlbGlobalRepository.Click += new System.EventHandler(this.tlbGlobalRepository_Click);
            // 
            // btn_Right1
            // 
            this.btn_Right1.BackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Right1.BackgroundImage")));
            this.btn_Right1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Right1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Right1.FlatAppearance.BorderSize = 0;
            this.btn_Right1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Right1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Right1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Right1.Location = new System.Drawing.Point(4, 1);
            this.btn_Right1.Name = "btn_Right1";
            this.btn_Right1.Size = new System.Drawing.Size(23, 22);
            this.btn_Right1.TabIndex = 16;
            this.btn_Right1.UseVisualStyleBackColor = false;
            // 
            // lbl_pnlSmallStripLeftBrd
            // 
            this.lbl_pnlSmallStripLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSmallStripLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSmallStripLeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_pnlSmallStripLeftBrd.Name = "lbl_pnlSmallStripLeftBrd";
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 734);
            this.lbl_pnlSmallStripLeftBrd.TabIndex = 9;
            // 
            // lbl_pnlSmallStripTopBrd
            // 
            this.lbl_pnlSmallStripTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSmallStripTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSmallStripTopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnlSmallStripTopBrd.Name = "lbl_pnlSmallStripTopBrd";
            this.lbl_pnlSmallStripTopBrd.Size = new System.Drawing.Size(24, 1);
            this.lbl_pnlSmallStripTopBrd.TabIndex = 141;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(27, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 735);
            this.label53.TabIndex = 143;
            // 
            // pnlProcess
            // 
            this.pnlProcess.BackColor = System.Drawing.Color.White;
            this.pnlProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProcess.Controls.Add(this.Label61);
            this.pnlProcess.Controls.Add(this.Label62);
            this.pnlProcess.Controls.Add(this.Label65);
            this.pnlProcess.Controls.Add(this.Label66);
            this.pnlProcess.Controls.Add(this.pictureBox1);
            this.pnlProcess.Controls.Add(this.lblProcess);
            this.pnlProcess.Location = new System.Drawing.Point(525, 292);
            this.pnlProcess.Name = "pnlProcess";
            this.pnlProcess.Size = new System.Drawing.Size(167, 154);
            this.pnlProcess.TabIndex = 29;
            this.pnlProcess.Visible = false;
            // 
            // Label61
            // 
            this.Label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.Label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label61.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Label61.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label61.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.Label61.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label61.Location = new System.Drawing.Point(166, 1);
            this.Label61.Name = "Label61";
            this.Label61.Size = new System.Drawing.Size(1, 152);
            this.Label61.TabIndex = 23;
            // 
            // Label62
            // 
            this.Label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.Label62.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label62.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Label62.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.Label62.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label62.Location = new System.Drawing.Point(0, 1);
            this.Label62.Name = "Label62";
            this.Label62.Size = new System.Drawing.Size(1, 152);
            this.Label62.TabIndex = 22;
            // 
            // Label65
            // 
            this.Label65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.Label65.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label65.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Label65.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label65.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.Label65.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label65.Location = new System.Drawing.Point(0, 153);
            this.Label65.Name = "Label65";
            this.Label65.Size = new System.Drawing.Size(167, 1);
            this.Label65.TabIndex = 21;
            // 
            // Label66
            // 
            this.Label66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            this.Label66.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label66.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Label66.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label66.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.Label66.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label66.Location = new System.Drawing.Point(0, 0);
            this.Label66.Name = "Label66";
            this.Label66.Size = new System.Drawing.Size(167, 1);
            this.Label66.TabIndex = 19;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::gloCommunity.Properties.Resources.Wait;
            this.pictureBox1.Location = new System.Drawing.Point(33, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 98);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // lblProcess
            // 
            this.lblProcess.AutoEllipsis = true;
            this.lblProcess.AutoSize = true;
            this.lblProcess.BackColor = System.Drawing.Color.Transparent;
            this.lblProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblProcess.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(125)))));
            this.lblProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProcess.Location = new System.Drawing.Point(12, 118);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(143, 23);
            this.lblProcess.TabIndex = 17;
            this.lblProcess.Text = "Please Wait...";
            // 
            // UCTemplates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlProcess);
            this.Controls.Add(this.pnlMainMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCTemplates";
            this.Size = new System.Drawing.Size(959, 738);
            this.Load += new System.EventHandler(this.UCTemplates_Load);
            this.pnlMainMain.ResumeLayout(false);
            this.pnlSimmary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wdopenfl)).EndInit();
            this.pnlTemplates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxTemplates)).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).EndInit();
            this.pnlProviderTop.ResumeLayout(false);
            this.pnlProvider.ResumeLayout(false);
            this.pnlProvider.PerformLayout();
            this.pnlCategory.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.pnlProcess.ResumeLayout(false);
            this.pnlProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlMainMain;
        internal System.Windows.Forms.Panel pnlTemplates;
        private System.Windows.Forms.Label lnl_pnlTemplatesBB;
        private System.Windows.Forms.Label lnl_pnlTemplatesLB;
        private System.Windows.Forms.Label lnl_pnlTemplatesRB;
        private System.Windows.Forms.Label lnl_pnlTemplatesTB;
        public C1.Win.C1FlexGrid.C1FlexGrid flxTemplates;
  //      internal AxDSOFramer.AxFramerControl wdTemplate;
        internal System.Windows.Forms.Panel pnlSimmary;
        private System.Windows.Forms.Label lbl_pnlSummaryBB;
        private System.Windows.Forms.Label lbl_pnlSummaryLB;
        private System.Windows.Forms.Label lbl_pnlSummaryRB;
        private System.Windows.Forms.Label lbl_pnlSummaryTB;
        internal System.Windows.Forms.Panel pnlProviderTop;
        internal System.Windows.Forms.Panel pnlProvider;
        internal System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Label lbl_pnlProviderName;
        private System.Windows.Forms.Label lbl_pnlProviderBB;
        private System.Windows.Forms.Label lbl_pnlProviderLB;
        private System.Windows.Forms.Label lbl_pnlProviderRB;
        private System.Windows.Forms.Label lbl_pnlProviderTB;
        internal System.Windows.Forms.Splitter Splitter1;
        internal System.Windows.Forms.Panel pnlCategory;
        internal System.Windows.Forms.Panel Panel1;
        public System.Windows.Forms.TreeView trvCategories;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Splitter splitter2;
        internal AxDSOFramer.AxFramerControl wdopenfl;
        public System.Windows.Forms.ComboBox cmbCategory;
        public System.Windows.Forms.CheckBox chkdncon;
        private System.Windows.Forms.ImageList imgTempalte;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label24;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Panel pnlProcess;
        private System.Windows.Forms.Label Label61;
        private System.Windows.Forms.Label Label62;
        private System.Windows.Forms.Label Label65;
        private System.Windows.Forms.Label Label66;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblProcess;


    }
}
