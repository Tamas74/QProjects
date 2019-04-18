namespace gloCommunity.UserControls
{
    partial class UCBillingConf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBillingConf));
            this.tabblconf = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flxICD = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flxCPT = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.flxMod = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.flxCat = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.flxSpec = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.flxPatr = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.flxPln = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.trvblconf = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.tabblconf.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxICD)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxCPT)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxMod)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxCat)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxSpec)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxPatr)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxPln)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabblconf
            // 
            this.tabblconf.Controls.Add(this.tabPage1);
            this.tabblconf.Controls.Add(this.tabPage2);
            this.tabblconf.Controls.Add(this.tabPage3);
            this.tabblconf.Controls.Add(this.tabPage4);
            this.tabblconf.Controls.Add(this.tabPage5);
            this.tabblconf.Controls.Add(this.tabPage6);
            this.tabblconf.Controls.Add(this.tabPage7);
            this.tabblconf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabblconf.Location = new System.Drawing.Point(1, 4);
            this.tabblconf.Name = "tabblconf";
            this.tabblconf.SelectedIndex = 0;
            this.tabblconf.Size = new System.Drawing.Size(612, 748);
            this.tabblconf.TabIndex = 14;
            this.tabblconf.SelectedIndexChanged += new System.EventHandler(this.tabblconf_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.flxICD);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(604, 721);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ICD9";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(603, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 719);
            this.label23.TabIndex = 13;
            this.label23.Text = "label4";
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 719);
            this.label22.TabIndex = 12;
            this.label22.Text = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 720);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(604, 1);
            this.label2.TabIndex = 11;
            this.label2.Text = "label1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(604, 1);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // flxICD
            // 
            this.flxICD.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxICD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxICD.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxICD.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxICD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxICD.ExtendLastCol = true;
            this.flxICD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxICD.Location = new System.Drawing.Point(0, 0);
            this.flxICD.Name = "flxICD";
            this.flxICD.Rows.Count = 1;
            this.flxICD.Rows.DefaultSize = 21;
            this.flxICD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxICD.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxICD.Size = new System.Drawing.Size(604, 721);
            this.flxICD.StyleInfo = resources.GetString("flxICD.StyleInfo");
            this.flxICD.TabIndex = 4;
            this.flxICD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxICD_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.flxCPT);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(604, 721);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CPT";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(603, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 719);
            this.label19.TabIndex = 13;
            this.label19.Text = "label4";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(0, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1, 719);
            this.label18.TabIndex = 12;
            this.label18.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 720);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(604, 1);
            this.label4.TabIndex = 11;
            this.label4.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(604, 1);
            this.label3.TabIndex = 10;
            this.label3.Text = "label1";
            // 
            // flxCPT
            // 
            this.flxCPT.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxCPT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxCPT.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxCPT.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxCPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxCPT.ExtendLastCol = true;
            this.flxCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxCPT.Location = new System.Drawing.Point(0, 0);
            this.flxCPT.Name = "flxCPT";
            this.flxCPT.Rows.Count = 1;
            this.flxCPT.Rows.DefaultSize = 21;
            this.flxCPT.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxCPT.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxCPT.Size = new System.Drawing.Size(604, 721);
            this.flxCPT.StyleInfo = resources.GetString("flxCPT.StyleInfo");
            this.flxCPT.TabIndex = 5;
            this.flxCPT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxCPT_MouseDown);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.flxMod);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(604, 721);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Modifiers";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // flxMod
            // 
            this.flxMod.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxMod.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxMod.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxMod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxMod.ExtendLastCol = true;
            this.flxMod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxMod.Location = new System.Drawing.Point(1, 1);
            this.flxMod.Name = "flxMod";
            this.flxMod.Rows.Count = 1;
            this.flxMod.Rows.DefaultSize = 21;
            this.flxMod.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxMod.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxMod.Size = new System.Drawing.Size(602, 719);
            this.flxMod.StyleInfo = resources.GetString("flxMod.StyleInfo");
            this.flxMod.TabIndex = 14;
            this.flxMod.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxMod_MouseDown);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(603, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 719);
            this.label17.TabIndex = 13;
            this.label17.Text = "label4";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(0, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 719);
            this.label16.TabIndex = 12;
            this.label16.Text = "label4";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 720);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(604, 1);
            this.label15.TabIndex = 11;
            this.label15.Text = "label1";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(604, 1);
            this.label14.TabIndex = 10;
            this.label14.Text = "label1";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.flxCat);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(604, 721);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Category";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // flxCat
            // 
            this.flxCat.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxCat.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxCat.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxCat.ExtendLastCol = true;
            this.flxCat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxCat.Location = new System.Drawing.Point(0, 0);
            this.flxCat.Name = "flxCat";
            this.flxCat.Rows.Count = 1;
            this.flxCat.Rows.DefaultSize = 21;
            this.flxCat.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxCat.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxCat.Size = new System.Drawing.Size(604, 721);
            this.flxCat.StyleInfo = resources.GetString("flxCat.StyleInfo");
            this.flxCat.TabIndex = 5;
            this.flxCat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxCat_MouseDown);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.flxSpec);
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(604, 721);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Specialty";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // flxSpec
            // 
            this.flxSpec.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxSpec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxSpec.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxSpec.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxSpec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxSpec.ExtendLastCol = true;
            this.flxSpec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxSpec.Location = new System.Drawing.Point(0, 0);
            this.flxSpec.Name = "flxSpec";
            this.flxSpec.Rows.Count = 1;
            this.flxSpec.Rows.DefaultSize = 21;
            this.flxSpec.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxSpec.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxSpec.Size = new System.Drawing.Size(604, 721);
            this.flxSpec.StyleInfo = resources.GetString("flxSpec.StyleInfo");
            this.flxSpec.TabIndex = 5;
            this.flxSpec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxSpec_MouseDown);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.flxPatr);
            this.tabPage6.Location = new System.Drawing.Point(4, 23);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(604, 721);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Patient Relationship";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // flxPatr
            // 
            this.flxPatr.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxPatr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxPatr.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxPatr.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxPatr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxPatr.ExtendLastCol = true;
            this.flxPatr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxPatr.Location = new System.Drawing.Point(0, 0);
            this.flxPatr.Name = "flxPatr";
            this.flxPatr.Rows.Count = 1;
            this.flxPatr.Rows.DefaultSize = 21;
            this.flxPatr.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxPatr.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxPatr.Size = new System.Drawing.Size(604, 721);
            this.flxPatr.StyleInfo = resources.GetString("flxPatr.StyleInfo");
            this.flxPatr.TabIndex = 5;
            this.flxPatr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxPatr_MouseDown);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.flxPln);
            this.tabPage7.Location = new System.Drawing.Point(4, 23);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(604, 721);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Plan Type";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // flxPln
            // 
            this.flxPln.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxPln.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxPln.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxPln.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxPln.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxPln.ExtendLastCol = true;
            this.flxPln.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxPln.Location = new System.Drawing.Point(0, 0);
            this.flxPln.Name = "flxPln";
            this.flxPln.Rows.Count = 1;
            this.flxPln.Rows.DefaultSize = 21;
            this.flxPln.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxPln.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxPln.Size = new System.Drawing.Size(604, 721);
            this.flxPln.StyleInfo = resources.GetString("flxPln.StyleInfo");
            this.flxPln.TabIndex = 5;
            this.flxPln.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxPln_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(28, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(286, 756);
            this.panel1.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.trvblconf);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel3.Size = new System.Drawing.Size(280, 750);
            this.panel3.TabIndex = 25;
            // 
            // trvblconf
            // 
            this.trvblconf.BackColor = System.Drawing.Color.White;
            this.trvblconf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvblconf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvblconf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvblconf.ForeColor = System.Drawing.Color.Black;
            this.trvblconf.HideSelection = false;
            this.trvblconf.Indent = 20;
            this.trvblconf.ItemHeight = 20;
            this.trvblconf.Location = new System.Drawing.Point(0, 0);
            this.trvblconf.Name = "trvblconf";
            this.trvblconf.ShowLines = false;
            this.trvblconf.ShowNodeToolTips = true;
            this.trvblconf.ShowRootLines = false;
            this.trvblconf.Size = new System.Drawing.Size(280, 747);
            this.trvblconf.TabIndex = 29;
            this.trvblconf.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvblconf_NodeMouseClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabblconf);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(314, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel2.Size = new System.Drawing.Size(617, 756);
            this.panel2.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(0, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 748);
            this.label9.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Location = new System.Drawing.Point(613, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 748);
            this.label10.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(0, 752);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(614, 1);
            this.label11.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(0, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(614, 1);
            this.label12.TabIndex = 0;
            // 
            // pnltls
            // 
            this.pnltls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltls.Controls.Add(this.label24);
            this.pnltls.Controls.Add(this.tlsgloCommunity);
            this.pnltls.Controls.Add(this.btn_Right1);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnltls.Controls.Add(this.label53);
            this.pnltls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltls.Location = new System.Drawing.Point(0, 0);
            this.pnltls.Name = "pnltls";
            this.pnltls.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltls.Size = new System.Drawing.Size(28, 756);
            this.pnltls.TabIndex = 104;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(4, 752);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(23, 1);
            this.label24.TabIndex = 144;
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
            this.tlsgloCommunity.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.tlsgloCommunity.Name = "tlsgloCommunity";
            this.tlsgloCommunity.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 730);
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
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 752);
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
            this.label53.Size = new System.Drawing.Size(1, 753);
            this.label53.TabIndex = 143;
            // 
            // UCBillingConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnltls);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCBillingConf";
            this.Size = new System.Drawing.Size(931, 756);
            this.Load += new System.EventHandler(this.UCBillingConf_Load);
            this.tabblconf.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxICD)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxCPT)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxMod)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxCat)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxSpec)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxPatr)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxPln)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabblconf;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public C1.Win.C1FlexGrid.C1FlexGrid flxICD;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public C1.Win.C1FlexGrid.C1FlexGrid flxCPT;
        private System.Windows.Forms.TabPage tabPage3;
        public C1.Win.C1FlexGrid.C1FlexGrid flxMod;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        public C1.Win.C1FlexGrid.C1FlexGrid flxCat;
        public C1.Win.C1FlexGrid.C1FlexGrid flxSpec;
        public C1.Win.C1FlexGrid.C1FlexGrid flxPatr;
        public C1.Win.C1FlexGrid.C1FlexGrid flxPln;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label24;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
        public System.Windows.Forms.TreeView trvblconf;
    }
}
