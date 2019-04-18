Public Class frmVWTemplateGallary
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
    Dim _nLoginProviderId = 0
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents GloUC_trvCategory As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        ''Sandip Darade 21 st feb 09
        ''Providerid retrievedro  set the login provider
        If appSettings("ProviderID") IsNot Nothing Then
            If appSettings("ProviderID") <> "" Then
                _nLoginProviderId = Convert.ToInt64(appSettings("ProviderID"))
            Else
                _nLoginProviderId = 0
            End If
        Else
            _nLoginProviderId = 0
        End If

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                If (IsNothing(grdTemplateGallery) = False) Then
                    grdTemplateGallery.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdTemplateGallery)
                    grdTemplateGallery.Dispose()
                    grdTemplateGallery = Nothing
                End If
            Catch ex As Exception

            End Try
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
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents trvCategory As System.Windows.Forms.TreeView
    Friend WithEvents txtCategorySearch As System.Windows.Forms.TextBox
    Friend WithEvents grdTemplateGallery As System.Windows.Forms.DataGrid
    Friend WithEvents pnlTopRight1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbProviders As System.Windows.Forms.ComboBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents imgTemplates As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWTemplateGallary))
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLeftTop = New System.Windows.Forms.Panel()
        Me.GloUC_trvCategory = New gloUserControlLibrary.gloUC_TreeView()
        Me.imgTemplates = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.trvCategory = New System.Windows.Forms.TreeView()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtCategorySearch = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.grdTemplateGallery = New System.Windows.Forms.DataGrid()
        Me.pnlTopRight1 = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbProviders = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTemplateGallery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTopRight1.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Controls.Add(Me.pnlSearch)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 83)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(264, 435)
        Me.pnlLeft.TabIndex = 2
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.Controls.Add(Me.GloUC_trvCategory)
        Me.pnlLeftTop.Controls.Add(Me.Panel2)
        Me.pnlLeftTop.Controls.Add(Me.trvCategory)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Size = New System.Drawing.Size(264, 435)
        Me.pnlLeftTop.TabIndex = 1
        '
        'GloUC_trvCategory
        '
        Me.GloUC_trvCategory.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvCategory.CheckBoxes = False
        Me.GloUC_trvCategory.CodeMember = Nothing
        Me.GloUC_trvCategory.Comment = Nothing
        Me.GloUC_trvCategory.ConceptID = Nothing
        Me.GloUC_trvCategory.CPT = Nothing
        Me.GloUC_trvCategory.DescriptionMember = Nothing
        Me.GloUC_trvCategory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvCategory.DrugFlag = CType(16, Short)
        Me.GloUC_trvCategory.DrugFormMember = Nothing
        Me.GloUC_trvCategory.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvCategory.DurationMember = Nothing
        Me.GloUC_trvCategory.FrequencyMember = Nothing
        Me.GloUC_trvCategory.HistoryType = Nothing
        Me.GloUC_trvCategory.ICD9 = Nothing
        Me.GloUC_trvCategory.ImageIndex = 1
        Me.GloUC_trvCategory.ImageList = Me.imgTemplates
        Me.GloUC_trvCategory.ImageObject = Nothing
        Me.GloUC_trvCategory.Indicator = Nothing
        Me.GloUC_trvCategory.IsDrug = False
        Me.GloUC_trvCategory.IsNarcoticsMember = Nothing
        Me.GloUC_trvCategory.IsSystemCategory = Nothing
        Me.GloUC_trvCategory.Location = New System.Drawing.Point(0, 28)
        Me.GloUC_trvCategory.MaximumNodes = 1000
        Me.GloUC_trvCategory.Name = "GloUC_trvCategory"
        Me.GloUC_trvCategory.NDCCodeMember = Nothing
        Me.GloUC_trvCategory.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.GloUC_trvCategory.ParentImageIndex = 0
        Me.GloUC_trvCategory.ParentMember = Nothing
        Me.GloUC_trvCategory.RouteMember = Nothing
        Me.GloUC_trvCategory.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvCategory.SearchBox = True
        Me.GloUC_trvCategory.SearchText = Nothing
        Me.GloUC_trvCategory.SelectedImageIndex = 1
        Me.GloUC_trvCategory.SelectedNode = Nothing
        Me.GloUC_trvCategory.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCategory.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvCategory.SelectedParentImageIndex = 0
        Me.GloUC_trvCategory.Size = New System.Drawing.Size(264, 407)
        Me.GloUC_trvCategory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCategory.TabIndex = 51
        Me.GloUC_trvCategory.Tag = Nothing
        Me.GloUC_trvCategory.UnitMember = Nothing
        Me.GloUC_trvCategory.ValueMember = Nothing
        '
        'imgTemplates
        '
        Me.imgTemplates.ImageStream = CType(resources.GetObject("imgTemplates.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTemplates.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTemplates.Images.SetKeyName(0, "")
        Me.imgTemplates.Images.SetKeyName(1, "")
        Me.imgTemplates.Images.SetKeyName(2, "")
        Me.imgTemplates.Images.SetKeyName(3, "")
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(264, 28)
        Me.Panel2.TabIndex = 52
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(261, 25)
        Me.Panel3.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(1, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(259, 23)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Category"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(260, 1)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "label1"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(260, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 24)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(0, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(261, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'trvCategory
        '
        Me.trvCategory.BackColor = System.Drawing.Color.White
        Me.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCategory.ForeColor = System.Drawing.Color.Black
        Me.trvCategory.HideSelection = False
        Me.trvCategory.ImageIndex = 0
        Me.trvCategory.ImageList = Me.imgTemplates
        Me.trvCategory.ItemHeight = 21
        Me.trvCategory.Location = New System.Drawing.Point(3, 115)
        Me.trvCategory.Name = "trvCategory"
        Me.trvCategory.SelectedImageIndex = 1
        Me.trvCategory.ShowLines = False
        Me.trvCategory.Size = New System.Drawing.Size(190, 291)
        Me.trvCategory.TabIndex = 0
        Me.trvCategory.Visible = False
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtCategorySearch)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(193, 26)
        Me.pnlSearch.TabIndex = 0
        Me.pnlSearch.Visible = False
        '
        'txtCategorySearch
        '
        Me.txtCategorySearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCategorySearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCategorySearch.ForeColor = System.Drawing.Color.Black
        Me.txtCategorySearch.Location = New System.Drawing.Point(32, 5)
        Me.txtCategorySearch.Name = "txtCategorySearch"
        Me.txtCategorySearch.Size = New System.Drawing.Size(160, 15)
        Me.txtCategorySearch.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(160, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 1
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(160, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 0
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 22)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(188, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(188, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 3
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(192, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'grdTemplateGallery
        '
        Me.grdTemplateGallery.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdTemplateGallery.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdTemplateGallery.BackgroundColor = System.Drawing.Color.White
        Me.grdTemplateGallery.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdTemplateGallery.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdTemplateGallery.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTemplateGallery.CaptionForeColor = System.Drawing.Color.White
        Me.grdTemplateGallery.CaptionVisible = False
        Me.grdTemplateGallery.DataMember = ""
        Me.grdTemplateGallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTemplateGallery.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTemplateGallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdTemplateGallery.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdTemplateGallery.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdTemplateGallery.HeaderForeColor = System.Drawing.Color.White
        Me.grdTemplateGallery.LinkColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdTemplateGallery.Location = New System.Drawing.Point(0, 0)
        Me.grdTemplateGallery.Name = "grdTemplateGallery"
        Me.grdTemplateGallery.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.grdTemplateGallery.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdTemplateGallery.ReadOnly = True
        Me.grdTemplateGallery.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdTemplateGallery.SelectionForeColor = System.Drawing.Color.Black
        Me.grdTemplateGallery.Size = New System.Drawing.Size(456, 432)
        Me.grdTemplateGallery.TabIndex = 0
        '
        'pnlTopRight1
        '
        Me.pnlTopRight1.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight1.Controls.Add(Me.btnClear)
        Me.pnlTopRight1.Controls.Add(Me.Label12)
        Me.pnlTopRight1.Controls.Add(Me.txtSearch)
        Me.pnlTopRight1.Controls.Add(Me.lblSearch)
        Me.pnlTopRight1.Controls.Add(Me.Label10)
        Me.pnlTopRight1.Controls.Add(Me.cmbProviders)
        Me.pnlTopRight1.Controls.Add(Me.Label1)
        Me.pnlTopRight1.Controls.Add(Me.Label7)
        Me.pnlTopRight1.Controls.Add(Me.Label9)
        Me.pnlTopRight1.Controls.Add(Me.Label8)
        Me.pnlTopRight1.Controls.Add(Me.Label6)
        Me.pnlTopRight1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight1.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight1.Name = "pnlTopRight1"
        Me.pnlTopRight1.Size = New System.Drawing.Size(720, 24)
        Me.pnlTopRight1.TabIndex = 1
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(680, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(676, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(4, 22)
        Me.Label12.TabIndex = 11
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(483, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(193, 22)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(353, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(130, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(300, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 22)
        Me.Label10.TabIndex = 10
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbProviders
        '
        Me.cmbProviders.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProviders.ForeColor = System.Drawing.Color.Black
        Me.cmbProviders.Location = New System.Drawing.Point(79, 1)
        Me.cmbProviders.Name = "cmbProviders"
        Me.cmbProviders.Size = New System.Drawing.Size(221, 22)
        Me.cmbProviders.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 22)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "  Provider :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 22)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(719, 1)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(719, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 23)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(0, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(720, 1)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label2"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(726, 53)
        Me.pnlToolStrip.TabIndex = 0
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_gloCommunityDownload, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(726, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
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
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_gloCommunityDownload
        '
        Me.ts_gloCommunityDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_gloCommunityDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_gloCommunityDownload.Image = CType(resources.GetObject("ts_gloCommunityDownload.Image"), System.Drawing.Image)
        Me.ts_gloCommunityDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_gloCommunityDownload.Name = "ts_gloCommunityDownload"
        Me.ts_gloCommunityDownload.Size = New System.Drawing.Size(73, 50)
        Me.ts_gloCommunityDownload.Tag = "gloCommunityDownload"
        Me.ts_gloCommunityDownload.Text = "Download"
        Me.ts_gloCommunityDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_gloCommunityDownload.ToolTipText = "Download from gloCommunity"
        Me.ts_gloCommunityDownload.Visible = False
        '
        'ts_btnRefresh
        '
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
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.grdTemplateGallery)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(267, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(459, 435)
        Me.Panel1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 431)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(454, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 431)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(455, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 431)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(456, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'pnlTopRight
        '
        Me.pnlTopRight.Controls.Add(Me.pnlTopRight1)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopRight.Location = New System.Drawing.Point(0, 53)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTopRight.Size = New System.Drawing.Size(726, 30)
        Me.pnlTopRight.TabIndex = 1
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(264, 83)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 435)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'frmVWTemplateGallary
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(726, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlTopRight)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWTemplateGallary"
        Me.Text = "Templates"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTemplateGallery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTopRight1.ResumeLayout(False)
        Me.pnlTopRight1.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public CategoryID As Int64
    Dim grdIndex As Integer
    Public mycaller As MainMenu
    '' when Select For Update Remember it in ID ()
    Dim ID As Long = 0

    Friend CategoryType As String
    Public Shared blnModify As Boolean
    'Public Shared nProviderID As Long
    Dim objTemplateGallery As New clsTemplateGallery
    Dim _blnSearch As Boolean = True

    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    ''code added to avoid flickering -pradeep(03062011)
    ''http://www.ms-windows.info/Help/flicker-free-painting-11696.aspx -link referred
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

#Region " Form Load Event "

    Private Sub frmView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Dim objTemplateGallery As New clsTemplateGallery
        Try

            FillProvider()
            grdTemplateGallery.Enabled = False
            grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
            grdTemplateGallery.Enabled = True
            If IsNothing(objTemplateGallery.GetDataview) = False Then
                objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)
            End If
            'CustomGridStyle()
            CustomTreeView()

            'CType(Me.ParentForm, MainMenu).pnlMenu.Visible = False
            'CType(Me.ParentForm, MainMenu).pnlLeft.Visible = False
            'CType(Me.ParentForm, MainMenu).Splitter1.Visible = False

            'Code Start added by kanchan on 20120102 for gloCommunity integration
            If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
                ts_gloCommunityDownload.Visible = gblngloCommunity
            End If
            'Code end added by kanchan on 20120102 for gloCommunity integration
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '  Finally
            '  objTemplateGallery = Nothing
        End Try
    End Sub
    'To reslove grid resizing problem
    Private Sub frmVWTemplateGallary_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        CustomGridStyle()
    End Sub
#End Region

    Private Sub FillProvider()
        Dim dt As DataTable
        dt = objTemplateGallery.GetAllProvider()
        If Not IsNothing(dt) Then
            '' Here we add "All"(indicating All Doctors / Providers) 
            '' To datatable dt which contains Provider Name & ID's 

            Dim objrow As DataRow
            objrow = dt.NewRow
            objrow.Item(0) = 0
            objrow.Item(1) = "All"
            dt.Rows.Add(objrow)

            '' Attach DataSource to  CmbProvider 
            cmbProviders.DataSource = dt
            cmbProviders.DisplayMember = dt.Columns(1).ColumnName 'Provider Name
            cmbProviders.ValueMember = dt.Columns(0).ColumnName 'Provider ID
            ''cmbProviders.SelectedValue = 0

            ''  20080923
            'cmbProviders.Text = gstrLoginProviderName

            ''Sandip Darade  21st feb 09
            ''set  the login provider  as selected
            If (_nLoginProviderId > 0) Then

                cmbProviders.SelectedValue = _nLoginProviderId
            Else
                cmbProviders.Text = "All"
            End If

        End If
    End Sub



    Private Sub UpdateTemplate()

        'By Shweta 20090827
        If CheckWordForException() = False Then
            Exit Sub
        End If
        'End Shweta


        ' Dim objTemplateGallery As New clsTemplateGallery
        Dim objfrmTemplateGallery As frmTemplateGallery
        Try

            If grdTemplateGallery.VisibleRowCount >= 1 Then
                blnModify = True

                ID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0).ToString
                grdIndex = grdTemplateGallery.CurrentRowIndex
                ''''''Code is Added by Anil 0n 20071102
                Dim myDataView As DataView = CType(grdTemplateGallery.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    sortOrder = myDataView.Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                End If

                ''''''''''''''''''
                objfrmTemplateGallery = New frmTemplateGallery(ID)
                With objfrmTemplateGallery
                    .Text = "Modify Template"
                    .mycaller = Me
                    .MdiParent = Me.ParentForm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .Show()
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()

                    'CType(Me.ParentForm, MainMenu).pnlRights.Visible = False
                    If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                        Try
                            '' 20070613
                            'mycaller.tlbbtn_Microphone.Visible = True
                        Catch ex As Exception

                        End Try
                    End If

                End With
                ' CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            'Finally
            '  objfrmTemplateGallery = Nothing
        End Try
    End Sub
    Public Sub RefreshGrid_old()
        ' Dim objTemplateGallery As New clsTemplateGallery
        txtSearch.Text = ""
        txtCategorySearch.Text = ""
        Try
            If IsNothing(cmbProviders.SelectedValue) = False Then
                'If blnModify = False Then
                grdTemplateGallery.Enabled = False
                grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
                grdTemplateGallery.Enabled = True
                'Else
                '    grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(frmTemplateGallery._CategoryID, cmbProviders.SelectedValue)
                'End If
                objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)

                Dim i As Integer

                '' to Select TreeNode according to Category
                For i = 0 To trvCategory.Nodes(0).GetNodeCount(False) - 1
                    Dim CategoryNode As myTreeNode
                    CategoryNode = trvCategory.Nodes(0).Nodes(i)
                    If frmTemplateGallery._CategoryID = CategoryNode.Key Then
                        trvCategory.SelectedNode = CategoryNode
                        Exit For
                    End If
                Next
                CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                '''' To Remember the Selection of Row 
                Dim myDataView As DataView = CType(grdTemplateGallery.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    For i = 0 To myDataView.Count - 1
                        '''' when Template Name Found select that matching Row
                        If frmTemplateGallery._TemplateName = grdTemplateGallery.Item(i, 1) Then
                            grdTemplateGallery.CurrentRowIndex = i
                            grdTemplateGallery.Select(i)
                            Exit For
                        End If
                    Next
                End If
                'End If
            End If
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Finally
            '  objTemplateGallery = Nothing
        End Try
    End Sub
    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        ' Dim objclsTemplateGallery As New clsTemplateGallery
        Dim dv As DataView
        dv = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
        If (IsNothing(dv)) Then
            Exit Sub
        End If
        Dim ts As New clsDataGridTableStyle(dv.Table.TableName)
        objTemplateGallery.SortDataview(dv.Table.Columns(1).ColumnName)

        Dim IDCol As New DataGridTextBoxColumn
        IDCol.Width = 0
        IDCol.MappingName = dv.Table.Columns(0).ColumnName
        IDCol.HeaderText = "ID"

        Dim TemplateGalleryNameCol As New DataGridTextBoxColumn
        With TemplateGalleryNameCol
            .Width = grdTemplateGallery.Width / 3 '175
            .MappingName = dv.Table.Columns(1).ColumnName
            .HeaderText = "Template Name"
            .NullText = ""
        End With

        Dim CategoryCol As New DataGridTextBoxColumn
        With CategoryCol
            .Width = grdTemplateGallery.Width / 3 '150
            .MappingName = dv.Table.Columns(2).ColumnName
            .HeaderText = "Category"
            .NullText = ""
        End With

        Dim ProviderCol As New DataGridTextBoxColumn
        With ProviderCol
            .Width = (grdTemplateGallery.Width / 3.5) '200
            .MappingName = dv.Table.Columns(3).ColumnName
            .HeaderText = "Provider"
            .NullText = ""
        End With
        '''''''Code is added by Anil on 02/11/2007
        txtSearch.Text = ""
        txtSearch.Text = strsearchtxt
        If strcolumnName = "" Then
            objTemplateGallery.SortDataview(dv.Table.Columns(1).ColumnName)
        Else
            Dim strColumn As String = Replace(strcolumnName, "[", "")

            objTemplateGallery.SortDataview(strColumn, strSortBy)
        End If
        ''''''''''''''''''''''''''''''''
        'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, TemplateGalleryNameCol, CategoryCol, ProviderCol, DescriptionCol})
        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, TemplateGalleryNameCol, CategoryCol, ProviderCol})
        ' ts.ColumnHeadersVisible = False
        grdTemplateGallery.TableStyles.Clear()
        grdTemplateGallery.TableStyles.Add(ts)
        If (grdTemplateGallery.VisibleRowCount > 0) Then
            grdTemplateGallery.Select(0)
        End If


        grdTemplateGallery.CurrentRowIndex = 0

    End Sub

    Private Sub grdTemplateGallery_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTemplateGallery.CurrentCellChanged
        ''''''Commented by Anil on 20071102
        ''Try
        ''    Select Case grdTemplateGallery.CurrentCell.ColumnNumber
        ''        Case 1
        ''            lblSearch.Text = "Template Name"
        ''        Case 2
        ''            lblSearch.Text = "Category"
        ''        Case 3
        ''            lblSearch.Text = "Provider"
        ''    End Select
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView
            dvPatient = CType(grdTemplateGallery.DataSource(), DataView)
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            'grdTemplateGallery.Enabled = False
            'grdTemplateGallery.DataSource = dvPatient
            'grdTemplateGallery.Enabled = True
            'COMMENTED BY SUDHIR 20090202 - SHIFTED ABOVE 3 LINES DOWN AFTER SELECT 
            Dim strPatientSearchDetails As String
            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If
            'shubhangi 20090930 Apply general search
            dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' "




            'COMMENTED BY SHUBHANGI 20090930 APPLY GENERAL SEARCH
            'Select Case Trim(lblSearch.Text)
            '    '''''Code Modified by Anil on 24/09/2007 at 3:10 p.m.
            '    '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
            '    '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
            '    '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

            '    Case "Template Name :"
            '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '        ''Else
            '        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '        ''End If
            '    Case "Category :"
            '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '        ''Else
            '        ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '        ''End If
            '    Case "Provider :"
            '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '        dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '        ''Else
            '        ''dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '        ''End If
            '        '''''Upto here the changes are made by Anil on 24/09/2007 at 3:10 p.m.
            'End Select
            grdTemplateGallery.Enabled = False
            grdTemplateGallery.DataSource = dvPatient
            grdTemplateGallery.Enabled = True
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CustomTreeView_old()

        Dim newNode As New TreeNode
        Dim objMyTreeView As myTreeNode
        Dim objchild As myTreeNode
        Dim dt As DataTable = objTemplateGallery.GetAllCategory
        Dim i As Integer
        trvCategory.Nodes.Clear()

        objMyTreeView = New myTreeNode("Category", 0)
        objMyTreeView.ImageIndex = 0
        objMyTreeView.SelectedImageIndex = 0
        trvCategory.Nodes.Add(objMyTreeView)

        If IsNothing(dt) = False Then
            For i = 0 To dt.Rows.Count - 1
                Dim ValueMember As Int64
                Dim DisplayMember As String
                ValueMember = dt.Rows(i)(0)
                DisplayMember = dt.Rows(i)(1)
                objchild = New myTreeNode(DisplayMember, ValueMember)
                objchild.ImageIndex = 1
                objchild.SelectedImageIndex = 1
                objMyTreeView.Nodes.Add(objchild)
            Next
            trvCategory.SelectedNode = trvCategory.Nodes(0).FirstNode
            '''''''This if condition is added by Anil on 09/10/2007 Because it was giving error for empty treeview

            If IsNothing(trvCategory.SelectedNode) Then
                Exit Sub
            Else
                Dim firstNode As myTreeNode
                firstNode = CType(trvCategory.SelectedNode, myTreeNode)
                CategoryID = firstNode.Key  'objMyTreeView.Key
                CategoryType = firstNode.Text ' objMyTreeView.Text
                grdTemplateGallery.Enabled = False
                grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
                grdTemplateGallery.Enabled = True
                If Not IsNothing(objTemplateGallery.GetDataview) Then
                    objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)
                End If
            End If
        End If

        ''Sandip Darade 24 Feb 09 
        ''To set  SOAP as selected categotry
        For Each n As TreeNode In trvCategory.Nodes(0).Nodes

            If (n.Text = "SOAP") Then
                trvCategory.SelectedNode = n
                Exit For
            End If

        Next

        trvCategory.ExpandAll()
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdTemplateGallery.CurrentRowIndex >= 0 Then
                    grdTemplateGallery.Select(0)
                    grdTemplateGallery.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdTemplateGallery_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTemplateGallery.MouseUp
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdTemplateGallery.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                grdTemplateGallery.Select(htInfo.Row)
            Else
                Exit Sub
            End If


            'If grdTemplateGallery.CurrentRowIndex >= 0 Then
            '    grdTemplateGallery.Select(grdTemplateGallery.CurrentRowIndex)
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVWTemplateGallary_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            'If CType(Me.MdiParent, MainMenu).pnlLeft.Visible = False Then
            '    CType(Me.MdiParent, MainMenu).Splitter1.Visible = False
            'End If
            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            Me.WindowState = FormWindowState.Maximized '' SUDHIR 20090810 '' WINDOW MAXIMIZE BUG ''

        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmbProviders_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProviders.SelectionChangeCommitted
        If cmbProviders.Items.Count > 0 Then
            RefreshGrid()
        End If
    End Sub

    Private Sub txtCategorySearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCategorySearch.TextChanged
        Try
            If Trim(txtCategorySearch.Text) <> "" Then
                If trvCategory.Nodes(0).GetNodeCount(False) > 0 Then
                    Dim mychildnode As TreeNode
                    'child node collection

                    For Each mychildnode In trvCategory.Nodes(0).Nodes
                        Dim str As String
                        str = UCase(Trim(mychildnode.Text))
                        If Mid(str, 1, Len(Trim(txtCategorySearch.Text))) = UCase(Trim(txtCategorySearch.Text)) Then
                            '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                            If Not IsNothing(trvCategory.SelectedNode) Then
                                If Not IsNothing(trvCategory.SelectedNode.LastNode) Then
                                    trvCategory.SelectedNode = trvCategory.SelectedNode.LastNode
                                End If
                            End If
                            '*************
                            trvCategory.SelectedNode = mychildnode
                            txtCategorySearch.Focus()
                            Exit Sub
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCategorySearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCategorySearch.KeyPress
        Try
            If trvCategory.GetNodeCount(False) > 0 Then
                If (e.KeyChar = ChrW(13)) Then
                    trvCategory.Select()
                    'Else
                    '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvCategory_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCategory.AfterSelect
        Try
            If IsNothing(e.Node) = False Then
                Dim objMyTreeView As myTreeNode
                objMyTreeView = CType(e.Node, myTreeNode)

                CategoryID = objMyTreeView.Key
                frmTemplateGallery._CategoryID = objMyTreeView.Key
                CategoryType = objMyTreeView.Text
                grdTemplateGallery.Enabled = False
                grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
                grdTemplateGallery.Enabled = True
                If Not IsNothing(objTemplateGallery.GetDataview) Then
                    objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub trvCategory_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvCategory.NodeMouseClick
    '    Try
    '        Dim objMyTreeView As myTreeNode
    '        objMyTreeView = CType(e.Node, myTreeNode)

    '        CategoryID = objMyTreeView.Key
    '        CategoryType = objMyTreeView.Text
    '        grdTemplateGallery.Enabled = False
    '        grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
    '        grdTemplateGallery.Enabled = True
    '        If Not IsNothing(objTemplateGallery.GetDataview) Then
    '            objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub grdTemplateGallery_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTemplateGallery.MouseDown
    '    'Dim ptPoint As Point = New Point(e.X, e.Y)
    '    'Dim htInfo As DataGrid.HitTestInfo = grdTemplateGallery.HitTest(ptPoint)
    '    'If htInfo.Type = DataGrid.HitTestType.Cell Then
    '    '    grdTemplateGallery.Select(htInfo.Row)
    '    'Else
    '    '    Exit Sub
    '    'End If
    'End Sub

    Private Sub grdTemplateGallery_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTemplateGallery.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdTemplateGallery.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
                '    Select Case htInfo.Column
                '        Case 1
                '            lblSearch.Text = "Template Name :"
                '        Case 2
                '            lblSearch.Text = "Category :"
                '        Case 3
                '            lblSearch.Text = "Provider :"
                '    End Select

                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                UpdateTemplate()
                'RefreshGrid()
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AddCategory()

        'By Shweta 20090827
        If CheckWordForException() = False Then
            Exit Sub
        End If
        'End Shweta

        Dim objfrmTemplateGallery As New frmTemplateGallery(CategoryType, cmbProviders.SelectedValue)
        ' Dim objTemplateGallery As New clsTemplateGallery
        Try
            blnModify = False
            objfrmTemplateGallery.Text = "Add New Template"

            '  objfrmTemplateGallery.MdiParent = Me.ParentForm
            objfrmTemplateGallery.mycaller = Me

            objfrmTemplateGallery.MdiParent = Me.ParentForm
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

            objfrmTemplateGallery.WindowState = FormWindowState.Maximized

            objfrmTemplateGallery.Show()

            'CType(Me.ParentForm, MainMenu).pnlLeft.Visible = True
            'CType(Me.ParentForm, MainMenu).pnlRights.Visible = False
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                Try
                    '' 20070613
                    'mycaller.tlbbtn_Microphone.Visible = True
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Finally
            '  objfrmTemplateGallery = Nothing
        End Try
    End Sub
    Private Sub UpdateCategory()
        If grdTemplateGallery.VisibleRowCount >= 1 Then
            If grdTemplateGallery.IsSelected(grdTemplateGallery.CurrentRowIndex) = False Then
                Exit Sub
            End If
            UpdateTemplate()
        End If
    End Sub
    Private Sub DeleteCategory()

        txtSearch.Focus()

        Dim ID As Long
        Dim TemplateName As String
        Dim bIsTemplateUsedinEducation = False
        Dim sMessage As String

        '' Dim objclsTemplateGallery As New clsTemplateGallery
        Try
            '18-May-15 Aniket: Resolving Bug #83386: EMR: Temlate- Question mark should be at the end of message
            sMessage = "Do you want to delete the selected template?"

            If grdTemplateGallery.VisibleRowCount >= 1 Then

                If IsNothing(GloUC_trvCategory.SelectedNode) = False Then
                    Dim objMyTreeView As New gloUserControlLibrary.myTreeNode
                    objMyTreeView = CType(GloUC_trvCategory.SelectedNode, gloUserControlLibrary.myTreeNode)

                    If objMyTreeView.Text <> String.Empty Then
                        If Convert.ToString(objMyTreeView.Text).Trim = "Patient Reference Material" Or Convert.ToString(objMyTreeView.Text).Trim = "Provider Reference Material" Then
                            ID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0).ToString
                            If ID <> 0 Then
                                bIsTemplateUsedinEducation = objTemplateGallery.IsTemplateUsedInPatientEducation(ID)
                                If bIsTemplateUsedinEducation Then
                                    sMessage = "Template is used in Education Material Association.Do you want to delete selected template ?"
                                End If
                            End If

                        End If
                    End If
                End If

                If grdTemplateGallery.IsSelected(grdTemplateGallery.CurrentRowIndex) = False Then
                    Exit Sub
                End If

                'blnModify = True
                If MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    ID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 0).ToString
                    TemplateName = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 1).ToString
                    objTemplateGallery.DeleteTemplateGallery(ID, TemplateName)

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, "Template (" + Trim(TemplateName) + ") Deleted", 0, ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    grdTemplateGallery.Enabled = False
                    grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
                    grdTemplateGallery.Enabled = True
                    objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)

                    ''''''Code is Added by Anil 0n 20071102
                    Dim myDataView As DataView = CType(grdTemplateGallery.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = myDataView.Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        ''''''''''''''''''
                    End If
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Finally
            '   objclsTemplateGallery = Nothing
        End Try
    End Sub
    Private Sub RefreshCategory_old()

        Dim objTemplateGallery As New clsTemplateGallery
        txtSearch.Text = ""
        txtCategorySearch.Text = ""
        Try
            If IsNothing(cmbProviders.SelectedValue) = False Then
                'If blnModify = False Then
                grdTemplateGallery.Enabled = False
                grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
                grdTemplateGallery.Enabled = True
                'Else
                '    grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(frmTemplateGallery._CategoryID, cmbProviders.SelectedValue)
                'End If
                objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)
                CustomTreeView()
                CustomGridStyle()
                '''''Following code lines are addded by Anil 0n 26/09/07 at 12:55 p.m.
                '''''This code clears the search textboxes, gets the focus on the root of theTreeView and clears the grid on click of Refresh button.
                txtSearch.Clear()
                txtCategorySearch.Clear()
                _blnSearch = True
                'grdTemplateGallery.DataSource = Nothing
                'trvCategory.CollapseAll()
                'trvCategory.Focus()
                'trvCategory.ExpandAll()
                Dim mynode As myTreeNode
                If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
                    mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                    trvCategory.SelectedNode = mynode
                End If

                '''''added upto here
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objTemplateGallery = Nothing
        End Try
        ''RefreshGrid()
    End Sub
    Private Sub FormClose()
        Try
            Me.Close()
        Catch ex As Exception
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

    Private Sub grdTemplateGallery_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles grdTemplateGallery.Navigate

    End Sub
    ''Sandip Darade 20090624
    ''existing methods modified after tree view control implementation
    ''And added treeview control events

#Region "tree view control implementation"

    Private Sub CustomTreeView()

        Dim dt As DataTable = objTemplateGallery.GetAllCategory

        If IsNothing(dt) = False Then

            GloUC_trvCategory.DataSource = dt
            GloUC_trvCategory.DescriptionMember = Convert.ToString(dt.Columns("sDescription").ColumnName)
            GloUC_trvCategory.ValueMember = Convert.ToString(dt.Columns("nCategoryID").ColumnName)
            GloUC_trvCategory.Tag = Convert.ToString(dt.Columns("nCategoryID").ColumnName)
            GloUC_trvCategory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
            GloUC_trvCategory.FillTreeView()

        End If

        Dim n As New TreeNode
        For Each n In GloUC_trvCategory.Nodes

            If (n.Text = "SOAP") Then
                GloUC_trvCategory.SelectedNode = n

                Exit For
            End If
        Next
    End Sub

    Public Sub RefreshGrid()
        ' Dim objTemplateGallery As New clsTemplateGallery

        Try
            If IsNothing(cmbProviders.SelectedValue) = False Then
                'If blnModify = False Then
                grdTemplateGallery.Enabled = False
                grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
                grdTemplateGallery.Enabled = True
                'Else
                '    grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(frmTemplateGallery._CategoryID, cmbProviders.SelectedValue)
                'End If
                objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)

                Dim i As Integer


                Dim oNode As New TreeNode
                For Each oNode In GloUC_trvCategory.Nodes
                    If frmTemplateGallery._CategoryID = oNode.Tag Then
                        GloUC_trvCategory.SelectedNode = oNode
                        Exit For
                    End If
                Next

                CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                '''' To Remember the Selection of Row 
                Dim myDataView As DataView = CType(grdTemplateGallery.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    For i = 0 To myDataView.Count - 1
                        '''' when Template Name Found select that matching Row
                        If frmTemplateGallery._TemplateName = grdTemplateGallery.Item(i, 1) Then
                            grdTemplateGallery.CurrentRowIndex = i
                            grdTemplateGallery.UnSelect(0)
                            grdTemplateGallery.Select(i)
                            Exit For
                        End If
                    Next
                End If
                'End If
            End If
            'End If
            grdTemplateGallery.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Finally
            '  objTemplateGallery = Nothing
        End Try
    End Sub

    Private Sub RefreshCategory()

        Dim objTemplateGallery As New clsTemplateGallery
        txtSearch.Text = ""
        txtCategorySearch.Text = ""
        Try
            If IsNothing(cmbProviders.SelectedValue) = False Then
                grdTemplateGallery.Enabled = False
                grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
                grdTemplateGallery.Enabled = True

                objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)
                CustomTreeView()
                CustomGridStyle()

                'If (GloUC_trvCategory.Nodes.Count > 0) Then
                '    GloUC_trvCategory.SelectedNode = GloUC_trvCategory.Nodes(0)
                'End If

                ''Sandip Darade 
                ''make SOAP as selected category
                Dim n As New TreeNode
                For Each n In GloUC_trvCategory.Nodes

                    If (n.Text = "SOAP") Then
                        GloUC_trvCategory.SelectedNode = n

                        Exit For
                    End If
                Next
                cmbProviders.Focus()
                '''''added upto here
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objTemplateGallery = Nothing
        End Try
        ''RefreshGrid()
    End Sub

    Private Sub GloUC_trvCategory_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles GloUC_trvCategory.AfterSelect
        Try
            If IsNothing(e.Node) = False Then
                Dim objMyTreeView As New gloUserControlLibrary.myTreeNode
                objMyTreeView = CType(e.Node, gloUserControlLibrary.myTreeNode)

                CategoryID = objMyTreeView.Tag
                frmTemplateGallery._CategoryID = objMyTreeView.Tag
                CategoryType = objMyTreeView.Text
                grdTemplateGallery.Enabled = False
                'grdTemplateGallery.DataSource = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)

                Dim dv As New DataView
                Dim dt As New DataTable

                dv = objTemplateGallery.GetAllTemplateGallery(CategoryID, cmbProviders.SelectedValue)
                grdTemplateGallery.DataSource = dv



                grdTemplateGallery.Enabled = True


                If Not IsNothing(objTemplateGallery.GetDataview) Then
                    objTemplateGallery.SortDataview(objTemplateGallery.GetDataview.Table.Columns(1).ColumnName)
                End If

                'Resolved issue 10350
                dt = dv.ToTable()
                If (dt.Rows.Count >= 1) Then
                    grdTemplateGallery.Select(0)
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANgI 20090930
        'Use to clear search text box.
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("Template", "Download")
            'code added by seema on 20120221 to prevent opening of multiple windows
            If gloCommunity.Classes.clsGeneral.getInstance(FrmgloCommunityViewData.Name, FrmgloCommunityViewData.Text) = False Then
                Try

                    With FrmgloCommunityViewData
                        .MdiParent = Application.OpenForms("MainMenu")
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With

                Catch objErr As Exception
                    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If
        End If
    End Sub

    ''Added for fixed Bug # : 35658 on 20120904
    Private Function CheckUser() As Boolean
        Dim oCommunity As gloCommunity.Classes.clsgloCommunityUsers = Nothing
        Dim _blnUserCheck As Boolean = False
        Try
            oCommunity = New gloCommunity.Classes.clsgloCommunityUsers()
            _blnUserCheck = oCommunity.CheckAuthentication()
        Catch ex As Exception
        Finally
            If Not IsNothing(oCommunity) Then
                oCommunity = Nothing
            End If
        End Try
        Return _blnUserCheck
    End Function
    ''End
End Class
