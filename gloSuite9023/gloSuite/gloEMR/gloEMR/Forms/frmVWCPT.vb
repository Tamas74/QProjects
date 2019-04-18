Public Class frmVWCPT

    Inherits System.Windows.Forms.Form
    Public Shared blnModify As Boolean
    Public CategoryID As Int64
    Friend CategoryType As String
    Private objclsCPT As New clsCPT
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Dim _blnSearch As Boolean = True

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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                If (IsNothing(grdCPT) = False) Then
                    grdCPT.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdCPT)
                    grdCPT.Dispose()
                    grdCPT = Nothing
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
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents trvCategory As System.Windows.Forms.TreeView
    Friend WithEvents txtCategorySearch As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents grdCPT As System.Windows.Forms.DataGrid
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuAddCategory As System.Windows.Forms.MenuItem
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents imgCPT As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWCPT))
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.lblSearch = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.trvCategory = New System.Windows.Forms.TreeView
        Me.imgCPT = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.txtCategorySearch = New System.Windows.Forms.TextBox
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label
        Me.PicBx_Search = New System.Windows.Forms.PictureBox
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label
        Me.grdCPT = New System.Windows.Forms.DataGrid
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuAddCategory = New System.Windows.Forms.MenuItem
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlTopRight.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCPT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.txtSearch)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_RightBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_TopBrd)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(703, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(98, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "   Description :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(99, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(400, 22)
        Me.txtSearch.TabIndex = 0
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(701, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(702, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(703, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.Panel1)
        Me.pnlLeft.Controls.Add(Me.pnlSearch)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 84)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(184, 434)
        Me.pnlLeft.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.trvCategory)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(184, 408)
        Me.Panel1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 404)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(179, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 404)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(183, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 404)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(181, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'trvCategory
        '
        Me.trvCategory.BackColor = System.Drawing.Color.White
        Me.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCategory.ForeColor = System.Drawing.Color.Black
        Me.trvCategory.HideSelection = False
        Me.trvCategory.ImageIndex = 1
        Me.trvCategory.ImageList = Me.imgCPT
        Me.trvCategory.Location = New System.Drawing.Point(3, 0)
        Me.trvCategory.Name = "trvCategory"
        Me.trvCategory.SelectedImageIndex = 1
        Me.trvCategory.ShowLines = False
        Me.trvCategory.Size = New System.Drawing.Size(181, 405)
        Me.trvCategory.TabIndex = 0
        '
        'imgCPT
        '
        Me.imgCPT.ImageStream = CType(resources.GetObject("imgCPT.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgCPT.TransparentColor = System.Drawing.Color.Transparent
        Me.imgCPT.Images.SetKeyName(0, "CPT_01.ico")
        Me.imgCPT.Images.SetKeyName(1, "arrow_01.ico")
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
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(184, 26)
        Me.pnlSearch.TabIndex = 16
        '
        'txtCategorySearch
        '
        Me.txtCategorySearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCategorySearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCategorySearch.ForeColor = System.Drawing.Color.Black
        Me.txtCategorySearch.Location = New System.Drawing.Point(32, 5)
        Me.txtCategorySearch.Name = "txtCategorySearch"
        Me.txtCategorySearch.Size = New System.Drawing.Size(151, 15)
        Me.txtCategorySearch.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(151, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(151, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
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
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(179, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(179, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
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
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(183, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'grdCPT
        '
        Me.grdCPT.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdCPT.BackgroundColor = System.Drawing.Color.White
        Me.grdCPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCPT.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdCPT.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCPT.CaptionForeColor = System.Drawing.Color.White
        Me.grdCPT.CaptionVisible = False
        Me.grdCPT.DataMember = ""
        Me.grdCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdCPT.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdCPT.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdCPT.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCPT.HeaderForeColor = System.Drawing.Color.White
        Me.grdCPT.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdCPT.Location = New System.Drawing.Point(0, 0)
        Me.grdCPT.Name = "grdCPT"
        Me.grdCPT.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdCPT.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdCPT.ReadOnly = True
        Me.grdCPT.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdCPT.SelectionForeColor = System.Drawing.Color.Black
        Me.grdCPT.Size = New System.Drawing.Size(519, 431)
        Me.grdCPT.TabIndex = 0
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAddCategory})
        '
        'mnuAddCategory
        '
        Me.mnuAddCategory.Index = 0
        Me.mnuAddCategory.Text = "Add Category"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(709, 54)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(709, 54)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.grdCPT)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(187, 84)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(522, 434)
        Me.Panel2.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 430)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(517, 1)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 430)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(518, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 430)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(519, 1)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlTopRight)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 54)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(709, 30)
        Me.Panel3.TabIndex = 14
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(184, 84)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 434)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'frmVWCPT
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(709, 518)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWCPT"
        Me.Text = "CPT"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCPT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmVWCPT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Dim objCPT As New clsCPT
        Try
            grdCPT.DataSource = objclsCPT.GetAllCPT(CategoryID)
            If Not IsNothing(objclsCPT.GetDataview) Then
                objclsCPT.SortDataview(objclsCPT.GetDataview.Table.Columns(1).ColumnName)
            End If
            CustomGridStyle()
            CustomTreeView()
            '''''The following code is added by Anil on 26/09/2007 at 4:05 p.m.
            '''''This code gets the focus of first node of treeview on form load and grid is filled with the data against the selected node.
            Dim mynode As myTreeNode
            If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                trvCategory.SelectedNode = mynode
            End If
            '''''Upto here the code is added.
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '   objCPT = Nothing
        End Try
    End Sub
    Private Sub UpdateCPT()
        Dim ID As Long
        Dim objfrmCPT As frmMSTCPT
        ''Dim objCPT As New clsCPT
        Dim grdIndex As Integer
        Try
            If grdCPT.VisibleRowCount >= 1 Then
                blnModify = True
                ID = grdCPT.Item(grdCPT.CurrentRowIndex, 0).ToString
                grdIndex = grdCPT.CurrentRowIndex

                '''''Code is added by Anil 20071102
                Dim myDataView As DataView = CType(grdCPT.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim sortOrder As String = myDataView.Sort
                    Dim strSearchstring As String = txtSearch.Text.Trim
                    Dim arrcolumnsort() As String = Split(sortOrder, "]")
                    Dim strcolumnName As String = arrcolumnsort.GetValue(0)
                    Dim strsortorder As String = ""
                    If arrcolumnsort.Length > 1 Then
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If


                    ''''''''''''
                    objfrmCPT = New frmMSTCPT(ID)
                    objfrmCPT.Text = "Modify CPT"
                    objfrmCPT.ShowDialog(IIf(IsNothing(objfrmCPT.Parent), Me, objfrmCPT.Parent))
                    objfrmCPT.BringToFront()


                    If objfrmCPT.CancelClick = False Then
                        Dim dv As DataView
                        dv = objclsCPT.GetAllCPT(objfrmCPT._CategoryID)
                        grdCPT.DataSource = dv

                        Dim i As Integer
                        '' to Select TreeNode according to Category
                        For i = 0 To trvCategory.Nodes(0).GetNodeCount(False) - 1
                            Dim CategoryNode As myTreeNode
                            CategoryNode = trvCategory.Nodes(0).Nodes(i)
                            If objfrmCPT._CategoryID = CategoryNode.Key Then
                                trvCategory.SelectedNode = CategoryNode
                                Exit For
                            End If
                        Next

                        '    For i = 0 To dv.Table.Rows.Count - 1
                        '        If ID = grdCPT.Item(i, 0) Then
                        '            grdCPT.CurrentRowIndex = i
                        '            grdCPT.Select(i)
                        '            Exit For
                        '        End If
                        '    Next

                        'Else
                        '    grdCPT.Select(grdIndex)
                        'End If

                        '''''Code line below is added by Anil on 20071102
                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        Dim myDatagView As DataView = CType(grdCPT.DataSource, DataView)
                        If (IsNothing(myDatagView) = False) Then



                            For i = 0 To myDatagView.Count - 1
                                If ID = grdCPT.Item(i, 0) Then
                                    grdCPT.CurrentRowIndex = i
                                    grdCPT.Select(i)
                                    Exit For
                                End If
                            Next
                        End If
                    
                    End If
                    objfrmCPT.Dispose()
                    objfrmCPT = Nothing
                End If
              
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmCPT = Nothing
        End Try
    End Sub
    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Dim dv As DataView
        dv = objclsCPT.GetDataview
        Dim ts As New clsDataGridTableStyle(dv.Table.TableName)
        'Dim objclsCPT As New clsCPT

        'objclsCPT.SortDataview(dv.Table.Columns(2).ColumnName)

        Dim IDCol As New DataGridTextBoxColumn
        IDCol.Width = 0
        IDCol.MappingName = dv.Table.Columns(0).ColumnName
        IDCol.HeaderText = "ID"

        Dim CPTCodeCol As New DataGridTextBoxColumn
        With CPTCodeCol
            .MappingName = dv.Table.Columns(1).ColumnName
            .HeaderText = "CPT Code"
            .NullText = ""
            .Width = 0.33 * grdCPT.Width
        End With

        Dim DescCol As New DataGridTextBoxColumn
        With DescCol
            .Width = 0.33 * grdCPT.Width
            .MappingName = dv.Table.Columns(2).ColumnName
            .HeaderText = "Description"
            .NullText = ""
        End With

        Dim SpecialityCol As New DataGridTextBoxColumn
        With SpecialityCol
            .Width = 0.33 * grdCPT.Width
            .MappingName = dv.Table.Columns(3).ColumnName
            .HeaderText = "Specialty"
            .NullText = ""
        End With
        'Dim CategoryCol As New DataGridTextBoxColumn
        'With CategoryCol
        '    .Width = 150
        '    .MappingName = objclsCPT.GetDataview.Table.Columns(4).ColumnName
        '    .HeaderText = "Category"
        '    .NullText=""
        'End With


        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, CPTCodeCol, DescCol, SpecialityCol})
        grdCPT.TableStyles.Clear()
        grdCPT.TableStyles.Add(ts)

        '''''''Code is added by Anil on 02/11/2007
        txtSearch.Text = ""
        txtSearch.Text = strsearchtxt
        If strcolumnName = "" Then
            objclsCPT.SortDataview(dv.Table.Columns(1).ColumnName)
        Else
            Dim strColumn As String = Replace(strcolumnName, "[", "")

            objclsCPT.SortDataview(strColumn, strSortBy)
        End If
        ''''''''''''''''''''''''''''''''
    End Sub

    Private Sub grdcpt_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdCPT.CurrentCellChanged
        ''Try
        ''    Select Case grdCPT.CurrentCell.ColumnNumber
        ''        Case 1
        ''            txtSearch.Text = ""
        ''            lblSearch.Text = "CPT Code"
        ''        Case 2
        ''            txtSearch.Text = ""
        ''            lblSearch.Text = "Description"
        ''        Case 3
        ''            txtSearch.Text = ""
        ''            lblSearch.Text = "Specialty"
        ''    End Select
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(grdCPT.DataSource(), DataView)

                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                grdCPT.DataSource = dvPatient
                Dim strPatientSearchDetails As String
                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                Select Case Trim(lblSearch.Text)
                    Case "CPT Code"

                        '''''Code Modified by Anil on 24/09/2007 at 12:30 p.m.
                        '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
                        '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
                        '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If
                    Case "Description"
                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If
                    Case "Specialty"
                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If

                        '''''Upto here the changes are made by Anil on 24/09/2007 at 12:30 p.m.

                End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CustomTreeView()
        'Dim newNode As New TreeNode
        'Dim objclsCPT As New clsCPT
        Dim objMyTreeView As myTreeNode
        Dim objchild As myTreeNode
        Dim i As Integer
        Try
            trvCategory.Nodes.Clear()

            objMyTreeView = New myTreeNode("Category", 0)
            objMyTreeView.ImageIndex = 0
            objMyTreeView.SelectedImageIndex = 0
            trvCategory.Nodes.Add(objMyTreeView)

            Dim dt As DataTable
            dt = objclsCPT.GetAllCategory()
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
            trvCategory.ExpandAll()

            If IsNothing(objclsCPT.GetDataview) = False Then
                objclsCPT.SortDataview(objclsCPT.GetDataview.Table.Columns(1).ColumnName)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub trvCategory_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCategory.AfterSelect
        Try
            Dim objMyTreeView As myTreeNode
            'Dim objCPT As New clsCPT
            objMyTreeView = CType(e.Node, myTreeNode)

            CategoryID = objMyTreeView.Key
            CategoryType = objMyTreeView.Text
            grdCPT.DataSource = objclsCPT.GetAllCPT(CategoryID)
            If Not IsNothing(objclsCPT.GetDataview) Then
                objclsCPT.SortDataview(objclsCPT.GetDataview.Table.Columns(1).ColumnName)
            End If
            grdCPT.CaptionText = "CPT - " & CategoryType
            lblSearch.Text = "Description"
            txtSearch.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub lnkLblAddCategory_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    '    'Dim objCategory As New CategoryMaster(ID)
    '    'Dim objCategory As New CategoryMaster
    '    'objCategory.ShowDialog(Me)
    '    'CustomTreeView()
    'End Sub

    ''Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    ''    'Dim objCPT As New clsCPT
    ''    'Dim dv As DataView
    ''    'dv = 

    ''    'objclsCPT.Search(grdCPT.DataSource, 1, txtSearch.Text)

    ''    Try
    ''        If Not IsNothing(objclsCPT.GetDataview) Then
    ''            Dim Search As String
    ''            Search = Replace(Trim(txtSearch.Text), "'", "''")

    ''            objclsCPT.SetRowFilter(Trim(Search))
    ''            'HideColumn()
    ''        End If
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.Message, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try
    ''    'dv = grdCPT.DataSource
    ''    'dv.RowFilter = "sCPTCode Like '%" & txtSearch.Text & "%'"
    ''    'grdCPT.DataSource = dv
    ''End Sub

    Private Sub mnuAddCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddCategory.Click
        Try
            Dim objCategory As New CategoryMaster
            objCategory.ShowDialog(IIf(IsNothing(objCategory.Parent), Me, objCategory.Parent))
            objCategory.Dispose()
            objCategory = Nothing
            CustomTreeView()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdCPT_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCPT.MouseUp
        If grdCPT.CurrentRowIndex >= 0 Then
            grdCPT.Select(grdCPT.CurrentRowIndex)
        End If
    End Sub

    Private Sub grdCPT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles grdCPT.KeyPress
        If grdCPT.CurrentRowIndex >= 0 Then
            grdCPT.Select(grdCPT.CurrentRowIndex)

            If (e.KeyChar = ChrW(13)) Then
                UpdateCPT()
            End If
        End If
    End Sub
    ''Private Sub grdCPT_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdCPT.KeyUp
    ''    If grdCPT.CurrentRowIndex >= 0 Then
    ''        grdCPT.Select(grdCPT.CurrentRowIndex)
    ''    End If
    ''End Sub

    Private Sub grdCPT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdCPT.KeyDown
        If grdCPT.CurrentRowIndex >= 0 Then
            grdCPT.Select(grdCPT.CurrentRowIndex)
        End If
    End Sub

    Private Sub grdCPT_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCPT.DoubleClick
        ''Dim ID As Long
        ''Dim objfrmCPT As frmMSTCPT
        ''''' Dim objCPT As New clsCPT
        '' UpdateCPT()

        ''Try
        ''    If grdCPT.VisibleRowCount >= 1 Then
        ''        blnModify = True
        ''        ID = grdCPT.Item(grdCPT.CurrentRowIndex, 0).ToString
        ''        objfrmCPT = New frmMSTCPT(ID)
        ''        objfrmCPT.Text = "Modify CPT"
        ''        objfrmCPT.ShowDialog(Me)
        ''        objfrmCPT.BringToFront()
        ''        If objfrmCPT.CancelClick = False Then
        ''            grdCPT.DataSource = objclsCPT.GetAllCPT(CategoryID)
        ''            If Not IsNothing(objclsCPT.GetDataview) Then
        ''                objclsCPT.SortDataview(objclsCPT.GetDataview.Table.Columns(2).ColumnName)
        ''            End If
        ''        End If
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    objfrmCPT = Nothing
        ''End Try
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
                            'trvCategory.HideSelection = False
                            txtCategorySearch.Focus()
                            Exit Sub
                        Else
                            'trvCategory.HideSelection = True
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdCPT.CurrentRowIndex >= 0 Then
                    grdCPT.Select(0)
                    grdCPT.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.Message, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub txtCategorySearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCategorySearch.KeyPress
        If trvCategory.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvCategory.Select()
                'Else
                '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
            End If
        End If
    End Sub

    Private Sub frmVWCPT_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub
    ''''''''Code/Event is added by Anil on 20071102
    Private Sub grdCPT_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdCPT.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdCPT.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                Select Case htInfo.Column
                    Case 1
                        txtSearch.Text = ""
                        lblSearch.Text = "CPT Code"
                    Case 2
                        txtSearch.Text = ""
                        lblSearch.Text = "Description"
                    Case 3
                        txtSearch.Text = ""
                        lblSearch.Text = "Specialty"
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
                UpdateCPT()
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''''''''''''''''''
    ''Code Added by Shilpa for adding the new buttons on 13th Nov 2007
    Private Sub AddCategory()
        Dim objfrmCPT As New frmMSTCPT(CategoryID, CategoryType)
        ' Dim objCPT As New clsCPT

        Try
            blnModify = False
            objfrmCPT.Text = "Add New CPT"
            ' objfrmCPT.Fill_Category()
            objfrmCPT.ShowDialog(IIf(IsNothing(objfrmCPT.Parent), Me, objfrmCPT.Parent))

            If objfrmCPT.CancelClick = False Then
                grdCPT.DataSource = objclsCPT.GetAllCPT(objfrmCPT._CategoryID)
                If Not IsNothing(objclsCPT.GetDataview) Then
                    objclsCPT.SortDataview(objclsCPT.GetDataview.Table.Columns(1).ColumnName)
                End If
                '''''
                'Dim dv As DataView
                'dv = objclsCPT.GetAllCPT(CategoryID)

                'grdCPT.DataSource = dv
                Dim myDataType As DataView = CType(grdCPT.DataSource, DataView)
                If (IsNothing(myDataType) = False) Then


                    Dim i As Integer
                    For i = 0 To myDataType.Table.Rows.Count - 1
                        If objfrmCPT._CPTCode = grdCPT.Item(i, 1) Then
                            grdCPT.CurrentRowIndex = i
                            grdCPT.Select(i)
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmCPT.Dispose()
            objfrmCPT = Nothing
            '      objfrmCPT = Nothing
        End Try
    End Sub
    Private Sub UpdateCategory()
        ''Dim ID As Long
        ''Dim objfrmCPT As frmMSTCPT
        ''''' Dim objCPT As New clsCPT

        UpdateCPT()

        ''Try
        ''    If grdCPT.VisibleRowCount >= 1 Then
        ''        blnModify = True
        ''        ID = grdCPT.Item(grdCPT.CurrentRowIndex, 0).ToString
        ''        objfrmCPT = New frmMSTCPT(ID)
        ''        objfrmCPT.Text = "Modify CPT"
        ''        objfrmCPT.ShowDialog(Me)
        ''        objfrmCPT.BringToFront()
        ''        If objfrmCPT.CancelClick = False Then
        ''            grdCPT.DataSource = objclsCPT.GetAllCPT(CategoryID)
        ''            If Not IsNothing(objclsCPT.GetDataview) Then
        ''                objclsCPT.SortDataview(objclsCPT.GetDataview.Table.Columns(2).ColumnName)
        ''            End If
        ''        End If
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    objfrmCPT = Nothing
        ''End Try
    End Sub
    Private Sub DeleteCategory()
        Dim ID As Long
        Dim CPTCode As String
        'Dim objclsCPT As New clsCPT
        Try
            If grdCPT.VisibleRowCount >= 1 Then
                'blnModify = True
                If MessageBox.Show("Are you sure you want to delete this CPT code?", "CPT", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    ID = grdCPT.Item(grdCPT.CurrentRowIndex, 0).ToString
                    CPTCode = grdCPT.Item(grdCPT.CurrentRowIndex, 1).ToString
                    objclsCPT.DeleteCPT(ID, CPTCode)
                    grdCPT.DataSource = objclsCPT.GetAllCPT(CategoryID)
                    If Not IsNothing(objclsCPT.GetDataview) Then
                        objclsCPT.SortDataview(objclsCPT.GetDataview.Table.Columns(1).ColumnName)
                    End If
                    ''''''Code is Added by Anil 0n 20071102
                    Dim myDataView As DataView = CType(grdCPT.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        Dim sortOrder As String = myDataView.Sort
                        Dim strSearchstring As String = txtSearch.Text.Trim
                        Dim arrcolumnsort() As String = Split(sortOrder, "]")
                        Dim strcolumnName As String = arrcolumnsort.GetValue(0)
                        Dim strsortorder As String = ""
                        If arrcolumnsort.Length > 1 Then
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If

                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                    End If
                    ''''''''''''''''''
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'objclsCPT = Nothing
        End Try
    End Sub
    Private Sub RefreshCategory()
        '  Dim objCPT As New clsCPT
        Try
            grdCPT.DataSource = objclsCPT.GetAllCPT(CategoryID)
            If Not IsNothing(objclsCPT.GetDataview) Then
                objclsCPT.SortDataview(objclsCPT.GetDataview.Table.Columns(1).ColumnName)
            End If
            CustomGridStyle()
            '''''Following code lines are addded by Anil 0n 28/09/07 at 03:40 p.m.
            '''''This code clears the search textboxes
            txtSearch.Clear()
            txtCategorySearch.Clear()
            _blnSearch = True
            '''''This code gets the focus of first node of treeview and grid is filled with the data against the selected node.
            Dim mynode As myTreeNode
            If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                trvCategory.SelectedNode = mynode
            End If
            '''''added upto here
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' objCPT = Nothing
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
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

    'Test Comment
End Class
