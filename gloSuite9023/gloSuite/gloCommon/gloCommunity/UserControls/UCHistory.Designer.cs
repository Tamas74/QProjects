namespace gloCommunity.UserControls
{
    partial class UCHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCHistory));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlLeftTop = new System.Windows.Forms.Panel();
            this.trvCategory = new System.Windows.Forms.TreeView();
            this.imgHistory = new System.Windows.Forms.ImageList(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.dgHistory = new System.Windows.Forms.DataGridView();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.pnlLeftTop.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistory)).BeginInit();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.Controls.Add(this.pnlLeftTop);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(28, 3);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(220, 731);
            this.pnlLeft.TabIndex = 3;
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
            this.trvCategory.CheckBoxes = true;
            this.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvCategory.ForeColor = System.Drawing.Color.Black;
            this.trvCategory.HideSelection = false;
            this.trvCategory.ImageIndex = 1;
            this.trvCategory.ImageList = this.imgHistory;
            this.trvCategory.Indent = 19;
            this.trvCategory.ItemHeight = 19;
            this.trvCategory.Location = new System.Drawing.Point(4, 4);
            this.trvCategory.Name = "trvCategory";
            this.trvCategory.SelectedImageIndex = 1;
            this.trvCategory.ShowLines = false;
            this.trvCategory.Size = new System.Drawing.Size(215, 723);
            this.trvCategory.TabIndex = 3;
            this.trvCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterCheck);
            this.trvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);
            // 
            // imgHistory
            // 
            this.imgHistory.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgHistory.ImageStream")));
            this.imgHistory.TransparentColor = System.Drawing.Color.Transparent;
            this.imgHistory.Images.SetKeyName(0, "History.ico");
            this.imgHistory.Images.SetKeyName(1, "Bullet06.ico");
            this.imgHistory.Images.SetKeyName(2, "Medication.ico");
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
            // Splitter1
            // 
            this.Splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Splitter1.Location = new System.Drawing.Point(248, 3);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 731);
            this.Splitter1.TabIndex = 15;
            this.Splitter1.TabStop = false;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.dgHistory);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Label2);
            this.Panel1.Controls.Add(this.Label3);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(251, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.Panel1.Size = new System.Drawing.Size(644, 731);
            this.Panel1.TabIndex = 16;
            // 
            // dgHistory
            // 
            this.dgHistory.AllowUserToAddRows = false;
            this.dgHistory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(143)))), ((int)(((byte)(217)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgHistory.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgHistory.EnableHeadersVisualStyles = false;
            this.dgHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dgHistory.Location = new System.Drawing.Point(1, 1);
            this.dgHistory.Name = "dgHistory";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgHistory.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgHistory.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHistory.Size = new System.Drawing.Size(639, 726);
            this.dgHistory.TabIndex = 9;
            this.dgHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgHistory_CellClick);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label1.Location = new System.Drawing.Point(1, 727);
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
            this.Label2.Location = new System.Drawing.Point(0, 1);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(1, 727);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "label4";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label3.Location = new System.Drawing.Point(640, 1);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(1, 727);
            this.Label3.TabIndex = 6;
            this.Label3.Text = "label3";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(0, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(641, 1);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "label1";
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
            this.pnltls.TabIndex = 101;
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
            // UCHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnltls);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCHistory";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Size = new System.Drawing.Size(895, 734);
            this.Load += new System.EventHandler(this.UCHistory_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeftTop.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgHistory)).EndInit();
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlLeft;
        internal System.Windows.Forms.Panel pnlLeftTop;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        internal System.Windows.Forms.TreeView trvCategory;
        internal System.Windows.Forms.Splitter Splitter1;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ImageList imgHistory;
        internal System.Windows.Forms.DataGridView dgHistory;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label6;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
    }
}
