namespace gloCommunity.UserControls
{
    partial class UCTaskMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTaskMail));
            this.trvsmartdiag = new System.Windows.Forms.TreeView();
            this.Label6 = new System.Windows.Forms.Label();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.tskmailtab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flxfollowup = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flxstattype = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.flxpritype = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.pnlgloUC_TreeView2 = new System.Windows.Forms.Panel();
            this.trvTaskMail = new System.Windows.Forms.TreeView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.Panel5.SuspendLayout();
            this.tskmailtab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxfollowup)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxstattype)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flxpritype)).BeginInit();
            this.pnlgloUC_TreeView2.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvsmartdiag
            // 
            this.trvsmartdiag.BackColor = System.Drawing.Color.White;
            this.trvsmartdiag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvsmartdiag.CheckBoxes = true;
            this.trvsmartdiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvsmartdiag.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvsmartdiag.ForeColor = System.Drawing.Color.Black;
            this.trvsmartdiag.HideSelection = false;
            this.trvsmartdiag.Indent = 21;
            this.trvsmartdiag.ItemHeight = 20;
            this.trvsmartdiag.Location = new System.Drawing.Point(1, 4);
            this.trvsmartdiag.Name = "trvsmartdiag";
            this.trvsmartdiag.ShowLines = false;
            this.trvsmartdiag.Size = new System.Drawing.Size(669, 836);
            this.trvsmartdiag.TabIndex = 0;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label6.Location = new System.Drawing.Point(1, 840);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(669, 1);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "label2";
            // 
            // Panel5
            // 
            this.Panel5.Controls.Add(this.tskmailtab);
            this.Panel5.Controls.Add(this.trvsmartdiag);
            this.Panel5.Controls.Add(this.Label6);
            this.Panel5.Controls.Add(this.Label7);
            this.Panel5.Controls.Add(this.Label8);
            this.Panel5.Controls.Add(this.Label9);
            this.Panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Panel5.Location = new System.Drawing.Point(311, 0);
            this.Panel5.Name = "Panel5";
            this.Panel5.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Panel5.Size = new System.Drawing.Size(674, 844);
            this.Panel5.TabIndex = 8;
            // 
            // tskmailtab
            // 
            this.tskmailtab.Controls.Add(this.tabPage1);
            this.tskmailtab.Controls.Add(this.tabPage2);
            this.tskmailtab.Controls.Add(this.tabPage3);
            this.tskmailtab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tskmailtab.Location = new System.Drawing.Point(1, 4);
            this.tskmailtab.Name = "tskmailtab";
            this.tskmailtab.SelectedIndex = 0;
            this.tskmailtab.Size = new System.Drawing.Size(669, 836);
            this.tskmailtab.TabIndex = 13;
            this.tskmailtab.SelectedIndexChanged += new System.EventHandler(this.tskmailtab_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.flxfollowup);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(661, 809);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Follow Up";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Right;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(660, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(1, 807);
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
            this.label22.Size = new System.Drawing.Size(1, 807);
            this.label22.TabIndex = 12;
            this.label22.Text = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 808);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(661, 1);
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
            this.label1.Size = new System.Drawing.Size(661, 1);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // flxfollowup
            // 
            this.flxfollowup.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxfollowup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxfollowup.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxfollowup.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxfollowup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxfollowup.ExtendLastCol = true;
            this.flxfollowup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxfollowup.Location = new System.Drawing.Point(0, 0);
            this.flxfollowup.Name = "flxfollowup";
            this.flxfollowup.Rows.Count = 1;
            this.flxfollowup.Rows.DefaultSize = 21;
            this.flxfollowup.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxfollowup.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxfollowup.Size = new System.Drawing.Size(661, 809);
            this.flxfollowup.StyleInfo = resources.GetString("flxfollowup.StyleInfo");
            this.flxfollowup.TabIndex = 4;
            this.flxfollowup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxfollowup_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.flxstattype);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(661, 809);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Status Type";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Right;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(660, 1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1, 807);
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
            this.label18.Size = new System.Drawing.Size(1, 807);
            this.label18.TabIndex = 12;
            this.label18.Text = "label4";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 808);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(661, 1);
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
            this.label3.Size = new System.Drawing.Size(661, 1);
            this.label3.TabIndex = 10;
            this.label3.Text = "label1";
            // 
            // flxstattype
            // 
            this.flxstattype.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxstattype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxstattype.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxstattype.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxstattype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxstattype.ExtendLastCol = true;
            this.flxstattype.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxstattype.Location = new System.Drawing.Point(0, 0);
            this.flxstattype.Name = "flxstattype";
            this.flxstattype.Rows.Count = 1;
            this.flxstattype.Rows.DefaultSize = 21;
            this.flxstattype.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxstattype.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxstattype.Size = new System.Drawing.Size(661, 809);
            this.flxstattype.StyleInfo = resources.GetString("flxstattype.StyleInfo");
            this.flxstattype.TabIndex = 5;
            this.flxstattype.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxstattype_MouseDown);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.flxpritype);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(661, 809);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Priority Type";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // flxpritype
            // 
            this.flxpritype.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flxpritype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.flxpritype.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.flxpritype.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:\"DataType:System.Boolean;ImageAlign:CenterCenter;\"" +
                ";}\t1{StyleFixed:\"TextAlign:CenterCenter;\";}\t";
            this.flxpritype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flxpritype.ExtendLastCol = true;
            this.flxpritype.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.flxpritype.Location = new System.Drawing.Point(1, 1);
            this.flxpritype.Name = "flxpritype";
            this.flxpritype.Rows.Count = 1;
            this.flxpritype.Rows.DefaultSize = 21;
            this.flxpritype.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flxpritype.ShowThemedHeaders = C1.Win.C1FlexGrid.ShowThemedHeadersEnum.Rows;
            this.flxpritype.Size = new System.Drawing.Size(659, 807);
            this.flxpritype.StyleInfo = resources.GetString("flxpritype.StyleInfo");
            this.flxpritype.TabIndex = 14;
            this.flxpritype.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flxpritype_MouseDown);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(660, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(1, 807);
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
            this.label16.Size = new System.Drawing.Size(1, 807);
            this.label16.TabIndex = 12;
            this.label16.Text = "label4";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 808);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(661, 1);
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
            this.label14.Size = new System.Drawing.Size(661, 1);
            this.label14.TabIndex = 10;
            this.label14.Text = "label1";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(0, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 837);
            this.Label7.TabIndex = 11;
            this.Label7.Text = "label4";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label8.Location = new System.Drawing.Point(670, 4);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(1, 837);
            this.Label8.TabIndex = 10;
            this.Label8.Text = "label3";
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(0, 3);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(671, 1);
            this.Label9.TabIndex = 9;
            this.Label9.Text = "label1";
            // 
            // Splitter1
            // 
            this.Splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Splitter1.Location = new System.Drawing.Point(308, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 844);
            this.Splitter1.TabIndex = 7;
            this.Splitter1.TabStop = false;
            // 
            // pnlgloUC_TreeView2
            // 
            this.pnlgloUC_TreeView2.Controls.Add(this.trvTaskMail);
            this.pnlgloUC_TreeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgloUC_TreeView2.Location = new System.Drawing.Point(0, 0);
            this.pnlgloUC_TreeView2.Name = "pnlgloUC_TreeView2";
            this.pnlgloUC_TreeView2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlgloUC_TreeView2.Size = new System.Drawing.Size(280, 844);
            this.pnlgloUC_TreeView2.TabIndex = 24;
            // 
            // trvTaskMail
            // 
            this.trvTaskMail.BackColor = System.Drawing.Color.White;
            this.trvTaskMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvTaskMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTaskMail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvTaskMail.ForeColor = System.Drawing.Color.Black;
            this.trvTaskMail.HideSelection = false;
            this.trvTaskMail.Indent = 20;
            this.trvTaskMail.ItemHeight = 20;
            this.trvTaskMail.Location = new System.Drawing.Point(3, 0);
            this.trvTaskMail.Name = "trvTaskMail";
            this.trvTaskMail.ShowLines = false;
            this.trvTaskMail.ShowNodeToolTips = true;
            this.trvTaskMail.ShowRootLines = false;
            this.trvTaskMail.Size = new System.Drawing.Size(277, 841);
            this.trvTaskMail.TabIndex = 30;
            this.trvTaskMail.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvTaskMail_NodeMouseClick);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.pnlgloUC_TreeView2);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel1.Location = new System.Drawing.Point(28, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(280, 844);
            this.Panel1.TabIndex = 6;
            this.Panel1.Visible = false;
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
            this.pnltls.Size = new System.Drawing.Size(28, 844);
            this.pnltls.TabIndex = 103;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(4, 840);
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
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 818);
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
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 840);
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
            this.label53.Size = new System.Drawing.Size(1, 841);
            this.label53.TabIndex = 143;
            // 
            // UCTaskMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.Panel5);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pnltls);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCTaskMail";
            this.Size = new System.Drawing.Size(985, 844);
            this.Panel5.ResumeLayout(false);
            this.tskmailtab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxfollowup)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxstattype)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flxpritype)).EndInit();
            this.pnlgloUC_TreeView2.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView trvsmartdiag;
        private System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Panel Panel5;
        private System.Windows.Forms.TabControl tskmailtab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Splitter Splitter1;
        private System.Windows.Forms.Panel pnlgloUC_TreeView2;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public C1.Win.C1FlexGrid.C1FlexGrid flxfollowup;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public C1.Win.C1FlexGrid.C1FlexGrid flxstattype;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        public C1.Win.C1FlexGrid.C1FlexGrid flxpritype;
        public System.Windows.Forms.Panel pnltls;
        private System.Windows.Forms.Label label24;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
        public System.Windows.Forms.TreeView trvTaskMail;
    }
}
