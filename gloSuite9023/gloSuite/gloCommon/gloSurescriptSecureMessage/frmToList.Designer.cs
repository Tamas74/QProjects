namespace gloSurescriptSecureMessage
{
    partial class frmToList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmToList));
            this.Tb_ToList = new System.Windows.Forms.TabControl();
            this.TbPg_Refferals = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.trvRefferals = new System.Windows.Forms.TreeView();
            this.imgPat = new System.Windows.Forms.ImageList(this.components);
            this.label22 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TbPg_OwnAddress = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.c1OwnAddress = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlSearchOwnAdd = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtOwnSearch = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btnCloseOwnAdd = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblOwnSearch = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TbPg_OtherAddress = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.c1OtherAddress = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pnlSearchOthAdd = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtOtherSearch = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.btnCloseOthAdd = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerOwnAddress = new System.Windows.Forms.Timer(this.components);
            this.timerOtherAddress = new System.Windows.Forms.Timer(this.components);
            this.TopToolStrip = new gloGlobal.gloToolStripIgnoreFocus();
            this.btnPatientList = new System.Windows.Forms.ToolStripButton();
            this.tsb_Saveclose = new System.Windows.Forms.ToolStripButton();
            this.ts_btnClose = new System.Windows.Forms.ToolStripButton();
            this.picProgress = new System.Windows.Forms.PictureBox();
            this.Tb_ToList.SuspendLayout();
            this.TbPg_Refferals.SuspendLayout();
            this.panel3.SuspendLayout();
            this.TbPg_OwnAddress.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1OwnAddress)).BeginInit();
            this.pnlSearchOwnAdd.SuspendLayout();
            this.panel4.SuspendLayout();
            this.TbPg_OtherAddress.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1OtherAddress)).BeginInit();
            this.pnlSearchOthAdd.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // Tb_ToList
            // 
            this.Tb_ToList.Controls.Add(this.TbPg_Refferals);
            this.Tb_ToList.Controls.Add(this.TbPg_OwnAddress);
            this.Tb_ToList.Controls.Add(this.TbPg_OtherAddress);
            this.Tb_ToList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tb_ToList.ItemSize = new System.Drawing.Size(90, 19);
            this.Tb_ToList.Location = new System.Drawing.Point(0, 56);
            this.Tb_ToList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Tb_ToList.Name = "Tb_ToList";
            this.Tb_ToList.SelectedIndex = 0;
            this.Tb_ToList.Size = new System.Drawing.Size(980, 507);
            this.Tb_ToList.TabIndex = 0;
            // 
            // TbPg_Refferals
            // 
            this.TbPg_Refferals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.TbPg_Refferals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TbPg_Refferals.Controls.Add(this.panel3);
            this.TbPg_Refferals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.TbPg_Refferals.Location = new System.Drawing.Point(4, 23);
            this.TbPg_Refferals.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TbPg_Refferals.Name = "TbPg_Refferals";
            this.TbPg_Refferals.Size = new System.Drawing.Size(972, 480);
            this.TbPg_Refferals.TabIndex = 0;
            this.TbPg_Refferals.Tag = "Patient Refferals";
            this.TbPg_Refferals.Text = "Patient\'s Providers";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.trvRefferals);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(972, 480);
            this.panel3.TabIndex = 339;
            this.panel3.TabStop = true;
            // 
            // trvRefferals
            // 
            this.trvRefferals.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvRefferals.CheckBoxes = true;
            this.trvRefferals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvRefferals.FullRowSelect = true;
            this.trvRefferals.ImageIndex = 0;
            this.trvRefferals.ImageList = this.imgPat;
            this.trvRefferals.ItemHeight = 18;
            this.trvRefferals.Location = new System.Drawing.Point(4, 8);
            this.trvRefferals.Name = "trvRefferals";
            this.trvRefferals.SelectedImageIndex = 0;
            this.trvRefferals.Size = new System.Drawing.Size(964, 468);
            this.trvRefferals.TabIndex = 18;
            this.trvRefferals.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvRefferals_AfterCheck);
            // 
            // imgPat
            // 
            this.imgPat.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgPat.ImageStream")));
            this.imgPat.TransparentColor = System.Drawing.Color.Transparent;
            this.imgPat.Images.SetKeyName(0, "PatientDetails.ico");
            this.imgPat.Images.SetKeyName(1, "DirectDoctor.ico");
            this.imgPat.Images.SetKeyName(2, "Refferal.ico");
            this.imgPat.Images.SetKeyName(3, "Patient collaboration.ico");
            this.imgPat.Images.SetKeyName(4, "Orders  Association.ico");
            this.imgPat.Images.SetKeyName(5, "Small Arrow.ico");
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.White;
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(4, 4);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(964, 4);
            this.label22.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 476);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(964, 1);
            this.label3.TabIndex = 7;
            this.label3.Text = "label1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(964, 1);
            this.label4.TabIndex = 6;
            this.label4.Text = "label1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(968, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 474);
            this.label5.TabIndex = 5;
            this.label5.Text = "label4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1, 474);
            this.label6.TabIndex = 4;
            this.label6.Text = "label4";
            // 
            // TbPg_OwnAddress
            // 
            this.TbPg_OwnAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.TbPg_OwnAddress.Controls.Add(this.panel5);
            this.TbPg_OwnAddress.Controls.Add(this.pnlSearchOwnAdd);
            this.TbPg_OwnAddress.Location = new System.Drawing.Point(4, 23);
            this.TbPg_OwnAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TbPg_OwnAddress.Name = "TbPg_OwnAddress";
            this.TbPg_OwnAddress.Size = new System.Drawing.Size(972, 480);
            this.TbPg_OwnAddress.TabIndex = 1;
            this.TbPg_OwnAddress.Tag = "Own Address";
            this.TbPg_OwnAddress.Text = "Practice Contacts";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.c1OwnAddress);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 32);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel5.Size = new System.Drawing.Size(972, 448);
            this.panel5.TabIndex = 340;
            this.panel5.TabStop = true;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 444);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(964, 1);
            this.label11.TabIndex = 7;
            this.label11.Text = "label1";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(964, 1);
            this.label12.TabIndex = 6;
            this.label12.Text = "label1";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(968, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 445);
            this.label13.TabIndex = 5;
            this.label13.Text = "label4";
            // 
            // c1OwnAddress
            // 
            this.c1OwnAddress.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1OwnAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1OwnAddress.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1OwnAddress.ColumnInfo = "0,0,0,0,0,105,Columns:";
            this.c1OwnAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1OwnAddress.ExtendLastCol = true;
            this.c1OwnAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1OwnAddress.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1OwnAddress.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1OwnAddress.Location = new System.Drawing.Point(4, 0);
            this.c1OwnAddress.Name = "c1OwnAddress";
            this.c1OwnAddress.Rows.Count = 5;
            this.c1OwnAddress.Rows.DefaultSize = 21;
            this.c1OwnAddress.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1OwnAddress.ShowCellLabels = true;
            this.c1OwnAddress.Size = new System.Drawing.Size(965, 445);
            this.c1OwnAddress.StyleInfo = resources.GetString("c1OwnAddress.StyleInfo");
            this.c1OwnAddress.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 445);
            this.label14.TabIndex = 4;
            this.label14.Text = "label4";
            // 
            // pnlSearchOwnAdd
            // 
            this.pnlSearchOwnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSearchOwnAdd.BackgroundImage")));
            this.pnlSearchOwnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchOwnAdd.Controls.Add(this.panel4);
            this.pnlSearchOwnAdd.Controls.Add(this.lblOwnSearch);
            this.pnlSearchOwnAdd.Controls.Add(this.label7);
            this.pnlSearchOwnAdd.Controls.Add(this.label8);
            this.pnlSearchOwnAdd.Controls.Add(this.label9);
            this.pnlSearchOwnAdd.Controls.Add(this.label10);
            this.pnlSearchOwnAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchOwnAdd.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchOwnAdd.Name = "pnlSearchOwnAdd";
            this.pnlSearchOwnAdd.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearchOwnAdd.Size = new System.Drawing.Size(972, 32);
            this.pnlSearchOwnAdd.TabIndex = 339;
            this.pnlSearchOwnAdd.TabStop = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.txtOwnSearch);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.btnCloseOwnAdd);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.label27);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(63, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(241, 24);
            this.panel4.TabIndex = 48;
            // 
            // txtOwnSearch
            // 
            this.txtOwnSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOwnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOwnSearch.Location = new System.Drawing.Point(5, 3);
            this.txtOwnSearch.Name = "txtOwnSearch";
            this.txtOwnSearch.Size = new System.Drawing.Size(213, 15);
            this.txtOwnSearch.TabIndex = 26;
            this.txtOwnSearch.TextChanged += new System.EventHandler(this.txtOwnSearch_TextChanged);
            this.txtOwnSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOwnSearch_KeyDown);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Location = new System.Drawing.Point(5, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(213, 3);
            this.label23.TabIndex = 37;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.White;
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(5, 19);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(213, 5);
            this.label24.TabIndex = 43;
            // 
            // btnCloseOwnAdd
            // 
            this.btnCloseOwnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseOwnAdd.FlatAppearance.BorderSize = 0;
            this.btnCloseOwnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCloseOwnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCloseOwnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseOwnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseOwnAdd.Image")));
            this.btnCloseOwnAdd.Location = new System.Drawing.Point(218, 0);
            this.btnCloseOwnAdd.Name = "btnCloseOwnAdd";
            this.btnCloseOwnAdd.Size = new System.Drawing.Size(22, 24);
            this.btnCloseOwnAdd.TabIndex = 30;
            this.btnCloseOwnAdd.UseVisualStyleBackColor = true;
            this.btnCloseOwnAdd.Click += new System.EventHandler(this.btnCloseOwnAdd_Click);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.White;
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Location = new System.Drawing.Point(1, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(4, 24);
            this.label25.TabIndex = 38;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 24);
            this.label26.TabIndex = 39;
            this.label26.Text = "label4";
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Location = new System.Drawing.Point(240, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 24);
            this.label27.TabIndex = 40;
            this.label27.Text = "label4";
            // 
            // lblOwnSearch
            // 
            this.lblOwnSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblOwnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblOwnSearch.Location = new System.Drawing.Point(4, 4);
            this.lblOwnSearch.Name = "lblOwnSearch";
            this.lblOwnSearch.Size = new System.Drawing.Size(59, 24);
            this.lblOwnSearch.TabIndex = 25;
            this.lblOwnSearch.Text = "Search :";
            this.lblOwnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(964, 1);
            this.label7.TabIndex = 7;
            this.label7.Text = "label1";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(964, 1);
            this.label8.TabIndex = 6;
            this.label8.Text = "label1";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(968, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 26);
            this.label9.TabIndex = 5;
            this.label9.Text = "label4";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 26);
            this.label10.TabIndex = 4;
            this.label10.Text = "label4";
            // 
            // TbPg_OtherAddress
            // 
            this.TbPg_OtherAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.TbPg_OtherAddress.Controls.Add(this.panel2);
            this.TbPg_OtherAddress.Controls.Add(this.pnlSearchOthAdd);
            this.TbPg_OtherAddress.Location = new System.Drawing.Point(4, 23);
            this.TbPg_OtherAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TbPg_OtherAddress.Name = "TbPg_OtherAddress";
            this.TbPg_OtherAddress.Size = new System.Drawing.Size(972, 480);
            this.TbPg_OtherAddress.TabIndex = 2;
            this.TbPg_OtherAddress.Tag = "Other Address";
            this.TbPg_OtherAddress.Text = "Surescripts Catalogue";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.c1OtherAddress);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(972, 448);
            this.panel2.TabIndex = 338;
            this.panel2.TabStop = true;
            // 
            // c1OtherAddress
            // 
            this.c1OtherAddress.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1OtherAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1OtherAddress.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1OtherAddress.ColumnInfo = "0,0,0,0,0,105,Columns:";
            this.c1OtherAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1OtherAddress.ExtendLastCol = true;
            this.c1OtherAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1OtherAddress.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1OtherAddress.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1OtherAddress.Location = new System.Drawing.Point(4, 1);
            this.c1OtherAddress.Name = "c1OtherAddress";
            this.c1OtherAddress.Rows.Count = 5;
            this.c1OtherAddress.Rows.DefaultSize = 21;
            this.c1OtherAddress.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1OtherAddress.ShowCellLabels = true;
            this.c1OtherAddress.Size = new System.Drawing.Size(964, 443);
            this.c1OtherAddress.StyleInfo = resources.GetString("c1OtherAddress.StyleInfo");
            this.c1OtherAddress.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 444);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(964, 1);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(964, 1);
            this.label2.TabIndex = 6;
            this.label2.Text = "label1";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(968, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(1, 445);
            this.label15.TabIndex = 5;
            this.label15.Text = "label4";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 445);
            this.label16.TabIndex = 4;
            this.label16.Text = "label4";
            // 
            // pnlSearchOthAdd
            // 
            this.pnlSearchOthAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSearchOthAdd.BackgroundImage")));
            this.pnlSearchOthAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchOthAdd.Controls.Add(this.panel6);
            this.pnlSearchOthAdd.Controls.Add(this.label19);
            this.pnlSearchOthAdd.Controls.Add(this.label17);
            this.pnlSearchOthAdd.Controls.Add(this.label18);
            this.pnlSearchOthAdd.Controls.Add(this.label20);
            this.pnlSearchOthAdd.Controls.Add(this.label21);
            this.pnlSearchOthAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchOthAdd.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchOthAdd.Name = "pnlSearchOthAdd";
            this.pnlSearchOthAdd.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSearchOthAdd.Size = new System.Drawing.Size(972, 32);
            this.pnlSearchOthAdd.TabIndex = 340;
            this.pnlSearchOthAdd.TabStop = true;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.txtOtherSearch);
            this.panel6.Controls.Add(this.picProgress);
            this.panel6.Controls.Add(this.label28);
            this.panel6.Controls.Add(this.label29);
            this.panel6.Controls.Add(this.btnCloseOthAdd);
            this.panel6.Controls.Add(this.label30);
            this.panel6.Controls.Add(this.label31);
            this.panel6.Controls.Add(this.label32);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.ForeColor = System.Drawing.Color.Black;
            this.panel6.Location = new System.Drawing.Point(63, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(241, 24);
            this.panel6.TabIndex = 49;
            // 
            // txtOtherSearch
            // 
            this.txtOtherSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOtherSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOtherSearch.Location = new System.Drawing.Point(5, 3);
            this.txtOtherSearch.Name = "txtOtherSearch";
            this.txtOtherSearch.Size = new System.Drawing.Size(193, 15);
            this.txtOtherSearch.TabIndex = 28;
            this.txtOtherSearch.TextChanged += new System.EventHandler(this.txtOtherSearch_TextChanged);
            this.txtOtherSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherSearch_KeyDown);
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.White;
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Location = new System.Drawing.Point(5, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(213, 3);
            this.label28.TabIndex = 37;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.White;
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Location = new System.Drawing.Point(5, 19);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(213, 5);
            this.label29.TabIndex = 43;
            // 
            // btnCloseOthAdd
            // 
            this.btnCloseOthAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseOthAdd.FlatAppearance.BorderSize = 0;
            this.btnCloseOthAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCloseOthAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCloseOthAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseOthAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseOthAdd.Image")));
            this.btnCloseOthAdd.Location = new System.Drawing.Point(218, 0);
            this.btnCloseOthAdd.Name = "btnCloseOthAdd";
            this.btnCloseOthAdd.Size = new System.Drawing.Size(22, 24);
            this.btnCloseOthAdd.TabIndex = 29;
            this.btnCloseOthAdd.UseVisualStyleBackColor = true;
            this.btnCloseOthAdd.Click += new System.EventHandler(this.btnCloseOthAdd_Click);
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.White;
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Location = new System.Drawing.Point(1, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(4, 24);
            this.label30.TabIndex = 38;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Location = new System.Drawing.Point(0, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 24);
            this.label31.TabIndex = 39;
            this.label31.Text = "label4";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(240, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 24);
            this.label32.TabIndex = 40;
            this.label32.Text = "label4";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Left;
            this.label19.Location = new System.Drawing.Point(4, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 24);
            this.label19.TabIndex = 25;
            this.label19.Text = "Search :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(4, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(964, 1);
            this.label17.TabIndex = 7;
            this.label17.Text = "label1";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(4, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(964, 1);
            this.label18.TabIndex = 6;
            this.label18.Text = "label1";
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(968, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 26);
            this.label20.TabIndex = 5;
            this.label20.Text = "label4";
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Left;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(3, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1, 26);
            this.label21.TabIndex = 4;
            this.label21.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TopToolStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 56);
            this.panel1.TabIndex = 339;
            // 
            // timerOwnAddress
            // 
            this.timerOwnAddress.Tick += new System.EventHandler(this.timerOwnAddress_Tick);
            // 
            // timerOtherAddress
            // 
            this.timerOtherAddress.Tick += new System.EventHandler(this.timerOtherAddress_Tick);
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopToolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopToolStrip.BackgroundImage")));
            this.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopToolStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TopToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPatientList,
            this.tsb_Saveclose,
            this.ts_btnClose});
            this.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.TopToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.Size = new System.Drawing.Size(980, 53);
            this.TopToolStrip.TabIndex = 8;
            this.TopToolStrip.Text = "toolStrip1";
            // 
            // btnPatientList
            // 
            this.btnPatientList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientList.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientList.Image")));
            this.btnPatientList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPatientList.Name = "btnPatientList";
            this.btnPatientList.Size = new System.Drawing.Size(82, 50);
            this.btnPatientList.Text = "&Patient List";
            this.btnPatientList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPatientList.Click += new System.EventHandler(this.btnPatientList_Click);
            // 
            // tsb_Saveclose
            // 
            this.tsb_Saveclose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Saveclose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Saveclose.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Saveclose.Image")));
            this.tsb_Saveclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Saveclose.Name = "tsb_Saveclose";
            this.tsb_Saveclose.Size = new System.Drawing.Size(66, 50);
            this.tsb_Saveclose.Tag = "SaveFeeSchedule";
            this.tsb_Saveclose.Text = "Sa&ve&&Cls";
            this.tsb_Saveclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Saveclose.ToolTipText = "Save and Close";
            this.tsb_Saveclose.Click += new System.EventHandler(this.tsb_Saveclose_Click);
            // 
            // ts_btnClose
            // 
            this.ts_btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.ts_btnClose.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnClose.Image")));
            this.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnClose.Name = "ts_btnClose";
            this.ts_btnClose.Size = new System.Drawing.Size(43, 50);
            this.ts_btnClose.Tag = "Close";
            this.ts_btnClose.Text = "&Close";
            this.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ts_btnClose.ToolTipText = "Close";
            this.ts_btnClose.Click += new System.EventHandler(this.ts_btnClose_Click);
            // 
            // picProgress
            // 
            this.picProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.picProgress.Image = ((System.Drawing.Image)(resources.GetObject("picProgress.Image")));
            this.picProgress.Location = new System.Drawing.Point(198, 3);
            this.picProgress.Name = "picProgress";
            this.picProgress.Size = new System.Drawing.Size(20, 16);
            this.picProgress.TabIndex = 44;
            this.picProgress.TabStop = false;
            this.picProgress.Visible = false;
            // 
            // frmToList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(980, 563);
            this.Controls.Add(this.Tb_ToList);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmToList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Address Book";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmToList_FormClosing);
            this.Load += new System.EventHandler(this.frmToList_Load);
            this.Tb_ToList.ResumeLayout(false);
            this.TbPg_Refferals.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.TbPg_OwnAddress.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1OwnAddress)).EndInit();
            this.pnlSearchOwnAdd.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.TbPg_OtherAddress.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1OtherAddress)).EndInit();
            this.pnlSearchOthAdd.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProgress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl Tb_ToList;
        internal System.Windows.Forms.TabPage TbPg_Refferals;
        internal System.Windows.Forms.TabPage TbPg_OwnAddress;
        internal System.Windows.Forms.TabPage TbPg_OtherAddress;
        private System.Windows.Forms.Panel panel1;
        private gloGlobal.gloToolStripIgnoreFocus TopToolStrip;
        private System.Windows.Forms.ToolStripButton tsb_Saveclose;
        private System.Windows.Forms.ToolStripButton ts_btnClose;
        private System.Windows.Forms.TreeView trvRefferals;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1OwnAddress;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1OtherAddress;
        private System.Windows.Forms.Label lblOwnSearch;
        private System.Windows.Forms.TextBox txtOwnSearch;
        private System.Windows.Forms.Timer timerOwnAddress;
        private System.Windows.Forms.TextBox txtOtherSearch;
        private System.Windows.Forms.Timer timerOtherAddress;
        internal System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Panel pnlSearchOwnAdd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Panel pnlSearchOthAdd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ImageList imgPat;
        private System.Windows.Forms.Button btnCloseOthAdd;
        private System.Windows.Forms.Button btnCloseOwnAdd;
        private System.Windows.Forms.ToolStripButton btnPatientList;
        internal System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        internal System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.Label label28;
        internal System.Windows.Forms.Label label29;
        internal System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.PictureBox picProgress;
    }
}