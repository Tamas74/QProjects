Public Class frmVWOBPlan
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

                If (IsNothing(dgHistory) = False) Then
                    dgHistory.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgHistory)
                    dgHistory.Dispose()
                    dgHistory = Nothing
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
    Friend WithEvents dgHistory As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
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
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents cmnuAddCategory As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnCategoryClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Friend WithEvents imgHistory As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWOBPlan))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLeftTop = New System.Windows.Forms.Panel()
        Me.trvCategory = New System.Windows.Forms.TreeView()
        Me.imgHistory = New System.Windows.Forms.ImageList(Me.components)
        Me.Label16 = New System.Windows.Forms.Label()
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
        Me.dgHistory = New System.Windows.Forms.DataGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
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
        Me.Panel3.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgHistory, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTopRight.Controls.Add(Me.Panel3)
        Me.pnlTopRight.Controls.Add(Me.Label9)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label5)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(842, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.txtSearch)
        Me.Panel3.Controls.Add(Me.Label77)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(69, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(241, 22)
        Me.Panel3.TabIndex = 44
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 1
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
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(5, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(214, 3)
        Me.Label10.TabIndex = 37
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(1, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(4, 22)
        Me.Label11.TabIndex = 38
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
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 22)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Location = New System.Drawing.Point(240, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 22)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "label4"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(65, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label9.Size = New System.Drawing.Size(4, 20)
        Me.Label9.TabIndex = 47
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(64, 20)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(840, 1)
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
        Me.Label7.Location = New System.Drawing.Point(841, 1)
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
        Me.Label8.Size = New System.Drawing.Size(842, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Controls.Add(Me.pnlSearch)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 83)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(245, 435)
        Me.pnlLeft.TabIndex = 2
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeftTop.Controls.Add(Me.trvCategory)
        Me.pnlLeftTop.Controls.Add(Me.Label16)
        Me.pnlLeftTop.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_RightBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_TopBrd)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 26)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlLeftTop.Size = New System.Drawing.Size(245, 409)
        Me.pnlLeftTop.TabIndex = 1
        '
        'trvCategory
        '
        Me.trvCategory.BackColor = System.Drawing.Color.White
        Me.trvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCategory.ForeColor = System.Drawing.Color.Black
        Me.trvCategory.HideSelection = False
        Me.trvCategory.ImageIndex = 1
        Me.trvCategory.ImageList = Me.imgHistory
        Me.trvCategory.ItemHeight = 19
        Me.trvCategory.Location = New System.Drawing.Point(4, 6)
        Me.trvCategory.Name = "trvCategory"
        Me.trvCategory.SelectedImageIndex = 1
        Me.trvCategory.ShowLines = False
        Me.trvCategory.Size = New System.Drawing.Size(240, 399)
        Me.trvCategory.TabIndex = 3
        '
        'imgHistory
        '
        Me.imgHistory.ImageStream = CType(resources.GetObject("imgHistory.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgHistory.TransparentColor = System.Drawing.Color.Transparent
        Me.imgHistory.Images.SetKeyName(0, "OBPlan.ico")
        Me.imgHistory.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgHistory.Images.SetKeyName(2, "History.ico")
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(4, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(240, 5)
        Me.Label16.TabIndex = 44
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 405)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(240, 1)
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
        Me.lbl_RightBrd.Location = New System.Drawing.Point(244, 1)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(242, 1)
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
        Me.pnlSearch.Size = New System.Drawing.Size(245, 26)
        Me.pnlSearch.TabIndex = 0
        '
        'txtCategorySearch
        '
        Me.txtCategorySearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCategorySearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCategorySearch.ForeColor = System.Drawing.Color.Black
        Me.txtCategorySearch.Location = New System.Drawing.Point(32, 5)
        Me.txtCategorySearch.Name = "txtCategorySearch"
        Me.txtCategorySearch.Size = New System.Drawing.Size(191, 15)
        Me.txtCategorySearch.TabIndex = 2
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
        Me.btnCategoryClear.Location = New System.Drawing.Point(223, 5)
        Me.btnCategoryClear.Name = "btnCategoryClear"
        Me.btnCategoryClear.Size = New System.Drawing.Size(21, 15)
        Me.btnCategoryClear.TabIndex = 47
        Me.ToolTip1.SetToolTip(Me.btnCategoryClear, "Clear search")
        Me.btnCategoryClear.UseVisualStyleBackColor = False
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(212, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(212, 2)
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
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(240, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(240, 1)
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
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(244, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'dgHistory
        '
        Me.dgHistory.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgHistory.BackgroundColor = System.Drawing.Color.White
        Me.dgHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgHistory.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgHistory.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgHistory.CaptionForeColor = System.Drawing.Color.White
        Me.dgHistory.CaptionVisible = False
        Me.dgHistory.DataMember = ""
        Me.dgHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgHistory.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgHistory.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgHistory.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgHistory.HeaderForeColor = System.Drawing.Color.White
        Me.dgHistory.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgHistory.Location = New System.Drawing.Point(0, 1)
        Me.dgHistory.Name = "dgHistory"
        Me.dgHistory.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgHistory.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgHistory.ReadOnly = True
        Me.dgHistory.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgHistory.SelectionForeColor = System.Drawing.Color.Black
        Me.dgHistory.Size = New System.Drawing.Size(597, 431)
        Me.dgHistory.TabIndex = 4
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(848, 53)
        Me.pnlToolStrip.TabIndex = 0
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_gloCommunityDownload, Me.ts_btnRefresh, Me.ts_btnClose})
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
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.dgHistory)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(248, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(600, 435)
        Me.Panel1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 432)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(595, 0)
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
        Me.Label3.Location = New System.Drawing.Point(596, 1)
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
        Me.Label4.Size = New System.Drawing.Size(597, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(848, 30)
        Me.Panel2.TabIndex = 1
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(245, 83)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 435)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'cmnuAddCategory
        '
        Me.cmnuAddCategory.Name = "cmnuAddCategory"
        Me.cmnuAddCategory.Size = New System.Drawing.Size(61, 4)
        '
        'frmVWOBPlan
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(848, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWOBPlan"
        Me.Text = "OB Plan"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private key As Int64
    Private objOBPlanDBLayer As New clsOBPlanDBLayer
    Private strHistory As String
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String

    Private Sub frmVWHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mynode As myTreeNode

        Try

            ts_gloCommunityDownload.Visible = False


            Me.Cursor = Cursors.WaitCursor
            FillTreeView()
            dgHistory.AllowSorting = True
            trvCategory.ExpandAll()

            If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                key = mynode.Key
                trvCategory.SelectedNode = mynode
                BindGrid()

            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub BindGrid(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")

        Try

            If key <> -1 Then
                objOBPlanDBLayer.FetchData(key)
                dgHistory.SetDataBinding(objOBPlanDBLayer.DsDataview, "")

                objOBPlanDBLayer.SortDataview(objOBPlanDBLayer.DsDataview.Table.Columns(1).ColumnName)

                txtSearch.Text = ""
                txtSearch.Text = strsearchtxt

                If strcolumnName = "" Then
                    objOBPlanDBLayer.SortDataview(objOBPlanDBLayer.DsDataview.Table.Columns(1).ColumnName)
                Else
                    Dim strColumn As String = Replace(strcolumnName, "[", "")

                    objOBPlanDBLayer.SortDataview(strColumn, strSortBy)
                End If
            
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateHistory()

        Dim frm As frmMSTOBPlan
        Dim blnIsSystemDefined As Boolean

        If strHistory <> "Medical Condition" And strHistory <> "Coded History" Then

            If key <> -1 Then
                If dgHistory.VisibleRowCount >= 1 Then

                    '13-May-15 Aniket: Resolving Bug #83176: EMR: OB plan- Application should not allow user to delete system defined items
                    If CType(dgHistory.Item(dgHistory.CurrentRowIndex, 4), Boolean) = True Then
                        '15-May-15 Aniket: Resolving Bug #83344: EMR: OB plan - As user double click on Item from master form, application should not give message that user cannot modify
                        'MessageBox.Show("You cannot modify Items from System defined categories.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Return
                        blnIsSystemDefined = True
                    End If


                    Dim ID As Long
                    ID = CType(dgHistory.Item(dgHistory.CurrentRowIndex, 0), Long)
                    Dim grdIndex As Integer = dgHistory.CurrentRowIndex

                    Dim myDataView As DataView = CType(dgHistory.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = CType(dgHistory.DataSource, DataView).Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                    End If

                    ''''''''''''''''''
                    frm = New frmMSTOBPlan(key, ID)
                    frm.IsSystemDefined = blnIsSystemDefined
                    frm.Text = "Update OB Plan"
                    frm._SelectedCategoty = strHistory
                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    If frm._DialogResult = Windows.Forms.DialogResult.OK Then
                        BindGrid(strcolumnName, strsortorder, strSearchstring)
                        '''' 
                        Dim i As Integer

                        For i = 0 To trvCategory.Nodes(0).GetNodeCount(False) - 1
                            Dim CategoryNode As myTreeNode
                            CategoryNode = trvCategory.Nodes(0).Nodes(i)
                            If frm._CategoryID = CategoryNode.Key Then
                                trvCategory.SelectedNode = CategoryNode
                                Exit For
                            End If
                        Next
                        Dim myDatagView As DataView = CType(dgHistory.DataSource, DataView)
                        If (IsNothing(myDatagView) = False) Then


                            For i = 0 To CType(dgHistory.DataSource, DataView).Count - 1
                                If ID = dgHistory.Item(i, 0) Then
                                    dgHistory.CurrentRowIndex = i
                                    'Sanjog- Added on 2011 March 23 to unselet the first row when we modifies the record on serched Data Grid
                                    dgHistory.UnSelect(0)
                                    'Sanjog- Added on 2011 March 23 to unselet the first row when we modifies the record on serched Data Grid
                                    dgHistory.Select(i)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    frm.Dispose()
                    frm = Nothing
                End If
            End If
        Else
            MessageBox.Show("You cannot modify Items from System defined categories", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub HideColumn()

        '07-May-15 Aniket: Resolving Bug #83078: OB PLAN - Go - OB Plan - Exception thrown
        If IsNothing(objOBPlanDBLayer.DsDataview) = False Then


            Dim ts As New clsDataGridTableStyle(objOBPlanDBLayer.DsDataview.Table.TableName)

            Dim dgID As New DataGridTextBoxColumn

            With dgID
                .MappingName = objOBPlanDBLayer.DsDataview.Table.Columns(0).ColumnName
                .Alignment = HorizontalAlignment.Center
                .NullText = ""
                .Width = 0
            End With

            Dim dgCol1 As New DataGridTextBoxColumn
            With dgCol1
                .MappingName = objOBPlanDBLayer.DsDataview.Table.Columns(1).ColumnName
                .HeaderText = "Description"
                .NullText = ""
                .Width = 0.25 * dgHistory.Width
            End With

            Dim dgCol2 As New DataGridTextBoxColumn
            With dgCol2
                .MappingName = objOBPlanDBLayer.DsDataview.Table.Columns(2).ColumnName
                .HeaderText = "Comments"
                .NullText = ""
                .Width = 0.55 * dgHistory.Width
            End With
            'Sanjog - Added on 2011 march 23 to add column for Concept ID
            Dim dgCol3 As New DataGridTextBoxColumn
            With dgCol3
                .MappingName = objOBPlanDBLayer.DsDataview.Table.Columns(3).ColumnName
                .HeaderText = "Concept ID"
                .NullText = ""
                .Width = 0
            End With
            'Sanjog - Added on 2011 march 23 to add column for Concept ID

            'Sanjog - Added on 2011 Sept 26 to add column for Is SystemCategory
            Dim dgCol4 As New DataGridTextBoxColumn
            With dgCol4
                .MappingName = objOBPlanDBLayer.DsDataview.Table.Columns(4).ColumnName
                .HeaderText = "System Category"
                .NullText = ""
                .Width = 0
            End With
            'Sanjog - Added on 2011 Sept 26 to add column for Is SystemCategory
            ''chetan added for snomed changes 8020 
            Dim dgCol5 As New DataGridTextBoxColumn
            With dgCol5
                .MappingName = objOBPlanDBLayer.DsDataview.Table.Columns(5).ColumnName
                .HeaderText = "Snomed"
                .NullText = ""
                .Width = 0
            End With
            ''chetan added for snomed changes 8020 
            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3, dgCol4, dgCol5})
            dgHistory.TableStyles.Clear()
            dgHistory.TableStyles.Add(ts)

        End If

    End Sub

    Private Sub dgHistory_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgHistory.CurrentCellChanged
       
        If gblnResetSearchTextBox = True Then
            txtCategorySearch.ResetText()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(dgHistory.DataSource(), DataView)

                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                dgHistory.DataSource = dvPatient
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
                'USE GENERAL SEARCH INSTEAD OF COLUMN WISE SEARCH
                'Sanjog - Added on 2011 march to Fillter with Concept Id also
                dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                & dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' "
                'Sanjog - Added on 2011 march to Fillter with Concept Id also
                'Shubhangi 20091202
                'Set focus on first record of filtrated
                If dvPatient.Count > 0 Then
                    dgHistory.Select(0)
                End If

              
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub trvCategory_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCategory.AfterSelect
        Try

            txtSearch.Text = ""
            Dim mynode As myTreeNode = Nothing
            If Not IsNothing(e.Node) Then
                If Not mynode Is trvCategory.Nodes.Item(0) Then
                    mynode = CType(e.Node, myTreeNode)
                    key = mynode.Key
                    If key <> -1 Then
                        strHistory = mynode.Text
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
                        dgHistory.DataSource = Nothing
                    End If
                Else
                    dgHistory.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub dgHistory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgHistory.MouseUp
        If dgHistory.CurrentRowIndex >= 0 Then
            dgHistory.Select(dgHistory.CurrentRowIndex)
        End If
    End Sub

    Private Sub FillTreeView()

        Dim dc As DataTable
        Dim mynode As myTreeNode
        Dim mychildnode As myTreeNode
        Dim i As Integer
        Dim key As Int64
        Dim strname As String

        Try

            dc = objOBPlanDBLayer.FillControls


            mynode = New myTreeNode("OB Plan", -1)
            mynode.ImageIndex = 0
            mynode.SelectedImageIndex = 0
            trvCategory.Nodes.Add(mynode)

            For i = 0 To dc.Rows.Count - 1
                key = dc.Rows.Item(i)(0)
                strname = dc.Rows.Item(i)(1)
                mychildnode = New myTreeNode(strname, key)
                mychildnode.ImageIndex = 1
                mychildnode.SelectedImageIndex = 1
                mynode.Nodes.Add(mychildnode)
            Next

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

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
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtCategorySearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCategorySearch.KeyPress
        Try
            If trvCategory.GetNodeCount(False) > 0 Then
                If (e.KeyChar = ChrW(13)) Then
                    trvCategory.Select()
                    
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If dgHistory.CurrentRowIndex >= 0 Then
                    dgHistory.Select(0)
                    dgHistory.CurrentRowIndex = 0
                End If
            End If
            mdlGeneral.ValidateText(txtSearch.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

  
    '''''''Code/Event is added by Anil on 20071102
    Private Sub dgHistory_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgHistory.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgHistory.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

               

                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If

            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                UpdateHistory()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''''''''''''''''''''''
    Private Sub AddCategory()
        Try
            If strHistory <> "Medical Condition" And strHistory <> "Coded History" Then

                If key <> -1 Then
                    Dim frm As frmMSTOBPlan
                    frm = New frmMSTOBPlan(key)
                    'frm.Text = "Add History for " & strHistory
                    frm.Text = "Add OB Plan"
                    frm._SelectedCategoty = strHistory
                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    If frm._DialogResult = Windows.Forms.DialogResult.OK Then
                        BindGrid()
                        Dim myDataView As DataView = CType(dgHistory.DataSource, DataView)
                        If (IsNothing(myDataView) = False) Then


                            Dim i As Integer
                            For i = 0 To myDataView.Table.Rows.Count - 1
                                If frm._Description = dgHistory.Item(i, 1) Then
                                    dgHistory.CurrentRowIndex = i
                                    dgHistory.Select(i)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    frm.Dispose()
                    frm = Nothing
                End If
            Else
                MessageBox.Show("You cannot Add Items to System defined categories", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateCategory()
        Try

            UpdateHistory()

           
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteCategory()
        Try
            If strHistory <> "Medical Condition" And strHistory <> "Coded History" Then

                If key <> -1 Then
                    If dgHistory.VisibleRowCount >= 1 Then
                        Dim IsSys As Boolean = False
                        IsSys = CType(dgHistory.Item(dgHistory.CurrentRowIndex, 4), Boolean)
                        If IsSys Then
                            MessageBox.Show("You cannot Delete Items from System defined categories", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            If MessageBox.Show("Are you sure you want to delete this Record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                Dim ID As Long
                                ID = CType(dgHistory.Item(dgHistory.CurrentRowIndex, 0), Long)
                                Try
                                    'If CheckIsHistoryItemExistsinPortal(ID) Then
                                    '    Exit Sub
                                    'End If
                                    objOBPlanDBLayer.DeleteData(ID)
                                    ''''''Code is Added by Anil 0n 20071102
                                    Dim myDataView As DataView = CType(dgHistory.DataSource, DataView)
                                    If (IsNothing(myDataView) = False) Then


                                        sortOrder = CType(dgHistory.DataSource, DataView).Sort
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
                                    MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End Try

                            End If
                        End If
                    End If
                End If
            Else
                MessageBox.Show("You cannot Delete Items from System defined categories", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Function CheckIsHistoryItemExistsinPortal(ByVal HistoryID As Long) As Boolean
    '    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
    '    Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
    '    Dim _dt As DataTable = Nothing
    '    Dim _dt2 As DataTable = Nothing
    '    Try

    '        oParameters = New gloDatabaseLayer.DBParameters()
    '        oParameters.Add("@nhistoryid", HistoryID, ParameterDirection.Input, SqlDbType.BigInt)
    '        oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)

    '        oDB.Connect(False)
    '        oDB.Retrive("WS_IsHistoryItemExistsinHealthform", oParameters, _dt)
    '        If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then

    '            If MessageBox.Show("Selected history category item is used in patient portal forms. Once this history category item is deleted then patients can no longer see this history category item in patient portal forms." + System.Environment.NewLine + System.Environment.NewLine + "Do you want to continue with the deletion?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
    '                Try
    '                    ' Set IsRepublish Required to 1 & Delete Entry 
    '                    oParameters = New gloDatabaseLayer.DBParameters()
    '                    oParameters.Add("@nhistoryid", HistoryID, ParameterDirection.Input, SqlDbType.BigInt)
    '                    oParameters.Add("@IsDelete", True, ParameterDirection.Input, SqlDbType.Bit)
    '                    oDB.Connect(False)
    '                    oDB.Retrive("WS_IsHistoryItemExistsinHealthform", oParameters, _dt2)
    '                Catch ex As Exception
    '                End Try
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        Else
    '            Return False
    '        End If


    '    Catch dbEx As gloDatabaseLayer.DBException
    '        dbEx.ERROR_Log(dbEx.ToString())
    '        Throw dbEx
    '    Finally
    '        If oParameters IsNot Nothing Then
    '            oDB.Disconnect()
    '            oParameters.Dispose()
    '        End If
    '        If oDB IsNot Nothing Then
    '            oDB.Dispose()
    '        End If

    '        If Not IsNothing(_dt) Then
    '            _dt.Dispose()
    '            _dt = Nothing
    '        End If

    '        If Not IsNothing(_dt2) Then
    '            _dt2.Dispose()
    '            _dt2 = Nothing
    '        End If
    '    End Try

    '    Return False

    'End Function

    Private Sub RefreshCategory()
        Try
            If key <> -1 Then
                Me.Cursor = Cursors.WaitCursor

                BindGrid()
            End If
            '''''Following code lines are addded by Anil 0n 28/09/07 at 04:17 p.m.
            '''''This code clears the search textboxes, gets the focus on the Node(0) of theTreeView. 
            txtSearch.Clear()
            txtSearch.Focus()
            txtCategorySearch.Clear()
            dgHistory.DataSource = Nothing
            _blnSearch = True
            Dim mynode As myTreeNode
            If trvCategory.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvCategory.Nodes.Item(0).Nodes.Item(0)
                key = mynode.Key
                trvCategory.SelectedNode = mynode
                BindGrid()
            End If
            '''''added upto here
        Catch ex As Exception
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Private Sub AddHistoryCategory(ByVal sender As Object, ByVal e As EventArgs)
        'code added by dipak 20090909  for add category in history type
        Dim frm As New CategoryMaster
        Dim nd As TreeNode
        Try
            frm.Text = "Add OB Plan Category"

            'frm.cmbCategoryType.Text = "History"
            'frm.cmbCategoryType.Enabled = False
            frm.IsfromHistoryMaster = True
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
            MessageBox.Show(ex.Message, "OB Plan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            frm = Nothing
        End Try
        'end dipak
    End Sub

    Private Sub trvCategory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCategory.MouseDown
        'code added by dipak 20090909 for show context meny for treeview trvCategory 
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
        oMenuItem.Text = "Add OB Plan Category"
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
        AddHandler oMenuItem.Click, AddressOf AddHistoryCategory
        oMenuItem = Nothing
        cmnuAddCategory = Nothing
        'end dipak
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930
        'Us to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub btnCategoryClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCategoryClear.Click

        'Us to clear search text box in tree view
        txtCategorySearch.ResetText()
        txtCategorySearch.Focus()

        '04-May-15 Aniket: Resolving Bug #82512: gloEMR: Category- As user click on clear button, application should not refresh whole page
        'trvCategory.SelectedNode = trvCategory.Nodes(0)

    End Sub

    'Shubhangi 20091202
    'Add this event coz this event is fire after load so at that time form's size is maximize so the column size become appropriate according to actual size of control
    Private Sub frmVWHistory_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        HideColumn()
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
