namespace gloOffice
{
    partial class frmSetupTemplateGallery
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupTemplateGallery));
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trvDataDictionary = new System.Windows.Forms.TreeView();
            this.imgCommon = new System.Windows.Forms.ImageList(this.components);
            this.pnlAddLable = new System.Windows.Forms.Panel();
            this.chk_IncludeLable = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnltrvSerchAppBook = new System.Windows.Forms.Panel();
            this.txtSearchDataDictionary = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnltrvSerchAppBookBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookRightBrd = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_PatientStatement = new System.Windows.Forms.ToolStripButton();
            this.tsb_New = new System.Windows.Forms.ToolStripButton();
            this.tsb_Open = new System.Windows.Forms.ToolStripButton();
            this.tsb_Header = new System.Windows.Forms.ToolStripButton();
            this.tsb_InsertFile = new System.Windows.Forms.ToolStripButton();
            this.tsb_InsertCheckBox = new System.Windows.Forms.ToolStripButton();
            this.tsb_InsertDropDown = new System.Windows.Forms.ToolStripButton();
            this.tsb_Undo = new System.Windows.Forms.ToolStripButton();
            this.tsb_Redo = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveAndClose = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlTemplate = new System.Windows.Forms.Panel();
            this.wdTemplate = new AxDSOFramer.AxFramerControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTemplateDetails = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlAddLable.SuspendLayout();
            this.pnltrvSerchAppBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).BeginInit();
            this.pnlTemplateDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.panel1);
            this.pnlLeft.Controls.Add(this.pnlAddLable);
            this.pnlLeft.Controls.Add(this.pnltrvSerchAppBook);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 53);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(233, 541);
            this.pnlLeft.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.trvDataDictionary);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(233, 485);
            this.panel1.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(4, 484);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(228, 1);
            this.label8.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(232, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 484);
            this.label11.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(229, 1);
            this.label5.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 485);
            this.label9.TabIndex = 15;
            // 
            // trvDataDictionary
            // 
            this.trvDataDictionary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvDataDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDataDictionary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvDataDictionary.ForeColor = System.Drawing.Color.Black;
            this.trvDataDictionary.HideSelection = false;
            this.trvDataDictionary.ImageIndex = 1;
            this.trvDataDictionary.ImageList = this.imgCommon;
            this.trvDataDictionary.Indent = 20;
            this.trvDataDictionary.ItemHeight = 20;
            this.trvDataDictionary.Location = new System.Drawing.Point(3, 0);
            this.trvDataDictionary.Name = "trvDataDictionary";
            this.trvDataDictionary.SelectedImageIndex = 1;
            this.trvDataDictionary.ShowLines = false;
            this.trvDataDictionary.ShowNodeToolTips = true;
            this.trvDataDictionary.Size = new System.Drawing.Size(230, 485);
            this.trvDataDictionary.TabIndex = 0;
            this.trvDataDictionary.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDataDictionary_NodeMouseClick);
            this.trvDataDictionary.DoubleClick += new System.EventHandler(this.trvDataDictionary_DoubleClick);
            this.trvDataDictionary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trvDataDictionary_KeyPress);
            // 
            // imgCommon
            // 
            this.imgCommon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgCommon.ImageStream")));
            this.imgCommon.TransparentColor = System.Drawing.Color.Transparent;
            this.imgCommon.Images.SetKeyName(0, "Bullet06.ico");
            this.imgCommon.Images.SetKeyName(1, "Small Arrow.ico");
            // 
            // pnlAddLable
            // 
            this.pnlAddLable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlAddLable.Controls.Add(this.chk_IncludeLable);
            this.pnlAddLable.Controls.Add(this.label13);
            this.pnlAddLable.Controls.Add(this.label17);
            this.pnlAddLable.Controls.Add(this.label18);
            this.pnlAddLable.Controls.Add(this.label20);
            this.pnlAddLable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAddLable.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlAddLable.Location = new System.Drawing.Point(0, 513);
            this.pnlAddLable.Name = "pnlAddLable";
            this.pnlAddLable.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlAddLable.Size = new System.Drawing.Size(233, 28);
            this.pnlAddLable.TabIndex = 3;
            // 
            // chk_IncludeLable
            // 
            this.chk_IncludeLable.AutoSize = true;
            this.chk_IncludeLable.Location = new System.Drawing.Point(11, 5);
            this.chk_IncludeLable.Name = "chk_IncludeLable";
            this.chk_IncludeLable.Size = new System.Drawing.Size(98, 18);
            this.chk_IncludeLable.TabIndex = 0;
            this.chk_IncludeLable.Text = "Include Label";
            this.chk_IncludeLable.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(3, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 20);
            this.label13.TabIndex = 41;
            this.label13.Text = "label4";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Location = new System.Drawing.Point(3, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(229, 1);
            this.label17.TabIndex = 35;
            this.label17.Text = "label1";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(3, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(229, 1);
            this.label18.TabIndex = 36;
            this.label18.Text = "label1";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(232, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 22);
            this.label20.TabIndex = 40;
            this.label20.Text = "label4";
            // 
            // pnltrvSerchAppBook
            // 
            this.pnltrvSerchAppBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltrvSerchAppBook.Controls.Add(this.txtSearchDataDictionary);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnltrvSerchAppBook.Controls.Add(this.PicBx_Search);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_pnltrvSerchAppBookBottomBrd);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_pnltrvSerchAppBookTopBrd);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_pnltrvSerchAppBookLeftBrd);
            this.pnltrvSerchAppBook.Controls.Add(this.lbl_pnltrvSerchAppBookRightBrd);
            this.pnltrvSerchAppBook.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltrvSerchAppBook.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnltrvSerchAppBook.ForeColor = System.Drawing.Color.Black;
            this.pnltrvSerchAppBook.Location = new System.Drawing.Point(0, 0);
            this.pnltrvSerchAppBook.Name = "pnltrvSerchAppBook";
            this.pnltrvSerchAppBook.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnltrvSerchAppBook.Size = new System.Drawing.Size(233, 28);
            this.pnltrvSerchAppBook.TabIndex = 0;
            // 
            // txtSearchDataDictionary
            // 
            this.txtSearchDataDictionary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchDataDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchDataDictionary.ForeColor = System.Drawing.Color.Black;
            this.txtSearchDataDictionary.Location = new System.Drawing.Point(32, 8);
            this.txtSearchDataDictionary.Name = "txtSearchDataDictionary";
            this.txtSearchDataDictionary.Size = new System.Drawing.Size(200, 15);
            this.txtSearchDataDictionary.TabIndex = 0;
            this.txtSearchDataDictionary.TextChanged += new System.EventHandler(this.txtSearchDataDictionary_TextChanged);
            this.txtSearchDataDictionary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchDataDictionary_KeyPress);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(32, 4);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(200, 4);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(32, 22);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(200, 2);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(4, 4);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 20);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnltrvSerchAppBookBottomBrd
            // 
            this.lbl_pnltrvSerchAppBookBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnltrvSerchAppBookBottomBrd.Location = new System.Drawing.Point(4, 24);
            this.lbl_pnltrvSerchAppBookBottomBrd.Name = "lbl_pnltrvSerchAppBookBottomBrd";
            this.lbl_pnltrvSerchAppBookBottomBrd.Size = new System.Drawing.Size(228, 1);
            this.lbl_pnltrvSerchAppBookBottomBrd.TabIndex = 35;
            this.lbl_pnltrvSerchAppBookBottomBrd.Text = "label1";
            // 
            // lbl_pnltrvSerchAppBookTopBrd
            // 
            this.lbl_pnltrvSerchAppBookTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnltrvSerchAppBookTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnltrvSerchAppBookTopBrd.Name = "lbl_pnltrvSerchAppBookTopBrd";
            this.lbl_pnltrvSerchAppBookTopBrd.Size = new System.Drawing.Size(228, 1);
            this.lbl_pnltrvSerchAppBookTopBrd.TabIndex = 36;
            this.lbl_pnltrvSerchAppBookTopBrd.Text = "label1";
            // 
            // lbl_pnltrvSerchAppBookLeftBrd
            // 
            this.lbl_pnltrvSerchAppBookLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnltrvSerchAppBookLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnltrvSerchAppBookLeftBrd.Name = "lbl_pnltrvSerchAppBookLeftBrd";
            this.lbl_pnltrvSerchAppBookLeftBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnltrvSerchAppBookLeftBrd.TabIndex = 39;
            this.lbl_pnltrvSerchAppBookLeftBrd.Text = "label4";
            // 
            // lbl_pnltrvSerchAppBookRightBrd
            // 
            this.lbl_pnltrvSerchAppBookRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnltrvSerchAppBookRightBrd.Location = new System.Drawing.Point(232, 3);
            this.lbl_pnltrvSerchAppBookRightBrd.Name = "lbl_pnltrvSerchAppBookRightBrd";
            this.lbl_pnltrvSerchAppBookRightBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnltrvSerchAppBookRightBrd.TabIndex = 40;
            this.lbl_pnltrvSerchAppBookRightBrd.Text = "label4";
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(871, 53);
            this.pnlToolStrip.TabIndex = 3;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_PatientStatement,
            this.tsb_New,
            this.tsb_Open,
            this.tsb_Header,
            this.tsb_InsertFile,
            this.tsb_InsertCheckBox,
            this.tsb_InsertDropDown,
            this.tsb_Undo,
            this.tsb_Redo,
            this.tsb_Save,
            this.tsb_SaveAs,
            this.tsb_SaveAndClose,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(871, 53);
            this.ts_Commands.TabIndex = 10;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_PatientStatement
            // 
            this.tsb_PatientStatement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatientStatement.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientStatement.Image")));
            this.tsb_PatientStatement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientStatement.Name = "tsb_PatientStatement";
            this.tsb_PatientStatement.Size = new System.Drawing.Size(122, 50);
            this.tsb_PatientStatement.Tag = "Design Statement";
            this.tsb_PatientStatement.Text = "&Design Statement";
            this.tsb_PatientStatement.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_PatientStatement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatientStatement.Visible = false;
            this.tsb_PatientStatement.Click += new System.EventHandler(this.tsb_PatientStatement_Click);
            // 
            // tsb_New
            // 
            this.tsb_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_New.Image = ((System.Drawing.Image)(resources.GetObject("tsb_New.Image")));
            this.tsb_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_New.Name = "tsb_New";
            this.tsb_New.Size = new System.Drawing.Size(37, 50);
            this.tsb_New.Tag = "New";
            this.tsb_New.Text = "&New";
            this.tsb_New.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_New.Click += new System.EventHandler(this.tsb_New_Click);
            // 
            // tsb_Open
            // 
            this.tsb_Open.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Open.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Open.Image")));
            this.tsb_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Open.Name = "tsb_Open";
            this.tsb_Open.Size = new System.Drawing.Size(43, 50);
            this.tsb_Open.Tag = "Open";
            this.tsb_Open.Text = "&Open";
            this.tsb_Open.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Open.Click += new System.EventHandler(this.tsb_Open_Click);
            // 
            // tsb_Header
            // 
            this.tsb_Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Header.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Header.Image")));
            this.tsb_Header.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Header.Name = "tsb_Header";
            this.tsb_Header.Size = new System.Drawing.Size(54, 50);
            this.tsb_Header.Tag = "Header";
            this.tsb_Header.Text = "&Header";
            this.tsb_Header.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Header.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Header.Click += new System.EventHandler(this.tsb_Header_Click);
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
            this.tsb_InsertFile.Click += new System.EventHandler(this.tsb_InsertFile_Click);
            // 
            // tsb_InsertCheckBox
            // 
            this.tsb_InsertCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_InsertCheckBox.Image = ((System.Drawing.Image)(resources.GetObject("tsb_InsertCheckBox.Image")));
            this.tsb_InsertCheckBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_InsertCheckBox.Name = "tsb_InsertCheckBox";
            this.tsb_InsertCheckBox.Size = new System.Drawing.Size(46, 50);
            this.tsb_InsertCheckBox.Tag = "InsertCheckBox";
            this.tsb_InsertCheckBox.Text = "InsCB";
            this.tsb_InsertCheckBox.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_InsertCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_InsertCheckBox.ToolTipText = "Insert CheckBox";
            this.tsb_InsertCheckBox.Click += new System.EventHandler(this.tsb_InsertCheckBox_Click);
            // 
            // tsb_InsertDropDown
            // 
            this.tsb_InsertDropDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_InsertDropDown.Image = ((System.Drawing.Image)(resources.GetObject("tsb_InsertDropDown.Image")));
            this.tsb_InsertDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_InsertDropDown.Name = "tsb_InsertDropDown";
            this.tsb_InsertDropDown.Size = new System.Drawing.Size(55, 50);
            this.tsb_InsertDropDown.Tag = "InsertDropDown";
            this.tsb_InsertDropDown.Text = "InsDDL";
            this.tsb_InsertDropDown.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_InsertDropDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_InsertDropDown.ToolTipText = "Insert DropDownList";
            this.tsb_InsertDropDown.Click += new System.EventHandler(this.tsb_InsertDropDown_Click);
            // 
            // tsb_Undo
            // 
            this.tsb_Undo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Undo.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Undo.Image")));
            this.tsb_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Undo.Name = "tsb_Undo";
            this.tsb_Undo.Size = new System.Drawing.Size(43, 50);
            this.tsb_Undo.Tag = "Undo";
            this.tsb_Undo.Text = "&Undo";
            this.tsb_Undo.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Undo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Undo.Click += new System.EventHandler(this.tsb_Undo_Click);
            // 
            // tsb_Redo
            // 
            this.tsb_Redo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Redo.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Redo.Image")));
            this.tsb_Redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Redo.Name = "tsb_Redo";
            this.tsb_Redo.Size = new System.Drawing.Size(43, 50);
            this.tsb_Redo.Tag = "Redo";
            this.tsb_Redo.Text = "&Redo";
            this.tsb_Redo.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Redo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Redo.Click += new System.EventHandler(this.tsb_Redo_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(40, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save";
            this.tsb_Save.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_SaveAs
            // 
            this.tsb_SaveAs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_SaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveAs.Image")));
            this.tsb_SaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveAs.Name = "tsb_SaveAs";
            this.tsb_SaveAs.Size = new System.Drawing.Size(55, 50);
            this.tsb_SaveAs.Tag = "SaveAS";
            this.tsb_SaveAs.Text = "Save&As";
            this.tsb_SaveAs.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_SaveAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveAs.ToolTipText = "Save As";
            this.tsb_SaveAs.Click += new System.EventHandler(this.tsb_SaveAs_Click);
            // 
            // tsb_SaveAndClose
            // 
            this.tsb_SaveAndClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_SaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveAndClose.Image")));
            this.tsb_SaveAndClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveAndClose.Name = "tsb_SaveAndClose";
            this.tsb_SaveAndClose.Size = new System.Drawing.Size(66, 50);
            this.tsb_SaveAndClose.Tag = "Save";
            this.tsb_SaveAndClose.Text = "Sa&ve&&Cls";
            this.tsb_SaveAndClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_SaveAndClose.ToolTipText = "Save and Close";
            this.tsb_SaveAndClose.Click += new System.EventHandler(this.tsb_SaveAndClose_Click);
            // 
            // tsb_Close
            // 
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Close.Click += new System.EventHandler(this.tsb_Close_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter1.Location = new System.Drawing.Point(233, 53);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 541);
            this.splitter1.TabIndex = 21;
            this.splitter1.TabStop = false;
            // 
            // pnlTemplate
            // 
            this.pnlTemplate.Controls.Add(this.wdTemplate);
            this.pnlTemplate.Controls.Add(this.label4);
            this.pnlTemplate.Controls.Add(this.label3);
            this.pnlTemplate.Controls.Add(this.label2);
            this.pnlTemplate.Controls.Add(this.label1);
            this.pnlTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplate.Location = new System.Drawing.Point(236, 130);
            this.pnlTemplate.Name = "pnlTemplate";
            this.pnlTemplate.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlTemplate.Size = new System.Drawing.Size(635, 464);
            this.pnlTemplate.TabIndex = 2;
            // 
            // wdTemplate
            // 
            this.wdTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdTemplate.Enabled = true;
            this.wdTemplate.Location = new System.Drawing.Point(1, 1);
            this.wdTemplate.Name = "wdTemplate";
            this.wdTemplate.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdTemplate.OcxState")));
            this.wdTemplate.Size = new System.Drawing.Size(630, 459);
            this.wdTemplate.TabIndex = 0;
            this.wdTemplate.OnDocumentOpened += new AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEventHandler(this.wdTemplate_OnDocumentOpened);
            this.wdTemplate.OnDocumentClosed += new System.EventHandler(this.wdTemplate_OnDocumentClosed);
            this.wdTemplate.BeforeDocumentClosed += new AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEventHandler(this.wdTemplate_BeforeDocumentClosed);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(630, 1);
            this.label4.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(1, 460);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(630, 1);
            this.label3.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 461);
            this.label2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(631, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 461);
            this.label1.TabIndex = 17;
            // 
            // pnlTemplateDetails
            // 
            this.pnlTemplateDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlTemplateDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateDetails.Controls.Add(this.label19);
            this.pnlTemplateDetails.Controls.Add(this.label12);
            this.pnlTemplateDetails.Controls.Add(this.cmbProvider);
            this.pnlTemplateDetails.Controls.Add(this.label10);
            this.pnlTemplateDetails.Controls.Add(this.cmbCategory);
            this.pnlTemplateDetails.Controls.Add(this.label6);
            this.pnlTemplateDetails.Controls.Add(this.txtTemplateName);
            this.pnlTemplateDetails.Controls.Add(this.lblTemplateName);
            this.pnlTemplateDetails.Controls.Add(this.label7);
            this.pnlTemplateDetails.Controls.Add(this.label14);
            this.pnlTemplateDetails.Controls.Add(this.label15);
            this.pnlTemplateDetails.Controls.Add(this.label16);
            this.pnlTemplateDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateDetails.Location = new System.Drawing.Point(236, 53);
            this.pnlTemplateDetails.Name = "pnlTemplateDetails";
            this.pnlTemplateDetails.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlTemplateDetails.Size = new System.Drawing.Size(635, 77);
            this.pnlTemplateDetails.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(45, 44);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 115;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(631, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 69);
            this.label12.TabIndex = 25;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(405, 14);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(183, 22);
            this.cmbProvider.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(323, 16);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(79, 18);
            this.label10.TabIndex = 23;
            this.label10.Text = "Provider :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(125, 42);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(182, 22);
            this.cmbCategory.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 44);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(83, 18);
            this.label6.TabIndex = 21;
            this.label6.Text = "Category :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplateName.ForeColor = System.Drawing.Color.Black;
            this.txtTemplateName.Location = new System.Drawing.Point(125, 12);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(182, 22);
            this.txtTemplateName.TabIndex = 0;
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.AutoEllipsis = true;
            this.lblTemplateName.AutoSize = true;
            this.lblTemplateName.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplateName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateName.Location = new System.Drawing.Point(21, 15);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTemplateName.Size = new System.Drawing.Size(102, 14);
            this.lblTemplateName.TabIndex = 6;
            this.lblTemplateName.Text = "Template Name :";
            this.lblTemplateName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 69);
            this.label7.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(0, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(632, 1);
            this.label14.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(0, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(632, 1);
            this.label15.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.AutoEllipsis = true;
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(8, 12);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label16.Size = new System.Drawing.Size(14, 14);
            this.label16.TabIndex = 114;
            this.label16.Text = "*";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmSetupTemplateGallery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(871, 594);
            this.Controls.Add(this.pnlTemplate);
            this.Controls.Add(this.pnlTemplateDetails);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetupTemplateGallery";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Template Gallery";
            this.Load += new System.EventHandler(this.frmSetupTemplateGallery_Load);
            this.Shown += new System.EventHandler(this.frmSetupTemplateGallery_Shown);
            this.pnlLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlAddLable.ResumeLayout(false);
            this.pnlAddLable.PerformLayout();
            this.pnltrvSerchAppBook.ResumeLayout(false);
            this.pnltrvSerchAppBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlTemplate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).EndInit();
            this.pnlTemplateDetails.ResumeLayout(false);
            this.pnlTemplateDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView trvDataDictionary;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_SaveAndClose;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlTemplate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTemplateDetails;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private AxDSOFramer.AxFramerControl wdTemplate;
        internal System.Windows.Forms.Panel pnltrvSerchAppBook;
        private System.Windows.Forms.TextBox txtSearchDataDictionary;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookBottomBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookTopBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookLeftBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookRightBrd;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Panel pnlAddLable;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox chk_IncludeLable;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ToolStripButton tsb_PatientStatement;
        private System.Windows.Forms.ImageList imgCommon;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.ToolStripButton tsb_New;
        internal System.Windows.Forms.ToolStripButton tsb_Open;
        internal System.Windows.Forms.ToolStripButton tsb_Header;
        internal System.Windows.Forms.ToolStripButton tsb_InsertFile;
        internal System.Windows.Forms.ToolStripButton tsb_Undo;
        internal System.Windows.Forms.ToolStripButton tsb_Redo;
        internal System.Windows.Forms.ToolStripButton tsb_SaveAs;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_InsertCheckBox;
        internal System.Windows.Forms.ToolStripButton tsb_InsertDropDown;
    }
}