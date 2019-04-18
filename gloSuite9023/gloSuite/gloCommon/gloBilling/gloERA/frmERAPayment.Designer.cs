namespace gloBilling.gloERA
{
    partial class frmERAPayment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private bool blnDisposed;
        private static frmERAPayment frm;

        public static frmERAPayment GetInstance()
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmERAPayment();
                }
            }
            finally
            {

            }
            return frm;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    // Dispose managed resources. 
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                    try
                    {
                        if (oTimer != null)
                        {
                            oTimer.Tick -= new System.EventHandler(this.oTimer_Tick);
                            oTimer.Dispose();
                            oTimer = null;
                        }
                    }
                    catch
                    {
                    }
                    
                    if (oToolTip != null)
                    {
                        oToolTip.Dispose();
                        oToolTip = null;
                    }
                    //frm = Nothing 
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed. 

                // Note that this is not thread safe. 
                // Another thread could start disposing the object 
                // after the managed resources are disposed, 
                // but before the disposed flag is set to true. 
                // If thread safety is necessary, it must be 
                // implemented by the client. 
            }
            frm = null;
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        ~frmERAPayment()
        {
            Dispose(false);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmERAPayment));
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.tls_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Download = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsb_Import = new System.Windows.Forms.ToolStripButton();
            this.tsb_Post = new System.Windows.Forms.ToolStripButton();
            this.tsb_PaperEOB = new System.Windows.Forms.ToolStripButton();
            this.tsb_DetailPaperEOB = new System.Windows.Forms.ToolStripButton();
            this.tsb_PostingReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_ExceptionReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_DetailExceptionReport = new System.Windows.Forms.ToolStripButton();
            this.tsb_ViewPayment = new System.Windows.Forms.ToolStripButton();
            this.tsb_UnholdCheck = new System.Windows.Forms.ToolStripButton();
            this.tsb_HoldCheck = new System.Windows.Forms.ToolStripButton();
            this.tsb_UndoDelete = new System.Windows.Forms.ToolStripButton();
            this.tsb_MarkDeleted = new System.Windows.Forms.ToolStripButton();
            this.tsb_ERAFiles = new System.Windows.Forms.ToolStripButton();
            this.tsb_View = new System.Windows.Forms.ToolStripButton();
            this.tsb_Trial = new System.Windows.Forms.ToolStripButton();
            this.tsb_Notes = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_ExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.tbpg_Posted = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.c1Posted = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel17 = new System.Windows.Forms.Panel();
            this.c1PostedTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label93 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchPosted = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.btnClearSearchPosted = new System.Windows.Forms.Button();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label91 = new System.Windows.Forms.Label();
            this.cmbPosted = new System.Windows.Forms.ComboBox();
            this.btnShowChecksPosted = new System.Windows.Forms.Button();
            this.mskToDatePosted = new System.Windows.Forms.MaskedTextBox();
            this.mskFromDatePosted = new System.Windows.Forms.MaskedTextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.tbpg_ReadyToPost = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.c1ReadyToPost = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel16 = new System.Windows.Forms.Panel();
            this.c1ReadyToPostTotal = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label87 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlCloseSearch = new System.Windows.Forms.Panel();
            this.txtSearchReadyToPost = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.btnClearSearchReadyToPost = new System.Windows.Forms.Button();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.lblReadyToPostSearch = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabERA = new System.Windows.Forms.TabControl();
            this.tbpg_Hold = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label34 = new System.Windows.Forms.Label();
            this.c1Hold = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.txtSearchHold = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.btnClearSearchHold = new System.Windows.Forms.Button();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnShowChecksHold = new System.Windows.Forms.Button();
            this.mskToDateHold = new System.Windows.Forms.MaskedTextBox();
            this.mskFromDateHold = new System.Windows.Forms.MaskedTextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.tbpg_Deleted = new System.Windows.Forms.TabPage();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label54 = new System.Windows.Forms.Label();
            this.c1Deleted = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearchDeleted = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.btnClearSearchDeleted = new System.Windows.Forms.Button();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnShowChecksDeleted = new System.Windows.Forms.Button();
            this.mskToDateDeleted = new System.Windows.Forms.MaskedTextBox();
            this.mskFromDateDeletd = new System.Windows.Forms.MaskedTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.imgERA = new System.Windows.Forms.ImageList(this.components);
            this.pnlTab = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUP = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.prgProgress = new System.Windows.Forms.ProgressBar();
            this.pnlAdditionalCheckInfo = new System.Windows.Forms.Panel();
            this.lblPayerContact = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblPaymentBalanced = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.lblTotalPLBAmount = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblTotalClaimPaid = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblOriginalFileName = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.lblProductionDate = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlPostInfo = new System.Windows.Forms.Panel();
            this.label92 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.lblPayTray = new System.Windows.Forms.Label();
            this.pnlAddCheckInfoHeader = new System.Windows.Forms.Panel();
            this.pnlMessagetxtbox = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.pnlMessage = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.pnlToolStrip.SuspendLayout();
            this.tls_Main.SuspendLayout();
            this.tbpg_Posted.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Posted)).BeginInit();
            this.panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PostedTotal)).BeginInit();
            this.panel15.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel13.SuspendLayout();
            this.tbpg_ReadyToPost.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ReadyToPost)).BeginInit();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ReadyToPostTotal)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlCloseSearch.SuspendLayout();
            this.tabERA.SuspendLayout();
            this.tbpg_Hold.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Hold)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tbpg_Deleted.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Deleted)).BeginInit();
            this.panel14.SuspendLayout();
            this.panel11.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel10.SuspendLayout();
            this.pnlTab.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlAdditionalCheckInfo.SuspendLayout();
            this.pnlPostInfo.SuspendLayout();
            this.pnlAddCheckInfoHeader.SuspendLayout();
            this.pnlMessagetxtbox.SuspendLayout();
            this.pnlMessage.SuspendLayout();
            this.panel18.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.tls_Main);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1274, 56);
            this.pnlToolStrip.TabIndex = 19;
            // 
            // tls_Main
            // 
            this.tls_Main.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_Main.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Download,
            this.tsb_Import,
            this.tsb_Post,
            this.tsb_PaperEOB,
            this.tsb_DetailPaperEOB,
            this.tsb_PostingReport,
            this.tsb_ExceptionReport,
            this.tsb_DetailExceptionReport,
            this.tsb_ViewPayment,
            this.tsb_UnholdCheck,
            this.tsb_HoldCheck,
            this.tsb_UndoDelete,
            this.tsb_MarkDeleted,
            this.tsb_ERAFiles,
            this.tsb_View,
            this.tsb_Trial,
            this.tsb_Notes,
            this.tsb_Refresh,
            this.tsb_ExportToExcel,
            this.tsb_Close});
            this.tls_Main.Location = new System.Drawing.Point(0, 0);
            this.tls_Main.Name = "tls_Main";
            this.tls_Main.Size = new System.Drawing.Size(1274, 53);
            this.tls_Main.TabIndex = 0;
            this.tls_Main.Text = "toolStrip1";
            // 
            // tsb_Download
            // 
            this.tsb_Download.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Download.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Download.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Download.Image")));
            this.tsb_Download.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Download.Name = "tsb_Download";
            this.tsb_Download.Size = new System.Drawing.Size(82, 50);
            this.tsb_Download.Tag = "Download";
            this.tsb_Download.Text = "&Download";
            this.tsb_Download.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Download.ToolTipText = "Download ERA Files";
            // 
            // tsb_Import
            // 
            this.tsb_Import.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Import.Image")));
            this.tsb_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Import.Name = "tsb_Import";
            this.tsb_Import.Size = new System.Drawing.Size(54, 50);
            this.tsb_Import.Tag = "Import";
            this.tsb_Import.Text = "&Import";
            this.tsb_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Import.ToolTipText = "Import ERA Files";
            this.tsb_Import.Visible = false;
            this.tsb_Import.Click += new System.EventHandler(this.tsb_Import_Click);
            // 
            // tsb_Post
            // 
            this.tsb_Post.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Post.Image")));
            this.tsb_Post.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Post.Name = "tsb_Post";
            this.tsb_Post.Size = new System.Drawing.Size(39, 50);
            this.tsb_Post.Tag = "Post";
            this.tsb_Post.Text = "&Post";
            this.tsb_Post.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Post.Click += new System.EventHandler(this.tsb_Post_Click);
            // 
            // tsb_PaperEOB
            // 
            this.tsb_PaperEOB.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PaperEOB.Image")));
            this.tsb_PaperEOB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PaperEOB.Name = "tsb_PaperEOB";
            this.tsb_PaperEOB.Size = new System.Drawing.Size(74, 50);
            this.tsb_PaperEOB.Tag = "PaperEOB";
            this.tsb_PaperEOB.Text = "&Paper EOB";
            this.tsb_PaperEOB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PaperEOB.ToolTipText = "Paper EOB";
            this.tsb_PaperEOB.Click += new System.EventHandler(this.tsb_PaperEOB_Click);
            // 
            // tsb_DetailPaperEOB
            // 
            this.tsb_DetailPaperEOB.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DetailPaperEOB.Image")));
            this.tsb_DetailPaperEOB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DetailPaperEOB.Name = "tsb_DetailPaperEOB";
            this.tsb_DetailPaperEOB.Size = new System.Drawing.Size(113, 50);
            this.tsb_DetailPaperEOB.Text = "Detail Paper E&OB";
            this.tsb_DetailPaperEOB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DetailPaperEOB.Visible = false;
            this.tsb_DetailPaperEOB.Click += new System.EventHandler(this.tsb_DetailPaperEOB_Click);
            // 
            // tsb_PostingReport
            // 
            this.tsb_PostingReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PostingReport.Image")));
            this.tsb_PostingReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PostingReport.Name = "tsb_PostingReport";
            this.tsb_PostingReport.Size = new System.Drawing.Size(105, 50);
            this.tsb_PostingReport.Tag = "PostingReport";
            this.tsb_PostingReport.Text = "&Posting Report";
            this.tsb_PostingReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PostingReport.Visible = false;
            this.tsb_PostingReport.Click += new System.EventHandler(this.tsb_PostingReport_Click);
            // 
            // tsb_ExceptionReport
            // 
            this.tsb_ExceptionReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ExceptionReport.Image")));
            this.tsb_ExceptionReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ExceptionReport.Name = "tsb_ExceptionReport";
            this.tsb_ExceptionReport.Size = new System.Drawing.Size(118, 50);
            this.tsb_ExceptionReport.Tag = "Exception Report";
            this.tsb_ExceptionReport.Text = "&Exception Report";
            this.tsb_ExceptionReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ExceptionReport.ToolTipText = "Exception Report";
            this.tsb_ExceptionReport.Click += new System.EventHandler(this.tsb_ExceptionReport_Click);
            // 
            // tsb_DetailExceptionReport
            // 
            this.tsb_DetailExceptionReport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DetailExceptionReport.Image")));
            this.tsb_DetailExceptionReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DetailExceptionReport.Name = "tsb_DetailExceptionReport";
            this.tsb_DetailExceptionReport.Size = new System.Drawing.Size(157, 50);
            this.tsb_DetailExceptionReport.Text = "Detail Expection Report";
            this.tsb_DetailExceptionReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DetailExceptionReport.Visible = false;
            this.tsb_DetailExceptionReport.Click += new System.EventHandler(this.tsb_DetailExceptionReport_Click);
            // 
            // tsb_ViewPayment
            // 
            this.tsb_ViewPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ViewPayment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ViewPayment.Image")));
            this.tsb_ViewPayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ViewPayment.Name = "tsb_ViewPayment";
            this.tsb_ViewPayment.Size = new System.Drawing.Size(98, 50);
            this.tsb_ViewPayment.Tag = "Cancel";
            this.tsb_ViewPayment.Text = "&View Payment";
            this.tsb_ViewPayment.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_ViewPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ViewPayment.ToolTipText = "View Insurance Payment";
            this.tsb_ViewPayment.Click += new System.EventHandler(this.tsb_ViewPayment_Click);
            // 
            // tsb_UnholdCheck
            // 
            this.tsb_UnholdCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsb_UnholdCheck.Image")));
            this.tsb_UnholdCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UnholdCheck.Name = "tsb_UnholdCheck";
            this.tsb_UnholdCheck.Size = new System.Drawing.Size(57, 50);
            this.tsb_UnholdCheck.Tag = "Release";
            this.tsb_UnholdCheck.Text = "&Release";
            this.tsb_UnholdCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_UnholdCheck.Click += new System.EventHandler(this.tsb_UnholdCheck_Click);
            // 
            // tsb_HoldCheck
            // 
            this.tsb_HoldCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsb_HoldCheck.Image")));
            this.tsb_HoldCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_HoldCheck.Name = "tsb_HoldCheck";
            this.tsb_HoldCheck.Size = new System.Drawing.Size(39, 50);
            this.tsb_HoldCheck.Tag = "Hold";
            this.tsb_HoldCheck.Text = "&Hold";
            this.tsb_HoldCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_HoldCheck.Click += new System.EventHandler(this.tsb_HoldCheck_Click);
            // 
            // tsb_UndoDelete
            // 
            this.tsb_UndoDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_UndoDelete.Image")));
            this.tsb_UndoDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UndoDelete.Name = "tsb_UndoDelete";
            this.tsb_UndoDelete.Size = new System.Drawing.Size(86, 50);
            this.tsb_UndoDelete.Tag = "UndoDelete";
            this.tsb_UndoDelete.Text = "&Undo Delete";
            this.tsb_UndoDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_UndoDelete.Click += new System.EventHandler(this.tsb_UndoDelete_Click);
            // 
            // tsb_MarkDeleted
            // 
            this.tsb_MarkDeleted.Image = ((System.Drawing.Image)(resources.GetObject("tsb_MarkDeleted.Image")));
            this.tsb_MarkDeleted.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_MarkDeleted.Name = "tsb_MarkDeleted";
            this.tsb_MarkDeleted.Size = new System.Drawing.Size(92, 50);
            this.tsb_MarkDeleted.Tag = "MarkDeleted";
            this.tsb_MarkDeleted.Text = "&Mark Deleted";
            this.tsb_MarkDeleted.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_MarkDeleted.Click += new System.EventHandler(this.tsb_MarkDeleted_Click);
            // 
            // tsb_ERAFiles
            // 
            this.tsb_ERAFiles.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ERAFiles.Image")));
            this.tsb_ERAFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ERAFiles.Name = "tsb_ERAFiles";
            this.tsb_ERAFiles.Size = new System.Drawing.Size(65, 50);
            this.tsb_ERAFiles.Tag = "ERAFiles";
            this.tsb_ERAFiles.Text = "&ERA Files";
            this.tsb_ERAFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ERAFiles.ToolTipText = "View ERA Files";
            this.tsb_ERAFiles.Click += new System.EventHandler(this.tsb_ERAFiles_Click);
            // 
            // tsb_View
            // 
            this.tsb_View.Image = ((System.Drawing.Image)(resources.GetObject("tsb_View.Image")));
            this.tsb_View.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_View.Name = "tsb_View";
            this.tsb_View.Size = new System.Drawing.Size(63, 50);
            this.tsb_View.Tag = "ViewFile";
            this.tsb_View.Text = "&View File";
            this.tsb_View.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_View.Visible = false;
            this.tsb_View.Click += new System.EventHandler(this.tsb_View_Click);
            // 
            // tsb_Trial
            // 
            this.tsb_Trial.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Trial.Image")));
            this.tsb_Trial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Trial.Name = "tsb_Trial";
            this.tsb_Trial.Size = new System.Drawing.Size(36, 50);
            this.tsb_Trial.Tag = "Trial";
            this.tsb_Trial.Text = "&Trial";
            this.tsb_Trial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Trial.Click += new System.EventHandler(this.tsb_Trial_Click);
            // 
            // tsb_Notes
            // 
            this.tsb_Notes.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Notes.Image")));
            this.tsb_Notes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Notes.Name = "tsb_Notes";
            this.tsb_Notes.Size = new System.Drawing.Size(46, 50);
            this.tsb_Notes.Tag = "Notes";
            this.tsb_Notes.Text = "&Notes";
            this.tsb_Notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Notes.Click += new System.EventHandler(this.tsb_Notes_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "&Refresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_ExportToExcel
            // 
            this.tsb_ExportToExcel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ExportToExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ExportToExcel.Image")));
            this.tsb_ExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ExportToExcel.Name = "tsb_ExportToExcel";
            this.tsb_ExportToExcel.Size = new System.Drawing.Size(105, 50);
            this.tsb_ExportToExcel.Tag = "Export";
            this.tsb_ExportToExcel.Text = "Export To Excel";
            this.tsb_ExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ExportToExcel.Click += new System.EventHandler(this.tsb_ExportToExcel_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // tbpg_Posted
            // 
            this.tbpg_Posted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_Posted.Controls.Add(this.panel9);
            this.tbpg_Posted.Controls.Add(this.panel15);
            this.tbpg_Posted.Controls.Add(this.panel13);
            this.tbpg_Posted.ImageIndex = 2;
            this.tbpg_Posted.Location = new System.Drawing.Point(4, 23);
            this.tbpg_Posted.Name = "tbpg_Posted";
            this.tbpg_Posted.Size = new System.Drawing.Size(1266, 429);
            this.tbpg_Posted.TabIndex = 1;
            this.tbpg_Posted.Tag = "Posted";
            this.tbpg_Posted.Text = "Posted";
            this.tbpg_Posted.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.c1Posted);
            this.panel9.Controls.Add(this.panel17);
            this.panel9.Controls.Add(this.label44);
            this.panel9.Controls.Add(this.label45);
            this.panel9.Controls.Add(this.label46);
            this.panel9.Controls.Add(this.label47);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 61);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel9.Size = new System.Drawing.Size(1266, 368);
            this.panel9.TabIndex = 2;
            // 
            // c1Posted
            // 
            this.c1Posted.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Posted.BackColor = System.Drawing.Color.White;
            this.c1Posted.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Posted.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1Posted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Posted.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Posted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Posted.Location = new System.Drawing.Point(1, 4);
            this.c1Posted.Name = "c1Posted";
            this.c1Posted.Padding = new System.Windows.Forms.Padding(3);
            this.c1Posted.Rows.Count = 1;
            this.c1Posted.Rows.DefaultSize = 19;
            this.c1Posted.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Posted.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Posted.ShowCellLabels = true;
            this.c1Posted.Size = new System.Drawing.Size(1264, 343);
            this.c1Posted.StyleInfo = resources.GetString("c1Posted.StyleInfo");
            this.c1Posted.TabIndex = 0;
            this.c1Posted.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.C1All_AfterSort);
            this.c1Posted.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Posted_AfterResizeColumn);
            this.c1Posted.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Posted_AfterScroll);
            this.c1Posted.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
            this.c1Posted.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_MouseClick);
            // 
            // panel17
            // 
            this.panel17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel17.Controls.Add(this.c1PostedTotal);
            this.panel17.Controls.Add(this.label93);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel17.Location = new System.Drawing.Point(1, 347);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(1264, 20);
            this.panel17.TabIndex = 28;
            // 
            // c1PostedTotal
            // 
            this.c1PostedTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1PostedTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PostedTotal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1PostedTotal.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PostedTotal.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1PostedTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1PostedTotal.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1PostedTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PostedTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PostedTotal.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1PostedTotal.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PostedTotal.Location = new System.Drawing.Point(0, 1);
            this.c1PostedTotal.Name = "c1PostedTotal";
            this.c1PostedTotal.Rows.Count = 1;
            this.c1PostedTotal.Rows.DefaultSize = 19;
            this.c1PostedTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PostedTotal.ShowCellLabels = true;
            this.c1PostedTotal.Size = new System.Drawing.Size(1264, 19);
            this.c1PostedTotal.StyleInfo = resources.GetString("c1PostedTotal.StyleInfo");
            this.c1PostedTotal.TabIndex = 25;
            this.c1PostedTotal.TabStop = false;
            // 
            // label93
            // 
            this.label93.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label93.Dock = System.Windows.Forms.DockStyle.Top;
            this.label93.Location = new System.Drawing.Point(0, 0);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(1264, 1);
            this.label93.TabIndex = 24;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label44.Location = new System.Drawing.Point(1, 367);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1264, 1);
            this.label44.TabIndex = 25;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Location = new System.Drawing.Point(1, 3);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(1264, 1);
            this.label45.TabIndex = 24;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Right;
            this.label46.Location = new System.Drawing.Point(1265, 3);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(1, 365);
            this.label46.TabIndex = 23;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Left;
            this.label47.Location = new System.Drawing.Point(0, 3);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(1, 365);
            this.label47.TabIndex = 22;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.panel4);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 37);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(1266, 24);
            this.panel15.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.panel19);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.label31);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1266, 24);
            this.panel4.TabIndex = 20;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.Transparent;
            this.panel19.Controls.Add(this.label1);
            this.panel19.Controls.Add(this.txtSearchPosted);
            this.panel19.Controls.Add(this.label96);
            this.panel19.Controls.Add(this.label97);
            this.panel19.Controls.Add(this.btnClearSearchPosted);
            this.panel19.Controls.Add(this.label98);
            this.panel19.Controls.Add(this.label99);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel19.ForeColor = System.Drawing.Color.Black;
            this.panel19.Location = new System.Drawing.Point(66, 1);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(241, 22);
            this.panel19.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 5);
            this.label1.TabIndex = 43;
            // 
            // txtSearchPosted
            // 
            this.txtSearchPosted.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchPosted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchPosted.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchPosted.ForeColor = System.Drawing.Color.Black;
            this.txtSearchPosted.Location = new System.Drawing.Point(5, 3);
            this.txtSearchPosted.Name = "txtSearchPosted";
            this.txtSearchPosted.Size = new System.Drawing.Size(214, 15);
            this.txtSearchPosted.TabIndex = 0;
            this.txtSearchPosted.Tag = "Posted";
            this.txtSearchPosted.TextChanged += new System.EventHandler(this.txtSearchAll_TextChanged);
            this.txtSearchPosted.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchAll_KeyDown);
            // 
            // label96
            // 
            this.label96.BackColor = System.Drawing.Color.White;
            this.label96.Dock = System.Windows.Forms.DockStyle.Top;
            this.label96.Location = new System.Drawing.Point(5, 0);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(214, 3);
            this.label96.TabIndex = 37;
            // 
            // label97
            // 
            this.label97.BackColor = System.Drawing.Color.White;
            this.label97.Dock = System.Windows.Forms.DockStyle.Left;
            this.label97.Location = new System.Drawing.Point(1, 0);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(4, 22);
            this.label97.TabIndex = 38;
            // 
            // btnClearSearchPosted
            // 
            this.btnClearSearchPosted.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearSearchPosted.BackgroundImage")));
            this.btnClearSearchPosted.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearSearchPosted.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearSearchPosted.FlatAppearance.BorderSize = 0;
            this.btnClearSearchPosted.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchPosted.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchPosted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearchPosted.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSearchPosted.Image")));
            this.btnClearSearchPosted.Location = new System.Drawing.Point(219, 0);
            this.btnClearSearchPosted.Name = "btnClearSearchPosted";
            this.btnClearSearchPosted.Size = new System.Drawing.Size(21, 22);
            this.btnClearSearchPosted.TabIndex = 41;
            this.btnClearSearchPosted.Tag = "Posted";
            this.btnClearSearchPosted.UseVisualStyleBackColor = true;
            this.btnClearSearchPosted.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label98
            // 
            this.label98.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label98.Dock = System.Windows.Forms.DockStyle.Left;
            this.label98.Location = new System.Drawing.Point(0, 0);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(1, 22);
            this.label98.TabIndex = 39;
            this.label98.Text = "label4";
            // 
            // label99
            // 
            this.label99.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label99.Dock = System.Windows.Forms.DockStyle.Right;
            this.label99.Location = new System.Drawing.Point(240, 0);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(1, 22);
            this.label99.TabIndex = 40;
            this.label99.Text = "label4";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 22);
            this.label16.TabIndex = 6;
            this.label16.Text = "Search :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(0, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 22);
            this.label17.TabIndex = 21;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(1253, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(12, 22);
            this.label18.TabIndex = 60;
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1265, 1);
            this.label19.TabIndex = 19;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label20.Location = new System.Drawing.Point(0, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1265, 1);
            this.label20.TabIndex = 20;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Right;
            this.label31.Location = new System.Drawing.Point(1265, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 24);
            this.label31.TabIndex = 22;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.label91);
            this.panel13.Controls.Add(this.cmbPosted);
            this.panel13.Controls.Add(this.btnShowChecksPosted);
            this.panel13.Controls.Add(this.mskToDatePosted);
            this.panel13.Controls.Add(this.mskFromDatePosted);
            this.panel13.Controls.Add(this.label58);
            this.panel13.Controls.Add(this.label59);
            this.panel13.Controls.Add(this.label60);
            this.panel13.Controls.Add(this.label61);
            this.panel13.Controls.Add(this.label62);
            this.panel13.Controls.Add(this.label63);
            this.panel13.Controls.Add(this.label64);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel13.Size = new System.Drawing.Size(1266, 37);
            this.panel13.TabIndex = 0;
            // 
            // label91
            // 
            this.label91.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label91.AutoSize = true;
            this.label91.BackColor = System.Drawing.Color.Transparent;
            this.label91.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label91.Location = new System.Drawing.Point(1068, 10);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(53, 14);
            this.label91.TabIndex = 85;
            this.label91.Text = "Posted :";
            this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPosted
            // 
            this.cmbPosted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPosted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPosted.FormattingEnabled = true;
            this.cmbPosted.Location = new System.Drawing.Point(1124, 6);
            this.cmbPosted.Name = "cmbPosted";
            this.cmbPosted.Size = new System.Drawing.Size(114, 22);
            this.cmbPosted.TabIndex = 83;
            this.cmbPosted.SelectedIndexChanged += new System.EventHandler(this.cmbPosted_SelectedIndexChanged);
            // 
            // btnShowChecksPosted
            // 
            this.btnShowChecksPosted.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.btnShowChecksPosted.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowChecksPosted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowChecksPosted.Image = ((System.Drawing.Image)(resources.GetObject("btnShowChecksPosted.Image")));
            this.btnShowChecksPosted.Location = new System.Drawing.Point(444, 6);
            this.btnShowChecksPosted.Name = "btnShowChecksPosted";
            this.btnShowChecksPosted.Size = new System.Drawing.Size(23, 22);
            this.btnShowChecksPosted.TabIndex = 28;
            this.btnShowChecksPosted.UseVisualStyleBackColor = true;
            this.btnShowChecksPosted.Click += new System.EventHandler(this.btnShowChecks_Click);
            // 
            // mskToDatePosted
            // 
            this.mskToDatePosted.Location = new System.Drawing.Point(355, 6);
            this.mskToDatePosted.Mask = "00/00/0000";
            this.mskToDatePosted.Name = "mskToDatePosted";
            this.mskToDatePosted.Size = new System.Drawing.Size(83, 22);
            this.mskToDatePosted.TabIndex = 1;
            this.mskToDatePosted.ValidatingType = typeof(System.DateTime);
            this.mskToDatePosted.MouseClick += new System.Windows.Forms.MouseEventHandler(this.All_MaskBox_MouseClick);
            // 
            // mskFromDatePosted
            // 
            this.mskFromDatePosted.Location = new System.Drawing.Point(237, 6);
            this.mskFromDatePosted.Mask = "00/00/0000";
            this.mskFromDatePosted.Name = "mskFromDatePosted";
            this.mskFromDatePosted.Size = new System.Drawing.Size(83, 22);
            this.mskFromDatePosted.TabIndex = 0;
            this.mskFromDatePosted.ValidatingType = typeof(System.DateTime);
            this.mskFromDatePosted.MouseClick += new System.Windows.Forms.MouseEventHandler(this.All_MaskBox_MouseClick);
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label58.Location = new System.Drawing.Point(1, 33);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(1264, 1);
            this.label58.TabIndex = 26;
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Top;
            this.label59.Location = new System.Drawing.Point(1, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1264, 1);
            this.label59.TabIndex = 25;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Right;
            this.label60.Location = new System.Drawing.Point(1265, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 34);
            this.label60.TabIndex = 24;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Left;
            this.label61.Location = new System.Drawing.Point(0, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1, 34);
            this.label61.TabIndex = 23;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Location = new System.Drawing.Point(22, 10);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(172, 14);
            this.label62.TabIndex = 27;
            this.label62.Text = "Select Check Date Range : ";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.BackColor = System.Drawing.Color.Transparent;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Location = new System.Drawing.Point(330, 10);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(22, 14);
            this.label63.TabIndex = 27;
            this.label63.Text = "To";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Location = new System.Drawing.Point(197, 10);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(38, 14);
            this.label64.TabIndex = 27;
            this.label64.Text = "From ";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbpg_ReadyToPost
            // 
            this.tbpg_ReadyToPost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_ReadyToPost.Controls.Add(this.panel8);
            this.tbpg_ReadyToPost.Controls.Add(this.panel3);
            this.tbpg_ReadyToPost.ImageIndex = 0;
            this.tbpg_ReadyToPost.Location = new System.Drawing.Point(4, 23);
            this.tbpg_ReadyToPost.Name = "tbpg_ReadyToPost";
            this.tbpg_ReadyToPost.Size = new System.Drawing.Size(1266, 429);
            this.tbpg_ReadyToPost.TabIndex = 0;
            this.tbpg_ReadyToPost.Tag = "ReadyToPost";
            this.tbpg_ReadyToPost.Text = "Ready to Post";
            this.tbpg_ReadyToPost.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.c1ReadyToPost);
            this.panel8.Controls.Add(this.panel16);
            this.panel8.Controls.Add(this.label40);
            this.panel8.Controls.Add(this.label41);
            this.panel8.Controls.Add(this.label42);
            this.panel8.Controls.Add(this.label43);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 25);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel8.Size = new System.Drawing.Size(1266, 404);
            this.panel8.TabIndex = 18;
            // 
            // c1ReadyToPost
            // 
            this.c1ReadyToPost.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ReadyToPost.BackColor = System.Drawing.Color.White;
            this.c1ReadyToPost.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ReadyToPost.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1ReadyToPost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1ReadyToPost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ReadyToPost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ReadyToPost.Location = new System.Drawing.Point(1, 4);
            this.c1ReadyToPost.Name = "c1ReadyToPost";
            this.c1ReadyToPost.Padding = new System.Windows.Forms.Padding(3);
            this.c1ReadyToPost.Rows.Count = 1;
            this.c1ReadyToPost.Rows.DefaultSize = 19;
            this.c1ReadyToPost.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.ScrollByRowColumn;
            this.c1ReadyToPost.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ReadyToPost.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1ReadyToPost.ShowCellLabels = true;
            this.c1ReadyToPost.Size = new System.Drawing.Size(1264, 379);
            this.c1ReadyToPost.StyleInfo = resources.GetString("c1ReadyToPost.StyleInfo");
            this.c1ReadyToPost.TabIndex = 26;
            this.c1ReadyToPost.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.C1All_AfterSort);
            this.c1ReadyToPost.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1ReadyToPost_AfterResizeColumn);
            this.c1ReadyToPost.AfterScroll += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1ReadyToPost_AfterScroll);
            this.c1ReadyToPost.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
            this.c1ReadyToPost.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_MouseClick);
            // 
            // panel16
            // 
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.c1ReadyToPostTotal);
            this.panel16.Controls.Add(this.label87);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel16.Location = new System.Drawing.Point(1, 383);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1264, 20);
            this.panel16.TabIndex = 29;
            // 
            // c1ReadyToPostTotal
            // 
            this.c1ReadyToPostTotal.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1ReadyToPostTotal.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
            this.c1ReadyToPostTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1ReadyToPostTotal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1ReadyToPostTotal.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1ReadyToPostTotal.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1ReadyToPostTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1ReadyToPostTotal.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1ReadyToPostTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1ReadyToPostTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1ReadyToPostTotal.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1ReadyToPostTotal.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1ReadyToPostTotal.Location = new System.Drawing.Point(0, 1);
            this.c1ReadyToPostTotal.Name = "c1ReadyToPostTotal";
            this.c1ReadyToPostTotal.Rows.Count = 1;
            this.c1ReadyToPostTotal.Rows.DefaultSize = 19;
            this.c1ReadyToPostTotal.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1ReadyToPostTotal.ShowCellLabels = true;
            this.c1ReadyToPostTotal.Size = new System.Drawing.Size(1264, 19);
            this.c1ReadyToPostTotal.StyleInfo = resources.GetString("c1ReadyToPostTotal.StyleInfo");
            this.c1ReadyToPostTotal.TabIndex = 25;
            this.c1ReadyToPostTotal.TabStop = false;
            // 
            // label87
            // 
            this.label87.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label87.Dock = System.Windows.Forms.DockStyle.Top;
            this.label87.Location = new System.Drawing.Point(0, 0);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(1264, 1);
            this.label87.TabIndex = 24;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label40.Location = new System.Drawing.Point(1, 403);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1264, 1);
            this.label40.TabIndex = 25;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Top;
            this.label41.Location = new System.Drawing.Point(1, 3);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(1264, 1);
            this.label41.TabIndex = 24;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Right;
            this.label42.Location = new System.Drawing.Point(1265, 3);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1, 401);
            this.label42.TabIndex = 23;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Location = new System.Drawing.Point(0, 3);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 401);
            this.label43.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.pnlCloseSearch);
            this.panel3.Controls.Add(this.lblReadyToPostSearch);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1266, 25);
            this.panel3.TabIndex = 17;
            // 
            // pnlCloseSearch
            // 
            this.pnlCloseSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlCloseSearch.Controls.Add(this.txtSearchReadyToPost);
            this.pnlCloseSearch.Controls.Add(this.label89);
            this.pnlCloseSearch.Controls.Add(this.label94);
            this.pnlCloseSearch.Controls.Add(this.label95);
            this.pnlCloseSearch.Controls.Add(this.btnClearSearchReadyToPost);
            this.pnlCloseSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlCloseSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlCloseSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCloseSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCloseSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlCloseSearch.Location = new System.Drawing.Point(66, 1);
            this.pnlCloseSearch.Name = "pnlCloseSearch";
            this.pnlCloseSearch.Size = new System.Drawing.Size(241, 23);
            this.pnlCloseSearch.TabIndex = 83;
            // 
            // txtSearchReadyToPost
            // 
            this.txtSearchReadyToPost.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchReadyToPost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchReadyToPost.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchReadyToPost.ForeColor = System.Drawing.Color.Black;
            this.txtSearchReadyToPost.Location = new System.Drawing.Point(5, 3);
            this.txtSearchReadyToPost.Name = "txtSearchReadyToPost";
            this.txtSearchReadyToPost.Size = new System.Drawing.Size(214, 15);
            this.txtSearchReadyToPost.TabIndex = 15;
            this.txtSearchReadyToPost.Tag = "ReadyToPost";
            this.txtSearchReadyToPost.TextChanged += new System.EventHandler(this.txtSearchAll_TextChanged);
            this.txtSearchReadyToPost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchAll_KeyDown);
            // 
            // label89
            // 
            this.label89.BackColor = System.Drawing.Color.White;
            this.label89.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label89.Location = new System.Drawing.Point(5, 18);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(214, 5);
            this.label89.TabIndex = 43;
            // 
            // label94
            // 
            this.label94.BackColor = System.Drawing.Color.White;
            this.label94.Dock = System.Windows.Forms.DockStyle.Top;
            this.label94.Location = new System.Drawing.Point(5, 0);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(214, 3);
            this.label94.TabIndex = 37;
            // 
            // label95
            // 
            this.label95.BackColor = System.Drawing.Color.White;
            this.label95.Dock = System.Windows.Forms.DockStyle.Left;
            this.label95.Location = new System.Drawing.Point(1, 0);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(4, 23);
            this.label95.TabIndex = 38;
            // 
            // btnClearSearchReadyToPost
            // 
            this.btnClearSearchReadyToPost.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearSearchReadyToPost.BackgroundImage")));
            this.btnClearSearchReadyToPost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearSearchReadyToPost.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearSearchReadyToPost.FlatAppearance.BorderSize = 0;
            this.btnClearSearchReadyToPost.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchReadyToPost.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchReadyToPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearchReadyToPost.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSearchReadyToPost.Image")));
            this.btnClearSearchReadyToPost.Location = new System.Drawing.Point(219, 0);
            this.btnClearSearchReadyToPost.Name = "btnClearSearchReadyToPost";
            this.btnClearSearchReadyToPost.Size = new System.Drawing.Size(21, 23);
            this.btnClearSearchReadyToPost.TabIndex = 41;
            this.btnClearSearchReadyToPost.Tag = "ReadyToPost";
            this.btnClearSearchReadyToPost.UseVisualStyleBackColor = true;
            this.btnClearSearchReadyToPost.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(240, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // lblReadyToPostSearch
            // 
            this.lblReadyToPostSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblReadyToPostSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblReadyToPostSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReadyToPostSearch.Location = new System.Drawing.Point(1, 1);
            this.lblReadyToPostSearch.Name = "lblReadyToPostSearch";
            this.lblReadyToPostSearch.Size = new System.Drawing.Size(65, 23);
            this.lblReadyToPostSearch.TabIndex = 6;
            this.lblReadyToPostSearch.Text = "Search :";
            this.lblReadyToPostSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 23);
            this.label8.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1253, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 23);
            this.label9.TabIndex = 60;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1265, 1);
            this.label10.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(0, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1265, 1);
            this.label11.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(1265, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 25);
            this.label12.TabIndex = 22;
            // 
            // tabERA
            // 
            this.tabERA.Controls.Add(this.tbpg_ReadyToPost);
            this.tabERA.Controls.Add(this.tbpg_Posted);
            this.tabERA.Controls.Add(this.tbpg_Hold);
            this.tabERA.Controls.Add(this.tbpg_Deleted);
            this.tabERA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabERA.ImageList = this.imgERA;
            this.tabERA.Location = new System.Drawing.Point(0, 0);
            this.tabERA.Name = "tabERA";
            this.tabERA.SelectedIndex = 0;
            this.tabERA.Size = new System.Drawing.Size(1274, 456);
            this.tabERA.TabIndex = 20;
            this.tabERA.SelectedIndexChanged += new System.EventHandler(this.tabERA_SelectedIndexChanged);
            // 
            // tbpg_Hold
            // 
            this.tbpg_Hold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_Hold.Controls.Add(this.panel1);
            this.tbpg_Hold.Controls.Add(this.panel2);
            this.tbpg_Hold.Controls.Add(this.panel7);
            this.tbpg_Hold.ImageIndex = 4;
            this.tbpg_Hold.Location = new System.Drawing.Point(4, 23);
            this.tbpg_Hold.Name = "tbpg_Hold";
            this.tbpg_Hold.Size = new System.Drawing.Size(1266, 429);
            this.tbpg_Hold.TabIndex = 3;
            this.tbpg_Hold.Tag = "Hold";
            this.tbpg_Hold.Text = "Hold";
            this.tbpg_Hold.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label34);
            this.panel1.Controls.Add(this.c1Hold);
            this.panel1.Controls.Add(this.label65);
            this.panel1.Controls.Add(this.label66);
            this.panel1.Controls.Add(this.label67);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 61);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(1266, 368);
            this.panel1.TabIndex = 1;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 367);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1264, 1);
            this.label34.TabIndex = 25;
            // 
            // c1Hold
            // 
            this.c1Hold.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Hold.BackColor = System.Drawing.Color.White;
            this.c1Hold.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Hold.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1Hold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Hold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Hold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Hold.Location = new System.Drawing.Point(1, 4);
            this.c1Hold.Name = "c1Hold";
            this.c1Hold.Padding = new System.Windows.Forms.Padding(3);
            this.c1Hold.Rows.Count = 1;
            this.c1Hold.Rows.DefaultSize = 19;
            this.c1Hold.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Hold.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Hold.ShowCellLabels = true;
            this.c1Hold.Size = new System.Drawing.Size(1264, 364);
            this.c1Hold.StyleInfo = resources.GetString("c1Hold.StyleInfo");
            this.c1Hold.TabIndex = 0;
            this.c1Hold.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.C1All_AfterSort);
            this.c1Hold.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
            this.c1Hold.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_MouseClick);
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label65.Dock = System.Windows.Forms.DockStyle.Top;
            this.label65.Location = new System.Drawing.Point(1, 3);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(1264, 1);
            this.label65.TabIndex = 24;
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label66.Dock = System.Windows.Forms.DockStyle.Right;
            this.label66.Location = new System.Drawing.Point(1265, 3);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(1, 365);
            this.label66.TabIndex = 23;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Dock = System.Windows.Forms.DockStyle.Left;
            this.label67.Location = new System.Drawing.Point(0, 3);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(1, 365);
            this.label67.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1266, 24);
            this.panel2.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.panel20);
            this.panel6.Controls.Add(this.label69);
            this.panel6.Controls.Add(this.label72);
            this.panel6.Controls.Add(this.label77);
            this.panel6.Controls.Add(this.label78);
            this.panel6.Controls.Add(this.label79);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1266, 24);
            this.panel6.TabIndex = 0;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.Transparent;
            this.panel20.Controls.Add(this.txtSearchHold);
            this.panel20.Controls.Add(this.label2);
            this.panel20.Controls.Add(this.label100);
            this.panel20.Controls.Add(this.label101);
            this.panel20.Controls.Add(this.btnClearSearchHold);
            this.panel20.Controls.Add(this.label102);
            this.panel20.Controls.Add(this.label103);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel20.ForeColor = System.Drawing.Color.Black;
            this.panel20.Location = new System.Drawing.Point(66, 1);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(241, 22);
            this.panel20.TabIndex = 85;
            // 
            // txtSearchHold
            // 
            this.txtSearchHold.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchHold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchHold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchHold.ForeColor = System.Drawing.Color.Black;
            this.txtSearchHold.Location = new System.Drawing.Point(5, 3);
            this.txtSearchHold.Name = "txtSearchHold";
            this.txtSearchHold.Size = new System.Drawing.Size(214, 15);
            this.txtSearchHold.TabIndex = 0;
            this.txtSearchHold.Tag = "Deleted";
            this.txtSearchHold.TextChanged += new System.EventHandler(this.txtSearchAll_TextChanged);
            this.txtSearchHold.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchAll_KeyDown);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(5, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 5);
            this.label2.TabIndex = 43;
            // 
            // label100
            // 
            this.label100.BackColor = System.Drawing.Color.White;
            this.label100.Dock = System.Windows.Forms.DockStyle.Top;
            this.label100.Location = new System.Drawing.Point(5, 0);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(214, 3);
            this.label100.TabIndex = 37;
            // 
            // label101
            // 
            this.label101.BackColor = System.Drawing.Color.White;
            this.label101.Dock = System.Windows.Forms.DockStyle.Left;
            this.label101.Location = new System.Drawing.Point(1, 0);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(4, 22);
            this.label101.TabIndex = 38;
            // 
            // btnClearSearchHold
            // 
            this.btnClearSearchHold.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearSearchHold.BackgroundImage")));
            this.btnClearSearchHold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearSearchHold.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearSearchHold.FlatAppearance.BorderSize = 0;
            this.btnClearSearchHold.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchHold.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchHold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearchHold.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSearchHold.Image")));
            this.btnClearSearchHold.Location = new System.Drawing.Point(219, 0);
            this.btnClearSearchHold.Name = "btnClearSearchHold";
            this.btnClearSearchHold.Size = new System.Drawing.Size(21, 22);
            this.btnClearSearchHold.TabIndex = 41;
            this.btnClearSearchHold.Tag = "Hold";
            this.btnClearSearchHold.UseVisualStyleBackColor = true;
            this.btnClearSearchHold.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label102
            // 
            this.label102.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label102.Dock = System.Windows.Forms.DockStyle.Left;
            this.label102.Location = new System.Drawing.Point(0, 0);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(1, 22);
            this.label102.TabIndex = 39;
            this.label102.Text = "label4";
            // 
            // label103
            // 
            this.label103.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label103.Dock = System.Windows.Forms.DockStyle.Right;
            this.label103.Location = new System.Drawing.Point(240, 0);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(1, 22);
            this.label103.TabIndex = 40;
            this.label103.Text = "label4";
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.Transparent;
            this.label69.Dock = System.Windows.Forms.DockStyle.Left;
            this.label69.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(1, 1);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(65, 22);
            this.label69.TabIndex = 6;
            this.label69.Text = "Search :";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Left;
            this.label72.Location = new System.Drawing.Point(0, 1);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(1, 22);
            this.label72.TabIndex = 21;
            // 
            // label77
            // 
            this.label77.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label77.Dock = System.Windows.Forms.DockStyle.Top;
            this.label77.Location = new System.Drawing.Point(0, 0);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(1265, 1);
            this.label77.TabIndex = 19;
            // 
            // label78
            // 
            this.label78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label78.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label78.Location = new System.Drawing.Point(0, 23);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(1265, 1);
            this.label78.TabIndex = 20;
            // 
            // label79
            // 
            this.label79.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label79.Dock = System.Windows.Forms.DockStyle.Right;
            this.label79.Location = new System.Drawing.Point(1265, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(1, 24);
            this.label79.TabIndex = 22;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnShowChecksHold);
            this.panel7.Controls.Add(this.mskToDateHold);
            this.panel7.Controls.Add(this.mskFromDateHold);
            this.panel7.Controls.Add(this.label80);
            this.panel7.Controls.Add(this.label81);
            this.panel7.Controls.Add(this.label82);
            this.panel7.Controls.Add(this.label83);
            this.panel7.Controls.Add(this.label84);
            this.panel7.Controls.Add(this.label85);
            this.panel7.Controls.Add(this.label86);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel7.Size = new System.Drawing.Size(1266, 37);
            this.panel7.TabIndex = 3;
            // 
            // btnShowChecksHold
            // 
            this.btnShowChecksHold.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.btnShowChecksHold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowChecksHold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowChecksHold.Image = ((System.Drawing.Image)(resources.GetObject("btnShowChecksHold.Image")));
            this.btnShowChecksHold.Location = new System.Drawing.Point(444, 6);
            this.btnShowChecksHold.Name = "btnShowChecksHold";
            this.btnShowChecksHold.Size = new System.Drawing.Size(23, 22);
            this.btnShowChecksHold.TabIndex = 32;
            this.btnShowChecksHold.UseVisualStyleBackColor = true;
            this.btnShowChecksHold.Click += new System.EventHandler(this.btnShowChecks_Click);
            // 
            // mskToDateHold
            // 
            this.mskToDateHold.Location = new System.Drawing.Point(355, 6);
            this.mskToDateHold.Mask = "00/00/0000";
            this.mskToDateHold.Name = "mskToDateHold";
            this.mskToDateHold.Size = new System.Drawing.Size(83, 22);
            this.mskToDateHold.TabIndex = 1;
            this.mskToDateHold.ValidatingType = typeof(System.DateTime);
            this.mskToDateHold.MouseClick += new System.Windows.Forms.MouseEventHandler(this.All_MaskBox_MouseClick);
            // 
            // mskFromDateHold
            // 
            this.mskFromDateHold.Location = new System.Drawing.Point(237, 6);
            this.mskFromDateHold.Mask = "00/00/0000";
            this.mskFromDateHold.Name = "mskFromDateHold";
            this.mskFromDateHold.Size = new System.Drawing.Size(83, 22);
            this.mskFromDateHold.TabIndex = 0;
            this.mskFromDateHold.ValidatingType = typeof(System.DateTime);
            this.mskFromDateHold.MouseClick += new System.Windows.Forms.MouseEventHandler(this.All_MaskBox_MouseClick);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.BackColor = System.Drawing.Color.Transparent;
            this.label80.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label80.Location = new System.Drawing.Point(330, 10);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(22, 14);
            this.label80.TabIndex = 30;
            this.label80.Text = "To";
            this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.BackColor = System.Drawing.Color.Transparent;
            this.label81.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label81.Location = new System.Drawing.Point(197, 10);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(38, 14);
            this.label81.TabIndex = 31;
            this.label81.Text = "From ";
            this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label82
            // 
            this.label82.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label82.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label82.Location = new System.Drawing.Point(1, 33);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(1264, 1);
            this.label82.TabIndex = 26;
            // 
            // label83
            // 
            this.label83.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label83.Dock = System.Windows.Forms.DockStyle.Top;
            this.label83.Location = new System.Drawing.Point(1, 0);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(1264, 1);
            this.label83.TabIndex = 25;
            // 
            // label84
            // 
            this.label84.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label84.Dock = System.Windows.Forms.DockStyle.Right;
            this.label84.Location = new System.Drawing.Point(1265, 0);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(1, 34);
            this.label84.TabIndex = 24;
            // 
            // label85
            // 
            this.label85.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label85.Dock = System.Windows.Forms.DockStyle.Left;
            this.label85.Location = new System.Drawing.Point(0, 0);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(1, 34);
            this.label85.TabIndex = 23;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.BackColor = System.Drawing.Color.Transparent;
            this.label86.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label86.Location = new System.Drawing.Point(22, 10);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(172, 14);
            this.label86.TabIndex = 27;
            this.label86.Text = "Select Check Date Range : ";
            this.label86.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbpg_Deleted
            // 
            this.tbpg_Deleted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_Deleted.Controls.Add(this.panel12);
            this.tbpg_Deleted.Controls.Add(this.panel14);
            this.tbpg_Deleted.Controls.Add(this.panel10);
            this.tbpg_Deleted.ImageIndex = 1;
            this.tbpg_Deleted.Location = new System.Drawing.Point(4, 23);
            this.tbpg_Deleted.Name = "tbpg_Deleted";
            this.tbpg_Deleted.Size = new System.Drawing.Size(1266, 429);
            this.tbpg_Deleted.TabIndex = 2;
            this.tbpg_Deleted.Tag = "Deleted";
            this.tbpg_Deleted.Text = "Deleted";
            this.tbpg_Deleted.UseVisualStyleBackColor = true;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label54);
            this.panel12.Controls.Add(this.c1Deleted);
            this.panel12.Controls.Add(this.label55);
            this.panel12.Controls.Add(this.label56);
            this.panel12.Controls.Add(this.label57);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 61);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel12.Size = new System.Drawing.Size(1266, 368);
            this.panel12.TabIndex = 2;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label54.Location = new System.Drawing.Point(1, 367);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1264, 1);
            this.label54.TabIndex = 25;
            // 
            // c1Deleted
            // 
            this.c1Deleted.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Deleted.BackColor = System.Drawing.Color.White;
            this.c1Deleted.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Deleted.ColumnInfo = "1,0,0,0,0,95,Columns:";
            this.c1Deleted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Deleted.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Deleted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Deleted.Location = new System.Drawing.Point(1, 4);
            this.c1Deleted.Name = "c1Deleted";
            this.c1Deleted.Padding = new System.Windows.Forms.Padding(3);
            this.c1Deleted.Rows.Count = 1;
            this.c1Deleted.Rows.DefaultSize = 19;
            this.c1Deleted.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Deleted.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1Deleted.ShowCellLabels = true;
            this.c1Deleted.Size = new System.Drawing.Size(1264, 364);
            this.c1Deleted.StyleInfo = resources.GetString("c1Deleted.StyleInfo");
            this.c1Deleted.TabIndex = 2;
            this.c1Deleted.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.C1All_AfterSort);
            this.c1Deleted.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.C1All_AfterRowColChange);
            this.c1Deleted.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1FlexGrid_MouseClick);
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Top;
            this.label55.Location = new System.Drawing.Point(1, 3);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1264, 1);
            this.label55.TabIndex = 24;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Right;
            this.label56.Location = new System.Drawing.Point(1265, 3);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1, 365);
            this.label56.TabIndex = 23;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Left;
            this.label57.Location = new System.Drawing.Point(0, 3);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(1, 365);
            this.label57.TabIndex = 22;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel11);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(0, 37);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1266, 24);
            this.panel14.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Controls.Add(this.pnlSearch);
            this.panel11.Controls.Add(this.label48);
            this.panel11.Controls.Add(this.label49);
            this.panel11.Controls.Add(this.label51);
            this.panel11.Controls.Add(this.label52);
            this.panel11.Controls.Add(this.label53);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1266, 24);
            this.panel11.TabIndex = 0;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.Controls.Add(this.txtSearchDeleted);
            this.pnlSearch.Controls.Add(this.label68);
            this.pnlSearch.Controls.Add(this.label104);
            this.pnlSearch.Controls.Add(this.label105);
            this.pnlSearch.Controls.Add(this.btnClearSearchDeleted);
            this.pnlSearch.Controls.Add(this.label106);
            this.pnlSearch.Controls.Add(this.label107);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSearch.Location = new System.Drawing.Point(66, 1);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(241, 22);
            this.pnlSearch.TabIndex = 86;
            // 
            // txtSearchDeleted
            // 
            this.txtSearchDeleted.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchDeleted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchDeleted.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchDeleted.ForeColor = System.Drawing.Color.Black;
            this.txtSearchDeleted.Location = new System.Drawing.Point(5, 3);
            this.txtSearchDeleted.Name = "txtSearchDeleted";
            this.txtSearchDeleted.Size = new System.Drawing.Size(214, 15);
            this.txtSearchDeleted.TabIndex = 0;
            this.txtSearchDeleted.Tag = "Deleted";
            this.txtSearchDeleted.TextChanged += new System.EventHandler(this.txtSearchAll_TextChanged);
            this.txtSearchDeleted.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchAll_KeyDown);
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.White;
            this.label68.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label68.Location = new System.Drawing.Point(5, 17);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(214, 5);
            this.label68.TabIndex = 43;
            // 
            // label104
            // 
            this.label104.BackColor = System.Drawing.Color.White;
            this.label104.Dock = System.Windows.Forms.DockStyle.Top;
            this.label104.Location = new System.Drawing.Point(5, 0);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(214, 3);
            this.label104.TabIndex = 37;
            // 
            // label105
            // 
            this.label105.BackColor = System.Drawing.Color.White;
            this.label105.Dock = System.Windows.Forms.DockStyle.Left;
            this.label105.Location = new System.Drawing.Point(1, 0);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(4, 22);
            this.label105.TabIndex = 38;
            // 
            // btnClearSearchDeleted
            // 
            this.btnClearSearchDeleted.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearSearchDeleted.BackgroundImage")));
            this.btnClearSearchDeleted.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearSearchDeleted.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearSearchDeleted.FlatAppearance.BorderSize = 0;
            this.btnClearSearchDeleted.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchDeleted.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchDeleted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearchDeleted.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSearchDeleted.Image")));
            this.btnClearSearchDeleted.Location = new System.Drawing.Point(219, 0);
            this.btnClearSearchDeleted.Name = "btnClearSearchDeleted";
            this.btnClearSearchDeleted.Size = new System.Drawing.Size(21, 22);
            this.btnClearSearchDeleted.TabIndex = 41;
            this.btnClearSearchDeleted.Tag = "Deleted";
            this.btnClearSearchDeleted.UseVisualStyleBackColor = true;
            this.btnClearSearchDeleted.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label106
            // 
            this.label106.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label106.Dock = System.Windows.Forms.DockStyle.Left;
            this.label106.Location = new System.Drawing.Point(0, 0);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(1, 22);
            this.label106.TabIndex = 39;
            this.label106.Text = "label4";
            // 
            // label107
            // 
            this.label107.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label107.Dock = System.Windows.Forms.DockStyle.Right;
            this.label107.Location = new System.Drawing.Point(240, 0);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(1, 22);
            this.label107.TabIndex = 40;
            this.label107.Text = "label4";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.label48.Dock = System.Windows.Forms.DockStyle.Left;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(1, 1);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(65, 22);
            this.label48.TabIndex = 6;
            this.label48.Text = "Search :";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Left;
            this.label49.Location = new System.Drawing.Point(0, 1);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(1, 22);
            this.label49.TabIndex = 21;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Top;
            this.label51.Location = new System.Drawing.Point(0, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(1265, 1);
            this.label51.TabIndex = 19;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label52.Location = new System.Drawing.Point(0, 23);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1265, 1);
            this.label52.TabIndex = 20;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(1265, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 24);
            this.label53.TabIndex = 22;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btnShowChecksDeleted);
            this.panel10.Controls.Add(this.mskToDateDeleted);
            this.panel10.Controls.Add(this.mskFromDateDeletd);
            this.panel10.Controls.Add(this.label23);
            this.panel10.Controls.Add(this.label33);
            this.panel10.Controls.Add(this.label38);
            this.panel10.Controls.Add(this.label37);
            this.panel10.Controls.Add(this.label36);
            this.panel10.Controls.Add(this.label35);
            this.panel10.Controls.Add(this.label32);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel10.Size = new System.Drawing.Size(1266, 37);
            this.panel10.TabIndex = 0;
            // 
            // btnShowChecksDeleted
            // 
            this.btnShowChecksDeleted.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.btnShowChecksDeleted.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowChecksDeleted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowChecksDeleted.Image = ((System.Drawing.Image)(resources.GetObject("btnShowChecksDeleted.Image")));
            this.btnShowChecksDeleted.Location = new System.Drawing.Point(444, 6);
            this.btnShowChecksDeleted.Name = "btnShowChecksDeleted";
            this.btnShowChecksDeleted.Size = new System.Drawing.Size(23, 22);
            this.btnShowChecksDeleted.TabIndex = 32;
            this.btnShowChecksDeleted.UseVisualStyleBackColor = true;
            this.btnShowChecksDeleted.Click += new System.EventHandler(this.btnShowChecks_Click);
            // 
            // mskToDateDeleted
            // 
            this.mskToDateDeleted.Location = new System.Drawing.Point(355, 6);
            this.mskToDateDeleted.Mask = "00/00/0000";
            this.mskToDateDeleted.Name = "mskToDateDeleted";
            this.mskToDateDeleted.Size = new System.Drawing.Size(83, 22);
            this.mskToDateDeleted.TabIndex = 1;
            this.mskToDateDeleted.ValidatingType = typeof(System.DateTime);
            this.mskToDateDeleted.MouseClick += new System.Windows.Forms.MouseEventHandler(this.All_MaskBox_MouseClick);
            // 
            // mskFromDateDeletd
            // 
            this.mskFromDateDeletd.Location = new System.Drawing.Point(237, 6);
            this.mskFromDateDeletd.Mask = "00/00/0000";
            this.mskFromDateDeletd.Name = "mskFromDateDeletd";
            this.mskFromDateDeletd.Size = new System.Drawing.Size(83, 22);
            this.mskFromDateDeletd.TabIndex = 0;
            this.mskFromDateDeletd.ValidatingType = typeof(System.DateTime);
            this.mskFromDateDeletd.MouseClick += new System.Windows.Forms.MouseEventHandler(this.All_MaskBox_MouseClick);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Location = new System.Drawing.Point(330, 10);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(22, 14);
            this.label23.TabIndex = 30;
            this.label23.Text = "To";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Location = new System.Drawing.Point(197, 10);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(38, 14);
            this.label33.TabIndex = 31;
            this.label33.Text = "From ";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label38.Location = new System.Drawing.Point(1, 33);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1264, 1);
            this.label38.TabIndex = 26;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Location = new System.Drawing.Point(1, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(1264, 1);
            this.label37.TabIndex = 25;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Right;
            this.label36.Location = new System.Drawing.Point(1265, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 34);
            this.label36.TabIndex = 24;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Left;
            this.label35.Location = new System.Drawing.Point(0, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 34);
            this.label35.TabIndex = 23;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Location = new System.Drawing.Point(22, 10);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(172, 14);
            this.label32.TabIndex = 27;
            this.label32.Text = "Select Check Date Range : ";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // imgERA
            // 
            this.imgERA.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgERA.ImageStream")));
            this.imgERA.TransparentColor = System.Drawing.Color.Transparent;
            this.imgERA.Images.SetKeyName(0, "Ready to Post.ico");
            this.imgERA.Images.SetKeyName(1, "Marked Deleted.ico");
            this.imgERA.Images.SetKeyName(2, "Posted.ico");
            this.imgERA.Images.SetKeyName(3, "Clearing House01.ico");
            this.imgERA.Images.SetKeyName(4, "Hold check.ico");
            // 
            // pnlTab
            // 
            this.pnlTab.Controls.Add(this.tabERA);
            this.pnlTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab.Location = new System.Drawing.Point(0, 56);
            this.pnlTab.Name = "pnlTab";
            this.pnlTab.Size = new System.Drawing.Size(1274, 456);
            this.pnlTab.TabIndex = 21;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.btnDown);
            this.panel5.Controls.Add(this.btnUP);
            this.panel5.Controls.Add(this.label21);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1268, 24);
            this.panel5.TabIndex = 21;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.ForeColor = System.Drawing.Color.Transparent;
            this.btnDown.Image = global::gloBilling.Properties.Resources.Down;
            this.btnDown.Location = new System.Drawing.Point(1219, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(24, 22);
            this.btnDown.TabIndex = 28;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnDown.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnUP
            // 
            this.btnUP.BackColor = System.Drawing.Color.Transparent;
            this.btnUP.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUP.FlatAppearance.BorderSize = 0;
            this.btnUP.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUP.ForeColor = System.Drawing.Color.Transparent;
            this.btnUP.Image = global::gloBilling.Properties.Resources.UP;
            this.btnUP.Location = new System.Drawing.Point(1243, 1);
            this.btnUP.Name = "btnUP";
            this.btnUP.Size = new System.Drawing.Size(24, 22);
            this.btnUP.TabIndex = 27;
            this.btnUP.UseVisualStyleBackColor = false;
            this.btnUP.Click += new System.EventHandler(this.btnUP_Click);
            this.btnUP.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnUP.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Location = new System.Drawing.Point(1, 1);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(236, 22);
            this.label21.TabIndex = 26;
            this.label21.Text = "Additional ERA Check Information  :";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(1, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1266, 1);
            this.label7.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1266, 1);
            this.label13.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Location = new System.Drawing.Point(1267, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 24);
            this.label14.TabIndex = 23;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Left;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 24);
            this.label15.TabIndex = 22;
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProgress.Location = new System.Drawing.Point(294, 1);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(523, 22);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Text = "label23";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prgProgress
            // 
            this.prgProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.prgProgress.Location = new System.Drawing.Point(817, 1);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Size = new System.Drawing.Size(450, 22);
            this.prgProgress.TabIndex = 1;
            // 
            // pnlAdditionalCheckInfo
            // 
            this.pnlAdditionalCheckInfo.Controls.Add(this.lblPayerContact);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label26);
            this.pnlAdditionalCheckInfo.Controls.Add(this.lblPaymentBalanced);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label71);
            this.pnlAdditionalCheckInfo.Controls.Add(this.lblTotalPLBAmount);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label29);
            this.pnlAdditionalCheckInfo.Controls.Add(this.lblTotalClaimPaid);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label28);
            this.pnlAdditionalCheckInfo.Controls.Add(this.lblOriginalFileName);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label70);
            this.pnlAdditionalCheckInfo.Controls.Add(this.lblProductionDate);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label22);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label3);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label4);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label5);
            this.pnlAdditionalCheckInfo.Controls.Add(this.label6);
            this.pnlAdditionalCheckInfo.Controls.Add(this.pnlPostInfo);
            this.pnlAdditionalCheckInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAdditionalCheckInfo.Location = new System.Drawing.Point(0, 539);
            this.pnlAdditionalCheckInfo.Name = "pnlAdditionalCheckInfo";
            this.pnlAdditionalCheckInfo.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlAdditionalCheckInfo.Size = new System.Drawing.Size(1274, 88);
            this.pnlAdditionalCheckInfo.TabIndex = 21;
            // 
            // lblPayerContact
            // 
            this.lblPayerContact.AutoSize = true;
            this.lblPayerContact.BackColor = System.Drawing.Color.Transparent;
            this.lblPayerContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayerContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayerContact.Location = new System.Drawing.Point(152, 60);
            this.lblPayerContact.Name = "lblPayerContact";
            this.lblPayerContact.Size = new System.Drawing.Size(92, 14);
            this.lblPayerContact.TabIndex = 27;
            this.lblPayerContact.Text = "Payer Contact :";
            this.lblPayerContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Location = new System.Drawing.Point(50, 60);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(102, 14);
            this.label26.TabIndex = 27;
            this.label26.Text = "Payer Contact :";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaymentBalanced
            // 
            this.lblPaymentBalanced.AutoSize = true;
            this.lblPaymentBalanced.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentBalanced.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentBalanced.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPaymentBalanced.Location = new System.Drawing.Point(639, 60);
            this.lblPaymentBalanced.Name = "lblPaymentBalanced";
            this.lblPaymentBalanced.Size = new System.Drawing.Size(115, 14);
            this.lblPaymentBalanced.TabIndex = 27;
            this.lblPaymentBalanced.Text = "Payment Balanced :";
            this.lblPaymentBalanced.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.BackColor = System.Drawing.Color.Transparent;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Location = new System.Drawing.Point(511, 60);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(127, 14);
            this.label71.TabIndex = 27;
            this.label71.Text = "Payment Balanced :";
            this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalPLBAmount
            // 
            this.lblTotalPLBAmount.AutoSize = true;
            this.lblTotalPLBAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalPLBAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPLBAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTotalPLBAmount.Location = new System.Drawing.Point(639, 34);
            this.lblTotalPLBAmount.Name = "lblTotalPLBAmount";
            this.lblTotalPLBAmount.Size = new System.Drawing.Size(113, 14);
            this.lblTotalPLBAmount.TabIndex = 27;
            this.lblTotalPLBAmount.Text = "Total PLB amount :";
            this.lblTotalPLBAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Location = new System.Drawing.Point(513, 34);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(125, 14);
            this.label29.TabIndex = 27;
            this.label29.Text = "Total PLB amount :";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalClaimPaid
            // 
            this.lblTotalClaimPaid.AutoSize = true;
            this.lblTotalClaimPaid.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalClaimPaid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalClaimPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTotalClaimPaid.Location = new System.Drawing.Point(639, 8);
            this.lblTotalClaimPaid.Name = "lblTotalClaimPaid";
            this.lblTotalClaimPaid.Size = new System.Drawing.Size(100, 14);
            this.lblTotalClaimPaid.TabIndex = 27;
            this.lblTotalClaimPaid.Text = "Total Claim Paid :";
            this.lblTotalClaimPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Location = new System.Drawing.Point(526, 8);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(112, 14);
            this.label28.TabIndex = 27;
            this.label28.Text = "Total Claim Paid :";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOriginalFileName
            // 
            this.lblOriginalFileName.AutoSize = true;
            this.lblOriginalFileName.BackColor = System.Drawing.Color.Transparent;
            this.lblOriginalFileName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOriginalFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOriginalFileName.Location = new System.Drawing.Point(152, 8);
            this.lblOriginalFileName.Name = "lblOriginalFileName";
            this.lblOriginalFileName.Size = new System.Drawing.Size(110, 14);
            this.lblOriginalFileName.TabIndex = 27;
            this.lblOriginalFileName.Text = "Original File Name :";
            this.lblOriginalFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.BackColor = System.Drawing.Color.Transparent;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Location = new System.Drawing.Point(31, 8);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(121, 14);
            this.label70.TabIndex = 27;
            this.label70.Text = "Original File Name :";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProductionDate
            // 
            this.lblProductionDate.AutoSize = true;
            this.lblProductionDate.BackColor = System.Drawing.Color.Transparent;
            this.lblProductionDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductionDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProductionDate.Location = new System.Drawing.Point(152, 34);
            this.lblProductionDate.Name = "lblProductionDate";
            this.lblProductionDate.Size = new System.Drawing.Size(104, 14);
            this.lblProductionDate.TabIndex = 27;
            this.lblProductionDate.Text = "Production Date :";
            this.lblProductionDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Location = new System.Drawing.Point(36, 34);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(116, 14);
            this.label22.TabIndex = 27;
            this.label22.Text = "Production Date :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(4, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1266, 1);
            this.label3.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1266, 1);
            this.label4.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(1270, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 85);
            this.label5.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 85);
            this.label6.TabIndex = 22;
            // 
            // pnlPostInfo
            // 
            this.pnlPostInfo.Controls.Add(this.label92);
            this.pnlPostInfo.Controls.Add(this.lblUser);
            this.pnlPostInfo.Controls.Add(this.lblCloseDate);
            this.pnlPostInfo.Controls.Add(this.label88);
            this.pnlPostInfo.Controls.Add(this.label90);
            this.pnlPostInfo.Controls.Add(this.lblPayTray);
            this.pnlPostInfo.Location = new System.Drawing.Point(860, 4);
            this.pnlPostInfo.Name = "pnlPostInfo";
            this.pnlPostInfo.Size = new System.Drawing.Size(374, 77);
            this.pnlPostInfo.TabIndex = 34;
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.BackColor = System.Drawing.Color.Transparent;
            this.label92.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label92.Location = new System.Drawing.Point(23, 4);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(80, 14);
            this.label92.TabIndex = 30;
            this.label92.Text = "Close Date :";
            this.label92.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblUser.Location = new System.Drawing.Point(103, 56);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(31, 14);
            this.lblUser.TabIndex = 31;
            this.lblUser.Text = "User";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCloseDate
            // 
            this.lblCloseDate.AutoSize = true;
            this.lblCloseDate.BackColor = System.Drawing.Color.Transparent;
            this.lblCloseDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCloseDate.Location = new System.Drawing.Point(103, 4);
            this.lblCloseDate.Name = "lblCloseDate";
            this.lblCloseDate.Size = new System.Drawing.Size(65, 14);
            this.lblCloseDate.TabIndex = 29;
            this.lblCloseDate.Text = "Close Date";
            this.lblCloseDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.BackColor = System.Drawing.Color.Transparent;
            this.label88.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label88.Location = new System.Drawing.Point(62, 56);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(41, 14);
            this.label88.TabIndex = 32;
            this.label88.Text = "User :";
            this.label88.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label90.Location = new System.Drawing.Point(4, 30);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(99, 14);
            this.label90.TabIndex = 28;
            this.label90.Text = "Payment Tray :";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPayTray
            // 
            this.lblPayTray.AutoSize = true;
            this.lblPayTray.BackColor = System.Drawing.Color.Transparent;
            this.lblPayTray.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPayTray.Location = new System.Drawing.Point(103, 30);
            this.lblPayTray.Name = "lblPayTray";
            this.lblPayTray.Size = new System.Drawing.Size(83, 14);
            this.lblPayTray.TabIndex = 33;
            this.lblPayTray.Text = "Payment Tray";
            this.lblPayTray.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlAddCheckInfoHeader
            // 
            this.pnlAddCheckInfoHeader.Controls.Add(this.panel5);
            this.pnlAddCheckInfoHeader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAddCheckInfoHeader.Location = new System.Drawing.Point(0, 512);
            this.pnlAddCheckInfoHeader.Name = "pnlAddCheckInfoHeader";
            this.pnlAddCheckInfoHeader.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlAddCheckInfoHeader.Size = new System.Drawing.Size(1274, 27);
            this.pnlAddCheckInfoHeader.TabIndex = 22;
            // 
            // pnlMessagetxtbox
            // 
            this.pnlMessagetxtbox.Controls.Add(this.txtMessage);
            this.pnlMessagetxtbox.Controls.Add(this.label73);
            this.pnlMessagetxtbox.Controls.Add(this.label74);
            this.pnlMessagetxtbox.Controls.Add(this.label75);
            this.pnlMessagetxtbox.Controls.Add(this.label76);
            this.pnlMessagetxtbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMessagetxtbox.Location = new System.Drawing.Point(0, 651);
            this.pnlMessagetxtbox.Name = "pnlMessagetxtbox";
            this.pnlMessagetxtbox.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMessagetxtbox.Size = new System.Drawing.Size(1274, 85);
            this.pnlMessagetxtbox.TabIndex = 23;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.White;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(4, 4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(1266, 77);
            this.txtMessage.TabIndex = 26;
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label73.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label73.Location = new System.Drawing.Point(4, 81);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(1266, 1);
            this.label73.TabIndex = 25;
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Dock = System.Windows.Forms.DockStyle.Top;
            this.label74.Location = new System.Drawing.Point(4, 3);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(1266, 1);
            this.label74.TabIndex = 24;
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label75.Dock = System.Windows.Forms.DockStyle.Right;
            this.label75.Location = new System.Drawing.Point(1270, 3);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(1, 79);
            this.label75.TabIndex = 23;
            // 
            // label76
            // 
            this.label76.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label76.Dock = System.Windows.Forms.DockStyle.Left;
            this.label76.Location = new System.Drawing.Point(3, 3);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(1, 79);
            this.label76.TabIndex = 22;
            // 
            // pnlMessage
            // 
            this.pnlMessage.Controls.Add(this.panel18);
            this.pnlMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMessage.Location = new System.Drawing.Point(0, 627);
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlMessage.Size = new System.Drawing.Size(1274, 24);
            this.pnlMessage.TabIndex = 24;
            // 
            // panel18
            // 
            this.panel18.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel18.Controls.Add(this.lblProgress);
            this.panel18.Controls.Add(this.label24);
            this.panel18.Controls.Add(this.prgProgress);
            this.panel18.Controls.Add(this.label25);
            this.panel18.Controls.Add(this.label27);
            this.panel18.Controls.Add(this.label30);
            this.panel18.Controls.Add(this.label50);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel18.Location = new System.Drawing.Point(3, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(1268, 24);
            this.panel18.TabIndex = 21;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Location = new System.Drawing.Point(1, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(73, 22);
            this.label24.TabIndex = 26;
            this.label24.Text = " Message :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Location = new System.Drawing.Point(1, 23);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1266, 1);
            this.label25.TabIndex = 25;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Location = new System.Drawing.Point(1, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1266, 1);
            this.label27.TabIndex = 24;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Right;
            this.label30.Location = new System.Drawing.Point(1267, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 24);
            this.label30.TabIndex = 23;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Left;
            this.label50.Location = new System.Drawing.Point(0, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(1, 24);
            this.label50.TabIndex = 22;
            // 
            // frmERAPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1274, 736);
            this.Controls.Add(this.pnlTab);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.pnlAddCheckInfoHeader);
            this.Controls.Add(this.pnlAdditionalCheckInfo);
            this.Controls.Add(this.pnlMessage);
            this.Controls.Add(this.pnlMessagetxtbox);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmERAPayment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ERA Payment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmERAPayment_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmERAPayment_FormClosed);
            this.Load += new System.EventHandler(this.frmERAPayment_Load);
            this.Shown += new System.EventHandler(this.frmERAPayment_Shown);
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.tls_Main.ResumeLayout(false);
            this.tls_Main.PerformLayout();
            this.tbpg_Posted.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Posted)).EndInit();
            this.panel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PostedTotal)).EndInit();
            this.panel15.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.tbpg_ReadyToPost.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ReadyToPost)).EndInit();
            this.panel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1ReadyToPostTotal)).EndInit();
            this.panel3.ResumeLayout(false);
            this.pnlCloseSearch.ResumeLayout(false);
            this.pnlCloseSearch.PerformLayout();
            this.tabERA.ResumeLayout(false);
            this.tbpg_Hold.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Hold)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tbpg_Deleted.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Deleted)).EndInit();
            this.panel14.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.pnlTab.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlAdditionalCheckInfo.ResumeLayout(false);
            this.pnlAdditionalCheckInfo.PerformLayout();
            this.pnlPostInfo.ResumeLayout(false);
            this.pnlPostInfo.PerformLayout();
            this.pnlAddCheckInfoHeader.ResumeLayout(false);
            this.pnlMessagetxtbox.ResumeLayout(false);
            this.pnlMessagetxtbox.PerformLayout();
            this.pnlMessage.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus tls_Main;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.TabPage tbpg_Posted;
        private System.Windows.Forms.Panel panel9;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Posted;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TabPage tbpg_ReadyToPost;
        private System.Windows.Forms.Panel panel8;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ReadyToPost;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtSearchReadyToPost;
        private System.Windows.Forms.Label lblReadyToPostSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabControl tabERA;
        private System.Windows.Forms.TabPage tbpg_Deleted;
        private System.Windows.Forms.Panel pnlTab;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlAdditionalCheckInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlAddCheckInfoHeader;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Panel panel12;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Deleted;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox txtSearchDeleted;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.ToolStripButton tsb_Import;
        private System.Windows.Forms.ToolStripButton tsb_ERAFiles;
        private System.Windows.Forms.ToolStripButton tsb_PostingReport;
        private System.Windows.Forms.ToolStripButton tsb_Trial;
        private System.Windows.Forms.ToolStripButton tsb_Post;
        private System.Windows.Forms.ToolStripButton tsb_PaperEOB;
        private System.Windows.Forms.ToolStripButton tsb_MarkDeleted;
        private System.Windows.Forms.ToolStripButton tsb_View;
        private System.Windows.Forms.ToolStripButton tsb_UndoDelete;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.ToolStripButton tsb_Refresh;
        private System.Windows.Forms.Label lblPayerContact;
        private System.Windows.Forms.Label lblPaymentBalanced;
        private System.Windows.Forms.Label lblTotalPLBAmount;
        private System.Windows.Forms.Label lblTotalClaimPaid;
        private System.Windows.Forms.Label lblOriginalFileName;
        private System.Windows.Forms.Label lblProductionDate;
        private System.Windows.Forms.ProgressBar prgProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlMessagetxtbox;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Panel pnlMessage;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUP;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ImageList imgERA;
        private System.Windows.Forms.MaskedTextBox mskToDatePosted;
        private System.Windows.Forms.MaskedTextBox mskFromDatePosted;
        private System.Windows.Forms.MaskedTextBox mskToDateDeleted;
        private System.Windows.Forms.MaskedTextBox mskFromDateDeletd;
        private System.Windows.Forms.Button btnShowChecksPosted;
        private System.Windows.Forms.Button btnShowChecksDeleted;
        internal System.Windows.Forms.ToolStripDropDownButton tsb_Download;
        private System.Windows.Forms.ToolStripButton tsb_ExceptionReport;
        internal System.Windows.Forms.ToolStripButton tsb_ViewPayment;
        private System.Windows.Forms.TabPage tbpg_Hold;
        private System.Windows.Forms.ToolStripButton tsb_HoldCheck;
        private System.Windows.Forms.ToolStripButton tsb_UnholdCheck;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnShowChecksHold;
        private System.Windows.Forms.MaskedTextBox mskToDateHold;
        private System.Windows.Forms.MaskedTextBox mskFromDateHold;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtSearchHold;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label34;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Hold;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.ToolStripButton tsb_Notes;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label lblPayTray;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label lblCloseDate;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Panel pnlPostInfo;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label93;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PostedTotal;
        private System.Windows.Forms.Panel panel16;
        private C1.Win.C1FlexGrid.C1FlexGrid c1ReadyToPostTotal;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.ComboBox cmbPosted;
        internal System.Windows.Forms.Panel pnlCloseSearch;
        internal System.Windows.Forms.Label label89;
        internal System.Windows.Forms.Label label94;
        internal System.Windows.Forms.Label label95;
        internal System.Windows.Forms.Button btnClearSearchReadyToPost;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        internal System.Windows.Forms.Panel panel19;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchPosted;
        internal System.Windows.Forms.Label label96;
        internal System.Windows.Forms.Label label97;
        internal System.Windows.Forms.Button btnClearSearchPosted;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        internal System.Windows.Forms.Panel panel20;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label100;
        internal System.Windows.Forms.Label label101;
        internal System.Windows.Forms.Button btnClearSearchHold;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        internal System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.Label label68;
        internal System.Windows.Forms.Label label104;
        internal System.Windows.Forms.Label label105;
        internal System.Windows.Forms.Button btnClearSearchDeleted;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.ToolStripButton tsb_DetailPaperEOB;
        private System.Windows.Forms.ToolStripButton tsb_DetailExceptionReport;
        private System.Windows.Forms.ToolStripButton tsb_ExportToExcel;
    }
}
