<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloDrugListRevised
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloDrugListRevised))
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.picProgress = New System.Windows.Forms.PictureBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.pnlTreeView = New System.Windows.Forms.Panel()
        Me.trvMain = New System.Windows.Forms.TreeView()
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddDrugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddProviderFavoriteDrugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddtoPlanMedMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImgList = New System.Windows.Forms.ImageList(Me.components)
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnl_DrugList = New System.Windows.Forms.Panel()
        Me.btnDrugList = New System.Windows.Forms.Button()
        Me.pnl_Classified = New System.Windows.Forms.Panel()
        Me.btnClassified = New System.Windows.Forms.Button()
        Me.pnl_ProviderFav = New System.Windows.Forms.Panel()
        Me.btnProviderFav = New System.Windows.Forms.Button()
        Me.pnl_AllDrugs = New System.Windows.Forms.Panel()
        Me.btnAllDrugs = New System.Windows.Forms.Button()
        Me.pnl_PracticeFav = New System.Windows.Forms.Panel()
        Me.btnPracticeFav = New System.Windows.Forms.Button()
        Me.pnl_ProviderFrequent = New System.Windows.Forms.Panel()
        Me.btnProviderFrequent = New System.Windows.Forms.Button()
        Me.pnl_Frequent = New System.Windows.Forms.Panel()
        Me.btnFrequent = New System.Windows.Forms.Button()
        Me.pnlMainSearch = New System.Windows.Forms.Panel()
        Me.pnlbtnSearchFilter = New System.Windows.Forms.Panel()
        Me.btnDrugStartContent = New System.Windows.Forms.Button()
        Me.pnlSearchFilter = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbContains = New System.Windows.Forms.RadioButton()
        Me.rbStartswith = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnl_PlannedDrugs = New System.Windows.Forms.Panel()
        Me.btnPlannedDrugs = New System.Windows.Forms.Button()
        Me.pnlSearch.SuspendLayout()
        CType(Me.picProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTreeView.SuspendLayout()
        Me.cntListmenuStrip.SuspendLayout()
        Me.pnl_DrugList.SuspendLayout()
        Me.pnl_Classified.SuspendLayout()
        Me.pnl_ProviderFav.SuspendLayout()
        Me.pnl_AllDrugs.SuspendLayout()
        Me.pnl_PracticeFav.SuspendLayout()
        Me.pnl_ProviderFrequent.SuspendLayout()
        Me.pnl_Frequent.SuspendLayout()
        Me.pnlMainSearch.SuspendLayout()
        Me.pnlbtnSearchFilter.SuspendLayout()
        Me.pnlSearchFilter.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_PlannedDrugs.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.White
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.picProgress)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(244, 26)
        Me.pnlSearch.TabIndex = 1
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(29, 4)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(178, 15)
        Me.txtSearch.TabIndex = 1
        '
        'picProgress
        '
        Me.picProgress.BackColor = System.Drawing.Color.White
        Me.picProgress.Dock = System.Windows.Forms.DockStyle.Right
        Me.picProgress.Image = Global.gloUserControlLibrary.My.Resources.Resources.Progress
        Me.picProgress.Location = New System.Drawing.Point(207, 4)
        Me.picProgress.Margin = New System.Windows.Forms.Padding(2)
        Me.picProgress.Name = "picProgress"
        Me.picProgress.Size = New System.Drawing.Size(15, 16)
        Me.picProgress.TabIndex = 46
        Me.picProgress.TabStop = False
        Me.picProgress.Visible = False
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.Transparent
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(29, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(193, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.Transparent
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(29, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(193, 5)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(222, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 24)
        Me.btnClear.TabIndex = 41
        Me.btnClear.TabStop = False
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(1, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 24)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(1, 25)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(242, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(242, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 26)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(243, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 26)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'pnlTreeView
        '
        Me.pnlTreeView.Controls.Add(Me.trvMain)
        Me.pnlTreeView.Controls.Add(Me.Label33)
        Me.pnlTreeView.Controls.Add(Me.Label31)
        Me.pnlTreeView.Controls.Add(Me.Label5)
        Me.pnlTreeView.Controls.Add(Me.Label6)
        Me.pnlTreeView.Controls.Add(Me.Label7)
        Me.pnlTreeView.Controls.Add(Me.Label8)
        Me.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTreeView.Location = New System.Drawing.Point(0, 88)
        Me.pnlTreeView.Name = "pnlTreeView"
        Me.pnlTreeView.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlTreeView.Size = New System.Drawing.Size(270, 317)
        Me.pnlTreeView.TabIndex = 1
        '
        'trvMain
        '
        Me.trvMain.BackColor = System.Drawing.Color.White
        Me.trvMain.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvMain.ContextMenuStrip = Me.cntListmenuStrip
        Me.trvMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvMain.ForeColor = System.Drawing.Color.Black
        Me.trvMain.HideSelection = False
        Me.trvMain.ImageIndex = 0
        Me.trvMain.ImageList = Me.ImgList
        Me.trvMain.Indent = 20
        Me.trvMain.ItemHeight = 20
        Me.trvMain.Location = New System.Drawing.Point(5, 8)
        Me.trvMain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.trvMain.Name = "trvMain"
        Me.trvMain.SelectedImageIndex = 0
        Me.trvMain.ShowLines = False
        Me.trvMain.ShowNodeToolTips = True
        Me.trvMain.ShowPlusMinus = False
        Me.trvMain.ShowRootLines = False
        Me.trvMain.Size = New System.Drawing.Size(264, 308)
        Me.trvMain.TabIndex = 1
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddDrugToolStripMenuItem, Me.AddProviderFavoriteDrugToolStripMenuItem, Me.AddtoPlanMedMenuItem})
        Me.cntListmenuStrip.Name = "cntListmenuStrip"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(222, 70)
        '
        'AddDrugToolStripMenuItem
        '
        Me.AddDrugToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.AddDrugToolStripMenuItem.Image = CType(resources.GetObject("AddDrugToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AddDrugToolStripMenuItem.Name = "AddDrugToolStripMenuItem"
        Me.AddDrugToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.AddDrugToolStripMenuItem.Tag = "1"
        Me.AddDrugToolStripMenuItem.Text = "Add Drug"
        Me.AddDrugToolStripMenuItem.Visible = False
        '
        'AddProviderFavoriteDrugToolStripMenuItem
        '
        Me.AddProviderFavoriteDrugToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.AddProviderFavoriteDrugToolStripMenuItem.Image = CType(resources.GetObject("AddProviderFavoriteDrugToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AddProviderFavoriteDrugToolStripMenuItem.Name = "AddProviderFavoriteDrugToolStripMenuItem"
        Me.AddProviderFavoriteDrugToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.AddProviderFavoriteDrugToolStripMenuItem.Tag = "2"
        Me.AddProviderFavoriteDrugToolStripMenuItem.Text = "Add Provider Favorite Drug"
        Me.AddProviderFavoriteDrugToolStripMenuItem.Visible = False
        '
        'AddtoPlanMedMenuItem
        '
        Me.AddtoPlanMedMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.AddtoPlanMedMenuItem.Image = CType(resources.GetObject("AddtoPlanMedMenuItem.Image"), System.Drawing.Image)
        Me.AddtoPlanMedMenuItem.Name = "AddtoPlanMedMenuItem"
        Me.AddtoPlanMedMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.AddtoPlanMedMenuItem.Text = "Add To Planned Medication"
        '
        'ImgList
        '
        Me.ImgList.ImageStream = CType(resources.GetObject("ImgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImgList.Images.SetKeyName(1, "Small Arrow.ico")
        Me.ImgList.Images.SetKeyName(2, "Drugs.ico")
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.White
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Location = New System.Drawing.Point(1, 8)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(4, 308)
        Me.Label33.TabIndex = 39
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.White
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Location = New System.Drawing.Point(1, 4)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(268, 4)
        Me.Label31.TabIndex = 38
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 316)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(268, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 313)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(269, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 313)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(270, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnl_DrugList
        '
        Me.pnl_DrugList.BackColor = System.Drawing.Color.Transparent
        Me.pnl_DrugList.Controls.Add(Me.btnDrugList)
        Me.pnl_DrugList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_DrugList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_DrugList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_DrugList.Location = New System.Drawing.Point(0, 56)
        Me.pnl_DrugList.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_DrugList.Name = "pnl_DrugList"
        Me.pnl_DrugList.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_DrugList.Size = New System.Drawing.Size(270, 32)
        Me.pnl_DrugList.TabIndex = 655555555
        Me.pnl_DrugList.TabStop = True
        '
        'btnDrugList
        '
        Me.btnDrugList.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnDrugList.BackgroundImage = CType(resources.GetObject("btnDrugList.BackgroundImage"), System.Drawing.Image)
        Me.btnDrugList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDrugList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDrugList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDrugList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDrugList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrugList.Location = New System.Drawing.Point(0, 3)
        Me.btnDrugList.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnDrugList.Name = "btnDrugList"
        Me.btnDrugList.Size = New System.Drawing.Size(270, 29)
        Me.btnDrugList.TabIndex = 1
        Me.btnDrugList.Text = "Drug List"
        Me.btnDrugList.UseVisualStyleBackColor = False
        '
        'pnl_Classified
        '
        Me.pnl_Classified.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Classified.Controls.Add(Me.btnClassified)
        Me.pnl_Classified.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_Classified.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Classified.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Classified.Location = New System.Drawing.Point(0, 565)
        Me.pnl_Classified.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_Classified.Name = "pnl_Classified"
        Me.pnl_Classified.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_Classified.Size = New System.Drawing.Size(270, 32)
        Me.pnl_Classified.TabIndex = 7
        '
        'btnClassified
        '
        Me.btnClassified.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnClassified.BackgroundImage = CType(resources.GetObject("btnClassified.BackgroundImage"), System.Drawing.Image)
        Me.btnClassified.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClassified.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClassified.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClassified.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClassified.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClassified.Location = New System.Drawing.Point(0, 3)
        Me.btnClassified.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnClassified.Name = "btnClassified"
        Me.btnClassified.Size = New System.Drawing.Size(270, 29)
        Me.btnClassified.TabIndex = 0
        Me.btnClassified.Tag = "22"
        Me.btnClassified.Text = "Classified Drugs"
        Me.btnClassified.UseVisualStyleBackColor = False
        '
        'pnl_ProviderFav
        '
        Me.pnl_ProviderFav.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_ProviderFav.Controls.Add(Me.btnProviderFav)
        Me.pnl_ProviderFav.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_ProviderFav.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_ProviderFav.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_ProviderFav.Location = New System.Drawing.Point(0, 533)
        Me.pnl_ProviderFav.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_ProviderFav.Name = "pnl_ProviderFav"
        Me.pnl_ProviderFav.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_ProviderFav.Size = New System.Drawing.Size(270, 32)
        Me.pnl_ProviderFav.TabIndex = 6
        '
        'btnProviderFav
        '
        Me.btnProviderFav.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnProviderFav.BackgroundImage = CType(resources.GetObject("btnProviderFav.BackgroundImage"), System.Drawing.Image)
        Me.btnProviderFav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnProviderFav.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProviderFav.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnProviderFav.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProviderFav.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProviderFav.Location = New System.Drawing.Point(0, 3)
        Me.btnProviderFav.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnProviderFav.Name = "btnProviderFav"
        Me.btnProviderFav.Size = New System.Drawing.Size(270, 29)
        Me.btnProviderFav.TabIndex = 0
        Me.btnProviderFav.Tag = "21"
        Me.btnProviderFav.Text = "Provider Favorites"
        Me.btnProviderFav.UseVisualStyleBackColor = False
        '
        'pnl_AllDrugs
        '
        Me.pnl_AllDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_AllDrugs.Controls.Add(Me.btnAllDrugs)
        Me.pnl_AllDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_AllDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_AllDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_AllDrugs.Location = New System.Drawing.Point(0, 501)
        Me.pnl_AllDrugs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_AllDrugs.Name = "pnl_AllDrugs"
        Me.pnl_AllDrugs.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_AllDrugs.Size = New System.Drawing.Size(270, 32)
        Me.pnl_AllDrugs.TabIndex = 5
        '
        'btnAllDrugs
        '
        Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnAllDrugs.BackgroundImage = CType(resources.GetObject("btnAllDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnAllDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAllDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAllDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAllDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllDrugs.Location = New System.Drawing.Point(0, 3)
        Me.btnAllDrugs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnAllDrugs.Name = "btnAllDrugs"
        Me.btnAllDrugs.Size = New System.Drawing.Size(270, 29)
        Me.btnAllDrugs.TabIndex = 0
        Me.btnAllDrugs.Tag = "11"
        Me.btnAllDrugs.Text = "All Drugs"
        Me.btnAllDrugs.UseVisualStyleBackColor = False
        '
        'pnl_PracticeFav
        '
        Me.pnl_PracticeFav.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_PracticeFav.Controls.Add(Me.btnPracticeFav)
        Me.pnl_PracticeFav.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_PracticeFav.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_PracticeFav.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_PracticeFav.Location = New System.Drawing.Point(0, 469)
        Me.pnl_PracticeFav.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_PracticeFav.Name = "pnl_PracticeFav"
        Me.pnl_PracticeFav.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_PracticeFav.Size = New System.Drawing.Size(270, 32)
        Me.pnl_PracticeFav.TabIndex = 4
        '
        'btnPracticeFav
        '
        Me.btnPracticeFav.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnPracticeFav.BackgroundImage = CType(resources.GetObject("btnPracticeFav.BackgroundImage"), System.Drawing.Image)
        Me.btnPracticeFav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPracticeFav.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPracticeFav.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPracticeFav.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPracticeFav.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPracticeFav.Location = New System.Drawing.Point(0, 3)
        Me.btnPracticeFav.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnPracticeFav.Name = "btnPracticeFav"
        Me.btnPracticeFav.Size = New System.Drawing.Size(270, 29)
        Me.btnPracticeFav.TabIndex = 0
        Me.btnPracticeFav.Tag = "12"
        Me.btnPracticeFav.Text = "Practice Favorites"
        Me.btnPracticeFav.UseVisualStyleBackColor = False
        '
        'pnl_ProviderFrequent
        '
        Me.pnl_ProviderFrequent.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_ProviderFrequent.Controls.Add(Me.btnProviderFrequent)
        Me.pnl_ProviderFrequent.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_ProviderFrequent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_ProviderFrequent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_ProviderFrequent.Location = New System.Drawing.Point(0, 437)
        Me.pnl_ProviderFrequent.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_ProviderFrequent.Name = "pnl_ProviderFrequent"
        Me.pnl_ProviderFrequent.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_ProviderFrequent.Size = New System.Drawing.Size(270, 32)
        Me.pnl_ProviderFrequent.TabIndex = 3
        '
        'btnProviderFrequent
        '
        Me.btnProviderFrequent.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnProviderFrequent.BackgroundImage = CType(resources.GetObject("btnProviderFrequent.BackgroundImage"), System.Drawing.Image)
        Me.btnProviderFrequent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnProviderFrequent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProviderFrequent.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnProviderFrequent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProviderFrequent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProviderFrequent.Location = New System.Drawing.Point(0, 3)
        Me.btnProviderFrequent.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnProviderFrequent.Name = "btnProviderFrequent"
        Me.btnProviderFrequent.Size = New System.Drawing.Size(270, 29)
        Me.btnProviderFrequent.TabIndex = 0
        Me.btnProviderFrequent.Tag = "20"
        Me.btnProviderFrequent.Text = "Provider Frequently Used "
        Me.btnProviderFrequent.UseVisualStyleBackColor = False
        '
        'pnl_Frequent
        '
        Me.pnl_Frequent.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Frequent.Controls.Add(Me.btnFrequent)
        Me.pnl_Frequent.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_Frequent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Frequent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Frequent.Location = New System.Drawing.Point(0, 405)
        Me.pnl_Frequent.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_Frequent.Name = "pnl_Frequent"
        Me.pnl_Frequent.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_Frequent.Size = New System.Drawing.Size(270, 32)
        Me.pnl_Frequent.TabIndex = 2
        '
        'btnFrequent
        '
        Me.btnFrequent.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnFrequent.BackgroundImage = CType(resources.GetObject("btnFrequent.BackgroundImage"), System.Drawing.Image)
        Me.btnFrequent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFrequent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFrequent.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnFrequent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFrequent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrequent.Location = New System.Drawing.Point(0, 3)
        Me.btnFrequent.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnFrequent.Name = "btnFrequent"
        Me.btnFrequent.Size = New System.Drawing.Size(270, 29)
        Me.btnFrequent.TabIndex = 0
        Me.btnFrequent.Tag = "13"
        Me.btnFrequent.Text = "Frequently Used Drugs"
        Me.btnFrequent.UseVisualStyleBackColor = False
        '
        'pnlMainSearch
        '
        Me.pnlMainSearch.Controls.Add(Me.pnlSearch)
        Me.pnlMainSearch.Controls.Add(Me.pnlbtnSearchFilter)
        Me.pnlMainSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainSearch.Location = New System.Drawing.Point(0, 3)
        Me.pnlMainSearch.Name = "pnlMainSearch"
        Me.pnlMainSearch.Size = New System.Drawing.Size(270, 26)
        Me.pnlMainSearch.TabIndex = 0
        '
        'pnlbtnSearchFilter
        '
        Me.pnlbtnSearchFilter.Controls.Add(Me.btnDrugStartContent)
        Me.pnlbtnSearchFilter.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlbtnSearchFilter.Location = New System.Drawing.Point(244, 0)
        Me.pnlbtnSearchFilter.Name = "pnlbtnSearchFilter"
        Me.pnlbtnSearchFilter.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.pnlbtnSearchFilter.Size = New System.Drawing.Size(26, 26)
        Me.pnlbtnSearchFilter.TabIndex = 49
        Me.pnlbtnSearchFilter.TabStop = True
        '
        'btnDrugStartContent
        '
        Me.btnDrugStartContent.BackColor = System.Drawing.Color.White
        Me.btnDrugStartContent.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnDrugStartContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugStartContent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDrugStartContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDrugStartContent.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDrugStartContent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDrugStartContent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDrugStartContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugStartContent.Image = CType(resources.GetObject("btnDrugStartContent.Image"), System.Drawing.Image)
        Me.btnDrugStartContent.Location = New System.Drawing.Point(3, 0)
        Me.btnDrugStartContent.Name = "btnDrugStartContent"
        Me.btnDrugStartContent.Size = New System.Drawing.Size(23, 26)
        Me.btnDrugStartContent.TabIndex = 48
        Me.btnDrugStartContent.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnDrugStartContent, "Search options")
        Me.btnDrugStartContent.UseVisualStyleBackColor = False
        '
        'pnlSearchFilter
        '
        Me.pnlSearchFilter.Controls.Add(Me.Panel1)
        Me.pnlSearchFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchFilter.Location = New System.Drawing.Point(0, 29)
        Me.pnlSearchFilter.Name = "pnlSearchFilter"
        Me.pnlSearchFilter.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlSearchFilter.Size = New System.Drawing.Size(270, 27)
        Me.pnlSearchFilter.TabIndex = 405555555
        Me.pnlSearchFilter.TabStop = True
        Me.pnlSearchFilter.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbContains)
        Me.Panel1.Controls.Add(Me.rbStartswith)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(270, 24)
        Me.Panel1.TabIndex = 0
        Me.Panel1.TabStop = True
        '
        'rbContains
        '
        Me.rbContains.AutoSize = True
        Me.rbContains.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbContains.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbContains.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.rbContains.Location = New System.Drawing.Point(171, 1)
        Me.rbContains.Name = "rbContains"
        Me.rbContains.Size = New System.Drawing.Size(71, 22)
        Me.rbContains.TabIndex = 44
        Me.rbContains.Text = "Contains"
        Me.rbContains.UseVisualStyleBackColor = True
        '
        'rbStartswith
        '
        Me.rbStartswith.AutoSize = True
        Me.rbStartswith.Checked = True
        Me.rbStartswith.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbStartswith.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbStartswith.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.rbStartswith.Location = New System.Drawing.Point(64, 1)
        Me.rbStartswith.Name = "rbStartswith"
        Me.rbStartswith.Size = New System.Drawing.Size(107, 22)
        Me.rbStartswith.TabIndex = 43
        Me.rbStartswith.TabStop = True
        Me.rbStartswith.Text = "Starts with   "
        Me.rbStartswith.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(1, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 22)
        Me.Label9.TabIndex = 45
        Me.Label9.Text = "Search : "
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 22)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(269, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 22)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(0, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(270, 1)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(270, 1)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "label1"
        '
        'pnl_PlannedDrugs
        '
        Me.pnl_PlannedDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_PlannedDrugs.Controls.Add(Me.btnPlannedDrugs)
        Me.pnl_PlannedDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_PlannedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_PlannedDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_PlannedDrugs.Location = New System.Drawing.Point(0, 597)
        Me.pnl_PlannedDrugs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_PlannedDrugs.Name = "pnl_PlannedDrugs"
        Me.pnl_PlannedDrugs.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_PlannedDrugs.Size = New System.Drawing.Size(270, 32)
        Me.pnl_PlannedDrugs.TabIndex = 655555556
        '
        'btnPlannedDrugs
        '
        Me.btnPlannedDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnPlannedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_LongButton
        Me.btnPlannedDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPlannedDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPlannedDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPlannedDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlannedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlannedDrugs.Location = New System.Drawing.Point(0, 3)
        Me.btnPlannedDrugs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnPlannedDrugs.Name = "btnPlannedDrugs"
        Me.btnPlannedDrugs.Size = New System.Drawing.Size(270, 29)
        Me.btnPlannedDrugs.TabIndex = 0
        Me.btnPlannedDrugs.Tag = "23"
        Me.btnPlannedDrugs.Text = "Planned Drugs"
        Me.btnPlannedDrugs.UseVisualStyleBackColor = False
        '
        'gloDrugListRevised
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlTreeView)
        Me.Controls.Add(Me.pnl_Frequent)
        Me.Controls.Add(Me.pnl_ProviderFrequent)
        Me.Controls.Add(Me.pnl_PracticeFav)
        Me.Controls.Add(Me.pnl_AllDrugs)
        Me.Controls.Add(Me.pnl_DrugList)
        Me.Controls.Add(Me.pnl_ProviderFav)
        Me.Controls.Add(Me.pnl_Classified)
        Me.Controls.Add(Me.pnl_PlannedDrugs)
        Me.Controls.Add(Me.pnlSearchFilter)
        Me.Controls.Add(Me.pnlMainSearch)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "gloDrugListRevised"
        Me.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Size = New System.Drawing.Size(270, 632)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.picProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTreeView.ResumeLayout(False)
        Me.cntListmenuStrip.ResumeLayout(False)
        Me.pnl_DrugList.ResumeLayout(False)
        Me.pnl_Classified.ResumeLayout(False)
        Me.pnl_ProviderFav.ResumeLayout(False)
        Me.pnl_AllDrugs.ResumeLayout(False)
        Me.pnl_PracticeFav.ResumeLayout(False)
        Me.pnl_ProviderFrequent.ResumeLayout(False)
        Me.pnl_Frequent.ResumeLayout(False)
        Me.pnlMainSearch.ResumeLayout(False)
        Me.pnlbtnSearchFilter.ResumeLayout(False)
        Me.pnlSearchFilter.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnl_PlannedDrugs.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    'Public WithEvents txtsearch As gloUserControlLibrary.gloSearchTextBox
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents pnlTreeView As System.Windows.Forms.Panel
    Friend WithEvents trvMain As System.Windows.Forms.TreeView
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents pnl_DrugList As System.Windows.Forms.Panel
    Protected WithEvents btnDrugList As System.Windows.Forms.Button
    Private WithEvents pnl_Classified As System.Windows.Forms.Panel
    Protected WithEvents btnClassified As System.Windows.Forms.Button
    Private WithEvents pnl_ProviderFrequent As System.Windows.Forms.Panel
    Protected WithEvents btnProviderFrequent As System.Windows.Forms.Button
    Private WithEvents pnl_PracticeFav As System.Windows.Forms.Panel
    Protected WithEvents btnPracticeFav As System.Windows.Forms.Button
    Private WithEvents pnl_AllDrugs As System.Windows.Forms.Panel
    Protected WithEvents btnAllDrugs As System.Windows.Forms.Button
    Private WithEvents pnl_ProviderFav As System.Windows.Forms.Panel
    Protected WithEvents btnProviderFav As System.Windows.Forms.Button
    Private WithEvents pnl_Frequent As System.Windows.Forms.Panel
    Protected WithEvents btnFrequent As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents ImgList As System.Windows.Forms.ImageList
    Private WithEvents picProgress As System.Windows.Forms.PictureBox
    Friend WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddDrugToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddProviderFavoriteDrugToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlMainSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnSearchFilter As System.Windows.Forms.Panel
    Friend WithEvents btnDrugStartContent As System.Windows.Forms.Button
    Friend WithEvents pnlSearchFilter As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbContains As System.Windows.Forms.RadioButton
    Friend WithEvents rbStartswith As System.Windows.Forms.RadioButton
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents pnl_PlannedDrugs As System.Windows.Forms.Panel
    Protected WithEvents btnPlannedDrugs As System.Windows.Forms.Button
    Friend WithEvents AddtoPlanMedMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
