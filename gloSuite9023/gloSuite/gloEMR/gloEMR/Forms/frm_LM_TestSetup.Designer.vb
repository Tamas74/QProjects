<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_LM_TestSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                components.Dispose()
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
    '<System.Diagnostics.DebuggerStepThrough()> _
    'Private Sub InitializeComponent()
    '    components = New System.ComponentModel.Container
    '    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    '    Me.Text = "frm_LM_TestSetup"
    'End Sub

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LM_TestSetup))
        Me.pnlCategory = New System.Windows.Forms.Panel
        Me.trvCategories = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblDivierCategory = New System.Windows.Forms.Label
        Me.lblCategoryHeader = New System.Windows.Forms.Label
        Me.lblDividerCategory = New System.Windows.Forms.Label
        Me.pnlList = New System.Windows.Forms.Panel
        Me.c1List = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tls_strip = New gloGlobal.gloToolStripIgnoreFocus
        Me.tls_Category_strip = New System.Windows.Forms.ToolStripButton
        Me.tls_Specimen_strip = New System.Windows.Forms.ToolStripButton
        Me.tls_NewGroup_strip = New System.Windows.Forms.ToolStripButton
        Me.tls_NewTest_strip = New System.Windows.Forms.ToolStripButton
        Me.tls_Modify_strip = New System.Windows.Forms.ToolStripButton
        Me.tls_Delete_strip = New System.Windows.Forms.ToolStripButton
        Me.tls_Refresh_strip = New System.Windows.Forms.ToolStripButton
        Me.tls_Close_strip = New System.Windows.Forms.ToolStripButton
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlCategory.SuspendLayout()
        Me.pnlList.SuspendLayout()
        CType(Me.c1List, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tls_strip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlCategory
        '
        Me.pnlCategory.BackColor = System.Drawing.Color.Transparent
        Me.pnlCategory.Controls.Add(Me.trvCategories)
        Me.pnlCategory.Controls.Add(Me.Label10)
        Me.pnlCategory.Controls.Add(Me.Label5)
        Me.pnlCategory.Controls.Add(Me.Label6)
        Me.pnlCategory.Controls.Add(Me.Label7)
        Me.pnlCategory.Controls.Add(Me.Label8)
        Me.pnlCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCategory.Location = New System.Drawing.Point(0, 30)
        Me.pnlCategory.Name = "pnlCategory"
        Me.pnlCategory.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlCategory.Size = New System.Drawing.Size(185, 343)
        Me.pnlCategory.TabIndex = 2
        '
        'trvCategories
        '
        Me.trvCategories.BackColor = System.Drawing.Color.White
        Me.trvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCategories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCategories.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCategories.ForeColor = System.Drawing.Color.Black
        Me.trvCategories.FullRowSelect = True
        Me.trvCategories.HideSelection = False
        Me.trvCategories.ImageIndex = 0
        Me.trvCategories.ImageList = Me.ImageList1
        Me.trvCategories.Indent = 19
        Me.trvCategories.ItemHeight = 20
        Me.trvCategories.Location = New System.Drawing.Point(4, 5)
        Me.trvCategories.Name = "trvCategories"
        Me.trvCategories.SelectedImageIndex = 0
        Me.trvCategories.ShowLines = False
        Me.trvCategories.Size = New System.Drawing.Size(180, 334)
        Me.trvCategories.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Bullet06.ico")
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(180, 4)
        Me.Label10.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 339)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(180, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 339)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(184, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 339)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(182, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'lblDivierCategory
        '
        Me.lblDivierCategory.BackColor = System.Drawing.Color.CornflowerBlue
        Me.lblDivierCategory.Location = New System.Drawing.Point(470, 109)
        Me.lblDivierCategory.Name = "lblDivierCategory"
        Me.lblDivierCategory.Size = New System.Drawing.Size(105, 10)
        Me.lblDivierCategory.TabIndex = 2
        Me.lblDivierCategory.Visible = False
        '
        'lblCategoryHeader
        '
        Me.lblCategoryHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblCategoryHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCategoryHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategoryHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCategoryHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblCategoryHeader.Name = "lblCategoryHeader"
        Me.lblCategoryHeader.Size = New System.Drawing.Size(182, 24)
        Me.lblCategoryHeader.TabIndex = 0
        Me.lblCategoryHeader.Text = " Categories"
        Me.lblCategoryHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDividerCategory
        '
        Me.lblDividerCategory.AutoSize = True
        Me.lblDividerCategory.BackColor = System.Drawing.Color.CornflowerBlue
        Me.lblDividerCategory.Location = New System.Drawing.Point(256, 147)
        Me.lblDividerCategory.Name = "lblDividerCategory"
        Me.lblDividerCategory.Size = New System.Drawing.Size(0, 14)
        Me.lblDividerCategory.TabIndex = 3
        Me.lblDividerCategory.Visible = False
        '
        'pnlList
        '
        Me.pnlList.Controls.Add(Me.c1List)
        Me.pnlList.Controls.Add(Me.Label1)
        Me.pnlList.Controls.Add(Me.Label2)
        Me.pnlList.Controls.Add(Me.Label4)
        Me.pnlList.Controls.Add(Me.Label9)
        Me.pnlList.Controls.Add(Me.lblDividerCategory)
        Me.pnlList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlList.Location = New System.Drawing.Point(188, 54)
        Me.pnlList.Name = "pnlList"
        Me.pnlList.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlList.Size = New System.Drawing.Size(510, 373)
        Me.pnlList.TabIndex = 3
        '
        'c1List
        '
        Me.c1List.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1List.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.c1List.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1List.Location = New System.Drawing.Point(1, 4)
        Me.c1List.Name = "c1List"
        Me.c1List.Rows.DefaultSize = 19
        Me.c1List.Size = New System.Drawing.Size(505, 365)
        Me.c1List.StyleInfo = resources.GetString("c1List.StyleInfo")
        Me.c1List.TabIndex = 0
        Me.c1List.Tree.LineColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.c1List.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Solid
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 369)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(505, 1)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 366)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(506, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 366)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(507, 1)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(185, 30)
        Me.Panel4.TabIndex = 21
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.lblCategoryHeader)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(182, 24)
        Me.Panel1.TabIndex = 19
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(1, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(180, 1)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 23)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(181, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 23)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(182, 1)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlCategory)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(185, 373)
        Me.Panel2.TabIndex = 1
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tls_strip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(698, 54)
        Me.pnl_tlsp_Top.TabIndex = 0
        '
        'tls_strip
        '
        Me.tls_strip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tls_strip.BackgroundImage = CType(resources.GetObject("tls_strip.BackgroundImage"), System.Drawing.Image)
        Me.tls_strip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_strip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_strip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_strip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_strip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tls_Category_strip, Me.tls_Specimen_strip, Me.tls_NewGroup_strip, Me.tls_NewTest_strip, Me.tls_Modify_strip, Me.tls_Delete_strip, Me.tls_Refresh_strip, Me.tls_Close_strip})
        Me.tls_strip.Location = New System.Drawing.Point(0, 0)
        Me.tls_strip.Name = "tls_strip"
        Me.tls_strip.Size = New System.Drawing.Size(698, 53)
        Me.tls_strip.TabIndex = 0
        Me.tls_strip.Text = "ToolStrip1"
        '
        'tls_Category_strip
        '
        Me.tls_Category_strip.AutoSize = False
        Me.tls_Category_strip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Category_strip.Image = CType(resources.GetObject("tls_Category_strip.Image"), System.Drawing.Image)
        Me.tls_Category_strip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Category_strip.Name = "tls_Category_strip"
        Me.tls_Category_strip.Size = New System.Drawing.Size(67, 50)
        Me.tls_Category_strip.Tag = "Category"
        Me.tls_Category_strip.Text = "C&ategory"
        Me.tls_Category_strip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Category_strip.ToolTipText = "Category"
        '
        'tls_Specimen_strip
        '
        Me.tls_Specimen_strip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Specimen_strip.Image = CType(resources.GetObject("tls_Specimen_strip.Image"), System.Drawing.Image)
        Me.tls_Specimen_strip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Specimen_strip.Name = "tls_Specimen_strip"
        Me.tls_Specimen_strip.Size = New System.Drawing.Size(69, 50)
        Me.tls_Specimen_strip.Tag = "Specimen"
        Me.tls_Specimen_strip.Text = "&Specimen"
        Me.tls_Specimen_strip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Specimen_strip.ToolTipText = "Specimen"
        Me.tls_Specimen_strip.Visible = False
        '
        'tls_NewGroup_strip
        '
        Me.tls_NewGroup_strip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_NewGroup_strip.Image = CType(resources.GetObject("tls_NewGroup_strip.Image"), System.Drawing.Image)
        Me.tls_NewGroup_strip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_NewGroup_strip.Name = "tls_NewGroup_strip"
        Me.tls_NewGroup_strip.Size = New System.Drawing.Size(78, 50)
        Me.tls_NewGroup_strip.Tag = "NewGroup"
        Me.tls_NewGroup_strip.Text = "New &Group"
        Me.tls_NewGroup_strip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_NewGroup_strip.ToolTipText = "New Group"
        '
        'tls_NewTest_strip
        '
        Me.tls_NewTest_strip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_NewTest_strip.Image = CType(resources.GetObject("tls_NewTest_strip.Image"), System.Drawing.Image)
        Me.tls_NewTest_strip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_NewTest_strip.Name = "tls_NewTest_strip"
        Me.tls_NewTest_strip.Size = New System.Drawing.Size(67, 50)
        Me.tls_NewTest_strip.Tag = "NewTest"
        Me.tls_NewTest_strip.Text = "New &Test"
        Me.tls_NewTest_strip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_NewTest_strip.ToolTipText = "New Test"
        '
        'tls_Modify_strip
        '
        Me.tls_Modify_strip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Modify_strip.Image = CType(resources.GetObject("tls_Modify_strip.Image"), System.Drawing.Image)
        Me.tls_Modify_strip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Modify_strip.Name = "tls_Modify_strip"
        Me.tls_Modify_strip.Size = New System.Drawing.Size(53, 50)
        Me.tls_Modify_strip.Tag = "Modify"
        Me.tls_Modify_strip.Text = "&Modify"
        Me.tls_Modify_strip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Modify_strip.ToolTipText = "Modify"
        '
        'tls_Delete_strip
        '
        Me.tls_Delete_strip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Delete_strip.Image = CType(resources.GetObject("tls_Delete_strip.Image"), System.Drawing.Image)
        Me.tls_Delete_strip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Delete_strip.Name = "tls_Delete_strip"
        Me.tls_Delete_strip.Size = New System.Drawing.Size(50, 50)
        Me.tls_Delete_strip.Tag = "Delete"
        Me.tls_Delete_strip.Text = "&Delete"
        Me.tls_Delete_strip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Delete_strip.ToolTipText = "Delete"
        '
        'tls_Refresh_strip
        '
        Me.tls_Refresh_strip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Refresh_strip.Image = CType(resources.GetObject("tls_Refresh_strip.Image"), System.Drawing.Image)
        Me.tls_Refresh_strip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Refresh_strip.Name = "tls_Refresh_strip"
        Me.tls_Refresh_strip.Size = New System.Drawing.Size(58, 50)
        Me.tls_Refresh_strip.Tag = "Refresh"
        Me.tls_Refresh_strip.Text = "&Refresh"
        Me.tls_Refresh_strip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Refresh_strip.ToolTipText = "Refresh"
        '
        'tls_Close_strip
        '
        Me.tls_Close_strip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Close_strip.Image = CType(resources.GetObject("tls_Close_strip.Image"), System.Drawing.Image)
        Me.tls_Close_strip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Close_strip.Name = "tls_Close_strip"
        Me.tls_Close_strip.Size = New System.Drawing.Size(43, 50)
        Me.tls_Close_strip.Tag = "Close"
        Me.tls_Close_strip.Text = "&Close"
        Me.tls_Close_strip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_Close_strip.ToolTipText = "Close"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(185, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 373)
        Me.Splitter1.TabIndex = 24
        Me.Splitter1.TabStop = False
        '
        'frm_LM_TestSetup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(698, 427)
        Me.Controls.Add(Me.pnlList)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Controls.Add(Me.lblDivierCategory)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_LM_TestSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Order Templates"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlCategory.ResumeLayout(False)
        Me.pnlList.ResumeLayout(False)
        Me.pnlList.PerformLayout()
        CType(Me.c1List, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tls_strip.ResumeLayout(False)
        Me.tls_strip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tls_strip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tls_Category_strip As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_Specimen_strip As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_NewGroup_strip As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_NewTest_strip As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_Modify_strip As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_Delete_strip As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_Refresh_strip As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_Close_strip As System.Windows.Forms.ToolStripButton
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter

End Class
