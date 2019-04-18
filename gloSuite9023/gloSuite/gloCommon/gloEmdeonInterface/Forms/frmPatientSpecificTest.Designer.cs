namespace gloEmdeonInterface.Forms
{
    partial class frmPatientSpecificTest
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
                    System.Windows.Forms.ContextMenu[] cntmnuControls = { c1PatientResultRange.ContextMenu };
                  //  System.Windows.Forms.Control[] cntControls = { c1PatientRes };
                    if (cntmnuControls != null)
                    {
                        if (cntmnuControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntmnuControls);

                        }
                    }

                    gloGlobal.cEventHelper.DisposeContextMenu(ref cntmnuControls);

                        
                    //if (c1PatientResultRange.ContextMenu != null)
                    //{
                    //    gloGlobal.cEventHelper.RemoveAllEventHandlers(c1PatientResultRange.ContextMenu);
                    //    if (c1PatientResultRange.ContextMenu.MenuItems != null)
                    //    {
                    //        c1PatientResultRange.ContextMenu.MenuItems.Clear();
                    //    }
                    //    c1PatientResultRange.ContextMenu.Dispose();
                    //    c1PatientResultRange.ContextMenu = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPatientSpecificTest));
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlList_Detail = new System.Windows.Forms.Panel();
            this.pnltrvList = new System.Windows.Forms.Panel();
            this.GloUC_trvTest = new gloUserControlLibrary.gloUC_TreeView();
            this.trvList = new System.Windows.Forms.TreeView();
            this.pnl_btnTests = new System.Windows.Forms.Panel();
            this.btnTests = new System.Windows.Forms.Button();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtListSearch = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1PatientResultRange = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_LabMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbbtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tlbbtnClose = new System.Windows.Forms.ToolStripButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlLeft.SuspendLayout();
            this.pnlList_Detail.SuspendLayout();
            this.pnltrvList.SuspendLayout();
            this.pnl_btnTests.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientResultRange)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.ts_LabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlLeft.Controls.Add(this.pnlList_Detail);
            this.pnlLeft.Controls.Add(this.pnlSearch);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlLeft.Location = new System.Drawing.Point(0, 54);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlLeft.Size = new System.Drawing.Size(252, 618);
            this.pnlLeft.TabIndex = 6;
            // 
            // pnlList_Detail
            // 
            this.pnlList_Detail.Controls.Add(this.pnltrvList);
            this.pnlList_Detail.Controls.Add(this.pnl_btnTests);
            this.pnlList_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList_Detail.Location = new System.Drawing.Point(0, 3);
            this.pnlList_Detail.Name = "pnlList_Detail";
            this.pnlList_Detail.Size = new System.Drawing.Size(252, 615);
            this.pnlList_Detail.TabIndex = 8;
            // 
            // pnltrvList
            // 
            this.pnltrvList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltrvList.Controls.Add(this.GloUC_trvTest);
            this.pnltrvList.Controls.Add(this.trvList);
            this.pnltrvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltrvList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnltrvList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnltrvList.Location = new System.Drawing.Point(0, 30);
            this.pnltrvList.Name = "pnltrvList";
            this.pnltrvList.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltrvList.Size = new System.Drawing.Size(252, 585);
            this.pnltrvList.TabIndex = 5;
            // 
            // GloUC_trvTest
            // 
            this.GloUC_trvTest.BackColor = System.Drawing.Color.Transparent;
            this.GloUC_trvTest.CheckBoxes = false;
            this.GloUC_trvTest.CodeMember = null;
            
            this.GloUC_trvTest.DescriptionMember = null;
            this.GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
            this.GloUC_trvTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GloUC_trvTest.DrugFlag = ((short)(16));
            this.GloUC_trvTest.DrugFormMember = null;
            this.GloUC_trvTest.DrugQtyQualifierMember = null;
            this.GloUC_trvTest.DurationMember = null;
            this.GloUC_trvTest.FrequencyMember = null;
            this.GloUC_trvTest.ImageIndex = -1;
            this.GloUC_trvTest.ImageList = null;
            this.GloUC_trvTest.ImageObject = null;
            this.GloUC_trvTest.IsDrug = false;
            this.GloUC_trvTest.IsNarcoticsMember = null;
            this.GloUC_trvTest.Location = new System.Drawing.Point(3, 0);
            this.GloUC_trvTest.MaximumNodes = 1000;
            this.GloUC_trvTest.Name = "GloUC_trvTest";
            this.GloUC_trvTest.NDCCodeMember = null;
            this.GloUC_trvTest.ParentImageIndex = 0;
            this.GloUC_trvTest.ParentMember = null;
            this.GloUC_trvTest.RouteMember = null;
            this.GloUC_trvTest.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring;
            this.GloUC_trvTest.SearchBox = true;
            this.GloUC_trvTest.SearchText = null;
            this.GloUC_trvTest.SelectedImageIndex = -1;
            this.GloUC_trvTest.SelectedNode = null;
            this.GloUC_trvTest.SelectedNodeIDs = ((System.Collections.ArrayList)(resources.GetObject("GloUC_trvTest.SelectedNodeIDs")));
            this.GloUC_trvTest.SelectedParentImageIndex = 0;
            this.GloUC_trvTest.Size = new System.Drawing.Size(249, 582);
            this.GloUC_trvTest.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription;
            this.GloUC_trvTest.TabIndex = 42;
            this.GloUC_trvTest.Tag = null;
            this.GloUC_trvTest.UnitMember = null;
            this.GloUC_trvTest.ValueMember = null;
            this.GloUC_trvTest.NodeMouseDoubleClick += new gloUserControlLibrary.gloUC_TreeView.NodeMouseDoubleClickEventHandler(this.GloUC_trvTest_NodeMouseDoubleClick);
            // 
            // trvList
            // 
            this.trvList.BackColor = System.Drawing.Color.White;
            this.trvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvList.ForeColor = System.Drawing.Color.Black;
            this.trvList.HideSelection = false;
            this.trvList.Indent = 20;
            this.trvList.ItemHeight = 20;
            this.trvList.Location = new System.Drawing.Point(8, 5);
            this.trvList.Name = "trvList";
            this.trvList.ShowNodeToolTips = true;
            this.trvList.ShowPlusMinus = false;
            this.trvList.ShowRootLines = false;
            this.trvList.Size = new System.Drawing.Size(226, 500);
            this.trvList.TabIndex = 3;
            this.trvList.Visible = false;
            // 
            // pnl_btnTests
            // 
            this.pnl_btnTests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_btnTests.Controls.Add(this.btnTests);
            this.pnl_btnTests.Controls.Add(this.Label15);
            this.pnl_btnTests.Controls.Add(this.Label16);
            this.pnl_btnTests.Controls.Add(this.Label17);
            this.pnl_btnTests.Controls.Add(this.Label18);
            this.pnl_btnTests.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_btnTests.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_btnTests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_btnTests.Location = new System.Drawing.Point(0, 0);
            this.pnl_btnTests.Name = "pnl_btnTests";
            this.pnl_btnTests.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnl_btnTests.Size = new System.Drawing.Size(252, 30);
            this.pnl_btnTests.TabIndex = 4;
            // 
            // btnTests
            // 
            this.btnTests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnTests.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTests.BackgroundImage")));
            this.btnTests.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTests.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnTests.FlatAppearance.BorderSize = 0;
            this.btnTests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTests.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTests.Location = new System.Drawing.Point(4, 1);
            this.btnTests.Name = "btnTests";
            this.btnTests.Size = new System.Drawing.Size(247, 25);
            this.btnTests.TabIndex = 0;
            this.btnTests.Text = "Tests";
            this.btnTests.UseVisualStyleBackColor = false;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label15.Location = new System.Drawing.Point(4, 26);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(247, 1);
            this.Label15.TabIndex = 18;
            this.Label15.Text = "label2";
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.Location = new System.Drawing.Point(3, 1);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(1, 26);
            this.Label16.TabIndex = 17;
            this.Label16.Text = "label4";
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label17.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label17.Location = new System.Drawing.Point(251, 1);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(1, 26);
            this.Label17.TabIndex = 16;
            this.Label17.Text = "label3";
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.Location = new System.Drawing.Point(3, 0);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(249, 1);
            this.Label18.TabIndex = 15;
            this.Label18.Text = "label1";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearch.Controls.Add(this.txtListSearch);
            this.pnlSearch.Controls.Add(this.Label20);
            this.pnlSearch.Controls.Add(this.Label21);
            this.pnlSearch.Controls.Add(this.PictureBox1);
            this.pnlSearch.Controls.Add(this.label9);
            this.pnlSearch.Controls.Add(this.Label12);
            this.pnlSearch.Controls.Add(this.label10);
            this.pnlSearch.Controls.Add(this.label11);
            this.pnlSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.ForeColor = System.Drawing.Color.Black;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlSearch.Size = new System.Drawing.Size(235, 29);
            this.pnlSearch.TabIndex = 16;
            this.pnlSearch.Visible = false;
            // 
            // txtListSearch
            // 
            this.txtListSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtListSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtListSearch.ForeColor = System.Drawing.Color.Black;
            this.txtListSearch.Location = new System.Drawing.Point(32, 8);
            this.txtListSearch.Name = "txtListSearch";
            this.txtListSearch.Size = new System.Drawing.Size(202, 15);
            this.txtListSearch.TabIndex = 0;
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.White;
            this.Label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label20.Location = new System.Drawing.Point(32, 4);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(202, 4);
            this.Label20.TabIndex = 37;
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.White;
            this.Label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label21.Location = new System.Drawing.Point(32, 23);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(202, 2);
            this.Label21.TabIndex = 38;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.White;
            this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.PictureBox1.Location = new System.Drawing.Point(4, 4);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(28, 21);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox1.TabIndex = 9;
            this.PictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(4, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(230, 1);
            this.label9.TabIndex = 35;
            this.label9.Text = "label1";
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label12.Location = new System.Drawing.Point(4, 3);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(230, 1);
            this.Label12.TabIndex = 36;
            this.Label12.Text = "label1";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 23);
            this.label10.TabIndex = 39;
            this.label10.Text = "label4";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Location = new System.Drawing.Point(234, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 23);
            this.label11.TabIndex = 40;
            this.label11.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.c1PatientResultRange);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(255, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panel1.Size = new System.Drawing.Size(656, 618);
            this.panel1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 614);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(651, 1);
            this.label4.TabIndex = 21;
            this.label4.Text = "label1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(651, 1);
            this.label3.TabIndex = 20;
            this.label3.Text = "label1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(652, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 612);
            this.label2.TabIndex = 19;
            this.label2.Text = "label4";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1, 612);
            this.label1.TabIndex = 18;
            this.label1.Text = "label4";
            // 
            // c1PatientResultRange
            // 
            this.c1PatientResultRange.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1PatientResultRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.c1PatientResultRange.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientResultRange.ColumnInfo = "10,1,0,0,0,95,Columns:0{Visible:False;}\t1{AllowDragging:False;}\t";
            this.c1PatientResultRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientResultRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientResultRange.Location = new System.Drawing.Point(0, 3);
            this.c1PatientResultRange.Name = "c1PatientResultRange";
            this.c1PatientResultRange.Rows.DefaultSize = 19;
            this.c1PatientResultRange.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientResultRange.ShowCellLabels = true;
            this.c1PatientResultRange.Size = new System.Drawing.Size(653, 612);
            this.c1PatientResultRange.StyleInfo = resources.GetString("c1PatientResultRange.StyleInfo");
            this.c1PatientResultRange.TabIndex = 2;
            this.c1PatientResultRange.EnterCell += new System.EventHandler(this.c1PatientResultRange_EnterCell);
            this.c1PatientResultRange.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientResultRange_AfterEdit);
            this.c1PatientResultRange.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1PatientResultRange_MouseDown);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.Controls.Add(this.ts_LabMain);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Size = new System.Drawing.Size(911, 54);
            this.pnlToolStrip.TabIndex = 8;
            // 
            // ts_LabMain
            // 
            this.ts_LabMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_LabMain.BackgroundImage")));
            this.ts_LabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_LabMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_LabMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_LabMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbbtnNew,
            this.toolStripButton1,
            this.tlbbtnSave,
            this.tlbbtnClose});
            this.ts_LabMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_LabMain.Location = new System.Drawing.Point(0, 0);
            this.ts_LabMain.Name = "ts_LabMain";
            this.ts_LabMain.Size = new System.Drawing.Size(911, 53);
            this.ts_LabMain.TabIndex = 0;
            this.ts_LabMain.Text = "toolStrip1";
            // 
            // tlbbtnNew
            // 
            this.tlbbtnNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnNew.Image")));
            this.tlbbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnNew.Name = "tlbbtnNew";
            this.tlbbtnNew.Size = new System.Drawing.Size(37, 50);
            this.tlbbtnNew.Text = "&New";
            this.tlbbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnNew.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(79, 50);
            this.toolStripButton1.Text = "Remove All";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.ToolTipText = "Remove All";
            this.toolStripButton1.Visible = false;
            // 
            // tlbbtnSave
            // 
            this.tlbbtnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnSave.Image")));
            this.tlbbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnSave.Name = "tlbbtnSave";
            this.tlbbtnSave.Size = new System.Drawing.Size(66, 50);
            this.tlbbtnSave.Text = "&Save&&Cls";
            this.tlbbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnSave.ToolTipText = "Save and Close";
            this.tlbbtnSave.Click += new System.EventHandler(this.tlbbtnSave_Click);
            // 
            // tlbbtnClose
            // 
            this.tlbbtnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbbtnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tlbbtnClose.Image")));
            this.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbbtnClose.Name = "tlbbtnClose";
            this.tlbbtnClose.Size = new System.Drawing.Size(43, 50);
            this.tlbbtnClose.Text = "&Close";
            this.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbbtnClose.ToolTipText = "Close";
            this.tlbbtnClose.Click += new System.EventHandler(this.tlbbtnClose_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(252, 54);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 618);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // frmPatientSpecificTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(911, 672);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPatientSpecificTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient Specific Result Range";
            this.Load += new System.EventHandler(this.frmPatientSpecificTest_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPatientSpecificTest_FormClosing);
            this.pnlLeft.ResumeLayout(false);
            this.pnlList_Detail.ResumeLayout(false);
            this.pnltrvList.ResumeLayout(false);
            this.pnl_btnTests.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientResultRange)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_LabMain.ResumeLayout(false);
            this.ts_LabMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlLeft;
        internal System.Windows.Forms.Panel pnlList_Detail;
        private System.Windows.Forms.Panel pnltrvList;
        internal gloUserControlLibrary.gloUC_TreeView GloUC_trvTest;
        internal System.Windows.Forms.TreeView trvList;
        private System.Windows.Forms.Panel pnl_btnTests;
        internal System.Windows.Forms.Button btnTests;
        private System.Windows.Forms.Label Label15;
        private System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label Label17;
        private System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.Panel pnlSearch;
        internal System.Windows.Forms.TextBox txtListSearch;
        internal System.Windows.Forms.Label Label20;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.PictureBox PictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label Label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        internal C1.Win.C1FlexGrid.C1FlexGrid c1PatientResultRange;
        internal System.Windows.Forms.Panel pnlToolStrip;
        private gloGlobal.gloToolStripIgnoreFocus ts_LabMain;
        private System.Windows.Forms.ToolStripButton tlbbtnNew;
        private System.Windows.Forms.ToolStripButton tlbbtnSave;
        private System.Windows.Forms.ToolStripButton tlbbtnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Splitter splitter1;
    }
}