namespace gloEDocumentV3.Forms
{
    partial class frmEDocumentViewer
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
                
                
                
                if (oPatientStrip != null)
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(oPatientStrip);
                    }
                    catch
                    {
                    }
                    oPatientStrip.Dispose();
                    oPatientStrip = null;
                }
                try
                {
                    if (ogloSettings != null)
                    {
                        ogloSettings.Dispose();
                        ogloSettings = null;
                    }
                }
                catch
                {
                }
                _oMdiParent = null;
                dMdi = null;

            }
            base.Dispose(disposing);

            System.Windows.Forms.ContextMenuStrip[] dtpControls = { lvwPages.ContextMenuStrip, c1Documents.ContextMenuStrip };
            

            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(ref dtpControls);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEDocumentViewer));
            this.pnlPages = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lvwPages = new System.Windows.Forms.ListView();
            this.imgPages = new System.Windows.Forms.ImageList(this.components);
            this.label21 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl_DocDateTime = new System.Windows.Forms.Label();
            this.btnPageView_Large = new System.Windows.Forms.Button();
            this.btnPageView_Small = new System.Windows.Forms.Button();
            this.btnPageView_List = new System.Windows.Forms.Button();
            this.lblPagesHeader = new System.Windows.Forms.Label();
            this.btnPageView_Tile = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlPreview1 = new System.Windows.Forms.Panel();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.label75 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.pnlDocumentPreviewCommand2 = new System.Windows.Forms.Panel();
            this.pnlDocumentPreviewCommand1 = new System.Windows.Forms.Panel();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.cmbZoomPercentage = new System.Windows.Forms.ComboBox();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.lblAlertMessage = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.btnPrv_Up = new System.Windows.Forms.Button();
            this.btnPrv_Down = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.RichTextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.lblSplNoteBottom = new System.Windows.Forms.Label();
            this.lblSplNoteTop = new System.Windows.Forms.Label();
            this.pnlNotes = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnNote_Down = new System.Windows.Forms.Button();
            this.btnNote_Up = new System.Windows.Forms.Button();
            this.lblNotes = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.pnlTags = new System.Windows.Forms.Panel();
            this.panel21 = new System.Windows.Forms.Panel();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.panel23 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTags = new System.Windows.Forms.Label();
            this.btnTag_Up = new System.Windows.Forms.Button();
            this.btnTag_Down = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txtSearch_DocumentName = new System.Windows.Forms.TextBox();
            this.chkSearch_PageName = new System.Windows.Forms.CheckBox();
            this.chkSearch_DocumentName = new System.Windows.Forms.CheckBox();
            this.txtSearch_PageName = new System.Windows.Forms.TextBox();
            this.chkSearch_Acknowledge = new System.Windows.Forms.CheckBox();
            this.txtSearch_Acknowledge = new System.Windows.Forms.TextBox();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbSearchYear = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.chkSearch_Notes = new System.Windows.Forms.CheckBox();
            this.txtSearch_UserTag = new System.Windows.Forms.TextBox();
            this.chkSearch_UserTag = new System.Windows.Forms.CheckBox();
            this.txtSearch_Notes = new System.Windows.Forms.TextBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkInSearchMode = new System.Windows.Forms.CheckBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.toolStrip1 = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Search_Search = new System.Windows.Forms.ToolStripButton();
            this.tsb_Search_Close = new System.Windows.Forms.ToolStripButton();
            this.c1Documents = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlDocuments = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label60 = new System.Windows.Forms.Label();
            this.lblDocumentsHeader = new System.Windows.Forms.Label();
            this.btnDoc_Up = new System.Windows.Forms.Button();
            this.btnDoc_Left = new System.Windows.Forms.Button();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.pnlDocumentsLegends = new System.Windows.Forms.Panel();
            this.pnlLegends = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLegends = new System.Windows.Forms.Label();
            this.btnLed_Up = new System.Windows.Forms.Button();
            this.btnLed_Down = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlSmallStrip = new System.Windows.Forms.Panel();
            this.pnlSmallStripMain = new System.Windows.Forms.Panel();
            this.ts_SmallStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_SmallStrip_btn_Document = new System.Windows.Forms.ToolStripButton();
            this.ts_SmallStrip_btn_Legend = new System.Windows.Forms.ToolStripButton();
            this.label45 = new System.Windows.Forms.Label();
            this.btn_Right = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.pnlPatients = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPatients = new System.Windows.Forms.Label();
            this.btnPat_Up = new System.Windows.Forms.Button();
            this.btnPat_Down = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter6 = new System.Windows.Forms.Splitter();
            this.bwLoadDocument = new System.ComponentModel.BackgroundWorker();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tls_MaintainDoc = new gloToolStrip.gloToolStrip();
            this.tsb_ChangeYearPrevious = new System.Windows.Forms.ToolStripButton();
            this.tsb_ChangeYear = new System.Windows.Forms.ToolStripLabel();
            this.tsb_ChangeYearNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Scan = new System.Windows.Forms.ToolStripButton();
            this.tsb_Import = new System.Windows.Forms.ToolStripButton();
            this.tsb_CopyDocument = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_RotateBack = new System.Windows.Forms.ToolStripButton();
            this.tsb_RotateForward = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsb_DeletePage = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_ShowHideAck = new System.Windows.Forms.ToolStripButton();
            this.tsb_Acknowledge = new System.Windows.Forms.ToolStripButton();
            this.tsb_ViewAcknowledge = new System.Windows.Forms.ToolStripButton();
            this.tsb_Task = new System.Windows.Forms.ToolStripButton();
            this.tsb_AddNote = new System.Windows.Forms.ToolStripButton();
            this.tsb_AddTags = new System.Windows.Forms.ToolStripButton();
            this.tsb_History = new System.Windows.Forms.ToolStripButton();
            this.tsb_InsertSign1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.tsb_Fax = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_PrintAll = new System.Windows.Forms.ToolStripButton();
            this.tsb_FaxAll = new System.Windows.Forms.ToolStripButton();
            this.tsb_Search = new System.Windows.Forms.ToolStripButton();
            this.tsb_Refresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_Splitter = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Archive = new System.Windows.Forms.ToolStripButton();
            this.tsb_Annotate = new System.Windows.Forms.ToolStripSplitButton();
            this.stsannot_Signature = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_ProviderSign = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_Checkmark = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_Line = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_Rectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannote_Ellipse = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_Arrow = new System.Windows.Forms.ToolStripMenuItem();
            this.sts_Freehand = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_Seperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stsannot_Editing = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_ClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_Seperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.textAnnotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModifyText = new System.Windows.Forms.ToolStripMenuItem();
            this.stsannot_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.stamperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_InsertSign = new System.Windows.Forms.ToolStripSplitButton();
            this.stb_Signature_with_Text = new System.Windows.Forms.ToolStripMenuItem();
            this.stb_Signature_with_Acknowledgement = new System.Windows.Forms.ToolStripMenuItem();
            this.stb_Signature_with_Notes = new System.Windows.Forms.ToolStripMenuItem();
            this.stb_Signature_with_Acknowledgement_Notes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_ProviderSign = new System.Windows.Forms.ToolStripSplitButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowFileArchivedDocuments = new System.Windows.Forms.ToolStripButton();
            this.pnlPages.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlPreview1.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.panel15.SuspendLayout();
            this.pnlDocumentPreviewCommand2.SuspendLayout();
            this.pnlDocumentPreviewCommand1.SuspendLayout();
            this.pnlNotes.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlTags.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel13.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Documents)).BeginInit();
            this.pnlDocuments.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel16.SuspendLayout();
            this.pnlDocumentsLegends.SuspendLayout();
            this.pnlLegends.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel12.SuspendLayout();
            this.pnlSmallStrip.SuspendLayout();
            this.pnlSmallStripMain.SuspendLayout();
            this.ts_SmallStrip.SuspendLayout();
            this.pnlPatients.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tls_MaintainDoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPages
            // 
            this.pnlPages.BackColor = System.Drawing.Color.Transparent;
            this.pnlPages.Controls.Add(this.panel10);
            this.pnlPages.Controls.Add(this.panel17);
            this.pnlPages.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPages.Location = new System.Drawing.Point(341, 110);
            this.pnlPages.Name = "pnlPages";
            this.pnlPages.Size = new System.Drawing.Size(520, 81);
            this.pnlPages.TabIndex = 20;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.lvwPages);
            this.panel10.Controls.Add(this.label21);
            this.panel10.Controls.Add(this.label29);
            this.panel10.Controls.Add(this.label5);
            this.panel10.Controls.Add(this.label23);
            this.panel10.Controls.Add(this.label24);
            this.panel10.Controls.Add(this.label46);
            this.panel10.Controls.Add(this.label22);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 26);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.panel10.Size = new System.Drawing.Size(520, 55);
            this.panel10.TabIndex = 14;
            // 
            // lvwPages
            // 
            this.lvwPages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvwPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwPages.ForeColor = System.Drawing.Color.Black;
            this.lvwPages.FullRowSelect = true;
            this.lvwPages.HideSelection = false;
            this.lvwPages.LargeImageList = this.imgPages;
            this.lvwPages.Location = new System.Drawing.Point(4, 4);
            this.lvwPages.Name = "lvwPages";
            this.lvwPages.Size = new System.Drawing.Size(509, 50);
            this.lvwPages.SmallImageList = this.imgPages;
            this.lvwPages.TabIndex = 13;
            this.lvwPages.UseCompatibleStateImageBehavior = false;
            this.lvwPages.Click += new System.EventHandler(this.lvwPages_Click);
            this.lvwPages.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwPages_MouseClick);
            this.lvwPages.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwPages_MouseDown);
            this.lvwPages.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwPages_MouseUp);
            // 
            // imgPages
            // 
            this.imgPages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgPages.ImageStream")));
            this.imgPages.TransparentColor = System.Drawing.Color.Transparent;
            this.imgPages.Images.SetKeyName(0, "New.ico");
            this.imgPages.Images.SetKeyName(1, "pageNote.ico");
            this.imgPages.Images.SetKeyName(2, "Result.ico");
            this.imgPages.Images.SetKeyName(3, "Result_Comment.ico");
            this.imgPages.Images.SetKeyName(4, "InsertPictureHS.png");
            this.imgPages.Images.SetKeyName(5, "LegendHS.png");
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.SystemColors.Window;
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(1, 4);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(3, 50);
            this.label21.TabIndex = 11;
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.SystemColors.Window;
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(513, 4);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(3, 50);
            this.label29.TabIndex = 12;
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(515, 3);
            this.label5.TabIndex = 9;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(0, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 53);
            this.label23.TabIndex = 16;
            this.label23.Text = "label4";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.Location = new System.Drawing.Point(516, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 53);
            this.label24.TabIndex = 15;
            this.label24.Text = "label3";
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label46.Dock = System.Windows.Forms.DockStyle.Top;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(0, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(517, 1);
            this.label46.TabIndex = 14;
            this.label46.Text = "label1";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label22.Location = new System.Drawing.Point(0, 54);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(517, 1);
            this.label22.TabIndex = 17;
            this.label22.Text = "label2";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.panel4);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Name = "panel17";
            this.panel17.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel17.Size = new System.Drawing.Size(520, 26);
            this.panel17.TabIndex = 28;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.lbl_DocDateTime);
            this.panel4.Controls.Add(this.btnPageView_Large);
            this.panel4.Controls.Add(this.btnPageView_Small);
            this.panel4.Controls.Add(this.btnPageView_List);
            this.panel4.Controls.Add(this.lblPagesHeader);
            this.panel4.Controls.Add(this.btnPageView_Tile);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(517, 20);
            this.panel4.TabIndex = 0;
            // 
            // lbl_DocDateTime
            // 
            this.lbl_DocDateTime.AutoSize = true;
            this.lbl_DocDateTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_DocDateTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_DocDateTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DocDateTime.Location = new System.Drawing.Point(365, 1);
            this.lbl_DocDateTime.Name = "lbl_DocDateTime";
            this.lbl_DocDateTime.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lbl_DocDateTime.Size = new System.Drawing.Size(55, 16);
            this.lbl_DocDateTime.TabIndex = 13;
            this.lbl_DocDateTime.Text = "Created";
            this.lbl_DocDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPageView_Large
            // 
            this.btnPageView_Large.BackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Large.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPageView_Large.FlatAppearance.BorderSize = 0;
            this.btnPageView_Large.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Large.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Large.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageView_Large.Image = ((System.Drawing.Image)(resources.GetObject("btnPageView_Large.Image")));
            this.btnPageView_Large.Location = new System.Drawing.Point(420, 1);
            this.btnPageView_Large.Name = "btnPageView_Large";
            this.btnPageView_Large.Size = new System.Drawing.Size(24, 18);
            this.btnPageView_Large.TabIndex = 8;
            this.btnPageView_Large.UseVisualStyleBackColor = false;
            this.btnPageView_Large.Visible = false;
            // 
            // btnPageView_Small
            // 
            this.btnPageView_Small.BackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Small.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPageView_Small.FlatAppearance.BorderSize = 0;
            this.btnPageView_Small.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Small.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Small.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageView_Small.Image = ((System.Drawing.Image)(resources.GetObject("btnPageView_Small.Image")));
            this.btnPageView_Small.Location = new System.Drawing.Point(444, 1);
            this.btnPageView_Small.Name = "btnPageView_Small";
            this.btnPageView_Small.Size = new System.Drawing.Size(24, 18);
            this.btnPageView_Small.TabIndex = 7;
            this.btnPageView_Small.UseVisualStyleBackColor = false;
            this.btnPageView_Small.Visible = false;
            // 
            // btnPageView_List
            // 
            this.btnPageView_List.BackColor = System.Drawing.Color.Transparent;
            this.btnPageView_List.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPageView_List.FlatAppearance.BorderSize = 0;
            this.btnPageView_List.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPageView_List.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPageView_List.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageView_List.Image = ((System.Drawing.Image)(resources.GetObject("btnPageView_List.Image")));
            this.btnPageView_List.Location = new System.Drawing.Point(468, 1);
            this.btnPageView_List.Name = "btnPageView_List";
            this.btnPageView_List.Size = new System.Drawing.Size(24, 18);
            this.btnPageView_List.TabIndex = 6;
            this.btnPageView_List.UseVisualStyleBackColor = false;
            this.btnPageView_List.Visible = false;
            // 
            // lblPagesHeader
            // 
            this.lblPagesHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblPagesHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPagesHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagesHeader.Location = new System.Drawing.Point(1, 1);
            this.lblPagesHeader.Name = "lblPagesHeader";
            this.lblPagesHeader.Size = new System.Drawing.Size(491, 18);
            this.lblPagesHeader.TabIndex = 2;
            this.lblPagesHeader.Text = "  Pages";
            this.lblPagesHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPageView_Tile
            // 
            this.btnPageView_Tile.BackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Tile.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPageView_Tile.FlatAppearance.BorderSize = 0;
            this.btnPageView_Tile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Tile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPageView_Tile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageView_Tile.Image = ((System.Drawing.Image)(resources.GetObject("btnPageView_Tile.Image")));
            this.btnPageView_Tile.Location = new System.Drawing.Point(492, 1);
            this.btnPageView_Tile.Name = "btnPageView_Tile";
            this.btnPageView_Tile.Size = new System.Drawing.Size(24, 18);
            this.btnPageView_Tile.TabIndex = 1;
            this.btnPageView_Tile.UseVisualStyleBackColor = false;
            this.btnPageView_Tile.Visible = false;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(1, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(515, 1);
            this.label17.TabIndex = 12;
            this.label17.Text = "label2";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 19);
            this.label18.TabIndex = 11;
            this.label18.Text = "label4";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label19.Location = new System.Drawing.Point(516, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 19);
            this.label19.TabIndex = 10;
            this.label19.Text = "label3";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(517, 1);
            this.label20.TabIndex = 9;
            this.label20.Text = "label1";
            // 
            // pnlPreview1
            // 
            this.pnlPreview1.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreview1.Controls.Add(this.pnlPreview);
            this.pnlPreview1.Controls.Add(this.panel15);
            this.pnlPreview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview1.Location = new System.Drawing.Point(341, 194);
            this.pnlPreview1.Name = "pnlPreview1";
            this.pnlPreview1.Size = new System.Drawing.Size(520, 121);
            this.pnlPreview1.TabIndex = 26;
            // 
            // pnlPreview
            // 
            this.pnlPreview.BackColor = System.Drawing.Color.White;
            this.pnlPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPreview.Controls.Add(this.label75);
            this.pnlPreview.Controls.Add(this.label74);
            this.pnlPreview.Controls.Add(this.label72);
            this.pnlPreview.Controls.Add(this.label73);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPreview.ForeColor = System.Drawing.Color.White;
            this.pnlPreview.Location = new System.Drawing.Point(0, 27);
            this.pnlPreview.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlPreview.Size = new System.Drawing.Size(520, 94);
            this.pnlPreview.TabIndex = 29;
            this.pnlPreview.Leave += new System.EventHandler(this.pnlPreview_Leave);
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label75.Dock = System.Windows.Forms.DockStyle.Top;
            this.label75.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(1, 0);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(515, 1);
            this.label75.TabIndex = 5;
            this.label75.Text = "label1";
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label74.Dock = System.Windows.Forms.DockStyle.Right;
            this.label74.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label74.Location = new System.Drawing.Point(516, 0);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(1, 90);
            this.label74.TabIndex = 6;
            this.label74.Text = "label3";
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label72.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label72.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label72.Location = new System.Drawing.Point(1, 90);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(516, 1);
            this.label72.TabIndex = 8;
            this.label72.Text = "label2";
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label73.Dock = System.Windows.Forms.DockStyle.Left;
            this.label73.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(0, 0);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(1, 91);
            this.label73.TabIndex = 7;
            this.label73.Text = "label4";
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.pnlDocumentPreviewCommand2);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel15.Size = new System.Drawing.Size(520, 27);
            this.panel15.TabIndex = 28;
            // 
            // pnlDocumentPreviewCommand2
            // 
            this.pnlDocumentPreviewCommand2.BackColor = System.Drawing.Color.Transparent;
            this.pnlDocumentPreviewCommand2.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.pnlDocumentPreviewCommand2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDocumentPreviewCommand2.Controls.Add(this.pnlDocumentPreviewCommand1);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.panel1);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.btnZoomOut);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.cmbZoomPercentage);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.btnZoomIn);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.lblAlertMessage);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.lblPreview);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.btnPrv_Up);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.btnPrv_Down);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.label41);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.label42);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.label43);
            this.pnlDocumentPreviewCommand2.Controls.Add(this.label44);
            this.pnlDocumentPreviewCommand2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDocumentPreviewCommand2.Location = new System.Drawing.Point(0, 0);
            this.pnlDocumentPreviewCommand2.Name = "pnlDocumentPreviewCommand2";
            this.pnlDocumentPreviewCommand2.Size = new System.Drawing.Size(517, 24);
            this.pnlDocumentPreviewCommand2.TabIndex = 0;
            // 
            // pnlDocumentPreviewCommand1
            // 
            this.pnlDocumentPreviewCommand1.BackColor = System.Drawing.Color.Transparent;
            this.pnlDocumentPreviewCommand1.Controls.Add(this.btnFirstPage);
            this.pnlDocumentPreviewCommand1.Controls.Add(this.btnPrevPage);
            this.pnlDocumentPreviewCommand1.Controls.Add(this.btnNextPage);
            this.pnlDocumentPreviewCommand1.Controls.Add(this.btnLastPage);
            this.pnlDocumentPreviewCommand1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDocumentPreviewCommand1.Location = new System.Drawing.Point(70, 1);
            this.pnlDocumentPreviewCommand1.Name = "pnlDocumentPreviewCommand1";
            this.pnlDocumentPreviewCommand1.Size = new System.Drawing.Size(199, 22);
            this.pnlDocumentPreviewCommand1.TabIndex = 6;
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.BackColor = System.Drawing.Color.Transparent;
            this.btnFirstPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFirstPage.FlatAppearance.BorderSize = 0;
            this.btnFirstPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFirstPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirstPage.Location = new System.Drawing.Point(103, 0);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(24, 22);
            this.btnFirstPage.TabIndex = 11;
            this.btnFirstPage.UseVisualStyleBackColor = false;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            this.btnFirstPage.MouseLeave += new System.EventHandler(this.btnFirstPage_MouseLeave);
            this.btnFirstPage.MouseHover += new System.EventHandler(this.btnFirstPage_MouseHover);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrevPage.FlatAppearance.BorderSize = 0;
            this.btnPrevPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrevPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevPage.Location = new System.Drawing.Point(127, 0);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(24, 22);
            this.btnPrevPage.TabIndex = 10;
            this.btnPrevPage.UseVisualStyleBackColor = false;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            this.btnPrevPage.MouseLeave += new System.EventHandler(this.btnPrevPage_MouseLeave);
            this.btnPrevPage.MouseHover += new System.EventHandler(this.btnPrevPage_MouseHover);
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = System.Drawing.Color.Transparent;
            this.btnNextPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextPage.FlatAppearance.BorderSize = 0;
            this.btnNextPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNextPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Location = new System.Drawing.Point(151, 0);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(24, 22);
            this.btnNextPage.TabIndex = 9;
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            this.btnNextPage.MouseLeave += new System.EventHandler(this.btnNextPage_MouseLeave);
            this.btnNextPage.MouseHover += new System.EventHandler(this.btnNextPage_MouseHover);
            // 
            // btnLastPage
            // 
            this.btnLastPage.BackColor = System.Drawing.Color.Transparent;
            this.btnLastPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLastPage.FlatAppearance.BorderSize = 0;
            this.btnLastPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLastPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastPage.Location = new System.Drawing.Point(175, 0);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(24, 22);
            this.btnLastPage.TabIndex = 8;
            this.btnLastPage.UseVisualStyleBackColor = false;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            this.btnLastPage.MouseLeave += new System.EventHandler(this.btnLastPage_MouseLeave);
            this.btnLastPage.MouseHover += new System.EventHandler(this.btnLastPage_MouseHover);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(269, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(23, 22);
            this.panel1.TabIndex = 4;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnZoomOut.FlatAppearance.BorderSize = 0;
            this.btnZoomOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.Location = new System.Drawing.Point(292, 1);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(24, 22);
            this.btnZoomOut.TabIndex = 5;
            this.btnZoomOut.UseVisualStyleBackColor = false;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            this.btnZoomOut.MouseLeave += new System.EventHandler(this.btnZoomOut_MouseLeave);
            this.btnZoomOut.MouseHover += new System.EventHandler(this.btnZoomOut_MouseHover);
            // 
            // cmbZoomPercentage
            // 
            this.cmbZoomPercentage.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbZoomPercentage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZoomPercentage.FormattingEnabled = true;
            this.cmbZoomPercentage.Items.AddRange(new object[] {
            "25%",
            "50%",
            "75%",
            "100%",
            "125%",
            "150%",
            "175%",
            "200%",
            "400%",
            "Fit To Width",
            "Full Page"});
            this.cmbZoomPercentage.Location = new System.Drawing.Point(316, 1);
            this.cmbZoomPercentage.Name = "cmbZoomPercentage";
            this.cmbZoomPercentage.Size = new System.Drawing.Size(128, 22);
            this.cmbZoomPercentage.TabIndex = 0;
            this.cmbZoomPercentage.SelectedIndexChanged += new System.EventHandler(this.cmbZoomPercentage_SelectedIndexChanged);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnZoomIn.FlatAppearance.BorderSize = 0;
            this.btnZoomIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.Location = new System.Drawing.Point(444, 1);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(24, 22);
            this.btnZoomIn.TabIndex = 1;
            this.btnZoomIn.UseVisualStyleBackColor = false;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            this.btnZoomIn.MouseLeave += new System.EventHandler(this.btnZoomIn_MouseLeave);
            this.btnZoomIn.MouseHover += new System.EventHandler(this.btnZoomIn_MouseHover);
            // 
            // lblAlertMessage
            // 
            this.lblAlertMessage.AutoSize = true;
            this.lblAlertMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblAlertMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertMessage.ForeColor = System.Drawing.Color.Red;
            this.lblAlertMessage.Location = new System.Drawing.Point(74, 5);
            this.lblAlertMessage.Name = "lblAlertMessage";
            this.lblAlertMessage.Size = new System.Drawing.Size(59, 14);
            this.lblAlertMessage.TabIndex = 3;
            this.lblAlertMessage.Text = "  Sample";
            this.lblAlertMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPreview
            // 
            this.lblPreview.BackColor = System.Drawing.Color.Transparent;
            this.lblPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPreview.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPreview.Location = new System.Drawing.Point(1, 1);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(467, 22);
            this.lblPreview.TabIndex = 2;
            this.lblPreview.Text = "   Preview";
            this.lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPrv_Up
            // 
            this.btnPrv_Up.BackColor = System.Drawing.Color.Transparent;
            this.btnPrv_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrv_Up.FlatAppearance.BorderSize = 0;
            this.btnPrv_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrv_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrv_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrv_Up.Image = ((System.Drawing.Image)(resources.GetObject("btnPrv_Up.Image")));
            this.btnPrv_Up.Location = new System.Drawing.Point(468, 1);
            this.btnPrv_Up.Name = "btnPrv_Up";
            this.btnPrv_Up.Size = new System.Drawing.Size(24, 22);
            this.btnPrv_Up.TabIndex = 1;
            this.btnPrv_Up.UseVisualStyleBackColor = false;
            this.btnPrv_Up.Visible = false;
            // 
            // btnPrv_Down
            // 
            this.btnPrv_Down.BackColor = System.Drawing.Color.Transparent;
            this.btnPrv_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrv_Down.FlatAppearance.BorderSize = 0;
            this.btnPrv_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrv_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrv_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrv_Down.Image = ((System.Drawing.Image)(resources.GetObject("btnPrv_Down.Image")));
            this.btnPrv_Down.Location = new System.Drawing.Point(492, 1);
            this.btnPrv_Down.Name = "btnPrv_Down";
            this.btnPrv_Down.Size = new System.Drawing.Size(24, 22);
            this.btnPrv_Down.TabIndex = 0;
            this.btnPrv_Down.UseVisualStyleBackColor = false;
            this.btnPrv_Down.Visible = false;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label41.Location = new System.Drawing.Point(1, 23);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(515, 1);
            this.label41.TabIndex = 12;
            this.label41.Text = "label2";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Left;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(0, 1);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(1, 23);
            this.label42.TabIndex = 11;
            this.label42.Text = "label4";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Right;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label43.Location = new System.Drawing.Point(516, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 23);
            this.label43.TabIndex = 10;
            this.label43.Text = "label3";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Top;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(0, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(517, 1);
            this.label44.TabIndex = 9;
            this.label44.Text = "label1";
            // 
            // txtNotes
            // 
            this.txtNotes.BackColor = System.Drawing.Color.White;
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.ForeColor = System.Drawing.Color.Black;
            this.txtNotes.Location = new System.Drawing.Point(4, 4);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ReadOnly = true;
            this.txtNotes.Size = new System.Drawing.Size(509, 83);
            this.txtNotes.TabIndex = 14;
            this.txtNotes.Text = "";
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.SystemColors.Window;
            this.label50.Dock = System.Windows.Forms.DockStyle.Right;
            this.label50.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(513, 1);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(3, 86);
            this.label50.TabIndex = 13;
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.SystemColors.Window;
            this.label49.Dock = System.Windows.Forms.DockStyle.Left;
            this.label49.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(1, 4);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(3, 83);
            this.label49.TabIndex = 12;
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSplNoteBottom
            // 
            this.lblSplNoteBottom.BackColor = System.Drawing.SystemColors.Window;
            this.lblSplNoteBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSplNoteBottom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSplNoteBottom.Location = new System.Drawing.Point(1, 87);
            this.lblSplNoteBottom.Name = "lblSplNoteBottom";
            this.lblSplNoteBottom.Size = new System.Drawing.Size(515, 3);
            this.lblSplNoteBottom.TabIndex = 11;
            this.lblSplNoteBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSplNoteTop
            // 
            this.lblSplNoteTop.BackColor = System.Drawing.Color.White;
            this.lblSplNoteTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSplNoteTop.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSplNoteTop.Location = new System.Drawing.Point(1, 1);
            this.lblSplNoteTop.Name = "lblSplNoteTop";
            this.lblSplNoteTop.Size = new System.Drawing.Size(512, 3);
            this.lblSplNoteTop.TabIndex = 10;
            this.lblSplNoteTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlNotes
            // 
            this.pnlNotes.BackColor = System.Drawing.Color.Transparent;
            this.pnlNotes.Controls.Add(this.panel20);
            this.pnlNotes.Controls.Add(this.panel22);
            this.pnlNotes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNotes.Location = new System.Drawing.Point(341, 315);
            this.pnlNotes.Name = "pnlNotes";
            this.pnlNotes.Size = new System.Drawing.Size(520, 120);
            this.pnlNotes.TabIndex = 24;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.txtNotes);
            this.panel20.Controls.Add(this.label49);
            this.panel20.Controls.Add(this.lblSplNoteTop);
            this.panel20.Controls.Add(this.label50);
            this.panel20.Controls.Add(this.lblSplNoteBottom);
            this.panel20.Controls.Add(this.label31);
            this.panel20.Controls.Add(this.label32);
            this.panel20.Controls.Add(this.label33);
            this.panel20.Controls.Add(this.label30);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(0, 26);
            this.panel20.Name = "panel20";
            this.panel20.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel20.Size = new System.Drawing.Size(520, 94);
            this.panel20.TabIndex = 15;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(0, 1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 89);
            this.label31.TabIndex = 17;
            this.label31.Text = "label4";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label32.Location = new System.Drawing.Point(516, 1);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 89);
            this.label32.TabIndex = 16;
            this.label32.Text = "label3";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(0, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(517, 1);
            this.label33.TabIndex = 15;
            this.label33.Text = "label1";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label30.Location = new System.Drawing.Point(0, 90);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(517, 1);
            this.label30.TabIndex = 18;
            this.label30.Text = "label2";
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.panel8);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel22.Location = new System.Drawing.Point(0, 0);
            this.panel22.Name = "panel22";
            this.panel22.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel22.Size = new System.Drawing.Size(520, 26);
            this.panel22.TabIndex = 16;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.btnNote_Down);
            this.panel8.Controls.Add(this.btnNote_Up);
            this.panel8.Controls.Add(this.lblNotes);
            this.panel8.Controls.Add(this.label27);
            this.panel8.Controls.Add(this.label26);
            this.panel8.Controls.Add(this.label25);
            this.panel8.Controls.Add(this.label28);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(517, 23);
            this.panel8.TabIndex = 0;
            // 
            // btnNote_Down
            // 
            this.btnNote_Down.BackColor = System.Drawing.Color.Transparent;
            this.btnNote_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNote_Down.FlatAppearance.BorderSize = 0;
            this.btnNote_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNote_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNote_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNote_Down.Location = new System.Drawing.Point(468, 1);
            this.btnNote_Down.Name = "btnNote_Down";
            this.btnNote_Down.Size = new System.Drawing.Size(24, 21);
            this.btnNote_Down.TabIndex = 0;
            this.btnNote_Down.UseVisualStyleBackColor = false;
            this.btnNote_Down.Click += new System.EventHandler(this.btnNote_Down_Click);
            this.btnNote_Down.MouseLeave += new System.EventHandler(this.btnNote_Down_MouseLeave);
            this.btnNote_Down.MouseHover += new System.EventHandler(this.btnNote_Down_MouseHover);
            // 
            // btnNote_Up
            // 
            this.btnNote_Up.BackColor = System.Drawing.Color.Transparent;
            this.btnNote_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNote_Up.FlatAppearance.BorderSize = 0;
            this.btnNote_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNote_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNote_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNote_Up.Location = new System.Drawing.Point(492, 1);
            this.btnNote_Up.Name = "btnNote_Up";
            this.btnNote_Up.Size = new System.Drawing.Size(24, 21);
            this.btnNote_Up.TabIndex = 1;
            this.btnNote_Up.UseVisualStyleBackColor = false;
            this.btnNote_Up.Click += new System.EventHandler(this.btnNote_Up_Click);
            this.btnNote_Up.MouseLeave += new System.EventHandler(this.btnNote_Up_MouseLeave);
            this.btnNote_Up.MouseHover += new System.EventHandler(this.btnNote_Up_MouseHover);
            // 
            // lblNotes
            // 
            this.lblNotes.BackColor = System.Drawing.Color.Transparent;
            this.lblNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(1, 1);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(515, 21);
            this.lblNotes.TabIndex = 2;
            this.lblNotes.Text = "   Notes";
            this.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label27.Location = new System.Drawing.Point(516, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 21);
            this.label27.TabIndex = 10;
            this.label27.Text = "label3";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(0, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 21);
            this.label26.TabIndex = 11;
            this.label26.Text = "label4";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label25.Location = new System.Drawing.Point(0, 22);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(517, 1);
            this.label25.TabIndex = 12;
            this.label25.Text = "label2";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(517, 1);
            this.label28.TabIndex = 9;
            this.label28.Text = "label1";
            // 
            // pnlTags
            // 
            this.pnlTags.BackColor = System.Drawing.Color.Transparent;
            this.pnlTags.Controls.Add(this.panel21);
            this.pnlTags.Controls.Add(this.panel23);
            this.pnlTags.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTags.Location = new System.Drawing.Point(341, 435);
            this.pnlTags.Name = "pnlTags";
            this.pnlTags.Size = new System.Drawing.Size(520, 84);
            this.pnlTags.TabIndex = 21;
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.txtTags);
            this.panel21.Controls.Add(this.label39);
            this.panel21.Controls.Add(this.label40);
            this.panel21.Controls.Add(this.label48);
            this.panel21.Controls.Add(this.label71);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel21.Location = new System.Drawing.Point(0, 26);
            this.panel21.Name = "panel21";
            this.panel21.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel21.Size = new System.Drawing.Size(520, 58);
            this.panel21.TabIndex = 10;
            // 
            // txtTags
            // 
            this.txtTags.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTags.ForeColor = System.Drawing.Color.Black;
            this.txtTags.Location = new System.Drawing.Point(1, 1);
            this.txtTags.Multiline = true;
            this.txtTags.Name = "txtTags";
            this.txtTags.Size = new System.Drawing.Size(515, 53);
            this.txtTags.TabIndex = 9;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label39.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label39.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label39.Location = new System.Drawing.Point(1, 54);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(515, 1);
            this.label39.TabIndex = 13;
            this.label39.Text = "label2";
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label40.Dock = System.Windows.Forms.DockStyle.Left;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(0, 1);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(1, 54);
            this.label40.TabIndex = 12;
            this.label40.Text = "label4";
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Right;
            this.label48.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label48.Location = new System.Drawing.Point(516, 1);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(1, 54);
            this.label48.TabIndex = 11;
            this.label48.Text = "label3";
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label71.Dock = System.Windows.Forms.DockStyle.Top;
            this.label71.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(0, 0);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(517, 1);
            this.label71.TabIndex = 10;
            this.label71.Text = "label1";
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.panel6);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Name = "panel23";
            this.panel23.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel23.Size = new System.Drawing.Size(520, 26);
            this.panel23.TabIndex = 16;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.lblTags);
            this.panel6.Controls.Add(this.btnTag_Up);
            this.panel6.Controls.Add(this.btnTag_Down);
            this.panel6.Controls.Add(this.label34);
            this.panel6.Controls.Add(this.label35);
            this.panel6.Controls.Add(this.label36);
            this.panel6.Controls.Add(this.label38);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(517, 23);
            this.panel6.TabIndex = 0;
            // 
            // lblTags
            // 
            this.lblTags.BackColor = System.Drawing.Color.Transparent;
            this.lblTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTags.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTags.Location = new System.Drawing.Point(1, 1);
            this.lblTags.Name = "lblTags";
            this.lblTags.Size = new System.Drawing.Size(467, 21);
            this.lblTags.TabIndex = 2;
            this.lblTags.Text = "  Tags";
            this.lblTags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTag_Up
            // 
            this.btnTag_Up.BackColor = System.Drawing.Color.Transparent;
            this.btnTag_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTag_Up.FlatAppearance.BorderSize = 0;
            this.btnTag_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTag_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTag_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTag_Up.Image = ((System.Drawing.Image)(resources.GetObject("btnTag_Up.Image")));
            this.btnTag_Up.Location = new System.Drawing.Point(468, 1);
            this.btnTag_Up.Name = "btnTag_Up";
            this.btnTag_Up.Size = new System.Drawing.Size(24, 21);
            this.btnTag_Up.TabIndex = 1;
            this.btnTag_Up.UseVisualStyleBackColor = false;
            // 
            // btnTag_Down
            // 
            this.btnTag_Down.BackColor = System.Drawing.Color.Transparent;
            this.btnTag_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTag_Down.FlatAppearance.BorderSize = 0;
            this.btnTag_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTag_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTag_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTag_Down.Image = ((System.Drawing.Image)(resources.GetObject("btnTag_Down.Image")));
            this.btnTag_Down.Location = new System.Drawing.Point(492, 1);
            this.btnTag_Down.Name = "btnTag_Down";
            this.btnTag_Down.Size = new System.Drawing.Size(24, 21);
            this.btnTag_Down.TabIndex = 0;
            this.btnTag_Down.UseVisualStyleBackColor = false;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label34.Location = new System.Drawing.Point(1, 22);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(515, 1);
            this.label34.TabIndex = 12;
            this.label34.Text = "label2";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Left;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(0, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 22);
            this.label35.TabIndex = 11;
            this.label35.Text = "label4";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Right;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label36.Location = new System.Drawing.Point(516, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 22);
            this.label36.TabIndex = 10;
            this.label36.Text = "label3";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(517, 1);
            this.label38.TabIndex = 9;
            this.label38.Text = "label1";
            // 
            // txtSearch_DocumentName
            // 
            this.txtSearch_DocumentName.BackColor = System.Drawing.Color.White;
            this.txtSearch_DocumentName.Location = new System.Drawing.Point(134, 117);
            this.txtSearch_DocumentName.Name = "txtSearch_DocumentName";
            this.txtSearch_DocumentName.ReadOnly = true;
            this.txtSearch_DocumentName.Size = new System.Drawing.Size(162, 22);
            this.txtSearch_DocumentName.TabIndex = 8;
            // 
            // chkSearch_PageName
            // 
            this.chkSearch_PageName.AutoSize = true;
            this.chkSearch_PageName.Location = new System.Drawing.Point(13, 157);
            this.chkSearch_PageName.Name = "chkSearch_PageName";
            this.chkSearch_PageName.Size = new System.Drawing.Size(88, 18);
            this.chkSearch_PageName.TabIndex = 18;
            this.chkSearch_PageName.Text = "Page Name";
            this.chkSearch_PageName.UseVisualStyleBackColor = true;
            this.chkSearch_PageName.Visible = false;
            // 
            // chkSearch_DocumentName
            // 
            this.chkSearch_DocumentName.AutoSize = true;
            this.chkSearch_DocumentName.Location = new System.Drawing.Point(10, 120);
            this.chkSearch_DocumentName.Name = "chkSearch_DocumentName";
            this.chkSearch_DocumentName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSearch_DocumentName.Size = new System.Drawing.Size(118, 18);
            this.chkSearch_DocumentName.TabIndex = 7;
            this.chkSearch_DocumentName.Text = "Document Name";
            this.chkSearch_DocumentName.UseVisualStyleBackColor = true;
            this.chkSearch_DocumentName.CheckedChanged += new System.EventHandler(this.chkSearch_DocumentName_CheckedChanged);
            // 
            // txtSearch_PageName
            // 
            this.txtSearch_PageName.Location = new System.Drawing.Point(134, 153);
            this.txtSearch_PageName.Name = "txtSearch_PageName";
            this.txtSearch_PageName.Size = new System.Drawing.Size(162, 22);
            this.txtSearch_PageName.TabIndex = 17;
            this.txtSearch_PageName.Visible = false;
            // 
            // chkSearch_Acknowledge
            // 
            this.chkSearch_Acknowledge.AutoSize = true;
            this.chkSearch_Acknowledge.Location = new System.Drawing.Point(28, 95);
            this.chkSearch_Acknowledge.Name = "chkSearch_Acknowledge";
            this.chkSearch_Acknowledge.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSearch_Acknowledge.Size = new System.Drawing.Size(100, 18);
            this.chkSearch_Acknowledge.TabIndex = 5;
            this.chkSearch_Acknowledge.Text = "Acknowledge";
            this.chkSearch_Acknowledge.UseVisualStyleBackColor = true;
            this.chkSearch_Acknowledge.CheckedChanged += new System.EventHandler(this.chkSearch_Acknowledge_CheckedChanged);
            // 
            // txtSearch_Acknowledge
            // 
            this.txtSearch_Acknowledge.BackColor = System.Drawing.Color.White;
            this.txtSearch_Acknowledge.Location = new System.Drawing.Point(134, 92);
            this.txtSearch_Acknowledge.MaxLength = 50;
            this.txtSearch_Acknowledge.Name = "txtSearch_Acknowledge";
            this.txtSearch_Acknowledge.ReadOnly = true;
            this.txtSearch_Acknowledge.Size = new System.Drawing.Size(162, 22);
            this.txtSearch_Acknowledge.TabIndex = 6;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.Controls.Add(this.panel11);
            this.pnlSearch.Controls.Add(this.panel14);
            this.pnlSearch.Controls.Add(this.toolStrip1);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(306, 179);
            this.pnlSearch.TabIndex = 15;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.label11);
            this.panel11.Controls.Add(this.cmbSearchYear);
            this.panel11.Controls.Add(this.label10);
            this.panel11.Controls.Add(this.label51);
            this.panel11.Controls.Add(this.label55);
            this.panel11.Controls.Add(this.label56);
            this.panel11.Controls.Add(this.label61);
            this.panel11.Controls.Add(this.chkSearch_DocumentName);
            this.panel11.Controls.Add(this.txtSearch_DocumentName);
            this.panel11.Controls.Add(this.chkSearch_Notes);
            this.panel11.Controls.Add(this.txtSearch_Acknowledge);
            this.panel11.Controls.Add(this.txtSearch_UserTag);
            this.panel11.Controls.Add(this.chkSearch_Acknowledge);
            this.panel11.Controls.Add(this.chkSearch_PageName);
            this.panel11.Controls.Add(this.txtSearch_PageName);
            this.panel11.Controls.Add(this.chkSearch_UserTag);
            this.panel11.Controls.Add(this.txtSearch_Notes);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 30);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel11.Size = new System.Drawing.Size(306, 149);
            this.panel11.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Location = new System.Drawing.Point(4, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(301, 1);
            this.label11.TabIndex = 23;
            this.label11.Text = "label11";
            // 
            // cmbSearchYear
            // 
            this.cmbSearchYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchYear.FormattingEnabled = true;
            this.cmbSearchYear.Location = new System.Drawing.Point(134, 6);
            this.cmbSearchYear.Name = "cmbSearchYear";
            this.cmbSearchYear.Size = new System.Drawing.Size(162, 22);
            this.cmbSearchYear.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 14);
            this.label10.TabIndex = 21;
            this.label10.Text = "Select Year :";
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label51.Location = new System.Drawing.Point(4, 148);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(301, 1);
            this.label51.TabIndex = 12;
            this.label51.Text = "label2";
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Left;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(3, 1);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(1, 148);
            this.label55.TabIndex = 11;
            this.label55.Text = "label4";
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label56.Dock = System.Windows.Forms.DockStyle.Right;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label56.Location = new System.Drawing.Point(305, 1);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(1, 148);
            this.label56.TabIndex = 10;
            this.label56.Text = "label3";
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Dock = System.Windows.Forms.DockStyle.Top;
            this.label61.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(3, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(303, 1);
            this.label61.TabIndex = 9;
            this.label61.Text = "label1";
            // 
            // chkSearch_Notes
            // 
            this.chkSearch_Notes.AutoSize = true;
            this.chkSearch_Notes.Location = new System.Drawing.Point(70, 70);
            this.chkSearch_Notes.Name = "chkSearch_Notes";
            this.chkSearch_Notes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSearch_Notes.Size = new System.Drawing.Size(58, 18);
            this.chkSearch_Notes.TabIndex = 3;
            this.chkSearch_Notes.Text = "Notes";
            this.chkSearch_Notes.UseVisualStyleBackColor = true;
            this.chkSearch_Notes.CheckedChanged += new System.EventHandler(this.chkSearch_Notes_CheckedChanged);
            // 
            // txtSearch_UserTag
            // 
            this.txtSearch_UserTag.BackColor = System.Drawing.Color.White;
            this.txtSearch_UserTag.Location = new System.Drawing.Point(134, 42);
            this.txtSearch_UserTag.MaxLength = 50;
            this.txtSearch_UserTag.Name = "txtSearch_UserTag";
            this.txtSearch_UserTag.ReadOnly = true;
            this.txtSearch_UserTag.Size = new System.Drawing.Size(162, 22);
            this.txtSearch_UserTag.TabIndex = 2;
            // 
            // chkSearch_UserTag
            // 
            this.chkSearch_UserTag.AutoSize = true;
            this.chkSearch_UserTag.Location = new System.Drawing.Point(53, 45);
            this.chkSearch_UserTag.Name = "chkSearch_UserTag";
            this.chkSearch_UserTag.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSearch_UserTag.Size = new System.Drawing.Size(75, 18);
            this.chkSearch_UserTag.TabIndex = 1;
            this.chkSearch_UserTag.Text = "User Tag";
            this.chkSearch_UserTag.UseVisualStyleBackColor = true;
            this.chkSearch_UserTag.CheckedChanged += new System.EventHandler(this.chkSearch_UserTag_CheckedChanged);
            // 
            // txtSearch_Notes
            // 
            this.txtSearch_Notes.BackColor = System.Drawing.Color.White;
            this.txtSearch_Notes.Location = new System.Drawing.Point(134, 67);
            this.txtSearch_Notes.MaxLength = 50;
            this.txtSearch_Notes.Name = "txtSearch_Notes";
            this.txtSearch_Notes.ReadOnly = true;
            this.txtSearch_Notes.Size = new System.Drawing.Size(162, 22);
            this.txtSearch_Notes.TabIndex = 4;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel13);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel14.Size = new System.Drawing.Size(306, 30);
            this.panel14.TabIndex = 18;
            // 
            // panel13
            // 
            this.panel13.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel13.Controls.Add(this.button2);
            this.panel13.Controls.Add(this.button1);
            this.panel13.Controls.Add(this.chkInSearchMode);
            this.panel13.Controls.Add(this.label67);
            this.panel13.Controls.Add(this.label68);
            this.panel13.Controls.Add(this.label69);
            this.panel13.Controls.Add(this.label70);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(303, 24);
            this.panel13.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button2.Location = new System.Drawing.Point(89, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 22);
            this.button2.TabIndex = 18;
            this.button2.Text = "    Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.tsb_Search_Close_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 22);
            this.button1.TabIndex = 17;
            this.button1.Text = "    Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.tsb_Search_Search_Click);
            // 
            // chkInSearchMode
            // 
            this.chkInSearchMode.AutoSize = true;
            this.chkInSearchMode.BackColor = System.Drawing.Color.Transparent;
            this.chkInSearchMode.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkInSearchMode.Location = new System.Drawing.Point(190, 1);
            this.chkInSearchMode.Name = "chkInSearchMode";
            this.chkInSearchMode.Size = new System.Drawing.Size(112, 22);
            this.chkInSearchMode.TabIndex = 16;
            this.chkInSearchMode.Text = "In Search Mode";
            this.chkInSearchMode.UseVisualStyleBackColor = false;
            this.chkInSearchMode.Visible = false;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label67.Location = new System.Drawing.Point(1, 23);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(301, 1);
            this.label67.TabIndex = 12;
            this.label67.Text = "label2";
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label68.Dock = System.Windows.Forms.DockStyle.Left;
            this.label68.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(0, 1);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(1, 23);
            this.label68.TabIndex = 11;
            this.label68.Text = "label4";
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label69.Dock = System.Windows.Forms.DockStyle.Right;
            this.label69.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label69.Location = new System.Drawing.Point(302, 1);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(1, 23);
            this.label69.TabIndex = 10;
            this.label69.Text = "label3";
            // 
            // label70
            // 
            this.label70.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label70.Dock = System.Windows.Forms.DockStyle.Top;
            this.label70.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(0, 0);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(303, 1);
            this.label70.TabIndex = 9;
            this.label70.Text = "label1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Search_Search,
            this.tsb_Search_Close});
            this.toolStrip1.Location = new System.Drawing.Point(66, 121);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(105, 36);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_Search_Search
            // 
            this.tsb_Search_Search.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Search_Search.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Search_Search.Image")));
            this.tsb_Search_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Search_Search.Name = "tsb_Search_Search";
            this.tsb_Search_Search.Size = new System.Drawing.Size(56, 33);
            this.tsb_Search_Search.Text = "Search";
            this.tsb_Search_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Search_Search.Click += new System.EventHandler(this.tsb_Search_Search_Click);
            // 
            // tsb_Search_Close
            // 
            this.tsb_Search_Close.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Search_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Search_Close.Image")));
            this.tsb_Search_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Search_Close.Name = "tsb_Search_Close";
            this.tsb_Search_Close.Size = new System.Drawing.Size(46, 33);
            this.tsb_Search_Close.Text = "Close";
            this.tsb_Search_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Search_Close.Click += new System.EventHandler(this.tsb_Search_Close_Click);
            // 
            // c1Documents
            // 
            this.c1Documents.BackColor = System.Drawing.Color.White;
            this.c1Documents.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Documents.ColumnInfo = "3,0,0,0,0,105,Columns:";
            this.c1Documents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Documents.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Documents.Location = new System.Drawing.Point(3, 0);
            this.c1Documents.Name = "c1Documents";
            this.c1Documents.Rows.DefaultSize = 21;
            this.c1Documents.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Documents.Size = new System.Drawing.Size(303, 241);
            this.c1Documents.StyleInfo = resources.GetString("c1Documents.StyleInfo");
            this.c1Documents.TabIndex = 8;
            this.c1Documents.Tree.NodeImageCollapsed = global::gloEDocumentV3.Properties.Resources.Plus;
            this.c1Documents.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1Documents.Tree.NodeImageExpanded")));
            this.c1Documents.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None;
            this.c1Documents.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_BeforeRowColChange);
            this.c1Documents.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_AfterRowColChange);
            this.c1Documents.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Documents_AfterEdit);
            this.c1Documents.Click += new System.EventHandler(this.c1Documents_Click);
            this.c1Documents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Documents_MouseDown);
            // 
            // pnlDocuments
            // 
            this.pnlDocuments.BackColor = System.Drawing.Color.Transparent;
            this.pnlDocuments.Controls.Add(this.panel5);
            this.pnlDocuments.Controls.Add(this.panel3);
            this.pnlDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDocuments.Location = new System.Drawing.Point(0, 179);
            this.pnlDocuments.Name = "pnlDocuments";
            this.pnlDocuments.Size = new System.Drawing.Size(306, 274);
            this.pnlDocuments.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label57);
            this.panel5.Controls.Add(this.label58);
            this.panel5.Controls.Add(this.label59);
            this.panel5.Controls.Add(this.label65);
            this.panel5.Controls.Add(this.c1Documents);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 30);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel5.Size = new System.Drawing.Size(306, 244);
            this.panel5.TabIndex = 9;
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label57.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label57.Location = new System.Drawing.Point(4, 240);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(301, 1);
            this.label57.TabIndex = 12;
            this.label57.Text = "label2";
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label58.Dock = System.Windows.Forms.DockStyle.Left;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(3, 1);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(1, 240);
            this.label58.TabIndex = 11;
            this.label58.Text = "label4";
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label59.Dock = System.Windows.Forms.DockStyle.Right;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label59.Location = new System.Drawing.Point(305, 1);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(1, 240);
            this.label59.TabIndex = 10;
            this.label59.Text = "label3";
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label65.Dock = System.Windows.Forms.DockStyle.Top;
            this.label65.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(3, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(303, 1);
            this.label65.TabIndex = 9;
            this.label65.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.panel16);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel3.Size = new System.Drawing.Size(306, 30);
            this.panel3.TabIndex = 9;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.Transparent;
            this.panel16.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel16.Controls.Add(this.label60);
            this.panel16.Controls.Add(this.lblDocumentsHeader);
            this.panel16.Controls.Add(this.btnDoc_Up);
            this.panel16.Controls.Add(this.btnDoc_Left);
            this.panel16.Controls.Add(this.label62);
            this.panel16.Controls.Add(this.label63);
            this.panel16.Controls.Add(this.label64);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(3, 3);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(303, 24);
            this.panel16.TabIndex = 0;
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label60.Dock = System.Windows.Forms.DockStyle.Left;
            this.label60.Location = new System.Drawing.Point(0, 1);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(1, 22);
            this.label60.TabIndex = 4;
            this.label60.Text = "label60";
            // 
            // lblDocumentsHeader
            // 
            this.lblDocumentsHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblDocumentsHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDocumentsHeader.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentsHeader.Location = new System.Drawing.Point(0, 1);
            this.lblDocumentsHeader.Name = "lblDocumentsHeader";
            this.lblDocumentsHeader.Size = new System.Drawing.Size(254, 22);
            this.lblDocumentsHeader.TabIndex = 2;
            this.lblDocumentsHeader.Text = "Documents";
            this.lblDocumentsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDoc_Up
            // 
            this.btnDoc_Up.BackColor = System.Drawing.Color.Transparent;
            this.btnDoc_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDoc_Up.FlatAppearance.BorderSize = 0;
            this.btnDoc_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoc_Up.Image = ((System.Drawing.Image)(resources.GetObject("btnDoc_Up.Image")));
            this.btnDoc_Up.Location = new System.Drawing.Point(254, 1);
            this.btnDoc_Up.Name = "btnDoc_Up";
            this.btnDoc_Up.Size = new System.Drawing.Size(24, 22);
            this.btnDoc_Up.TabIndex = 1;
            this.btnDoc_Up.UseVisualStyleBackColor = false;
            this.btnDoc_Up.Visible = false;
            // 
            // btnDoc_Left
            // 
            this.btnDoc_Left.BackColor = System.Drawing.Color.Transparent;
            this.btnDoc_Left.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDoc_Left.FlatAppearance.BorderSize = 0;
            this.btnDoc_Left.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDoc_Left.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDoc_Left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoc_Left.Location = new System.Drawing.Point(278, 1);
            this.btnDoc_Left.Name = "btnDoc_Left";
            this.btnDoc_Left.Size = new System.Drawing.Size(24, 22);
            this.btnDoc_Left.TabIndex = 0;
            this.btnDoc_Left.UseVisualStyleBackColor = false;
            this.btnDoc_Left.Click += new System.EventHandler(this.btnDoc_Left_Click);
            this.btnDoc_Left.MouseLeave += new System.EventHandler(this.btnDoc_Left_MouseLeave);
            this.btnDoc_Left.MouseHover += new System.EventHandler(this.btnDoc_Left_MouseHover);
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Dock = System.Windows.Forms.DockStyle.Top;
            this.label62.Location = new System.Drawing.Point(0, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(302, 1);
            this.label62.TabIndex = 1;
            this.label62.Text = "label62";
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label63.Location = new System.Drawing.Point(0, 23);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(302, 1);
            this.label63.TabIndex = 3;
            this.label63.Text = "label63";
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label64.Dock = System.Windows.Forms.DockStyle.Right;
            this.label64.Location = new System.Drawing.Point(302, 0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(1, 24);
            this.label64.TabIndex = 5;
            this.label64.Text = "label64";
            // 
            // pnlDocumentsLegends
            // 
            this.pnlDocumentsLegends.BackColor = System.Drawing.Color.Transparent;
            this.pnlDocumentsLegends.Controls.Add(this.pnlDocuments);
            this.pnlDocumentsLegends.Controls.Add(this.pnlLegends);
            this.pnlDocumentsLegends.Controls.Add(this.pnlSearch);
            this.pnlDocumentsLegends.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDocumentsLegends.Location = new System.Drawing.Point(0, 0);
            this.pnlDocumentsLegends.Name = "pnlDocumentsLegends";
            this.pnlDocumentsLegends.Size = new System.Drawing.Size(306, 519);
            this.pnlDocumentsLegends.TabIndex = 16;
            this.pnlDocumentsLegends.Visible = false;
            // 
            // pnlLegends
            // 
            this.pnlLegends.BackColor = System.Drawing.Color.Transparent;
            this.pnlLegends.Controls.Add(this.panel9);
            this.pnlLegends.Controls.Add(this.panel7);
            this.pnlLegends.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLegends.Location = new System.Drawing.Point(0, 453);
            this.pnlLegends.Name = "pnlLegends";
            this.pnlLegends.Size = new System.Drawing.Size(306, 66);
            this.pnlLegends.TabIndex = 8;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label1);
            this.panel9.Controls.Add(this.label2);
            this.panel9.Controls.Add(this.numYear);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Controls.Add(this.label66);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 28);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel9.Size = new System.Drawing.Size(306, 38);
            this.panel9.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(4, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 1);
            this.label1.TabIndex = 12;
            this.label1.Text = "label2";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 34);
            this.label2.TabIndex = 11;
            this.label2.Text = "label4";
            // 
            // numYear
            // 
            this.numYear.Location = new System.Drawing.Point(13, 6);
            this.numYear.Maximum = new decimal(new int[] {
            2010,
            0,
            0,
            0});
            this.numYear.Minimum = new decimal(new int[] {
            2006,
            0,
            0,
            0});
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(100, 22);
            this.numYear.TabIndex = 18;
            this.numYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numYear.Value = new decimal(new int[] {
            2006,
            0,
            0,
            0});
            this.numYear.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(305, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 34);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label66.Dock = System.Windows.Forms.DockStyle.Top;
            this.label66.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(3, 0);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(303, 1);
            this.label66.TabIndex = 9;
            this.label66.Text = "label1";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel12);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel7.Size = new System.Drawing.Size(306, 28);
            this.panel7.TabIndex = 19;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Button;
            this.panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel12.Controls.Add(this.label4);
            this.panel12.Controls.Add(this.lblLegends);
            this.panel12.Controls.Add(this.btnLed_Up);
            this.panel12.Controls.Add(this.btnLed_Down);
            this.panel12.Controls.Add(this.label6);
            this.panel12.Controls.Add(this.label7);
            this.panel12.Controls.Add(this.label8);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(3, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(303, 25);
            this.panel12.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "label4";
            // 
            // lblLegends
            // 
            this.lblLegends.BackColor = System.Drawing.Color.Transparent;
            this.lblLegends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLegends.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLegends.Location = new System.Drawing.Point(0, 1);
            this.lblLegends.Name = "lblLegends";
            this.lblLegends.Size = new System.Drawing.Size(254, 23);
            this.lblLegends.TabIndex = 2;
            this.lblLegends.Text = "Legend";
            this.lblLegends.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLed_Up
            // 
            this.btnLed_Up.BackColor = System.Drawing.Color.Transparent;
            this.btnLed_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLed_Up.FlatAppearance.BorderSize = 0;
            this.btnLed_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLed_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLed_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLed_Up.Image = ((System.Drawing.Image)(resources.GetObject("btnLed_Up.Image")));
            this.btnLed_Up.Location = new System.Drawing.Point(254, 1);
            this.btnLed_Up.Name = "btnLed_Up";
            this.btnLed_Up.Size = new System.Drawing.Size(24, 23);
            this.btnLed_Up.TabIndex = 1;
            this.btnLed_Up.UseVisualStyleBackColor = false;
            // 
            // btnLed_Down
            // 
            this.btnLed_Down.BackColor = System.Drawing.Color.Transparent;
            this.btnLed_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLed_Down.FlatAppearance.BorderSize = 0;
            this.btnLed_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLed_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLed_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLed_Down.Location = new System.Drawing.Point(278, 1);
            this.btnLed_Down.Name = "btnLed_Down";
            this.btnLed_Down.Size = new System.Drawing.Size(24, 23);
            this.btnLed_Down.TabIndex = 0;
            this.btnLed_Down.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(302, 1);
            this.label6.TabIndex = 1;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(0, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(302, 1);
            this.label7.TabIndex = 3;
            this.label7.Text = "label7";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(302, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 25);
            this.label8.TabIndex = 5;
            this.label8.Text = "label8";
            // 
            // pnlSmallStrip
            // 
            this.pnlSmallStrip.BackColor = System.Drawing.Color.Transparent;
            this.pnlSmallStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSmallStrip.Controls.Add(this.pnlSmallStripMain);
            this.pnlSmallStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSmallStrip.Location = new System.Drawing.Point(311, 0);
            this.pnlSmallStrip.Name = "pnlSmallStrip";
            this.pnlSmallStrip.Padding = new System.Windows.Forms.Padding(0, 3, 5, 3);
            this.pnlSmallStrip.Size = new System.Drawing.Size(30, 519);
            this.pnlSmallStrip.TabIndex = 27;
            this.pnlSmallStrip.Visible = false;
            // 
            // pnlSmallStripMain
            // 
            this.pnlSmallStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSmallStripMain.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_VerticalBtnStrip;
            this.pnlSmallStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSmallStripMain.Controls.Add(this.ts_SmallStrip);
            this.pnlSmallStripMain.Controls.Add(this.label45);
            this.pnlSmallStripMain.Controls.Add(this.btn_Right);
            this.pnlSmallStripMain.Controls.Add(this.label37);
            this.pnlSmallStripMain.Controls.Add(this.label52);
            this.pnlSmallStripMain.Controls.Add(this.label53);
            this.pnlSmallStripMain.Controls.Add(this.label54);
            this.pnlSmallStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSmallStripMain.Location = new System.Drawing.Point(0, 3);
            this.pnlSmallStripMain.Name = "pnlSmallStripMain";
            this.pnlSmallStripMain.Size = new System.Drawing.Size(25, 513);
            this.pnlSmallStripMain.TabIndex = 17;
            // 
            // ts_SmallStrip
            // 
            this.ts_SmallStrip.BackColor = System.Drawing.Color.Transparent;
            this.ts_SmallStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_SmallStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ts_SmallStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_SmallStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_SmallStrip_btn_Document,
            this.ts_SmallStrip_btn_Legend});
            this.ts_SmallStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.ts_SmallStrip.Location = new System.Drawing.Point(1, 24);
            this.ts_SmallStrip.Name = "ts_SmallStrip";
            this.ts_SmallStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ts_SmallStrip.Size = new System.Drawing.Size(23, 488);
            this.ts_SmallStrip.TabIndex = 21;
            this.ts_SmallStrip.Text = "toolStrip1";
            this.ts_SmallStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // ts_SmallStrip_btn_Document
            // 
            this.ts_SmallStrip_btn_Document.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_SmallStrip_btn_Document.Image = ((System.Drawing.Image)(resources.GetObject("ts_SmallStrip_btn_Document.Image")));
            this.ts_SmallStrip_btn_Document.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_SmallStrip_btn_Document.Name = "ts_SmallStrip_btn_Document";
            this.ts_SmallStrip_btn_Document.Size = new System.Drawing.Size(21, 85);
            this.ts_SmallStrip_btn_Document.Text = "Document";
            this.ts_SmallStrip_btn_Document.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ts_SmallStrip_btn_Document.Click += new System.EventHandler(this.ts_SmallStrip_btn_Document_Click);
            // 
            // ts_SmallStrip_btn_Legend
            // 
            this.ts_SmallStrip_btn_Legend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_SmallStrip_btn_Legend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_SmallStrip_btn_Legend.Name = "ts_SmallStrip_btn_Legend";
            this.ts_SmallStrip_btn_Legend.Size = new System.Drawing.Size(21, 52);
            this.ts_SmallStrip_btn_Legend.Text = "Legend";
            this.ts_SmallStrip_btn_Legend.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ts_SmallStrip_btn_Legend.Visible = false;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Location = new System.Drawing.Point(1, 23);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(23, 1);
            this.label45.TabIndex = 22;
            // 
            // btn_Right
            // 
            this.btn_Right.BackColor = System.Drawing.Color.Transparent;
            this.btn_Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Right.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Right.FlatAppearance.BorderSize = 0;
            this.btn_Right.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Right.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Right.Location = new System.Drawing.Point(1, 1);
            this.btn_Right.Name = "btn_Right";
            this.btn_Right.Size = new System.Drawing.Size(23, 22);
            this.btn_Right.TabIndex = 16;
            this.btn_Right.UseVisualStyleBackColor = false;
            this.btn_Right.Click += new System.EventHandler(this.btn_Right_Click);
            this.btn_Right.MouseLeave += new System.EventHandler(this.btn_Right_MouseLeave_1);
            this.btn_Right.MouseHover += new System.EventHandler(this.btn_Right_MouseHover_1);
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Location = new System.Drawing.Point(1, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(23, 1);
            this.label37.TabIndex = 20;
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Left;
            this.label52.Location = new System.Drawing.Point(0, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(1, 512);
            this.label52.TabIndex = 9;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Right;
            this.label53.Location = new System.Drawing.Point(24, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 512);
            this.label53.TabIndex = 14;
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label54.Location = new System.Drawing.Point(0, 512);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(25, 1);
            this.label54.TabIndex = 17;
            // 
            // pnlPatients
            // 
            this.pnlPatients.BackColor = System.Drawing.Color.Transparent;
            this.pnlPatients.Controls.Add(this.panel19);
            this.pnlPatients.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatients.Location = new System.Drawing.Point(341, 0);
            this.pnlPatients.Name = "pnlPatients";
            this.pnlPatients.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlPatients.Size = new System.Drawing.Size(520, 110);
            this.pnlPatients.TabIndex = 18;
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.label16);
            this.panel19.Controls.Add(this.panel18);
            this.panel19.Controls.Add(this.label15);
            this.panel19.Controls.Add(this.label14);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(0, 3);
            this.panel19.Name = "panel19";
            this.panel19.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.panel19.Size = new System.Drawing.Size(520, 107);
            this.panel19.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1, 106);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(515, 1);
            this.label16.TabIndex = 10;
            this.label16.Text = "label4";
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.panel2);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel18.Location = new System.Drawing.Point(1, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(515, 26);
            this.panel18.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Blue2007;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lblPatients);
            this.panel2.Controls.Add(this.btnPat_Up);
            this.panel2.Controls.Add(this.btnPat_Down);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(515, 26);
            this.panel2.TabIndex = 0;
            // 
            // lblPatients
            // 
            this.lblPatients.BackColor = System.Drawing.Color.Transparent;
            this.lblPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatients.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatients.ForeColor = System.Drawing.Color.White;
            this.lblPatients.Location = new System.Drawing.Point(0, 1);
            this.lblPatients.Name = "lblPatients";
            this.lblPatients.Size = new System.Drawing.Size(467, 24);
            this.lblPatients.TabIndex = 2;
            this.lblPatients.Text = "   Patients";
            this.lblPatients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPat_Up
            // 
            this.btnPat_Up.BackColor = System.Drawing.Color.Transparent;
            this.btnPat_Up.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPat_Up.FlatAppearance.BorderSize = 0;
            this.btnPat_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPat_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPat_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPat_Up.Location = new System.Drawing.Point(467, 1);
            this.btnPat_Up.Name = "btnPat_Up";
            this.btnPat_Up.Size = new System.Drawing.Size(24, 24);
            this.btnPat_Up.TabIndex = 1;
            this.btnPat_Up.UseVisualStyleBackColor = false;
            this.btnPat_Up.Click += new System.EventHandler(this.btnPat_Up_Click);
            this.btnPat_Up.MouseLeave += new System.EventHandler(this.btnPat_Up_MouseLeave);
            this.btnPat_Up.MouseHover += new System.EventHandler(this.btnPat_Up_MouseHover);
            // 
            // btnPat_Down
            // 
            this.btnPat_Down.BackColor = System.Drawing.Color.Transparent;
            this.btnPat_Down.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPat_Down.FlatAppearance.BorderSize = 0;
            this.btnPat_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPat_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPat_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPat_Down.Location = new System.Drawing.Point(491, 1);
            this.btnPat_Down.Name = "btnPat_Down";
            this.btnPat_Down.Size = new System.Drawing.Size(24, 24);
            this.btnPat_Down.TabIndex = 0;
            this.btnPat_Down.UseVisualStyleBackColor = false;
            this.btnPat_Down.Click += new System.EventHandler(this.btnPat_Down_Click);
            this.btnPat_Down.MouseLeave += new System.EventHandler(this.btnPat_Down_MouseLeave);
            this.btnPat_Down.MouseHover += new System.EventHandler(this.btnPat_Down_MouseHover);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(515, 1);
            this.label12.TabIndex = 9;
            this.label12.Text = "label1";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(515, 1);
            this.label9.TabIndex = 10;
            this.label9.Text = "label1";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(516, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 107);
            this.label15.TabIndex = 9;
            this.label15.Text = "label4";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 107);
            this.label14.TabIndex = 8;
            this.label14.Text = "label4";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(341, 191);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(520, 3);
            this.splitter1.TabIndex = 28;
            this.splitter1.TabStop = false;
            this.splitter1.Resize += new System.EventHandler(this.splitter1_Resize);
            // 
            // splitter6
            // 
            this.splitter6.Location = new System.Drawing.Point(306, 0);
            this.splitter6.Name = "splitter6";
            this.splitter6.Size = new System.Drawing.Size(5, 519);
            this.splitter6.TabIndex = 19;
            this.splitter6.TabStop = false;
            // 
            // bwLoadDocument
            // 
            this.bwLoadDocument.WorkerReportsProgress = true;
            this.bwLoadDocument.WorkerSupportsCancellation = true;
            this.bwLoadDocument.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadDocument_DoWork);
            this.bwLoadDocument.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwLoadDocument_ProgressChanged);
            this.bwLoadDocument.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoadDocument_RunWorkerCompleted);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlPreview1);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.pnlPages);
            this.pnlMain.Controls.Add(this.pnlPatients);
            this.pnlMain.Controls.Add(this.pnlNotes);
            this.pnlMain.Controls.Add(this.pnlTags);
            this.pnlMain.Controls.Add(this.pnlSmallStrip);
            this.pnlMain.Controls.Add(this.splitter6);
            this.pnlMain.Controls.Add(this.pnlDocumentsLegends);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 159);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(861, 519);
            this.pnlMain.TabIndex = 13;
            // 
            // tls_MaintainDoc
            // 
            this.tls_MaintainDoc.AddSeparatorsBetweenEachButton = false;
            this.tls_MaintainDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tls_MaintainDoc.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Img_Toolstrip;
            this.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tls_MaintainDoc.ButtonsToHide = ((System.Collections.ArrayList)(resources.GetObject("tls_MaintainDoc.ButtonsToHide")));
            this.tls_MaintainDoc.ConnectionString = null;
            this.tls_MaintainDoc.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText;
            this.tls_MaintainDoc.FinishTemplate = false;
            this.tls_MaintainDoc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_MaintainDoc.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tls_MaintainDoc.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tls_MaintainDoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ChangeYearPrevious,
            this.tsb_ChangeYear,
            this.tsb_ChangeYearNext,
            this.toolStripSeparator6,
            this.tsb_Scan,
            this.tsb_Import,
            this.tsb_CopyDocument,
            this.toolStripSeparator5,
            this.tsb_RotateBack,
            this.tsb_RotateForward,
            this.ToolStripSeparator2,
            this.tsb_Delete,
            this.tsb_DeletePage,
            this.ToolStripSeparator3,
            this.tsb_ShowHideAck,
            this.tsb_Acknowledge,
            this.tsb_ViewAcknowledge,
            this.tsb_Task,
            this.tsb_AddNote,
            this.tsb_AddTags,
            this.tsb_History,
            this.tsb_InsertSign1,
            this.toolStripSeparator4,
            this.tsb_Print,
            this.tsb_Fax,
            this.ToolStripSeparator1,
            this.tsb_PrintAll,
            this.tsb_FaxAll,
            this.tsb_Search,
            this.tsb_Refresh,
            this.tsb_Splitter,
            this.tsb_Archive,
            this.tsb_ShowFileArchivedDocuments,
            this.tsb_Annotate,
            this.tsb_InsertSign,
            this.tsb_ProviderSign,
            this.tsb_Save,
            this.tsb_Close});
            this.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tls_MaintainDoc.Location = new System.Drawing.Point(0, 0);
            this.tls_MaintainDoc.ModuleName = null;
            this.tls_MaintainDoc.Name = "tls_MaintainDoc";
            this.tls_MaintainDoc.Size = new System.Drawing.Size(861, 159);
            this.tls_MaintainDoc.TabIndex = 15;
            this.tls_MaintainDoc.Text = "toolStrip1";
            this.tls_MaintainDoc.UserID = ((long)(0));
            this.tls_MaintainDoc.MouseHover += new System.EventHandler(this.tls_MaintainDoc_MouseHover);
            // 
            // tsb_ChangeYearPrevious
            // 
            this.tsb_ChangeYearPrevious.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ChangeYearPrevious.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ChangeYearPrevious.Image")));
            this.tsb_ChangeYearPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ChangeYearPrevious.Name = "tsb_ChangeYearPrevious";
            this.tsb_ChangeYearPrevious.Size = new System.Drawing.Size(63, 50);
            this.tsb_ChangeYearPrevious.Tag = "ChangeYear";
            this.tsb_ChangeYearPrevious.Text = "&Previous";
            this.tsb_ChangeYearPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ChangeYearPrevious.ToolTipText = "Previous Year";
            this.tsb_ChangeYearPrevious.Click += new System.EventHandler(this.tsb_ChangeYearPrevious_Click);
            // 
            // tsb_ChangeYear
            // 
            this.tsb_ChangeYear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsb_ChangeYear.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ChangeYear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsb_ChangeYear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_ChangeYear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ChangeYear.Name = "tsb_ChangeYear";
            this.tsb_ChangeYear.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.tsb_ChangeYear.Size = new System.Drawing.Size(45, 49);
            this.tsb_ChangeYear.Tag = "ChangeYear";
            this.tsb_ChangeYear.Text = "2008";
            this.tsb_ChangeYear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ChangeYear.ToolTipText = "Year";
            // 
            // tsb_ChangeYearNext
            // 
            this.tsb_ChangeYearNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ChangeYearNext.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ChangeYearNext.Image")));
            this.tsb_ChangeYearNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ChangeYearNext.Name = "tsb_ChangeYearNext";
            this.tsb_ChangeYearNext.Size = new System.Drawing.Size(39, 50);
            this.tsb_ChangeYearNext.Tag = "ChangeYear";
            this.tsb_ChangeYearNext.Text = "&Next";
            this.tsb_ChangeYearNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ChangeYearNext.ToolTipText = "Next Year";
            this.tsb_ChangeYearNext.Click += new System.EventHandler(this.tsb_ChangeYearNext_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AutoSize = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 49);
            // 
            // tsb_Scan
            // 
            this.tsb_Scan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Scan.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Scan.Image")));
            this.tsb_Scan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Scan.Name = "tsb_Scan";
            this.tsb_Scan.Size = new System.Drawing.Size(52, 50);
            this.tsb_Scan.Tag = "Scan";
            this.tsb_Scan.Text = "  &Scan ";
            this.tsb_Scan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Scan.ToolTipText = "Scan Document";
            this.tsb_Scan.Click += new System.EventHandler(this.tsb_Scan_Click);
            // 
            // tsb_Import
            // 
            this.tsb_Import.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Import.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Import.Image")));
            this.tsb_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Import.Name = "tsb_Import";
            this.tsb_Import.Size = new System.Drawing.Size(66, 50);
            this.tsb_Import.Tag = "Import";
            this.tsb_Import.Text = "  &Import ";
            this.tsb_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Import.ToolTipText = "Import Document";
            this.tsb_Import.Click += new System.EventHandler(this.tsb_Import_Click);
            // 
            // tsb_CopyDocument
            // 
            this.tsb_CopyDocument.Enabled = false;
            this.tsb_CopyDocument.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_CopyDocument.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CopyDocument.Image")));
            this.tsb_CopyDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CopyDocument.Name = "tsb_CopyDocument";
            this.tsb_CopyDocument.Size = new System.Drawing.Size(105, 50);
            this.tsb_CopyDocument.Tag = "Copy Exisiting";
            this.tsb_CopyDocument.Text = "  Copy E&xisiting";
            this.tsb_CopyDocument.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_CopyDocument.ToolTipText = "Select VIS Document to Copy";
            this.tsb_CopyDocument.Click += new System.EventHandler(this.tsb_CopyDocument_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 49);
            // 
            // tsb_RotateBack
            // 
            this.tsb_RotateBack.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_RotateBack.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RotateBack.Image")));
            this.tsb_RotateBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RotateBack.Name = "tsb_RotateBack";
            this.tsb_RotateBack.Size = new System.Drawing.Size(86, 50);
            this.tsb_RotateBack.Tag = "RotateBack";
            this.tsb_RotateBack.Text = "Rotate CC&W";
            this.tsb_RotateBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RotateBack.ToolTipText = "Rotate Counterclockwise";
            this.tsb_RotateBack.Click += new System.EventHandler(this.tsb_RotateBack_Click);
            // 
            // tsb_RotateForward
            // 
            this.tsb_RotateForward.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_RotateForward.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RotateForward.Image")));
            this.tsb_RotateForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RotateForward.Name = "tsb_RotateForward";
            this.tsb_RotateForward.Size = new System.Drawing.Size(78, 50);
            this.tsb_RotateForward.Tag = "RotateForward";
            this.tsb_RotateForward.Text = "Rotat&e CW";
            this.tsb_RotateForward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RotateForward.ToolTipText = "Rotate Clockwise";
            this.tsb_RotateForward.Click += new System.EventHandler(this.tsb_RotateForward_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.AutoSize = false;
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 49);
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(50, 50);
            this.tsb_Delete.Tag = "Delete";
            this.tsb_Delete.Text = "&Delete";
            this.tsb_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // tsb_DeletePage
            // 
            this.tsb_DeletePage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_DeletePage.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DeletePage.Image")));
            this.tsb_DeletePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DeletePage.Name = "tsb_DeletePage";
            this.tsb_DeletePage.Size = new System.Drawing.Size(84, 50);
            this.tsb_DeletePage.Tag = "DeletePage";
            this.tsb_DeletePage.Text = "&Delete Page";
            this.tsb_DeletePage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_DeletePage.Visible = false;
            this.tsb_DeletePage.Click += new System.EventHandler(this.tsb_DeletePage_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.AutoSize = false;
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 49);
            // 
            // tsb_ShowHideAck
            // 
            this.tsb_ShowHideAck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowHideAck.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowHideAck.Image")));
            this.tsb_ShowHideAck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowHideAck.Name = "tsb_ShowHideAck";
            this.tsb_ShowHideAck.Size = new System.Drawing.Size(134, 50);
            this.tsb_ShowHideAck.Tag = "View Acknowledged";
            this.tsb_ShowHideAck.Text = "&View Acknowledged";
            this.tsb_ShowHideAck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowHideAck.ToolTipText = "View Acknowledged";
            this.tsb_ShowHideAck.Click += new System.EventHandler(this.tsb_ShowHideAck_Click);
            // 
            // tsb_Acknowledge
            // 
            this.tsb_Acknowledge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Acknowledge.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Acknowledge.Image")));
            this.tsb_Acknowledge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Acknowledge.Name = "tsb_Acknowledge";
            this.tsb_Acknowledge.Size = new System.Drawing.Size(93, 50);
            this.tsb_Acknowledge.Tag = "Acknowledgment";
            this.tsb_Acknowledge.Text = "&Acknowledge";
            this.tsb_Acknowledge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Acknowledge.ToolTipText = "Acknowledge";
            this.tsb_Acknowledge.Click += new System.EventHandler(this.tsb_Acknowledge_Click);
            // 
            // tsb_ViewAcknowledge
            // 
            this.tsb_ViewAcknowledge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ViewAcknowledge.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ViewAcknowledge.Image")));
            this.tsb_ViewAcknowledge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ViewAcknowledge.Name = "tsb_ViewAcknowledge";
            this.tsb_ViewAcknowledge.Size = new System.Drawing.Size(74, 50);
            this.tsb_ViewAcknowledge.Tag = "VWAcknowledgment";
            this.tsb_ViewAcknowledge.Text = "  &View Ack";
            this.tsb_ViewAcknowledge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ViewAcknowledge.ToolTipText = "View Acknowledgment";
            this.tsb_ViewAcknowledge.Visible = false;
            this.tsb_ViewAcknowledge.Click += new System.EventHandler(this.tsb_ViewAcknowledge_Click);
            // 
            // tsb_Task
            // 
            this.tsb_Task.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Task.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Task.Image")));
            this.tsb_Task.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Task.Name = "tsb_Task";
            this.tsb_Task.Size = new System.Drawing.Size(38, 50);
            this.tsb_Task.Tag = "Task";
            this.tsb_Task.Text = "Tas&k";
            this.tsb_Task.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Task.ToolTipText = "Task";
            this.tsb_Task.Click += new System.EventHandler(this.tsb_GenerateTask_Click);
            // 
            // tsb_AddNote
            // 
            this.tsb_AddNote.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AddNote.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AddNote.Image")));
            this.tsb_AddNote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AddNote.Name = "tsb_AddNote";
            this.tsb_AddNote.Size = new System.Drawing.Size(75, 50);
            this.tsb_AddNote.Tag = "AddNotes";
            this.tsb_AddNote.Text = "Add N&otes";
            this.tsb_AddNote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AddNote.Click += new System.EventHandler(this.tsb_AddNote_Click);
            // 
            // tsb_AddTags
            // 
            this.tsb_AddTags.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_AddTags.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AddTags.Image")));
            this.tsb_AddTags.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AddTags.Name = "tsb_AddTags";
            this.tsb_AddTags.Size = new System.Drawing.Size(68, 50);
            this.tsb_AddTags.Tag = "AddNotes";
            this.tsb_AddTags.Text = "&Add Tags";
            this.tsb_AddTags.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AddTags.ToolTipText = "Add Tags";
            this.tsb_AddTags.Click += new System.EventHandler(this.tsb_AddTags_Click);
            // 
            // tsb_History
            // 
            this.tsb_History.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_History.Image = ((System.Drawing.Image)(resources.GetObject("tsb_History.Image")));
            this.tsb_History.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_History.Name = "tsb_History";
            this.tsb_History.Size = new System.Drawing.Size(92, 50);
            this.tsb_History.Tag = "History";
            this.tsb_History.Text = "A&mendments";
            this.tsb_History.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_History.ToolTipText = "Amendments";
            this.tsb_History.Click += new System.EventHandler(this.tsb_History_Click);
            // 
            // tsb_InsertSign1
            // 
            this.tsb_InsertSign1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_InsertSign1.Image = ((System.Drawing.Image)(resources.GetObject("tsb_InsertSign1.Image")));
            this.tsb_InsertSign1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_InsertSign1.Name = "tsb_InsertSign1";
            this.tsb_InsertSign1.Size = new System.Drawing.Size(57, 50);
            this.tsb_InsertSign1.Tag = "";
            this.tsb_InsertSign1.Text = "Int&Sign";
            this.tsb_InsertSign1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_InsertSign1.ToolTipText = "Insert Signature";
            this.tsb_InsertSign1.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 49);
            // 
            // tsb_Print
            // 
            this.tsb_Print.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(41, 50);
            this.tsb_Print.Tag = "Print";
            this.tsb_Print.Text = "&Print";
            this.tsb_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Print.Click += new System.EventHandler(this.tsb_Print_Click);
            // 
            // tsb_Fax
            // 
            this.tsb_Fax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Fax.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Fax.Image")));
            this.tsb_Fax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Fax.Name = "tsb_Fax";
            this.tsb_Fax.Size = new System.Drawing.Size(36, 50);
            this.tsb_Fax.Tag = "Fax";
            this.tsb_Fax.Text = " &Fax";
            this.tsb_Fax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Fax.ToolTipText = "Fax";
            this.tsb_Fax.Click += new System.EventHandler(this.tsb_Fax_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.AutoSize = false;
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 49);
            // 
            // tsb_PrintAll
            // 
            this.tsb_PrintAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PrintAll.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PrintAll.Image")));
            this.tsb_PrintAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PrintAll.Name = "tsb_PrintAll";
            this.tsb_PrintAll.Size = new System.Drawing.Size(60, 50);
            this.tsb_PrintAll.Tag = "PrintAll";
            this.tsb_PrintAll.Text = "P&rint All";
            this.tsb_PrintAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PrintAll.ToolTipText = "Print All";
            this.tsb_PrintAll.Visible = false;
            this.tsb_PrintAll.Click += new System.EventHandler(this.tsb_PrintAll_Click);
            // 
            // tsb_FaxAll
            // 
            this.tsb_FaxAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_FaxAll.Image = ((System.Drawing.Image)(resources.GetObject("tsb_FaxAll.Image")));
            this.tsb_FaxAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_FaxAll.Name = "tsb_FaxAll";
            this.tsb_FaxAll.Size = new System.Drawing.Size(50, 50);
            this.tsb_FaxAll.Tag = "FaxAll";
            this.tsb_FaxAll.Text = "Fa&x All";
            this.tsb_FaxAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_FaxAll.ToolTipText = "Fax All";
            this.tsb_FaxAll.Visible = false;
            this.tsb_FaxAll.Click += new System.EventHandler(this.tsb_FaxAll_Click);
            // 
            // tsb_Search
            // 
            this.tsb_Search.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Search.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Search.Image")));
            this.tsb_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Search.Name = "tsb_Search";
            this.tsb_Search.Size = new System.Drawing.Size(52, 50);
            this.tsb_Search.Tag = "Search";
            this.tsb_Search.Text = "Searc&h";
            this.tsb_Search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Search.ToolTipText = "Search";
            this.tsb_Search.Click += new System.EventHandler(this.tsb_Search_Click);
            // 
            // tsb_Refresh
            // 
            this.tsb_Refresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Refresh.Image")));
            this.tsb_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Refresh.Name = "tsb_Refresh";
            this.tsb_Refresh.Size = new System.Drawing.Size(58, 50);
            this.tsb_Refresh.Tag = "Refresh";
            this.tsb_Refresh.Text = "Re&fresh";
            this.tsb_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Refresh.Click += new System.EventHandler(this.tsb_Refresh_Click);
            // 
            // tsb_Splitter
            // 
            this.tsb_Splitter.AutoSize = false;
            this.tsb_Splitter.Name = "tsb_Splitter";
            this.tsb_Splitter.Size = new System.Drawing.Size(6, 49);
            // 
            // tsb_Archive
            // 
            this.tsb_Archive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Archive.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Archive.Image")));
            this.tsb_Archive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Archive.Name = "tsb_Archive";
            this.tsb_Archive.Size = new System.Drawing.Size(59, 50);
            this.tsb_Archive.Tag = "Restore";
            this.tsb_Archive.Text = "&Restore";
            this.tsb_Archive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Archive.ToolTipText = "Restore Documents";
            this.tsb_Archive.Visible = false;
            this.tsb_Archive.Click += new System.EventHandler(this.tsb_Archive_Click);
            // 
            // tsb_Annotate
            // 
            this.tsb_Annotate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsannot_Signature,
            this.stsannot_ProviderSign,
            this.stsannot_Checkmark,
            this.stsannot_Line,
            this.stsannot_Rectangle,
            this.stsannote_Ellipse,
            this.stsannot_Arrow,
            this.sts_Freehand,
            this.stsannot_Seperator1,
            this.stsannot_Editing,
            this.stsannot_Undo,
            this.stsannot_ClearAll,
            this.stsannot_Seperator2,
            this.textAnnotationToolStripMenuItem,
            this.stsannot_Save,
            this.stamperToolStripMenuItem});
            this.tsb_Annotate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_Annotate.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Annotate.Image")));
            this.tsb_Annotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Annotate.Name = "tsb_Annotate";
            this.tsb_Annotate.Size = new System.Drawing.Size(82, 50);
            this.tsb_Annotate.Tag = "Annotate";
            this.tsb_Annotate.Text = "Annotat&e";
            this.tsb_Annotate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // stsannot_Signature
            // 
            this.stsannot_Signature.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_Signature.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_Signature.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_Signature.Image")));
            this.stsannot_Signature.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_Signature.Name = "stsannot_Signature";
            this.stsannot_Signature.Size = new System.Drawing.Size(189, 22);
            this.stsannot_Signature.Text = "Signature";
            this.stsannot_Signature.Click += new System.EventHandler(this.stsannot_Signature_Click);
            // 
            // stsannot_ProviderSign
            // 
            this.stsannot_ProviderSign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_ProviderSign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_ProviderSign.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_ProviderSign.Image")));
            this.stsannot_ProviderSign.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_ProviderSign.Name = "stsannot_ProviderSign";
            this.stsannot_ProviderSign.Size = new System.Drawing.Size(189, 22);
            this.stsannot_ProviderSign.Text = "Provider Signature";
            this.stsannot_ProviderSign.Click += new System.EventHandler(this.stsannot_ProviderSign_Click);
            // 
            // stsannot_Checkmark
            // 
            this.stsannot_Checkmark.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_Checkmark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_Checkmark.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_Checkmark.Image")));
            this.stsannot_Checkmark.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_Checkmark.Name = "stsannot_Checkmark";
            this.stsannot_Checkmark.Size = new System.Drawing.Size(189, 22);
            this.stsannot_Checkmark.Text = "Checkmark";
            this.stsannot_Checkmark.Click += new System.EventHandler(this.stsannot_Checkmark_Click);
            // 
            // stsannot_Line
            // 
            this.stsannot_Line.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_Line.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_Line.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_Line.Image")));
            this.stsannot_Line.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_Line.Name = "stsannot_Line";
            this.stsannot_Line.Size = new System.Drawing.Size(189, 22);
            this.stsannot_Line.Text = "Line";
            this.stsannot_Line.Click += new System.EventHandler(this.stsannot_Line_Click);
            // 
            // stsannot_Rectangle
            // 
            this.stsannot_Rectangle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_Rectangle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_Rectangle.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_Rectangle.Image")));
            this.stsannot_Rectangle.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_Rectangle.Name = "stsannot_Rectangle";
            this.stsannot_Rectangle.Size = new System.Drawing.Size(189, 22);
            this.stsannot_Rectangle.Text = "Rectangle";
            this.stsannot_Rectangle.Click += new System.EventHandler(this.stsannot_Rectangle_Click);
            // 
            // stsannote_Ellipse
            // 
            this.stsannote_Ellipse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannote_Ellipse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannote_Ellipse.Image = ((System.Drawing.Image)(resources.GetObject("stsannote_Ellipse.Image")));
            this.stsannote_Ellipse.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannote_Ellipse.Name = "stsannote_Ellipse";
            this.stsannote_Ellipse.Size = new System.Drawing.Size(189, 22);
            this.stsannote_Ellipse.Text = "Ellipse";
            this.stsannote_Ellipse.Click += new System.EventHandler(this.stsannote_Ellipse_Click);
            // 
            // stsannot_Arrow
            // 
            this.stsannot_Arrow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_Arrow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_Arrow.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_Arrow.Image")));
            this.stsannot_Arrow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_Arrow.Name = "stsannot_Arrow";
            this.stsannot_Arrow.Size = new System.Drawing.Size(189, 22);
            this.stsannot_Arrow.Text = "Arrow";
            this.stsannot_Arrow.Click += new System.EventHandler(this.stsannot_Arrow_Click);
            // 
            // sts_Freehand
            // 
            this.sts_Freehand.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sts_Freehand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.sts_Freehand.Image = ((System.Drawing.Image)(resources.GetObject("sts_Freehand.Image")));
            this.sts_Freehand.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sts_Freehand.Name = "sts_Freehand";
            this.sts_Freehand.Size = new System.Drawing.Size(189, 22);
            this.sts_Freehand.Text = "Free Hand";
            this.sts_Freehand.Click += new System.EventHandler(this.sts_Freehand_Click);
            // 
            // stsannot_Seperator1
            // 
            this.stsannot_Seperator1.Name = "stsannot_Seperator1";
            this.stsannot_Seperator1.Size = new System.Drawing.Size(186, 6);
            // 
            // stsannot_Editing
            // 
            this.stsannot_Editing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_Editing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_Editing.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_Editing.Image")));
            this.stsannot_Editing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_Editing.Name = "stsannot_Editing";
            this.stsannot_Editing.Size = new System.Drawing.Size(189, 22);
            this.stsannot_Editing.Text = "Edit";
            this.stsannot_Editing.Click += new System.EventHandler(this.stsannot_Editing_Click);
            // 
            // stsannot_Undo
            // 
            this.stsannot_Undo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_Undo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_Undo.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_Undo.Image")));
            this.stsannot_Undo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_Undo.Name = "stsannot_Undo";
            this.stsannot_Undo.Size = new System.Drawing.Size(189, 22);
            this.stsannot_Undo.Text = "Undo";
            this.stsannot_Undo.Click += new System.EventHandler(this.stsannot_Undo_Click);
            // 
            // stsannot_ClearAll
            // 
            this.stsannot_ClearAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_ClearAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_ClearAll.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_ClearAll.Image")));
            this.stsannot_ClearAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_ClearAll.Name = "stsannot_ClearAll";
            this.stsannot_ClearAll.Size = new System.Drawing.Size(189, 22);
            this.stsannot_ClearAll.Text = "Clear All";
            this.stsannot_ClearAll.Click += new System.EventHandler(this.stsannot_ClearAll_Click_1);
            // 
            // stsannot_Seperator2
            // 
            this.stsannot_Seperator2.Name = "stsannot_Seperator2";
            this.stsannot_Seperator2.Size = new System.Drawing.Size(186, 6);
            // 
            // textAnnotationToolStripMenuItem
            // 
            this.textAnnotationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddText,
            this.mnuModifyText});
            this.textAnnotationToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.textAnnotationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("textAnnotationToolStripMenuItem.Image")));
            this.textAnnotationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.textAnnotationToolStripMenuItem.Name = "textAnnotationToolStripMenuItem";
            this.textAnnotationToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.textAnnotationToolStripMenuItem.Text = "Text Annotation";
            // 
            // mnuAddText
            // 
            this.mnuAddText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuAddText.Image = ((System.Drawing.Image)(resources.GetObject("mnuAddText.Image")));
            this.mnuAddText.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuAddText.Name = "mnuAddText";
            this.mnuAddText.Size = new System.Drawing.Size(147, 22);
            this.mnuAddText.Text = "Add Text";
            this.mnuAddText.Click += new System.EventHandler(this.mnuAddText_Click);
            // 
            // mnuModifyText
            // 
            this.mnuModifyText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuModifyText.Image = ((System.Drawing.Image)(resources.GetObject("mnuModifyText.Image")));
            this.mnuModifyText.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuModifyText.Name = "mnuModifyText";
            this.mnuModifyText.Size = new System.Drawing.Size(147, 22);
            this.mnuModifyText.Text = "Modify Text";
            this.mnuModifyText.Click += new System.EventHandler(this.mnuModifyText_Click);
            // 
            // stsannot_Save
            // 
            this.stsannot_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsannot_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stsannot_Save.Image = ((System.Drawing.Image)(resources.GetObject("stsannot_Save.Image")));
            this.stsannot_Save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stsannot_Save.Name = "stsannot_Save";
            this.stsannot_Save.Size = new System.Drawing.Size(189, 22);
            this.stsannot_Save.Text = "Save";
            this.stsannot_Save.Click += new System.EventHandler(this.stsannot_Save_Click);
            // 
            // stamperToolStripMenuItem
            // 
            this.stamperToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stamperToolStripMenuItem.Name = "stamperToolStripMenuItem";
            this.stamperToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.stamperToolStripMenuItem.Text = "Stamper";
            this.stamperToolStripMenuItem.Visible = false;
            this.stamperToolStripMenuItem.Click += new System.EventHandler(this.stamperToolStripMenuItem_Click);
            // 
            // tsb_InsertSign
            // 
            this.tsb_InsertSign.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stb_Signature_with_Text,
            this.stb_Signature_with_Acknowledgement,
            this.stb_Signature_with_Notes,
            this.stb_Signature_with_Acknowledgement_Notes});
            this.tsb_InsertSign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_InsertSign.Image = ((System.Drawing.Image)(resources.GetObject("tsb_InsertSign.Image")));
            this.tsb_InsertSign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_InsertSign.Name = "tsb_InsertSign";
            this.tsb_InsertSign.Size = new System.Drawing.Size(50, 50);
            this.tsb_InsertSign.Tag = "Sign";
            this.tsb_InsertSign.Text = "Sig&n";
            this.tsb_InsertSign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_InsertSign.ToolTipText = "Sign";
            this.tsb_InsertSign.ButtonClick += new System.EventHandler(this.tsb_InsertSign_ButtonClick);
            // 
            // stb_Signature_with_Text
            // 
            this.stb_Signature_with_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stb_Signature_with_Text.Image = ((System.Drawing.Image)(resources.GetObject("stb_Signature_with_Text.Image")));
            this.stb_Signature_with_Text.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stb_Signature_with_Text.Name = "stb_Signature_with_Text";
            this.stb_Signature_with_Text.Size = new System.Drawing.Size(252, 22);
            this.stb_Signature_with_Text.Text = "Sign";
            this.stb_Signature_with_Text.Click += new System.EventHandler(this.stb_Signature_with_Text_Click);
            // 
            // stb_Signature_with_Acknowledgement
            // 
            this.stb_Signature_with_Acknowledgement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stb_Signature_with_Acknowledgement.Image = ((System.Drawing.Image)(resources.GetObject("stb_Signature_with_Acknowledgement.Image")));
            this.stb_Signature_with_Acknowledgement.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stb_Signature_with_Acknowledgement.Name = "stb_Signature_with_Acknowledgement";
            this.stb_Signature_with_Acknowledgement.Size = new System.Drawing.Size(252, 22);
            this.stb_Signature_with_Acknowledgement.Text = "Sign && Acknowledge";
            this.stb_Signature_with_Acknowledgement.Click += new System.EventHandler(this.stb_Signature_with_Acknowledgement_Click);
            // 
            // stb_Signature_with_Notes
            // 
            this.stb_Signature_with_Notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stb_Signature_with_Notes.Image = ((System.Drawing.Image)(resources.GetObject("stb_Signature_with_Notes.Image")));
            this.stb_Signature_with_Notes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stb_Signature_with_Notes.Name = "stb_Signature_with_Notes";
            this.stb_Signature_with_Notes.Size = new System.Drawing.Size(252, 22);
            this.stb_Signature_with_Notes.Text = "Sign && Notes";
            this.stb_Signature_with_Notes.Click += new System.EventHandler(this.stb_Signature_with_Notes_Click);
            // 
            // stb_Signature_with_Acknowledgement_Notes
            // 
            this.stb_Signature_with_Acknowledgement_Notes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.stb_Signature_with_Acknowledgement_Notes.Image = ((System.Drawing.Image)(resources.GetObject("stb_Signature_with_Acknowledgement_Notes.Image")));
            this.stb_Signature_with_Acknowledgement_Notes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stb_Signature_with_Acknowledgement_Notes.Name = "stb_Signature_with_Acknowledgement_Notes";
            this.stb_Signature_with_Acknowledgement_Notes.Size = new System.Drawing.Size(252, 22);
            this.stb_Signature_with_Acknowledgement_Notes.Text = "Sign && Acknowledge && Notes";
            this.stb_Signature_with_Acknowledgement_Notes.Click += new System.EventHandler(this.stb_Signature_with_Acknowledgement_Notes_Click);
            // 
            // tsb_ProviderSign
            // 
            this.tsb_ProviderSign.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_ProviderSign.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ProviderSign.Image")));
            this.tsb_ProviderSign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ProviderSign.Name = "tsb_ProviderSign";
            this.tsb_ProviderSign.Size = new System.Drawing.Size(105, 50);
            this.tsb_ProviderSign.Tag = "Provide Sign";
            this.tsb_ProviderSign.Text = "Pro&vider Sign";
            this.tsb_ProviderSign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ProviderSign.ButtonClick += new System.EventHandler(this.tsb_ProviderSign_ButtonClick_1);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.ToolTipText = "Close";
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // tsb_ShowFileArchivedDocuments
            // 
            this.tsb_ShowFileArchivedDocuments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowFileArchivedDocuments.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowFileArchivedDocuments.Image")));
            this.tsb_ShowFileArchivedDocuments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowFileArchivedDocuments.Name = "tsb_ShowFileArchivedDocuments";
            this.tsb_ShowFileArchivedDocuments.Size = new System.Drawing.Size(128, 50);
            this.tsb_ShowFileArchivedDocuments.Tag = "ViewArchivedDocuments";
            this.tsb_ShowFileArchivedDocuments.Text = "View Ar&chived Doc.";
            this.tsb_ShowFileArchivedDocuments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowFileArchivedDocuments.ToolTipText = "View Archived Documents";
            this.tsb_ShowFileArchivedDocuments.Visible = false;
            this.tsb_ShowFileArchivedDocuments.Click += new System.EventHandler(this.tsb_ShowFileArchivedDocuments_Click);
            // 
            // frmEDocumentViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(861, 678);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tls_MaintainDoc);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmEDocumentViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Documents V3";
            this.Activated += new System.EventHandler(this.frmEDocumentViewer_Activated);
            this.Deactivate += new System.EventHandler(this.frmEDocumentViewer_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEDocumentViewer_FormClosing);
            this.Load += new System.EventHandler(this.frmEDocumentViewer_Load);
            this.pnlPages.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlPreview1.ResumeLayout(false);
            this.pnlPreview.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.pnlDocumentPreviewCommand2.ResumeLayout(false);
            this.pnlDocumentPreviewCommand2.PerformLayout();
            this.pnlDocumentPreviewCommand1.ResumeLayout(false);
            this.pnlNotes.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.pnlTags.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Documents)).EndInit();
            this.pnlDocuments.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.pnlDocumentsLegends.ResumeLayout(false);
            this.pnlLegends.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.pnlSmallStrip.ResumeLayout(false);
            this.pnlSmallStripMain.ResumeLayout(false);
            this.pnlSmallStripMain.PerformLayout();
            this.ts_SmallStrip.ResumeLayout(false);
            this.ts_SmallStrip.PerformLayout();
            this.pnlPatients.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.tls_MaintainDoc.ResumeLayout(false);
            this.tls_MaintainDoc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNote_Down;
        private System.Windows.Forms.Panel pnlPages;
        private System.Windows.Forms.ListView lvwPages;
        private System.Windows.Forms.ImageList imgPages;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnPageView_Large;
        private System.Windows.Forms.Button btnPageView_Small;
        private System.Windows.Forms.Button btnPageView_List;
        private System.Windows.Forms.Label lblPagesHeader;
        private System.Windows.Forms.Button btnPageView_Tile;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Button btnNote_Up;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbZoomPercentage;
        private System.Windows.Forms.Panel pnlDocumentPreviewCommand1;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Panel pnlDocumentPreviewCommand2;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button btnPrv_Up;
        private System.Windows.Forms.Button btnPrv_Down;
        private System.Windows.Forms.Panel pnlPreview1;
        private System.Windows.Forms.RichTextBox txtNotes;
        private System.Windows.Forms.Button btn_Right;
        private System.Windows.Forms.Label lblTags;
        private System.Windows.Forms.Button btnTag_Up;
        private System.Windows.Forms.Button btnTag_Down;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label lblSplNoteBottom;
        private System.Windows.Forms.Label lblSplNoteTop;
        private gloGlobal.gloToolStripIgnoreFocus ts_SmallStrip;
        private System.Windows.Forms.ToolStripButton ts_SmallStrip_btn_Document;
        private System.Windows.Forms.ToolStripButton ts_SmallStrip_btn_Legend;
        private System.Windows.Forms.Panel pnlNotes;
        private System.Windows.Forms.Panel pnlTags;
        private System.Windows.Forms.TextBox txtTags;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolStripButton tsb_RotateBack;
        internal System.Windows.Forms.ToolStripSeparator tsb_Splitter;
        private System.Windows.Forms.ToolStripButton tsb_RotateForward;
        private System.Windows.Forms.ToolStripButton tsb_Refresh;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsb_FaxAll;
        private System.Windows.Forms.ToolStripButton tsb_Fax;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsb_AddTags;
        private System.Windows.Forms.ToolStripButton tsb_AddNote;
        private System.Windows.Forms.Panel pnlSmallStrip;
        private System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsb_Search;
        private System.Windows.Forms.ToolStripButton tsb_History;
        private System.Windows.Forms.ToolStripButton tsb_PrintAll;
        private System.Windows.Forms.ToolStripButton tsb_ChangeYearPrevious;
        private System.Windows.Forms.ToolStripButton tsb_ChangeYearNext;
        private System.Windows.Forms.ToolStripButton tsb_DeletePage;
        private System.Windows.Forms.ToolStripButton tsb_Delete;
        private System.Windows.Forms.ToolStripButton tsb_InsertSign1;
        private System.Windows.Forms.ToolStripButton tsb_Print;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_ViewAcknowledge;
        private System.Windows.Forms.ToolStripButton tsb_Acknowledge;
        private System.Windows.Forms.ToolStripButton tsb_Task;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsb_Import;
        private System.Windows.Forms.ToolStripButton tsb_Scan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearch_DocumentName;
        private System.Windows.Forms.CheckBox chkSearch_PageName;
        private System.Windows.Forms.Label lblLegends;
        private System.Windows.Forms.Button btnLed_Up;
        private System.Windows.Forms.CheckBox chkSearch_DocumentName;
        private System.Windows.Forms.TextBox txtSearch_PageName;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button btnLed_Down;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkInSearchMode;
        private System.Windows.Forms.CheckBox chkSearch_Acknowledge;
        private gloGlobal.gloToolStripIgnoreFocus toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_Search_Search;
        private System.Windows.Forms.ToolStripButton tsb_Search_Close;
        private System.Windows.Forms.TextBox txtSearch_Acknowledge;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.CheckBox chkSearch_Notes;
        private System.Windows.Forms.TextBox txtSearch_Notes;
        private System.Windows.Forms.CheckBox chkSearch_UserTag;
        private System.Windows.Forms.TextBox txtSearch_UserTag;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Documents;
        private System.Windows.Forms.Panel pnlDocuments;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label lblDocumentsHeader;
        private System.Windows.Forms.Button btnDoc_Up;
        private System.Windows.Forms.Button btnDoc_Left;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Panel pnlDocumentsLegends;
        private System.Windows.Forms.Panel pnlLegends;
        private System.Windows.Forms.NumericUpDown numYear;
        private gloToolStrip.gloToolStrip tls_MaintainDoc;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblPatients;
        private System.Windows.Forms.Button btnPat_Up;
        private System.Windows.Forms.Button btnPat_Down;
        private System.Windows.Forms.Panel pnlPatients;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Panel panel23;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel pnlSmallStripMain;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Splitter splitter6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsb_Archive;
        private System.Windows.Forms.ToolStripLabel tsb_ChangeYear;
        private System.Windows.Forms.ToolStripSplitButton tsb_Annotate;
        private System.Windows.Forms.ToolStripMenuItem stsannot_Line;
        private System.Windows.Forms.ToolStripMenuItem stsannot_Rectangle;
        private System.Windows.Forms.ToolStripMenuItem stsannote_Ellipse;
        private System.Windows.Forms.ToolStripMenuItem stsannot_Arrow;
        private System.Windows.Forms.ToolStripMenuItem sts_Freehand;
        private System.Windows.Forms.ToolStripSeparator stsannot_Seperator1;
        private System.Windows.Forms.ToolStripMenuItem stsannot_Editing;
        private System.Windows.Forms.ToolStripSeparator stsannot_Seperator2;
        private System.Windows.Forms.ToolStripMenuItem stsannot_Save;
        private System.Windows.Forms.ToolStripMenuItem stsannot_Signature;
        private System.Windows.Forms.ToolStripMenuItem stsannot_Undo;
        private System.Windows.Forms.ToolStripMenuItem stsannot_ClearAll;
        private System.Windows.Forms.ToolStripMenuItem stsannot_Checkmark;
        private System.Windows.Forms.ToolStripSplitButton tsb_InsertSign;
        private System.Windows.Forms.ToolStripMenuItem stb_Signature_with_Acknowledgement;
        private System.Windows.Forms.ToolStripMenuItem stb_Signature_with_Notes;
        private System.Windows.Forms.ToolStripMenuItem stb_Signature_with_Acknowledgement_Notes;
        private System.Windows.Forms.ToolStripMenuItem stb_Signature_with_Text;
        private System.Windows.Forms.ToolStripSplitButton tsb_ProviderSign;
        private System.Windows.Forms.ToolStripMenuItem stsannot_ProviderSign;
        private System.ComponentModel.BackgroundWorker bwLoadDocument;
        private System.Windows.Forms.ToolStripButton tsb_CopyDocument;
        private System.Windows.Forms.ToolStripMenuItem textAnnotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAddText;
        private System.Windows.Forms.ToolStripMenuItem mnuModifyText;
        private System.Windows.Forms.ToolStripMenuItem stamperToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblAlertMessage;
        private System.Windows.Forms.Label lbl_DocDateTime;
        private System.Windows.Forms.ToolStripButton tsb_ShowHideAck;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbSearchYear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripButton tsb_ShowFileArchivedDocuments;
    }
}
