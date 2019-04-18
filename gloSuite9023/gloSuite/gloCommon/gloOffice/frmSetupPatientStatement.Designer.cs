namespace gloOffice
{
    partial class frmSetupPatientStatement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupPatientStatement));
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trvDataDictionary = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlAddLable = new System.Windows.Forms.Panel();
            this.chk_IncludeLable = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pnltrvSerchAppBook = new System.Windows.Forms.Panel();
            this.txtSearchAppBook = new System.Windows.Forms.TextBox();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.PicBx_Search = new System.Windows.Forms.PictureBox();
            this.lbl_pnltrvSerchAppBookBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnltrvSerchAppBookRightBrd = new System.Windows.Forms.Label();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_CreateTable = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlTemplateDetails = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlTemplate = new System.Windows.Forms.Panel();
            this.wdTemplate = new AxDSOFramer.AxFramerControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlAddLable.SuspendLayout();
            this.pnltrvSerchAppBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlTemplateDetails.SuspendLayout();
            this.pnlTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.panel1);
            this.pnlLeft.Controls.Add(this.panel2);
            this.pnlLeft.Controls.Add(this.pnlAddLable);
            this.pnlLeft.Controls.Add(this.pnltrvSerchAppBook);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 54);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(272, 655);
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
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(272, 570);
            this.panel1.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(4, 569);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(267, 1);
            this.label8.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(271, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 569);
            this.label11.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(268, 1);
            this.label5.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 570);
            this.label9.TabIndex = 15;
            // 
            // trvDataDictionary
            // 
            this.trvDataDictionary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvDataDictionary.CheckBoxes = true;
            this.trvDataDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDataDictionary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvDataDictionary.ForeColor = System.Drawing.Color.Black;
            this.trvDataDictionary.HideSelection = false;
            this.trvDataDictionary.Indent = 20;
            this.trvDataDictionary.ItemHeight = 20;
            this.trvDataDictionary.Location = new System.Drawing.Point(3, 0);
            this.trvDataDictionary.Name = "trvDataDictionary";
            this.trvDataDictionary.ShowNodeToolTips = true;
            this.trvDataDictionary.Size = new System.Drawing.Size(269, 570);
            this.trvDataDictionary.TabIndex = 0;
            this.trvDataDictionary.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvDataDictionary_AfterCheck);
            this.trvDataDictionary.DoubleClick += new System.EventHandler(this.trvDataDictionary_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.panel2.Size = new System.Drawing.Size(272, 26);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.btnSelectAll);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.btnClearAll);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.btnDown);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.btnUp);
            this.panel3.Controls.Add(this.label25);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(269, 23);
            this.panel3.TabIndex = 0;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnSelectAll.FlatAppearance.BorderSize = 0;
            this.btnSelectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.Location = new System.Drawing.Point(164, 1);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(21, 21);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(185, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(5, 21);
            this.label22.TabIndex = 27;
            // 
            // btnClearAll
            // 
            this.btnClearAll.BackColor = System.Drawing.Color.Transparent;
            this.btnClearAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClearAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClearAll.FlatAppearance.BorderSize = 0;
            this.btnClearAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAll.Image")));
            this.btnClearAll.Location = new System.Drawing.Point(190, 1);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(21, 21);
            this.btnClearAll.TabIndex = 1;
            this.btnClearAll.UseVisualStyleBackColor = false;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Location = new System.Drawing.Point(211, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(5, 21);
            this.label23.TabIndex = 28;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(216, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(21, 21);
            this.btnDown.TabIndex = 2;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Location = new System.Drawing.Point(237, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(5, 21);
            this.label24.TabIndex = 29;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(242, 1);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(21, 21);
            this.btnUp.TabIndex = 3;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(263, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(5, 21);
            this.label25.TabIndex = 30;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Location = new System.Drawing.Point(1, 22);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(267, 1);
            this.label21.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.label6.Location = new System.Drawing.Point(268, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 22);
            this.label6.TabIndex = 21;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(1, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(268, 1);
            this.label16.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 23);
            this.label19.TabIndex = 20;
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
            this.pnlAddLable.Location = new System.Drawing.Point(0, 625);
            this.pnlAddLable.Name = "pnlAddLable";
            this.pnlAddLable.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlAddLable.Size = new System.Drawing.Size(272, 30);
            this.pnlAddLable.TabIndex = 3;
            this.pnlAddLable.Visible = false;
            // 
            // chk_IncludeLable
            // 
            this.chk_IncludeLable.AutoSize = true;
            this.chk_IncludeLable.Location = new System.Drawing.Point(8, 6);
            this.chk_IncludeLable.Name = "chk_IncludeLable";
            this.chk_IncludeLable.Size = new System.Drawing.Size(98, 18);
            this.chk_IncludeLable.TabIndex = 0;
            this.chk_IncludeLable.Text = "Include Lable";
            this.chk_IncludeLable.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Location = new System.Drawing.Point(3, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 22);
            this.label13.TabIndex = 41;
            this.label13.Text = "label4";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Location = new System.Drawing.Point(3, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(268, 1);
            this.label17.TabIndex = 35;
            this.label17.Text = "label1";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(3, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(268, 1);
            this.label18.TabIndex = 36;
            this.label18.Text = "label1";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(271, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 24);
            this.label20.TabIndex = 40;
            this.label20.Text = "label4";
            // 
            // pnltrvSerchAppBook
            // 
            this.pnltrvSerchAppBook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltrvSerchAppBook.Controls.Add(this.txtSearchAppBook);
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
            this.pnltrvSerchAppBook.Size = new System.Drawing.Size(272, 29);
            this.pnltrvSerchAppBook.TabIndex = 0;
            // 
            // txtSearchAppBook
            // 
            this.txtSearchAppBook.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchAppBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchAppBook.ForeColor = System.Drawing.Color.Black;
            this.txtSearchAppBook.Location = new System.Drawing.Point(31, 8);
            this.txtSearchAppBook.Name = "txtSearchAppBook";
            this.txtSearchAppBook.Size = new System.Drawing.Size(240, 15);
            this.txtSearchAppBook.TabIndex = 0;
            this.txtSearchAppBook.TextChanged += new System.EventHandler(this.txtSearchAppBook_TextChanged);
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(31, 4);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(240, 4);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(31, 23);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(240, 2);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // PicBx_Search
            // 
            this.PicBx_Search.BackColor = System.Drawing.Color.White;
            this.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.PicBx_Search.Image = ((System.Drawing.Image)(resources.GetObject("PicBx_Search.Image")));
            this.PicBx_Search.Location = new System.Drawing.Point(4, 4);
            this.PicBx_Search.Name = "PicBx_Search";
            this.PicBx_Search.Size = new System.Drawing.Size(27, 21);
            this.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBx_Search.TabIndex = 9;
            this.PicBx_Search.TabStop = false;
            // 
            // lbl_pnltrvSerchAppBookBottomBrd
            // 
            this.lbl_pnltrvSerchAppBookBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnltrvSerchAppBookBottomBrd.Location = new System.Drawing.Point(4, 25);
            this.lbl_pnltrvSerchAppBookBottomBrd.Name = "lbl_pnltrvSerchAppBookBottomBrd";
            this.lbl_pnltrvSerchAppBookBottomBrd.Size = new System.Drawing.Size(267, 1);
            this.lbl_pnltrvSerchAppBookBottomBrd.TabIndex = 35;
            this.lbl_pnltrvSerchAppBookBottomBrd.Text = "label1";
            // 
            // lbl_pnltrvSerchAppBookTopBrd
            // 
            this.lbl_pnltrvSerchAppBookTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnltrvSerchAppBookTopBrd.Location = new System.Drawing.Point(4, 3);
            this.lbl_pnltrvSerchAppBookTopBrd.Name = "lbl_pnltrvSerchAppBookTopBrd";
            this.lbl_pnltrvSerchAppBookTopBrd.Size = new System.Drawing.Size(267, 1);
            this.lbl_pnltrvSerchAppBookTopBrd.TabIndex = 36;
            this.lbl_pnltrvSerchAppBookTopBrd.Text = "label1";
            // 
            // lbl_pnltrvSerchAppBookLeftBrd
            // 
            this.lbl_pnltrvSerchAppBookLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnltrvSerchAppBookLeftBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_pnltrvSerchAppBookLeftBrd.Name = "lbl_pnltrvSerchAppBookLeftBrd";
            this.lbl_pnltrvSerchAppBookLeftBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnltrvSerchAppBookLeftBrd.TabIndex = 39;
            this.lbl_pnltrvSerchAppBookLeftBrd.Text = "label4";
            // 
            // lbl_pnltrvSerchAppBookRightBrd
            // 
            this.lbl_pnltrvSerchAppBookRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnltrvSerchAppBookRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnltrvSerchAppBookRightBrd.Location = new System.Drawing.Point(271, 3);
            this.lbl_pnltrvSerchAppBookRightBrd.Name = "lbl_pnltrvSerchAppBookRightBrd";
            this.lbl_pnltrvSerchAppBookRightBrd.Size = new System.Drawing.Size(1, 23);
            this.lbl_pnltrvSerchAppBookRightBrd.TabIndex = 40;
            this.lbl_pnltrvSerchAppBookRightBrd.Text = "label4";
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(1064, 54);
            this.pnlToolStrip.TabIndex = 4;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Commands.BackgroundImage")));
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_CreateTable,
            this.tsb_Save,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1064, 53);
            this.ts_Commands.TabIndex = 10;
            this.ts_Commands.Text = "ToolStrip1";
            // 
            // tsb_CreateTable
            // 
            this.tsb_CreateTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_CreateTable.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CreateTable.Image")));
            this.tsb_CreateTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CreateTable.Name = "tsb_CreateTable";
            this.tsb_CreateTable.Size = new System.Drawing.Size(87, 50);
            this.tsb_CreateTable.Tag = "CreateTable";
            this.tsb_CreateTable.Text = "&Create Table";
            this.tsb_CreateTable.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_CreateTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_CreateTable.Click += new System.EventHandler(this.tsb_CreateTable_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "Save";
            this.tsb_Save.Text = "&Save&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
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
            // pnlTemplateDetails
            // 
            this.pnlTemplateDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlTemplateDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateDetails.Controls.Add(this.label12);
            this.pnlTemplateDetails.Controls.Add(this.cmbProvider);
            this.pnlTemplateDetails.Controls.Add(this.label10);
            this.pnlTemplateDetails.Controls.Add(this.txtTemplateName);
            this.pnlTemplateDetails.Controls.Add(this.lblTemplateName);
            this.pnlTemplateDetails.Controls.Add(this.label7);
            this.pnlTemplateDetails.Controls.Add(this.label14);
            this.pnlTemplateDetails.Controls.Add(this.label15);
            this.pnlTemplateDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateDetails.Location = new System.Drawing.Point(275, 54);
            this.pnlTemplateDetails.Name = "pnlTemplateDetails";
            this.pnlTemplateDetails.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pnlTemplateDetails.Size = new System.Drawing.Size(789, 42);
            this.pnlTemplateDetails.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Right;
            this.label12.Location = new System.Drawing.Point(785, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1, 34);
            this.label12.TabIndex = 25;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(463, 10);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(213, 22);
            this.cmbProvider.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(367, 12);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(92, 19);
            this.label10.TabIndex = 23;
            this.label10.Text = "Provider :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplateName.ForeColor = System.Drawing.Color.Black;
            this.txtTemplateName.Location = new System.Drawing.Point(137, 10);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(212, 22);
            this.txtTemplateName.TabIndex = 0;
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplateName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateName.Location = new System.Drawing.Point(1, 12);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTemplateName.Size = new System.Drawing.Size(131, 19);
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
            this.label7.Size = new System.Drawing.Size(1, 34);
            this.label7.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(0, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(786, 1);
            this.label14.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(0, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(786, 1);
            this.label15.TabIndex = 20;
            // 
            // pnlTemplate
            // 
            this.pnlTemplate.Controls.Add(this.wdTemplate);
            this.pnlTemplate.Controls.Add(this.label4);
            this.pnlTemplate.Controls.Add(this.label3);
            this.pnlTemplate.Controls.Add(this.label2);
            this.pnlTemplate.Controls.Add(this.label1);
            this.pnlTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplate.Location = new System.Drawing.Point(275, 96);
            this.pnlTemplate.Name = "pnlTemplate";
            this.pnlTemplate.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pnlTemplate.Size = new System.Drawing.Size(789, 613);
            this.pnlTemplate.TabIndex = 3;
            // 
            // wdTemplate
            // 
            this.wdTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wdTemplate.Enabled = true;
            this.wdTemplate.Location = new System.Drawing.Point(1, 1);
            this.wdTemplate.Name = "wdTemplate";
            this.wdTemplate.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wdTemplate.OcxState")));
            this.wdTemplate.Size = new System.Drawing.Size(784, 608);
            this.wdTemplate.TabIndex = 0;
            this.wdTemplate.BeforeDocumentClosed += new AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEventHandler(this.wdTemplate_BeforeDocumentClosed);
            this.wdTemplate.OnDocumentOpened += new AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEventHandler(this.wdTemplate_OnDocumentOpened);
            this.wdTemplate.OnDocumentClosed += new System.EventHandler(this.wdTemplate_OnDocumentClosed);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(784, 1);
            this.label4.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(1, 609);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(784, 1);
            this.label3.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 610);
            this.label2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(785, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 610);
            this.label1.TabIndex = 17;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.splitter1.Location = new System.Drawing.Point(272, 54);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 655);
            this.splitter1.TabIndex = 22;
            this.splitter1.TabStop = false;
            // 
            // frmSetupPatientStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1064, 709);
            this.Controls.Add(this.pnlTemplate);
            this.Controls.Add(this.pnlTemplateDetails);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetupPatientStatement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup Patient Statement";
            this.Load += new System.EventHandler(this.frmSetupPatientStatement_Load);
            this.pnlLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlAddLable.ResumeLayout(false);
            this.pnlAddLable.PerformLayout();
            this.pnltrvSerchAppBook.ResumeLayout(false);
            this.pnltrvSerchAppBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBx_Search)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlTemplateDetails.ResumeLayout(false);
            this.pnlTemplateDetails.PerformLayout();
            this.pnlTemplate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wdTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TreeView trvDataDictionary;
        internal System.Windows.Forms.Panel pnlAddLable;
        private System.Windows.Forms.CheckBox chk_IncludeLable;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.Panel pnltrvSerchAppBook;
        private System.Windows.Forms.TextBox txtSearchAppBook;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.PictureBox PicBx_Search;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookBottomBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookTopBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookLeftBrd;
        private System.Windows.Forms.Label lbl_pnltrvSerchAppBookRightBrd;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        private System.Windows.Forms.Panel pnlTemplateDetails;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel pnlTemplate;
        private AxDSOFramer.AxFramerControl wdTemplate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Splitter splitter1;
        internal System.Windows.Forms.ToolStripButton tsb_CreateTable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
    }
}