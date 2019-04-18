Public Class frmVWDrugs
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

                If (IsNothing(grdDrugs) = False) Then
                    grdDrugs.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdDrugs)
                    grdDrugs.Dispose()
                    grdDrugs = Nothing
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
    Friend WithEvents txtCategorySearch As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents trvDrugs As System.Windows.Forms.TreeView
    Friend WithEvents grdDrugs As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
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
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnCategoryClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents imgDrugs As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWDrugs))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.trvDrugs = New System.Windows.Forms.TreeView()
        Me.imgDrugs = New System.Windows.Forms.ImageList(Me.components)
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtCategorySearch = New System.Windows.Forms.TextBox()
        Me.btnCategoryClear = New System.Windows.Forms.Button()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.grdDrugs = New System.Windows.Forms.DataGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.pnlTopRight.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDrugs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.Panel4)
        Me.pnlTopRight.Controls.Add(Me.Label11)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label5)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(714, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(73, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label11.Size = New System.Drawing.Size(4, 20)
        Me.Label11.TabIndex = 44
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(72, 20)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "    Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(712, 1)
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
        Me.Label7.Location = New System.Drawing.Point(713, 1)
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
        Me.Label8.Size = New System.Drawing.Size(714, 1)
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
        Me.pnlLeft.Size = New System.Drawing.Size(220, 435)
        Me.pnlLeft.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.trvDrugs)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(220, 409)
        Me.Panel1.TabIndex = 17
        '
        'trvDrugs
        '
        Me.trvDrugs.BackColor = System.Drawing.Color.White
        Me.trvDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvDrugs.ForeColor = System.Drawing.Color.Black
        Me.trvDrugs.HideSelection = False
        Me.trvDrugs.ImageIndex = 1
        Me.trvDrugs.ImageList = Me.imgDrugs
        Me.trvDrugs.Indent = 20
        Me.trvDrugs.ItemHeight = 20
        Me.trvDrugs.Location = New System.Drawing.Point(7, 4)
        Me.trvDrugs.Name = "trvDrugs"
        Me.trvDrugs.SelectedImageIndex = 1
        Me.trvDrugs.ShowLines = False
        Me.trvDrugs.Size = New System.Drawing.Size(212, 401)
        Me.trvDrugs.TabIndex = 3
        '
        'imgDrugs
        '
        Me.imgDrugs.ImageStream = CType(resources.GetObject("imgDrugs.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgDrugs.TransparentColor = System.Drawing.Color.Transparent
        Me.imgDrugs.Images.SetKeyName(0, "Drugs.ico")
        Me.imgDrugs.Images.SetKeyName(1, "Bullet06.ico")
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(3, 401)
        Me.Label10.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(215, 3)
        Me.Label9.TabIndex = 9
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 405)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 405)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(217, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtCategorySearch)
        Me.pnlSearch.Controls.Add(Me.btnCategoryClear)
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
        Me.pnlSearch.TabIndex = 16
        '
        'txtCategorySearch
        '
        Me.txtCategorySearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCategorySearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCategorySearch.ForeColor = System.Drawing.Color.Black
        Me.txtCategorySearch.Location = New System.Drawing.Point(32, 5)
        Me.txtCategorySearch.Name = "txtCategorySearch"
        Me.txtCategorySearch.Size = New System.Drawing.Size(166, 15)
        Me.txtCategorySearch.TabIndex = 1
        '
        'btnCategoryClear
        '
        Me.btnCategoryClear.BackColor = System.Drawing.Color.Transparent
        Me.btnCategoryClear.BackgroundImage = CType(resources.GetObject("btnCategoryClear.BackgroundImage"), System.Drawing.Image)
        Me.btnCategoryClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCategoryClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCategoryClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCategoryClear.FlatAppearance.BorderSize = 0
        Me.btnCategoryClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCategoryClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCategoryClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCategoryClear.Image = CType(resources.GetObject("btnCategoryClear.Image"), System.Drawing.Image)
        Me.btnCategoryClear.Location = New System.Drawing.Point(198, 5)
        Me.btnCategoryClear.Name = "btnCategoryClear"
        Me.btnCategoryClear.Size = New System.Drawing.Size(21, 15)
        Me.btnCategoryClear.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.btnCategoryClear, "Clear search")
        Me.btnCategoryClear.UseVisualStyleBackColor = False
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
        'grdDrugs
        '
        Me.grdDrugs.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdDrugs.BackgroundColor = System.Drawing.Color.White
        Me.grdDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdDrugs.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdDrugs.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDrugs.CaptionForeColor = System.Drawing.Color.White
        Me.grdDrugs.CaptionVisible = False
        Me.grdDrugs.DataMember = ""
        Me.grdDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdDrugs.GridLineColor = System.Drawing.Color.Black
        Me.grdDrugs.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdDrugs.HeaderForeColor = System.Drawing.Color.White
        Me.grdDrugs.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdDrugs.Location = New System.Drawing.Point(0, 0)
        Me.grdDrugs.Name = "grdDrugs"
        Me.grdDrugs.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdDrugs.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdDrugs.ReadOnly = True
        Me.grdDrugs.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdDrugs.SelectionForeColor = System.Drawing.Color.Black
        Me.grdDrugs.Size = New System.Drawing.Size(494, 432)
        Me.grdDrugs.TabIndex = 4
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(720, 53)
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
        Me.ts_ViewButtons.Size = New System.Drawing.Size(720, 53)
        Me.ts_ViewButtons.TabIndex = 5
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
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.grdDrugs)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(223, 83)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(497, 435)
        Me.Panel2.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 431)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(492, 1)
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
        Me.Label2.Size = New System.Drawing.Size(1, 431)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(493, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 431)
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
        Me.Label4.Size = New System.Drawing.Size(494, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlTopRight)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 53)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(720, 30)
        Me.Panel3.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(220, 83)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 435)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.txtSearch)
        Me.Panel4.Controls.Add(Me.Label77)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.btnClear)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.Black
        Me.Panel4.Location = New System.Drawing.Point(77, 1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(241, 22)
        Me.Panel4.TabIndex = 45
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 17)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(214, 5)
        Me.Label77.TabIndex = 43
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.White
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(5, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(214, 3)
        Me.Label12.TabIndex = 37
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(1, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(4, 22)
        Me.Label13.TabIndex = 38
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
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
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 22)
        Me.Label14.TabIndex = 39
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(240, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 22)
        Me.Label15.TabIndex = 40
        Me.Label15.Text = "label4"
        '
        'frmVWDrugs
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(720, 518)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWDrugs"
        Me.Text = "Drugs"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDrugs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public NodeID As Integer = 1
    ''NodeID  = 1  ''All 
    ''NodeID  = 2  ''Clinical
    ''NodeID  = 3  ''Non Clinical
    Public NodeName As String
    Private objclsDrugs As New clsDrugs
    Public Shared blnModify As Boolean
    Dim _blnSearch As Boolean = True

    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String '[
    Dim strsortorder As String

    Private Sub frmVWDrugs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            '' Initially Fill Clincal Drugs 
            grdDrugs.DataSource = objclsDrugs.GetAllDrugs(2)  '' For Showing All-Clinical Drugs 
            objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
            '            CustomGridStyle()
            CustomTreeView()
            'CCHIT 08 - by default search the drug on drug name
            ' lblSearch.Text = "Drug Name"
            'CCHIT 08
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CustomTreeView()
        Dim newNode As New TreeNode
        Dim objMyTreeView As myTreeNode
        Dim objchild As myTreeNode
        'Dim i As Integer
        trvDrugs.Nodes.Clear()

        objMyTreeView = New myTreeNode("Drugs", 0)
        objMyTreeView.ImageIndex = 0
        objMyTreeView.SelectedImageIndex = 0
        trvDrugs.Nodes.Add(objMyTreeView)
        'For i = 0 To objclsCPT.GetAllCategory.Table.Rows.Count - 1
        Dim ValueMember As Int64
        Dim DisplayMember As String

        ValueMember = 2
        DisplayMember = "Practice Favorites"
        objchild = New myTreeNode(DisplayMember, ValueMember)
        objMyTreeView.Nodes.Add(objchild)
        trvDrugs.SelectedNode = objchild

        '' By Mahesh - 20070124
        ValueMember = 4
        DisplayMember = "Allergic Drugs"
        objchild = New myTreeNode(DisplayMember, ValueMember)
        objMyTreeView.Nodes.Add(objchild)
        ''''''
        '' By Pradeep -20100610
        'ValueMember = 5
        'DisplayMember = "Beers List"
        'objchild = New myTreeNode(DisplayMember, ValueMember)
        'objMyTreeView.Nodes.Add(objchild)

        ValueMember = 1
        DisplayMember = "All Drugs"
        objchild = New myTreeNode(DisplayMember, ValueMember)
        objMyTreeView.Nodes.Add(objchild)

        'Next
        trvDrugs.ExpandAll()

        If IsNothing(objclsDrugs.GetDataview) = False Then
            objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
        End If
    End Sub

    Private Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Dim dv As DataView
        dv = objclsDrugs.GetDataview()
        Dim ts As New clsDataGridTableStyle(dv.Table.TableName)

        objclsDrugs.SortDataview(dv.Table.Columns(1).ColumnName)

        Dim IDCol As New DataGridTextBoxColumn
        IDCol.Width = 0
        IDCol.MappingName = dv.Table.Columns(0).ColumnName
        IDCol.HeaderText = "ID"

        Dim NameCol As New DataGridTextBoxColumn
        With NameCol
            .Width = grdDrugs.Width * 0.2
            .MappingName = dv.Table.Columns(1).ColumnName
            .HeaderText = "Drug Name"
            .NullText = ""
        End With

        Dim GenNameCol As New DataGridTextBoxColumn
        With GenNameCol
            .Width = grdDrugs.Width * 0.1
            .MappingName = dv.Table.Columns(2).ColumnName
            .HeaderText = "Generic Name"
            .NullText = ""
        End With

        Dim DosageCol As New DataGridTextBoxColumn
        With DosageCol
            .Width = grdDrugs.Width * 0.1
            .MappingName = dv.Table.Columns(3).ColumnName
            .HeaderText = "Dosage"
            .NullText = ""
        End With

        Dim RouteCol As New DataGridTextBoxColumn
        With RouteCol
            .Width = grdDrugs.Width * 0.08
            .MappingName = dv.Table.Columns(4).ColumnName
            .HeaderText = "Route"
            .NullText = ""
        End With

        Dim FrequencyCol As New DataGridTextBoxColumn
        With FrequencyCol
            .Width = grdDrugs.Width * 0.07
            .MappingName = dv.Table.Columns(5).ColumnName
            .HeaderText = "Frequency"
            .NullText = ""
        End With

        Dim DurationCol As New DataGridTextBoxColumn
        With DurationCol
            .Width = grdDrugs.Width * 0.07
            .MappingName = dv.Table.Columns(6).ColumnName
            .HeaderText = "Duration"
            .NullText = ""
        End With
        Dim AmountCol As New DataGridTextBoxColumn
        With AmountCol
            .Width = grdDrugs.Width * 0.06
            .MappingName = dv.Table.Columns(8).ColumnName
            .HeaderText = "Quantity"
            .NullText = ""
        End With
        Dim ClinicDrugCol As New DataGridBoolColumn
        With ClinicDrugCol
            .Width = grdDrugs.Width * 0.08
            .MappingName = dv.Table.Columns(7).ColumnName
            'This code changed by Mayuri:20091027
            'Replaced "Is Clinic" by "Practice Favorites"
            .HeaderText = "Practice Favorites"
            'End code Added by Mayuri:20091027
            .NullText = ""
        End With

        Dim AllergicDrugCol As New DataGridBoolColumn
        With AllergicDrugCol
            .Width = grdDrugs.Width * 0.07
            .MappingName = dv.Table.Columns("bIsAllergicDrug").ColumnName
            .HeaderText = "Is Allergic"
            .NullText = ""
        End With

        Dim NDCCodeCol As New DataGridTextBoxColumn
        With NDCCodeCol
            .Width = grdDrugs.Width * 0.1
            .MappingName = dv.Table.Columns("NDCCode").ColumnName
            .HeaderText = "NDC Code"
            .NullText = ""
        End With
        'code added  on 10/06/2010 and modified in 6030 to show checkbox instead of text
        'Dim BeersListCol As New DataGridBoolColumn
        'With BeersListCol
        '    .Width = grdDrugs.Width * 0.07
        '    .MappingName = dv.Table.Columns("DrugType").ColumnName
        '    .HeaderText = "Beers List"
        '    .NullText = ""

        'End With

        'code added in 6030 to show column DEA Class 
        Dim DEAClassCol As New DataGridTextBoxColumn
        With DEAClassCol
            .Width = grdDrugs.Width * 0.08
            .MappingName = dv.Table.Columns("nIsNarcotics").ColumnName
            .HeaderText = "DEA Class"
            .NullText = ""
        End With

        Dim AlternativeFormID As New DataGridTextBoxColumn
        With AlternativeFormID
            .Width = 0
            .MappingName = dv.Table.Columns("AlternativeFormID").ColumnName
            .HeaderText = "AlternativeFormID"
            .NullText = ""
        End With

        '''''''Code is added by Anil on 02/11/2007
        txtSearch.Text = ""
        txtSearch.Text = strsearchtxt
        If strcolumnName = "" Then
            objclsDrugs.SortDataview(dv.Table.Columns(1).ColumnName)
        Else
            Dim strColumn As String = Replace(strcolumnName, "[", "")

            objclsDrugs.SortDataview(strColumn, strSortBy)
        End If
        ''''''''''''''''''''''''''''''''
        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, NameCol, GenNameCol, DosageCol, RouteCol, FrequencyCol, DurationCol, AmountCol, ClinicDrugCol, AllergicDrugCol, NDCCodeCol, DEAClassCol, AlternativeFormID})
        grdDrugs.TableStyles.Clear()
        grdDrugs.TableStyles.Add(ts)

    End Sub


    Private Sub grdDrugs_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDrugs.CurrentCellChanged
        ''''Commented by Anil on 20071102
        ''Try
        ''    Select Case grdDrugs.CurrentCell.ColumnNumber
        ''        Case 1
        ''            lblSearch.Text = "Drug Name"
        ''        Case 2
        ''            lblSearch.Text = "Generic Name"
        ''        Case 3
        ''            lblSearch.Text = "Dosage"
        ''        Case 4
        ''            lblSearch.Text = "Route"
        ''        Case 5
        ''            lblSearch.Text = "Frequency"
        ''        Case 6
        ''            lblSearch.Text = "Duration"
        ''        Case 8
        ''            lblSearch.Text = "Dispense"
        ''    End Select
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub


    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(grdDrugs.DataSource(), DataView)

                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                grdDrugs.DataSource = dvPatient
                Dim strPatientSearchDetails As String
                If Trim(txtSearch.Text) <> "" Then

                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''Sandip Darade 20090512
                    ''If search string starts with '*' char then repalce all '*' chars except the one at start  
                    If (strPatientSearchDetails.StartsWith("*") = True) Then

                        strPatientSearchDetails = Replace(strPatientSearchDetails, "*", "") & ""
                        strPatientSearchDetails = "*" + strPatientSearchDetails
                    Else
                        strPatientSearchDetails = Replace(strPatientSearchDetails, "*", "") & ""
                    End If
                    'strPatientSearchDetails = Replace(txtSearch.Text, "*", "")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)

                Else
                    strPatientSearchDetails = ""
                End If
                'SHUBHANGI 20090930
                'Apply General Search 
                dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(4).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(5).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(6).ColumnName & " Like '" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(8).ColumnName & " Like '" & strPatientSearchDetails & "%'"

                'Shubhangi 20091202
                'Set focus on first line from the filtered line.
                If dvPatient.Count > 0 Then
                    grdDrugs.Select(0)
                End If
                'COMMENTED BY SHUBHANGI 20090930 


                'Select Case Trim(lblSearch.Text)
                '    Case "Drug Name"
                '        'commented by Mayuri:20090904
                '        'For In String Search
                '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        'Else
                '        'dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        'End If
                '    Case "Generic Name"
                '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        'Else
                '        'dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        'End If
                '    Case "Dosage"
                '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        'Else
                '        'dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        'End If
                '    Case "Route"
                '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        'Else
                '        'dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        'End If
                '    Case "Frequency"
                '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(5).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        'Else
                '        'dvPatient.RowFilter = dvPatient.Table.Columns(5).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        'End If
                '    Case "Duration"
                '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(6).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        ' Else
                '        'dvPatient.RowFilter = dvPatient.Table.Columns(6).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        'End If
                '    Case "Dispense"
                '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(8).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        'Else
                '        'dvPatient.RowFilter = dvPatient.Table.Columns(8).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        'End If
                'End Select

                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Query, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    ''Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles txtSearch.TextChanged
    ''    'Try
    ''    '    objclsDrugs.Search(grdDrugs.DataSource, 1, txtSearch.Text)
    ''    'Catch ex As Exception
    ''    '    MessageBox.Show(ex.ToString, "Drugs", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    'End Try

    ''    Try
    ''        If Not IsNothing(objclsDrugs.GetDataview) Then
    ''            'Check if sort column is not of type boolean
    ''            Dim Search As String
    ''            Search = Replace(Trim(txtSearch.Text), "'", "''")

    ''            objclsDrugs.SetRowFilter(Trim(Search))

    ''            'HideColumn()
    ''        End If
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.Message, "Drugs", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try
    ''End Sub


    Private Sub trvDrugs_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvDrugs.AfterSelect
        Try
            Dim objMyTreeView As myTreeNode

            If IsNothing(trvDrugs.SelectedNode) Then
                Exit Sub
            End If

            'If IsNothing(trvDrugs.SelectedNode.Parent) Then
            '    Exit Sub
            'End If

            objMyTreeView = CType(e.Node, myTreeNode)
            ' Dim NodeID As Integer
            NodeID = objMyTreeView.Key
            NodeName = objMyTreeView.NodeName

            If NodeID <> 0 Then
                grdDrugs.DataSource = objclsDrugs.GetAllDrugs(NodeID)
            End If

            If IsNothing(objclsDrugs.GetDataview) = False Then
                objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
            End If
            grdDrugs.CaptionText = "Drugs - " & NodeName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try

    End Sub
    Private Sub UpdateDrugs()
        Dim ID As Long
        Dim AlternativeFormID As Int32 = 0
        Dim objfrmMSTDrugs As frmMSTDrugs
        Dim strcolumnName As String = String.Empty
        Dim strsortorder As String = String.Empty
        Try
            If grdDrugs.VisibleRowCount >= 1 Then
                blnModify = True
                ID = grdDrugs.Item(grdDrugs.CurrentRowIndex, 0).ToString
                If Not IsDBNull(grdDrugs.Item(grdDrugs.CurrentRowIndex, 12)) Then
                    AlternativeFormID = Convert.ToInt32(grdDrugs.Item(grdDrugs.CurrentRowIndex, 12))
                End If

                Dim grdIndex As Integer = grdDrugs.CurrentRowIndex

                objfrmMSTDrugs = New frmMSTDrugs(ID, AlternativeFormID)
                objfrmMSTDrugs.Text = "Modify Drugs"
                objfrmMSTDrugs.ShowDialog(IIf(IsNothing(objfrmMSTDrugs.Parent), Me, objfrmMSTDrugs.Parent))
                If objfrmMSTDrugs.CancelClick = False Then
                    grdDrugs.DataSource = objclsDrugs.GetAllDrugs(objfrmMSTDrugs._DrugCategory)
                    'objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)

                    '''''Code is added by Anil 20071102
                    Dim myDataView As DataView = CType(grdDrugs.DataSource, DataView)
                    Dim i As Integer
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = myDataView.Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        ''''''''''''
                        '' To Remember the Selection of Row 


                        For i = 0 To trvDrugs.Nodes(0).GetNodeCount(False) - 1
                            Dim CategoryNode As myTreeNode
                            CategoryNode = trvDrugs.Nodes(0).Nodes(i)
                            If objfrmMSTDrugs._DrugCategory = CategoryNode.Key Then
                                trvDrugs.SelectedNode = CategoryNode
                                Exit For
                            End If
                        Next

                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                    End If
                    Dim myDatagView As DataView = CType(grdDrugs.DataSource, DataView)
                    If (IsNothing(myDatagView) = False) Then


                        For i = 0 To myDatagView.Count - 1
                            If ID = grdDrugs.Item(i, 0) Then
                                grdDrugs.CurrentRowIndex = i
                                grdDrugs.Select(i)
                                Exit For
                            End If
                        Next
                    End If

                    'Else
                    '    grdDrugs.Select(grdIndex)
                End If
                objfrmMSTDrugs.Dispose()
                objfrmMSTDrugs = Nothing
            End If
            'CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmMSTDrugs = Nothing
        End Try
    End Sub

    Private Sub grdDrugs_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDrugs.DoubleClick
        'Try
        '    UpdateDrugs()
        'Catch ex As Exception
        'End Try
        '' to O
        'Dim ID As Long
        'Dim objfrmMSTDrugs As frmMSTDrugs
        'Try
        '    If grdDrugs.VisibleRowCount >= 1 Then
        '        blnModify = True
        '        ID = grdDrugs.Item(grdDrugs.CurrentRowIndex, 0).ToString
        '        Dim grdIndex As Integer = grdDrugs.CurrentRowIndex

        '        objfrmMSTDrugs = New frmMSTDrugs(ID)
        '        objfrmMSTDrugs.Text = "Modify Drugs"
        '        objfrmMSTDrugs.ShowDialog(Me)
        '        If objfrmMSTDrugs.CancelClick = False Then
        '            grdDrugs.DataSource = objclsDrugs.GetAllDrugs(NodeID)
        '            objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
        '        End If

        '        grdDrugs.Select(grdIndex)

        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "Drugs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    objfrmMSTDrugs = Nothing
        'End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdDrugs.CurrentRowIndex >= 0 Then
                    grdDrugs.Select(0)
                    grdDrugs.CurrentRowIndex = 0
                End If
            End If
            ''Sandip Darade 20090512
            ''commented to allow '-','%'
            ' ''--Added by Anil on 20071213
            'mdlGeneral.ValidateText(txtSearch.Text, e)
            ' ''----
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdDrugs_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDrugs.MouseUp
        Try
            If grdDrugs.CurrentRowIndex >= 0 Then
                grdDrugs.Select(grdDrugs.CurrentRowIndex)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub grdDrugs_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDrugs.KeyUp
        Try
            If grdDrugs.CurrentRowIndex >= 0 Then
                grdDrugs.Select(grdDrugs.CurrentRowIndex)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txtCategorySearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCategorySearch.TextChanged
        Try
            If Trim(txtCategorySearch.Text) <> "" Then
                If trvDrugs.Nodes(0).GetNodeCount(False) > 0 Then
                    Dim mychildnode As TreeNode
                    ''''child node collection

                    For Each mychildnode In trvDrugs.Nodes(0).Nodes
                        Dim str As String
                        str = UCase(Trim(mychildnode.Text))
                        If Mid(str, 1, Len(Trim(txtCategorySearch.Text))) = UCase(Trim(txtCategorySearch.Text)) Then
                            '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                            trvDrugs.SelectedNode = trvDrugs.SelectedNode.LastNode
                            '*************
                            trvDrugs.SelectedNode = mychildnode
                            txtCategorySearch.Focus()
                            Exit Sub
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCategorySearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCategorySearch.KeyPress
        If trvDrugs.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvDrugs.Select()
                'Else
                '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
            End If
        End If
    End Sub

    Private Sub frmVWDrugs_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub


    '''''''''Event is added by Anil on 20071102
    Private Sub grdDrugs_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDrugs.MouseDoubleClick
        'COMMENTED BY SHUBHANGI 20090930
        'INSTEAD OF COLUMN WISE, APPLY GENERAL SEARCH
        'Added on 20091028:by Mayuri
        'To fix Bug:#4525
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdDrugs.HitTest(ptPoint)
            '    If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

            '        Select Case htInfo.Column
            '            Case 1
            '                lblSearch.Text = "Drug Name"
            '            Case 2
            '                lblSearch.Text = "Generic Name"
            '            Case 3
            '                lblSearch.Text = "Dosage"
            '            Case 4
            '                lblSearch.Text = "Route"
            '            Case 5
            '                lblSearch.Text = "Frequency"
            '            Case 6
            '                lblSearch.Text = "Duration"
            '            Case 7                  '\\20090128 suraj-> change 8 to 7 for Dispense col
            '                lblSearch.Text = "Dispense"
            '        End Select

            '        If txtSearch.Text = "" Then
            '            _blnSearch = True
            '        Else
            '            _blnSearch = False
            '            txtSearch.Text = ""
            '            _blnSearch = True
            '        End If

            If htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                UpdateDrugs()
            End If
            'End code Added by Mayuri:20091028

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '''''''''''''''''''''''''

    Private Sub AddCategory()
        Dim objfrmMSTDrugs As New frmMSTDrugs
        Try
            blnModify = False
            objfrmMSTDrugs.Text = "Add New Drugs"
            objfrmMSTDrugs.ShowDialog(IIf(IsNothing(objfrmMSTDrugs.Parent), Me, objfrmMSTDrugs.Parent))

            If objfrmMSTDrugs.CancelClick = False Then
                grdDrugs.DataSource = objclsDrugs.GetAllDrugs(NodeID)
                objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
                Dim myDataView As DataView = CType(grdDrugs.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim i As Integer
                    For i = 0 To myDataView.Table.Rows.Count - 1
                        If objfrmMSTDrugs._DrugName = grdDrugs.Item(i, 1) Then
                            grdDrugs.CurrentRowIndex = i
                            grdDrugs.Select(i)
                            Exit For
                        End If
                    Next
                End If

            End If
            objfrmMSTDrugs.Dispose()
            objfrmMSTDrugs = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmMSTDrugs = Nothing
        End Try
    End Sub
    Private Sub UpdateCategory()
        Try
            UpdateDrugs()
        Catch ex As Exception
        End Try


        ''Dim ID As Long
        ''Dim objfrmMSTDrugs As frmMSTDrugs
        ''Try
        ''    If grdDrugs.VisibleRowCount >= 1 Then
        ''        blnModify = True
        ''        ID = grdDrugs.Item(grdDrugs.CurrentRowIndex, 0).ToString
        ''        Dim grdIndex As Integer = grdDrugs.CurrentRowIndex

        ''        objfrmMSTDrugs = New frmMSTDrugs(ID)
        ''        objfrmMSTDrugs.Text = "Modify Drugs"
        ''        objfrmMSTDrugs.ShowDialog(Me)
        ''        If objfrmMSTDrugs.CancelClick = False Then
        ''            grdDrugs.DataSource = objclsDrugs.GetAllDrugs(NodeID)
        ''            objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
        ''        End If
        ''        grdDrugs.Select(grdIndex)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, "Drugs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    objfrmMSTDrugs = Nothing
        ''End Try
    End Sub
    Private Sub DeleteCategory()
        Dim ID As Long
        'Dim DrugName As String
        Dim m_Drugtype As String = 0
        Dim m_sOldNDC As String = ""
        Try
            If grdDrugs.VisibleRowCount >= 1 Then
                'blnModify = True

                ''check first this drugs NDC code not in used in [Tables = Prescription, medication, DrugProviderAssociation] if it is in used don't delete 20090107
                m_sOldNDC = grdDrugs.Item(grdDrugs.CurrentRowIndex, 10).ToString
                'variable for deleting beers list table entry
                m_Drugtype = grdDrugs.Item(grdDrugs.CurrentRowIndex, 11).ToString
                If objclsDrugs.CheckNDCinUsed(m_sOldNDC) Then
                    ''Drug in used asked user to delete.... changed message case no GLO2009-0003851
                    If (MessageBox.Show("There are transactions present for this drug. You will no longer be able to prescribe or refill this drug. Do you still want to delete it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes) Then
                        'Yes 'delete drug from Drug mst
                        If (m_Drugtype <> "Beers List") Then
                            objclsDrugs.DeleteDrugAgainstNDC(m_sOldNDC)
                        Else
                            objclsDrugs.DeleteDrugAgainstNDC(m_sOldNDC, 1)
                        End If


                        ID = grdDrugs.Item(grdDrugs.CurrentRowIndex, 0).ToString
                        grdDrugs.DataSource = objclsDrugs.GetAllDrugs(NodeID)
                        objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
                        ''''''Code is Added by Anil 0n 20071102
                        Dim myDataView As DataView = CType(grdDrugs.DataSource, DataView)
                        If (IsNothing(myDataView) = False) Then


                            sortOrder = myDataView.Sort
                            strSearchstring = txtSearch.Text.Trim
                            arrcolumnsort = Split(sortOrder, "]")
                            If arrcolumnsort.Length > 1 Then
                                strcolumnName = arrcolumnsort.GetValue(0)
                                strsortorder = arrcolumnsort.GetValue(1)
                            End If
                            CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        End If
                    End If
                Else
                    ''Drug Not in used So delete the drug. changed message case no GLO2009-0003851
                    If MessageBox.Show("Are you sure you want to delete this drug?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        ID = grdDrugs.Item(grdDrugs.CurrentRowIndex, 0).ToString
                        ''DrugName = grdDrugs.Item(grdDrugs.CurrentRowIndex, 1).ToString
                        ''objclsDrugs.DeleteDrug(ID, DrugName)
                        If (m_Drugtype <> "Beers List") Then
                            objclsDrugs.DeleteDrugAgainstNDC(m_sOldNDC)
                        Else
                            objclsDrugs.DeleteDrugAgainstNDC(m_sOldNDC, 1)
                        End If
                        'objclsDrugs.DeleteDrugAgainstNDC(m_sOldNDC) ''Delete drug from drug MST against NDC-code
                        grdDrugs.DataSource = objclsDrugs.GetAllDrugs(NodeID)
                        objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
                        ''''''Code is Added by Anil 0n 20071102
                        Dim myDataView As DataView = CType(grdDrugs.DataSource, DataView)
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
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Try
            'if condition added by dipak to fix bug no :4721 :-Dashboard -> Edit - Drugs
            If (NodeID = 0) Then
                NodeID = 1
            End If
            'end addition by dipak 20091103
            grdDrugs.DataSource = objclsDrugs.GetAllDrugs(NodeID)
            objclsDrugs.SortDataview(objclsDrugs.GetDataview.Table.Columns(1).ColumnName)
            txtSearch.Clear()
            txtCategorySearch.Clear()
            _blnSearch = True
            'grdDrugs.DataSource = Nothing
            'trvDrugs.CollapseAll()
            'trvDrugs.Focus()
            'trvDrugs.ExpandAll()
            Dim mynode As myTreeNode
            If trvDrugs.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvDrugs.Nodes.Item(0).Nodes.Item(0)
                trvDrugs.SelectedNode = mynode
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                If trvDrugs.SelectedNode.Text <> "Drugs" Then '''''resolved bug 5254
                    Call AddCategory()
                End If
            Case "Modify"
                If trvDrugs.SelectedNode.Text <> "Drugs" Then '''''resolved bug 5254
                    Call UpdateCategory()
                End If
            Case "Delete"
                If trvDrugs.SelectedNode.Text <> "Drugs" Then '''''resolved bug 5254
                    Call DeleteCategory()
                End If
            Case "Refresh"
                If trvDrugs.SelectedNode.Text <> "Drugs" Then '''''resolved bug 5254
                    Call RefreshCategory()
                End If
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930
        'Use Clear button to clear the search text box.
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub btnCategoryClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCategoryClear.Click
        'SHUBHANGI 20091001
        'Use Clear button to clear the search text box in tree view.
        txtCategorySearch.ResetText()
        txtCategorySearch.Focus()
        trvDrugs.SelectedNode = trvDrugs.Nodes(0)
        ' trvDrugs.Focus()
    End Sub

    'Shubhangi 20091202
    'Add this event coz this event is fire after load so at that time form's size is maximize so the column size become appropriate according to actual size of control
    Private Sub frmVWDrugs_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        CustomGridStyle()
    End Sub

    Private Sub ts_btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnDelete.Click

    End Sub
End Class
