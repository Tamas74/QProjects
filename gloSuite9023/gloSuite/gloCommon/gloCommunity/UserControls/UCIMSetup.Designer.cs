namespace gloCommunity.UserControls
{
    partial class UCIMSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCIMSetup));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.C1IMView = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlLeftTop = new System.Windows.Forms.Panel();
            this.trvCategory = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.PanelCombo1 = new System.Windows.Forms.Panel();
            this.pnlTopRight = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.C1IMView)).BeginInit();
            this.pnlLeft.SuspendLayout();
            this.pnlLeftTop.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.PanelCombo1.SuspendLayout();
            this.pnlTopRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.C1IMView);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(251, 27);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Panel1.Size = new System.Drawing.Size(644, 707);
            this.Panel1.TabIndex = 17;
            // 
            // C1IMView
            // 
            this.C1IMView.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.C1IMView.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.C1IMView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.C1IMView.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.C1IMView.ColumnInfo = resources.GetString("C1IMView.ColumnInfo");
            this.C1IMView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.C1IMView.ExtendLastCol = true;
            this.C1IMView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C1IMView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.C1IMView.Location = new System.Drawing.Point(1, 4);
            this.C1IMView.Name = "C1IMView";
            this.C1IMView.Rows.DefaultSize = 19;
            this.C1IMView.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.C1IMView.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.C1IMView.Size = new System.Drawing.Size(639, 699);
            this.C1IMView.StyleInfo = resources.GetString("C1IMView.StyleInfo");
            this.C1IMView.TabIndex = 9;
            this.C1IMView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.C1IMView_MouseDown);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label1.Location = new System.Drawing.Point(1, 703);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(639, 1);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "label2";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(0, 4);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 700);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "label4";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label3.Location = new System.Drawing.Point(640, 4);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 700);
            this.Label3.TabIndex = 6;
            this.Label3.Text = "label3";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(0, 3);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(641, 1);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "label1";
            // 
            // Splitter1
            // 
            this.Splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Splitter1.Location = new System.Drawing.Point(248, 3);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 731);
            this.Splitter1.TabIndex = 18;
            this.Splitter1.TabStop = false;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.Controls.Add(this.pnlLeftTop);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(28, 3);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(220, 731);
            this.pnlLeft.TabIndex = 19;
            // 
            // pnlLeftTop
            // 
            this.pnlLeftTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeftTop.Controls.Add(this.trvCategory);
            this.pnlLeftTop.Controls.Add(this.label5);
            this.pnlLeftTop.Controls.Add(this.lbl_BottomBrd);
            this.pnlLeftTop.Controls.Add(this.lbl_LeftBrd);
            this.pnlLeftTop.Controls.Add(this.lbl_RightBrd);
            this.pnlLeftTop.Controls.Add(this.lbl_TopBrd);
            this.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftTop.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftTop.Name = "pnlLeftTop";
            this.pnlLeftTop.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlLeftTop.Size = new System.Drawing.Size(220, 731);
            this.pnlLeftTop.TabIndex = 1;
            // 
            // trvCategory
            // 
            this.trvCategory.BackColor = System.Drawing.Color.White;
            this.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCategory.ForeColor = System.Drawing.Color.Black;
            this.trvCategory.HideSelection = false;
            this.trvCategory.ImageIndex = 0;
            this.trvCategory.ImageList = this.imageList1;
            this.trvCategory.Indent = 19;
            this.trvCategory.ItemHeight = 20;
            this.trvCategory.Location = new System.Drawing.Point(4, 4);
            this.trvCategory.Name = "trvCategory";
            this.trvCategory.SelectedImageIndex = 0;
            this.trvCategory.ShowLines = false;
            this.trvCategory.Size = new System.Drawing.Size(215, 723);
            this.trvCategory.TabIndex = 3;
            this.trvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Immunization.ico");
            this.imageList1.Images.SetKeyName(1, "Bullet06.ico");
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(215, 3);
            this.label5.TabIndex = 9;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 727);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(215, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 727);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(219, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 727);
            this.lbl_RightBrd.TabIndex = 6;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(217, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // pnltls
            // 
            this.pnltls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltls.Controls.Add(this.label6);
            this.pnltls.Controls.Add(this.tlsgloCommunity);
            this.pnltls.Controls.Add(this.btn_Right1);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnltls.Controls.Add(this.label53);
            this.pnltls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltls.Location = new System.Drawing.Point(0, 3);
            this.pnltls.Name = "pnltls";
            this.pnltls.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltls.Size = new System.Drawing.Size(28, 731);
            this.pnltls.TabIndex = 102;
            this.pnltls.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(4, 727);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 1);
            this.label6.TabIndex = 144;
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
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 705);
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
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 727);
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
            this.label53.Size = new System.Drawing.Size(1, 728);
            this.label53.TabIndex = 143;
            // 
            // PanelCombo1
            // 
            this.PanelCombo1.BackColor = System.Drawing.Color.Transparent;
            this.PanelCombo1.Controls.Add(this.pnlTopRight);
            this.PanelCombo1.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelCombo1.Location = new System.Drawing.Point(251, 3);
            this.PanelCombo1.Name = "PanelCombo1";
            this.PanelCombo1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.PanelCombo1.Size = new System.Drawing.Size(644, 24);
            this.PanelCombo1.TabIndex = 103;
            // 
            // pnlTopRight
            // 
            this.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTopRight.Controls.Add(this.label12);
            this.pnlTopRight.Controls.Add(this.cmbLocation);
            this.pnlTopRight.Controls.Add(this.cmbStatus);
            this.pnlTopRight.Controls.Add(this.Label29);
            this.pnlTopRight.Controls.Add(this.label7);
            this.pnlTopRight.Controls.Add(this.label8);
            this.pnlTopRight.Controls.Add(this.label9);
            this.pnlTopRight.Controls.Add(this.label10);
            this.pnlTopRight.Controls.Add(this.label11);
            this.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopRight.Location = new System.Drawing.Point(0, 0);
            this.pnlTopRight.Name = "pnlTopRight";
            this.pnlTopRight.Size = new System.Drawing.Size(641, 24);
            this.pnlTopRight.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 14);
            this.label12.TabIndex = 54;
            this.label12.Text = "Location :";
            // 
            // cmbLocation
            // 
            this.cmbLocation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(72, 1);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(109, 22);
            this.cmbLocation.TabIndex = 55;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(248, 1);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(109, 22);
            this.cmbStatus.TabIndex = 51;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // Label29
            // 
            this.Label29.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.Label29.AutoSize = true;
            this.Label29.Location = new System.Drawing.Point(195, 5);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(50, 14);
            this.Label29.TabIndex = 50;
            this.Label29.Text = "Status :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Location = new System.Drawing.Point(1, 1);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.label7.Size = new System.Drawing.Size(4, 20);
            this.label7.TabIndex = 49;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label8.ForeColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(639, 1);
            this.label8.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 23);
            this.label9.TabIndex = 7;
            this.label9.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label10.Location = new System.Drawing.Point(640, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 23);
            this.label10.TabIndex = 6;
            this.label10.Text = "label3";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.ForeColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(0, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(641, 1);
            this.label11.TabIndex = 8;
            // 
            // UCIMSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.PanelCombo1);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnltls);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCIMSetup";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Size = new System.Drawing.Size(895, 734);
            this.Load += new System.EventHandler(this.UCIMSetup_Load);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.C1IMView)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeftTop.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.PanelCombo1.ResumeLayout(false);
            this.pnlTopRight.ResumeLayout(false);
            this.pnlTopRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Splitter Splitter1;
        internal System.Windows.Forms.Panel pnlLeft;
        internal System.Windows.Forms.Panel pnlLeftTop;
        internal System.Windows.Forms.TreeView trvCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label6;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
        public C1.Win.C1FlexGrid.C1FlexGrid C1IMView;
        internal System.Windows.Forms.Panel PanelCombo1;
        internal System.Windows.Forms.Panel pnlTopRight;
        internal System.Windows.Forms.ComboBox cmbStatus;
        internal System.Windows.Forms.Label Label29;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ImageList imageList1;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.ComboBox cmbLocation;
    }
}
