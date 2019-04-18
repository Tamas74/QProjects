namespace gloCommunity.UserControls
{
    partial class UCLiquidData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLiquidData));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.pnlTableEntry = new System.Windows.Forms.Panel();
            this.pnldgTableField = new System.Windows.Forms.Panel();
            this.dgTableField = new System.Windows.Forms.DataGridView();
            this.Col_Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_HiddenCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sHiddenControlType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_AssociatedCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_AssociatedItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_HiddenAssociatedCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_AssociatedPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnTableUp = new System.Windows.Forms.Button();
            this.Label8 = new System.Windows.Forms.Label();
            this.Button4 = new System.Windows.Forms.Button();
            this.btnTableDown = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.Label40 = new System.Windows.Forms.Label();
            this.pnlAddCategory = new System.Windows.Forms.Panel();
            this.Label47 = new System.Windows.Forms.Label();
            this.Label46 = new System.Windows.Forms.Label();
            this.chkAssociateStd = new System.Windows.Forms.CheckBox();
            this.pnlStandardEM = new System.Windows.Forms.Panel();
            this.grbEM = new System.Windows.Forms.GroupBox();
            this.btnaddcategory = new System.Windows.Forms.Button();
            this.btnaddstandreddata = new System.Windows.Forms.Button();
            this.cmbAssociateSubItem = new System.Windows.Forms.ComboBox();
            this.cmbAssoicatedItem = new System.Windows.Forms.ComboBox();
            this.cmbAssociatedCategory = new System.Windows.Forms.ComboBox();
            this.lblAssociateSubItem = new System.Windows.Forms.Label();
            this.lblAssociatedCategory = new System.Windows.Forms.Label();
            this.lblAssoicatedItem = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Label48 = new System.Windows.Forms.Label();
            this.lblControl = new System.Windows.Forms.Label();
            this.CmbControl = new System.Windows.Forms.ComboBox();
            this.lblcaption = new System.Windows.Forms.Label();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCatModify = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.trvstd = new System.Windows.Forms.TreeView();
            this.txtcategory = new System.Windows.Forms.TextBox();
            this.btncatAdd = new System.Windows.Forms.Button();
            this.txtCatItem = new System.Windows.Forms.TextBox();
            this.lblitem = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.pnlFieldValues = new System.Windows.Forms.Panel();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.dgItemList = new System.Windows.Forms.DataGridView();
            this.Col_HiddenID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_CotrolType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_HiddenAssociatedItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label41 = new System.Windows.Forms.Label();
            this.Label42 = new System.Windows.Forms.Label();
            this.Label43 = new System.Windows.Forms.Label();
            this.Label44 = new System.Windows.Forms.Label();
            this.Panel8 = new System.Windows.Forms.Panel();
            this.btnItemUp = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnItemDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.Label37 = new System.Windows.Forms.Label();
            this.Label38 = new System.Windows.Forms.Label();
            this.Label39 = new System.Windows.Forms.Label();
            this.Label45 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Panel6 = new System.Windows.Forms.Panel();
            this.chkAssociatestddata = new System.Windows.Forms.CheckBox();
            this.pnlHPIExtended = new System.Windows.Forms.Panel();
            this.RdbtnExtended = new System.Windows.Forms.RadioButton();
            this.RdbtnBrief = new System.Windows.Forms.RadioButton();
            this.chckRequired = new System.Windows.Forms.CheckBox();
            this.pnlassociateStdItem = new System.Windows.Forms.Panel();
            this.btnaddassociated = new System.Windows.Forms.Button();
            this.btnaddfieldvalue = new System.Windows.Forms.Button();
            this.cmbstddata = new System.Windows.Forms.ComboBox();
            this.lblstdData = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblFieldcategory = new System.Windows.Forms.Label();
            this.cmbFieldCategory = new System.Windows.Forms.ComboBox();
            this.pnlBtns = new System.Windows.Forms.Panel();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtField = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.Label35 = new System.Windows.Forms.Label();
            this.Label36 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnltrvDiscrete = new System.Windows.Forms.Panel();
            this.trvDiscrete = new System.Windows.Forms.TreeView();
            this.Label33 = new System.Windows.Forms.Label();
            this.pnlLiquidDataDictionaryHeader = new System.Windows.Forms.Panel();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.lblLiquidDataDictionary = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.pnltls = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.tlsgloCommunity = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlbClinicRepository = new System.Windows.Forms.ToolStripButton();
            this.tlbGlobalRepository = new System.Windows.Forms.ToolStripButton();
            this.btn_Right1 = new System.Windows.Forms.Button();
            this.lbl_pnlSmallStripLeftBrd = new System.Windows.Forms.Label();
            this.lbl_pnlSmallStripTopBrd = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.pnlTableEntry.SuspendLayout();
            this.pnldgTableField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTableField)).BeginInit();
            this.Panel1.SuspendLayout();
            this.pnlAddCategory.SuspendLayout();
            this.pnlStandardEM.SuspendLayout();
            this.grbEM.SuspendLayout();
            this.Panel5.SuspendLayout();
            this.pnlFieldValues.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).BeginInit();
            this.Panel8.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel6.SuspendLayout();
            this.pnlHPIExtended.SuspendLayout();
            this.pnlassociateStdItem.SuspendLayout();
            this.pnlBtns.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnltrvDiscrete.SuspendLayout();
            this.pnlLiquidDataDictionaryHeader.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.pnltls.SuspendLayout();
            this.tlsgloCommunity.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlEdit);
            this.pnlMain.Controls.Add(this.Splitter1);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Controls.Add(this.pnltls);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1224, 798);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.Color.Transparent;
            this.pnlEdit.Controls.Add(this.pnlTableEntry);
            this.pnlEdit.Controls.Add(this.pnlFieldValues);
            this.pnlEdit.Controls.Add(this.Panel2);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEdit.Location = new System.Drawing.Point(290, 0);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(934, 798);
            this.pnlEdit.TabIndex = 12;
            // 
            // pnlTableEntry
            // 
            this.pnlTableEntry.Controls.Add(this.pnldgTableField);
            this.pnlTableEntry.Controls.Add(this.Panel1);
            this.pnlTableEntry.Controls.Add(this.pnlAddCategory);
            this.pnlTableEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTableEntry.Location = new System.Drawing.Point(0, 323);
            this.pnlTableEntry.Name = "pnlTableEntry";
            this.pnlTableEntry.Size = new System.Drawing.Size(934, 475);
            this.pnlTableEntry.TabIndex = 17;
            this.pnlTableEntry.Visible = false;
            // 
            // pnldgTableField
            // 
            this.pnldgTableField.Controls.Add(this.dgTableField);
            this.pnldgTableField.Controls.Add(this.Label23);
            this.pnldgTableField.Controls.Add(this.Label24);
            this.pnldgTableField.Controls.Add(this.Label25);
            this.pnldgTableField.Controls.Add(this.Label26);
            this.pnldgTableField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnldgTableField.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnldgTableField.Location = new System.Drawing.Point(0, 191);
            this.pnldgTableField.Name = "pnldgTableField";
            this.pnldgTableField.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.pnldgTableField.Size = new System.Drawing.Size(934, 284);
            this.pnldgTableField.TabIndex = 11;
            // 
            // dgTableField
            // 
            this.dgTableField.AllowUserToAddRows = false;
            this.dgTableField.AllowUserToDeleteRows = false;
            this.dgTableField.AllowUserToResizeColumns = false;
            this.dgTableField.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.dgTableField.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgTableField.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgTableField.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.dgTableField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(126)))), ((int)(((byte)(211)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTableField.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgTableField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTableField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Category,
            this.Col_Item,
            this.Col_HiddenCategory,
            this.sHiddenControlType,
            this.sColumnType,
            this.col_AssociatedCategory,
            this.col_AssociatedItem,
            this.Col_HiddenAssociatedCategory,
            this.Col_AssociatedPropertyName});
            this.dgTableField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTableField.EnableHeadersVisualStyles = false;
            this.dgTableField.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dgTableField.Location = new System.Drawing.Point(1, 4);
            this.dgTableField.MultiSelect = false;
            this.dgTableField.Name = "dgTableField";
            this.dgTableField.RowHeadersVisible = false;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.dgTableField.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgTableField.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTableField.Size = new System.Drawing.Size(932, 276);
            this.dgTableField.TabIndex = 0;
            // 
            // Col_Category
            // 
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(96)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.Col_Category.DefaultCellStyle = dataGridViewCellStyle13;
            this.Col_Category.HeaderText = "Category";
            this.Col_Category.Name = "Col_Category";
            this.Col_Category.ReadOnly = true;
            this.Col_Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_Item
            // 
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(96)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.Col_Item.DefaultCellStyle = dataGridViewCellStyle14;
            this.Col_Item.HeaderText = "Item";
            this.Col_Item.Name = "Col_Item";
            this.Col_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_HiddenCategory
            // 
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(96)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.Col_HiddenCategory.DefaultCellStyle = dataGridViewCellStyle15;
            this.Col_HiddenCategory.HeaderText = "Hiddent Category";
            this.Col_HiddenCategory.Name = "Col_HiddenCategory";
            this.Col_HiddenCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_HiddenCategory.Visible = false;
            // 
            // sHiddenControlType
            // 
            this.sHiddenControlType.HeaderText = "Hidden Column Type";
            this.sHiddenControlType.Name = "sHiddenControlType";
            this.sHiddenControlType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sHiddenControlType.Visible = false;
            // 
            // sColumnType
            // 
            this.sColumnType.HeaderText = "Control Type";
            this.sColumnType.Name = "sColumnType";
            this.sColumnType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_AssociatedCategory
            // 
            this.col_AssociatedCategory.HeaderText = "Associated Category";
            this.col_AssociatedCategory.Name = "col_AssociatedCategory";
            this.col_AssociatedCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_AssociatedCategory.Visible = false;
            // 
            // col_AssociatedItem
            // 
            this.col_AssociatedItem.HeaderText = "AssociatedItem";
            this.col_AssociatedItem.Name = "col_AssociatedItem";
            this.col_AssociatedItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_AssociatedItem.Visible = false;
            // 
            // Col_HiddenAssociatedCategory
            // 
            this.Col_HiddenAssociatedCategory.HeaderText = "Associated Hidden Category";
            this.Col_HiddenAssociatedCategory.Name = "Col_HiddenAssociatedCategory";
            this.Col_HiddenAssociatedCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_HiddenAssociatedCategory.Visible = false;
            // 
            // Col_AssociatedPropertyName
            // 
            this.Col_AssociatedPropertyName.HeaderText = "AssociatedPropertyName";
            this.Col_AssociatedPropertyName.Name = "Col_AssociatedPropertyName";
            this.Col_AssociatedPropertyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.Location = new System.Drawing.Point(1, 280);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(932, 1);
            this.Label23.TabIndex = 52;
            this.Label23.Text = "label4";
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label24.Location = new System.Drawing.Point(1, 3);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(932, 1);
            this.Label24.TabIndex = 51;
            this.Label24.Text = "label4";
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.Location = new System.Drawing.Point(933, 3);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(1, 278);
            this.Label25.TabIndex = 50;
            this.Label25.Text = "label4";
            // 
            // Label26
            // 
            this.Label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label26.Location = new System.Drawing.Point(0, 3);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(1, 278);
            this.Label26.TabIndex = 49;
            this.Label26.Text = "label4";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel1.BackgroundImage")));
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel1.Controls.Add(this.btnTableUp);
            this.Panel1.Controls.Add(this.Label8);
            this.Panel1.Controls.Add(this.Button4);
            this.Panel1.Controls.Add(this.btnTableDown);
            this.Panel1.Controls.Add(this.Button6);
            this.Panel1.Controls.Add(this.Label16);
            this.Panel1.Controls.Add(this.Label17);
            this.Panel1.Controls.Add(this.Label18);
            this.Panel1.Controls.Add(this.Label40);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel1.Location = new System.Drawing.Point(0, 166);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(934, 25);
            this.Panel1.TabIndex = 59;
            // 
            // btnTableUp
            // 
            this.btnTableUp.BackColor = System.Drawing.Color.Transparent;
            this.btnTableUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTableUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTableUp.Enabled = false;
            this.btnTableUp.FlatAppearance.BorderSize = 0;
            this.btnTableUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTableUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTableUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTableUp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTableUp.Image = ((System.Drawing.Image)(resources.GetObject("btnTableUp.Image")));
            this.btnTableUp.Location = new System.Drawing.Point(889, 1);
            this.btnTableUp.Name = "btnTableUp";
            this.btnTableUp.Size = new System.Drawing.Size(22, 23);
            this.btnTableUp.TabIndex = 10;
            this.btnTableUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTableUp.UseVisualStyleBackColor = false;
            // 
            // Label8
            // 
            this.Label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Location = new System.Drawing.Point(1, 1);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(67, 23);
            this.Label8.TabIndex = 0;
            this.Label8.Text = "Fields :";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button4
            // 
            this.Button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button4.BackgroundImage")));
            this.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button4.Enabled = false;
            this.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button4.Image = ((System.Drawing.Image)(resources.GetObject("Button4.Image")));
            this.Button4.Location = new System.Drawing.Point(803, 0);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(24, 24);
            this.Button4.TabIndex = 4;
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Visible = false;
            // 
            // btnTableDown
            // 
            this.btnTableDown.BackColor = System.Drawing.Color.Transparent;
            this.btnTableDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTableDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTableDown.Enabled = false;
            this.btnTableDown.FlatAppearance.BorderSize = 0;
            this.btnTableDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTableDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTableDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTableDown.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTableDown.Image = ((System.Drawing.Image)(resources.GetObject("btnTableDown.Image")));
            this.btnTableDown.Location = new System.Drawing.Point(911, 1);
            this.btnTableDown.Name = "btnTableDown";
            this.btnTableDown.Size = new System.Drawing.Size(22, 23);
            this.btnTableDown.TabIndex = 9;
            this.btnTableDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTableDown.UseVisualStyleBackColor = false;
            // 
            // Button6
            // 
            this.Button6.AutoSize = true;
            this.Button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button6.BackgroundImage")));
            this.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button6.Enabled = false;
            this.Button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.Button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button6.Image = ((System.Drawing.Image)(resources.GetObject("Button6.Image")));
            this.Button6.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Button6.Location = new System.Drawing.Point(833, 0);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(24, 24);
            this.Button6.TabIndex = 3;
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Visible = false;
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label16.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label16.Location = new System.Drawing.Point(1, 24);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(932, 1);
            this.Label16.TabIndex = 8;
            this.Label16.Text = "label2";
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.Location = new System.Drawing.Point(0, 1);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(1, 24);
            this.Label17.TabIndex = 7;
            this.Label17.Text = "label4";
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label18.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label18.Location = new System.Drawing.Point(933, 1);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(1, 24);
            this.Label18.TabIndex = 6;
            this.Label18.Text = "label3";
            // 
            // Label40
            // 
            this.Label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label40.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label40.Location = new System.Drawing.Point(0, 0);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(934, 1);
            this.Label40.TabIndex = 5;
            this.Label40.Text = "label1";
            // 
            // pnlAddCategory
            // 
            this.pnlAddCategory.Controls.Add(this.Label47);
            this.pnlAddCategory.Controls.Add(this.Label46);
            this.pnlAddCategory.Controls.Add(this.chkAssociateStd);
            this.pnlAddCategory.Controls.Add(this.pnlStandardEM);
            this.pnlAddCategory.Controls.Add(this.btnRefresh);
            this.pnlAddCategory.Controls.Add(this.Panel5);
            this.pnlAddCategory.Controls.Add(this.lblcaption);
            this.pnlAddCategory.Controls.Add(this.txtCaption);
            this.pnlAddCategory.Controls.Add(this.btnDelete);
            this.pnlAddCategory.Controls.Add(this.btnCatModify);
            this.pnlAddCategory.Controls.Add(this.lblCategory);
            this.pnlAddCategory.Controls.Add(this.trvstd);
            this.pnlAddCategory.Controls.Add(this.txtcategory);
            this.pnlAddCategory.Controls.Add(this.btncatAdd);
            this.pnlAddCategory.Controls.Add(this.txtCatItem);
            this.pnlAddCategory.Controls.Add(this.lblitem);
            this.pnlAddCategory.Controls.Add(this.Label27);
            this.pnlAddCategory.Controls.Add(this.Label28);
            this.pnlAddCategory.Controls.Add(this.Label29);
            this.pnlAddCategory.Controls.Add(this.Label30);
            this.pnlAddCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddCategory.Location = new System.Drawing.Point(0, 0);
            this.pnlAddCategory.Name = "pnlAddCategory";
            this.pnlAddCategory.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlAddCategory.Size = new System.Drawing.Size(934, 166);
            this.pnlAddCategory.TabIndex = 10;
            this.pnlAddCategory.Visible = false;
            // 
            // Label47
            // 
            this.Label47.AutoSize = true;
            this.Label47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label47.ForeColor = System.Drawing.Color.Red;
            this.Label47.Location = new System.Drawing.Point(76, 68);
            this.Label47.Name = "Label47";
            this.Label47.Size = new System.Drawing.Size(14, 14);
            this.Label47.TabIndex = 70;
            this.Label47.Text = "*";
            // 
            // Label46
            // 
            this.Label46.AutoSize = true;
            this.Label46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label46.ForeColor = System.Drawing.Color.Red;
            this.Label46.Location = new System.Drawing.Point(54, 39);
            this.Label46.Name = "Label46";
            this.Label46.Size = new System.Drawing.Size(14, 14);
            this.Label46.TabIndex = 69;
            this.Label46.Text = "*";
            // 
            // chkAssociateStd
            // 
            this.chkAssociateStd.AutoSize = true;
            this.chkAssociateStd.Location = new System.Drawing.Point(391, 7);
            this.chkAssociateStd.Name = "chkAssociateStd";
            this.chkAssociateStd.Size = new System.Drawing.Size(242, 18);
            this.chkAssociateStd.TabIndex = 12;
            this.chkAssociateStd.Text = "Associate standard Physical Examination";
            this.chkAssociateStd.UseVisualStyleBackColor = true;
            this.chkAssociateStd.Visible = false;
            // 
            // pnlStandardEM
            // 
            this.pnlStandardEM.Controls.Add(this.grbEM);
            this.pnlStandardEM.Location = new System.Drawing.Point(386, 25);
            this.pnlStandardEM.Name = "pnlStandardEM";
            this.pnlStandardEM.Size = new System.Drawing.Size(455, 96);
            this.pnlStandardEM.TabIndex = 59;
            this.pnlStandardEM.Visible = false;
            // 
            // grbEM
            // 
            this.grbEM.Controls.Add(this.btnaddcategory);
            this.grbEM.Controls.Add(this.btnaddstandreddata);
            this.grbEM.Controls.Add(this.cmbAssociateSubItem);
            this.grbEM.Controls.Add(this.cmbAssoicatedItem);
            this.grbEM.Controls.Add(this.cmbAssociatedCategory);
            this.grbEM.Controls.Add(this.lblAssociateSubItem);
            this.grbEM.Controls.Add(this.lblAssociatedCategory);
            this.grbEM.Controls.Add(this.lblAssoicatedItem);
            this.grbEM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbEM.Location = new System.Drawing.Point(0, 0);
            this.grbEM.Name = "grbEM";
            this.grbEM.Size = new System.Drawing.Size(455, 96);
            this.grbEM.TabIndex = 16;
            this.grbEM.TabStop = false;
            // 
            // btnaddcategory
            // 
            this.btnaddcategory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnaddcategory.BackgroundImage")));
            this.btnaddcategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddcategory.Enabled = false;
            this.btnaddcategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddcategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddcategory.Image = ((System.Drawing.Image)(resources.GetObject("btnaddcategory.Image")));
            this.btnaddcategory.Location = new System.Drawing.Point(391, 68);
            this.btnaddcategory.Name = "btnaddcategory";
            this.btnaddcategory.Size = new System.Drawing.Size(21, 21);
            this.btnaddcategory.TabIndex = 3;
            this.btnaddcategory.Text = "  &Add to category ";
            this.btnaddcategory.UseVisualStyleBackColor = true;
            // 
            // btnaddstandreddata
            // 
            this.btnaddstandreddata.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnaddstandreddata.BackgroundImage")));
            this.btnaddstandreddata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddstandreddata.Enabled = false;
            this.btnaddstandreddata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddstandreddata.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddstandreddata.Image = ((System.Drawing.Image)(resources.GetObject("btnaddstandreddata.Image")));
            this.btnaddstandreddata.Location = new System.Drawing.Point(415, 68);
            this.btnaddstandreddata.Name = "btnaddstandreddata";
            this.btnaddstandreddata.Size = new System.Drawing.Size(21, 21);
            this.btnaddstandreddata.TabIndex = 4;
            this.btnaddstandreddata.Text = "  &Insert Standard data";
            this.btnaddstandreddata.UseVisualStyleBackColor = true;
            // 
            // cmbAssociateSubItem
            // 
            this.cmbAssociateSubItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssociateSubItem.Enabled = false;
            this.cmbAssociateSubItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAssociateSubItem.ForeColor = System.Drawing.Color.Black;
            this.cmbAssociateSubItem.FormattingEnabled = true;
            this.cmbAssociateSubItem.Location = new System.Drawing.Point(144, 67);
            this.cmbAssociateSubItem.Name = "cmbAssociateSubItem";
            this.cmbAssociateSubItem.Size = new System.Drawing.Size(244, 22);
            this.cmbAssociateSubItem.TabIndex = 2;
            // 
            // cmbAssoicatedItem
            // 
            this.cmbAssoicatedItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssoicatedItem.Enabled = false;
            this.cmbAssoicatedItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAssoicatedItem.ForeColor = System.Drawing.Color.Black;
            this.cmbAssoicatedItem.FormattingEnabled = true;
            this.cmbAssoicatedItem.Location = new System.Drawing.Point(144, 40);
            this.cmbAssoicatedItem.Name = "cmbAssoicatedItem";
            this.cmbAssoicatedItem.Size = new System.Drawing.Size(244, 22);
            this.cmbAssoicatedItem.TabIndex = 1;
            // 
            // cmbAssociatedCategory
            // 
            this.cmbAssociatedCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssociatedCategory.Enabled = false;
            this.cmbAssociatedCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAssociatedCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbAssociatedCategory.FormattingEnabled = true;
            this.cmbAssociatedCategory.Location = new System.Drawing.Point(144, 14);
            this.cmbAssociatedCategory.Name = "cmbAssociatedCategory";
            this.cmbAssociatedCategory.Size = new System.Drawing.Size(244, 22);
            this.cmbAssociatedCategory.TabIndex = 0;
            // 
            // lblAssociateSubItem
            // 
            this.lblAssociateSubItem.AutoSize = true;
            this.lblAssociateSubItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAssociateSubItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssociateSubItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAssociateSubItem.Location = new System.Drawing.Point(15, 71);
            this.lblAssociateSubItem.Name = "lblAssociateSubItem";
            this.lblAssociateSubItem.Size = new System.Drawing.Size(126, 14);
            this.lblAssociateSubItem.TabIndex = 11;
            this.lblAssociateSubItem.Text = "Associated sub Item :";
            // 
            // lblAssociatedCategory
            // 
            this.lblAssociatedCategory.AutoSize = true;
            this.lblAssociatedCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAssociatedCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssociatedCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAssociatedCategory.Location = new System.Drawing.Point(15, 18);
            this.lblAssociatedCategory.Name = "lblAssociatedCategory";
            this.lblAssociatedCategory.Size = new System.Drawing.Size(126, 14);
            this.lblAssociatedCategory.TabIndex = 9;
            this.lblAssociatedCategory.Text = "Associated Category :";
            // 
            // lblAssoicatedItem
            // 
            this.lblAssoicatedItem.AutoSize = true;
            this.lblAssoicatedItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAssoicatedItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssoicatedItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblAssoicatedItem.Location = new System.Drawing.Point(38, 44);
            this.lblAssoicatedItem.Name = "lblAssoicatedItem";
            this.lblAssoicatedItem.Size = new System.Drawing.Size(103, 14);
            this.lblAssoicatedItem.TabIndex = 11;
            this.lblAssoicatedItem.Text = "Associated Item :";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Enabled = false;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(273, 125);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(69, 26);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // Panel5
            // 
            this.Panel5.Controls.Add(this.Label48);
            this.Panel5.Controls.Add(this.lblControl);
            this.Panel5.Controls.Add(this.CmbControl);
            this.Panel5.Location = new System.Drawing.Point(61, 92);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(299, 22);
            this.Panel5.TabIndex = 14;
            this.Panel5.Visible = false;
            // 
            // Label48
            // 
            this.Label48.AutoSize = true;
            this.Label48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label48.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label48.ForeColor = System.Drawing.Color.Red;
            this.Label48.Location = new System.Drawing.Point(1, 5);
            this.Label48.Name = "Label48";
            this.Label48.Size = new System.Drawing.Size(14, 14);
            this.Label48.TabIndex = 68;
            this.Label48.Text = "*";
            // 
            // lblControl
            // 
            this.lblControl.AutoSize = true;
            this.lblControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.lblControl.Location = new System.Drawing.Point(14, 4);
            this.lblControl.Name = "lblControl";
            this.lblControl.Size = new System.Drawing.Size(54, 14);
            this.lblControl.TabIndex = 52;
            this.lblControl.Text = "Control :";
            // 
            // CmbControl
            // 
            this.CmbControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbControl.Enabled = false;
            this.CmbControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbControl.ForeColor = System.Drawing.Color.Black;
            this.CmbControl.FormattingEnabled = true;
            this.CmbControl.Location = new System.Drawing.Point(70, 0);
            this.CmbControl.Name = "CmbControl";
            this.CmbControl.Size = new System.Drawing.Size(228, 22);
            this.CmbControl.TabIndex = 0;
            // 
            // lblcaption
            // 
            this.lblcaption.AutoSize = true;
            this.lblcaption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblcaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblcaption.Location = new System.Drawing.Point(73, 12);
            this.lblcaption.Name = "lblcaption";
            this.lblcaption.Size = new System.Drawing.Size(56, 14);
            this.lblcaption.TabIndex = 12;
            this.lblcaption.Text = "Caption :";
            // 
            // txtCaption
            // 
            this.txtCaption.Enabled = false;
            this.txtCaption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaption.Location = new System.Drawing.Point(132, 8);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(227, 22);
            this.txtCaption.TabIndex = 10;
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(199, 125);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 26);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnCatModify
            // 
            this.btnCatModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCatModify.BackgroundImage")));
            this.btnCatModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCatModify.Enabled = false;
            this.btnCatModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCatModify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCatModify.Location = new System.Drawing.Point(348, 125);
            this.btnCatModify.Name = "btnCatModify";
            this.btnCatModify.Size = new System.Drawing.Size(69, 26);
            this.btnCatModify.TabIndex = 21;
            this.btnCatModify.Text = "&Modify";
            this.btnCatModify.UseVisualStyleBackColor = true;
            this.btnCatModify.Visible = false;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblCategory.Location = new System.Drawing.Point(65, 40);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(64, 14);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "Category :";
            // 
            // trvstd
            // 
            this.trvstd.CheckBoxes = true;
            this.trvstd.Indent = 21;
            this.trvstd.Location = new System.Drawing.Point(882, 6);
            this.trvstd.Name = "trvstd";
            this.trvstd.ShowNodeToolTips = true;
            this.trvstd.ShowRootLines = false;
            this.trvstd.Size = new System.Drawing.Size(21, 27);
            this.trvstd.TabIndex = 0;
            this.trvstd.Visible = false;
            // 
            // txtcategory
            // 
            this.txtcategory.Enabled = false;
            this.txtcategory.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcategory.Location = new System.Drawing.Point(132, 36);
            this.txtcategory.Name = "txtcategory";
            this.txtcategory.Size = new System.Drawing.Size(227, 22);
            this.txtcategory.TabIndex = 11;
            // 
            // btncatAdd
            // 
            this.btncatAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btncatAdd.BackgroundImage")));
            this.btncatAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncatAdd.Enabled = false;
            this.btncatAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncatAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncatAdd.Location = new System.Drawing.Point(130, 125);
            this.btncatAdd.Name = "btncatAdd";
            this.btncatAdd.Size = new System.Drawing.Size(64, 26);
            this.btncatAdd.TabIndex = 17;
            this.btncatAdd.Text = "&Add";
            this.btncatAdd.UseVisualStyleBackColor = true;
            // 
            // txtCatItem
            // 
            this.txtCatItem.Enabled = false;
            this.txtCatItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCatItem.Location = new System.Drawing.Point(132, 64);
            this.txtCatItem.Name = "txtCatItem";
            this.txtCatItem.Size = new System.Drawing.Size(227, 22);
            this.txtCatItem.TabIndex = 13;
            // 
            // lblitem
            // 
            this.lblitem.AutoSize = true;
            this.lblitem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblitem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblitem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblitem.Location = new System.Drawing.Point(88, 68);
            this.lblitem.Name = "lblitem";
            this.lblitem.Size = new System.Drawing.Size(41, 14);
            this.lblitem.TabIndex = 7;
            this.lblitem.Text = "Item :";
            // 
            // Label27
            // 
            this.Label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label27.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label27.Location = new System.Drawing.Point(1, 162);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(932, 1);
            this.Label27.TabIndex = 52;
            this.Label27.Text = "label4";
            // 
            // Label28
            // 
            this.Label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label28.Location = new System.Drawing.Point(1, 0);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(932, 1);
            this.Label28.TabIndex = 51;
            this.Label28.Text = "label4";
            // 
            // Label29
            // 
            this.Label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(933, 0);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(1, 163);
            this.Label29.TabIndex = 50;
            this.Label29.Text = "label4";
            // 
            // Label30
            // 
            this.Label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label30.Location = new System.Drawing.Point(0, 0);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(1, 163);
            this.Label30.TabIndex = 49;
            this.Label30.Text = "label4";
            // 
            // pnlFieldValues
            // 
            this.pnlFieldValues.Controls.Add(this.Panel3);
            this.pnlFieldValues.Controls.Add(this.Panel8);
            this.pnlFieldValues.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFieldValues.Location = new System.Drawing.Point(0, 143);
            this.pnlFieldValues.Name = "pnlFieldValues";
            this.pnlFieldValues.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlFieldValues.Size = new System.Drawing.Size(934, 180);
            this.pnlFieldValues.TabIndex = 16;
            this.pnlFieldValues.Visible = false;
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.dgItemList);
            this.Panel3.Controls.Add(this.Label41);
            this.Panel3.Controls.Add(this.Label42);
            this.Panel3.Controls.Add(this.Label43);
            this.Panel3.Controls.Add(this.Label44);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel3.Location = new System.Drawing.Point(0, 25);
            this.Panel3.Name = "Panel3";
            this.Panel3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Panel3.Size = new System.Drawing.Size(934, 152);
            this.Panel3.TabIndex = 53;
            // 
            // dgItemList
            // 
            this.dgItemList.AllowUserToAddRows = false;
            this.dgItemList.AllowUserToDeleteRows = false;
            this.dgItemList.AllowUserToResizeColumns = false;
            this.dgItemList.AllowUserToResizeRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(231)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.dgItemList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dgItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgItemList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.dgItemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(126)))), ((int)(((byte)(211)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_HiddenID,
            this.sItem,
            this.Col_CotrolType,
            this.Col_HiddenAssociatedItem});
            this.dgItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItemList.EnableHeadersVisualStyles = false;
            this.dgItemList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(221)))));
            this.dgItemList.Location = new System.Drawing.Point(1, 4);
            this.dgItemList.MultiSelect = false;
            this.dgItemList.Name = "dgItemList";
            this.dgItemList.ReadOnly = true;
            this.dgItemList.RowHeadersVisible = false;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(207)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Black;
            this.dgItemList.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.dgItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItemList.Size = new System.Drawing.Size(932, 147);
            this.dgItemList.TabIndex = 0;
            // 
            // Col_HiddenID
            // 
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(96)))), ((int)(((byte)(162)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            this.Col_HiddenID.DefaultCellStyle = dataGridViewCellStyle19;
            this.Col_HiddenID.HeaderText = "HiddenID";
            this.Col_HiddenID.Name = "Col_HiddenID";
            this.Col_HiddenID.ReadOnly = true;
            this.Col_HiddenID.Visible = false;
            // 
            // sItem
            // 
            this.sItem.HeaderText = "Item";
            this.sItem.Name = "sItem";
            this.sItem.ReadOnly = true;
            this.sItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_CotrolType
            // 
            this.Col_CotrolType.HeaderText = "Control Type";
            this.Col_CotrolType.Name = "Col_CotrolType";
            this.Col_CotrolType.ReadOnly = true;
            this.Col_CotrolType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Col_HiddenAssociatedItem
            // 
            this.Col_HiddenAssociatedItem.HeaderText = "Associated Item";
            this.Col_HiddenAssociatedItem.Name = "Col_HiddenAssociatedItem";
            this.Col_HiddenAssociatedItem.ReadOnly = true;
            this.Col_HiddenAssociatedItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Col_HiddenAssociatedItem.Visible = false;
            // 
            // Label41
            // 
            this.Label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label41.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label41.Location = new System.Drawing.Point(0, 4);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(1, 147);
            this.Label41.TabIndex = 51;
            this.Label41.Text = "label4";
            // 
            // Label42
            // 
            this.Label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label42.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label42.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label42.Location = new System.Drawing.Point(933, 4);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(1, 147);
            this.Label42.TabIndex = 52;
            this.Label42.Text = "label4";
            // 
            // Label43
            // 
            this.Label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label43.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label43.Location = new System.Drawing.Point(0, 151);
            this.Label43.Name = "Label43";
            this.Label43.Size = new System.Drawing.Size(934, 1);
            this.Label43.TabIndex = 53;
            this.Label43.Text = "label4";
            // 
            // Label44
            // 
            this.Label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label44.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label44.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label44.Location = new System.Drawing.Point(0, 3);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(934, 1);
            this.Label44.TabIndex = 54;
            this.Label44.Text = "label4";
            // 
            // Panel8
            // 
            this.Panel8.BackColor = System.Drawing.Color.Transparent;
            this.Panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Panel8.BackgroundImage")));
            this.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel8.Controls.Add(this.btnItemUp);
            this.Panel8.Controls.Add(this.Label2);
            this.Panel8.Controls.Add(this.btnDown);
            this.Panel8.Controls.Add(this.btnItemDown);
            this.Panel8.Controls.Add(this.btnUp);
            this.Panel8.Controls.Add(this.Label37);
            this.Panel8.Controls.Add(this.Label38);
            this.Panel8.Controls.Add(this.Label39);
            this.Panel8.Controls.Add(this.Label45);
            this.Panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel8.Location = new System.Drawing.Point(0, 0);
            this.Panel8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel8.Name = "Panel8";
            this.Panel8.Size = new System.Drawing.Size(934, 25);
            this.Panel8.TabIndex = 0;
            // 
            // btnItemUp
            // 
            this.btnItemUp.BackColor = System.Drawing.Color.Transparent;
            this.btnItemUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnItemUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnItemUp.Enabled = false;
            this.btnItemUp.FlatAppearance.BorderSize = 0;
            this.btnItemUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnItemUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnItemUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemUp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemUp.Image = ((System.Drawing.Image)(resources.GetObject("btnItemUp.Image")));
            this.btnItemUp.Location = new System.Drawing.Point(889, 1);
            this.btnItemUp.Name = "btnItemUp";
            this.btnItemUp.Size = new System.Drawing.Size(22, 23);
            this.btnItemUp.TabIndex = 10;
            this.btnItemUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnItemUp.UseVisualStyleBackColor = false;
            // 
            // Label2
            // 
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Location = new System.Drawing.Point(1, 1);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(67, 23);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Fields :";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Enabled = false;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(803, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(24, 24);
            this.btnDown.TabIndex = 4;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Visible = false;
            // 
            // btnItemDown
            // 
            this.btnItemDown.BackColor = System.Drawing.Color.Transparent;
            this.btnItemDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnItemDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnItemDown.Enabled = false;
            this.btnItemDown.FlatAppearance.BorderSize = 0;
            this.btnItemDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnItemDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnItemDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemDown.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemDown.Image = ((System.Drawing.Image)(resources.GetObject("btnItemDown.Image")));
            this.btnItemDown.Location = new System.Drawing.Point(911, 1);
            this.btnItemDown.Name = "btnItemDown";
            this.btnItemDown.Size = new System.Drawing.Size(22, 23);
            this.btnItemDown.TabIndex = 9;
            this.btnItemDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnItemDown.UseVisualStyleBackColor = false;
            // 
            // btnUp
            // 
            this.btnUp.AutoSize = true;
            this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Enabled = false;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnUp.Location = new System.Drawing.Point(833, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(24, 24);
            this.btnUp.TabIndex = 3;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Visible = false;
            // 
            // Label37
            // 
            this.Label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label37.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label37.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label37.Location = new System.Drawing.Point(1, 24);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(932, 1);
            this.Label37.TabIndex = 8;
            this.Label37.Text = "label2";
            // 
            // Label38
            // 
            this.Label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label38.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label38.Location = new System.Drawing.Point(0, 1);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(1, 24);
            this.Label38.TabIndex = 7;
            this.Label38.Text = "label4";
            // 
            // Label39
            // 
            this.Label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label39.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label39.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label39.Location = new System.Drawing.Point(933, 1);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(1, 24);
            this.Label39.TabIndex = 6;
            this.Label39.Text = "label3";
            // 
            // Label45
            // 
            this.Label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label45.Location = new System.Drawing.Point(0, 0);
            this.Label45.Name = "Label45";
            this.Label45.Size = new System.Drawing.Size(934, 1);
            this.Label45.TabIndex = 5;
            this.Label45.Text = "label1";
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Panel6);
            this.Panel2.Controls.Add(this.chckRequired);
            this.Panel2.Controls.Add(this.pnlassociateStdItem);
            this.Panel2.Controls.Add(this.Label22);
            this.Panel2.Controls.Add(this.Label21);
            this.Panel2.Controls.Add(this.Label20);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Controls.Add(this.lblFieldcategory);
            this.Panel2.Controls.Add(this.cmbFieldCategory);
            this.Panel2.Controls.Add(this.pnlBtns);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Controls.Add(this.txtField);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.cmbDataType);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.txtItem);
            this.Panel2.Controls.Add(this.Label34);
            this.Panel2.Controls.Add(this.Label35);
            this.Panel2.Controls.Add(this.Label36);
            this.Panel2.Controls.Add(this.Label6);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Panel2.Size = new System.Drawing.Size(934, 143);
            this.Panel2.TabIndex = 0;
            // 
            // Panel6
            // 
            this.Panel6.Controls.Add(this.chkAssociatestddata);
            this.Panel6.Controls.Add(this.pnlHPIExtended);
            this.Panel6.Location = new System.Drawing.Point(365, 71);
            this.Panel6.Name = "Panel6";
            this.Panel6.Size = new System.Drawing.Size(437, 26);
            this.Panel6.TabIndex = 70;
            // 
            // chkAssociatestddata
            // 
            this.chkAssociatestddata.AutoSize = true;
            this.chkAssociatestddata.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkAssociatestddata.Enabled = false;
            this.chkAssociatestddata.Location = new System.Drawing.Point(178, 0);
            this.chkAssociatestddata.Name = "chkAssociatestddata";
            this.chkAssociatestddata.Size = new System.Drawing.Size(235, 26);
            this.chkAssociatestddata.TabIndex = 5;
            this.chkAssociatestddata.Text = "Associate standard Managment option";
            this.chkAssociatestddata.UseVisualStyleBackColor = true;
            this.chkAssociatestddata.Visible = false;
            // 
            // pnlHPIExtended
            // 
            this.pnlHPIExtended.Controls.Add(this.RdbtnExtended);
            this.pnlHPIExtended.Controls.Add(this.RdbtnBrief);
            this.pnlHPIExtended.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHPIExtended.Location = new System.Drawing.Point(0, 0);
            this.pnlHPIExtended.Name = "pnlHPIExtended";
            this.pnlHPIExtended.Size = new System.Drawing.Size(178, 26);
            this.pnlHPIExtended.TabIndex = 6;
            // 
            // RdbtnExtended
            // 
            this.RdbtnExtended.AutoSize = true;
            this.RdbtnExtended.Enabled = false;
            this.RdbtnExtended.Location = new System.Drawing.Point(80, 4);
            this.RdbtnExtended.Name = "RdbtnExtended";
            this.RdbtnExtended.Size = new System.Drawing.Size(78, 18);
            this.RdbtnExtended.TabIndex = 68;
            this.RdbtnExtended.TabStop = true;
            this.RdbtnExtended.Text = "Extended";
            this.RdbtnExtended.UseVisualStyleBackColor = true;
            // 
            // RdbtnBrief
            // 
            this.RdbtnBrief.AutoSize = true;
            this.RdbtnBrief.Enabled = false;
            this.RdbtnBrief.Location = new System.Drawing.Point(7, 4);
            this.RdbtnBrief.Name = "RdbtnBrief";
            this.RdbtnBrief.Size = new System.Drawing.Size(49, 18);
            this.RdbtnBrief.TabIndex = 68;
            this.RdbtnBrief.TabStop = true;
            this.RdbtnBrief.Text = "Brief";
            this.RdbtnBrief.UseVisualStyleBackColor = true;
            // 
            // chckRequired
            // 
            this.chckRequired.AutoSize = true;
            this.chckRequired.Enabled = false;
            this.chckRequired.Location = new System.Drawing.Point(376, 14);
            this.chckRequired.Name = "chckRequired";
            this.chckRequired.Size = new System.Drawing.Size(96, 18);
            this.chckRequired.TabIndex = 2;
            this.chckRequired.Text = "Is Mandatory";
            this.chckRequired.UseVisualStyleBackColor = true;
            // 
            // pnlassociateStdItem
            // 
            this.pnlassociateStdItem.Controls.Add(this.btnaddassociated);
            this.pnlassociateStdItem.Controls.Add(this.btnaddfieldvalue);
            this.pnlassociateStdItem.Controls.Add(this.cmbstddata);
            this.pnlassociateStdItem.Controls.Add(this.lblstdData);
            this.pnlassociateStdItem.Location = new System.Drawing.Point(367, 90);
            this.pnlassociateStdItem.Name = "pnlassociateStdItem";
            this.pnlassociateStdItem.Size = new System.Drawing.Size(531, 42);
            this.pnlassociateStdItem.TabIndex = 62;
            this.pnlassociateStdItem.Visible = false;
            // 
            // btnaddassociated
            // 
            this.btnaddassociated.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnaddassociated.BackgroundImage")));
            this.btnaddassociated.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddassociated.Enabled = false;
            this.btnaddassociated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddassociated.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddassociated.Image = ((System.Drawing.Image)(resources.GetObject("btnaddassociated.Image")));
            this.btnaddassociated.Location = new System.Drawing.Point(497, 10);
            this.btnaddassociated.Name = "btnaddassociated";
            this.btnaddassociated.Size = new System.Drawing.Size(22, 22);
            this.btnaddassociated.TabIndex = 9;
            this.btnaddassociated.Text = "  &Insert Associated Items";
            this.btnaddassociated.UseVisualStyleBackColor = true;
            // 
            // btnaddfieldvalue
            // 
            this.btnaddfieldvalue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnaddfieldvalue.BackgroundImage")));
            this.btnaddfieldvalue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddfieldvalue.Enabled = false;
            this.btnaddfieldvalue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddfieldvalue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddfieldvalue.Image = ((System.Drawing.Image)(resources.GetObject("btnaddfieldvalue.Image")));
            this.btnaddfieldvalue.Location = new System.Drawing.Point(470, 10);
            this.btnaddfieldvalue.Name = "btnaddfieldvalue";
            this.btnaddfieldvalue.Size = new System.Drawing.Size(22, 22);
            this.btnaddfieldvalue.TabIndex = 8;
            this.btnaddfieldvalue.Text = "  &Add to field";
            this.btnaddfieldvalue.UseVisualStyleBackColor = true;
            // 
            // cmbstddata
            // 
            this.cmbstddata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstddata.Enabled = false;
            this.cmbstddata.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbstddata.ForeColor = System.Drawing.Color.Black;
            this.cmbstddata.FormattingEnabled = true;
            this.cmbstddata.Location = new System.Drawing.Point(111, 10);
            this.cmbstddata.Name = "cmbstddata";
            this.cmbstddata.Size = new System.Drawing.Size(353, 22);
            this.cmbstddata.TabIndex = 7;
            // 
            // lblstdData
            // 
            this.lblstdData.AutoSize = true;
            this.lblstdData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblstdData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstdData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblstdData.Location = new System.Drawing.Point(7, 14);
            this.lblstdData.Name = "lblstdData";
            this.lblstdData.Size = new System.Drawing.Size(103, 14);
            this.lblstdData.TabIndex = 9;
            this.lblstdData.Text = "Associated Item :";
            // 
            // Label22
            // 
            this.Label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label22.Location = new System.Drawing.Point(1, 139);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(932, 1);
            this.Label22.TabIndex = 48;
            this.Label22.Text = "label4";
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label21.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label21.Location = new System.Drawing.Point(1, 0);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(932, 1);
            this.Label21.TabIndex = 47;
            this.Label21.Text = "label4";
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.Location = new System.Drawing.Point(933, 0);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(1, 140);
            this.Label20.TabIndex = 46;
            this.Label20.Text = "label4";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(0, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(1, 140);
            this.Label1.TabIndex = 45;
            this.Label1.Text = "label4";
            // 
            // lblFieldcategory
            // 
            this.lblFieldcategory.AutoSize = true;
            this.lblFieldcategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFieldcategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFieldcategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.lblFieldcategory.Location = new System.Drawing.Point(36, 78);
            this.lblFieldcategory.Name = "lblFieldcategory";
            this.lblFieldcategory.Size = new System.Drawing.Size(92, 14);
            this.lblFieldcategory.TabIndex = 9;
            this.lblFieldcategory.Text = "Field Category :";
            // 
            // cmbFieldCategory
            // 
            this.cmbFieldCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFieldCategory.Enabled = false;
            this.cmbFieldCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFieldCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbFieldCategory.FormattingEnabled = true;
            this.cmbFieldCategory.Location = new System.Drawing.Point(130, 74);
            this.cmbFieldCategory.Name = "cmbFieldCategory";
            this.cmbFieldCategory.Size = new System.Drawing.Size(229, 22);
            this.cmbFieldCategory.TabIndex = 4;
            this.cmbFieldCategory.SelectionChangeCommitted += new System.EventHandler(this.cmbFieldCategory_SelectionChangeCommitted);
            this.cmbFieldCategory.Click += new System.EventHandler(this.cmbFieldCategory_Click);
            // 
            // pnlBtns
            // 
            this.pnlBtns.Controls.Add(this.btn_Refresh);
            this.pnlBtns.Controls.Add(this.btnAdd);
            this.pnlBtns.Controls.Add(this.btnRemove);
            this.pnlBtns.Controls.Add(this.btnModify);
            this.pnlBtns.Location = new System.Drawing.Point(366, 35);
            this.pnlBtns.Name = "pnlBtns";
            this.pnlBtns.Size = new System.Drawing.Size(169, 33);
            this.pnlBtns.TabIndex = 7;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Refresh.BackgroundImage")));
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Refresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("btn_Refresh.Image")));
            this.btn_Refresh.Location = new System.Drawing.Point(61, 5);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(21, 21);
            this.btn_Refresh.TabIndex = 7;
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(11, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(21, 21);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove.BackgroundImage")));
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(36, 5);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(21, 21);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
            this.btnModify.Location = new System.Drawing.Point(138, 5);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(21, 21);
            this.btnModify.TabIndex = 6;
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Visible = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.Label4.Location = new System.Drawing.Point(25, 16);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(103, 14);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Field Description :";
            // 
            // txtField
            // 
            this.txtField.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtField.ForeColor = System.Drawing.Color.Black;
            this.txtField.Location = new System.Drawing.Point(130, 12);
            this.txtField.Name = "txtField";
            this.txtField.ReadOnly = true;
            this.txtField.Size = new System.Drawing.Size(229, 22);
            this.txtField.TabIndex = 1;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.Label5.Location = new System.Drawing.Point(57, 47);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(71, 14);
            this.Label5.TabIndex = 5;
            this.Label5.Text = "Field Type :";
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.Enabled = false;
            this.cmbDataType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDataType.ForeColor = System.Drawing.Color.Black;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(130, 43);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(229, 22);
            this.cmbDataType.TabIndex = 3;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.Label3.Location = new System.Drawing.Point(55, 109);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(73, 14);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Field Value :";
            // 
            // txtItem
            // 
            this.txtItem.Enabled = false;
            this.txtItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem.ForeColor = System.Drawing.Color.Black;
            this.txtItem.Location = new System.Drawing.Point(130, 105);
            this.txtItem.MaxLength = 100;
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(229, 22);
            this.txtItem.TabIndex = 6;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label34.ForeColor = System.Drawing.Color.Red;
            this.Label34.Location = new System.Drawing.Point(45, 47);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(14, 14);
            this.Label34.TabIndex = 65;
            this.Label34.Text = "*";
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label35.ForeColor = System.Drawing.Color.Red;
            this.Label35.Location = new System.Drawing.Point(24, 78);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(14, 14);
            this.Label35.TabIndex = 66;
            this.Label35.Text = "*";
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label36.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label36.ForeColor = System.Drawing.Color.Red;
            this.Label36.Location = new System.Drawing.Point(44, 109);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(14, 14);
            this.Label36.TabIndex = 67;
            this.Label36.Text = "*";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Red;
            this.Label6.Location = new System.Drawing.Point(13, 16);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(14, 14);
            this.Label6.TabIndex = 64;
            this.Label6.Text = "*";
            // 
            // Splitter1
            // 
            this.Splitter1.Location = new System.Drawing.Point(287, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 798);
            this.Splitter1.TabIndex = 15;
            this.Splitter1.TabStop = false;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.pnltrvDiscrete);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(28, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(259, 798);
            this.pnlLeft.TabIndex = 14;
            // 
            // pnltrvDiscrete
            // 
            this.pnltrvDiscrete.Controls.Add(this.trvDiscrete);
            this.pnltrvDiscrete.Controls.Add(this.Label33);
            this.pnltrvDiscrete.Controls.Add(this.pnlLiquidDataDictionaryHeader);
            this.pnltrvDiscrete.Controls.Add(this.Label15);
            this.pnltrvDiscrete.Controls.Add(this.Label14);
            this.pnltrvDiscrete.Controls.Add(this.Label12);
            this.pnltrvDiscrete.Controls.Add(this.Label13);
            this.pnltrvDiscrete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltrvDiscrete.Location = new System.Drawing.Point(0, 0);
            this.pnltrvDiscrete.Name = "pnltrvDiscrete";
            this.pnltrvDiscrete.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltrvDiscrete.Size = new System.Drawing.Size(259, 798);
            this.pnltrvDiscrete.TabIndex = 1;
            // 
            // trvDiscrete
            // 
            this.trvDiscrete.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvDiscrete.CheckBoxes = true;
            this.trvDiscrete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDiscrete.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvDiscrete.ForeColor = System.Drawing.Color.Black;
            this.trvDiscrete.ItemHeight = 21;
            this.trvDiscrete.Location = new System.Drawing.Point(4, 5);
            this.trvDiscrete.Name = "trvDiscrete";
            this.trvDiscrete.Size = new System.Drawing.Size(254, 789);
            this.trvDiscrete.TabIndex = 0;
            this.trvDiscrete.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvDiscrete_AfterCheck);
            this.trvDiscrete.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDiscrete_NodeMouseClick);
            this.trvDiscrete.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDiscrete_NodeMouseDoubleClick);
            // 
            // Label33
            // 
            this.Label33.BackColor = System.Drawing.Color.White;
            this.Label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label33.Location = new System.Drawing.Point(4, 1);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(254, 4);
            this.Label33.TabIndex = 48;
            // 
            // pnlLiquidDataDictionaryHeader
            // 
            this.pnlLiquidDataDictionaryHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLiquidDataDictionaryHeader.BackgroundImage")));
            this.pnlLiquidDataDictionaryHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLiquidDataDictionaryHeader.Controls.Add(this.Panel4);
            this.pnlLiquidDataDictionaryHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnlLiquidDataDictionaryHeader.Location = new System.Drawing.Point(82, 105);
            this.pnlLiquidDataDictionaryHeader.Name = "pnlLiquidDataDictionaryHeader";
            this.pnlLiquidDataDictionaryHeader.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnlLiquidDataDictionaryHeader.Size = new System.Drawing.Size(119, 28);
            this.pnlLiquidDataDictionaryHeader.TabIndex = 13;
            this.pnlLiquidDataDictionaryHeader.Visible = false;
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Transparent;
            this.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel4.Controls.Add(this.Label11);
            this.Panel4.Controls.Add(this.Label10);
            this.Panel4.Controls.Add(this.Label9);
            this.Panel4.Controls.Add(this.Label7);
            this.Panel4.Controls.Add(this.lblLiquidDataDictionary);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Panel4.Location = new System.Drawing.Point(3, 0);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(116, 25);
            this.Panel4.TabIndex = 8;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.Location = new System.Drawing.Point(1, 24);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(114, 1);
            this.Label11.TabIndex = 44;
            this.Label11.Text = "label4";
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.Location = new System.Drawing.Point(1, 0);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(114, 1);
            this.Label10.TabIndex = 43;
            this.Label10.Text = "label4";
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.Location = new System.Drawing.Point(115, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(1, 25);
            this.Label9.TabIndex = 42;
            this.Label9.Text = "label4";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(0, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 25);
            this.Label7.TabIndex = 41;
            this.Label7.Text = "label4";
            // 
            // lblLiquidDataDictionary
            // 
            this.lblLiquidDataDictionary.BackColor = System.Drawing.Color.Transparent;
            this.lblLiquidDataDictionary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLiquidDataDictionary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLiquidDataDictionary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLiquidDataDictionary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLiquidDataDictionary.Location = new System.Drawing.Point(0, 0);
            this.lblLiquidDataDictionary.Name = "lblLiquidDataDictionary";
            this.lblLiquidDataDictionary.Size = new System.Drawing.Size(116, 25);
            this.lblLiquidDataDictionary.TabIndex = 2;
            this.lblLiquidDataDictionary.Text = "  Liquid Data ";
            this.lblLiquidDataDictionary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(4, 794);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(254, 1);
            this.Label15.TabIndex = 47;
            this.Label15.Text = "label4";
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.Location = new System.Drawing.Point(258, 1);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(1, 794);
            this.Label14.TabIndex = 46;
            this.Label14.Text = "label4";
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.Location = new System.Drawing.Point(4, 0);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(255, 1);
            this.Label12.TabIndex = 45;
            this.Label12.Text = "label4";
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.Location = new System.Drawing.Point(3, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(1, 795);
            this.Label13.TabIndex = 44;
            this.Label13.Text = "label4";
            // 
            // pnltls
            // 
            this.pnltls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnltls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnltls.Controls.Add(this.label19);
            this.pnltls.Controls.Add(this.tlsgloCommunity);
            this.pnltls.Controls.Add(this.btn_Right1);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripLeftBrd);
            this.pnltls.Controls.Add(this.lbl_pnlSmallStripTopBrd);
            this.pnltls.Controls.Add(this.label53);
            this.pnltls.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnltls.Location = new System.Drawing.Point(0, 0);
            this.pnltls.Name = "pnltls";
            this.pnltls.Padding = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.pnltls.Size = new System.Drawing.Size(28, 798);
            this.pnltls.TabIndex = 100;
            this.pnltls.Visible = false;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Location = new System.Drawing.Point(4, 794);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 1);
            this.label19.TabIndex = 144;
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
            this.tlsgloCommunity.Size = new System.Drawing.Size(23, 772);
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
            this.lbl_pnlSmallStripLeftBrd.Size = new System.Drawing.Size(1, 794);
            this.lbl_pnlSmallStripLeftBrd.TabIndex = 9;
            this.lbl_pnlSmallStripLeftBrd.Click += new System.EventHandler(this.tlbGlobalRepository_Click);
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
            this.label53.Size = new System.Drawing.Size(1, 795);
            this.label53.TabIndex = 143;
            // 
            // UCLiquidData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "UCLiquidData";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Size = new System.Drawing.Size(1224, 801);
            this.Load += new System.EventHandler(this.UCLiquidData_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlEdit.ResumeLayout(false);
            this.pnlTableEntry.ResumeLayout(false);
            this.pnldgTableField.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTableField)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.pnlAddCategory.ResumeLayout(false);
            this.pnlAddCategory.PerformLayout();
            this.pnlStandardEM.ResumeLayout(false);
            this.grbEM.ResumeLayout(false);
            this.grbEM.PerformLayout();
            this.Panel5.ResumeLayout(false);
            this.Panel5.PerformLayout();
            this.pnlFieldValues.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItemList)).EndInit();
            this.Panel8.ResumeLayout(false);
            this.Panel8.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel6.ResumeLayout(false);
            this.Panel6.PerformLayout();
            this.pnlHPIExtended.ResumeLayout(false);
            this.pnlHPIExtended.PerformLayout();
            this.pnlassociateStdItem.ResumeLayout(false);
            this.pnlassociateStdItem.PerformLayout();
            this.pnlBtns.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnltrvDiscrete.ResumeLayout(false);
            this.pnlLiquidDataDictionaryHeader.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.pnltls.ResumeLayout(false);
            this.pnltls.PerformLayout();
            this.tlsgloCommunity.ResumeLayout(false);
            this.tlsgloCommunity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlMain;
        internal System.Windows.Forms.Panel pnlEdit;
        internal System.Windows.Forms.Panel pnlTableEntry;
        internal System.Windows.Forms.Panel pnldgTableField;
        internal System.Windows.Forms.DataGridView dgTableField;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_Category;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_Item;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_HiddenCategory;
        internal System.Windows.Forms.DataGridViewTextBoxColumn sHiddenControlType;
        internal System.Windows.Forms.DataGridViewTextBoxColumn sColumnType;
        internal System.Windows.Forms.DataGridViewTextBoxColumn col_AssociatedCategory;
        internal System.Windows.Forms.DataGridViewTextBoxColumn col_AssociatedItem;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_HiddenAssociatedCategory;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_AssociatedPropertyName;
        private System.Windows.Forms.Label Label23;
        private System.Windows.Forms.Label Label24;
        private System.Windows.Forms.Label Label25;
        private System.Windows.Forms.Label Label26;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button btnTableUp;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Button Button4;
        internal System.Windows.Forms.Button btnTableDown;
        internal System.Windows.Forms.Button Button6;
        private System.Windows.Forms.Label Label16;
        private System.Windows.Forms.Label Label17;
        private System.Windows.Forms.Label Label18;
        private System.Windows.Forms.Label Label40;
        internal System.Windows.Forms.Panel pnlAddCategory;
        internal System.Windows.Forms.Label Label47;
        internal System.Windows.Forms.Label Label46;
        internal System.Windows.Forms.CheckBox chkAssociateStd;
        internal System.Windows.Forms.Panel pnlStandardEM;
        internal System.Windows.Forms.GroupBox grbEM;
        internal System.Windows.Forms.Button btnaddcategory;
        internal System.Windows.Forms.Button btnaddstandreddata;
        internal System.Windows.Forms.ComboBox cmbAssociateSubItem;
        internal System.Windows.Forms.ComboBox cmbAssoicatedItem;
        internal System.Windows.Forms.ComboBox cmbAssociatedCategory;
        internal System.Windows.Forms.Label lblAssociateSubItem;
        internal System.Windows.Forms.Label lblAssociatedCategory;
        internal System.Windows.Forms.Label lblAssoicatedItem;
        internal System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.Panel Panel5;
        internal System.Windows.Forms.Label Label48;
        internal System.Windows.Forms.Label lblControl;
        internal System.Windows.Forms.ComboBox CmbControl;
        internal System.Windows.Forms.Label lblcaption;
        internal System.Windows.Forms.TextBox txtCaption;
        internal System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.Button btnCatModify;
        internal System.Windows.Forms.Label lblCategory;
        internal System.Windows.Forms.TreeView trvstd;
        internal System.Windows.Forms.TextBox txtcategory;
        internal System.Windows.Forms.Button btncatAdd;
        internal System.Windows.Forms.TextBox txtCatItem;
        internal System.Windows.Forms.Label lblitem;
        private System.Windows.Forms.Label Label27;
        private System.Windows.Forms.Label Label28;
        private System.Windows.Forms.Label Label29;
        private System.Windows.Forms.Label Label30;
        internal System.Windows.Forms.Panel pnlFieldValues;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.DataGridView dgItemList;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_HiddenID;
        internal System.Windows.Forms.DataGridViewTextBoxColumn sItem;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_CotrolType;
        internal System.Windows.Forms.DataGridViewTextBoxColumn Col_HiddenAssociatedItem;
        private System.Windows.Forms.Label Label41;
        private System.Windows.Forms.Label Label42;
        private System.Windows.Forms.Label Label43;
        private System.Windows.Forms.Label Label44;
        internal System.Windows.Forms.Panel Panel8;
        internal System.Windows.Forms.Button btnItemUp;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnDown;
        internal System.Windows.Forms.Button btnItemDown;
        internal System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label Label37;
        private System.Windows.Forms.Label Label38;
        private System.Windows.Forms.Label Label39;
        private System.Windows.Forms.Label Label45;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Panel Panel6;
        internal System.Windows.Forms.CheckBox chkAssociatestddata;
        internal System.Windows.Forms.Panel pnlHPIExtended;
        internal System.Windows.Forms.RadioButton RdbtnExtended;
        internal System.Windows.Forms.RadioButton RdbtnBrief;
        internal System.Windows.Forms.CheckBox chckRequired;
        internal System.Windows.Forms.Panel pnlassociateStdItem;
        internal System.Windows.Forms.Button btnaddassociated;
        internal System.Windows.Forms.Button btnaddfieldvalue;
        internal System.Windows.Forms.ComboBox cmbstddata;
        internal System.Windows.Forms.Label lblstdData;
        private System.Windows.Forms.Label Label22;
        private System.Windows.Forms.Label Label21;
        private System.Windows.Forms.Label Label20;
        private System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label lblFieldcategory;
        internal System.Windows.Forms.ComboBox cmbFieldCategory;
        internal System.Windows.Forms.Panel pnlBtns;
        internal System.Windows.Forms.Button btn_Refresh;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Button btnRemove;
        internal System.Windows.Forms.Button btnModify;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtField;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cmbDataType;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtItem;
        internal System.Windows.Forms.Label Label34;
        internal System.Windows.Forms.Label Label35;
        internal System.Windows.Forms.Label Label36;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Splitter Splitter1;
        internal System.Windows.Forms.Panel pnlLeft;
        internal System.Windows.Forms.Panel pnltrvDiscrete;
        internal System.Windows.Forms.TreeView trvDiscrete;
        private System.Windows.Forms.Label Label33;
        private System.Windows.Forms.Label Label15;
        private System.Windows.Forms.Label Label14;
        private System.Windows.Forms.Label Label12;
        private System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Panel pnlLiquidDataDictionaryHeader;
        internal System.Windows.Forms.Panel Panel4;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label lblLiquidDataDictionary;
        private System.Windows.Forms.Label label19;
        private gloGlobal.gloToolStripIgnoreFocus tlsgloCommunity;
        private System.Windows.Forms.ToolStripButton tlbClinicRepository;
        private System.Windows.Forms.ToolStripButton tlbGlobalRepository;
        private System.Windows.Forms.Button btn_Right1;
        private System.Windows.Forms.Label lbl_pnlSmallStripLeftBrd;
        private System.Windows.Forms.Label lbl_pnlSmallStripTopBrd;
        private System.Windows.Forms.Label label53;
        public System.Windows.Forms.Panel pnltls;

    }
}
