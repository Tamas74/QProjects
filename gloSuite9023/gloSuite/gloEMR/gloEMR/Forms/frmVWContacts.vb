Imports gloContacts

Public Class frmVWContacts
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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
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
    Friend WithEvents dgContactsList As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As System.Windows.Forms.ToolStrip
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnView As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ViewContacts As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents imgContacts As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWContacts))
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.trvCategory = New System.Windows.Forms.TreeView
        Me.imgContacts = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.txtCategorySearch = New System.Windows.Forms.TextBox
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label
        Me.PicBx_Search = New System.Windows.Forms.PictureBox
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label
        Me.dgContactsList = New System.Windows.Forms.DataGrid
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New System.Windows.Forms.ToolStrip
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnView = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ViewContacts = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlTopRight.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgContactsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.txtSearch)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(679, 24)
        Me.pnlTopRight.TabIndex = 0
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(109, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(400, 22)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(108, 20)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "   Middle Name :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(677, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
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
        Me.Label7.Location = New System.Drawing.Point(678, 1)
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
        Me.Label8.Size = New System.Drawing.Size(679, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.Panel1)
        Me.pnlLeft.Controls.Add(Me.pnlSearch)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 83)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(231, 435)
        Me.pnlLeft.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Controls.Add(Me.trvCategory)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(231, 409)
        Me.Panel1.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 405)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(226, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 405)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(230, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 405)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(228, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'trvCategory
        '
        Me.trvCategory.BackColor = System.Drawing.Color.White
        Me.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCategory.ForeColor = System.Drawing.Color.Black
        Me.trvCategory.HideSelection = False
        Me.trvCategory.ImageIndex = 1
        Me.trvCategory.ImageList = Me.imgContacts
        Me.trvCategory.Location = New System.Drawing.Point(3, 0)
        Me.trvCategory.Name = "trvCategory"
        Me.trvCategory.SelectedImageIndex = 1
        Me.trvCategory.ShowLines = False
        Me.trvCategory.Size = New System.Drawing.Size(228, 406)
        Me.trvCategory.TabIndex = 1
        '
        'imgContacts
        '
        Me.imgContacts.ImageStream = CType(resources.GetObject("imgContacts.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgContacts.TransparentColor = System.Drawing.Color.Transparent
        Me.imgContacts.Images.SetKeyName(0, "Contact.ico")
        Me.imgContacts.Images.SetKeyName(1, "Bullet06.ico")
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
        Me.pnlSearch.Size = New System.Drawing.Size(231, 26)
        Me.pnlSearch.TabIndex = 0
        '
        'txtCategorySearch
        '
        Me.txtCategorySearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCategorySearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCategorySearch.ForeColor = System.Drawing.Color.Black
        Me.txtCategorySearch.Location = New System.Drawing.Point(32, 5)
        Me.txtCategorySearch.Name = "txtCategorySearch"
        Me.txtCategorySearch.Size = New System.Drawing.Size(198, 15)
        Me.txtCategorySearch.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(198, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(198, 2)
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
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(226, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(226, 1)
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
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(230, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'dgContactsList
        '
        Me.dgContactsList.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgContactsList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgContactsList.BackgroundColor = System.Drawing.Color.White
        Me.dgContactsList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgContactsList.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgContactsList.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgContactsList.CaptionForeColor = System.Drawing.Color.White
        Me.dgContactsList.CaptionVisible = False
        Me.dgContactsList.DataMember = ""
        Me.dgContactsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgContactsList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgContactsList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgContactsList.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgContactsList.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgContactsList.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgContactsList.HeaderForeColor = System.Drawing.Color.White
        Me.dgContactsList.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgContactsList.Location = New System.Drawing.Point(1, 4)
        Me.dgContactsList.Name = "dgContactsList"
        Me.dgContactsList.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgContactsList.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgContactsList.ReadOnly = True
        Me.dgContactsList.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgContactsList.SelectionForeColor = System.Drawing.Color.Black
        Me.dgContactsList.Size = New System.Drawing.Size(446, 427)
        Me.dgContactsList.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(685, 53)
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnView, Me.ts_btnRefresh, Me.ViewContacts, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(685, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.BackColor = System.Drawing.Color.Transparent
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
        Me.ts_btnModify.BackColor = System.Drawing.Color.Transparent
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
        Me.ts_btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnView
        '
        Me.ts_btnView.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnView.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnView.Image = CType(resources.GetObject("ts_btnView.Image"), System.Drawing.Image)
        Me.ts_btnView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnView.Name = "ts_btnView"
        Me.ts_btnView.Size = New System.Drawing.Size(48, 50)
        Me.ts_btnView.Tag = "View"
        Me.ts_btnView.Text = " &View "
        Me.ts_btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnView.Visible = False
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ViewContacts
        '
        Me.ViewContacts.Image = CType(resources.GetObject("ViewContacts.Image"), System.Drawing.Image)
        Me.ViewContacts.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ViewContacts.Name = "ViewContacts"
        Me.ViewContacts.Size = New System.Drawing.Size(99, 50)
        Me.ViewContacts.Tag = "ViewContacts"
        Me.ViewContacts.Text = "View Contacts"
        Me.ViewContacts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.BackColor = System.Drawing.Color.Transparent
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
        Me.Panel2.Controls.Add(Me.dgContactsList)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(234, 83)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(451, 435)
        Me.Panel2.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 431)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(446, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 428)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(447, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 428)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(448, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlTopRight)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 53)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(685, 30)
        Me.Panel3.TabIndex = 1
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(231, 83)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 435)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'frmVWContacts
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(685, 518)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWContacts"
        Me.Text = "Contacts"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgContactsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim strcontacttype As String
    Dim key As Int64
    Private ContactDBLayer As New ClsContactDBLayer(True)
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String

    Private Sub frmVWContacts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            lblSearch.Text = "Last Name"       '''''Added by Anil on 01/10/2007 at 5:50 p.m. to give name to the label for search at form load. 
            FillTreeView()
            trvCategory.ExpandAll()

            Dim mynode As myTreeNode
            If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                trvCategory.SelectedNode = mynode
            End If
            '''''Added on 20071120 by Anil
            If dgContactsList.VisibleRowCount > 0 Then
                dgContactsList.CurrentRowIndex = 0
                dgContactsList.Select(0)
            End If
            ''''''''''''''
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BindGrid(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")

        ContactDBLayer.FetchData(strcontacttype)
        dgContactsList.SetDataBinding(ContactDBLayer.DsDataview, "")

        If key = 1 Or key = 2 Or key = 3 Or key = 30 Or key = 301 Or key = 302 Or key = 303 Or key = 4 Then
            ContactDBLayer.ContactType = False
            ContactDBLayer.SortDataview(ContactDBLayer.DsDataview.Table.Columns("Name").ColumnName)
            lblSearch.Text = "Name"          '''''' Code Line added by Anil on 01/10/2007 to give name to the search Label at node change.i.e.,The "Name" text is given to the label to set it as a default  label for search on grid load.
        ElseIf key = 0 Then
            ContactDBLayer.ContactType = True
            ContactDBLayer.SortDataview(ContactDBLayer.DsDataview.Table.Columns("LastName").ColumnName)
            lblSearch.Text = "Last Name"          '''''' Code Line added by Anil on 01/10/2007 to give name to the search Label at node change.i.e.,The "Last Name" text is given to the label to set it as a default  label for search on grid load.
        End If
        HideColumn()

    End Sub
    Private Sub HideColumn()
        'Dim ts As DataGridTableStyle = New DataGridTableStyle
        Dim ts As New clsDataGridTableStyle(ContactDBLayer.DsDataview.Table.TableName)
        Dim dgID As New DataGridTextBoxColumn
        With dgID
            .MappingName = ContactDBLayer.DsDataview.Table.Columns(0).ColumnName
            .Alignment = HorizontalAlignment.Center
            .Width = 0
        End With
        If ContactDBLayer.ContactType = False Then
            Dim dgCol1 As New DataGridTextBoxColumn
            With dgCol1
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(1).ColumnName
                .HeaderText = "Name"
                .NullText = ""
                .Width = 0.166 * dgContactsList.Width
            End With

            Dim dgCol2 As New DataGridTextBoxColumn
            With dgCol2
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(2).ColumnName
                .HeaderText = "Contact"
                .NullText = ""
                .Width = 0.166 * dgContactsList.Width
            End With

            Dim dgCol3 As New DataGridTextBoxColumn
            With dgCol3
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(3).ColumnName
                .HeaderText = "Phone"
                .NullText = ""
                .Width = 0.166 * dgContactsList.Width
            End With

            Dim dgCol4 As New DataGridTextBoxColumn
            With dgCol4
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(4).ColumnName
                .HeaderText = "Mobile"
                .NullText = ""
                .Width = 0.166 * dgContactsList.Width
            End With

            Dim dgCol5 As New DataGridTextBoxColumn
            With dgCol5
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(5).ColumnName
                .HeaderText = "Email"
                .NullText = ""
                .Width = 0.166 * dgContactsList.Width
            End With

            Dim dgCol6 As New DataGridTextBoxColumn
            With dgCol6
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(6).ColumnName
                .HeaderText = "URL"
                .NullText = ""
                .Width = 0
            End With

            Dim dgCol7 As New DataGridTextBoxColumn
            With dgCol7
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(7).ColumnName
                .HeaderText = "Fax"
                .NullText = ""
                .Width = 0.166 * dgContactsList.Width - 10
            End With

            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3, dgCol4, dgCol5, dgCol6, dgCol7})
        Else
            Dim dgCol1 As New DataGridTextBoxColumn
            '''''''Column is added by Anil on 20071122, for new field "Degree" to be shown as a Fourth column in a grid.
            With dgCol1
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(1).ColumnName
                .HeaderText = "First Name"
                .NullText = ""
                .Width = 0.142 * dgContactsList.Width
            End With
            '''''''
            Dim dgCol2 As New DataGridTextBoxColumn
            With dgCol2
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(2).ColumnName
                .HeaderText = "Middle Name"
                .NullText = ""
                .Width = 0.142 * dgContactsList.Width
            End With
            Dim dgCol3 As New DataGridTextBoxColumn
            With dgCol3
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(3).ColumnName
                .HeaderText = "Last Name"
                .NullText = ""
                .Width = 0.142 * dgContactsList.Width
            End With

            Dim dgCol4 As New DataGridTextBoxColumn
            With dgCol4
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(9).ColumnName
                .HeaderText = "Degree"
                .NullText = ""
                .Width = 0.13 * dgContactsList.Width
            End With

            Dim dgCol5 As New DataGridTextBoxColumn
            With dgCol5
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(4).ColumnName
                .HeaderText = "Phone"
                .NullText = ""
                .Width = 0.142 * dgContactsList.Width
            End With

            Dim dgCol6 As New DataGridTextBoxColumn
            With dgCol6
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(5).ColumnName
                .HeaderText = "Mobile"
                .NullText = ""
                .Width = 0.142 * dgContactsList.Width
            End With

            Dim dgCol7 As New DataGridTextBoxColumn
            With dgCol7
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(6).ColumnName
                .HeaderText = "Email"
                .NullText = ""
                .Width = 0.142 * dgContactsList.Width
            End With

            Dim dgCol8 As New DataGridTextBoxColumn
            With dgCol8
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(7).ColumnName
                .HeaderText = "URL"
                .NullText = ""
                .Width = 0
            End With

            Dim dgCol9 As New DataGridTextBoxColumn
            With dgCol9
                .MappingName = ContactDBLayer.DsDataview.Table.Columns(8).ColumnName
                .HeaderText = "Fax"
                .NullText = ""
                .Width = 0.142 * dgContactsList.Width - 10
            End With
            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3, dgCol4, dgCol5, dgCol6, dgCol7, dgCol8, dgCol9})
        End If
        dgContactsList.TableStyles.Clear()
        dgContactsList.TableStyles.Add(ts)
    End Sub
    Private Sub dgContactsList_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgContactsList.CurrentCellChanged
        '''''''''''Commented by Anil on 01/11/2007
        ''Try
        ''    If ContactDBLayer.ContactType = False Then
        ''        Select Case dgContactsList.CurrentCell.ColumnNumber
        ''            Case 1
        ''                lblSearch.Text = "Name"
        ''            Case 2
        ''                lblSearch.Text = "Contact"
        ''            Case 3
        ''                lblSearch.Text = "Phone"
        ''            Case 4
        ''                lblSearch.Text = "Mobile"
        ''            Case 5
        ''                lblSearch.Text = "Email"
        ''            Case 7
        ''                lblSearch.Text = "Fax"
        ''        End Select

        ''    Else
        ''        Select Case dgContactsList.CurrentCell.ColumnNumber
        ''            Case 1
        ''                lblSearch.Text = "First Name"
        ''            Case 2
        ''                lblSearch.Text = "Middle Name"
        ''            Case 3
        ''                lblSearch.Text = "Last Name"
        ''            Case 4
        ''                lblSearch.Text = "Phone"
        ''            Case 5
        ''                lblSearch.Text = "Mobile"
        ''            Case 6
        ''                lblSearch.Text = "Email"
        ''            Case 8
        ''                lblSearch.Text = "Fax"
        ''        End Select

        ''    End If
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        ''''''This IF statement is added by Anil on 01/11/2007
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(dgContactsList.DataSource(), DataView)

                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                dgContactsList.DataSource = dvPatient
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
                    Case "Name"
                        '''''Code Modified by Anil on 24/09/2007 at 3:20 p.m.
                        '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
                        '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
                        '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If
                    Case "Contact"

                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If

                    Case "Phone"
                        If ContactDBLayer.ContactType = False Then
                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                            ''Else
                            ''dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''End If
                        Else
                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                            ''Else
                            ''dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''End If
                        End If
                    Case "Mobile"
                        If ContactDBLayer.ContactType = False Then
                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                            ''Else
                            ''dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''End If
                        Else
                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(5).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                            ''Else
                            ''dvPatient.RowFilter = dvPatient.Table.Columns(5).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''End If
                        End If

                    Case "Email"
                        If ContactDBLayer.ContactType = False Then
                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(5).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                            ''Else
                            ''dvPatient.RowFilter = dvPatient.Table.Columns(5).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''End If
                        Else
                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(6).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                            ''Else
                            ''dvPatient.RowFilter = dvPatient.Table.Columns(6).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''End If
                        End If

                    Case "Fax"
                        If ContactDBLayer.ContactType = False Then
                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(7).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                            ''Else
                            ''dvPatient.RowFilter = dvPatient.Table.Columns(7).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''End If
                        Else
                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(8).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                            ''Else
                            ''Dim str As String
                            ''str = dvPatient.Table.Columns(8).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''dvPatient.RowFilter = str
                            ''End If
                        End If

                    Case "First Name"
                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If
                    Case "Middle Name"
                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If
                    Case "Last Name"
                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If

                        '''''Upto here the changes are made by Anil on 24/09/2007 at 3:20 p.m.


                        ''--------Added by Anil on 20071122
                    Case "Degree"
                        dvPatient.RowFilter = dvPatient.Table.Columns(9).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''----------------------


                End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub FillTreeView()
        'Dim dc As DataTable
        Dim mynode As myTreeNode
        '  Dim mysubchildnode As myTreeNode
        Dim myRequestNode As myTreeNode

        mynode = New myTreeNode("Contacts", -1)
        mynode.ImageIndex = 0
        mynode.SelectedImageIndex = 0
        trvCategory.Nodes.Add(mynode)

        Dim mychildnode As myTreeNode
        mychildnode = New myTreeNode("Physician", 0)
        mychildnode.ImageIndex = 1
        mychildnode.SelectedImageIndex = 1
        mynode.Nodes.Add(mychildnode)

        mychildnode = New myTreeNode("Hospital", 1)
        mychildnode.ImageIndex = 1
        mychildnode.SelectedImageIndex = 1
        mynode.Nodes.Add(mychildnode)

        mychildnode = New myTreeNode("Insurance", 2)
        mychildnode.ImageIndex = 1
        mychildnode.SelectedImageIndex = 1
        mynode.Nodes.Add(mychildnode)

        mychildnode = New myTreeNode("Pharmacy", 3)
        mychildnode.ImageIndex = 1
        mychildnode.SelectedImageIndex = 1
        mynode.Nodes.Add(mychildnode)

        mychildnode = New myTreeNode("e Pharmacy", 30)
        mychildnode.ImageIndex = 1
        mychildnode.SelectedImageIndex = 1
        mynode.Nodes.Add(mychildnode)

        myRequestNode = New myTreeNode("New Rx", 301)
        myRequestNode.ImageIndex = 1
        myRequestNode.SelectedImageIndex = 1
        mychildnode.Nodes.Add(myRequestNode)

        myRequestNode = New myTreeNode("New Rx & Refill Request", 302)
        myRequestNode.ImageIndex = 1
        myRequestNode.SelectedImageIndex = 1
        mychildnode.Nodes.Add(myRequestNode)

        myRequestNode = New myTreeNode("Other", 303)
        myRequestNode.ImageIndex = 1
        myRequestNode.SelectedImageIndex = 1
        mychildnode.Nodes.Add(myRequestNode)

        mychildnode = New myTreeNode("Others", 4)
        mychildnode.ImageIndex = 1
        mychildnode.SelectedImageIndex = 1
        mynode.Nodes.Add(mychildnode)
    End Sub

    Private Sub trvCategory_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCategory.AfterSelect
        Try
            txtSearch.Text = ""
            Dim mynode As myTreeNode
            If Not IsNothing(e.Node) Then
                mynode = CType(e.Node, myTreeNode)
                key = mynode.Key
                If key <> -1 Then
                    Select Case mynode.Key
                        Case 0
                            strcontacttype = "Physician"
                            ts_btnAdd.Visible = True
                            ts_btnDelete.Visible = True
                            ts_btnModify.Visible = True
                            ts_btnView.Visible = False
                        Case 1
                            strcontacttype = "Hospital"
                            ts_btnAdd.Visible = True
                            ts_btnDelete.Visible = True
                            ts_btnModify.Visible = True
                            ts_btnView.Visible = False
                        Case 2
                            strcontacttype = "Insurance"
                            ts_btnAdd.Visible = True
                            ts_btnDelete.Visible = True
                            ts_btnModify.Visible = True
                            ts_btnView.Visible = False
                        Case 3
                            strcontacttype = "Pharmacy"
                            ts_btnAdd.Visible = True
                            ts_btnDelete.Visible = True
                            ts_btnModify.Visible = True
                            ts_btnView.Visible = False
                        Case 30
                            strcontacttype = "e Pharmacy"
                            ts_btnAdd.Visible = False
                            ts_btnDelete.Visible = False
                            ts_btnModify.Visible = False
                            ts_btnView.Visible = True
                        Case 301
                            strcontacttype = "New Rx"
                            ts_btnAdd.Visible = False
                            ts_btnDelete.Visible = False
                            ts_btnModify.Visible = False
                            ts_btnView.Visible = True
                        Case 302
                            strcontacttype = "New Rx & Refill Request"
                            ts_btnAdd.Visible = False
                            ts_btnDelete.Visible = False
                            ts_btnModify.Visible = False
                            ts_btnView.Visible = True
                        Case 303
                            strcontacttype = "Other Service Level"
                            ts_btnAdd.Visible = False
                            ts_btnDelete.Visible = False
                            ts_btnModify.Visible = False
                            ts_btnView.Visible = True
                        Case 4
                            strcontacttype = "Others"
                            ts_btnAdd.Visible = True
                            ts_btnDelete.Visible = True
                            ts_btnModify.Visible = True
                            ts_btnView.Visible = False
                    End Select
                    BindGrid()
                    '''''Added on 20071120 by Anil
                    If dgContactsList.VisibleRowCount > 0 Then
                        dgContactsList.Select(0)
                    End If
                    ''''''''''''''
                Else
                    dgContactsList.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub dgContactsList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgContactsList.MouseUp
        If dgContactsList.CurrentRowIndex >= 0 Then
            dgContactsList.Select(dgContactsList.CurrentRowIndex)
        End If
    End Sub

    ''Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    ''    Try
    ''        'ContactDBLayer.SetRowFilter(1, txtSearch.Text)
    ''        If Not IsNothing(dgContactsList.DataSource) Then
    ''            Dim mystr As String
    ''            mystr = Replace(Trim(txtSearch.Text), "'", "''")
    ''            ContactDBLayer.SetRowFilter(mystr)
    ''            HideColumn()
    ''            If ContactDBLayer.DsDataview.Count > 0 Then
    ''                dgContactsList.Select(0)
    ''            End If
    ''        End If
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try
    ''End Sub
    Private Sub dgContactsList_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles dgContactsList.Navigate

    End Sub

    Private Sub dgContactsList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgContactsList.DoubleClick
        ''''''Commented by Anil on 01/11/2007
        ''Try
        ''    If dgContactsList.VisibleRowCount >= 1 Then
        ''        Dim Id As Long
        ''        Id = CType((dgContactsList.Item(dgContactsList.CurrentRowIndex, 0)), Long)
        ''        Dim grdIndex As Integer = dgContactsList.CurrentRowIndex
        ''        'Dim frm As ContactMaster
        ''        'If key = 1 Or key = 2 Or key = 3 Then
        ''        '    frm = New ContactMaster(False, Id, strcontacttype)
        ''        'ElseIf key = 0 Or key = 4 Then
        ''        '    frm = New ContactMaster(True, Id, strcontacttype)
        ''        '    If strcontacttype = "Physician" Then
        ''        '        frm.FillControls()
        ''        '    End If
        ''        'End If
        ''        Dim frm As frmContactMst
        ''        If key = 1 Or key = 2 Or key = 3 Or key = 4 Then
        ''            frm = New frmContactMst(False, Id, strcontacttype)
        ''        ElseIf key = 0 Then
        ''            frm = New frmContactMst(True, Id, strcontacttype)
        ''            If strcontacttype = "Physician" Then
        ''                frm.FillControls()
        ''            End If
        ''        End If
        ''        frm.Text = "Update Contacts for  " & strcontacttype
        ''        frm.ShowDialog(Me)
        ''        BindGrid()
        ''        dgContactsList.Select(grdIndex)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub
    Private Sub txtCategorySearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCategorySearch.TextChanged
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
                            trvCategory.SelectedNode = trvCategory.SelectedNode.LastNode
                            '*************
                            trvCategory.SelectedNode = mychildnode
                            txtCategorySearch.Focus()
                            Exit Sub
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If dgContactsList.CurrentRowIndex >= 0 Then
                    dgContactsList.Select(0)
                    dgContactsList.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.Message, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgContactsList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgContactsList.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgContactsList.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                If ContactDBLayer.ContactType = False Then
                    Select Case htInfo.Column
                        Case 1
                            lblSearch.Text = "Name"
                        Case 2
                            lblSearch.Text = "Contact"
                        Case 3
                            lblSearch.Text = "Phone"
                        Case 4
                            lblSearch.Text = "Mobile"
                        Case 5
                            lblSearch.Text = "Email"
                        Case 7
                            lblSearch.Text = "Fax"
                    End Select

                Else
                    Select Case htInfo.Column
                        Case 1
                            lblSearch.Text = "First Name"
                        Case 2
                            lblSearch.Text = "Middle Name"
                        Case 3
                            lblSearch.Text = "Last Name"
                        Case 4
                            lblSearch.Text = "Degree"
                        Case 5
                            lblSearch.Text = "Phone"
                        Case 6
                            lblSearch.Text = "Mobile"
                        Case 7
                            lblSearch.Text = "Email"
                        Case 8
                            lblSearch.Text = "Fax"
                    End Select

                End If
                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                Call UpdateCategory()
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '''''This Code is Added by Anil on 01/11/2007
    Public Sub UpdateContacts(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        ContactDBLayer.FetchData(strcontacttype)
        dgContactsList.SetDataBinding(ContactDBLayer.DsDataview, "")
        txtSearch.Text = ""
        txtSearch.Text = strsearchtxt
        If strcolumnName = "" Then
            ContactDBLayer.SortDataview(ContactDBLayer.DsDataview.Table.Columns(1).ColumnName)
        Else
            Dim strColumn As String = Replace(strcolumnName, "[", "")
            ContactDBLayer.SortDataview(strColumn, strSortBy)
        End If

        HideColumn()
    End Sub
    '''''''''''''''''
    Private Sub AddCategory()
        Try
            If key <> -1 Then
                '' code modified on 20070602 by Bipin
                Dim frm As frmContactMst = Nothing
                If key = 1 Or key = 2 Or key = 3 Or key = 4 Then
                    frm = New frmContactMst(False, strcontacttype)
                    frm.FillControls()
                ElseIf key = 0 Then
                    frm = New frmContactMst(True, strcontacttype)
                    If strcontacttype = "Physician" Then
                        frm.FillControls()
                    End If
                End If
                'Dim frm As ContactMaster
                'If key = 1 Or key = 2 Or key = 3 Then
                '    frm = New ContactMaster(False, strcontacttype)
                'ElseIf key = 0 Or key = 4 Then
                '    frm = New ContactMaster(True, strcontacttype)
                '    If strcontacttype = "Physician" Then
                '        frm.FillControls()
                '    End If
                'End If
                frm.Text = "Add Contacts for " & strcontacttype
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                Call BindGrid()

                ''''</Mahesh>
                '''' To select Newly Added Row 
                '' Sort Grid on ContactID 
                ContactDBLayer.SortDataview(ContactDBLayer.DsDataview.Table.Columns(0).ColumnName)

                'sarika 16th oct 07
                Dim nContactID As Long

                nContactID = frm.ContactID
                ' ''Newly  added Contact's ID is Always Highest. Keep it in nContactID 
                'If CType(dgContactsList.DataSource, DataView).Count > 0 Then
                '    nContactID = dgContactsList.Item((CType(dgContactsList.DataSource, DataView).Count - 1), 0)
                'End If
                '----------------------------


                '' Again Sort Grid On defalut Column i.e on Name
                ContactDBLayer.SortDataview(ContactDBLayer.DsDataview.Table.Columns(1).ColumnName)
                Dim myDataView As DataView = CType(dgContactsList.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim i As Integer
                    For i = 0 To myDataView.Table.Rows.Count - 1
                        ''Search for ContactID
                        If nContactID = dgContactsList.Item(i, 0) Then
                            '' If Found Select That Row & Exit For
                            dgContactsList.CurrentRowIndex = i
                            dgContactsList.Select(i)
                            Exit For
                        End If
                    Next
                End If

                frm.Dispose()
                frm = Nothing
                ''
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateCategory()
        Try
            If key <> -1 Then
                If dgContactsList.VisibleRowCount >= 1 Then
                    Dim Id As Long
                    Id = CType((dgContactsList.Item(dgContactsList.CurrentRowIndex, 0)), Long)

                    Dim grdIndex As Integer = dgContactsList.CurrentRowIndex

                    '' code modified on 20070602 by Bipin
                    If key = 30 Or key = 301 Or key = 302 Or key = 303 Then
                        strcontacttype = "Pharmacy"
                    End If
                    Dim frm As frmContactMst = Nothing 'ContactMaster
                    If key = 1 Or key = 2 Or key = 3 Or key = 4 Then
                        frm = New frmContactMst(False, Id, strcontacttype)
                        frm.FillControls()
                    ElseIf key = 30 Or key = 301 Or key = 302 Or key = 303 Then
                        frm = New frmContactMst(False, Id, strcontacttype, True)
                        frm.FillControls()
                    ElseIf key = 0 Then 'Physician
                        frm = New frmContactMst(True, Id, strcontacttype)
                        If strcontacttype = "Physician" Then
                            frm.FillControls()
                        End If
                    End If
                    'Dim frm As ContactMaster
                    'If key = 1 Or key = 2 Or key = 3 Then
                    '    frm = New ContactMaster(False, Id, strcontacttype)
                    'ElseIf key = 0 Or key = 4 Then 'Physician/Others
                    '    frm = New ContactMaster(True, Id, strcontacttype)
                    '    If strcontacttype = "Physician" Then
                    '        frm.FillControls()
                    '    End If
                    'End If
                    '''''Code is Added by Anil on 01/11/2007
                    Dim myDataView As DataView = CType(dgContactsList.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = myDataView.Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                    End If

                    ''''''''''
                    frm.Text = "Update Contacts for  " & strcontacttype
                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    If key = 30 Then
                        strcontacttype = "e Pharmacy"
                    ElseIf key = 301 Then
                        strcontacttype = "New Rx"
                    ElseIf key = 302 Then
                        strcontacttype = "New Rx & Refill Request"
                    ElseIf key = 303 Then
                        strcontacttype = "Other Service Level"
                    End If
                    ''''code line added by Anil on 01/11/2007
                    UpdateContacts(strcolumnName, strsortorder, strSearchstring)
                    Dim myDatagType As DataView = CType(dgContactsList.DataSource, DataView)
                    If (IsNothing(myDatagType) = False) Then


                        Dim i As Integer
                        For i = 0 To myDatagType.Count - 1
                            If Id = dgContactsList.Item(i, 0) Then
                                dgContactsList.CurrentRowIndex = i
                                dgContactsList.Select(i)
                                Exit For
                            End If
                        Next
                    End If
                    frm.Dispose()
                    frm = Nothing
                    'dgContactsList.Select(grdIndex)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteCategory()
        Try

            txtSearch.Focus()

            If key <> -1 Then

                If dgContactsList.VisibleRowCount >= 1 Then

                    Dim Id As Long
                    Id = CType((dgContactsList.Item(dgContactsList.CurrentRowIndex, 0)), Long)
                    'End If
                    'sarika 24th oct 07
                    'commented foll. lines of code
                    'Try
                    '    ContactDBLayer.DeleteData(Id)
                    'Catch ex As SqlClient.SqlException
                    '    MsgBox(ex.Message)
                    'End Try
                    'added foll. lines of code
                    If trvCategory.SelectedNode.Text = "Physician" Then
                        Try
                            'if doctor
                            'chk wheher the PCP or Referral doctor is associated with any Patient
                            Dim delflag As Boolean
                            delflag = ContactDBLayer.ChkPhyPatAss(Id)

                            'if not associated the delflag  -true so delete the Physician
                            If delflag = True Then
                                If MessageBox.Show("Are you sure you want to delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                                    ContactDBLayer.DeletePhysician(Id)
                                End If
                            Else
                                'Physician is associated with one or Patients -- delflag = false 
                                MessageBox.Show("You cannot delete this physician as this physician is associated with one or more patients.", "Contacts Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Else
                        Try
                            If MessageBox.Show("Are you sure you want to delete this Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                                ContactDBLayer.DeleteData(Id)
                            End If

                        Catch ex As SqlClient.SqlException
                            MsgBox(ex.Message)
                        End Try
                    End If
                    Dim myDataView As DataView = CType(dgContactsList.DataSource, DataView)
                    '--------------------------------------------------------------
                    ''''''This code is added by Anil on
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = CType(dgContactsList.DataSource, DataView).Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                    End If

                    UpdateContacts(strcolumnName, strsortorder, strSearchstring)
                    '''''''''''''''''''''
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Try
            If key <> -1 Then
                BindGrid()
            End If
            '''''Following code lines are addded by Anil 0n 26/09/07 at 10:50 a.m.
            '''''This code clears the search textboxes, gets the focus on the root of theTreeView and clears the grid on click of Refresh button.
            txtSearch.Clear()
            txtCategorySearch.Clear()
            ''''Code line is added by Anil on 01/11/2007.
            _blnSearch = True
            'dgContactsList.DataSource = Nothing
            'trvCategory.CollapseAll()
            'trvCategory.Focus()
            'trvCategory.ExpandAll()
            Dim mynode As myTreeNode
            If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                trvCategory.SelectedNode = mynode
            End If
            '''''added upto here

            '''''Added on 20071120 by Anil
            If dgContactsList.VisibleRowCount > 0 Then
                dgContactsList.Select(0)
            End If
            ''''''''''''''
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Try
            ContactDBLayer = Nothing
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ViewContactMst()


        Try
            'ShowHideMainMenu(False, False)
            'pnlMainToolBar.Visible = False
            'Dim frmContacts As New frmVWContacts
            'frmContacts.MdiParent = Me
            'frmContacts.Show()

            Dim ofrmContacts As New gloContacts.frmViewContacts(GetConnectionString())
            '  ofrmContacts.MdiParent = Me
            ofrmContacts.ShowDialog(IIf(IsNothing(ofrmContacts.Parent), Me, ofrmContacts.Parent))
            ofrmContacts.Dispose()
            ofrmContacts = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            Case "View"
                Call UpdateCategory()
            Case "ViewContacts"
                Call ViewContactMst()
        End Select
    End Sub

End Class
