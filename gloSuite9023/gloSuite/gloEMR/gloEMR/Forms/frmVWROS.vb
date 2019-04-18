Public Class frmVWROS
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
            Try

                If (IsNothing(dgROS) = False) Then
                    dgROS.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgROS)
                    dgROS.Dispose()
                    dgROS = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(cmnuAddCategory) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnuAddCategory)
                    If (IsNothing(cmnuAddCategory.Items) = False) Then
                        cmnuAddCategory.Items.Clear()
                    End If
                    cmnuAddCategory.Dispose()
                    cmnuAddCategory = Nothing
                End If
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
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents trvCategory As System.Windows.Forms.TreeView
    Friend WithEvents txtCategorySearch As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents dgROS As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents cmnuAddCategory As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnCategorySearch As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents imgROS As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWROS))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLeftTop = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.trvCategory = New System.Windows.Forms.TreeView()
        Me.imgROS = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtCategorySearch = New System.Windows.Forms.TextBox()
        Me.btnCategorySearch = New System.Windows.Forms.Button()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.dgROS = New System.Windows.Forms.DataGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.cmnuAddCategory = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgROS, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTopRight.Controls.Add(Me.panel4)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label5)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(618, 24)
        Me.pnlTopRight.TabIndex = 0
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.txtSearch)
        Me.panel4.Controls.Add(Me.label21)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Controls.Add(Me.btnClear)
        Me.panel4.Controls.Add(Me.label22)
        Me.panel4.Controls.Add(Me.label23)
        Me.panel4.Controls.Add(Me.label24)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.Black
        Me.panel4.Location = New System.Drawing.Point(65, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 22)
        Me.panel4.TabIndex = 47
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 0
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.White
        Me.label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.label21.Location = New System.Drawing.Point(5, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(214, 3)
        Me.label21.TabIndex = 37
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.White
        Me.label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label20.Location = New System.Drawing.Point(5, 17)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(214, 5)
        Me.label20.TabIndex = 43
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(219, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.White
        Me.label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.label22.Location = New System.Drawing.Point(1, 0)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(4, 22)
        Me.label22.TabIndex = 38
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.label23.Location = New System.Drawing.Point(0, 0)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(1, 22)
        Me.label23.TabIndex = 39
        Me.label23.Text = "label4"
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.label24.Location = New System.Drawing.Point(240, 0)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(1, 22)
        Me.label24.TabIndex = 40
        Me.label24.Text = "label4"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = " Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(616, 1)
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
        Me.Label7.Location = New System.Drawing.Point(617, 1)
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
        Me.Label8.Size = New System.Drawing.Size(618, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Controls.Add(Me.pnlSearch)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 84)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(220, 434)
        Me.pnlLeft.TabIndex = 2
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_RightBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_TopBrd)
        Me.pnlLeftTop.Controls.Add(Me.trvCategory)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 26)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlLeftTop.Size = New System.Drawing.Size(220, 408)
        Me.pnlLeftTop.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 404)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(215, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 404)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(219, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 404)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(217, 1)
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
        Me.trvCategory.ImageList = Me.imgROS
        Me.trvCategory.Indent = 20
        Me.trvCategory.ItemHeight = 20
        Me.trvCategory.Location = New System.Drawing.Point(3, 0)
        Me.trvCategory.Name = "trvCategory"
        Me.trvCategory.SelectedImageIndex = 1
        Me.trvCategory.ShowLines = False
        Me.trvCategory.Size = New System.Drawing.Size(217, 405)
        Me.trvCategory.TabIndex = 0
        '
        'imgROS
        '
        Me.imgROS.ImageStream = CType(resources.GetObject("imgROS.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgROS.TransparentColor = System.Drawing.Color.Transparent
        Me.imgROS.Images.SetKeyName(0, "ROS.ico")
        Me.imgROS.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgROS.Images.SetKeyName(2, "")
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtCategorySearch)
        Me.pnlSearch.Controls.Add(Me.btnCategorySearch)
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
        Me.pnlSearch.Size = New System.Drawing.Size(220, 26)
        Me.pnlSearch.TabIndex = 0
        Me.pnlSearch.Tag = "0"
        '
        'txtCategorySearch
        '
        Me.txtCategorySearch.BackColor = System.Drawing.Color.White
        Me.txtCategorySearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCategorySearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCategorySearch.ForeColor = System.Drawing.Color.Black
        Me.txtCategorySearch.Location = New System.Drawing.Point(32, 5)
        Me.txtCategorySearch.Name = "txtCategorySearch"
        Me.txtCategorySearch.Size = New System.Drawing.Size(166, 15)
        Me.txtCategorySearch.TabIndex = 0
        '
        'btnCategorySearch
        '
        Me.btnCategorySearch.BackColor = System.Drawing.Color.Transparent
        Me.btnCategorySearch.BackgroundImage = CType(resources.GetObject("btnCategorySearch.BackgroundImage"), System.Drawing.Image)
        Me.btnCategorySearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCategorySearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCategorySearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCategorySearch.FlatAppearance.BorderSize = 0
        Me.btnCategorySearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCategorySearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCategorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCategorySearch.Image = CType(resources.GetObject("btnCategorySearch.Image"), System.Drawing.Image)
        Me.btnCategorySearch.Location = New System.Drawing.Point(198, 5)
        Me.btnCategorySearch.Name = "btnCategorySearch"
        Me.btnCategorySearch.Size = New System.Drawing.Size(21, 15)
        Me.btnCategorySearch.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.btnCategorySearch, "Clear search")
        Me.btnCategorySearch.UseVisualStyleBackColor = False
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(187, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(187, 2)
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
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(215, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(215, 1)
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
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(219, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'dgROS
        '
        Me.dgROS.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgROS.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgROS.BackgroundColor = System.Drawing.Color.White
        Me.dgROS.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgROS.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgROS.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgROS.CaptionForeColor = System.Drawing.Color.White
        Me.dgROS.CaptionVisible = False
        Me.dgROS.DataMember = ""
        Me.dgROS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgROS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgROS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgROS.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgROS.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgROS.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgROS.HeaderForeColor = System.Drawing.Color.White
        Me.dgROS.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgROS.Location = New System.Drawing.Point(0, 0)
        Me.dgROS.Name = "dgROS"
        Me.dgROS.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgROS.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgROS.ReadOnly = True
        Me.dgROS.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgROS.SelectionForeColor = System.Drawing.Color.Black
        Me.dgROS.Size = New System.Drawing.Size(398, 431)
        Me.dgROS.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(624, 54)
        Me.pnlToolStrip.TabIndex = 0
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
        Me.ts_ViewButtons.Size = New System.Drawing.Size(624, 53)
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
        Me.Panel1.Controls.Add(Me.dgROS)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(223, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(401, 434)
        Me.Panel1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 430)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(396, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 430)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(397, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 430)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(398, 1)
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
        Me.Panel2.Size = New System.Drawing.Size(624, 30)
        Me.Panel2.TabIndex = 1
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(220, 84)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 434)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'cmnuAddCategory
        '
        Me.cmnuAddCategory.Name = "cmnuAddCategory"
        Me.cmnuAddCategory.Size = New System.Drawing.Size(61, 4)
        '
        'frmVWROS
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(624, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVWROS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Review of Systems"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgROS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private key As Int64
    Private objclsROS As New clsROS
    Private strROS As String
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim trvnode As myTreeNode


    Private Sub frmVWROS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            FillTreeView()
            dgROS.AllowSorting = True

            trvCategory.ExpandAll()
            Dim mynode As myTreeNode
            If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                key = mynode.Key
                trvCategory.SelectedNode = mynode
                BindGrid()
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BindGrid(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try
            If key <> -1 Then
                objclsROS.FetchData(key)
                dgROS.SetDataBinding(objclsROS.DsDataview, "")
                objclsROS.SortDataview(objclsROS.DsDataview.Table.Columns(1).ColumnName)
                '''''''Code is added by Anil on 20071102
                txtSearch.Text = ""
                txtSearch.Text = strsearchtxt
                If strcolumnName = "" Then
                    objclsROS.SortDataview(objclsROS.DsDataview.Table.Columns(1).ColumnName)
                Else
                    Dim strColumn As String = Replace(strcolumnName, "[", "")

                    objclsROS.SortDataview(strColumn, strSortBy)
                End If
                ''''''''''''''''''''''''''''''''
                'Commented by Shubhangi call this function on Shown event
                ' HideColumn()
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UpdateROS()
        If key <> -1 Then
            If dgROS.VisibleRowCount >= 1 Then
                Dim frm As frmMSTROS
                Dim ID As Long
                ID = CType(dgROS.Item(dgROS.CurrentRowIndex, 0), Long)
                Dim grdIndex As Integer = dgROS.CurrentRowIndex
                ''''''Code is Added by Anil 0n 20071102
                Dim myDataView As DataView = CType(dgROS.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    sortOrder = CType(dgROS.DataSource, DataView).Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                    ''''''''''''''''''
                End If
                frm = New frmMSTROS(key, ID)
                frm.Text = "Update ROS for " & strROS
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                frm.Dispose()
                frm = Nothing
                BindGrid(strcolumnName, strsortorder, strSearchstring)
                '''''
                Dim myDatagView As DataView = CType(dgROS.DataSource, DataView)
                If (IsNothing(myDatagView) = False) Then


                    Dim i As Integer
                    For i = 0 To myDatagView.Count - 1
                        If ID = dgROS.Item(i, 0) Then
                            dgROS.CurrentRowIndex = i
                            dgROS.Select(i)
                            Exit For
                        End If
                    Next
                End If
            End If
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub HideColumn()
        Dim ts As New clsDataGridTableStyle(objclsROS.DsDataview.Table.TableName)

        Dim dgID As New DataGridTextBoxColumn

        With dgID
            .MappingName = objclsROS.DsDataview.Table.Columns(0).ColumnName
            .HeaderText = "ROSID"
            .Alignment = HorizontalAlignment.Center
            .NullText = ""
            .Width = 0
        End With

        Dim dgCol1 As New DataGridTextBoxColumn
        With dgCol1
            .MappingName = objclsROS.DsDataview.Table.Columns(1).ColumnName
            .HeaderText = "Description"
            .NullText = ""
            .Width = 0.2 * dgROS.Width
        End With

        Dim dgCol2 As New DataGridTextBoxColumn
        With dgCol2
            .MappingName = objclsROS.DsDataview.Table.Columns(2).ColumnName
            .HeaderText = "Comments"
            .NullText = ""
            .Width = 0.2 * dgROS.Width - 10
        End With

        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2})
        dgROS.TableStyles.Clear()
        dgROS.TableStyles.Add(ts)

    End Sub

    Private Sub dgROS_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgROS.CurrentCellChanged
        '''''Code is commented by Anil on 20071102
        ''Try
        ''    Select Case dgROS.CurrentCell.ColumnNumber
        ''        Case 1
        ''            lblSearch.Text = "Description"
        ''        Case 2
        ''            lblSearch.Text = "Comments"
        ''    End Select
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView
            dvPatient = CType(dgROS.DataSource(), DataView)
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            dgROS.DataSource = dvPatient

            If IsNothing(dvPatient) = True Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If
            'SHUBHANGI 20091001
            'USE GENERAL SERACH INSTEAD OF COLUMN WISE SEARCH
            dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
             & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' "
            'Shubhangi 20091202
            'Set focus to the first row after filtrate
            If dvPatient.Count > 0 Then
                dgROS.Select(0)
            End If



            'COMMENTED BY SHUBHANGI 20091001 
            'Select Case Trim(lblSearch.Text.Replace(":", ""))
            '    Case "Description"
            '        '''''Code Modified by Anil on 24/09/2007 at 2:45 p.m.
            '        '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
            '        '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
            '        '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

            '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '        ''Else
            '        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '        ''End If
            '    Case "Comments"
            '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '        ''Else
            '        ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '        ''End If
            '        '''''Upto here the changes are made by Anil on 24/09/2007 at 12:45 p.m.
            'End Select
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub trvCategory_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCategory.AfterSelect
        Try
            txtSearch.Text = ""
            Dim mynode As myTreeNode = Nothing
            If Not IsNothing(e.Node) Then
                If Not mynode Is trvCategory.Nodes.Item(0) Then
                    trvnode = e.Node
                    mynode = CType(e.Node, myTreeNode)
                    key = mynode.Key
                    If key <> -1 Then
                        strROS = mynode.Text
                        'Select Case mynode.Key
                        '    Case 0
                        '        strcontacttype = "Physician"
                        '    Case 1
                        '        strcontacttype = "Hospital"
                        '    Case 2
                        '        strcontacttype = "Insurance"
                        '    Case 3
                        '        strcontacttype = "Pharmacy"
                        '    Case 4
                        '        strcontacttype = "Others"
                        'End Select
                        BindGrid()
                    Else
                        dgROS.DataSource = Nothing
                    End If
                Else
                    dgROS.DataSource = Nothing
                End If
            End If
            'trvnode = e.Node
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    ''    Try
    ''        If Not IsNothing(dgROS.DataSource) Then
    ''            Dim Search As String
    ''            Search = Replace(Trim(txtSearch.Text), "'", "''")

    ''            objclsROS.SetRowFilter(Search)
    ''            ' HideColumn()
    ''        End If
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try
    ''End Sub

    Private Sub dgROS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgROS.MouseUp
        If dgROS.CurrentRowIndex >= 0 Then
            dgROS.Select(dgROS.CurrentRowIndex)
        End If
    End Sub

    Private Sub FillTreeView()
        Dim dc As DataTable
        Try
            dc = objclsROS.FillControls
            Dim mynode As myTreeNode
            'mynode = New myTreeNode("Categories", -1)
            mynode = New myTreeNode("Review Of System", -1)
            mynode.ImageIndex = 0
            mynode.SelectedImageIndex = 0
            trvCategory.Nodes.Add(mynode)

            Dim mychildnode As myTreeNode
            Dim i As Integer
            Dim key As Int64
            Dim strname As String
            For i = 0 To dc.Rows.Count - 1
                key = dc.Rows.Item(i)(0)
                strname = dc.Rows.Item(i)(1)
                mychildnode = New myTreeNode(strname, key)
                mychildnode.ImageIndex = 1
                mychildnode.SelectedImageIndex = 1
                mynode.Nodes.Add(mychildnode)
            Next
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgROS_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgROS.DoubleClick
        ''''''Code is commented by Anil on 20071102
        ''Try
        ''    UpdateROS()
        ''    ''If key <> -1 Then
        ''    ''    If dgROS.VisibleRowCount >= 1 Then
        ''    ''        Dim frm As frmMSTROS
        ''    ''        Dim ID As Long
        ''    ''        ID = CType(dgROS.Item(dgROS.CurrentRowIndex, 0), Long)
        ''    ''        Dim grdIndex As Integer = dgROS.CurrentRowIndex
        ''    ''        frm = New frmMSTROS(key, ID)
        ''    ''        frm.Text = "Update ROS for " & strROS
        ''    ''        frm.ShowDialog(Me)
        ''    ''        BindGrid()
        ''    ''        dgROS.Select(grdIndex)
        ''    ''    End If
        ''    ''End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub txtSearch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSearch.Validating

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
            MessageBox.Show(ex.Message, "History", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, "History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If dgROS.CurrentRowIndex >= 0 Then
                    dgROS.Select(0)
                    dgROS.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVWROS_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub
    ''''''''Code/event added by Anil on 20071102
    Private Sub dgROS_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgROS.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgROS.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                'COMMENTED BY SHUBHANGI 20091001
                'Select Case htInfo.Column
                '    Case 1
                '        lblSearch.Text = "Description"
                '    Case 2
                '        lblSearch.Text = "Comments"
                'End Select

                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If

            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                UpdateROS()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AddCategory()
        Try
            If key <> -1 Then
                Dim frm As frmMSTROS
                frm = New frmMSTROS(key)
                frm.Text = "Add ROS for " & strROS
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                BindGrid()

                Dim myDataView As DataView = CType(dgROS.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim i As Integer
                    For i = 0 To myDataView.Table.Rows.Count - 1
                        If frm._Description = dgROS.Item(i, 1) Then
                            dgROS.CurrentRowIndex = i
                            dgROS.Select(i)
                            Exit For
                        End If
                    Next
                End If
                frm.Dispose()
                frm = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateCategory()
        Try
            UpdateROS()
            '''''If key <> -1 Then
            '''''    If dgROS.VisibleRowCount >= 1 Then
            '''''        Dim frm As frmMSTROS
            '''''        Dim ID As Long
            '''''        ID = CType(dgROS.Item(dgROS.CurrentRowIndex, 0), Long)
            '''''        Dim grdIndex As Integer = dgROS.CurrentRowIndex

            '''''        frm = New frmMSTROS(key, ID)
            '''''        frm.Text = "Update ROS for " & strROS
            '''''        frm.ShowDialog(Me)
            '''''        BindGrid()
            '''''        dgROS.Select(grdIndex)
            '''''    End If
            '''''End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteCategory()
        Try
            If key <> -1 Then
                If dgROS.VisibleRowCount >= 1 Then
                    If MessageBox.Show("Are you sure you want to delete this Record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        Dim ID As Long
                        ID = CType(dgROS.Item(dgROS.CurrentRowIndex, 0), Long)
                        Try

                            If CheckIsHistoryItemExistsinPortal(ID) Then
                                Exit Sub
                            End If

                            objclsROS.DeleteData(ID)

                            txtSearch.Focus()

                            ''''''Code is Added by Anil 0n 20071102
                            Dim myDataView As DataView = CType(dgROS.DataSource, DataView)
                            If (IsNothing(myDataView) = False) Then


                                sortOrder = myDataView.Sort
                                strSearchstring = txtSearch.Text.Trim
                                arrcolumnsort = Split(sortOrder, "]")
                                If arrcolumnsort.Length > 1 Then
                                    strcolumnName = arrcolumnsort.GetValue(0)
                                    strsortorder = arrcolumnsort.GetValue(1)
                                End If

                                BindGrid(strcolumnName, strsortorder, strSearchstring)
                            End If
                            ''''''''''''''''''
                        Catch ex As SqlClient.SqlException
                            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try

                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CheckIsHistoryItemExistsinPortal(ByVal HistoryID As Long) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dt2 As DataTable = Nothing
        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@nhistoryid", HistoryID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)
            oParameters.Add("@ItemType", 1, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Connect(False)
            oDB.Retrive("WS_IsHistoryItemExistsinHealthform", oParameters, _dt)
            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then

                If MessageBox.Show("Selected ROS item is used in patient portal forms. Once this ROS item is deleted then patients can no longer see this ROS item in patient portal forms." + System.Environment.NewLine + System.Environment.NewLine + "Do you want to continue with the deletion?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                    Try
                        ' Set IsRepublish Required to 1 & Delete Entry 
                        If oParameters IsNot Nothing Then

                            oParameters.Dispose()
                            oParameters = Nothing
                        End If
                        oParameters = New gloDatabaseLayer.DBParameters()
                        oParameters.Add("@nhistoryid", HistoryID, ParameterDirection.Input, SqlDbType.BigInt)
                        oParameters.Add("@IsDelete", True, ParameterDirection.Input, SqlDbType.Bit)
                        oParameters.Add("@ItemType", 1, ParameterDirection.Input, SqlDbType.BigInt)
                        oDB.Connect(False)
                        oDB.Retrive("WS_IsHistoryItemExistsinHealthform", oParameters, _dt2)
                    Catch ex As Exception
                    End Try
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If


        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Throw dbEx
        Finally

            If oParameters IsNot Nothing Then

                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If

            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If

            If Not IsNothing(_dt2) Then
                _dt2.Dispose()
                _dt2 = Nothing
            End If
        End Try

        Return False

    End Function

    Private Sub RefreshCategory()
        Try
            'shubhangi 20101118
            trvCategory.Nodes.Clear()
            FillTreeView()
            dgROS.AllowSorting = True
            trvCategory.ExpandAll()
            If key <> -1 Then
                Me.Cursor = Cursors.WaitCursor
                BindGrid()
            End If
            '''''Following code lines are addded by Anil 0n 28/09/07 at 04:11 p.m.
            '''''This code clears the search textboxes, gets the focus on the root of the TreeView 
            txtSearch.Clear()
            txtSearch.Focus()
            txtCategorySearch.Clear()
            _blnSearch = True
            ''shubhangi 20101118 commented
            'Dim mynode As myTreeNode
            'If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
            '    mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
            '    trvCategory.SelectedNode = mynode
            '    BindGrid()
            'End If
            '''''added upto here


            'key = trvnode.Key
            trvCategory.SelectedNode = trvCategory.Nodes.Item(0).Nodes.Item(trvnode.Index) 'trvnode.Index
            trvCategory.Select()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
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
    Private Sub trvCategory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCategory.MouseDown
        'code added by dipak 20090909 for show contextmenu "Add ROS Category for tree view trvCategory
        Try
            If (IsNothing(cmnuAddCategory) = False) Then
                gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnuAddCategory)
                If (IsNothing(cmnuAddCategory.Items) = False) Then
                    cmnuAddCategory.Items.Clear()
                End If
                cmnuAddCategory.Dispose()
                cmnuAddCategory = Nothing
            End If
        Catch

        End Try
        cmnuAddCategory = New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = New ToolStripMenuItem
        oMenuItem.Text = "Add ROS Category"
        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125)
        cmnuAddCategory.Items.Add(oMenuItem)
        Try
            If (IsNothing(trvCategory.ContextMenuStrip) = False) Then
                gloGlobal.cEventHelper.RemoveAllEventHandlers(trvCategory.ContextMenuStrip)
                If (IsNothing(trvCategory.ContextMenuStrip.Items) = False) Then
                    trvCategory.ContextMenuStrip.Items.Clear()
                End If
                trvCategory.ContextMenuStrip.Dispose()
                trvCategory.ContextMenuStrip = Nothing
            End If
        Catch

        End Try
        trvCategory.ContextMenuStrip = cmnuAddCategory
        AddHandler oMenuItem.Click, AddressOf AddRosCategory
        oMenuItem = Nothing
        cmnuAddCategory = Nothing
        'end dipak
    End Sub
    Private Sub AddRosCategory(ByVal sender As Object, ByVal e As EventArgs)
        'code added by dipak 20090909 for add Ros Category 
        Dim frm As New CategoryMaster
        Dim nd As TreeNode
        Try
            frm.Text = "Add ROS Category"
            frm.cmbCategoryType.Text = "ROS"
            frm.cmbCategoryType.Enabled = False
            frm.IsFromROS = True
            'frm.Label4.Visible = False
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            trvCategory.Nodes.Clear()
            FillTreeView()
            trvCategory.ExpandAll()
            'for each loop is used for select resently added category
            For Each nd In trvCategory.Nodes(0).Nodes
                If (nd.Text = frm.txtCategoryDesc.Text) Then
                    trvCategory.SelectedNode = nd
                    Exit For
                End If
            Next
            frm.Dispose()
            frm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            frm = Nothing
        End Try
        'end dipak
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub btnCategorySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCategorySearch.Click
        'SHUBHANGI 20091001
        'Use to clear search text box in tree view
        txtCategorySearch.ResetText()
        txtCategorySearch.Focus()
        trvCategory.SelectedNode = trvCategory.Nodes(0)
        'trvCategory.Select()
    End Sub

    'Shubhangi 20091202
    'Add this event coz this event is fire after load so at that time form's size is maximize so the column size become appropriate according to actual size of control
    Private Sub frmVWROS_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        HideColumn()
    End Sub

    
End Class
