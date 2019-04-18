namespace gloListControl
{
    partial class gloListControl
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
                if (SelectedItems != null)
                {
                    _SelectedItems.Clear();
                    _SelectedItems.Dispose();
                    _SelectedItems = null;
                }
                if (glItems != null)
                {
                    glItems.Clear();
                    glItems.Dispose();
                    glItems = null;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloListControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgListSubView = new System.Windows.Forms.DataGridView();
            this.trvItems = new System.Windows.Forms.TreeView();
            this.pnlToolStrip = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_UserGroups = new System.Windows.Forms.ToolStripButton();
            this.tsb_New = new System.Windows.Forms.ToolStripButton();
            this.tsb_Modify = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowAllRace = new System.Windows.Forms.ToolStripButton();
            this.tsb_ShowFavorites = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.dgListView = new System.Windows.Forms.DataGridView();
            this.pnlDataGridList = new System.Windows.Forms.Panel();
            this.lbl_pnlDataGridListTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlDataGridListBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlDataGridListRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnlDataGridListLeftBrd = new System.Windows.Forms.Label();
            this.pnl_TOP = new System.Windows.Forms.Panel();
            this.pnl_Base = new System.Windows.Forms.Panel();
            this.pnl_ProcedureResource = new System.Windows.Forms.Panel();
            this.lbl_pnl_ProcedureResourceRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_ProcedureResourceLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_ProcedureResourceTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_ProcedureResourceBottomBrd = new System.Windows.Forms.Label();
            this.pnlCommand = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pnl_ProcedureResourceHeader = new System.Windows.Forms.Panel();
            this.lbl_pnl_ProcedureResourceHeaderTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_ProcedureResourceHeaderBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_ProcedureResourceHeaderRightBrd = new System.Windows.Forms.Label();
            this.lbl_pnl_ProcedureResourceHeaderLeftBrd = new System.Windows.Forms.Label();
            this.lblProcedureResourceHeader = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.chkIncludeDescription = new System.Windows.Forms.CheckBox();
            this.chkIncludeAllUsers = new System.Windows.Forms.CheckBox();
            this.pnlExistsPredicate = new System.Windows.Forms.Panel();
            this.rd_ANDPredicate = new System.Windows.Forms.RadioButton();
            this.rd_ORPredicate = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbSpeciality = new System.Windows.Forms.ComboBox();
            this.lblSpeciality = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.cmbTypeOfservice = new System.Windows.Forms.ComboBox();
            this.lblTOS = new System.Windows.Forms.Label();
            this.lbl_pnlSearchSpaceMiddle = new System.Windows.Forms.Label();
            this.lbl_pnlSearchSpaceTop = new System.Windows.Forms.Label();
            this.lbl_pnlSearchSpaceBottom = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceBottom = new System.Windows.Forms.Label();
            this.lbl_WhiteSpaceTop = new System.Windows.Forms.Label();
            this.btnSearchCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlListCommand = new System.Windows.Forms.Panel();
            this.btnShowAllCPT = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lbl_pnlSearchSpace = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_pnlSearchBottomBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchTopBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchleftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSearchRightBrd = new System.Windows.Forms.Label();
            this.chkShowActpatient = new System.Windows.Forms.CheckBox();
            this.pnlICD9_10 = new System.Windows.Forms.Panel();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbICD10 = new System.Windows.Forms.RadioButton();
            this.rbICD9 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pn_CustomList = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.oTimerSearch = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgListSubView)).BeginInit();
            this.pnlToolStrip.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListView)).BeginInit();
            this.pnlDataGridList.SuspendLayout();
            this.pnl_TOP.SuspendLayout();
            this.pnl_Base.SuspendLayout();
            this.pnl_ProcedureResource.SuspendLayout();
            this.pnlCommand.SuspendLayout();
            this.pnl_ProcedureResourceHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlExistsPredicate.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlListCommand.SuspendLayout();
            this.pnlICD9_10.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pn_CustomList.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgListSubView
            // 
            this.dgListSubView.AllowUserToAddRows = false;
            this.dgListSubView.AllowUserToDeleteRows = false;
            this.dgListSubView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgListSubView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgListSubView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgListSubView.BackgroundColor = System.Drawing.Color.White;
            this.dgListSubView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(126)))), ((int)(((byte)(211)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgListSubView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgListSubView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgListSubView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgListSubView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgListSubView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgListSubView.EnableHeadersVisualStyles = false;
            this.dgListSubView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dgListSubView.Location = new System.Drawing.Point(3, 1);
            this.dgListSubView.MultiSelect = false;
            this.dgListSubView.Name = "dgListSubView";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(96)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(170)))), ((int)(((byte)(207)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgListSubView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgListSubView.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgListSubView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgListSubView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgListSubView.Size = new System.Drawing.Size(651, 118);
            this.dgListSubView.TabIndex = 8;
            this.dgListSubView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListSubView_CellContentClick);
            // 
            // trvItems
            // 
            this.trvItems.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvItems.ForeColor = System.Drawing.Color.Black;
            this.trvItems.Location = new System.Drawing.Point(461, 63);
            this.trvItems.Name = "trvItems";
            this.trvItems.Size = new System.Drawing.Size(158, 205);
            this.trvItems.TabIndex = 0;
            this.trvItems.Visible = false;
            this.trvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvItems_AfterSelect);
            // 
            // pnlToolStrip
            // 
            this.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlToolStrip.Controls.Add(this.ts_Commands);
            this.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlToolStrip.Location = new System.Drawing.Point(0, 31);
            this.pnlToolStrip.Name = "pnlToolStrip";
            this.pnlToolStrip.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlToolStrip.Size = new System.Drawing.Size(657, 57);
            this.pnlToolStrip.TabIndex = 27;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.Transparent;
            this.ts_Commands.BackgroundImage = global::gloListControl.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_UserGroups,
            this.tsb_New,
            this.tsb_Modify,
            this.tsb_ShowAllRace,
            this.tsb_ShowFavorites,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(3, 1);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(651, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_UserGroups
            // 
            this.tsb_UserGroups.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_UserGroups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_UserGroups.Image = global::gloListControl.Properties.Resources.Group;
            this.tsb_UserGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UserGroups.Name = "tsb_UserGroups";
            this.tsb_UserGroups.Size = new System.Drawing.Size(54, 50);
            this.tsb_UserGroups.Tag = "Groups";
            this.tsb_UserGroups.Text = "Groups";
            this.tsb_UserGroups.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_UserGroups.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_UserGroups.Click += new System.EventHandler(this.tsb_UserGroups_Click);
            // 
            // tsb_New
            // 
            this.tsb_New.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_New.Image = ((System.Drawing.Image)(resources.GetObject("tsb_New.Image")));
            this.tsb_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_New.Name = "tsb_New";
            this.tsb_New.Size = new System.Drawing.Size(37, 50);
            this.tsb_New.Tag = "Add";
            this.tsb_New.Text = "&New";
            this.tsb_New.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_New.Visible = false;
            this.tsb_New.Click += new System.EventHandler(this.tsb_New_Click);
            // 
            // tsb_Modify
            // 
            this.tsb_Modify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Modify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Modify.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Modify.Image")));
            this.tsb_Modify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Modify.Name = "tsb_Modify";
            this.tsb_Modify.Size = new System.Drawing.Size(53, 50);
            this.tsb_Modify.Tag = "Add";
            this.tsb_Modify.Text = "&Modify";
            this.tsb_Modify.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Modify.Visible = false;
            this.tsb_Modify.Click += new System.EventHandler(this.tsb_Modify_Click);
            // 
            // tsb_ShowAllRace
            // 
            this.tsb_ShowAllRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_ShowAllRace.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowAllRace.Image")));
            this.tsb_ShowAllRace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowAllRace.Name = "tsb_ShowAllRace";
            this.tsb_ShowAllRace.Size = new System.Drawing.Size(65, 50);
            this.tsb_ShowAllRace.Tag = "ShowAllRace";
            this.tsb_ShowAllRace.Text = "Show &All";
            this.tsb_ShowAllRace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowAllRace.Visible = false;
            // 
            // tsb_ShowFavorites
            // 
            this.tsb_ShowFavorites.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_ShowFavorites.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowFavorites.Image")));
            this.tsb_ShowFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowFavorites.Name = "tsb_ShowFavorites";
            this.tsb_ShowFavorites.Size = new System.Drawing.Size(105, 50);
            this.tsb_ShowFavorites.Tag = "ShowFavorites";
            this.tsb_ShowFavorites.Text = "Show &Favorites";
            this.tsb_ShowFavorites.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowFavorites.Visible = false;
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "Save";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(43, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = "&Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // dgListView
            // 
            this.dgListView.AllowUserToAddRows = false;
            this.dgListView.AllowUserToDeleteRows = false;
            this.dgListView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgListView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgListView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgListView.BackgroundColor = System.Drawing.Color.White;
            this.dgListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgListView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(126)))), ((int)(((byte)(211)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgListView.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgListView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgListView.EnableHeadersVisualStyles = false;
            this.dgListView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dgListView.Location = new System.Drawing.Point(3, 1);
            this.dgListView.MultiSelect = false;
            this.dgListView.Name = "dgListView";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(96)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(170)))), ((int)(((byte)(207)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgListView.RowHeadersVisible = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.dgListView.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgListView.Size = new System.Drawing.Size(651, 269);
            this.dgListView.StandardTab = true;
            this.dgListView.TabIndex = 0;
            this.dgListView.Tag = "PatientList";
            this.dgListView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListView_CellClick);
            this.dgListView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListView_CellContentClick);
            this.dgListView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListView_CellDoubleClick);
            this.dgListView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgListView_CellFormatting);
            this.dgListView.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgListView_ColumnHeaderMouseDoubleClick);
            this.dgListView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListView_RowEnter);
            this.dgListView.SelectionChanged += new System.EventHandler(this.dgListView_SelectionChanged);
            this.dgListView.Sorted += new System.EventHandler(this.dgListView_Sorted);
            this.dgListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgListView_KeyDown);
            // 
            // pnlDataGridList
            // 
            this.pnlDataGridList.AutoScroll = true;
            this.pnlDataGridList.AutoSize = true;
            this.pnlDataGridList.Controls.Add(this.trvItems);
            this.pnlDataGridList.Controls.Add(this.lbl_pnlDataGridListTopBrd);
            this.pnlDataGridList.Controls.Add(this.lbl_pnlDataGridListBottomBrd);
            this.pnlDataGridList.Controls.Add(this.lbl_pnlDataGridListRightBrd);
            this.pnlDataGridList.Controls.Add(this.lbl_pnlDataGridListLeftBrd);
            this.pnlDataGridList.Controls.Add(this.dgListView);
            this.pnlDataGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDataGridList.Location = new System.Drawing.Point(0, 59);
            this.pnlDataGridList.Name = "pnlDataGridList";
            this.pnlDataGridList.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlDataGridList.Size = new System.Drawing.Size(657, 273);
            this.pnlDataGridList.TabIndex = 1;
            // 
            // lbl_pnlDataGridListTopBrd
            // 
            this.lbl_pnlDataGridListTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlDataGridListTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlDataGridListTopBrd.Location = new System.Drawing.Point(4, 1);
            this.lbl_pnlDataGridListTopBrd.Name = "lbl_pnlDataGridListTopBrd";
            this.lbl_pnlDataGridListTopBrd.Size = new System.Drawing.Size(649, 1);
            this.lbl_pnlDataGridListTopBrd.TabIndex = 16;
            // 
            // lbl_pnlDataGridListBottomBrd
            // 
            this.lbl_pnlDataGridListBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlDataGridListBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlDataGridListBottomBrd.Location = new System.Drawing.Point(4, 269);
            this.lbl_pnlDataGridListBottomBrd.Name = "lbl_pnlDataGridListBottomBrd";
            this.lbl_pnlDataGridListBottomBrd.Size = new System.Drawing.Size(649, 1);
            this.lbl_pnlDataGridListBottomBrd.TabIndex = 15;
            // 
            // lbl_pnlDataGridListRightBrd
            // 
            this.lbl_pnlDataGridListRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlDataGridListRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlDataGridListRightBrd.Location = new System.Drawing.Point(653, 1);
            this.lbl_pnlDataGridListRightBrd.Name = "lbl_pnlDataGridListRightBrd";
            this.lbl_pnlDataGridListRightBrd.Size = new System.Drawing.Size(1, 269);
            this.lbl_pnlDataGridListRightBrd.TabIndex = 14;
            // 
            // lbl_pnlDataGridListLeftBrd
            // 
            this.lbl_pnlDataGridListLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlDataGridListLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlDataGridListLeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_pnlDataGridListLeftBrd.Name = "lbl_pnlDataGridListLeftBrd";
            this.lbl_pnlDataGridListLeftBrd.Size = new System.Drawing.Size(1, 269);
            this.lbl_pnlDataGridListLeftBrd.TabIndex = 13;
            // 
            // pnl_TOP
            // 
            this.pnl_TOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_TOP.Controls.Add(this.pnlDataGridList);
            this.pnl_TOP.Controls.Add(this.pnl_Base);
            this.pnl_TOP.Controls.Add(this.pnlSearch);
            this.pnl_TOP.Controls.Add(this.pnlICD9_10);
            this.pnl_TOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_TOP.Location = new System.Drawing.Point(0, 88);
            this.pnl_TOP.Name = "pnl_TOP";
            this.pnl_TOP.Size = new System.Drawing.Size(657, 480);
            this.pnl_TOP.TabIndex = 0;
            // 
            // pnl_Base
            // 
            this.pnl_Base.Controls.Add(this.pnl_ProcedureResource);
            this.pnl_Base.Controls.Add(this.pnl_ProcedureResourceHeader);
            this.pnl_Base.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Base.Location = new System.Drawing.Point(0, 332);
            this.pnl_Base.Name = "pnl_Base";
            this.pnl_Base.Size = new System.Drawing.Size(657, 148);
            this.pnl_Base.TabIndex = 28;
            this.pnl_Base.Visible = false;
            // 
            // pnl_ProcedureResource
            // 
            this.pnl_ProcedureResource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_ProcedureResource.Controls.Add(this.lbl_pnl_ProcedureResourceRightBrd);
            this.pnl_ProcedureResource.Controls.Add(this.lbl_pnl_ProcedureResourceLeftBrd);
            this.pnl_ProcedureResource.Controls.Add(this.lbl_pnl_ProcedureResourceTopBrd);
            this.pnl_ProcedureResource.Controls.Add(this.lbl_pnl_ProcedureResourceBottomBrd);
            this.pnl_ProcedureResource.Controls.Add(this.dgListSubView);
            this.pnl_ProcedureResource.Controls.Add(this.pnlCommand);
            this.pnl_ProcedureResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ProcedureResource.Location = new System.Drawing.Point(0, 26);
            this.pnl_ProcedureResource.Name = "pnl_ProcedureResource";
            this.pnl_ProcedureResource.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnl_ProcedureResource.Size = new System.Drawing.Size(657, 122);
            this.pnl_ProcedureResource.TabIndex = 0;
            // 
            // lbl_pnl_ProcedureResourceRightBrd
            // 
            this.lbl_pnl_ProcedureResourceRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_ProcedureResourceRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnl_ProcedureResourceRightBrd.Location = new System.Drawing.Point(653, 2);
            this.lbl_pnl_ProcedureResourceRightBrd.Name = "lbl_pnl_ProcedureResourceRightBrd";
            this.lbl_pnl_ProcedureResourceRightBrd.Size = new System.Drawing.Size(1, 116);
            this.lbl_pnl_ProcedureResourceRightBrd.TabIndex = 19;
            // 
            // lbl_pnl_ProcedureResourceLeftBrd
            // 
            this.lbl_pnl_ProcedureResourceLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_ProcedureResourceLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnl_ProcedureResourceLeftBrd.Location = new System.Drawing.Point(3, 2);
            this.lbl_pnl_ProcedureResourceLeftBrd.Name = "lbl_pnl_ProcedureResourceLeftBrd";
            this.lbl_pnl_ProcedureResourceLeftBrd.Size = new System.Drawing.Size(1, 116);
            this.lbl_pnl_ProcedureResourceLeftBrd.TabIndex = 18;
            // 
            // lbl_pnl_ProcedureResourceTopBrd
            // 
            this.lbl_pnl_ProcedureResourceTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_ProcedureResourceTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnl_ProcedureResourceTopBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_pnl_ProcedureResourceTopBrd.Name = "lbl_pnl_ProcedureResourceTopBrd";
            this.lbl_pnl_ProcedureResourceTopBrd.Size = new System.Drawing.Size(651, 1);
            this.lbl_pnl_ProcedureResourceTopBrd.TabIndex = 17;
            // 
            // lbl_pnl_ProcedureResourceBottomBrd
            // 
            this.lbl_pnl_ProcedureResourceBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_ProcedureResourceBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnl_ProcedureResourceBottomBrd.Location = new System.Drawing.Point(3, 118);
            this.lbl_pnl_ProcedureResourceBottomBrd.Name = "lbl_pnl_ProcedureResourceBottomBrd";
            this.lbl_pnl_ProcedureResourceBottomBrd.Size = new System.Drawing.Size(651, 1);
            this.lbl_pnl_ProcedureResourceBottomBrd.TabIndex = 16;
            // 
            // pnlCommand
            // 
            this.pnlCommand.BackColor = System.Drawing.Color.Transparent;
            this.pnlCommand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCommand.BackgroundImage")));
            this.pnlCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlCommand.Controls.Add(this.btnOK);
            this.pnlCommand.Controls.Add(this.btnCancel);
            this.pnlCommand.Controls.Add(this.label8);
            this.pnlCommand.Location = new System.Drawing.Point(37, 32);
            this.pnlCommand.Name = "pnlCommand";
            this.pnlCommand.Size = new System.Drawing.Size(290, 27);
            this.pnlCommand.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnOK.Location = new System.Drawing.Point(138, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 27);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.MouseLeave += new System.EventHandler(this.btnOK_MouseLeave);
            this.btnOK.MouseHover += new System.EventHandler(this.btnOK_MouseHover);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnCancel.Location = new System.Drawing.Point(214, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            this.btnCancel.MouseHover += new System.EventHandler(this.btnCancel_MouseHover);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(5, 27);
            this.label8.TabIndex = 0;
            // 
            // pnl_ProcedureResourceHeader
            // 
            this.pnl_ProcedureResourceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_ProcedureResourceHeader.BackgroundImage = global::gloListControl.Properties.Resources.Img_Button;
            this.pnl_ProcedureResourceHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_ProcedureResourceHeader.Controls.Add(this.lbl_pnl_ProcedureResourceHeaderTopBrd);
            this.pnl_ProcedureResourceHeader.Controls.Add(this.lbl_pnl_ProcedureResourceHeaderBottomBrd);
            this.pnl_ProcedureResourceHeader.Controls.Add(this.lbl_pnl_ProcedureResourceHeaderRightBrd);
            this.pnl_ProcedureResourceHeader.Controls.Add(this.lbl_pnl_ProcedureResourceHeaderLeftBrd);
            this.pnl_ProcedureResourceHeader.Controls.Add(this.lblProcedureResourceHeader);
            this.pnl_ProcedureResourceHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ProcedureResourceHeader.Location = new System.Drawing.Point(0, 0);
            this.pnl_ProcedureResourceHeader.Name = "pnl_ProcedureResourceHeader";
            this.pnl_ProcedureResourceHeader.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnl_ProcedureResourceHeader.Size = new System.Drawing.Size(657, 26);
            this.pnl_ProcedureResourceHeader.TabIndex = 12;
            // 
            // lbl_pnl_ProcedureResourceHeaderTopBrd
            // 
            this.lbl_pnl_ProcedureResourceHeaderTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_ProcedureResourceHeaderTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnl_ProcedureResourceHeaderTopBrd.Location = new System.Drawing.Point(4, 1);
            this.lbl_pnl_ProcedureResourceHeaderTopBrd.Name = "lbl_pnl_ProcedureResourceHeaderTopBrd";
            this.lbl_pnl_ProcedureResourceHeaderTopBrd.Size = new System.Drawing.Size(649, 1);
            this.lbl_pnl_ProcedureResourceHeaderTopBrd.TabIndex = 17;
            // 
            // lbl_pnl_ProcedureResourceHeaderBottomBrd
            // 
            this.lbl_pnl_ProcedureResourceHeaderBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_ProcedureResourceHeaderBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnl_ProcedureResourceHeaderBottomBrd.Location = new System.Drawing.Point(4, 22);
            this.lbl_pnl_ProcedureResourceHeaderBottomBrd.Name = "lbl_pnl_ProcedureResourceHeaderBottomBrd";
            this.lbl_pnl_ProcedureResourceHeaderBottomBrd.Size = new System.Drawing.Size(649, 1);
            this.lbl_pnl_ProcedureResourceHeaderBottomBrd.TabIndex = 16;
            // 
            // lbl_pnl_ProcedureResourceHeaderRightBrd
            // 
            this.lbl_pnl_ProcedureResourceHeaderRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_ProcedureResourceHeaderRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnl_ProcedureResourceHeaderRightBrd.Location = new System.Drawing.Point(653, 1);
            this.lbl_pnl_ProcedureResourceHeaderRightBrd.Name = "lbl_pnl_ProcedureResourceHeaderRightBrd";
            this.lbl_pnl_ProcedureResourceHeaderRightBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnl_ProcedureResourceHeaderRightBrd.TabIndex = 15;
            // 
            // lbl_pnl_ProcedureResourceHeaderLeftBrd
            // 
            this.lbl_pnl_ProcedureResourceHeaderLeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnl_ProcedureResourceHeaderLeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnl_ProcedureResourceHeaderLeftBrd.Location = new System.Drawing.Point(3, 1);
            this.lbl_pnl_ProcedureResourceHeaderLeftBrd.Name = "lbl_pnl_ProcedureResourceHeaderLeftBrd";
            this.lbl_pnl_ProcedureResourceHeaderLeftBrd.Size = new System.Drawing.Size(1, 22);
            this.lbl_pnl_ProcedureResourceHeaderLeftBrd.TabIndex = 14;
            // 
            // lblProcedureResourceHeader
            // 
            this.lblProcedureResourceHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblProcedureResourceHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProcedureResourceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcedureResourceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProcedureResourceHeader.Location = new System.Drawing.Point(3, 1);
            this.lblProcedureResourceHeader.Name = "lblProcedureResourceHeader";
            this.lblProcedureResourceHeader.Size = new System.Drawing.Size(651, 22);
            this.lblProcedureResourceHeader.TabIndex = 0;
            this.lblProcedureResourceHeader.Text = "  Problem Type - Resources";
            this.lblProcedureResourceHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BackgroundImage = global::gloListControl.Properties.Resources.Img_Button;
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.chkIncludeDescription);
            this.pnlSearch.Controls.Add(this.chkIncludeAllUsers);
            this.pnlSearch.Controls.Add(this.pnlExistsPredicate);
            this.pnlSearch.Controls.Add(this.cmbSpeciality);
            this.pnlSearch.Controls.Add(this.lblSpeciality);
            this.pnlSearch.Controls.Add(this.chkSelectAll);
            this.pnlSearch.Controls.Add(this.cmbTypeOfservice);
            this.pnlSearch.Controls.Add(this.lblTOS);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchSpaceMiddle);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchSpaceTop);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchSpaceBottom);
            this.pnlSearch.Controls.Add(this.panel2);
            this.pnlSearch.Controls.Add(this.pnlListCommand);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchSpace);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchBottomBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchTopBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchleftBrd);
            this.pnlSearch.Controls.Add(this.lbl_pnlSearchRightBrd);
            this.pnlSearch.Controls.Add(this.chkShowActpatient);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 30);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlSearch.Size = new System.Drawing.Size(657, 29);
            this.pnlSearch.TabIndex = 0;
            // 
            // chkIncludeDescription
            // 
            this.chkIncludeDescription.AutoSize = true;
            this.chkIncludeDescription.Checked = true;
            this.chkIncludeDescription.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeDescription.Location = new System.Drawing.Point(311, 4);
            this.chkIncludeDescription.Name = "chkIncludeDescription";
            this.chkIncludeDescription.Size = new System.Drawing.Size(130, 18);
            this.chkIncludeDescription.TabIndex = 14;
            this.chkIncludeDescription.Text = "Include Description";
            this.chkIncludeDescription.UseVisualStyleBackColor = true;
            this.chkIncludeDescription.Visible = false;
            // 
            // chkIncludeAllUsers
            // 
            this.chkIncludeAllUsers.AutoSize = true;
            this.chkIncludeAllUsers.Location = new System.Drawing.Point(394, 4);
            this.chkIncludeAllUsers.Name = "chkIncludeAllUsers";
            this.chkIncludeAllUsers.Size = new System.Drawing.Size(115, 18);
            this.chkIncludeAllUsers.TabIndex = 20;
            this.chkIncludeAllUsers.Text = "Include All Users";
            this.chkIncludeAllUsers.UseVisualStyleBackColor = true;
            this.chkIncludeAllUsers.Visible = false;
            this.chkIncludeAllUsers.CheckedChanged += new System.EventHandler(this.chkIncludeAllUsers_CheckedChanged);
            // 
            // pnlExistsPredicate
            // 
            this.pnlExistsPredicate.BackColor = System.Drawing.Color.Transparent;
            this.pnlExistsPredicate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlExistsPredicate.Controls.Add(this.rd_ANDPredicate);
            this.pnlExistsPredicate.Controls.Add(this.rd_ORPredicate);
            this.pnlExistsPredicate.Controls.Add(this.label14);
            this.pnlExistsPredicate.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlExistsPredicate.Location = new System.Drawing.Point(652, 2);
            this.pnlExistsPredicate.Name = "pnlExistsPredicate";
            this.pnlExistsPredicate.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlExistsPredicate.Size = new System.Drawing.Size(174, 22);
            this.pnlExistsPredicate.TabIndex = 30;
            this.pnlExistsPredicate.Visible = false;
            // 
            // rd_ANDPredicate
            // 
            this.rd_ANDPredicate.AutoSize = true;
            this.rd_ANDPredicate.BackColor = System.Drawing.Color.Transparent;
            this.rd_ANDPredicate.Dock = System.Windows.Forms.DockStyle.Left;
            this.rd_ANDPredicate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_ANDPredicate.Location = new System.Drawing.Point(106, 0);
            this.rd_ANDPredicate.Name = "rd_ANDPredicate";
            this.rd_ANDPredicate.Size = new System.Drawing.Size(49, 19);
            this.rd_ANDPredicate.TabIndex = 20;
            this.rd_ANDPredicate.TabStop = true;
            this.rd_ANDPredicate.Text = "AND";
            this.rd_ANDPredicate.UseVisualStyleBackColor = false;
            // 
            // rd_ORPredicate
            // 
            this.rd_ORPredicate.AutoSize = true;
            this.rd_ORPredicate.BackColor = System.Drawing.Color.Transparent;
            this.rd_ORPredicate.Dock = System.Windows.Forms.DockStyle.Left;
            this.rd_ORPredicate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rd_ORPredicate.Location = new System.Drawing.Point(57, 0);
            this.rd_ORPredicate.Name = "rd_ORPredicate";
            this.rd_ORPredicate.Size = new System.Drawing.Size(49, 19);
            this.rd_ORPredicate.TabIndex = 19;
            this.rd_ORPredicate.TabStop = true;
            this.rd_ORPredicate.Text = "OR  ";
            this.rd_ORPredicate.UseVisualStyleBackColor = false;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 19);
            this.label14.TabIndex = 21;
            this.label14.Text = "Type :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSpeciality
            // 
            this.cmbSpeciality.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbSpeciality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpeciality.ForeColor = System.Drawing.Color.Black;
            this.cmbSpeciality.FormattingEnabled = true;
            this.cmbSpeciality.Location = new System.Drawing.Point(505, 2);
            this.cmbSpeciality.Name = "cmbSpeciality";
            this.cmbSpeciality.Size = new System.Drawing.Size(147, 22);
            this.cmbSpeciality.TabIndex = 17;
            this.cmbSpeciality.SelectedIndexChanged += new System.EventHandler(this.cmbSpeciality_SelectedIndexChanged);
            // 
            // lblSpeciality
            // 
            this.lblSpeciality.BackColor = System.Drawing.Color.Transparent;
            this.lblSpeciality.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSpeciality.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeciality.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSpeciality.Location = new System.Drawing.Point(391, 2);
            this.lblSpeciality.Name = "lblSpeciality";
            this.lblSpeciality.Size = new System.Drawing.Size(114, 22);
            this.lblSpeciality.TabIndex = 18;
            this.lblSpeciality.Text = "Pharmacy Type :";
            this.lblSpeciality.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkSelectAll.Location = new System.Drawing.Point(311, 2);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(80, 22);
            this.chkSelectAll.TabIndex = 14;
            this.chkSelectAll.Text = "Select All ";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // cmbTypeOfservice
            // 
            this.cmbTypeOfservice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeOfservice.ForeColor = System.Drawing.Color.Black;
            this.cmbTypeOfservice.FormattingEnabled = true;
            this.cmbTypeOfservice.Location = new System.Drawing.Point(274, 2);
            this.cmbTypeOfservice.Name = "cmbTypeOfservice";
            this.cmbTypeOfservice.Size = new System.Drawing.Size(136, 22);
            this.cmbTypeOfservice.TabIndex = 1;
            this.cmbTypeOfservice.Visible = false;
            this.cmbTypeOfservice.SelectedIndexChanged += new System.EventHandler(this.cmbTypeOfservice_SelectedIndexChanged);
            // 
            // lblTOS
            // 
            this.lblTOS.BackColor = System.Drawing.Color.Transparent;
            this.lblTOS.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTOS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTOS.Location = new System.Drawing.Point(262, 2);
            this.lblTOS.Name = "lblTOS";
            this.lblTOS.Size = new System.Drawing.Size(49, 22);
            this.lblTOS.TabIndex = 7;
            this.lblTOS.Text = "TOS :";
            this.lblTOS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTOS.Visible = false;
            // 
            // lbl_pnlSearchSpaceMiddle
            // 
            this.lbl_pnlSearchSpaceMiddle.BackColor = System.Drawing.Color.Transparent;
            this.lbl_pnlSearchSpaceMiddle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchSpaceMiddle.Location = new System.Drawing.Point(252, 2);
            this.lbl_pnlSearchSpaceMiddle.Name = "lbl_pnlSearchSpaceMiddle";
            this.lbl_pnlSearchSpaceMiddle.Size = new System.Drawing.Size(10, 22);
            this.lbl_pnlSearchSpaceMiddle.TabIndex = 10;
            // 
            // lbl_pnlSearchSpaceTop
            // 
            this.lbl_pnlSearchSpaceTop.BackColor = System.Drawing.Color.Transparent;
            this.lbl_pnlSearchSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchSpaceTop.Location = new System.Drawing.Point(252, 1);
            this.lbl_pnlSearchSpaceTop.Name = "lbl_pnlSearchSpaceTop";
            this.lbl_pnlSearchSpaceTop.Size = new System.Drawing.Size(401, 1);
            this.lbl_pnlSearchSpaceTop.TabIndex = 8;
            // 
            // lbl_pnlSearchSpaceBottom
            // 
            this.lbl_pnlSearchSpaceBottom.BackColor = System.Drawing.Color.Transparent;
            this.lbl_pnlSearchSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchSpaceBottom.Location = new System.Drawing.Point(252, 24);
            this.lbl_pnlSearchSpaceBottom.Name = "lbl_pnlSearchSpaceBottom";
            this.lbl_pnlSearchSpaceBottom.Size = new System.Drawing.Size(401, 1);
            this.lbl_pnlSearchSpaceBottom.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lbl_WhiteSpaceBottom);
            this.panel2.Controls.Add(this.lbl_WhiteSpaceTop);
            this.panel2.Controls.Add(this.btnSearchCancel);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(11, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(241, 24);
            this.panel2.TabIndex = 17;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(5, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(207, 15);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(1, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(4, 15);
            this.label2.TabIndex = 41;
            // 
            // lbl_WhiteSpaceBottom
            // 
            this.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_WhiteSpaceBottom.Location = new System.Drawing.Point(1, 18);
            this.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom";
            this.lbl_WhiteSpaceBottom.Size = new System.Drawing.Size(211, 6);
            this.lbl_WhiteSpaceBottom.TabIndex = 38;
            // 
            // lbl_WhiteSpaceTop
            // 
            this.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White;
            this.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_WhiteSpaceTop.Location = new System.Drawing.Point(1, 0);
            this.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop";
            this.lbl_WhiteSpaceTop.Size = new System.Drawing.Size(211, 3);
            this.lbl_WhiteSpaceTop.TabIndex = 37;
            // 
            // btnSearchCancel
            // 
            this.btnSearchCancel.BackColor = System.Drawing.Color.White;
            this.btnSearchCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearchCancel.FlatAppearance.BorderSize = 0;
            this.btnSearchCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearchCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearchCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchCancel.Image")));
            this.btnSearchCancel.Location = new System.Drawing.Point(212, 0);
            this.btnSearchCancel.Name = "btnSearchCancel";
            this.btnSearchCancel.Size = new System.Drawing.Size(28, 24);
            this.btnSearchCancel.TabIndex = 16;
            this.btnSearchCancel.UseVisualStyleBackColor = false;
            this.btnSearchCancel.Click += new System.EventHandler(this.btnSearchCancel_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 24);
            this.label4.TabIndex = 39;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(240, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 24);
            this.label5.TabIndex = 40;
            this.label5.Text = "label4";
            // 
            // pnlListCommand
            // 
            this.pnlListCommand.BackColor = System.Drawing.Color.Transparent;
            this.pnlListCommand.Controls.Add(this.btnShowAllCPT);
            this.pnlListCommand.Controls.Add(this.btnSelect);
            this.pnlListCommand.Controls.Add(this.btnClose);
            this.pnlListCommand.Controls.Add(this.btnRefresh);
            this.pnlListCommand.Location = new System.Drawing.Point(704, 2);
            this.pnlListCommand.Name = "pnlListCommand";
            this.pnlListCommand.Size = new System.Drawing.Size(122, 23);
            this.pnlListCommand.TabIndex = 4;
            this.pnlListCommand.Visible = false;
            // 
            // btnShowAllCPT
            // 
            this.btnShowAllCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowAllCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnShowAllCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnShowAllCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnShowAllCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAllCPT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAllCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnShowAllCPT.Location = new System.Drawing.Point(0, 0);
            this.btnShowAllCPT.Name = "btnShowAllCPT";
            this.btnShowAllCPT.Size = new System.Drawing.Size(36, 19);
            this.btnShowAllCPT.TabIndex = 2;
            this.btnShowAllCPT.Text = "Show All";
            this.btnShowAllCPT.UseVisualStyleBackColor = true;
            this.btnShowAllCPT.Visible = false;
            this.btnShowAllCPT.Click += new System.EventHandler(this.btnShowAllCPT_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelect.BackgroundImage")));
            this.btnSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(35, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(29, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::gloListControl.Properties.Resources.ImgButton;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(64, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(29, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = global::gloListControl.Properties.Resources.ImgButton;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(93, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(29, 23);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSearch.Location = new System.Drawing.Point(6, 1);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.lblSearch.Size = new System.Drawing.Size(5, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_pnlSearchSpace
            // 
            this.lbl_pnlSearchSpace.BackColor = System.Drawing.Color.Transparent;
            this.lbl_pnlSearchSpace.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchSpace.Location = new System.Drawing.Point(5, 1);
            this.lbl_pnlSearchSpace.Name = "lbl_pnlSearchSpace";
            this.lbl_pnlSearchSpace.Size = new System.Drawing.Size(1, 24);
            this.lbl_pnlSearchSpace.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(1, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(1, 19);
            this.label1.TabIndex = 15;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_pnlSearchBottomBrd
            // 
            this.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlSearchBottomBrd.Location = new System.Drawing.Point(4, 25);
            this.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd";
            this.lbl_pnlSearchBottomBrd.Size = new System.Drawing.Size(649, 1);
            this.lbl_pnlSearchBottomBrd.TabIndex = 5;
            // 
            // lbl_pnlSearchTopBrd
            // 
            this.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlSearchTopBrd.Location = new System.Drawing.Point(4, 0);
            this.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd";
            this.lbl_pnlSearchTopBrd.Size = new System.Drawing.Size(649, 1);
            this.lbl_pnlSearchTopBrd.TabIndex = 11;
            // 
            // lbl_pnlSearchleftBrd
            // 
            this.lbl_pnlSearchleftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchleftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlSearchleftBrd.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnlSearchleftBrd.Name = "lbl_pnlSearchleftBrd";
            this.lbl_pnlSearchleftBrd.Size = new System.Drawing.Size(1, 26);
            this.lbl_pnlSearchleftBrd.TabIndex = 12;
            // 
            // lbl_pnlSearchRightBrd
            // 
            this.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlSearchRightBrd.Location = new System.Drawing.Point(653, 0);
            this.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd";
            this.lbl_pnlSearchRightBrd.Size = new System.Drawing.Size(1, 26);
            this.lbl_pnlSearchRightBrd.TabIndex = 13;
            // 
            // chkShowActpatient
            // 
            this.chkShowActpatient.AutoSize = true;
            this.chkShowActpatient.Location = new System.Drawing.Point(323, 4);
            this.chkShowActpatient.Name = "chkShowActpatient";
            this.chkShowActpatient.Size = new System.Drawing.Size(163, 18);
            this.chkShowActpatient.TabIndex = 19;
            this.chkShowActpatient.Text = "Show account patient(s)";
            this.chkShowActpatient.UseVisualStyleBackColor = true;
            this.chkShowActpatient.Visible = false;
            this.chkShowActpatient.CheckedChanged += new System.EventHandler(this.chkShowActpatient_CheckedChanged);
            // 
            // pnlICD9_10
            // 
            this.pnlICD9_10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlICD9_10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlICD9_10.Controls.Add(this.rbAll);
            this.pnlICD9_10.Controls.Add(this.rbICD10);
            this.pnlICD9_10.Controls.Add(this.rbICD9);
            this.pnlICD9_10.Controls.Add(this.label6);
            this.pnlICD9_10.Controls.Add(this.label7);
            this.pnlICD9_10.Controls.Add(this.label9);
            this.pnlICD9_10.Controls.Add(this.label10);
            this.pnlICD9_10.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlICD9_10.Location = new System.Drawing.Point(0, 0);
            this.pnlICD9_10.Name = "pnlICD9_10";
            this.pnlICD9_10.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlICD9_10.Size = new System.Drawing.Size(657, 30);
            this.pnlICD9_10.TabIndex = 29;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAll.Location = new System.Drawing.Point(157, 5);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(37, 18);
            this.rbAll.TabIndex = 20;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.Visible = false;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbICD9_CheckedChanged);
            // 
            // rbICD10
            // 
            this.rbICD10.AutoSize = true;
            this.rbICD10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbICD10.Location = new System.Drawing.Point(87, 5);
            this.rbICD10.Name = "rbICD10";
            this.rbICD10.Size = new System.Drawing.Size(58, 18);
            this.rbICD10.TabIndex = 19;
            this.rbICD10.TabStop = true;
            this.rbICD10.Text = "ICD10";
            this.rbICD10.UseVisualStyleBackColor = true;
            this.rbICD10.CheckedChanged += new System.EventHandler(this.rbICD9_CheckedChanged);
            // 
            // rbICD9
            // 
            this.rbICD9.AutoSize = true;
            this.rbICD9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbICD9.Location = new System.Drawing.Point(24, 5);
            this.rbICD9.Name = "rbICD9";
            this.rbICD9.Size = new System.Drawing.Size(51, 18);
            this.rbICD9.TabIndex = 18;
            this.rbICD9.TabStop = true;
            this.rbICD9.Text = "ICD9";
            this.rbICD9.UseVisualStyleBackColor = true;
            this.rbICD9.CheckedChanged += new System.EventHandler(this.rbICD9_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(649, 1);
            this.label6.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(4, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(649, 1);
            this.label7.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Location = new System.Drawing.Point(653, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 27);
            this.label9.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 27);
            this.label10.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pn_CustomList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(657, 31);
            this.panel1.TabIndex = 28;
            // 
            // pn_CustomList
            // 
            this.pn_CustomList.BackColor = System.Drawing.Color.Transparent;
            this.pn_CustomList.BackgroundImage = global::gloListControl.Properties.Resources.Img_Blue2007;
            this.pn_CustomList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pn_CustomList.Controls.Add(this.lblHeader);
            this.pn_CustomList.Controls.Add(this.lbl_BottomBrd);
            this.pn_CustomList.Controls.Add(this.lbl_LeftBrd);
            this.pn_CustomList.Controls.Add(this.lbl_RightBrd);
            this.pn_CustomList.Controls.Add(this.lbl_TopBrd);
            this.pn_CustomList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_CustomList.ForeColor = System.Drawing.Color.White;
            this.pn_CustomList.Location = new System.Drawing.Point(3, 3);
            this.pn_CustomList.Name = "pn_CustomList";
            this.pn_CustomList.Size = new System.Drawing.Size(651, 25);
            this.pn_CustomList.TabIndex = 5;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(1, 1);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(649, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "-";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(1, 24);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(649, 1);
            this.lbl_BottomBrd.TabIndex = 8;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(0, 1);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_LeftBrd.TabIndex = 7;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(650, 1);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 24);
            this.lbl_RightBrd.TabIndex = 6;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(0, 0);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(651, 1);
            this.lbl_TopBrd.TabIndex = 5;
            this.lbl_TopBrd.Text = "label1";
            // 
            // oTimerSearch
            // 
            this.oTimerSearch.Tick += new System.EventHandler(this.oTimerSearch_Tick);
            // 
            // gloListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnl_TOP);
            this.Controls.Add(this.pnlToolStrip);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloListControl";
            this.Size = new System.Drawing.Size(657, 568);
            this.Load += new System.EventHandler(this.gloListControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gloListControl_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgListSubView)).EndInit();
            this.pnlToolStrip.ResumeLayout(false);
            this.pnlToolStrip.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListView)).EndInit();
            this.pnlDataGridList.ResumeLayout(false);
            this.pnl_TOP.ResumeLayout(false);
            this.pnl_TOP.PerformLayout();
            this.pnl_Base.ResumeLayout(false);
            this.pnl_ProcedureResource.ResumeLayout(false);
            this.pnlCommand.ResumeLayout(false);
            this.pnl_ProcedureResourceHeader.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlExistsPredicate.ResumeLayout(false);
            this.pnlExistsPredicate.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlListCommand.ResumeLayout(false);
            this.pnlICD9_10.ResumeLayout(false);
            this.pnlICD9_10.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pn_CustomList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCommand;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pn_CustomList;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.DataGridView dgListSubView;
        private System.Windows.Forms.TreeView trvItems;
        private System.Windows.Forms.Panel pnl_ProcedureResourceHeader;
        private System.Windows.Forms.Label lblProcedureResourceHeader;
        private System.Windows.Forms.Panel pnlToolStrip;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlListCommand;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lbl_pnlSearchSpace;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlDataGridList;
        private System.Windows.Forms.Panel pnl_TOP;
        private System.Windows.Forms.Panel pnl_ProcedureResource;
        private System.Windows.Forms.Label lbl_pnlSearchBottomBrd;
        private System.Windows.Forms.Button btnShowAllCPT;
        private System.Windows.Forms.ComboBox cmbTypeOfservice;
        private System.Windows.Forms.Label lblTOS;
        private System.Windows.Forms.Label lbl_pnlSearchSpaceMiddle;
        private System.Windows.Forms.Label lbl_pnlSearchSpaceBottom;
        private System.Windows.Forms.Label lbl_pnlSearchRightBrd;
        private System.Windows.Forms.Label lbl_pnlSearchTopBrd;
        private System.Windows.Forms.Label lbl_pnlSearchleftBrd;
        private System.Windows.Forms.Label lbl_pnlDataGridListTopBrd;
        private System.Windows.Forms.Label lbl_pnlDataGridListBottomBrd;
        private System.Windows.Forms.Label lbl_pnlDataGridListRightBrd;
        private System.Windows.Forms.Label lbl_pnlDataGridListLeftBrd;
        private System.Windows.Forms.Panel pnl_Base;
        private System.Windows.Forms.Label lbl_pnl_ProcedureResourceRightBrd;
        private System.Windows.Forms.Label lbl_pnl_ProcedureResourceLeftBrd;
        private System.Windows.Forms.Label lbl_pnl_ProcedureResourceTopBrd;
        private System.Windows.Forms.Label lbl_pnl_ProcedureResourceBottomBrd;
        private System.Windows.Forms.Label lbl_pnl_ProcedureResourceHeaderTopBrd;
        private System.Windows.Forms.Label lbl_pnl_ProcedureResourceHeaderBottomBrd;
        private System.Windows.Forms.Label lbl_pnl_ProcedureResourceHeaderRightBrd;
        private System.Windows.Forms.Label lbl_pnl_ProcedureResourceHeaderLeftBrd;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ToolStripButton tsb_UserGroups;
        public System.Windows.Forms.DataGridView dgListView;
        internal System.Windows.Forms.ToolStripButton tsb_New;
        internal System.Windows.Forms.ToolStripButton tsb_Modify;
        private System.Windows.Forms.Timer oTimerSearch;
        internal System.Windows.Forms.ToolStripButton tsb_ShowAllRace;
        internal System.Windows.Forms.ToolStripButton tsb_ShowFavorites;
        private System.Windows.Forms.Button btnSearchCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSpeciality;
        private System.Windows.Forms.Label lblSpeciality;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label lbl_WhiteSpaceBottom;
        internal System.Windows.Forms.Label lbl_WhiteSpaceTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlICD9_10;
        private System.Windows.Forms.RadioButton rbICD10;
        private System.Windows.Forms.RadioButton rbICD9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbAll;
        public System.Windows.Forms.CheckBox chkShowActpatient;
        private System.Windows.Forms.Label lbl_pnlSearchSpaceTop;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIncludeAllUsers;
        private System.Windows.Forms.CheckBox chkIncludeDescription;
        private System.Windows.Forms.Panel pnlExistsPredicate;
        private System.Windows.Forms.RadioButton rd_ANDPredicate;
        private System.Windows.Forms.RadioButton rd_ORPredicate;
        private System.Windows.Forms.Label label14;
    }
}
