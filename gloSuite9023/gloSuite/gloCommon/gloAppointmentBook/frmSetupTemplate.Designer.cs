namespace gloAppointmentBook
{
    partial class frmSetupTemplate
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpStartTime, dtpEndTime };
            System.Windows.Forms.Control[] cntControls = { dtpStartTime, dtpEndTime };
            if (disposing && (components != null))
            {

             

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    if (cmnuDelete != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnuDelete);
                        if (cmnuDelete.Items != null)
                        {
                            cmnuDelete.Items.Clear();

                        }
                        cmnuDelete.Dispose();
                        cmnuDelete = null;
                    }
                }
                catch
                {
                }
                components.Dispose();
                try
                {
                    if (dlgColor != null)
                    {

                        dlgColor.Dispose();
                        dlgColor = null;
                    }
                }
                catch
                {
                }
            }
            base.Dispose(disposing);


            
            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }
            
            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                }
            }

            System.Windows.Forms.ContextMenuStrip[] dtpControlsContextMenuStrip = { cmnuDelete };
           

            if (dtpControlsContextMenuStrip != null)
            {
                if (dtpControlsContextMenuStrip.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControlsContextMenuStrip);
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(ref dtpControlsContextMenuStrip);


                }
            }


        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupTemplate));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lbl_pnlMainBottomBdr = new System.Windows.Forms.Label();
            this.lbl_pnlMainLeftBdr = new System.Windows.Forms.Label();
            this.lbl_pnlMainRightBdr = new System.Windows.Forms.Label();
            this.lbl_pnlMainTopBdr = new System.Windows.Forms.Label();
            this.c1Template = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtTemplateName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl_pblSearchRightBdr = new System.Windows.Forms.Label();
            this.lbl_pblSearchBottomBdr = new System.Windows.Forms.Label();
            this.lbl_pblSearchTopBdr = new System.Windows.Forms.Label();
            this.lbl_pblSearchLeftBdr = new System.Windows.Forms.Label();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_Add = new System.Windows.Forms.ToolStripButton();
            this.tsb_Remove = new System.Windows.Forms.ToolStripButton();
            this.tsb_Update = new System.Windows.Forms.ToolStripButton();
            this.tsb_UpdateAdd = new System.Windows.Forms.ToolStripButton();
            this.tlsp_btnSave = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsb_Close = new System.Windows.Forms.ToolStripButton();
            this.pnlTemplateDetails = new System.Windows.Forms.Panel();
            this.pnlTemplateInfo = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblmin = new System.Windows.Forms.Label();
            this.numericdurationHrs = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numOccurrences = new System.Windows.Forms.NumericUpDown();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowseColor = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.cmbAppointmentType = new System.Windows.Forms.ComboBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblAppointmentType = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lbl_pnlTempateDetailsRightBdr = new System.Windows.Forms.Label();
            this.lbl_pnlTempateDetailsTopBdr = new System.Windows.Forms.Label();
            this.pnlTemplateDetailsHeader = new System.Windows.Forms.Panel();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.lblTemplateDetails = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.cmnuDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnu_deleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Template)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.ts_Commands.SuspendLayout();
            this.pnlTemplateDetails.SuspendLayout();
            this.pnlTemplateInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericdurationHrs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOccurrences)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.pnlTemplateDetailsHeader.SuspendLayout();
            this.Panel8.SuspendLayout();
            this.cmnuDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lbl_pnlMainBottomBdr);
            this.pnlMain.Controls.Add(this.lbl_pnlMainLeftBdr);
            this.pnlMain.Controls.Add(this.lbl_pnlMainRightBdr);
            this.pnlMain.Controls.Add(this.lbl_pnlMainTopBdr);
            this.pnlMain.Controls.Add(this.c1Template);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlMain.Location = new System.Drawing.Point(0, 217);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Size = new System.Drawing.Size(684, 338);
            this.pnlMain.TabIndex = 2;
            // 
            // lbl_pnlMainBottomBdr
            // 
            this.lbl_pnlMainBottomBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlMainBottomBdr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pnlMainBottomBdr.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlMainBottomBdr.Location = new System.Drawing.Point(4, 334);
            this.lbl_pnlMainBottomBdr.Name = "lbl_pnlMainBottomBdr";
            this.lbl_pnlMainBottomBdr.Size = new System.Drawing.Size(676, 1);
            this.lbl_pnlMainBottomBdr.TabIndex = 10;
            this.lbl_pnlMainBottomBdr.Text = "label2";
            // 
            // lbl_pnlMainLeftBdr
            // 
            this.lbl_pnlMainLeftBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlMainLeftBdr.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pnlMainLeftBdr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlMainLeftBdr.Location = new System.Drawing.Point(3, 1);
            this.lbl_pnlMainLeftBdr.Name = "lbl_pnlMainLeftBdr";
            this.lbl_pnlMainLeftBdr.Size = new System.Drawing.Size(1, 334);
            this.lbl_pnlMainLeftBdr.TabIndex = 0;
            this.lbl_pnlMainLeftBdr.Text = "label4";
            // 
            // lbl_pnlMainRightBdr
            // 
            this.lbl_pnlMainRightBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlMainRightBdr.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlMainRightBdr.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlMainRightBdr.Location = new System.Drawing.Point(680, 1);
            this.lbl_pnlMainRightBdr.Name = "lbl_pnlMainRightBdr";
            this.lbl_pnlMainRightBdr.Size = new System.Drawing.Size(1, 334);
            this.lbl_pnlMainRightBdr.TabIndex = 8;
            this.lbl_pnlMainRightBdr.Text = "label3";
            // 
            // lbl_pnlMainTopBdr
            // 
            this.lbl_pnlMainTopBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlMainTopBdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlMainTopBdr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlMainTopBdr.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnlMainTopBdr.Name = "lbl_pnlMainTopBdr";
            this.lbl_pnlMainTopBdr.Size = new System.Drawing.Size(678, 1);
            this.lbl_pnlMainTopBdr.TabIndex = 7;
            this.lbl_pnlMainTopBdr.Text = "label1";
            // 
            // c1Template
            // 
            this.c1Template.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.c1Template.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.ColumnsUniform;
            this.c1Template.AutoGenerateColumns = false;
            this.c1Template.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Template.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Template.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.c1Template.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Template.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Template.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Template.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Template.Location = new System.Drawing.Point(3, 0);
            this.c1Template.Name = "c1Template";
            this.c1Template.Rows.Count = 1;
            this.c1Template.Rows.DefaultSize = 21;
            this.c1Template.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1Template.Size = new System.Drawing.Size(678, 335);
            this.c1Template.StyleInfo = resources.GetString("c1Template.StyleInfo");
            this.c1Template.TabIndex = 9;
            this.c1Template.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1Template_CellButtonClick);
            this.c1Template.DoubleClick += new System.EventHandler(this.c1Template_DoubleClick);
            this.c1Template.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Template_MouseDown);
            this.c1Template.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1Template_MouseMove);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSearch.BackgroundImage")));
            this.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearch.Controls.Add(this.txtTemplateName);
            this.pnlSearch.Controls.Add(this.panel1);
            this.pnlSearch.Controls.Add(this.lbl_pblSearchRightBdr);
            this.pnlSearch.Controls.Add(this.lbl_pblSearchBottomBdr);
            this.pnlSearch.Controls.Add(this.lbl_pblSearchTopBdr);
            this.pnlSearch.Controls.Add(this.lbl_pblSearchLeftBdr);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 53);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlSearch.Size = new System.Drawing.Size(684, 27);
            this.pnlSearch.TabIndex = 0;
            // 
            // txtTemplateName
            // 
            this.txtTemplateName.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtTemplateName.ForeColor = System.Drawing.Color.Black;
            this.txtTemplateName.Location = new System.Drawing.Point(157, 4);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new System.Drawing.Size(524, 22);
            this.txtTemplateName.TabIndex = 0;
            this.txtTemplateName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTemplateName_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblSearch);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 22);
            this.panel1.TabIndex = 11;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblSearch.Location = new System.Drawing.Point(34, 3);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(108, 14);
            this.lblSearch.TabIndex = 29;
            this.lblSearch.Text = "Template Name :";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(23, 2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 14);
            this.label19.TabIndex = 110;
            this.label19.Text = "*";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_pblSearchRightBdr
            // 
            this.lbl_pblSearchRightBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pblSearchRightBdr.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pblSearchRightBdr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pblSearchRightBdr.Location = new System.Drawing.Point(680, 4);
            this.lbl_pblSearchRightBdr.Name = "lbl_pblSearchRightBdr";
            this.lbl_pblSearchRightBdr.Size = new System.Drawing.Size(1, 22);
            this.lbl_pblSearchRightBdr.TabIndex = 21;
            this.lbl_pblSearchRightBdr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_pblSearchBottomBdr
            // 
            this.lbl_pblSearchBottomBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pblSearchBottomBdr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_pblSearchBottomBdr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pblSearchBottomBdr.Location = new System.Drawing.Point(4, 26);
            this.lbl_pblSearchBottomBdr.Name = "lbl_pblSearchBottomBdr";
            this.lbl_pblSearchBottomBdr.Size = new System.Drawing.Size(677, 1);
            this.lbl_pblSearchBottomBdr.TabIndex = 18;
            this.lbl_pblSearchBottomBdr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_pblSearchTopBdr
            // 
            this.lbl_pblSearchTopBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pblSearchTopBdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pblSearchTopBdr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pblSearchTopBdr.Location = new System.Drawing.Point(4, 3);
            this.lbl_pblSearchTopBdr.Name = "lbl_pblSearchTopBdr";
            this.lbl_pblSearchTopBdr.Size = new System.Drawing.Size(677, 1);
            this.lbl_pblSearchTopBdr.TabIndex = 19;
            this.lbl_pblSearchTopBdr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_pblSearchLeftBdr
            // 
            this.lbl_pblSearchLeftBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pblSearchLeftBdr.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_pblSearchLeftBdr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pblSearchLeftBdr.Location = new System.Drawing.Point(3, 3);
            this.lbl_pblSearchLeftBdr.Name = "lbl_pblSearchLeftBdr";
            this.lbl_pblSearchLeftBdr.Size = new System.Drawing.Size(1, 24);
            this.lbl_pblSearchLeftBdr.TabIndex = 20;
            this.lbl_pblSearchLeftBdr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.ts_Commands);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(684, 53);
            this.pnlTop.TabIndex = 10;
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_Add,
            this.tsb_Remove,
            this.tsb_Update,
            this.tsb_UpdateAdd,
            this.tlsp_btnSave,
            this.tsb_SaveAs,
            this.tsb_Save,
            this.tsb_Cancel,
            this.tsb_Close});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(684, 53);
            this.ts_Commands.TabIndex = 0;
            this.ts_Commands.TabStop = true;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_Add
            // 
            this.tsb_Add.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Add.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Add.Image")));
            this.tsb_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Add.Name = "tsb_Add";
            this.tsb_Add.Size = new System.Drawing.Size(37, 50);
            this.tsb_Add.Tag = "Add";
            this.tsb_Add.Text = "&New";
            this.tsb_Add.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Remove
            // 
            this.tsb_Remove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Remove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Remove.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Remove.Image")));
            this.tsb_Remove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Remove.Name = "tsb_Remove";
            this.tsb_Remove.Size = new System.Drawing.Size(60, 50);
            this.tsb_Remove.Tag = "Remove";
            this.tsb_Remove.Text = "&Remove";
            this.tsb_Remove.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Remove.Visible = false;
            // 
            // tsb_Update
            // 
            this.tsb_Update.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Update.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Update.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Update.Image")));
            this.tsb_Update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Update.Name = "tsb_Update";
            this.tsb_Update.Size = new System.Drawing.Size(40, 50);
            this.tsb_Update.Tag = "Update";
            this.tsb_Update.Text = "Sa&ve";
            this.tsb_Update.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Update.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Update.ToolTipText = "Save";
            this.tsb_Update.Visible = false;
            // 
            // tsb_UpdateAdd
            // 
            this.tsb_UpdateAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_UpdateAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_UpdateAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsb_UpdateAdd.Image")));
            this.tsb_UpdateAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UpdateAdd.Name = "tsb_UpdateAdd";
            this.tsb_UpdateAdd.Size = new System.Drawing.Size(74, 50);
            this.tsb_UpdateAdd.Tag = "Update & Add";
            this.tsb_UpdateAdd.Text = "Save&&&Add";
            this.tsb_UpdateAdd.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_UpdateAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_UpdateAdd.ToolTipText = "Save and Add";
            this.tsb_UpdateAdd.Visible = false;
            // 
            // tlsp_btnSave
            // 
            this.tlsp_btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsp_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsp_btnSave.Image")));
            this.tlsp_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsp_btnSave.Name = "tlsp_btnSave";
            this.tlsp_btnSave.Size = new System.Drawing.Size(40, 50);
            this.tlsp_btnSave.Tag = "Save";
            this.tlsp_btnSave.Text = "&Save";
            this.tlsp_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsp_btnSave.ToolTipText = "Save";
            // 
            // tsb_SaveAs
            // 
            this.tsb_SaveAs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_SaveAs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_SaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveAs.Image")));
            this.tsb_SaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveAs.Name = "tsb_SaveAs";
            this.tsb_SaveAs.Size = new System.Drawing.Size(59, 50);
            this.tsb_SaveAs.Tag = "Save As";
            this.tsb_SaveAs.Text = "Sav&e As";
            this.tsb_SaveAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsb_Save
            // 
            this.tsb_Save.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(66, 50);
            this.tsb_Save.Tag = "Save_Close";
            this.tsb_Save.Text = "Sa&ve&&Cls";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.ToolTipText = "Save and Close";
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
            this.tsb_Cancel.Visible = false;
            // 
            // tsb_Close
            // 
            this.tsb_Close.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Close.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Close.Image")));
            this.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Close.Name = "tsb_Close";
            this.tsb_Close.Size = new System.Drawing.Size(43, 50);
            this.tsb_Close.Tag = "Close";
            this.tsb_Close.Text = "&Close";
            this.tsb_Close.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // pnlTemplateDetails
            // 
            this.pnlTemplateDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlTemplateDetails.Controls.Add(this.pnlTemplateInfo);
            this.pnlTemplateDetails.Controls.Add(this.lbl_pnlTempateDetailsRightBdr);
            this.pnlTemplateDetails.Controls.Add(this.lbl_pnlTempateDetailsTopBdr);
            this.pnlTemplateDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlTemplateDetails.Location = new System.Drawing.Point(0, 108);
            this.pnlTemplateDetails.Name = "pnlTemplateDetails";
            this.pnlTemplateDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlTemplateDetails.Size = new System.Drawing.Size(684, 109);
            this.pnlTemplateDetails.TabIndex = 1;
            // 
            // pnlTemplateInfo
            // 
            this.pnlTemplateInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlTemplateInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateInfo.Controls.Add(this.label3);
            this.pnlTemplateInfo.Controls.Add(this.label2);
            this.pnlTemplateInfo.Controls.Add(this.label1);
            this.pnlTemplateInfo.Controls.Add(this.lblmin);
            this.pnlTemplateInfo.Controls.Add(this.numericdurationHrs);
            this.pnlTemplateInfo.Controls.Add(this.label7);
            this.pnlTemplateInfo.Controls.Add(this.numOccurrences);
            this.pnlTemplateInfo.Controls.Add(this.cmbDepartment);
            this.pnlTemplateInfo.Controls.Add(this.label6);
            this.pnlTemplateInfo.Controls.Add(this.cmbLocation);
            this.pnlTemplateInfo.Controls.Add(this.btnRemove);
            this.pnlTemplateInfo.Controls.Add(this.btnSave);
            this.pnlTemplateInfo.Controls.Add(this.label5);
            this.pnlTemplateInfo.Controls.Add(this.btnBrowseColor);
            this.pnlTemplateInfo.Controls.Add(this.label10);
            this.pnlTemplateInfo.Controls.Add(this.numDuration);
            this.pnlTemplateInfo.Controls.Add(this.label9);
            this.pnlTemplateInfo.Controls.Add(this.txtColor);
            this.pnlTemplateInfo.Controls.Add(this.cmbAppointmentType);
            this.pnlTemplateInfo.Controls.Add(this.lblStart);
            this.pnlTemplateInfo.Controls.Add(this.dtpEndTime);
            this.pnlTemplateInfo.Controls.Add(this.lblAppointmentType);
            this.pnlTemplateInfo.Controls.Add(this.lblEnd);
            this.pnlTemplateInfo.Controls.Add(this.dtpStartTime);
            this.pnlTemplateInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTemplateInfo.Location = new System.Drawing.Point(3, 1);
            this.pnlTemplateInfo.Name = "pnlTemplateInfo";
            this.pnlTemplateInfo.Size = new System.Drawing.Size(677, 105);
            this.pnlTemplateInfo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 104);
            this.label3.TabIndex = 40;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(677, 1);
            this.label2.TabIndex = 39;
            this.label2.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(585, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 11);
            this.label1.TabIndex = 38;
            this.label1.Text = "hh";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblmin
            // 
            this.lblmin.AutoSize = true;
            this.lblmin.BackColor = System.Drawing.Color.Transparent;
            this.lblmin.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmin.Location = new System.Drawing.Point(632, 36);
            this.lblmin.Name = "lblmin";
            this.lblmin.Size = new System.Drawing.Size(21, 11);
            this.lblmin.TabIndex = 37;
            this.lblmin.Text = "mm";
            this.lblmin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericdurationHrs
            // 
            this.numericdurationHrs.ForeColor = System.Drawing.Color.Black;
            this.numericdurationHrs.Location = new System.Drawing.Point(571, 12);
            this.numericdurationHrs.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.numericdurationHrs.Name = "numericdurationHrs";
            this.numericdurationHrs.Size = new System.Drawing.Size(43, 22);
            this.numericdurationHrs.TabIndex = 6;
            this.numericdurationHrs.ValueChanged += new System.EventHandler(this.numericdurationHrs_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(485, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 14);
            this.label7.TabIndex = 28;
            this.label7.Text = "Occurrences :";
            // 
            // numOccurrences
            // 
            this.numOccurrences.ForeColor = System.Drawing.Color.Black;
            this.numOccurrences.Location = new System.Drawing.Point(571, 68);
            this.numOccurrences.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numOccurrences.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOccurrences.Name = "numOccurrences";
            this.numOccurrences.Size = new System.Drawing.Size(43, 22);
            this.numOccurrences.TabIndex = 8;
            this.numOccurrences.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDepartment.ForeColor = System.Drawing.Color.Black;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(132, 68);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(153, 22);
            this.cmbDepartment.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Location = new System.Drawing.Point(47, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 14);
            this.label6.TabIndex = 25;
            this.label6.Text = "Department :";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocation.ForeColor = System.Drawing.Color.Black;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(132, 40);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(153, 22);
            this.cmbLocation.TabIndex = 2;
            // 
            // btnRemove
            // 
            this.btnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove.BackgroundImage")));
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(650, 49);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(20, 24);
            this.btnRemove.TabIndex = 11;
            this.btnRemove.TabStop = false;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Visible = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnRemove.MouseLeave += new System.EventHandler(this.btnRemove_MouseLeave);
            this.btnRemove.MouseHover += new System.EventHandler(this.btnRemove_MouseHover);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(114)))), ((int)(((byte)(176)))));
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(85)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(125)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(625, 49);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(22, 24);
            this.btnSave.TabIndex = 10;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSave_MouseLeave);
            this.btnSave.MouseHover += new System.EventHandler(this.btnSave_MouseHover);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Location = new System.Drawing.Point(67, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 14);
            this.label5.TabIndex = 23;
            this.label5.Text = "Location :";
            // 
            // btnBrowseColor
            // 
            this.btnBrowseColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseColor.BackgroundImage")));
            this.btnBrowseColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnBrowseColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseColor.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseColor.Image")));
            this.btnBrowseColor.Location = new System.Drawing.Point(448, 69);
            this.btnBrowseColor.Name = "btnBrowseColor";
            this.btnBrowseColor.Size = new System.Drawing.Size(20, 20);
            this.btnBrowseColor.TabIndex = 9;
            this.btnBrowseColor.TabStop = false;
            this.btnBrowseColor.UseVisualStyleBackColor = true;
            this.btnBrowseColor.Visible = false;
            this.btnBrowseColor.Click += new System.EventHandler(this.btnBrowseColor_Click);
            this.btnBrowseColor.MouseLeave += new System.EventHandler(this.btnBrowseColor_MouseLeave);
            this.btnBrowseColor.MouseHover += new System.EventHandler(this.btnBrowseColor_MouseHover);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(507, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 14);
            this.label10.TabIndex = 19;
            this.label10.Text = "Duration :";
            // 
            // numDuration
            // 
            this.numDuration.ForeColor = System.Drawing.Color.Black;
            this.numDuration.Location = new System.Drawing.Point(621, 12);
            this.numDuration.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(43, 22);
            this.numDuration.TabIndex = 7;
            this.numDuration.ValueChanged += new System.EventHandler(this.numDuration_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Location = new System.Drawing.Point(331, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 14);
            this.label9.TabIndex = 17;
            this.label9.Text = "Color :";
            // 
            // txtColor
            // 
            this.txtColor.BackColor = System.Drawing.Color.White;
            this.txtColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColor.ForeColor = System.Drawing.Color.Black;
            this.txtColor.Location = new System.Drawing.Point(377, 68);
            this.txtColor.Name = "txtColor";
            this.txtColor.ReadOnly = true;
            this.txtColor.Size = new System.Drawing.Size(66, 22);
            this.txtColor.TabIndex = 8;
            this.txtColor.TabStop = false;
            // 
            // cmbAppointmentType
            // 
            this.cmbAppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppointmentType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAppointmentType.ForeColor = System.Drawing.Color.Black;
            this.cmbAppointmentType.FormattingEnabled = true;
            this.cmbAppointmentType.Location = new System.Drawing.Point(132, 12);
            this.cmbAppointmentType.Name = "cmbAppointmentType";
            this.cmbAppointmentType.Size = new System.Drawing.Size(153, 22);
            this.cmbAppointmentType.TabIndex = 1;
            this.cmbAppointmentType.SelectedIndexChanged += new System.EventHandler(this.cmbAppointmentType_SelectedIndexChanged);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.BackColor = System.Drawing.Color.Transparent;
            this.lblStart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblStart.Location = new System.Drawing.Point(300, 16);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(73, 14);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "Start Time :";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpEndTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpEndTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpEndTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpEndTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpEndTime.CustomFormat = "hh:mm tt";
            this.dtpEndTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(377, 40);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(101, 22);
            this.dtpEndTime.TabIndex = 5;
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // lblAppointmentType
            // 
            this.lblAppointmentType.AutoSize = true;
            this.lblAppointmentType.BackColor = System.Drawing.Color.Transparent;
            this.lblAppointmentType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAppointmentType.Location = new System.Drawing.Point(9, 16);
            this.lblAppointmentType.Name = "lblAppointmentType";
            this.lblAppointmentType.Size = new System.Drawing.Size(119, 14);
            this.lblAppointmentType.TabIndex = 1;
            this.lblAppointmentType.Text = "Appointment Type :";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.BackColor = System.Drawing.Color.Transparent;
            this.lblEnd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEnd.Location = new System.Drawing.Point(306, 44);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(67, 14);
            this.lblEnd.TabIndex = 1;
            this.lblEnd.Text = "End Time :";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpStartTime.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpStartTime.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpStartTime.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpStartTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpStartTime.CustomFormat = "hh:mm tt";
            this.dtpStartTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(377, 12);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(101, 22);
            this.dtpStartTime.TabIndex = 4;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // lbl_pnlTempateDetailsRightBdr
            // 
            this.lbl_pnlTempateDetailsRightBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTempateDetailsRightBdr.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_pnlTempateDetailsRightBdr.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_pnlTempateDetailsRightBdr.Location = new System.Drawing.Point(680, 1);
            this.lbl_pnlTempateDetailsRightBdr.Name = "lbl_pnlTempateDetailsRightBdr";
            this.lbl_pnlTempateDetailsRightBdr.Size = new System.Drawing.Size(1, 105);
            this.lbl_pnlTempateDetailsRightBdr.TabIndex = 7;
            this.lbl_pnlTempateDetailsRightBdr.Text = "label3";
            // 
            // lbl_pnlTempateDetailsTopBdr
            // 
            this.lbl_pnlTempateDetailsTopBdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_pnlTempateDetailsTopBdr.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_pnlTempateDetailsTopBdr.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pnlTempateDetailsTopBdr.Location = new System.Drawing.Point(3, 0);
            this.lbl_pnlTempateDetailsTopBdr.Name = "lbl_pnlTempateDetailsTopBdr";
            this.lbl_pnlTempateDetailsTopBdr.Size = new System.Drawing.Size(678, 1);
            this.lbl_pnlTempateDetailsTopBdr.TabIndex = 6;
            this.lbl_pnlTempateDetailsTopBdr.Text = "label1";
            // 
            // pnlTemplateDetailsHeader
            // 
            this.pnlTemplateDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTemplateDetailsHeader.Controls.Add(this.Panel8);
            this.pnlTemplateDetailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateDetailsHeader.Location = new System.Drawing.Point(0, 80);
            this.pnlTemplateDetailsHeader.Name = "pnlTemplateDetailsHeader";
            this.pnlTemplateDetailsHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTemplateDetailsHeader.Size = new System.Drawing.Size(684, 28);
            this.pnlTemplateDetailsHeader.TabIndex = 0;
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.Transparent;
            this.Panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel8.BackgroundImage")));
            this.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel8.Controls.Add(this.lblTemplateDetails);
            this.Panel8.Controls.Add(this.Label25);
            this.Panel8.Controls.Add(this.Label26);
            this.Panel8.Controls.Add(this.Label27);
            this.Panel8.Controls.Add(this.Label24);
            this.Panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel8.Location = new System.Drawing.Point(3, 3);
            this.Panel8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(678, 22);
            this.Panel8.TabIndex = 20;
            // 
            // lblTemplateDetails
            // 
            this.lblTemplateDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblTemplateDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTemplateDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblTemplateDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTemplateDetails.Location = new System.Drawing.Point(1, 1);
            this.lblTemplateDetails.Name = "lblTemplateDetails";
            this.lblTemplateDetails.Size = new System.Drawing.Size(143, 20);
            this.lblTemplateDetails.TabIndex = 1;
            this.lblTemplateDetails.Text = "  Template Details :";
            this.lblTemplateDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(0, 1);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(1, 20);
            this.Label25.TabIndex = 7;
            this.Label25.Text = "label4";
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label26.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label26.Location = new System.Drawing.Point(677, 1);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(1, 20);
            this.Label26.TabIndex = 6;
            this.Label26.Text = "label3";
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(0, 0);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(678, 1);
            this.Label27.TabIndex = 5;
            this.Label27.Text = "label1";
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label24.Location = new System.Drawing.Point(0, 21);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(678, 1);
            this.Label24.TabIndex = 8;
            this.Label24.Text = "label2";
            // 
            // cmnuDelete
            // 
            this.cmnuDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnu_deleteRow});
            this.cmnuDelete.Name = "cmnuDelete";
            this.cmnuDelete.Size = new System.Drawing.Size(122, 26);
            // 
            // cmnu_deleteRow
            // 
            this.cmnu_deleteRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_deleteRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnu_deleteRow.Image = ((System.Drawing.Image)(resources.GetObject("cmnu_deleteRow.Image")));
            this.cmnu_deleteRow.Name = "cmnu_deleteRow";
            this.cmnu_deleteRow.Size = new System.Drawing.Size(121, 22);
            this.cmnu_deleteRow.Text = "Delete";
            this.cmnu_deleteRow.Click += new System.EventHandler(this.cmnu_deleteRow_Click);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // frmSetupTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(220)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(684, 555);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTemplateDetails);
            this.Controls.Add(this.pnlTemplateDetailsHeader);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupTemplate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetupTemplate";
            this.Load += new System.EventHandler(this.frmSetupTemplate_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Template)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlTemplateDetails.ResumeLayout(false);
            this.pnlTemplateInfo.ResumeLayout(false);
            this.pnlTemplateInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericdurationHrs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOccurrences)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.pnlTemplateDetailsHeader.ResumeLayout(false);
            this.Panel8.ResumeLayout(false);
            this.cmnuDelete.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Template;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lbl_pblSearchBottomBdr;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlTemplateDetails;
        private System.Windows.Forms.Panel pnlTemplateInfo;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Panel pnlTemplateDetailsHeader;
        private System.Windows.Forms.Label lblTemplateDetails;
        private System.Windows.Forms.ComboBox cmbAppointmentType;
        private System.Windows.Forms.Label lblAppointmentType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Button btnBrowseColor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ContextMenuStrip cmnuDelete;
        private System.Windows.Forms.ToolStripMenuItem cmnu_deleteRow;
        private System.Windows.Forms.Label lbl_pblSearchTopBdr;
        private System.Windows.Forms.Label lbl_pblSearchRightBdr;
        private System.Windows.Forms.Label lbl_pblSearchLeftBdr;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numOccurrences;
        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_Save;
        internal System.Windows.Forms.ToolStripButton tsb_Close;
        internal System.Windows.Forms.ToolStripButton tsb_Add;
        internal System.Windows.Forms.ToolStripButton tsb_Remove;
        internal System.Windows.Forms.ToolStripButton tsb_Update;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.Label lblSearch;
        internal System.Windows.Forms.ToolStripButton tsb_UpdateAdd;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Label lbl_pnlMainBottomBdr;
        private System.Windows.Forms.Label lbl_pnlMainLeftBdr;
        private System.Windows.Forms.Label lbl_pnlMainRightBdr;
        private System.Windows.Forms.Label lbl_pnlMainTopBdr;
        private System.Windows.Forms.Label lbl_pnlTempateDetailsRightBdr;
        private System.Windows.Forms.Label lbl_pnlTempateDetailsTopBdr;
        internal System.Windows.Forms.ToolStripButton tsb_SaveAs;
        private System.Windows.Forms.NumericUpDown numericdurationHrs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblmin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.Panel Panel8;
        private System.Windows.Forms.Label Label24;
        private System.Windows.Forms.Label Label25;
        private System.Windows.Forms.Label Label26;
        private System.Windows.Forms.Label Label27;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        internal System.Windows.Forms.ToolStripButton tlsp_btnSave;
    }
}