namespace gloCommunity.UserControls
{
    partial class UCDmSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDmSetup));
            this.pnlMainMain = new System.Windows.Forms.Panel();
            this.pnlCVSetup = new System.Windows.Forms.Panel();
            this.splitCVsetup = new System.Windows.Forms.SplitContainer();
            this.pnlCVCriteria = new System.Windows.Forms.Panel();
            this.c1PatientCriteria = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlSummaryOthers = new System.Windows.Forms.Panel();
            this.pnlGuideline = new System.Windows.Forms.Panel();
            this.Label99 = new System.Windows.Forms.Label();
            this.Label100 = new System.Windows.Forms.Label();
            this.Label101 = new System.Windows.Forms.Label();
            this.Label102 = new System.Windows.Forms.Label();
            this.trOrderInfo = new System.Windows.Forms.TreeView();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlGuidelineHeader = new System.Windows.Forms.Panel();
            this.pnl3 = new System.Windows.Forms.Panel();
            this.Label95 = new System.Windows.Forms.Label();
            this.Label96 = new System.Windows.Forms.Label();
            this.Label97 = new System.Windows.Forms.Label();
            this.Label98 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Splitter5 = new System.Windows.Forms.Splitter();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.Label91 = new System.Windows.Forms.Label();
            this.Label92 = new System.Windows.Forms.Label();
            this.Label93 = new System.Windows.Forms.Label();
            this.Label94 = new System.Windows.Forms.Label();
            this.txt_summary = new System.Windows.Forms.TextBox();
            this.pnlSummaryHeader = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label64 = new System.Windows.Forms.Label();
            this.Label87 = new System.Windows.Forms.Label();
            this.Label88 = new System.Windows.Forms.Label();
            this.SplitterMain = new System.Windows.Forms.Splitter();
            this.pnlRepository = new System.Windows.Forms.Panel();
            this.pnlgloUC_TreeView2 = new System.Windows.Forms.Panel();
            this.trvDmSetup = new System.Windows.Forms.TreeView();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.pnlMainMain.SuspendLayout();
            this.pnlCVSetup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCVsetup)).BeginInit();
            this.splitCVsetup.Panel1.SuspendLayout();
            this.splitCVsetup.Panel2.SuspendLayout();
            this.splitCVsetup.SuspendLayout();
            this.pnlCVCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientCriteria)).BeginInit();
            this.pnlSummaryOthers.SuspendLayout();
            this.pnlGuideline.SuspendLayout();
            this.pnlGuidelineHeader.SuspendLayout();
            this.pnl3.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlSummaryHeader.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnlRepository.SuspendLayout();
            this.pnlgloUC_TreeView2.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainMain
            // 
            this.pnlMainMain.Controls.Add(this.pnlCVSetup);
            this.pnlMainMain.Controls.Add(this.SplitterMain);
            this.pnlMainMain.Controls.Add(this.pnlRepository);
            this.pnlMainMain.Controls.Add(this.pnltls);
            this.pnlMainMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMainMain.Name = "pnlMainMain";
            this.pnlMainMain.Size = new System.Drawing.Size(985, 844);
            this.pnlMainMain.TabIndex = 6;
            // 
            // pnlCVSetup
            // 
            this.pnlCVSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCVSetup.Controls.Add(this.splitCVsetup);
            this.pnlCVSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCVSetup.Location = new System.Drawing.Point(292, 0);
            this.pnlCVSetup.Name = "pnlCVSetup";
            this.pnlCVSetup.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.pnlCVSetup.Size = new System.Drawing.Size(693, 844);
            this.pnlCVSetup.TabIndex = 2;
            // 
            // splitCVsetup
            // 
            this.splitCVsetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCVsetup.Location = new System.Drawing.Point(0, 0);
            this.splitCVsetup.Name = "splitCVsetup";
            // 
            // splitCVsetup.Panel1
            // 
            this.splitCVsetup.Panel1.Controls.Add(this.pnlCVCriteria);
            // 
            // splitCVsetup.Panel2
            // 
            this.splitCVsetup.Panel2.Controls.Add(this.pnlSummaryOthers);
            this.splitCVsetup.Size = new System.Drawing.Size(690, 844);
            this.splitCVsetup.SplitterDistance = 258;
            this.splitCVsetup.TabIndex = 0;
            // 
            // pnlCVCriteria
            // 
            this.pnlCVCriteria.Controls.Add(this.c1PatientCriteria);
            this.pnlCVCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCVCriteria.Location = new System.Drawing.Point(0, 0);
            this.pnlCVCriteria.Name = "pnlCVCriteria";
            this.pnlCVCriteria.Size = new System.Drawing.Size(258, 844);
            this.pnlCVCriteria.TabIndex = 0;
            // 
            // c1PatientCriteria
            // 
            this.c1PatientCriteria.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientCriteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientCriteria.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientCriteria.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.c1PatientCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientCriteria.ExtendLastCol = true;
            this.c1PatientCriteria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientCriteria.Location = new System.Drawing.Point(0, 0);
            this.c1PatientCriteria.Name = "c1PatientCriteria";
            this.c1PatientCriteria.Rows.Count = 1;
            this.c1PatientCriteria.Rows.DefaultSize = 21;
            this.c1PatientCriteria.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientCriteria.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.c1PatientCriteria.Size = new System.Drawing.Size(258, 844);
            this.c1PatientCriteria.StyleInfo = resources.GetString("c1PatientCriteria.StyleInfo");
            this.c1PatientCriteria.TabIndex = 4;
            this.c1PatientCriteria.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1PatientCriteria_MouseDoubleClick);
            this.c1PatientCriteria.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1PatientCriteria_MouseDown);
            // 
            // pnlSummaryOthers
            // 
            this.pnlSummaryOthers.Controls.Add(this.pnlGuideline);
            this.pnlSummaryOthers.Controls.Add(this.pnlGuidelineHeader);
            this.pnlSummaryOthers.Controls.Add(this.Splitter5);
            this.pnlSummaryOthers.Controls.Add(this.pnlSummary);
            this.pnlSummaryOthers.Controls.Add(this.pnlSummaryHeader);
            this.pnlSummaryOthers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSummaryOthers.Location = new System.Drawing.Point(0, 0);
            this.pnlSummaryOthers.Name = "pnlSummaryOthers";
            this.pnlSummaryOthers.Size = new System.Drawing.Size(428, 844);
            this.pnlSummaryOthers.TabIndex = 4;
            this.pnlSummaryOthers.Visible = false;
            // 
            // pnlGuideline
            // 
            this.pnlGuideline.Controls.Add(this.Label99);
            this.pnlGuideline.Controls.Add(this.Label100);
            this.pnlGuideline.Controls.Add(this.Label101);
            this.pnlGuideline.Controls.Add(this.Label102);
            this.pnlGuideline.Controls.Add(this.trOrderInfo);
            this.pnlGuideline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGuideline.Location = new System.Drawing.Point(0, 417);
            this.pnlGuideline.Name = "pnlGuideline";
            this.pnlGuideline.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlGuideline.Size = new System.Drawing.Size(428, 427);
            this.pnlGuideline.TabIndex = 2;
            // 
            // Label99
            // 
            this.Label99.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label99.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label99.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label99.Location = new System.Drawing.Point(1, 423);
            this.Label99.Name = "Label99";
            this.Label99.Size = new System.Drawing.Size(426, 1);
            this.Label99.TabIndex = 12;
            this.Label99.Text = "label2";
            // 
            // Label100
            // 
            this.Label100.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label100.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label100.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label100.Location = new System.Drawing.Point(0, 1);
            this.Label100.Name = "Label100";
            this.Label100.Size = new System.Drawing.Size(1, 423);
            this.Label100.TabIndex = 11;
            // 
            // Label101
            // 
            this.Label101.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label101.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label101.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label101.Location = new System.Drawing.Point(427, 1);
            this.Label101.Name = "Label101";
            this.Label101.Size = new System.Drawing.Size(1, 423);
            this.Label101.TabIndex = 10;
            this.Label101.Text = "label3";
            // 
            // Label102
            // 
            this.Label102.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label102.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label102.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label102.Location = new System.Drawing.Point(0, 0);
            this.Label102.Name = "Label102";
            this.Label102.Size = new System.Drawing.Size(428, 1);
            this.Label102.TabIndex = 9;
            this.Label102.Text = "label1";
            // 
            // trOrderInfo
            // 
            this.trOrderInfo.BackColor = System.Drawing.Color.White;
            this.trOrderInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trOrderInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trOrderInfo.ForeColor = System.Drawing.Color.Black;
            this.trOrderInfo.HideSelection = false;
            this.trOrderInfo.ImageIndex = 0;
            this.trOrderInfo.ImageList = this.ImageList1;
            this.trOrderInfo.ItemHeight = 21;
            this.trOrderInfo.Location = new System.Drawing.Point(0, 0);
            this.trOrderInfo.Name = "trOrderInfo";
            this.trOrderInfo.SelectedImageIndex = 0;
            this.trOrderInfo.ShowLines = false;
            this.trOrderInfo.Size = new System.Drawing.Size(428, 424);
            this.trOrderInfo.TabIndex = 5;
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "Bullet06.ico");
            this.ImageList1.Images.SetKeyName(1, "Pat Demographic.ico");
            this.ImageList1.Images.SetKeyName(2, "History.ico");
            this.ImageList1.Images.SetKeyName(3, "ICD 09.ico");
            this.ImageList1.Images.SetKeyName(4, "Drugs.ico");
            this.ImageList1.Images.SetKeyName(5, "CPT.ico");
            this.ImageList1.Images.SetKeyName(6, "Lab.ico");
            this.ImageList1.Images.SetKeyName(7, "Radiology.ico");
            this.ImageList1.Images.SetKeyName(8, "Guideline Template.ico");
            this.ImageList1.Images.SetKeyName(9, "RX.ico");
            this.ImageList1.Images.SetKeyName(10, "PatientDetails.ico");
            this.ImageList1.Images.SetKeyName(11, "Small Arrow.ico");
            this.ImageList1.Images.SetKeyName(12, "Immunization_Old.ico");
            this.ImageList1.Images.SetKeyName(13, "Tempate Category_new.ico");
            // 
            // pnlGuidelineHeader
            // 
            this.pnlGuidelineHeader.Controls.Add(this.pnl3);
            this.pnlGuidelineHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGuidelineHeader.Location = new System.Drawing.Point(0, 390);
            this.pnlGuidelineHeader.Name = "pnlGuidelineHeader";
            this.pnlGuidelineHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlGuidelineHeader.Size = new System.Drawing.Size(428, 27);
            this.pnlGuidelineHeader.TabIndex = 5;
            // 
            // pnl3
            // 
            this.pnl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl3.Controls.Add(this.Label95);
            this.pnl3.Controls.Add(this.Label96);
            this.pnl3.Controls.Add(this.Label97);
            this.pnl3.Controls.Add(this.Label98);
            this.pnl3.Controls.Add(this.Label6);
            this.pnl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl3.Location = new System.Drawing.Point(0, 0);
            this.pnl3.Name = "pnl3";
            this.pnl3.Size = new System.Drawing.Size(428, 24);
            this.pnl3.TabIndex = 1;
            // 
            // Label95
            // 
            this.Label95.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label95.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label95.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label95.Location = new System.Drawing.Point(1, 23);
            this.Label95.Name = "Label95";
            this.Label95.Size = new System.Drawing.Size(426, 1);
            this.Label95.TabIndex = 12;
            this.Label95.Text = "label2";
            // 
            // Label96
            // 
            this.Label96.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label96.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label96.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label96.Location = new System.Drawing.Point(0, 1);
            this.Label96.Name = "Label96";
            this.Label96.Size = new System.Drawing.Size(1, 23);
            this.Label96.TabIndex = 11;
            // 
            // Label97
            // 
            this.Label97.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label97.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label97.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label97.Location = new System.Drawing.Point(427, 1);
            this.Label97.Name = "Label97";
            this.Label97.Size = new System.Drawing.Size(1, 23);
            this.Label97.TabIndex = 10;
            this.Label97.Text = "label3";
            // 
            // Label98
            // 
            this.Label98.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label98.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label98.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label98.Location = new System.Drawing.Point(0, 0);
            this.Label98.Name = "Label98";
            this.Label98.Size = new System.Drawing.Size(428, 1);
            this.Label98.TabIndex = 9;
            this.Label98.Text = "label1";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.White;
            this.Label6.Image = ((System.Drawing.Image)(resources.GetObject("Label6.Image")));
            this.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label6.Location = new System.Drawing.Point(0, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(428, 24);
            this.Label6.TabIndex = 0;
            this.Label6.Text = "       Orders to be Given";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Splitter5
            // 
            this.Splitter5.Dock = System.Windows.Forms.DockStyle.Top;
            this.Splitter5.Location = new System.Drawing.Point(0, 387);
            this.Splitter5.Name = "Splitter5";
            this.Splitter5.Size = new System.Drawing.Size(428, 3);
            this.Splitter5.TabIndex = 6;
            this.Splitter5.TabStop = false;
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.Label91);
            this.pnlSummary.Controls.Add(this.Label92);
            this.pnlSummary.Controls.Add(this.Label93);
            this.pnlSummary.Controls.Add(this.Label94);
            this.pnlSummary.Controls.Add(this.txt_summary);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(0, 27);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(428, 360);
            this.pnlSummary.TabIndex = 1;
            // 
            // Label91
            // 
            this.Label91.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label91.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label91.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label91.Location = new System.Drawing.Point(1, 359);
            this.Label91.Name = "Label91";
            this.Label91.Size = new System.Drawing.Size(426, 1);
            this.Label91.TabIndex = 12;
            this.Label91.Text = "label2";
            // 
            // Label92
            // 
            this.Label92.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label92.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label92.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label92.Location = new System.Drawing.Point(0, 1);
            this.Label92.Name = "Label92";
            this.Label92.Size = new System.Drawing.Size(1, 359);
            this.Label92.TabIndex = 11;
            // 
            // Label93
            // 
            this.Label93.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label93.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label93.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label93.Location = new System.Drawing.Point(427, 1);
            this.Label93.Name = "Label93";
            this.Label93.Size = new System.Drawing.Size(1, 359);
            this.Label93.TabIndex = 10;
            this.Label93.Text = "label3";
            // 
            // Label94
            // 
            this.Label94.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label94.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label94.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label94.Location = new System.Drawing.Point(0, 0);
            this.Label94.Name = "Label94";
            this.Label94.Size = new System.Drawing.Size(428, 1);
            this.Label94.TabIndex = 9;
            this.Label94.Text = "label1";
            // 
            // txt_summary
            // 
            this.txt_summary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_summary.ForeColor = System.Drawing.Color.Black;
            this.txt_summary.Location = new System.Drawing.Point(0, 0);
            this.txt_summary.Multiline = true;
            this.txt_summary.Name = "txt_summary";
            this.txt_summary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_summary.Size = new System.Drawing.Size(428, 360);
            this.txt_summary.TabIndex = 1;
            // 
            // pnlSummaryHeader
            // 
            this.pnlSummaryHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSummaryHeader.Controls.Add(this.Panel1);
            this.pnlSummaryHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummaryHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSummaryHeader.Name = "pnlSummaryHeader";
            this.pnlSummaryHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlSummaryHeader.Size = new System.Drawing.Size(428, 27);
            this.pnlSummaryHeader.TabIndex = 0;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel1.Controls.Add(this.Label26);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.Label64);
            this.Panel1.Controls.Add(this.Label87);
            this.Panel1.Controls.Add(this.Label88);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(428, 24);
            this.Panel1.TabIndex = 20;
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label26.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label26.Location = new System.Drawing.Point(1, 23);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(426, 1);
            this.Label26.TabIndex = 8;
            this.Label26.Text = "label2";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.White;
            this.Label5.Image = ((System.Drawing.Image)(resources.GetObject("Label5.Image")));
            this.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label5.Location = new System.Drawing.Point(1, 1);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(426, 23);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "     Summary";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label64
            // 
            this.Label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label64.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label64.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label64.Location = new System.Drawing.Point(0, 1);
            this.Label64.Name = "Label64";
            this.Label64.Size = new System.Drawing.Size(1, 23);
            this.Label64.TabIndex = 7;
            this.Label64.Text = "label4";
            // 
            // Label87
            // 
            this.Label87.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label87.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label87.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label87.Location = new System.Drawing.Point(427, 1);
            this.Label87.Name = "Label87";
            this.Label87.Size = new System.Drawing.Size(1, 23);
            this.Label87.TabIndex = 6;
            this.Label87.Text = "label3";
            // 
            // Label88
            // 
            this.Label88.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label88.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label88.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label88.Location = new System.Drawing.Point(0, 0);
            this.Label88.Name = "Label88";
            this.Label88.Size = new System.Drawing.Size(428, 1);
            this.Label88.TabIndex = 5;
            this.Label88.Text = "label1";
            // 
            // SplitterMain
            // 
            this.SplitterMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.SplitterMain.Location = new System.Drawing.Point(289, 0);
            this.SplitterMain.Name = "SplitterMain";
            this.SplitterMain.Size = new System.Drawing.Size(3, 844);
            this.SplitterMain.TabIndex = 3;
            this.SplitterMain.TabStop = false;
            // 
            // pnlRepository
            // 
            this.pnlRepository.Controls.Add(this.pnlgloUC_TreeView2);
            this.pnlRepository.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRepository.Location = new System.Drawing.Point(28, 0);
            this.pnlRepository.Name = "pnlRepository";
            this.pnlRepository.Size = new System.Drawing.Size(261, 844);
            this.pnlRepository.TabIndex = 0;
            // 
            // pnlgloUC_TreeView2
            // 
            this.pnlgloUC_TreeView2.Controls.Add(this.trvDmSetup);
            this.pnlgloUC_TreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgloUC_TreeView2.Location = new System.Drawing.Point(0, 0);
            this.pnlgloUC_TreeView2.Name = "pnlgloUC_TreeView2";
            this.pnlgloUC_TreeView2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlgloUC_TreeView2.Size = new System.Drawing.Size(261, 844);
            this.pnlgloUC_TreeView2.TabIndex = 59;
            // 
            // trvDmSetup
            // 
            this.trvDmSetup.BackColor = System.Drawing.Color.White;
            this.trvDmSetup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvDmSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDmSetup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvDmSetup.ForeColor = System.Drawing.Color.Black;
            this.trvDmSetup.HideSelection = false;
            this.trvDmSetup.ImageIndex = 0;
            this.trvDmSetup.ImageList = this.ImageList1;
            this.trvDmSetup.Indent = 20;
            this.trvDmSetup.ItemHeight = 20;
            this.trvDmSetup.Location = new System.Drawing.Point(3, 0);
            this.trvDmSetup.Name = "trvDmSetup";
            this.trvDmSetup.SelectedImageIndex = 0;
            this.trvDmSetup.ShowLines = false;
            this.trvDmSetup.ShowNodeToolTips = true;
            this.trvDmSetup.ShowRootLines = false;
            this.trvDmSetup.Size = new System.Drawing.Size(258, 841);
            this.trvDmSetup.TabIndex = 26;
            this.trvDmSetup.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDmSetup_NodeMouseClick);
            // 
            // pnltls
            // 
            this.pnltls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltls.Controls.Add(this.label19);
            this.pnltls.Controls.Add(this.tlsgloCommunity);
            this.pnltls.Controls.Add(this.btn_Right1);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnltls.Controls.Add(this.label53);
            this.pnltls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltls.Location = new System.Drawing.Point(0, 0);
            this.pnltls.Name = "pnltls";
            this.pnltls.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltls.Size = new System.Drawing.Size(28, 844);
            this.pnltls.TabIndex = 101;
            this.pnltls.Visible = false;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 840);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 1);
            this.label19.TabIndex = 144;
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
            this.tlsgloCommunity.Name = "tlsgloCommunity";
            this.tlsgloCommunity.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 818);
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
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 840);
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
            this.label53.Size = new System.Drawing.Size(1, 841);
            this.label53.TabIndex = 143;
            // 
            // UCDmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlMainMain);
            this.Name = "UCDmSetup";
            this.Size = new System.Drawing.Size(985, 844);
            this.Load += new System.EventHandler(this.UCDmSetup_Load);
            this.pnlMainMain.ResumeLayout(false);
            this.pnlCVSetup.ResumeLayout(false);
            this.splitCVsetup.Panel1.ResumeLayout(false);
            this.splitCVsetup.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCVsetup)).EndInit();
            this.splitCVsetup.ResumeLayout(false);
            this.pnlCVCriteria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientCriteria)).EndInit();
            this.pnlSummaryOthers.ResumeLayout(false);
            this.pnlGuideline.ResumeLayout(false);
            this.pnlGuidelineHeader.ResumeLayout(false);
            this.pnl3.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlSummaryHeader.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.pnlRepository.ResumeLayout(false);
            this.pnlgloUC_TreeView2.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlMainMain;
        internal System.Windows.Forms.Panel pnlCVSetup;
        private System.Windows.Forms.SplitContainer splitCVsetup;
        private System.Windows.Forms.Panel pnlCVCriteria;
        public C1.Win.C1FlexGrid.C1FlexGrid c1PatientCriteria;
        internal System.Windows.Forms.Splitter SplitterMain;
        internal System.Windows.Forms.Panel pnlRepository;
        private System.Windows.Forms.Panel pnlgloUC_TreeView2;
        internal System.Windows.Forms.Panel pnlSummaryOthers;
        internal System.Windows.Forms.Panel pnlGuideline;
        private System.Windows.Forms.Label Label99;
        private System.Windows.Forms.Label Label100;
        private System.Windows.Forms.Label Label101;
        private System.Windows.Forms.Label Label102;
        internal System.Windows.Forms.TreeView trOrderInfo;
        internal System.Windows.Forms.Panel pnlGuidelineHeader;
        internal System.Windows.Forms.Panel pnl3;
        private System.Windows.Forms.Label Label95;
        private System.Windows.Forms.Label Label96;
        private System.Windows.Forms.Label Label97;
        private System.Windows.Forms.Label Label98;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Splitter Splitter5;
        internal System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label Label91;
        private System.Windows.Forms.Label Label92;
        private System.Windows.Forms.Label Label93;
        private System.Windows.Forms.Label Label94;
        internal System.Windows.Forms.TextBox txt_summary;
        internal System.Windows.Forms.Panel pnlSummaryHeader;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label64;
        private System.Windows.Forms.Label Label87;
        private System.Windows.Forms.Label Label88;
        internal System.Windows.Forms.ImageList ImageList1;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label19;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
        public System.Windows.Forms.TreeView trvDmSetup;
    }
}
