Public Class frmVWTasks
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents pnltrTasks As System.Windows.Forms.Panel
    Friend WithEvents trTasks As System.Windows.Forms.TreeView
    Friend WithEvents dgTasks As System.Windows.Forms.DataGrid
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents ContextMnuReAssTask As System.Windows.Forms.ContextMenu
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As System.Windows.Forms.ToolStrip
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents spltTasks As System.Windows.Forms.Splitter
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWTasks))
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnltrTasks = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.trTasks = New System.Windows.Forms.TreeView
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.dgTasks = New System.Windows.Forms.DataGrid
        Me.ContextMnuReAssTask = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New System.Windows.Forms.ToolStrip
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.spltTasks = New System.Windows.Forms.Splitter
        Me.pnlTopRight.SuspendLayout()
        Me.pnltrTasks.SuspendLayout()
        CType(Me.dgTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.txtSearch)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label5)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTopRight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(599, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(61, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(301, 22)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(60, 20)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = "  Tasks :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(597, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 23)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(598, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(599, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnltrTasks
        '
        Me.pnltrTasks.Controls.Add(Me.lbl_BottomBrd)
        Me.pnltrTasks.Controls.Add(Me.lbl_LeftBrd)
        Me.pnltrTasks.Controls.Add(Me.lbl_RightBrd)
        Me.pnltrTasks.Controls.Add(Me.lbl_TopBrd)
        Me.pnltrTasks.Controls.Add(Me.trTasks)
        Me.pnltrTasks.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnltrTasks.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrTasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnltrTasks.Location = New System.Drawing.Point(0, 84)
        Me.pnltrTasks.Name = "pnltrTasks"
        Me.pnltrTasks.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrTasks.Size = New System.Drawing.Size(211, 434)
        Me.pnltrTasks.TabIndex = 2
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 430)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(206, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 430)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(210, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 430)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(208, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'trTasks
        '
        Me.trTasks.BackColor = System.Drawing.Color.White
        Me.trTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trTasks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trTasks.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trTasks.ForeColor = System.Drawing.Color.Black
        Me.trTasks.HideSelection = False
        Me.trTasks.ImageIndex = 0
        Me.trTasks.ImageList = Me.imgTreeView
        Me.trTasks.Indent = 20
        Me.trTasks.ItemHeight = 20
        Me.trTasks.Location = New System.Drawing.Point(3, 0)
        Me.trTasks.Name = "trTasks"
        Me.trTasks.SelectedImageIndex = 0
        Me.trTasks.ShowLines = False
        Me.trTasks.Size = New System.Drawing.Size(208, 431)
        Me.trTasks.TabIndex = 0
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Task All_01.ico")
        Me.imgTreeView.Images.SetKeyName(1, "Active Task_02.ico")
        Me.imgTreeView.Images.SetKeyName(2, "Next Seven Days_03.ico")
        Me.imgTreeView.Images.SetKeyName(3, "OverDue Task_03.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Task_02.ico")
        '
        'dgTasks
        '
        Me.dgTasks.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgTasks.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgTasks.BackgroundColor = System.Drawing.Color.White
        Me.dgTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgTasks.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgTasks.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTasks.CaptionForeColor = System.Drawing.Color.White
        Me.dgTasks.CaptionVisible = False
        Me.dgTasks.DataMember = ""
        Me.dgTasks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgTasks.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTasks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgTasks.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgTasks.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgTasks.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgTasks.HeaderForeColor = System.Drawing.Color.White
        Me.dgTasks.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgTasks.Location = New System.Drawing.Point(1, 1)
        Me.dgTasks.Name = "dgTasks"
        Me.dgTasks.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgTasks.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgTasks.ReadOnly = True
        Me.dgTasks.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgTasks.SelectionForeColor = System.Drawing.Color.Black
        Me.dgTasks.Size = New System.Drawing.Size(387, 430)
        Me.dgTasks.TabIndex = 3
        '
        'ContextMnuReAssTask
        '
        Me.ContextMnuReAssTask.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Re Assign Task"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(605, 54)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(605, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dgTasks)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(214, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(391, 434)
        Me.Panel1.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(2, 430)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(385, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 429)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(387, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 429)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(387, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(605, 30)
        Me.Panel2.TabIndex = 13
        '
        'spltTasks
        '
        Me.spltTasks.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltTasks.Location = New System.Drawing.Point(211, 84)
        Me.spltTasks.Name = "spltTasks"
        Me.spltTasks.Size = New System.Drawing.Size(3, 434)
        Me.spltTasks.TabIndex = 14
        Me.spltTasks.TabStop = False
        '
        'frmVWTasks
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(605, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.spltTasks)
        Me.Controls.Add(Me.pnltrTasks)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWTasks"
        Me.ShowInTaskbar = False
        Me.Text = "Tasks"
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnltrTasks.ResumeLayout(False)
        CType(Me.dgTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private TasksDBLayer As New ClsTasksDBLayer
    Private LoginId As Integer
    Private _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String



    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub BindGrid(ByVal type As String, Optional ByVal strcolumnName As String = "", Optional ByVal strsortorder As String = "", Optional ByVal strSearchstring As String = "")
        Try
            If TasksDBLayer.FetchData(type) Then
                If Not IsNothing(TasksDBLayer.DsDataview) Then
                    dgTasks.SetDataBinding(TasksDBLayer.DsDataview, "")
                    TasksDBLayer.SortDataview(TasksDBLayer.DsDataview.Table.Columns(1).ColumnName)
                    txtSearch.Text = ""
                    txtSearch.Text = strSearchstring
                    If strcolumnName = "" Then
                        TasksDBLayer.SortDataview(TasksDBLayer.DsDataview.Table.Columns(1).ColumnName)
                    Else
                        Dim strColumn As String = Replace(strcolumnName, "[", "")

                        TasksDBLayer.SortDataview(strColumn, strsortorder)
                    End If

                    HideColumn()
                End If
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub HideColumn()
        Dim ts As New clsDataGridTableStyle(TasksDBLayer.DsDataview.Table.TableName)
        ts.ReadOnly = True
        Dim dgID As New DataGridTextBoxColumn

        With dgID
            .MappingName = TasksDBLayer.DsDataview.Table.Columns(0).ColumnName
            .Alignment = HorizontalAlignment.Center
            .NullText = ""
            .Width = 0
            .ReadOnly = True
        End With

        Dim dgCol1 As New DataGridTextBoxColumn
        With dgCol1
            .MappingName = TasksDBLayer.DsDataview.Table.Columns(1).ColumnName
            .HeaderText = "Subject"
            .NullText = ""
            .Width = 0.5 * dgTasks.Width
            .ReadOnly = True
        End With

        Dim dgCol2 As New DataGridTextBoxColumn
        With dgCol2
            .MappingName = TasksDBLayer.DsDataview.Table.Columns(2).ColumnName
            .HeaderText = "Due Date"
            .NullText = ""
            .Width = 0.2 * dgTasks.Width
            .ReadOnly = True
        End With

        Dim dgCol3 As New DataGridTextBoxColumn
        With dgCol3
            .MappingName = TasksDBLayer.DsDataview.Table.Columns(3).ColumnName
            .HeaderText = "Status"
            .NullText = ""
            .Width = 0.5 * dgTasks.Width - 7
            .ReadOnly = True
        End With

        ts.GridColumnStyles.Clear()
        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3})
        dgTasks.TableStyles.Clear()
        dgTasks.TableStyles.Add(ts)

    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub frmVWTasks_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            trTasks.Nodes.Add("All Tasks")
            trTasks.Nodes.Item(0).ForeColor = Color.Blue
            trTasks.Nodes.Item(0).ImageIndex = 0
            trTasks.Nodes.Item(0).SelectedImageIndex = 0

            trTasks.Nodes.Add("Active Tasks")
            trTasks.Nodes.Item(1).ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)
            trTasks.Nodes.Item(1).ImageIndex = 1
            trTasks.Nodes.Item(1).SelectedImageIndex = 1

            trTasks.Nodes.Add("Next seven Days")
            trTasks.Nodes.Item(2).ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)
            trTasks.Nodes.Item(2).ImageIndex = 2
            trTasks.Nodes.Item(2).SelectedImageIndex = 2

            trTasks.Nodes.Add("Overdue Tasks")
            trTasks.Nodes.Item(3).ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)
            trTasks.Nodes.Item(3).ImageIndex = 3
            trTasks.Nodes.Item(3).SelectedImageIndex = 3

            trTasks.Nodes.Add("Completed Tasks")
            trTasks.Nodes.Item(4).ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)
            trTasks.Nodes.Item(4).ImageIndex = 4
            trTasks.Nodes.Item(4).SelectedImageIndex = 4

            trTasks.SelectedNode = trTasks.Nodes.Item(0)
            LoginId = TasksDBLayer.GetLoginId
            dgTasks.AllowSorting = True
            CallBindGrid()
            lblSearch.Text = "Subject"
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '''''''''Added by Anil on 04/10/2007 at 2:30 p.m.
    '''''''This code is added for In-String search in the grid.
    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If dgTasks.CurrentRowIndex >= 0 Then
                    dgTasks.Select(0)
                    dgTasks.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            'TasksDBLayer.SetRowFilter(1, txtSearch.Text)

            ''If Not IsNothing(dgTasks.DataSource) Then
            ''Dim str As String
            ''str = TasksDBLayer.DsDataview.Sort
            ''str = Mid(str, 2)
            ''str = Mid(str, 1, Len(str) - 1)
            ''If str <> TasksDBLayer.DsDataview.Table.Columns(2).ColumnName Then
            ''    TasksDBLayer.SetRowFilter(Trim(txtSearch.Text))
            ''    HideColumn()
            ''End If
            ''End If

            ''''#####*****The above code is commented by Anil on 04/10/2007 at 2:30 p.m.
            ''''#####*****The code below is Added by Anil 0n 04/10/2007 at 2:30 p.m.
            ''''''''''''''The code written below is for In-string search.
            If _blnSearch = True Then
                Dim dvTasks As DataView
                dvTasks = CType(dgTasks.DataSource, DataView)

                If IsNothing(dvTasks) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                dgTasks.DataSource = dvTasks
                Dim strTaskSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strTaskSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strTaskSearchDetails = Replace(strTaskSearchDetails, "[", "") & ""
                    strTaskSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strTaskSearchDetails)
                Else
                    strTaskSearchDetails = ""
                End If
                Select Case Trim(lblSearch.Text)
                    Case "Subject"
                        dvTasks.RowFilter = dvTasks.Table.Columns(1).ColumnName & " Like '%" & strTaskSearchDetails & "%'"
                    Case "Status"
                        dvTasks.RowFilter = dvTasks.Table.Columns(3).ColumnName & " Like '%" & strTaskSearchDetails & "%'"
                End Select



                ' dvTasks.RowFilter = dvTasks.Table.Columns(1).ColumnName & " Like '%" & strTaskSearchDetails & "%'"

                ''''''''''''''Upto here the code is added.
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgTasks_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If dgTasks.CurrentRowIndex >= 0 Then
            dgTasks.Select(dgTasks.CurrentRowIndex)
        End If
    End Sub
    Private Sub dgTasks_MouseUp1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If dgTasks.CurrentRowIndex >= 0 Then
            dgTasks.Select(dgTasks.CurrentRowIndex)
        End If
    End Sub

    Private Sub dgTasks_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgTasks.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As DataGrid.HitTestInfo = dgTasks.HitTest(ptPoint)
        If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
            Select Case htInfo.Column ' dgCategoryList.CurrentCell.ColumnNumber
                Case 1
                    lblSearch.Text = "Subject"
                Case 3
                    lblSearch.Text = "Status"
            End Select
            If txtSearch.Text = "" Then
                _blnSearch = True
            Else
                _blnSearch = False
                txtSearch.Text = ""
                _blnSearch = True
            End If
        ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
            _blnSearch = True
            Dim ex As System.EventArgs
            UpdateCategory()
        End If


    End Sub

    Private Sub dgTasks_MouseUp2(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgTasks.MouseUp
        If dgTasks.CurrentRowIndex >= 0 Then
            dgTasks.Select(dgTasks.CurrentRowIndex)
        End If
    End Sub
    Private Sub CallBindGrid(Optional ByVal strcolumnName As String = "", Optional ByVal strsortorder As String = "", Optional ByVal strSearchstring As String = "")
        Select Case trTasks.SelectedNode.Index
            Case 0
                BindGrid("", strcolumnName, strsortorder, strSearchstring)
            Case 1
                BindGrid("A", strcolumnName, strsortorder, strSearchstring)
            Case 2
                BindGrid("S", strcolumnName, strsortorder, strSearchstring)
            Case 3
                BindGrid("O", strcolumnName, strsortorder, strSearchstring)
            Case 4
                BindGrid("C", strcolumnName, strsortorder, strSearchstring)
        End Select
    End Sub
    Private Sub trTasks_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trTasks.AfterSelect
        CallBindGrid()
    End Sub

    Private Sub frmVWTasks_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub



    Private Sub dgTasks_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgTasks.MouseDown
        Dim TaskStatus As String = "Completed"
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If dgTasks.CurrentRowIndex >= 0 Then
                    '' To Select The Row
                    dgTasks.Select(dgTasks.CurrentRowIndex)

                    '' To Assign Context Menu
                    If TaskStatus <> CType(dgTasks.Item(dgTasks.CurrentRowIndex, 3), System.String) Then
                        '''' If Status is NOT Completed Then Only show Context Menu
                        dgTasks.ContextMenu = ContextMnuReAssTask
                    Else
                        dgTasks.ContextMenu = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Context Menu --Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        'Dim objfrmTasks As New  frmTasks

        'Try
        '    objfrmMSTHistory.Text = "Add History for " & BtnText
        '    objfrmMSTHistory.ShowDialog()
        '    'btntext contains the description of selected category
        '    FillHistoryCategory1(BtnText)
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    objfrmMSTHistory = Nothing
        'End Try
        Try
            If dgTasks.VisibleRowCount >= 1 Then
                Dim frm As frmTasks
                Dim ID As Long

                ID = CType(dgTasks.Item(dgTasks.CurrentRowIndex, 0), System.Int64)
                frm = New frmTasks(LoginId, ID)
                frm.ReAssignFlag = True
                frm.Text = "Re Assign Task"
                frm.ShowDialog()
                CallBindGrid()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007
    Private Sub AddCategory()
        Try
            Dim frm As frmTasks
            frm = New frmTasks(LoginId)
            frm.Text = "Add Tasks"
            frm.ShowDialog()
            CallBindGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateCategory()
        Try
            If dgTasks.VisibleRowCount >= 1 Then
                Dim ID As Long
                ID = CType(dgTasks.Item(dgTasks.CurrentRowIndex, 0), System.Int64)

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070723
                Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As New mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.Task, ID, 0, Now)
                    If mydt.Description <> gstrClientMachineName Then
                        If MessageBox.Show("This Tasks is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify this task now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            blnRecordLock = True
                        Else
                            Exit Sub
                        End If
                    End If
                End If
                ' '' <><><> Record Level Locking <><><><> 
                sortOrder = CType(dgTasks.DataSource, DataView).Sort
                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If
                Dim frm As frmTasks
                frm = New frmTasks(LoginId, ID, , blnRecordLock)
                frm.Text = "Update Tasks"
                frm.ShowDialog()
                CallBindGrid(strcolumnName, strsortorder, strSearchstring)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteCategory()
        Try
            If dgTasks.VisibleRowCount >= 1 Then
                Dim ID As Long
                ID = CType(dgTasks.Item(dgTasks.CurrentRowIndex, 0), Long)

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070724
                If gblnRecordLocking = True Then
                    Dim mydt As New mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.Task, ID, 0, Now)
                    If mydt.Description <> gstrClientMachineName Then
                        MessageBox.Show("This Tasks is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
                ' '' <><><> Record Level Locking <><><><> 

                If MessageBox.Show("Are you sure you want to delete this Record", "Tasks", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    TasksDBLayer.DeleteData(ID)
                    sortOrder = CType(dgTasks.DataSource, DataView).Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                    CallBindGrid(strcolumnName, strsortorder, strSearchstring)
                End If
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Try
            Me.Cursor = Cursors.WaitCursor
            '''''''' The following 3 lines are added by Anil on 04/10/2007 at 11:00 a.m.
            ''''''''This is done to clear the Search Textbox, to refresh the grid and  to get the focus on first tree node on refresh click. 
            txtSearch.Clear()
            trTasks.SelectedNode = trTasks.Nodes.Item(0)
            CallBindGrid()
            ''''''''upto here.
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Try
            Dim frm As MainMenu
            frm = Me.MdiParent
            frm.Fill_Tasks()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                Call AddCategory()
            Case "Modify"
                Call UpdateCategory()
            Case "Delete"
                Call DeleteCategory()
            Case "Refresh"
                Call RefreshCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub
End Class
