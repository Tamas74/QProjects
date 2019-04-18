Public Class frmVWICD9
    Inherits System.Windows.Forms.Form

    Public Shared blnModify As Boolean
    Public SpecialityID As Int64
    Public SpecialityName As String
    Private objclsICD9 As New clsICD9
    Dim dv As New DataView
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
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
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

                If (IsNothing(grdICD9) = False) Then
                    grdICD9.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdICD9)
                    grdICD9.Dispose()
                    grdICD9 = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If IsNothing(ContextMenu1) = False Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ContextMenu1)
                    If (IsNothing(ContextMenu1.MenuItems) = False) Then
                        ContextMenu1.MenuItems.Clear()
                    End If
                    ContextMenu1.Dispose()
                    ContextMenu1 = Nothing
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
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents txtCategorySearch As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents trvSpeciality As System.Windows.Forms.TreeView
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuAddSpeciality As System.Windows.Forms.MenuItem
    Friend WithEvents grdICD9 As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents imgICD9 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWICD9))
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.pnlLeftTop = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.trvSpeciality = New System.Windows.Forms.TreeView
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuAddSpeciality = New System.Windows.Forms.MenuItem
        Me.imgICD9 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.txtCategorySearch = New System.Windows.Forms.TextBox
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label
        Me.PicBx_Search = New System.Windows.Forms.PictureBox
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label
        Me.grdICD9 = New System.Windows.Forms.DataGrid
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlTopRight.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdICD9, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Controls.Add(Me.Label9)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(842, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(840, 1)
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
        Me.Label7.Size = New System.Drawing.Size(1, 23)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(841, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 23)
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
        Me.Label9.Size = New System.Drawing.Size(842, 1)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "label1"
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(97, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(299, 22)
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
        Me.lblSearch.Size = New System.Drawing.Size(96, 20)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "  Description :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Controls.Add(Me.pnlSearch)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 83)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(184, 435)
        Me.pnlLeft.TabIndex = 2
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeftTop.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_RightBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_TopBrd)
        Me.pnlLeftTop.Controls.Add(Me.trvSpeciality)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 26)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlLeftTop.Size = New System.Drawing.Size(184, 409)
        Me.pnlLeftTop.TabIndex = 2
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 405)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(179, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 402)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(183, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 402)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(181, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'trvSpeciality
        '
        Me.trvSpeciality.BackColor = System.Drawing.Color.White
        Me.trvSpeciality.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSpeciality.ContextMenu = Me.ContextMenu1
        Me.trvSpeciality.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSpeciality.ForeColor = System.Drawing.Color.Black
        Me.trvSpeciality.HideSelection = False
        Me.trvSpeciality.ImageIndex = 1
        Me.trvSpeciality.ImageList = Me.imgICD9
        Me.trvSpeciality.ItemHeight = 18
        Me.trvSpeciality.Location = New System.Drawing.Point(3, 3)
        Me.trvSpeciality.Name = "trvSpeciality"
        Me.trvSpeciality.SelectedImageIndex = 1
        Me.trvSpeciality.ShowLines = False
        Me.trvSpeciality.Size = New System.Drawing.Size(181, 403)
        Me.trvSpeciality.TabIndex = 0
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAddSpeciality})
        '
        'mnuAddSpeciality
        '
        Me.mnuAddSpeciality.Index = 0
        Me.mnuAddSpeciality.Text = "Add Specialty"
        '
        'imgICD9
        '
        Me.imgICD9.ImageStream = CType(resources.GetObject("imgICD9.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgICD9.TransparentColor = System.Drawing.Color.Transparent
        Me.imgICD9.Images.SetKeyName(0, "ICD 9_01.ico")
        Me.imgICD9.Images.SetKeyName(1, "arrow_01.ico")
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
        'grdICD9
        '
        Me.grdICD9.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdICD9.BackgroundColor = System.Drawing.Color.White
        Me.grdICD9.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdICD9.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdICD9.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdICD9.CaptionForeColor = System.Drawing.Color.White
        Me.grdICD9.CaptionVisible = False
        Me.grdICD9.DataMember = ""
        Me.grdICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdICD9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdICD9.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdICD9.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdICD9.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdICD9.HeaderForeColor = System.Drawing.Color.White
        Me.grdICD9.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdICD9.Location = New System.Drawing.Point(0, 1)
        Me.grdICD9.Name = "grdICD9"
        Me.grdICD9.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdICD9.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdICD9.ReadOnly = True
        Me.grdICD9.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdICD9.SelectionForeColor = System.Drawing.Color.Black
        Me.grdICD9.Size = New System.Drawing.Size(657, 431)
        Me.grdICD9.TabIndex = 6
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(848, 53)
        Me.pnlToolStrip.TabIndex = 11
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(848, 53)
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
        Me.Panel1.Controls.Add(Me.grdICD9)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(187, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(661, 435)
        Me.Panel1.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 431)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(656, 1)
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
        Me.Label4.Location = New System.Drawing.Point(657, 1)
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
        Me.Label5.Size = New System.Drawing.Size(658, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(848, 30)
        Me.Panel2.TabIndex = 13
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(184, 83)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 435)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'frmVWICD9
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(848, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWICD9"
        Me.Text = "ICD9"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdICD9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmVWICD9_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim objclsICD9 As New clsICD9
        Try
            grdICD9.DataSource = objclsICD9.GetAllICD(SpecialityID)
            dv = objclsICD9.GetDataview
            objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)

            CustomGridStyle()
            CustomTreeView()
            '''''The following code is added by Anil on 26/09/2007 at 4:00 p.m.
            '''''This code gets the focus of first node of treeview on form load and grid is filled with the data against the selected node.
            Dim mynode As myTreeNode
            If trvSpeciality.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvSpeciality.Nodes.Item(0).Nodes.Item(0)
                trvSpeciality.SelectedNode = mynode
            End If
            '''''Upto here the code is added.
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateICD9()
        If Not trvSpeciality.SelectedNode Is trvSpeciality.Nodes.Item(0) Then
            Dim ID As Long
            Dim objfrmMSTICD9 As frmMSTICD9
            'Dim objclsICD9 As New clsICD9
            Try
                If grdICD9.VisibleRowCount >= 1 Then
                    blnModify = True
                    ID = grdICD9.Item(grdICD9.CurrentRowIndex, 0).ToString
                    Dim grdIndex As Integer = grdICD9.CurrentRowIndex
                    Dim myDataView As DataView = CType(grdICD9.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        ''''''Code is Added by Anil 0n 20071102
                        Dim sortOrder As String = CType(grdICD9.DataSource, DataView).Sort
                        Dim strSearchstring As String = txtSearch.Text.Trim
                        Dim arrcolumnsort() As String = Split(sortOrder, "]")
                        Dim strcolumnName As String = arrcolumnsort.GetValue(0)
                        Dim strsortorder As String = ""
                        If arrcolumnsort.Length > 1 Then
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        ''''''''''''''''''

                        objfrmMSTICD9 = New frmMSTICD9(ID)
                        objfrmMSTICD9.Text = "Modify ICD9"
                        objfrmMSTICD9.ShowDialog(IIf(IsNothing(objfrmMSTICD9.Parent), Me, objfrmMSTICD9.Parent))

                        If objfrmMSTICD9.CancelClick = False Then
                            grdICD9.DataSource = objclsICD9.GetAllICD(SpecialityID)
                            dv = objclsICD9.GetDataview
                            'objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)

                            '' To Remember the Selection of Row 
                            Dim i As Integer

                            For i = 0 To trvSpeciality.Nodes(0).GetNodeCount(False) - 1
                                Dim CategoryNode As myTreeNode
                                CategoryNode = trvSpeciality.Nodes(0).Nodes(i)
                                If objfrmMSTICD9._SpecialityID = CategoryNode.Key Then
                                    trvSpeciality.SelectedNode = CategoryNode
                                    Exit For
                                End If
                            Next
                            CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                            Dim myDatagView As DataView = CType(grdICD9.DataSource, DataView)
                            If (IsNothing(myDatagView) = False) Then


                                For i = 0 To myDatagView.Count - 1
                                    '' when ID Found select that matching Row
                                    If ID = grdICD9.Item(i, 0) Then
                                        grdICD9.CurrentRowIndex = i
                                        grdICD9.Select(i)
                                        Exit For
                                    End If
                                Next
                            End If
                            ' ''Else
                            ' ''    grdICD9.Select(grdIndex)
                        End If
                        objfrmMSTICD9.Dispose()
                        objfrmMSTICD9 = Nothing
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                objfrmMSTICD9 = Nothing
                'objclsICD9 = Nothing
            End Try
        End If
    End Sub
    Private Sub grdICD9_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdICD9.DoubleClick
        ''Try
        ''    UpdateICD9()

        ''Catch ex As Exception

        ''End Try
        ''Dim ID As Long
        ''Dim objfrmMSTICD9 As frmMSTICD9
        ''''Dim objclsICD9 As New clsICD9
        ''Try
        ''    If grdICD9.VisibleRowCount >= 1 Then
        ''        blnModify = True
        ''        ID = grdICD9.Item(grdICD9.CurrentRowIndex, 0).ToString
        ''        Dim grdIndex As Integer = grdICD9.CurrentRowIndex

        ''        objfrmMSTICD9 = New frmMSTICD9(ID)
        ''        objfrmMSTICD9.Text = "Modify ICD9"
        ''        objfrmMSTICD9.ShowDialog(Me)
        ''        If objfrmMSTICD9.CancelClick = False Then
        ''            grdICD9.DataSource = objclsICD9.GetAllICD(SpecialityID)
        ''            dv = objclsICD9.GetDataview
        ''            objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)
        ''        End If
        ''        grdICD9.Select(grdIndex)
        ''    End If
        ''Catch ex As Exception
        ''    MsgBox(ex.ToString)
        ''Finally
        ''    objfrmMSTICD9 = Nothing
        ''    'objclsICD9 = Nothing
        ''End Try
    End Sub
    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Dim dv As DataView
        dv = objclsICD9.GetDataview
        Dim ts As New clsDataGridTableStyle(dv.Table.TableName)

        objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)

        Dim IDCol As New DataGridTextBoxColumn
        IDCol.Width = 0
        IDCol.MappingName = dv.Table.Columns(0).ColumnName
        IDCol.HeaderText = "ID"

        Dim ICD9CodeCol As New DataGridTextBoxColumn
        With ICD9CodeCol
            .Width = 0.25 * grdICD9.Width
            .MappingName = dv.Table.Columns(1).ColumnName
            .HeaderText = "ICD9 Code"
            .NullText = ""
        End With

        Dim DescCol As New DataGridTextBoxColumn
        With DescCol
            .Width = 0.75 * grdICD9.Width - 10
            .MappingName = dv.Table.Columns(2).ColumnName
            .HeaderText = "Description"
            .NullText = ""
        End With

        'Dim SpecialityCol As New DataGridTextBoxColumn
        'With SpecialityCol
        '    .Width = 200
        '    .MappingName = objclsICD9.GetDataview.Table.Columns(3).ColumnName
        '    .HeaderText = "Specialty"
        '    .NullText = ""
        'End With

        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, ICD9CodeCol, DescCol})
        grdICD9.TableStyles.Clear()
        grdICD9.TableStyles.Add(ts)

        '''''''Code is added by Anil on 02/11/2007
        txtSearch.Text = ""
        txtSearch.Text = strsearchtxt
        If strcolumnName = "" Then
            objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)
        Else
            Dim strColumn As String = Replace(strcolumnName, "[", "")

            objclsICD9.SortDataview(strColumn, strSortBy)
        End If
        ''''''''''''''''''''''''''''''''

    End Sub
    Private Sub grdICD9_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdICD9.CurrentCellChanged
        '''''''Code is commented by Anil on 20071102
        ''Try
        ''    Select Case grdICD9.CurrentCell.ColumnNumber
        ''        Case 1
        ''            txtSearch.Text = ""
        ''            lblSearch.Text = "ICD9 Code"
        ''        Case 2
        ''            txtSearch.Text = ""
        ''            lblSearch.Text = "Description"
        ''    End Select
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(grdICD9.DataSource(), DataView)

                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                grdICD9.DataSource = dvPatient
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
                    Case "ICD9 Code"
                        '''''Code Modified by Anil on 24/09/2007 at 10:48 a.m.
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

                        '''''Upto here the changes are made by Anil on 24/09/2007 at 10:48 a.m.
                End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    ''Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    ''    Try
    ''        If Not IsNothing(objclsICD9.GetDataview) Then
    ''            'Check if sort column is not of type boolean
    ''            Dim Search As String
    ''            Search = Replace(Trim(txtSearch.Text), "'", "''")
    ''            objclsICD9.SetRowFilter(Trim(Search))
    ''            'HideColumn()
    ''        End If
    ''        'objclsICD9.Search(grdICD9.DataSource, 1, txtSearch.Text)
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.ToString, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''        ' objclsICD9 = Nothing
    ''    End Try
    ''End Sub

    Public Sub CustomTreeView()
        Dim newNode As New TreeNode
        'Dim objclsICD9 As New clsICD9
        Dim objMyTreeView As myTreeNode
        Dim objchild As myTreeNode
        Dim i As Integer
        trvSpeciality.Nodes.Clear()

        objMyTreeView = New myTreeNode("Specialty", 0)
        objMyTreeView.ImageIndex = 0
        objMyTreeView.SelectedImageIndex = 0
        trvSpeciality.Nodes.Add(objMyTreeView)

        Dim dt As DataTable = objclsICD9.GetAllSpeciality
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
        trvSpeciality.ExpandAll()
    End Sub

    Private Sub trvSpeciality_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvSpeciality.AfterSelect

        If Not e.Node Is trvSpeciality.Nodes.Item(0) Then
            Dim objMyTreeView As myTreeNode
            ' Dim objclsICD9 As New clsICD9
            objMyTreeView = CType(e.Node, myTreeNode)

            SpecialityID = objMyTreeView.Key
            SpecialityName = objMyTreeView.NodeName
            grdICD9.DataSource = objclsICD9.GetAllICD(SpecialityID)
            dv = objclsICD9.GetDataview
            If IsNothing(dv) = False Then

                objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)
            End If
            grdICD9.CaptionText = "ICD9 - " & SpecialityName
            lblSearch.Text = "Description"
            txtSearch.Text = ""
            txtSearch.Focus()
        Else

            grdICD9.DataSource = Nothing
        End If
    End Sub
    Private Sub mnuAddSpeciality_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddSpeciality.Click
        Dim objfrmSpeciality As New SpecialtyMaster
        objfrmSpeciality.ShowDialog(IIf(IsNothing(objfrmSpeciality.Parent), Me, objfrmSpeciality.Parent))
        objfrmSpeciality.Dispose()
        objfrmSpeciality = Nothing
        CustomTreeView()
    End Sub
    Private Sub grdICD9_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdICD9.MouseUp
        If grdICD9.CurrentRowIndex >= 0 Then
            grdICD9.Select(grdICD9.CurrentRowIndex)
        End If
    End Sub

    Private Sub txtCategorySearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCategorySearch.TextChanged
        Try
            If Trim(txtCategorySearch.Text) <> "" Then
                If trvSpeciality.Nodes(0).GetNodeCount(False) > 0 Then
                    Dim mychildnode As TreeNode
                    'child node collection

                    For Each mychildnode In trvSpeciality.Nodes(0).Nodes
                        Dim str As String
                        str = UCase(Trim(mychildnode.Text))
                        If Mid(str, 1, Len(Trim(txtCategorySearch.Text))) = UCase(Trim(txtCategorySearch.Text)) Then
                            '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                            trvSpeciality.SelectedNode = mychildnode.LastNode 'trvSpeciality.Nodes(trvSpeciality.Nodes.Count - 1)
                            '*************
                            trvSpeciality.SelectedNode = mychildnode
                            txtCategorySearch.Focus()
                            Exit Sub
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCategorySearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCategorySearch.KeyPress
        If trvSpeciality.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvSpeciality.Select()
                'Else
                '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
            End If
        End If
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdICD9.CurrentRowIndex >= 0 Then
                    grdICD9.Select(0)
                    grdICD9.CurrentRowIndex = 0
                    'grdICD9.Focus()
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVWICD9_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub

    ''''''''Code/Event is Added by Anil on 20071102

    Private Sub grdICD9_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdICD9.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdICD9.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                Select Case htInfo.Column
                    Case 1
                        txtSearch.Text = ""
                        lblSearch.Text = "ICD9 Code"
                    Case 2
                        txtSearch.Text = ""
                        lblSearch.Text = "Description"
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
                UpdateICD9()
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''''''''''''''''''
    ''Code Added by Shilpa for adding the new buttons on 13th Nov 2007
    Private Sub AddCategory()
        If Not trvSpeciality.SelectedNode Is trvSpeciality.Nodes.Item(0) Then
            Dim objfrmMSTICD9 As New frmMSTICD9(SpecialityName)
            'Dim objclsICD9 As New clsICD9
            Try
                blnModify = False
                objfrmMSTICD9.Text = "Add New ICD9"
                objfrmMSTICD9.ShowDialog(IIf(IsNothing(objfrmMSTICD9.Parent), Me, objfrmMSTICD9.Parent))

                If objfrmMSTICD9.CancelClick = False Then
                    grdICD9.DataSource = objclsICD9.GetAllICD(objfrmMSTICD9._SpecialityID)
                    'dv = objclsICD9.GetDataview

                    If IsNothing(dv) = False Then
                        objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)
                    End If

                    Dim i As Integer

                    For i = 0 To trvSpeciality.Nodes(0).GetNodeCount(False) - 1
                        Dim CategoryNode As myTreeNode
                        CategoryNode = trvSpeciality.Nodes(0).Nodes(i)
                        If objfrmMSTICD9._SpecialityID = CategoryNode.Key Then
                            trvSpeciality.SelectedNode = CategoryNode
                            Exit For
                        End If
                    Next
                    '' To Remember the Selection of Row 
                    ' Dim i As Integer
                    Dim myDataView As DataView = CType(grdICD9.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then

                        For i = 0 To myDataView.Table.Rows.Count - 1
                            '' when ID Found select that matching Row
                            If objfrmMSTICD9._ICD9Code = grdICD9.Item(i, 1) Then
                                grdICD9.CurrentRowIndex = i
                                grdICD9.Select(i)
                                Exit For
                            End If
                        Next
                    End If

                End If
                objfrmMSTICD9.Dispose()
                objfrmMSTICD9 = Nothing
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                objfrmMSTICD9 = Nothing
                ' objclsICD9 = Nothing
            End Try
        End If
    End Sub
    Private Sub UpdateCategory()
        Try
            UpdateICD9()
        Catch ex As Exception

        End Try
        ''If Not trvSpeciality.SelectedNode Is trvSpeciality.Nodes.Item(0) Then
        ''    Dim ID As Long
        ''    Dim objfrmMSTICD9 As frmMSTICD9
        ''    'Dim objclsICD9 As New clsICD9
        ''    Try
        ''        If grdICD9.VisibleRowCount >= 1 Then
        ''            blnModify = True
        ''            ID = grdICD9.Item(grdICD9.CurrentRowIndex, 0).ToString
        ''            Dim grdIndex As Integer = grdICD9.CurrentRowIndex

        ''            objfrmMSTICD9 = New frmMSTICD9(ID)
        ''            objfrmMSTICD9.Text = "Modify ICD9"
        ''            objfrmMSTICD9.ShowDialog(Me)
        ''            If objfrmMSTICD9.CancelClick = False Then
        ''                grdICD9.DataSource = objclsICD9.GetAllICD(SpecialityID)
        ''                dv = objclsICD9.GetDataview
        ''                objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)
        ''            End If
        ''            grdICD9.Select(grdIndex)
        ''        End If
        ''    Catch ex As Exception
        ''        MessageBox.Show(ex.ToString, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''    Finally
        ''        objfrmMSTICD9 = Nothing
        ''        'objclsICD9 = Nothing
        ''    End Try
        ''End If
    End Sub
    Private Sub DeleteCategory()
        If Not trvSpeciality.SelectedNode Is trvSpeciality.Nodes.Item(0) Then
            Dim ID As Long
            'Dim objclsICD9 As New clsICD9
            Dim ICD9Code As String
            Try
                If grdICD9.VisibleRowCount >= 1 Then
                    'blnModify = True

                    If MessageBox.Show("Are you sure to Delete this ICD9's Detail", "ICD9", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                        ID = grdICD9.Item(grdICD9.CurrentRowIndex, 0).ToString
                        ICD9Code = grdICD9.Item(grdICD9.CurrentRowIndex, 1).ToString
                        objclsICD9.DeleteICD9(ID, ICD9Code)
                        grdICD9.DataSource = objclsICD9.GetAllICD(SpecialityID)
                        dv = objclsICD9.GetDataview
                        objclsICD9.SortDataview(dv.Table.Columns(1).ColumnName)
                        ''''''Code is Added by Anil 0n 20071102
                        Dim myDataView As DataView = CType(grdICD9.DataSource, DataView)
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
                    ' objclsICD9.SortDataview(objclsICD9.GetDataview.Table.Columns(1).ColumnName)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'objclsICD9 = Nothing
            End Try
        End If
    End Sub
    Private Sub RefreshCategory()
        ' Dim objICD9 As New clsICD9
        If Not trvSpeciality.SelectedNode Is trvSpeciality.Nodes.Item(0) Then
            Try
                grdICD9.DataSource = objclsICD9.GetAllICD(SpecialityID)

                CustomGridStyle()
                'CustomTreeView()
                '''''Following code lines are addded by Anil 0n 26/09/07 at 1:00 p.m.
                '''''This code clears the search textboxes, gets the focus on the root of theTreeView and clears the grid on click of Refresh button.
                txtSearch.Clear()
                txtCategorySearch.Clear()
                _blnSearch = True
                Dim mynode As myTreeNode
                If trvSpeciality.Nodes.Item(0).GetNodeCount(False) > 0 Then
                    mynode = trvSpeciality.Nodes.Item(0).Nodes.Item(0)
                    trvSpeciality.SelectedNode = mynode
                End If
                'grdICD9.DataSource = Nothing
                'trvSpeciality.CollapseAll()
                'trvSpeciality.Focus()
                'trvSpeciality.ExpandAll()
                '''''added upto here
            Catch ex As Exception
                MessageBox.Show(ex.ToString, "ICD 9", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'objICD9 = Nothing
            End Try
        End If
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
End Class
