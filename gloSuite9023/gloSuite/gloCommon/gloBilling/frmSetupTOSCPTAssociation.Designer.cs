namespace gloBilling
{
    partial class frmSetupTOSCPTAssociation
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
                    if (cxtMS != null)
                    {

                        System.Windows.Forms.ContextMenuStrip[] cntmenuControls = { cxtMS };
                        System.Windows.Forms.Control[] cntControls = { cxtMS };
                        if (cntmenuControls != null)
                        {
                            if (cntmenuControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmenuControls);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupTOSCPTAssociation));
            this.pnlTlstrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.C1TOSCPT = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.PnlCPT = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.trvCPT = new System.Windows.Forms.TreeView();
            this.imgLstTOS = new System.Windows.Forms.ImageList(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pnl_btnICD9 = new System.Windows.Forms.Panel();
            this.btnICD9 = new System.Windows.Forms.Button();
            this.pnl_btnCPT = new System.Windows.Forms.Panel();
            this.btnCPT = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlSearchCriteria = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.chkBoxSelect = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtCPTSearch = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSelect = new System.Windows.Forms.Panel();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.rbDescription = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbCode = new System.Windows.Forms.RadioButton();
            this.pnlTOS = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.trvTOS = new System.Windows.Forms.TreeView();
            this.label9 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.txtTOSSearch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rbTOSDesc = new System.Windows.Forms.RadioButton();
            this.rbTOSCode = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cxtMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTlstrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1TOSCPT)).BeginInit();
            this.PnlCPT.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnl_btnICD9.SuspendLayout();
            this.pnl_btnCPT.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlSearchCriteria.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlSelect.SuspendLayout();
            this.pnlTOS.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.cxtMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTlstrip
            // 
            this.pnlTlstrip.Controls.Add(this.ts_Commands);
            this.pnlTlstrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTlstrip.Location = new System.Drawing.Point(0, 0);
            this.pnlTlstrip.Name = "pnlTlstrip";
            this.pnlTlstrip.Size = new System.Drawing.Size(979, 55);
            this.pnlTlstrip.TabIndex = 1;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAdd,
            this.ToolStripButton1,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(979, 53);
            this.ts_Commands.TabIndex = 15;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsbtnAdd
            // 
            this.tsbtnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAdd.Image")));
            this.tsbtnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAdd.Name = "tsbtnAdd";
            this.tsbtnAdd.Size = new System.Drawing.Size(37, 50);
            this.tsbtnAdd.Tag = "ADD";
            this.tsbtnAdd.Text = "&New";
            this.tsbtnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(40, 50);
            this.ToolStripButton1.Text = "Sa&ve";
            this.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolStripButton1.ToolTipText = "Save";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlMiddle);
            this.pnlMain.Controls.Add(this.splitter2);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.PnlCPT);
            this.pnlMain.Controls.Add(this.pnlTOS);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 55);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(979, 630);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.label19);
            this.pnlMiddle.Controls.Add(this.label18);
            this.pnlMiddle.Controls.Add(this.label17);
            this.pnlMiddle.Controls.Add(this.label16);
            this.pnlMiddle.Controls.Add(this.C1TOSCPT);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(276, 0);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.pnlMiddle.Size = new System.Drawing.Size(460, 630);
            this.pnlMiddle.TabIndex = 6;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(2, 626);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(456, 1);
            this.label19.TabIndex = 56;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(2, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(456, 1);
            this.label18.TabIndex = 55;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Location = new System.Drawing.Point(458, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 624);
            this.label17.TabIndex = 54;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(1, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 624);
            this.label16.TabIndex = 53;
            // 
            // C1TOSCPT
            // 
            this.C1TOSCPT.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.C1TOSCPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1TOSCPT.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1TOSCPT.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.C1TOSCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1TOSCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1TOSCPT.Location = new System.Drawing.Point(1, 3);
            this.C1TOSCPT.Name = "C1TOSCPT";
            this.C1TOSCPT.Rows.DefaultSize = 19;
            this.C1TOSCPT.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1TOSCPT.Size = new System.Drawing.Size(458, 624);
            this.C1TOSCPT.StyleInfo = resources.GetString("C1TOSCPT.StyleInfo");
            this.C1TOSCPT.TabIndex = 0;
            this.C1TOSCPT.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.C1TOSCPT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C1TOSCPT_MouseDown);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(736, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 630);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(273, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 630);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // PnlCPT
            // 
            this.PnlCPT.Controls.Add(this.panel6);
            this.PnlCPT.Controls.Add(this.pnl_btnICD9);
            this.PnlCPT.Controls.Add(this.pnl_btnCPT);
            this.PnlCPT.Controls.Add(this.panel4);
            this.PnlCPT.Controls.Add(this.pnlSearch);
            this.PnlCPT.Controls.Add(this.panel1);
            this.PnlCPT.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlCPT.Location = new System.Drawing.Point(739, 0);
            this.PnlCPT.Name = "PnlCPT";
            this.PnlCPT.Size = new System.Drawing.Size(240, 630);
            this.PnlCPT.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.trvCPT);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.lbl_BottomBrd);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label23);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 113);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(1, 1, 3, 3);
            this.panel6.Size = new System.Drawing.Size(240, 489);
            this.panel6.TabIndex = 63;
            // 
            // trvCPT
            // 
            this.trvCPT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCPT.CheckBoxes = true;
            this.trvCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCPT.ForeColor = System.Drawing.Color.Black;
            this.trvCPT.FullRowSelect = true;
            this.trvCPT.ImageIndex = 2;
            this.trvCPT.ImageList = this.imgLstTOS;
            this.trvCPT.LineColor = System.Drawing.Color.SteelBlue;
            this.trvCPT.Location = new System.Drawing.Point(2, 5);
            this.trvCPT.Name = "trvCPT";
            this.trvCPT.SelectedImageIndex = 2;
            this.trvCPT.Size = new System.Drawing.Size(234, 480);
            this.trvCPT.TabIndex = 2;
            this.trvCPT.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCPT_AfterCheck);
            this.trvCPT.DoubleClick += new System.EventHandler(this.trvCPT_DoubleClick);
            // 
            // imgLstTOS
            // 
            this.imgLstTOS.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLstTOS.ImageStream")));
            this.imgLstTOS.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLstTOS.Images.SetKeyName(0, "Diamond.ico");
            this.imgLstTOS.Images.SetKeyName(1, "Type of service.ico");
            this.imgLstTOS.Images.SetKeyName(2, "CPT.ico");
            this.imgLstTOS.Images.SetKeyName(3, "ICD 09.ico");
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(234, 3);
            this.label8.TabIndex = 9;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(2, 485);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(234, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(1, 2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 484);
            this.label21.TabIndex = 7;
            this.label21.Text = "label4";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label22.Location = new System.Drawing.Point(236, 2);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 484);
            this.label22.TabIndex = 6;
            this.label22.Text = "label3";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(1, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(236, 1);
            this.label23.TabIndex = 5;
            this.label23.Text = "label1";
            // 
            // pnl_btnICD9
            // 
            this.pnl_btnICD9.Controls.Add(this.btnICD9);
            this.pnl_btnICD9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_btnICD9.Location = new System.Drawing.Point(0, 602);
            this.pnl_btnICD9.Name = "pnl_btnICD9";
            this.pnl_btnICD9.Padding = new System.Windows.Forms.Padding(1, 1, 3, 3);
            this.pnl_btnICD9.Size = new System.Drawing.Size(240, 28);
            this.pnl_btnICD9.TabIndex = 64;
            // 
            // btnICD9
            // 
            this.btnICD9.AutoEllipsis = true;
            this.btnICD9.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
            this.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnICD9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnICD9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnICD9.Location = new System.Drawing.Point(1, 1);
            this.btnICD9.Name = "btnICD9";
            this.btnICD9.Size = new System.Drawing.Size(236, 24);
            this.btnICD9.TabIndex = 55;
            this.btnICD9.Text = "ICD9";
            this.btnICD9.UseVisualStyleBackColor = true;
            this.btnICD9.MouseLeave += new System.EventHandler(this.btnICD9_MouseLeave);
            this.btnICD9.Click += new System.EventHandler(this.btnICD9_Click);
            this.btnICD9.MouseHover += new System.EventHandler(this.btnICD9_MouseHover);
            // 
            // pnl_btnCPT
            // 
            this.pnl_btnCPT.Controls.Add(this.btnCPT);
            this.pnl_btnCPT.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_btnCPT.Location = new System.Drawing.Point(0, 85);
            this.pnl_btnCPT.Name = "pnl_btnCPT";
            this.pnl_btnCPT.Padding = new System.Windows.Forms.Padding(1, 1, 3, 3);
            this.pnl_btnCPT.Size = new System.Drawing.Size(240, 28);
            this.pnl_btnCPT.TabIndex = 62;
            // 
            // btnCPT
            // 
            this.btnCPT.AutoEllipsis = true;
            this.btnCPT.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
            this.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCPT.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCPT.Location = new System.Drawing.Point(1, 1);
            this.btnCPT.Name = "btnCPT";
            this.btnCPT.Size = new System.Drawing.Size(236, 24);
            this.btnCPT.TabIndex = 56;
            this.btnCPT.Text = "CPT";
            this.btnCPT.UseVisualStyleBackColor = true;
            this.btnCPT.MouseLeave += new System.EventHandler(this.btnCPT_MouseLeave);
            this.btnCPT.Click += new System.EventHandler(this.btnCPT_Click);
            this.btnCPT.MouseHover += new System.EventHandler(this.btnCPT_MouseHover);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pnlSearchCriteria);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 57);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(1, 1, 3, 3);
            this.panel4.Size = new System.Drawing.Size(240, 28);
            this.panel4.TabIndex = 61;
            // 
            // pnlSearchCriteria
            // 
            this.pnlSearchCriteria.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlSearchCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchCriteria.Controls.Add(this.label13);
            this.pnlSearchCriteria.Controls.Add(this.label15);
            this.pnlSearchCriteria.Controls.Add(this.label20);
            this.pnlSearchCriteria.Controls.Add(this.chkBoxSelect);
            this.pnlSearchCriteria.Controls.Add(this.label14);
            this.pnlSearchCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchCriteria.Location = new System.Drawing.Point(1, 1);
            this.pnlSearchCriteria.Name = "pnlSearchCriteria";
            this.pnlSearchCriteria.Size = new System.Drawing.Size(236, 24);
            this.pnlSearchCriteria.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 22);
            this.label13.TabIndex = 60;
            this.label13.Text = "label4";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label15.Location = new System.Drawing.Point(235, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 22);
            this.label15.TabIndex = 59;
            this.label15.Text = "label3";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(236, 1);
            this.label20.TabIndex = 58;
            this.label20.Text = "label1";
            // 
            // chkBoxSelect
            // 
            this.chkBoxSelect.AutoSize = true;
            this.chkBoxSelect.BackColor = System.Drawing.Color.Transparent;
            this.chkBoxSelect.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkBoxSelect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxSelect.Location = new System.Drawing.Point(17, 3);
            this.chkBoxSelect.Name = "chkBoxSelect";
            this.chkBoxSelect.Size = new System.Drawing.Size(82, 18);
            this.chkBoxSelect.TabIndex = 58;
            this.chkBoxSelect.Text = "Select All";
            this.chkBoxSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBoxSelect.UseVisualStyleBackColor = false;
            this.chkBoxSelect.Click += new System.EventHandler(this.chkBoxSelect_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(0, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(236, 1);
            this.label14.TabIndex = 57;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.Controls.Add(this.txtCPTSearch);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceTop);
            this.pnlSearch.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.pnlSearch.Controls.Add(this.PicBx_Search);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchLeftBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSearch.Location = new System.Drawing.Point(0, 30);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(1, 1, 3, 3);
            this.pnlSearch.Size = new System.Drawing.Size(240, 27);
            this.pnlSearch.TabIndex = 59;
            // 
            // txtCPTSearch
            // 
            this.txtCPTSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCPTSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCPTSearch.ForeColor = System.Drawing.Color.Black;
            this.txtCPTSearch.Location = new System.Drawing.Point(30, 6);
            this.txtCPTSearch.Multiline = true;
            this.txtCPTSearch.Name = "txtCPTSearch";
            this.txtCPTSearch.Size = new System.Drawing.Size(206, 15);
            this.txtCPTSearch.TabIndex = 0;
            this.txtCPTSearch.TextChanged += new System.EventHandler(this.txtCPTSearch_TextChanged);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(30, 2);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(206, 4);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(30, 21);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(206, 2);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(2, 2);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(28, 21);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(2, 23);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(234, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 35;
            this.lbl_pnlSearchBottomBrd.Text = "label1";
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(2, 1);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(234, 1);
            this.lbl_pnlSearchTopBrd.TabIndex = 36;
            this.lbl_pnlSearchTopBrd.Text = "label1";
            // 
            // lbl_pnlSearchLeftBrd
            // 
            this.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchLeftBrd.Location = new System.Drawing.Point(1, 1);
            this.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd";
            this.lbl_pnlSearchLeftBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchLeftBrd.TabIndex = 39;
            this.lbl_pnlSearchLeftBrd.Text = "label4";
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(236, 1);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnlSearchRightBrd.TabIndex = 40;
            this.lbl_pnlSearchRightBrd.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.panel1.Size = new System.Drawing.Size(240, 30);
            this.panel1.TabIndex = 60;
            // 
            // pnlSelect
            // 
            this.pnlSelect.BackColor = System.Drawing.Color.Transparent;
            this.pnlSelect.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.pnlSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSelect.Controls.Add(this.lbl_LeftBrd);
            this.pnlSelect.Controls.Add(this.lbl_RightBrd);
            this.pnlSelect.Controls.Add(this.lbl_TopBrd);
            this.pnlSelect.Controls.Add(this.rbDescription);
            this.pnlSelect.Controls.Add(this.label2);
            this.pnlSelect.Controls.Add(this.rbCode);
            this.pnlSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSelect.Location = new System.Drawing.Point(1, 3);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Size = new System.Drawing.Size(236, 24);
            this.pnlSelect.TabIndex = 57;
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_LeftBrd.TabIndex = 60;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(235, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_RightBrd.TabIndex = 59;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(236, 1);
            this.lbl_TopBrd.TabIndex = 58;
            this.lbl_TopBrd.Text = "label1";
            // 
            // rbDescription
            // 
            this.rbDescription.AutoSize = true;
            this.rbDescription.BackColor = System.Drawing.Color.Transparent;
            this.rbDescription.Checked = true;
            this.rbDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDescription.Location = new System.Drawing.Point(133, 3);
            this.rbDescription.Name = "rbDescription";
            this.rbDescription.Size = new System.Drawing.Size(94, 18);
            this.rbDescription.TabIndex = 57;
            this.rbDescription.TabStop = true;
            this.rbDescription.Text = "Description";
            this.rbDescription.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 1);
            this.label2.TabIndex = 57;
            // 
            // rbCode
            // 
            this.rbCode.AutoSize = true;
            this.rbCode.BackColor = System.Drawing.Color.Transparent;
            this.rbCode.Location = new System.Drawing.Point(19, 3);
            this.rbCode.Name = "rbCode";
            this.rbCode.Size = new System.Drawing.Size(53, 18);
            this.rbCode.TabIndex = 0;
            this.rbCode.Text = "Code";
            this.rbCode.UseVisualStyleBackColor = false;
            this.rbCode.CheckedChanged += new System.EventHandler(this.rbCode_CheckedChanged);
            // 
            // pnlTOS
            // 
            this.pnlTOS.Controls.Add(this.panel11);
            this.pnlTOS.Controls.Add(this.panel10);
            this.pnlTOS.Controls.Add(this.panel9);
            this.pnlTOS.Controls.Add(this.panel8);
            this.pnlTOS.Controls.Add(this.label6);
            this.pnlTOS.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTOS.Location = new System.Drawing.Point(0, 0);
            this.pnlTOS.Name = "pnlTOS";
            this.pnlTOS.Size = new System.Drawing.Size(273, 630);
            this.pnlTOS.TabIndex = 5;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.trvTOS);
            this.panel11.Controls.Add(this.label9);
            this.panel11.Controls.Add(this.label33);
            this.panel11.Controls.Add(this.label34);
            this.panel11.Controls.Add(this.label35);
            this.panel11.Controls.Add(this.label36);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 88);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(3, 1, 1, 3);
            this.panel11.Size = new System.Drawing.Size(273, 541);
            this.panel11.TabIndex = 57;
            // 
            // trvTOS
            // 
            this.trvTOS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTOS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTOS.ForeColor = System.Drawing.Color.Black;
            this.trvTOS.ImageIndex = 1;
            this.trvTOS.ImageList = this.imgLstTOS;
            this.trvTOS.Location = new System.Drawing.Point(4, 5);
            this.trvTOS.Name = "trvTOS";
            this.trvTOS.SelectedImageIndex = 0;
            this.trvTOS.Size = new System.Drawing.Size(267, 532);
            this.trvTOS.TabIndex = 2;
            this.trvTOS.DoubleClick += new System.EventHandler(this.trvTOS_DoubleClick);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(267, 3);
            this.label9.TabIndex = 10;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label33.Location = new System.Drawing.Point(4, 537);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(267, 1);
            this.label33.TabIndex = 8;
            this.label33.Text = "label2";
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Left;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(3, 2);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1, 536);
            this.label34.TabIndex = 7;
            this.label34.Text = "label4";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Right;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label35.Location = new System.Drawing.Point(271, 2);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 536);
            this.label35.TabIndex = 6;
            this.label35.Text = "label3";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(3, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(269, 1);
            this.label36.TabIndex = 5;
            this.label36.Text = "label1";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel2);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 58);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(3, 1, 1, 3);
            this.panel10.Size = new System.Drawing.Size(273, 30);
            this.panel10.TabIndex = 56;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
            this.panel2.Controls.Add(this.label29);
            this.panel2.Controls.Add(this.label30);
            this.panel2.Controls.Add(this.label31);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 26);
            this.panel2.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label29.Location = new System.Drawing.Point(1, 25);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(267, 1);
            this.label29.TabIndex = 8;
            this.label29.Text = "label2";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(0, 1);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 25);
            this.label30.TabIndex = 7;
            this.label30.Text = "label4";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Right;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label31.Location = new System.Drawing.Point(268, 1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 25);
            this.label31.TabIndex = 6;
            this.label31.Text = "label3";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(269, 1);
            this.label32.TabIndex = 5;
            this.label32.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(269, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Type Of Service";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel9.Controls.Add(this.txtTOSSearch);
            this.panel9.Controls.Add(this.label12);
            this.panel9.Controls.Add(this.label24);
            this.panel9.Controls.Add(this.pictureBox1);
            this.panel9.Controls.Add(this.label25);
            this.panel9.Controls.Add(this.label26);
            this.panel9.Controls.Add(this.label27);
            this.panel9.Controls.Add(this.label28);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel9.ForeColor = System.Drawing.Color.Black;
            this.panel9.Location = new System.Drawing.Point(0, 31);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(3, 1, 1, 3);
            this.panel9.Size = new System.Drawing.Size(273, 27);
            this.panel9.TabIndex = 55;
            // 
            // txtTOSSearch
            // 
            this.txtTOSSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTOSSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTOSSearch.ForeColor = System.Drawing.Color.Black;
            this.txtTOSSearch.Location = new System.Drawing.Point(32, 6);
            this.txtTOSSearch.Name = "txtTOSSearch";
            this.txtTOSSearch.Size = new System.Drawing.Size(239, 15);
            this.txtTOSSearch.TabIndex = 0;
            this.txtTOSSearch.TextChanged += new System.EventHandler(this.txtTOSSearch_TextChanged);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(32, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(239, 4);
            this.label12.TabIndex = 37;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.White;
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(32, 21);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(239, 2);
            this.label24.TabIndex = 38;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 21);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label25.Location = new System.Drawing.Point(4, 23);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(267, 1);
            this.label25.TabIndex = 35;
            this.label25.Text = "label1";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.label26.Location = new System.Drawing.Point(4, 1);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(267, 1);
            this.label26.TabIndex = 36;
            this.label26.Text = "label1";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Left;
            this.label27.Location = new System.Drawing.Point(3, 1);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 23);
            this.label27.TabIndex = 39;
            this.label27.Text = "label4";
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Right;
            this.label28.Location = new System.Drawing.Point(271, 1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1, 23);
            this.label28.TabIndex = 40;
            this.label28.Text = "label4";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.panel8.Size = new System.Drawing.Size(273, 31);
            this.panel8.TabIndex = 54;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.rbTOSDesc);
            this.panel3.Controls.Add(this.rbTOSCode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(269, 25);
            this.panel3.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(1, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 1);
            this.label1.TabIndex = 8;
            this.label1.Text = "label2";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label5.Location = new System.Drawing.Point(268, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "label3";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(269, 1);
            this.label7.TabIndex = 5;
            this.label7.Text = "label1";
            // 
            // rbTOSDesc
            // 
            this.rbTOSDesc.AutoSize = true;
            this.rbTOSDesc.BackColor = System.Drawing.Color.Transparent;
            this.rbTOSDesc.Checked = true;
            this.rbTOSDesc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTOSDesc.Location = new System.Drawing.Point(142, 3);
            this.rbTOSDesc.Name = "rbTOSDesc";
            this.rbTOSDesc.Size = new System.Drawing.Size(94, 18);
            this.rbTOSDesc.TabIndex = 1;
            this.rbTOSDesc.TabStop = true;
            this.rbTOSDesc.Text = "Description";
            this.rbTOSDesc.UseVisualStyleBackColor = false;
            // 
            // rbTOSCode
            // 
            this.rbTOSCode.AutoSize = true;
            this.rbTOSCode.BackColor = System.Drawing.Color.Transparent;
            this.rbTOSCode.Location = new System.Drawing.Point(42, 3);
            this.rbTOSCode.Name = "rbTOSCode";
            this.rbTOSCode.Size = new System.Drawing.Size(53, 18);
            this.rbTOSCode.TabIndex = 0;
            this.rbTOSCode.Text = "Code";
            this.rbTOSCode.UseVisualStyleBackColor = false;
            this.rbTOSCode.CheckedChanged += new System.EventHandler(this.rbTOSCode_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 629);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(273, 1);
            this.label6.TabIndex = 53;
            // 
            // cxtMS
            // 
            this.cxtMS.BackColor = System.Drawing.SystemColors.Control;
            this.cxtMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.cxtMS.Name = "contextMenuStrip1";
            this.cxtMS.Size = new System.Drawing.Size(117, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // frmSetupTOSCPTAssociation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(979, 685);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTlstrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetupTOSCPTAssociation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TOS - CPT/ICD9  Association";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmSetupTOSCPTAssociation_Shown);
            this.Load += new System.EventHandler(this.frmSetupTOSCPTAssociation_Load);
            this.pnlTlstrip.ResumeLayout(false);
            this.pnlTlstrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1TOSCPT)).EndInit();
            this.PnlCPT.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.pnl_btnICD9.ResumeLayout(false);
            this.pnl_btnCPT.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlSearchCriteria.ResumeLayout(false);
            this.pnlSearchCriteria.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlSelect.ResumeLayout(false);
            this.pnlSelect.PerformLayout();
            this.pnlTOS.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.cxtMS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTlstrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTOS;
        private System.Windows.Forms.TreeView trvTOS;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTOSSearch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbTOSDesc;
        private System.Windows.Forms.RadioButton rbTOSCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel PnlCPT;
        private System.Windows.Forms.TreeView trvCPT;
        private System.Windows.Forms.TextBox txtCPTSearch;
        private System.Windows.Forms.RadioButton rbCode;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private C1.Win.C1FlexGrid.C1FlexGrid C1TOSCPT;
        private System.Windows.Forms.ContextMenuStrip cxtMS;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripButton tsbtnAdd;
        private System.Windows.Forms.Button btnCPT;
        private System.Windows.Forms.Button btnICD9;
        private System.Windows.Forms.Panel pnlSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBoxSelect;
        private System.Windows.Forms.ImageList imgLstTOS;
        private System.Windows.Forms.RadioButton rbDescription;
        internal System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Panel pnlSearchCriteria;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel pnl_btnICD9;
        private System.Windows.Forms.Panel pnl_btnCPT;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        internal System.Windows.Forms.Panel panel9;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.ToolStripButton ToolStripButton1;
    }
}