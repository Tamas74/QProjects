<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMSTTableTemplate
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTTableTemplate))
        Me.tsb_TableTemplate = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnAddTable = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.gloTree_AvailableTables = New gloUserControlLibrary.gloUC_TreeView
        Me.imgCommon = New System.Windows.Forms.ImageList(Me.components)
        Me.gloTree_DataDictionary = New gloUserControlLibrary.gloUC_TreeView
        Me.trvTable = New System.Windows.Forms.TreeView
        Me.btnDown = New System.Windows.Forms.Button
        Me.btnUp = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.txtTemplateName = New System.Windows.Forms.TextBox
        Me.rbDay = New System.Windows.Forms.RadioButton
        Me.rbHx = New System.Windows.Forms.RadioButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmbTemplateCategory = New System.Windows.Forms.ComboBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.pnlToolStripTableTemplate = New System.Windows.Forms.Panel
        Me.pnlgloTree_AvailableTables = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label85 = New System.Windows.Forms.Label
        Me.Label89 = New System.Windows.Forms.Label
        Me.Label90 = New System.Windows.Forms.Label
        Me.lblNormHeader = New System.Windows.Forms.Label
        Me.pnlTableEditor = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.tsb_AddTable = New System.Windows.Forms.ToolStripButton
        Me.tsb_Close = New System.Windows.Forms.ToolStripButton
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.tsb_TableTemplate.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlToolStripTableTemplate.SuspendLayout()
        Me.pnlgloTree_AvailableTables.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTableEditor.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsb_TableTemplate
        '
        Me.tsb_TableTemplate.BackColor = System.Drawing.Color.Transparent
        Me.tsb_TableTemplate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tsb_TableTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsb_TableTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_TableTemplate.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsb_TableTemplate.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsb_TableTemplate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAddTable, Me.ToolStripButton3, Me.ToolStripButton2, Me.ToolStripButton1, Me.ts_btnClose})
        Me.tsb_TableTemplate.Location = New System.Drawing.Point(0, 0)
        Me.tsb_TableTemplate.Name = "tsb_TableTemplate"
        Me.tsb_TableTemplate.Size = New System.Drawing.Size(954, 53)
        Me.tsb_TableTemplate.TabIndex = 1
        Me.tsb_TableTemplate.Text = "ToolStrip1"
        '
        'ts_btnAddTable
        '
        Me.ts_btnAddTable.Image = CType(resources.GetObject("ts_btnAddTable.Image"), System.Drawing.Image)
        Me.ts_btnAddTable.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAddTable.Name = "ts_btnAddTable"
        Me.ts_btnAddTable.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAddTable.Tag = "New"
        Me.ts_btnAddTable.Text = "&New"
        Me.ts_btnAddTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(53, 50)
        Me.ToolStripButton3.Tag = "Modify"
        Me.ToolStripButton3.Text = "&Modify"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton2.Tag = "Delete"
        Me.ToolStripButton2.Text = "&Delete"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(58, 50)
        Me.ToolStripButton1.Tag = "Refresh"
        Me.ToolStripButton1.Text = "&Refresh"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'gloTree_AvailableTables
        '
        Me.gloTree_AvailableTables.BackColor = System.Drawing.Color.Transparent
        Me.gloTree_AvailableTables.CheckBoxes = False
        Me.gloTree_AvailableTables.CodeMember = Nothing
        Me.gloTree_AvailableTables.DescriptionMember = Nothing
        Me.gloTree_AvailableTables.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        Me.gloTree_AvailableTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloTree_AvailableTables.DrugFlag = CType(16, Short)
        Me.gloTree_AvailableTables.DrugFormMember = Nothing
        Me.gloTree_AvailableTables.DrugQtyQualifierMember = Nothing
        Me.gloTree_AvailableTables.DurationMember = Nothing
        Me.gloTree_AvailableTables.FrequencyMember = Nothing
        Me.gloTree_AvailableTables.ImageIndex = 2
        Me.gloTree_AvailableTables.ImageList = Me.imgCommon
        Me.gloTree_AvailableTables.ImageObject = Nothing
        Me.gloTree_AvailableTables.IsDrug = False
        Me.gloTree_AvailableTables.IsNarcoticsMember = Nothing
        Me.gloTree_AvailableTables.Location = New System.Drawing.Point(3, 29)
        Me.gloTree_AvailableTables.MaximumNodes = 1000
        Me.gloTree_AvailableTables.Name = "gloTree_AvailableTables"
        Me.gloTree_AvailableTables.NDCCodeMember = Nothing
        Me.gloTree_AvailableTables.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.gloTree_AvailableTables.ParentImageIndex = 1
        Me.gloTree_AvailableTables.ParentMember = Nothing
        Me.gloTree_AvailableTables.RouteMember = Nothing
        Me.gloTree_AvailableTables.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.gloTree_AvailableTables.SearchBox = True
        Me.gloTree_AvailableTables.SearchText = Nothing
        Me.gloTree_AvailableTables.SelectedImageIndex = 2
        Me.gloTree_AvailableTables.SelectedNode = Nothing
        Me.gloTree_AvailableTables.SelectedNodeIDs = CType(resources.GetObject("gloTree_AvailableTables.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.gloTree_AvailableTables.SelectedParentImageIndex = 1
        Me.gloTree_AvailableTables.Size = New System.Drawing.Size(264, 587)
        Me.gloTree_AvailableTables.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.gloTree_AvailableTables.TabIndex = 2
        Me.gloTree_AvailableTables.Tag = Nothing
        Me.gloTree_AvailableTables.UnitMember = Nothing
        Me.gloTree_AvailableTables.ValueMember = Nothing
        '
        'imgCommon
        '
        Me.imgCommon.ImageStream = CType(resources.GetObject("imgCommon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgCommon.TransparentColor = System.Drawing.Color.Transparent
        Me.imgCommon.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgCommon.Images.SetKeyName(1, "Add Data Dictornary.ico")
        Me.imgCommon.Images.SetKeyName(2, "FLow sheet.ico")
        '
        'gloTree_DataDictionary
        '
        Me.gloTree_DataDictionary.BackColor = System.Drawing.Color.Transparent
        Me.gloTree_DataDictionary.CheckBoxes = False
        Me.gloTree_DataDictionary.CodeMember = Nothing

        Me.gloTree_DataDictionary.DescriptionMember = Nothing
        Me.gloTree_DataDictionary.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        Me.gloTree_DataDictionary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloTree_DataDictionary.DrugFlag = CType(16, Short)
        Me.gloTree_DataDictionary.DrugFormMember = Nothing
        Me.gloTree_DataDictionary.DrugQtyQualifierMember = Nothing
        Me.gloTree_DataDictionary.DurationMember = Nothing
        Me.gloTree_DataDictionary.FrequencyMember = Nothing
        Me.gloTree_DataDictionary.ImageIndex = 0
        Me.gloTree_DataDictionary.ImageList = Me.imgCommon
        Me.gloTree_DataDictionary.ImageObject = Nothing
        Me.gloTree_DataDictionary.IsDrug = False
        Me.gloTree_DataDictionary.IsNarcoticsMember = Nothing
        Me.gloTree_DataDictionary.Location = New System.Drawing.Point(0, 3)
        Me.gloTree_DataDictionary.MaximumNodes = 1000
        Me.gloTree_DataDictionary.Name = "gloTree_DataDictionary"
        Me.gloTree_DataDictionary.NDCCodeMember = Nothing
        Me.gloTree_DataDictionary.ParentImageIndex = 0
        Me.gloTree_DataDictionary.ParentMember = Nothing
        Me.gloTree_DataDictionary.RouteMember = Nothing
        Me.gloTree_DataDictionary.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.gloTree_DataDictionary.SearchBox = False
        Me.gloTree_DataDictionary.SearchText = Nothing
        Me.gloTree_DataDictionary.SelectedImageIndex = 0
        Me.gloTree_DataDictionary.SelectedNode = Nothing
        Me.gloTree_DataDictionary.SelectedNodeIDs = CType(resources.GetObject("gloTree_DataDictionary.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.gloTree_DataDictionary.SelectedParentImageIndex = 0
        Me.gloTree_DataDictionary.Size = New System.Drawing.Size(288, 458)
        Me.gloTree_DataDictionary.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.gloTree_DataDictionary.TabIndex = 3
        Me.gloTree_DataDictionary.Tag = Nothing
        Me.gloTree_DataDictionary.UnitMember = Nothing
        Me.gloTree_DataDictionary.ValueMember = Nothing
        '
        'trvTable
        '
        Me.trvTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTable.HideSelection = False
        Me.trvTable.ImageIndex = 0
        Me.trvTable.ImageList = Me.imgCommon
        Me.trvTable.ItemHeight = 20
        Me.trvTable.Location = New System.Drawing.Point(0, 3)
        Me.trvTable.Name = "trvTable"
        Me.trvTable.SelectedImageIndex = 0
        Me.trvTable.ShowLines = False
        Me.trvTable.ShowPlusMinus = False
        Me.trvTable.ShowRootLines = False
        Me.trvTable.Size = New System.Drawing.Size(288, 457)
        Me.trvTable.TabIndex = 5
        '
        'btnDown
        '
        Me.btnDown.BackgroundImage = CType(resources.GetObject("btnDown.BackgroundImage"), System.Drawing.Image)
        Me.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(355, 255)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(25, 25)
        Me.btnDown.TabIndex = 7
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.BackgroundImage = CType(resources.GetObject("btnUp.BackgroundImage"), System.Drawing.Image)
        Me.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(355, 212)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(25, 25)
        Me.btnUp.TabIndex = 6
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.BackgroundImage = CType(resources.GetObject("btnRemove.BackgroundImage"), System.Drawing.Image)
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnRemove.FlatAppearance.BorderSize = 0
        Me.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Location = New System.Drawing.Point(15, 255)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(27, 25)
        Me.btnRemove.TabIndex = 2
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(15, 212)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(27, 25)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtTemplateName
        '
        Me.txtTemplateName.Location = New System.Drawing.Point(158, 12)
        Me.txtTemplateName.MaxLength = 100
        Me.txtTemplateName.Name = "txtTemplateName"
        Me.txtTemplateName.Size = New System.Drawing.Size(296, 22)
        Me.txtTemplateName.TabIndex = 0
        '
        'rbDay
        '
        Me.rbDay.AutoSize = True
        Me.rbDay.Location = New System.Drawing.Point(465, 10)
        Me.rbDay.Name = "rbDay"
        Me.rbDay.Size = New System.Drawing.Size(98, 18)
        Me.rbDay.TabIndex = 19
        Me.rbDay.TabStop = True
        Me.rbDay.Tag = "Day"
        Me.rbDay.Text = "Table for Day"
        Me.rbDay.UseVisualStyleBackColor = True
        Me.rbDay.Visible = False
        '
        'rbHx
        '
        Me.rbHx.AutoSize = True
        Me.rbHx.Location = New System.Drawing.Point(608, 10)
        Me.rbHx.Name = "rbHx"
        Me.rbHx.Size = New System.Drawing.Size(92, 18)
        Me.rbHx.TabIndex = 19
        Me.rbHx.TabStop = True
        Me.rbHx.Tag = "Hx"
        Me.rbHx.Text = "Table for Hx"
        Me.rbHx.UseVisualStyleBackColor = True
        Me.rbHx.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.cmbTemplateCategory)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel2.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel2.Controls.Add(Me.lbl_pnlRight)
        Me.Panel2.Controls.Add(Me.lbl_pnlTop)
        Me.Panel2.Controls.Add(Me.rbHx)
        Me.Panel2.Controls.Add(Me.rbDay)
        Me.Panel2.Controls.Add(Me.txtTemplateName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 3, 0)
        Me.Panel2.Size = New System.Drawing.Size(683, 73)
        Me.Panel2.TabIndex = 0
        '
        'cmbTemplateCategory
        '
        Me.cmbTemplateCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplateCategory.FormattingEnabled = True
        Me.cmbTemplateCategory.Items.AddRange(New Object() {"Taken today", "All Vitals", "Latest Vital", "Last 2 Vitals", "Last 3 Vitals", "Last 4 Vitals", "Last 5 Vitals", "Last 10 Vitals", "Taken in last 3 days", "Taken in last week", "Taken in last month", "Taken in last 3 months", "Taken in last 6 months", "Taken in last year"})
        Me.cmbTemplateCategory.Location = New System.Drawing.Point(158, 40)
        Me.cmbTemplateCategory.Name = "cmbTemplateCategory"
        Me.cmbTemplateCategory.Size = New System.Drawing.Size(296, 22)
        Me.cmbTemplateCategory.TabIndex = 1
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.ForeColor = System.Drawing.Color.Red
        Me.Label19.Location = New System.Drawing.Point(8, 14)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(14, 14)
        Me.Label19.TabIndex = 21
        Me.Label19.Text = "*"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(136, 14)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Table Template Name :"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 72)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(678, 1)
        Me.lbl_pnlBottom.TabIndex = 16
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 69)
        Me.lbl_pnlLeft.TabIndex = 15
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(679, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 69)
        Me.lbl_pnlRight.TabIndex = 14
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(680, 1)
        Me.lbl_pnlTop.TabIndex = 13
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnlToolStripTableTemplate
        '
        Me.pnlToolStripTableTemplate.Controls.Add(Me.tsb_TableTemplate)
        Me.pnlToolStripTableTemplate.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStripTableTemplate.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStripTableTemplate.Name = "pnlToolStripTableTemplate"
        Me.pnlToolStripTableTemplate.Size = New System.Drawing.Size(954, 54)
        Me.pnlToolStripTableTemplate.TabIndex = 21
        '
        'pnlgloTree_AvailableTables
        '
        Me.pnlgloTree_AvailableTables.Controls.Add(Me.gloTree_AvailableTables)
        Me.pnlgloTree_AvailableTables.Controls.Add(Me.Panel1)
        Me.pnlgloTree_AvailableTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlgloTree_AvailableTables.Location = New System.Drawing.Point(0, 54)
        Me.pnlgloTree_AvailableTables.Name = "pnlgloTree_AvailableTables"
        Me.pnlgloTree_AvailableTables.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlgloTree_AvailableTables.Size = New System.Drawing.Size(267, 619)
        Me.pnlgloTree_AvailableTables.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label65)
        Me.Panel1.Controls.Add(Me.Label85)
        Me.Panel1.Controls.Add(Me.Label89)
        Me.Panel1.Controls.Add(Me.Label90)
        Me.Panel1.Controls.Add(Me.lblNormHeader)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(264, 26)
        Me.Panel1.TabIndex = 20
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(1, 25)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(262, 1)
        Me.Label65.TabIndex = 13
        Me.Label65.Text = "label2"
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.Location = New System.Drawing.Point(0, 1)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 25)
        Me.Label85.TabIndex = 12
        Me.Label85.Text = "label4"
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label89.Location = New System.Drawing.Point(263, 1)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1, 25)
        Me.Label89.TabIndex = 11
        Me.Label89.Text = "label3"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(264, 1)
        Me.Label90.TabIndex = 10
        Me.Label90.Text = "label1"
        '
        'lblNormHeader
        '
        Me.lblNormHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblNormHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNormHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNormHeader.ForeColor = System.Drawing.Color.White
        Me.lblNormHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNormHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblNormHeader.Name = "lblNormHeader"
        Me.lblNormHeader.Size = New System.Drawing.Size(264, 26)
        Me.lblNormHeader.TabIndex = 9
        Me.lblNormHeader.Text = "   Table Templates"
        Me.lblNormHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlTableEditor
        '
        Me.pnlTableEditor.Controls.Add(Me.Panel4)
        Me.pnlTableEditor.Controls.Add(Me.Panel6)
        Me.pnlTableEditor.Controls.Add(Me.Panel2)
        Me.pnlTableEditor.Controls.Add(Me.pnlToolStrip)
        Me.pnlTableEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTableEditor.Location = New System.Drawing.Point(271, 54)
        Me.pnlTableEditor.Name = "pnlTableEditor"
        Me.pnlTableEditor.Size = New System.Drawing.Size(683, 619)
        Me.pnlTableEditor.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Panel7)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.btnAdd)
        Me.Panel4.Controls.Add(Me.btnRemove)
        Me.Panel4.Controls.Add(Me.btnDown)
        Me.Panel4.Controls.Add(Me.btnUp)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(288, 127)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(395, 492)
        Me.Panel4.TabIndex = 2
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel9)
        Me.Panel7.Controls.Add(Me.Panel8)
        Me.Panel7.Location = New System.Drawing.Point(55, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(288, 486)
        Me.Panel7.TabIndex = 2
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Label18)
        Me.Panel9.Controls.Add(Me.Label17)
        Me.Panel9.Controls.Add(Me.Label16)
        Me.Panel9.Controls.Add(Me.Label15)
        Me.Panel9.Controls.Add(Me.trvTable)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(0, 26)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel9.Size = New System.Drawing.Size(288, 460)
        Me.Panel9.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(1, 3)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(286, 1)
        Me.Label18.TabIndex = 17
        Me.Label18.Text = "label1"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(287, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 456)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 456)
        Me.Label16.TabIndex = 15
        Me.Label16.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(0, 459)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(288, 1)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "label2"
        '
        'Panel8
        '
        Me.Panel8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label10)
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Controls.Add(Me.Label12)
        Me.Panel8.Controls.Add(Me.Label13)
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(288, 26)
        Me.Panel8.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(286, 1)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 25)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(287, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 25)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(288, 1)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "label1"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(288, 26)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "   Selected Dictionary"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(0, 488)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(391, 1)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(391, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 485)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(0, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(392, 1)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel5)
        Me.Panel6.Controls.Add(Me.Panel3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(0, 127)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(288, 492)
        Me.Panel6.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.gloTree_DataDictionary)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 28)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel5.Size = New System.Drawing.Size(288, 461)
        Me.Panel5.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(288, 25)
        Me.Panel3.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(286, 1)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 24)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(287, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 24)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(288, 1)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(288, 25)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "   Data Dictionary"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.ToolStrip1)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlToolStrip.Size = New System.Drawing.Size(683, 54)
        Me.pnlToolStrip.TabIndex = 3
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_AddTable, Me.tsb_Close})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(683, 53)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsb_AddTable
        '
        Me.tsb_AddTable.Image = CType(resources.GetObject("tsb_AddTable.Image"), System.Drawing.Image)
        Me.tsb_AddTable.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_AddTable.Name = "tsb_AddTable"
        Me.tsb_AddTable.Size = New System.Drawing.Size(66, 50)
        Me.tsb_AddTable.Tag = "Save and Close"
        Me.tsb_AddTable.Text = "&Save&&Cls"
        Me.tsb_AddTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_AddTable.ToolTipText = "Save and Close"
        '
        'tsb_Close
        '
        Me.tsb_Close.Image = CType(resources.GetObject("tsb_Close.Image"), System.Drawing.Image)
        Me.tsb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Close.Name = "tsb_Close"
        Me.tsb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tsb_Close.Tag = "Close"
        Me.tsb_Close.Text = "&Close"
        Me.tsb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(267, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 619)
        Me.Splitter1.TabIndex = 24
        Me.Splitter1.TabStop = False
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Taken today", "All Vitals", "Latest Vital", "Last 2 Vitals", "Last 3 Vitals", "Last 4 Vitals", "Last 5 Vitals", "Last 10 Vitals", "Taken in last 3 days", "Taken in last week", "Taken in last month", "Taken in last 3 months", "Taken in last 6 months", "Taken in last year"})
        Me.ComboBox1.Location = New System.Drawing.Point(403, 40)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(228, 21)
        Me.ComboBox1.TabIndex = 22
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(91, 43)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 14)
        Me.Label21.TabIndex = 25
        Me.Label21.Text = "Category :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.Color.Red
        Me.Label20.Location = New System.Drawing.Point(82, 41)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(14, 14)
        Me.Label20.TabIndex = 26
        Me.Label20.Text = "*"
        '
        'frmMSTTableTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(954, 673)
        Me.Controls.Add(Me.pnlTableEditor)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlgloTree_AvailableTables)
        Me.Controls.Add(Me.pnlToolStripTableTemplate)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTTableTemplate"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Table Template "
        Me.tsb_TableTemplate.ResumeLayout(False)
        Me.tsb_TableTemplate.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlToolStripTableTemplate.ResumeLayout(False)
        Me.pnlToolStripTableTemplate.PerformLayout()
        Me.pnlgloTree_AvailableTables.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlTableEditor.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsb_TableTemplate As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents gloTree_AvailableTables As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents gloTree_DataDictionary As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents trvTable As System.Windows.Forms.TreeView
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents ts_btnAddTable As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtTemplateName As System.Windows.Forms.TextBox
    Friend WithEvents rbDay As System.Windows.Forms.RadioButton
    Friend WithEvents rbHx As System.Windows.Forms.RadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents pnlToolStripTableTemplate As System.Windows.Forms.Panel
    Friend WithEvents pnlgloTree_AvailableTables As System.Windows.Forms.Panel
    Friend WithEvents pnlTableEditor As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsb_AddTable As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents imgCommon As System.Windows.Forms.ImageList
    Friend WithEvents tsb_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents lblNormHeader As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbTemplateCategory As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
End Class
