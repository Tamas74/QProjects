namespace gloEmdeonInterface.Forms
{
    partial class frmSpirometryRaceMappingsConfiguration
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
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    System.Windows.Forms.ContextMenuStrip[] cntmnuControls = { Cntxt_gloEMR, Cntxt_Spiro };
                    System.Windows.Forms.Control[] cntControls = { Cntxt_gloEMR, Cntxt_Spiro };
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
                    
                    //if (Cntxt_gloEMR != null)
                    //{
                    //    gloGlobal.cEventHelper.RemoveAllEventHandlers(Cntxt_gloEMR);
                    //    if (Cntxt_gloEMR.Items != null)
                    //    {
                    //        Cntxt_gloEMR.Items.Clear();

                    //    }
                    //    Cntxt_gloEMR.Dispose();
                    //    Cntxt_gloEMR = null;
                    //}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSpirometryRaceMappingsConfiguration));
            this.tblStripMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Refresh_32 = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Save_32 = new System.Windows.Forms.ToolStripButton();
            this.tblbtn_Close_32 = new System.Windows.Forms.ToolStripButton();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlgloEMRFields = new System.Windows.Forms.Panel();
            this.TrvgloEMRRace = new System.Windows.Forms.TreeView();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.tbxSearchgloEMRRace = new System.Windows.Forms.TextBox();
            this.btnCleargloEMRSerch = new System.Windows.Forms.Button();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.btnEMRSearch = new System.Windows.Forms.PictureBox();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.pnlSpiroFields = new System.Windows.Forms.Panel();
            this.TrvSpiroRace = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.tbxSearchSpiroRace = new System.Windows.Forms.TextBox();
            this.btnClearSpiroSearch = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.btnSpiroSearch = new System.Windows.Forms.PictureBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Panel9 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlMapped = new System.Windows.Forms.Panel();
            this.TrvMappedRace = new System.Windows.Forms.TreeView();
            this.label7 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.Cntxt_gloEMR = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuDeleteGloEMRFiled = new System.Windows.Forms.ToolStripMenuItem();
            this.Cntxt_Spiro = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuDeleteSpiroField = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.tblStripMain.SuspendLayout();
            this.pnlgloEMRFields.SuspendLayout();
            this.Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEMRSearch)).BeginInit();
            this.Panel3.SuspendLayout();
            this.pnlSpiroFields.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSpiroSearch)).BeginInit();
            this.Panel9.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMapped.SuspendLayout();
            this.panel6.SuspendLayout();
            this.Cntxt_gloEMR.SuspendLayout();
            this.Cntxt_Spiro.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblStripMain
            // 
            this.tblStripMain.BackColor = System.Drawing.Color.Transparent;
            this.tblStripMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tblStripMain.BackgroundImage")));
            this.tblStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tblStripMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tblStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtnNew,
            this.tblbtn_Refresh_32,
            this.tblbtn_Save_32,
            this.tblbtn_Close_32});
            this.tblStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tblStripMain.Location = new System.Drawing.Point(0, 0);
            this.tblStripMain.Name = "tblStripMain";
            this.tblStripMain.Size = new System.Drawing.Size(814, 53);
            this.tblStripMain.TabIndex = 0;
            this.tblStripMain.Text = "ToolStrip1";
            this.tblStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tblStripMain_ItemClicked);
            // 
            // tlbbtnNew
            // 
            this.tlbbtnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnNew.Image")));
            this.tlbbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnNew.Name = "tlbbtnNew";
            this.tlbbtnNew.Size = new System.Drawing.Size(142, 50);
            this.tlbbtnNew.Tag = "AddNew";
            this.tlbbtnNew.Text = "&New Spirometry Race";
            this.tlbbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnNew.ToolTipText = "Add New Spirometry Race";
            // 
            // tblbtn_Refresh_32
            // 
            this.tblbtn_Refresh_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtn_Refresh_32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tblbtn_Refresh_32.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Refresh_32.Image")));
            this.tblbtn_Refresh_32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Refresh_32.Name = "tblbtn_Refresh_32";
            this.tblbtn_Refresh_32.Size = new System.Drawing.Size(58, 50);
            this.tblbtn_Refresh_32.Tag = "Refresh";
            this.tblbtn_Refresh_32.Text = "&Refresh";
            this.tblbtn_Refresh_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tblbtn_Save_32
            // 
            this.tblbtn_Save_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtn_Save_32.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Save_32.Image")));
            this.tblbtn_Save_32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Save_32.Name = "tblbtn_Save_32";
            this.tblbtn_Save_32.Size = new System.Drawing.Size(66, 50);
            this.tblbtn_Save_32.Tag = "Save&Close";
            this.tblbtn_Save_32.Text = "&Save&&Cls";
            this.tblbtn_Save_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tblbtn_Save_32.ToolTipText = "Save and Close";
            // 
            // tblbtn_Close_32
            // 
            this.tblbtn_Close_32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblbtn_Close_32.Image = ((System.Drawing.Image)(resources.GetObject("tblbtn_Close_32.Image")));
            this.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tblbtn_Close_32.Name = "tblbtn_Close_32";
            this.tblbtn_Close_32.Size = new System.Drawing.Size(43, 50);
            this.tblbtn_Close_32.Tag = "Close";
            this.tblbtn_Close_32.Text = "&Close";
            this.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlgloEMRFields
            // 
            this.pnlgloEMRFields.Controls.Add(this.TrvgloEMRRace);
            this.pnlgloEMRFields.Controls.Add(this.label4);
            this.pnlgloEMRFields.Controls.Add(this.Panel4);
            this.pnlgloEMRFields.Controls.Add(this.Label25);
            this.pnlgloEMRFields.Controls.Add(this.Panel3);
            this.pnlgloEMRFields.Controls.Add(this.Label20);
            this.pnlgloEMRFields.Controls.Add(this.Label23);
            this.pnlgloEMRFields.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlgloEMRFields.Location = new System.Drawing.Point(0, 54);
            this.pnlgloEMRFields.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnlgloEMRFields.Name = "pnlgloEMRFields";
            this.pnlgloEMRFields.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlgloEMRFields.Size = new System.Drawing.Size(250, 634);
            this.pnlgloEMRFields.TabIndex = 2;
            // 
            // TrvgloEMRRace
            // 
            this.TrvgloEMRRace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrvgloEMRRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvgloEMRRace.Font = new System.Drawing.Font("Tahoma", 9F);
            this.TrvgloEMRRace.ForeColor = System.Drawing.Color.Black;
            this.TrvgloEMRRace.HideSelection = false;
            this.TrvgloEMRRace.ImageIndex = 0;
            this.TrvgloEMRRace.ImageList = this.ImageList1;
            this.TrvgloEMRRace.Indent = 20;
            this.TrvgloEMRRace.ItemHeight = 20;
            this.TrvgloEMRRace.Location = new System.Drawing.Point(4, 60);
            this.TrvgloEMRRace.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.TrvgloEMRRace.Name = "TrvgloEMRRace";
            this.TrvgloEMRRace.SelectedImageIndex = 5;
            this.TrvgloEMRRace.Size = new System.Drawing.Size(245, 570);
            this.TrvgloEMRRace.TabIndex = 0;
            this.TrvgloEMRRace.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvgloEMRRace_NodeMouseDoubleClick);
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "gloEMR01.ico");
            this.ImageList1.Images.SetKeyName(1, "gloEMR Felid.ico");
            this.ImageList1.Images.SetKeyName(2, "HL7.ico");
            this.ImageList1.Images.SetKeyName(3, "HL7Segment.ico");
            this.ImageList1.Images.SetKeyName(4, "Map Field.ico");
            this.ImageList1.Images.SetKeyName(5, "arrow_01.ico");
            this.ImageList1.Images.SetKeyName(6, "Spiro Test.ico");
            this.ImageList1.Images.SetKeyName(7, "Bullet06.ico");
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(245, 1);
            this.label4.TabIndex = 32;
            this.label4.Text = "label1";
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Transparent;
            this.Panel4.Controls.Add(this.tbxSearchgloEMRRace);
            this.Panel4.Controls.Add(this.btnCleargloEMRSerch);
            this.Panel4.Controls.Add(this.Label26);
            this.Panel4.Controls.Add(this.Label27);
            this.Panel4.Controls.Add(this.btnEMRSearch);
            this.Panel4.Controls.Add(this.Label28);
            this.Panel4.Controls.Add(this.Label29);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel4.ForeColor = System.Drawing.Color.Black;
            this.Panel4.Location = new System.Drawing.Point(4, 30);
            this.Panel4.Name = "Panel4";
            this.Panel4.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Panel4.Size = new System.Drawing.Size(245, 29);
            this.Panel4.TabIndex = 31;
            // 
            // tbxSearchgloEMRRace
            // 
            this.tbxSearchgloEMRRace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxSearchgloEMRRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxSearchgloEMRRace.Location = new System.Drawing.Point(31, 8);
            this.tbxSearchgloEMRRace.Name = "tbxSearchgloEMRRace";
            this.tbxSearchgloEMRRace.Size = new System.Drawing.Size(185, 15);
            this.tbxSearchgloEMRRace.TabIndex = 0;
            this.tbxSearchgloEMRRace.TextChanged += new System.EventHandler(this.tbxSearchgloEMRRace_TextChanged);
            // 
            // btnCleargloEMRSerch
            // 
            this.btnCleargloEMRSerch.AutoSize = true;
            this.btnCleargloEMRSerch.BackColor = System.Drawing.Color.White;
            this.btnCleargloEMRSerch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCleargloEMRSerch.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCleargloEMRSerch.FlatAppearance.BorderSize = 0;
            this.btnCleargloEMRSerch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCleargloEMRSerch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCleargloEMRSerch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCleargloEMRSerch.Image = ((System.Drawing.Image)(resources.GetObject("btnCleargloEMRSerch.Image")));
            this.btnCleargloEMRSerch.Location = new System.Drawing.Point(216, 8);
            this.btnCleargloEMRSerch.Name = "btnCleargloEMRSerch";
            this.btnCleargloEMRSerch.Size = new System.Drawing.Size(29, 15);
            this.btnCleargloEMRSerch.TabIndex = 39;
            this.btnCleargloEMRSerch.UseVisualStyleBackColor = false;
            this.btnCleargloEMRSerch.Click += new System.EventHandler(this.btnCleargloEMRSerch_Click);
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.White;
            this.Label26.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label26.Location = new System.Drawing.Point(31, 4);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(214, 4);
            this.Label26.TabIndex = 37;
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.White;
            this.Label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label27.Location = new System.Drawing.Point(31, 23);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(214, 2);
            this.Label27.TabIndex = 38;
            // 
            // btnEMRSearch
            // 
            this.btnEMRSearch.BackColor = System.Drawing.Color.White;
            this.btnEMRSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEMRSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnEMRSearch.Image")));
            this.btnEMRSearch.Location = new System.Drawing.Point(0, 4);
            this.btnEMRSearch.Name = "btnEMRSearch";
            this.btnEMRSearch.Size = new System.Drawing.Size(31, 21);
            this.btnEMRSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnEMRSearch.TabIndex = 9;
            this.btnEMRSearch.TabStop = false;
            this.btnEMRSearch.Click += new System.EventHandler(this.btnEMRSearch_Click);
            // 
            // Label28
            // 
            this.Label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label28.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label28.Location = new System.Drawing.Point(0, 25);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(245, 1);
            this.Label28.TabIndex = 35;
            this.Label28.Text = "label1";
            // 
            // Label29
            // 
            this.Label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label29.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label29.Location = new System.Drawing.Point(0, 3);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(245, 1);
            this.Label29.TabIndex = 36;
            this.Label29.Text = "label1";
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label25.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(4, 630);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(245, 1);
            this.Label25.TabIndex = 30;
            this.Label25.Text = "label1";
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Transparent;
            this.Panel3.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel3.Controls.Add(this.Label21);
            this.Panel3.Controls.Add(this.Label3);
            this.Panel3.Controls.Add(this.Label22);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel3.Location = new System.Drawing.Point(4, 3);
            this.Panel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(245, 27);
            this.Panel3.TabIndex = 23;
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label21.Location = new System.Drawing.Point(0, 26);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(245, 1);
            this.Label21.TabIndex = 8;
            this.Label21.Text = "label2";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(0, 1);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(245, 26);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "gloEMR Race";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label22
            // 
            this.Label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label22.Location = new System.Drawing.Point(0, 0);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(245, 1);
            this.Label22.TabIndex = 5;
            this.Label22.Text = "label1";
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label20.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label20.Location = new System.Drawing.Point(249, 3);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(1, 628);
            this.Label20.TabIndex = 28;
            this.Label20.Text = "label3";
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label23.Location = new System.Drawing.Point(3, 3);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(1, 628);
            this.Label23.TabIndex = 29;
            this.Label23.Text = "label3";
            // 
            // pnlSpiroFields
            // 
            this.pnlSpiroFields.Controls.Add(this.TrvSpiroRace);
            this.pnlSpiroFields.Controls.Add(this.label2);
            this.pnlSpiroFields.Controls.Add(this.Panel2);
            this.pnlSpiroFields.Controls.Add(this.Label12);
            this.pnlSpiroFields.Controls.Add(this.Panel9);
            this.pnlSpiroFields.Controls.Add(this.Label10);
            this.pnlSpiroFields.Controls.Add(this.Label9);
            this.pnlSpiroFields.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSpiroFields.Location = new System.Drawing.Point(564, 54);
            this.pnlSpiroFields.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnlSpiroFields.Name = "pnlSpiroFields";
            this.pnlSpiroFields.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlSpiroFields.Size = new System.Drawing.Size(250, 634);
            this.pnlSpiroFields.TabIndex = 3;
            // 
            // TrvSpiroRace
            // 
            this.TrvSpiroRace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrvSpiroRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvSpiroRace.Font = new System.Drawing.Font("Tahoma", 9F);
            this.TrvSpiroRace.ForeColor = System.Drawing.Color.Black;
            this.TrvSpiroRace.HideSelection = false;
            this.TrvSpiroRace.ImageIndex = 2;
            this.TrvSpiroRace.ImageList = this.ImageList1;
            this.TrvSpiroRace.Indent = 20;
            this.TrvSpiroRace.ItemHeight = 20;
            this.TrvSpiroRace.Location = new System.Drawing.Point(1, 60);
            this.TrvSpiroRace.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.TrvSpiroRace.Name = "TrvSpiroRace";
            this.TrvSpiroRace.SelectedImageKey = "HL7 Segment.ico";
            this.TrvSpiroRace.Size = new System.Drawing.Size(245, 570);
            this.TrvSpiroRace.TabIndex = 0;
            this.TrvSpiroRace.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvSpiroRace_NodeMouseDoubleClick);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 1);
            this.label2.TabIndex = 26;
            this.label2.Text = "label1";
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.Controls.Add(this.tbxSearchSpiroRace);
            this.Panel2.Controls.Add(this.btnClearSpiroSearch);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Controls.Add(this.Label14);
            this.Panel2.Controls.Add(this.btnSpiroSearch);
            this.Panel2.Controls.Add(this.Label15);
            this.Panel2.Controls.Add(this.Label16);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.ForeColor = System.Drawing.Color.Black;
            this.Panel2.Location = new System.Drawing.Point(1, 30);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.Panel2.Size = new System.Drawing.Size(245, 29);
            this.Panel2.TabIndex = 25;
            // 
            // tbxSearchSpiroRace
            // 
            this.tbxSearchSpiroRace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxSearchSpiroRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxSearchSpiroRace.Location = new System.Drawing.Point(31, 8);
            this.tbxSearchSpiroRace.Name = "tbxSearchSpiroRace";
            this.tbxSearchSpiroRace.Size = new System.Drawing.Size(185, 15);
            this.tbxSearchSpiroRace.TabIndex = 0;
            this.tbxSearchSpiroRace.TextChanged += new System.EventHandler(this.tbxSearchSpiroRace_TextChanged);
            // 
            // btnClearSpiroSearch
            // 
            this.btnClearSpiroSearch.AutoSize = true;
            this.btnClearSpiroSearch.BackColor = System.Drawing.Color.White;
            this.btnClearSpiroSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearSpiroSearch.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClearSpiroSearch.FlatAppearance.BorderSize = 0;
            this.btnClearSpiroSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearSpiroSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearSpiroSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSpiroSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnClearSpiroSearch.Image")));
            this.btnClearSpiroSearch.Location = new System.Drawing.Point(216, 8);
            this.btnClearSpiroSearch.Name = "btnClearSpiroSearch";
            this.btnClearSpiroSearch.Size = new System.Drawing.Size(29, 15);
            this.btnClearSpiroSearch.TabIndex = 40;
            this.btnClearSpiroSearch.UseVisualStyleBackColor = false;
            this.btnClearSpiroSearch.Click += new System.EventHandler(this.btnClearSpiroSearch_Click);
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.White;
            this.Label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label13.Location = new System.Drawing.Point(31, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(214, 4);
            this.Label13.TabIndex = 37;
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.White;
            this.Label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label14.Location = new System.Drawing.Point(31, 23);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(214, 2);
            this.Label14.TabIndex = 38;
            // 
            // btnSpiroSearch
            // 
            this.btnSpiroSearch.BackColor = System.Drawing.Color.White;
            this.btnSpiroSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSpiroSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSpiroSearch.Image")));
            this.btnSpiroSearch.Location = new System.Drawing.Point(0, 4);
            this.btnSpiroSearch.Name = "btnSpiroSearch";
            this.btnSpiroSearch.Size = new System.Drawing.Size(31, 21);
            this.btnSpiroSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnSpiroSearch.TabIndex = 9;
            this.btnSpiroSearch.TabStop = false;
            this.btnSpiroSearch.Click += new System.EventHandler(this.btnSpiroSearch_Click);
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label15.Location = new System.Drawing.Point(0, 25);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(245, 1);
            this.Label15.TabIndex = 35;
            this.Label15.Text = "label1";
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label16.Location = new System.Drawing.Point(0, 3);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(245, 1);
            this.Label16.TabIndex = 36;
            this.Label16.Text = "label1";
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label12.Location = new System.Drawing.Point(1, 630);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(245, 1);
            this.Label12.TabIndex = 24;
            this.Label12.Text = "label2";
            // 
            // Panel9
            // 
            this.Panel9.BackColor = System.Drawing.Color.Transparent;
            this.Panel9.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel9.Controls.Add(this.Label1);
            this.Panel9.Controls.Add(this.Label8);
            this.Panel9.Controls.Add(this.Label11);
            this.Panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel9.Location = new System.Drawing.Point(1, 3);
            this.Panel9.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel9.Name = "Panel9";
            this.Panel9.Size = new System.Drawing.Size(245, 27);
            this.Panel9.TabIndex = 21;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(0, 1);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(245, 25);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Spirometry Race";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label8.Location = new System.Drawing.Point(0, 26);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(245, 1);
            this.Label8.TabIndex = 8;
            this.Label8.Text = "label2";
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(0, 0);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(245, 1);
            this.Label11.TabIndex = 5;
            this.Label11.Text = "label1";
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label10.Location = new System.Drawing.Point(246, 3);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(1, 628);
            this.Label10.TabIndex = 22;
            this.Label10.Text = "label3";
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label9.Location = new System.Drawing.Point(0, 3);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(1, 628);
            this.Label9.TabIndex = 23;
            this.Label9.Text = "label3";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.tblStripMain);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(814, 54);
            this.pnlTop.TabIndex = 1;
            // 
            // pnlMapped
            // 
            this.pnlMapped.Controls.Add(this.TrvMappedRace);
            this.pnlMapped.Controls.Add(this.label7);
            this.pnlMapped.Controls.Add(this.panel6);
            this.pnlMapped.Controls.Add(this.label24);
            this.pnlMapped.Controls.Add(this.label30);
            this.pnlMapped.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMapped.Location = new System.Drawing.Point(253, 54);
            this.pnlMapped.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pnlMapped.Name = "pnlMapped";
            this.pnlMapped.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnlMapped.Size = new System.Drawing.Size(308, 634);
            this.pnlMapped.TabIndex = 4;
            // 
            // TrvMappedRace
            // 
            this.TrvMappedRace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrvMappedRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvMappedRace.Font = new System.Drawing.Font("Tahoma", 9F);
            this.TrvMappedRace.ForeColor = System.Drawing.Color.Black;
            this.TrvMappedRace.HideSelection = false;
            this.TrvMappedRace.ImageIndex = 0;
            this.TrvMappedRace.ImageList = this.ImageList1;
            this.TrvMappedRace.Indent = 20;
            this.TrvMappedRace.ItemHeight = 20;
            this.TrvMappedRace.Location = new System.Drawing.Point(1, 30);
            this.TrvMappedRace.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.TrvMappedRace.Name = "TrvMappedRace";
            this.TrvMappedRace.SelectedImageIndex = 0;
            this.TrvMappedRace.Size = new System.Drawing.Size(306, 600);
            this.TrvMappedRace.TabIndex = 0;
            this.TrvMappedRace.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvMappedRace_AfterSelect);
            this.TrvMappedRace.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvMappedRace_NodeMouseClick);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1, 630);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(306, 1);
            this.label7.TabIndex = 30;
            this.label7.Text = "label1";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::gloEmdeonInterface.Properties.Resources.Img_LongButton;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label17);
            this.panel6.Controls.Add(this.label18);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(1, 3);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(306, 27);
            this.panel6.TabIndex = 23;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(0, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(306, 1);
            this.label17.TabIndex = 8;
            this.label17.Text = "label2";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(306, 26);
            this.label18.TabIndex = 2;
            this.label18.Text = "Mapped Race";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(306, 1);
            this.label19.TabIndex = 5;
            this.label19.Text = "label1";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.Location = new System.Drawing.Point(307, 3);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 628);
            this.label24.TabIndex = 28;
            this.label24.Text = "label3";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label30.Location = new System.Drawing.Point(0, 3);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 628);
            this.label30.TabIndex = 29;
            this.label30.Text = "label3";
            // 
            // Cntxt_gloEMR
            // 
            this.Cntxt_gloEMR.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuDeleteGloEMRFiled});
            this.Cntxt_gloEMR.Name = "Cntxt_gloEMR";
            this.Cntxt_gloEMR.Size = new System.Drawing.Size(180, 26);
            // 
            // MenuDeleteGloEMRFiled
            // 
            this.MenuDeleteGloEMRFiled.Name = "MenuDeleteGloEMRFiled";
            this.MenuDeleteGloEMRFiled.Size = new System.Drawing.Size(179, 22);
            this.MenuDeleteGloEMRFiled.Tag = "1";
            this.MenuDeleteGloEMRFiled.Text = "Delete gloEMR Field";
            this.MenuDeleteGloEMRFiled.Click += new System.EventHandler(this.MenuDeleteGloEMRFiled_Click);
            // 
            // Cntxt_Spiro
            // 
            this.Cntxt_Spiro.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuDeleteSpiroField});
            this.Cntxt_Spiro.Name = "Cntxt_gloEMR";
            this.Cntxt_Spiro.Size = new System.Drawing.Size(197, 26);
            // 
            // MenuDeleteSpiroField
            // 
            this.MenuDeleteSpiroField.Name = "MenuDeleteSpiroField";
            this.MenuDeleteSpiroField.Size = new System.Drawing.Size(196, 22);
            this.MenuDeleteSpiroField.Tag = "1";
            this.MenuDeleteSpiroField.Text = "Delete Spirometry Field";
            this.MenuDeleteSpiroField.Click += new System.EventHandler(this.MenuDeleteSpiroField_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(250, 54);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 634);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(561, 54);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 634);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // frmSpirometryRaceMappingsConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(814, 688);
            this.Controls.Add(this.pnlMapped);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlSpiroFields);
            this.Controls.Add(this.pnlgloEMRFields);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpirometryRaceMappingsConfiguration";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spirometry Race Configuration";
            this.Load += new System.EventHandler(this.frmSpiroTest_RaceConfiguration1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSpirometryRaceMappingsConfiguration_FormClosing);
            this.tblStripMain.ResumeLayout(false);
            this.tblStripMain.PerformLayout();
            this.pnlgloEMRFields.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEMRSearch)).EndInit();
            this.Panel3.ResumeLayout(false);
            this.pnlSpiroFields.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSpiroSearch)).EndInit();
            this.Panel9.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMapped.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.Cntxt_gloEMR.ResumeLayout(false);
            this.Cntxt_Spiro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus tblStripMain;
        internal System.Windows.Forms.ToolStripButton tblbtn_Save_32;
        internal System.Windows.Forms.ToolStripButton tblbtn_Close_32;
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.Panel pnlgloEMRFields;
        internal System.Windows.Forms.TreeView TrvgloEMRRace;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.TextBox tbxSearchgloEMRRace;
        internal System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.Label Label27;
        internal System.Windows.Forms.PictureBox btnEMRSearch;
        private System.Windows.Forms.Label Label28;
        private System.Windows.Forms.Label Label29;
        private System.Windows.Forms.Label Label25;
        internal System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label22;
        private System.Windows.Forms.Label Label20;
        private System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.Panel pnlSpiroFields;
        internal System.Windows.Forms.TreeView TrvSpiroRace;
        internal System.Windows.Forms.TextBox tbxSearchSpiroRace;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.PictureBox btnSpiroSearch;
        private System.Windows.Forms.Label Label15;
        private System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Panel Panel9;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.ImageList ImageList1;
        internal System.Windows.Forms.Panel pnlTop;
        internal System.Windows.Forms.Panel pnlMapped;
        internal System.Windows.Forms.TreeView TrvMappedRace;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ContextMenuStrip Cntxt_gloEMR;
        private System.Windows.Forms.ToolStripMenuItem MenuDeleteGloEMRFiled;
        private System.Windows.Forms.ContextMenuStrip Cntxt_Spiro;
        private System.Windows.Forms.ToolStripMenuItem MenuDeleteSpiroField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton tlbbtnNew;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.ToolStripButton tblbtn_Refresh_32;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button btnCleargloEMRSerch;
        private System.Windows.Forms.Button btnClearSpiroSearch;

    }
}