Public Class frmVWReferrals
    Inherits System.Windows.Forms.Form
    Implements IPatientContext
    Implements IHotKey

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
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
            If (IsNothing(ReferralsDBLayer) = False) Then
                ReferralsDBLayer.Dispose()
                ReferralsDBLayer = Nothing
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
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Private WithEvents tsb_referal As System.Windows.Forms.ToolStripDropDownButton
    Private WithEvents imgList_Common As System.Windows.Forms.ImageList
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents c1Referrals As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWReferrals))
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsb_referal = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.c1Referrals = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.imgList_Common = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.c1Referrals, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(842, 24)
        Me.pnlTopRight.TabIndex = 1
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
        Me.panel4.TabIndex = 50
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
        Me.btnClear.BackColor = System.Drawing.Color.White
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
        Me.btnClear.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = False
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
        Me.lblSearch.AutoSize = True
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(64, 20)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(840, 1)
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
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(841, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
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
        Me.Label4.Size = New System.Drawing.Size(842, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_referal, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(848, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tsb_referal
        '
        Me.tsb_referal.Image = CType(resources.GetObject("tsb_referal.Image"), System.Drawing.Image)
        Me.tsb_referal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsb_referal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_referal.Name = "tsb_referal"
        Me.tsb_referal.Size = New System.Drawing.Size(46, 50)
        Me.tsb_referal.Tag = "Add"
        Me.tsb_referal.Text = "New"
        Me.tsb_referal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.Panel1.Controls.Add(Me.c1Referrals)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(848, 435)
        Me.Panel1.TabIndex = 0
        '
        'c1Referrals
        '
        Me.c1Referrals.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Referrals.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1Referrals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Referrals.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Referrals.ColumnInfo = "1,0,0,0,0,95,Columns:"
        Me.c1Referrals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Referrals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Referrals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Referrals.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1Referrals.Location = New System.Drawing.Point(4, 1)
        Me.c1Referrals.Name = "c1Referrals"
        Me.c1Referrals.Rows.Count = 1
        Me.c1Referrals.Rows.DefaultSize = 19
        Me.c1Referrals.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Referrals.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Referrals.ShowCellLabels = True
        Me.c1Referrals.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1Referrals.Size = New System.Drawing.Size(840, 430)
        Me.c1Referrals.StyleInfo = resources.GetString("c1Referrals.StyleInfo")
        Me.c1Referrals.TabIndex = 15
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 431)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(840, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 431)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(844, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 431)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(842, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(848, 30)
        Me.Panel2.TabIndex = 0
        '
        'imgList_Common
        '
        Me.imgList_Common.ImageStream = CType(resources.GetObject("imgList_Common.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_Common.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_Common.Images.SetKeyName(0, "Past Exam.ico")
        Me.imgList_Common.Images.SetKeyName(1, "date.ico")
        '
        'frmVWReferrals
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(848, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWReferrals"
        Me.Text = "Referrals"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.c1Referrals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim WithEvents frmExamChild As IExamChildEvents
    Private ReferralsDBLayer As New ClsReferralsDBLayer
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim _PatientID As Long
    'column constant added by dipak 20100106 to open referral letters from view->referals
    Const COL_VISIT_ID = 6
    Const COL_EXAM_ID = 7
    Const COL_EXAM_PROVIDER_ID = 8
    Const COL_ISEXAM_FINISH = 5
    Const COL_REFERRAL_ID = 0
    Const COL_TEMPLATENAME = 3
    'end code added by dipak

    Dim Col_ReferralID As Integer = 0
    Dim Col_Referral_Date As Integer = 1
    Dim Col_Referral_Name As Integer = 2
    Dim Col_Template_Name As Integer = 3
    Dim Col_ExamName As Integer = 4
    Dim Col_IsFinished As Integer = 5
    Dim Col_VisitID As Integer = 6
    Dim Col_ExamID As Integer = 7
    Dim Col_ExamProviderID As Integer = 8
    Dim Col_Count As Integer = 9
    Dim ind As Integer = -1
    'Private Sub BindGridOld(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
    '    Try
    '        ReferralsDBLayer.FetchData(_PatientID)
    '        If Not IsNothing(ReferralsDBLayer.DsDataview) Then
    '            dgReferrals.SetDataBinding(ReferralsDBLayer.DsDataview, "")
    '            ReferralsDBLayer.SortDataview(ReferralsDBLayer.DsDataview.Table.Columns(2).ColumnName)
    '            '''''''Code is added by Anil on 02/11/2007
    '            txtSearch.Text = ""
    '            txtSearch.Text = strsearchtxt
    '            If strcolumnName = "" Then
    '                ReferralsDBLayer.SortDataview(ReferralsDBLayer.DsDataview.Table.Columns(2).ColumnName)
    '            Else
    '                Dim strColumn As String = Replace(strcolumnName, "[", "")

    '                ReferralsDBLayer.SortDataview(strColumn, strSortBy)
    '            End If
    '            ''''''''''''''''''''''''''''''''
    '            HideColumn()
    '        End If
    '    Catch ex As SqlClient.SqlException
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub BindGrid(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try
            ReferralsDBLayer.FetchData(_PatientID)
            If Not IsNothing(ReferralsDBLayer.DsDataview) Then
                c1Referrals.DataSource = ReferralsDBLayer.DsDataview
                ReferralsDBLayer.SortDataview(ReferralsDBLayer.DsDataview.Table.Columns(2).ColumnName)
                '''''''Code is added by Anil on 02/11/2007
                txtSearch.Text = ""
                txtSearch.Text = strsearchtxt
                If strcolumnName = "" Then
                    ReferralsDBLayer.SortDataview(ReferralsDBLayer.DsDataview.Table.Columns(2).ColumnName)
                Else
                    Dim strColumn As String = Replace(strcolumnName, "[", "")

                    ReferralsDBLayer.SortDataview(strColumn, strSortBy)
                End If
                ''''''''''''''''''''''''''''''''
                SetGridStyle()
                '' BindGridOld()
            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub HideColumn()
    '    Dim ts As New clsDataGridTableStyle(ReferralsDBLayer.DsDataview.Table.TableName)

    '    Dim dgID As New DataGridTextBoxColumn

    '    With dgID
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(0).ColumnName
    '        .Alignment = HorizontalAlignment.Center
    '        .NullText = "Referral ID"
    '        .Width = 0
    '    End With

    '    Dim dgCol1 As New DataGridTextBoxColumn
    '    With dgCol1
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(1).ColumnName
    '        .HeaderText = "Referral Date"
    '        .NullText = ""
    '        .Width = dgReferrals.Width / 5
    '    End With

    '    Dim dgCol2 As New DataGridTextBoxColumn
    '    With dgCol2
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(2).ColumnName
    '        .HeaderText = "Referral Name"
    '        .NullText = ""
    '        .Width = dgReferrals.Width / 5
    '    End With

    '    Dim dgCol3 As New DataGridTextBoxColumn
    '    With dgCol3
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(3).ColumnName
    '        .HeaderText = "Template Name"
    '        .NullText = ""
    '        .Width = dgReferrals.Width / 5
    '    End With
    '    'Shubhangi 200910007
    '    'Add 2 columns Exam Name & Stratus of exam in view referrals Screen

    '    Dim dgCol4 As New DataGridTextBoxColumn
    '    With dgCol4
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(4).ColumnName
    '        .HeaderText = "ExamName"
    '        .NullText = ""
    '        .Width = dgReferrals.Width / 5
    '    End With

    '    Dim dgCol5 As New DataGridTextBoxColumn
    '    With dgCol5
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(5).ColumnName
    '        .HeaderText = "IsFinished"
    '        .NullText = ""
    '        .Width = dgReferrals.Width / 5
    '    End With
    '    ''new  column  added by dipak 20100106 to open referral letters from view->referals
    '    Dim dgCol6 As New DataGridTextBoxColumn
    '    With dgCol6
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(6).ColumnName
    '        .HeaderText = "VisitID"
    '        .NullText = ""
    '        .Width = 0
    '    End With
    '    Dim dgCol7 As New DataGridTextBoxColumn
    '    With dgCol7
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(7).ColumnName
    '        .HeaderText = "ExamID"
    '        .NullText = ""
    '        .Width = 0
    '    End With
    '    Dim dgCol8 As New DataGridTextBoxColumn
    '    With dgCol8
    '        .MappingName = ReferralsDBLayer.DsDataview.Table.Columns(8).ColumnName
    '        .HeaderText = "ExamProviderID"
    '        .NullText = ""
    '        .Width = 0
    '    End With
    '    'end code by dipak 201006
    '    ts.GridColumnStyles.Clear()
    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3, dgCol4, dgCol5, dgCol6, dgCol7, dgCol8})
    '    dgReferrals.TableStyles.Clear()
    '    dgReferrals.TableStyles.Add(ts)

    'End Sub
    Public Sub SetGridStyle()
        Try





            With c1Referrals
                .AllowSorting = True


                .Redraw = False
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = Screen.PrimaryScreen.WorkingArea.Width - 60
                c1Referrals.Width = _TotalWidth
                ' c1Disclosure.Height = Me.Height - 20
                c1Referrals.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                .Cols.Count = Col_Count
                .Rows.Fixed = 1
                .Styles.ClearUnused()
                .AllowResizing = True

                .Cols(Col_ReferralID).Width = _TotalWidth * 0
                .Cols(Col_ReferralID).AllowEditing = False
                .Cols(Col_ReferralID).Visible = False
                .Cols(Col_ReferralID).Caption = "Referral ID"
                .Cols(Col_ReferralID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_Referral_Date).Width = _TotalWidth / 5
                .Cols(Col_Referral_Date).AllowEditing = False
                .Cols(Col_Referral_Date).Visible = True
                .Cols(Col_Referral_Date).Caption = "Referral Date"
                .Cols(Col_Referral_Date).Format = "MM/dd/yyyy h:mm tt"
                .Cols(Col_Referral_Date).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                .Cols(Col_Referral_Name).Width = _TotalWidth / 5
                .Cols(Col_Referral_Name).AllowEditing = False
                .Cols(Col_Referral_Name).Visible = True
                .Cols(Col_Referral_Name).Caption = "Referral Name"
                .Cols(Col_Referral_Name).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_Template_Name).Width = _TotalWidth / 5
                .Cols(Col_Template_Name).AllowEditing = False
                .Cols(Col_Template_Name).Visible = True
                .Cols(Col_Template_Name).Caption = "Template Name"
                .Cols(Col_Template_Name).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_ExamName).Width = _TotalWidth / 5
                .Cols(Col_ExamName).AllowEditing = False
                .Cols(Col_ExamName).Visible = True
                .Cols(Col_ExamName).Caption = "Exam Name"
                .Cols(Col_ExamName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_IsFinished).Width = _TotalWidth / 5
                .Cols(Col_IsFinished).AllowEditing = False
                .Cols(Col_IsFinished).Visible = True
                .Cols(Col_IsFinished).Caption = "Is Finished"
                .Cols(Col_IsFinished).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_VisitID).Width = _TotalWidth * 0
                .Cols(Col_VisitID).AllowEditing = False
                .Cols(Col_VisitID).Visible = False
                .Cols(Col_VisitID).Caption = "VisitID"
                .Cols(Col_VisitID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_ExamID).Width = _TotalWidth * 0
                .Cols(Col_ExamID).AllowEditing = False
                .Cols(Col_ExamID).Visible = False
                .Cols(Col_ExamID).Caption = "ExamID"
                .Cols(Col_ExamID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_ExamProviderID).Width = _TotalWidth * 0
                .Cols(Col_ExamProviderID).AllowEditing = False
                .Cols(Col_ExamProviderID).Visible = False
                .Cols(Col_ExamProviderID).Caption = "ExamProviderID"
                .Cols(Col_ExamProviderID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Redraw = True
            End With
        Catch ex As Exception
        End Try

    End Sub
    Private Sub frmVWReferrals_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub frmVWReferrals_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtSearch.Focus()
        'Line commented as we are not using gnPatientID in local scope.
        '_PatientID = gnPatientID
        Try
            Me.Cursor = Cursors.WaitCursor
            BindGrid()
            AddCategory()
            'btnAdd.Visible = False
            'btnUpdate.Visible = False
            'Sanjog - Added on 2011 May 17 for Patient Safety
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Sanjog - Added on 2011 May 17 for Patient Safety
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Dim dvReferrals As DataView = Nothing
            'ReferralsDBLayer.SetRowFilter(1, txtSearch.Text)
            If Not IsNothing(c1Referrals.DataSource) Then

                dvReferrals = CType(c1Referrals.DataSource, DataView)
                ' dvReferrals.RowFilter = dvReferrals.Table.Columns()
                'Dim strRecordSearch As String
                'str = ReferralsDBLayer.DsDataview.Sort
                'str = Mid(str, 2)
                'str = Mid(str, 1, Len(str) - 1)
                Dim strReferral As String = String.Empty
                If Trim(txtSearch.Text) <> "" Then
                    strReferral = Replace(txtSearch.Text, "'", "''")
                    strReferral = Replace(strReferral, "[", "") & ""
                    strReferral = mdlGeneral.ReplaceSpecialCharacters(strReferral)
                End If

                'Shubhangi 20091008
                'Use general search & in string search 
                dvReferrals.RowFilter = dvReferrals.Table.Columns(2).ColumnName & " Like '%" & strReferral & "%' OR " _
                                     & dvReferrals.Table.Columns(3).ColumnName & " Like '%" & strReferral & "%' OR " _
                                     & dvReferrals.Table.Columns(4).ColumnName & " Like '%" & strReferral & "%' OR " _
                                     & dvReferrals.Table.Columns(5).ColumnName & " Like '%" & strReferral & "%'"

                'Commented by shubhangi

                'SHUBHANGI 20090914 Search according to Selected column Header of data grid
                ' Dim lblRecordSearch As String
                'lblRecordSearch = lblSearch.Text
                'strRecordSearch = txtSearch.Text.Trim

                'Select Case lblRecordSearch.ToUpper.Trim

                '    'For referrals
                '    Case "Search:".ToUpper.Trim
                '        '''''''''''''''''''Code lines below are added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                '        Dim strReferral As String
                '        If Trim(txtSearch.Text) <> "" Then
                '            strReferral = Replace(txtSearch.Text, "'", "''")
                '            strReferral = Replace(strReferral, "[", "") & ""
                '            strReferral = mdlGeneral.ReplaceSpecialCharacters(strReferral)
                '        Else
                '            strReferral = ""
                '        End If
                '        ''''''''''''''''''''''''''''
                '        ' If str = "Referral" Then
                '        ReferralsDBLayer.SetRowFilter(2, Trim(strReferral))     'Sort for referrals
                '        HideColumn()

                '        'For template 
                '    Case "Template".ToUpper.Trim
                '        Dim strTemplate As String
                '        If Trim(txtSearch.Text) <> "" Then
                '            strTemplate = Replace(txtSearch.Text, "'", "''")
                '            strTemplate = Replace(strTemplate, "[", "") & ""
                '            strTemplate = mdlGeneral.ReplaceSpecialCharacters(strTemplate)
                '        Else
                '            strTemplate = ""
                '        End If
                ' ReferralsDBLayer.SetRowFilter(3, Trim(strTemplate))    'Search for Templates



                'End Select
            End If
            c1Referrals.DataSource = dvReferrals
            ' HideColumn()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'End Shubhangi

    'Private Sub dgReferrals_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    If dgReferrals.CurrentRowIndex >= 0 Then
    '        dgReferrals.Select(dgReferrals.CurrentRowIndex)
    '    End If
    'End Sub

    'Private Sub dgReferrals_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    ''sudhir 20081208
    '    Try

    '        'SHUBHANGI 20090914 
    '        'For checking Header name of column
    '        Dim ptPoint As Point = New Point(e.X, e.Y)
    '        Dim htInfo As DataGrid.HitTestInfo = dgReferrals.HitTest(ptPoint)

    '        If htInfo.Type = DataGrid.HitTestType.ColumnHeader Or htInfo.Type = DataGrid.HitTestType.None Then 'HitTestTypeEnum.ColumnHeader Then


    '            Exit Sub
    '        End If

    '        'End Shubhangi
    '        'code added by dipak 20100106 to open referral letters from view->referals
    '        Dim mgnVisitID As Int64 = dgReferrals.Item(htInfo.Row, COL_VISIT_ID) '40181955078867301 ' 983 '
    '        Dim examid As Int64 = dgReferrals.Item(htInfo.Row, COL_EXAM_ID) '401819677068313301 'dgReferrals.Item(1, 0)
    '        Dim ExamProviderId As Int64 = dgReferrals.Item(htInfo.Row, COL_EXAM_PROVIDER_ID) '1 'dgReferrals.Item(1, 0)
    '        Dim blnExamFinished As Boolean = False
    '        If (dgReferrals.Item(htInfo.Row, COL_ISEXAM_FINISH).ToString() = "YES") Then
    '            blnExamFinished = True
    '        End If
    '        Dim ReferralID As Long = dgReferrals.Item(htInfo.Row, COL_REFERRAL_ID)
    '        Dim TemplateName As String = dgReferrals.Item(htInfo.Row, COL_TEMPLATENAME)
    '        Try
    '            'Line Commented and modified by dipak 20100907 for case UC5070 
    '            'Dim frm As New frmSummaryofVisit(gnPatientID, mgnVisitID, examid, ExamNewDocumentName, ExamProviderId, blnExamFinished, True, "", ReferralID, TemplateName)
    '            Dim frm As New frmSummaryofVisit(_PatientID, mgnVisitID, examid, ExamNewDocumentName, ExamProviderId, blnExamFinished, True, "", ReferralID, TemplateName)
    '            frm.Text = "Patient Referrals"
    '            frmExamChild = frm
    '            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

    '            frm.Dispose()
    '            frm = Nothing

    '        Catch ex As Exception
    '            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '        'end code added by dipak 20100106


    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    ''sudhir 20081208
    'function is commented as not in use by dipak 20100906
    'Private Sub ViewReferral()
    '    Dim ReferralID As Long
    '    Dim objfrmRefView As frmPatientReferralView

    '    If dgReferrals.VisibleRowCount > 0 Then
    '        Dim grdIndex As Integer = dgReferrals.CurrentRowIndex

    '        ReferralID = dgReferrals.Item(grdIndex, 0).ToString

    '        objfrmRefView = New frmPatientReferralView(ReferralID, _PatientID)

    '        With objfrmRefView
    '            '.Text = "Modify Patient Letters"
    '            '.MdiParent = CType(Me.MdiParent, MainMenu)
    '            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '            '.WindowState = FormWindowState.Maximized
    '            .Show()
    '            '.BringToFront()
    '            '.wdPatientEducation.Open("E:\Developer Working Folder\Documents\Daily Task Sheet\2008 Dec\Internal_TaskListtemplate 01Dec2008.doc")
    '        End With
    '    End If
    'End Sub

    'Private Sub dgReferrals_MouseUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    'If dgReferrals.CurrentRowIndex >= 0 Then
    '    '    dgReferrals.Select(dgReferrals.CurrentRowIndex)
    '    'End If
    '    Try
    '        Dim ptPoint As Point = New Point(e.X, e.Y)
    '        Dim htInfo As DataGrid.HitTestInfo = dgReferrals.HitTest(ptPoint)
    '        If htInfo.Type = DataGrid.HitTestType.Cell Then
    '            dgReferrals.Select(htInfo.Row)
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    'function commented by dipak as not in use
    'Private Sub AddCategory_old()
    '    Try
    '        ''Sandip Darade 20100211  GLO2008-0002255  
    '        Dim oMenuItem As ToolStripMenuItem
    '        tsb_referal.DropDownItems.Clear()
    '        Dim cls As New ClsReferralsDBLayer()
    '        Dim dt As New DataTable
    '        ''dt = ReferralsDBLayer.GetPatientExams()
    '        If dt.Rows.Count > 1 Then

    '            For i As Int16 = 0 To dt.Rows.Count - 1
    '                oMenuItem = New ToolStripMenuItem
    '                With oMenuItem
    '                    .Text = Trim(dt.Rows(i)("ExamName"))
    '                    ''the  tag has  ExamId,ProviderID,Isfinished resp.
    '                    .Tag = Convert.ToString(dt.Rows(i)("ExamID")) & "," & Convert.ToString(dt.Rows(i)("ProviderID")) & "," & Convert.ToString(dt.Rows(i)("IsFinished")) '' & "," _
    '                    .Image = imgList_Common.Images(0)
    '                    .ImageScaling = ToolStripItemImageScaling.None
    '                    .Font = New Font("Tahoma", 9, FontStyle.Regular)
    '                    .ForeColor = Color.FromArgb(31, 73, 125)
    '                    tsb_referal.DropDownItems.Add(oMenuItem)
    '                    AddHandler oMenuItem.Click, AddressOf Addreferral
    '                    oMenuItem = Nothing
    '                End With
    '            Next

    '        ElseIf dt.Rows.Count = 1 Then

    '            Dim ExamID As Int64 = Convert.ToInt64(dt.Rows(0)("ExamID"))
    '            Dim ExamProviderId As Int64 = Convert.ToInt64(dt.Rows(0)("ProviderID"))
    '            Dim blnExamFinished As Boolean = Convert.ToBoolean(dt.Rows(0)("IsFinished"))
    '            Dim ExamName As String = Convert.ToString(dt.Rows(0)("ExamName"))
    '            Dim VisitID As Int64 = GenerateVisitID(Date.Now, _PatientID)
    '            'line comented and modify by dipak 20100907 to replace gnPatientID by local variable
    '            'Dim frm As New frmSummaryofVisit(gnPatientID, VisitID, ExamID, ExamName, ExamProviderId, blnExamFinished, True)
    '            Dim frm As New frmSummaryofVisit(_PatientID, VisitID, ExamID, ExamName, ExamProviderId, blnExamFinished, True)
    '            'end modification by dipak 20100907 
    '            frm.Text = "Patient Referrals"
    '            frmExamChild = frm
    '            frm.ShowDialog(Me)
    '            RefreshCategory()

    '        ElseIf dt.Rows.Count < 1 Then

    '            MessageBox.Show("There are no exam for selected patient today. No referral can be created for the patient. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    ''Sandip Darade 20100302 
    ''as per new requirements for case GLO2008-0002255  user can create referral letter for patient if 
    ''patient  has atlest an exam irrespective of what service date the exam has
    Private Sub AddCategory()

        Try
            ''Sandip Darade 20100211  GLO2008-0002255  
            Dim oMenuItem As ToolStripMenuItem
            Dim oChildItem As ToolStripMenuItem
            tsb_referal.DropDownItems.Clear()
            ' Dim cls As New ClsReferralsDBLayer()
            Dim dt As DataTable = Nothing
            dt = ReferralsDBLayer.GetPatientExamsInformation(False, _PatientID)
            If dt.Rows.Count > 0 Then

                For i As Int16 = 0 To dt.Rows.Count - 1
                    oMenuItem = New ToolStripMenuItem
                    With oMenuItem
                        .Text = CType(dt.Rows(i)("ServiceDate"), Date).ToShortDateString()
                        .Tag = CType(dt.Rows(i)("ServiceDate"), Date).ToShortDateString()
                        .Image = imgList_Common.Images(1)
                        .ImageAlign = ContentAlignment.MiddleLeft
                        .ImageScaling = ToolStripItemImageScaling.None
                        ''.Font = New Font("Tahoma", 9, FontStyle.Regular)
                        .Font = gloGlobal.clsgloFont.gFont
                        .ForeColor = Color.FromArgb(31, 73, 125)
                    End With


                    Dim dtDetail As DataTable
                    dtDetail = ReferralsDBLayer.GetPatientExamsInformation(True, _PatientID, Convert.ToDateTime(dt.Rows(i)("ServiceDate")))
                    For j As Int16 = 0 To dtDetail.Rows.Count - 1
                        oChildItem = New ToolStripMenuItem
                        With oChildItem
                            .Text = Trim(dtDetail.Rows(j)("ExamName"))
                            ''the  tag has  ExamId,ProviderID,Isfinished,Visitid resp.
                            .Tag = Convert.ToString(dtDetail.Rows(j)("ExamID")) & "," & Convert.ToString(dtDetail.Rows(j)("ProviderID")) & "," & Convert.ToString(dtDetail.Rows(j)("IsFinished")) & "," & Convert.ToString(dtDetail.Rows(j)("VisitId"))
                            .Image = imgList_Common.Images(0)
                            .ImageScaling = ToolStripItemImageScaling.None
                            ''.Font = New Font("Tahoma", 9, FontStyle.Regular)
                            .Font = gloGlobal.clsgloFont.gFont
                            .ForeColor = Color.FromArgb(31, 73, 125)
                            oMenuItem.DropDownItems.Add(oChildItem)
                            AddHandler oChildItem.Click, AddressOf Addreferral
                            oChildItem = Nothing
                        End With
                    Next
                    tsb_referal.DropDownItems.Add(oMenuItem)
                    oMenuItem = Nothing
                Next

            End If
            dt.Dispose()
            dt = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Sandip Darade 20100211  GLO2008-0002255  
    Private Sub Addreferral(ByVal sender As System.Object, ByVal e As System.EventArgs)



        If Convert.ToString(DirectCast(sender, ToolStripItem).Tag) <> "" Then

            ''the  tag has  ExamId,ProviderID,Isfinished ,VisitID resp. 
            Dim strInfo As String() = Convert.ToString(DirectCast(sender, ToolStripItem).Tag).Split(",")
            Dim ExamID As Int64 = Convert.ToInt64(strInfo(0))
            Dim ExamProviderId As Int64 = Convert.ToInt64(strInfo(1))
            Dim blnExamFinished As Boolean = Convert.ToBoolean(strInfo(2))
            Dim VisitID As Int64 = Convert.ToInt64(strInfo(3))
            Dim ExamName As String = Convert.ToString(DirectCast(sender, ToolStripItem).Text)
            'line comented and modify by dipak 20100907 to replace gnPatientID by local variable
            'Dim frm As New frmSummaryofVisit(gnPatientID, VisitID, ExamID, ExamName, ExamProviderId, blnExamFinished, True)
            Dim frm As New frmSummaryofVisit(_PatientID, VisitID, True, ExamID, ExamName, ExamProviderId, blnExamFinished, True)
            'End Modification by dipak 20100907
            frm.isNewRefferalFromViewRefferal = True
            frm.Text = "Patient Referrals"
            frmExamChild = frm
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            If Not IsNothing(frm) Then   'Obj Disposed by Mitesh
                frm.Close()
            End If
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If

            RefreshCategory()



        End If

    End Sub
    Private Sub UpdateCategory()

        'If dgReferrals.VisibleRowCount >= 1 Then
        '    Dim frm As ReferralsMaster
        '    Dim ID As Long
        '    ID = CType(dgReferrals.Item(dgReferrals.CurrentRowIndex, 0), Long)
        '    frm = New ReferralsMaster(ID)
        '    frm.Text = "Update Referrals"
        '    frm.ShowDialog(Me)
        '    BindGrid()
        'End If

        ''Sandip Darade 20100130 Case no 0002255
        ''Open patient referral to modify   
        '' functionality to create new patient referral  from this screen is yet under discussion    
        Try
            If c1Referrals.Rows.Count > 1 Then

                If c1Referrals.RowSel >= 1 Then

                    Dim mgnVisitID As Int64 = c1Referrals.Item(c1Referrals.RowSel, COL_VISIT_ID)
                    Dim examid As Int64 = c1Referrals.Item(c1Referrals.RowSel, COL_EXAM_ID)
                    Dim ExamProviderId As Int64 = c1Referrals.Item(c1Referrals.RowSel, COL_EXAM_PROVIDER_ID)
                    Dim blnExamFinished As Boolean = False
                    If (c1Referrals.Item(c1Referrals.RowSel, COL_ISEXAM_FINISH).ToString() = "YES") Then
                        blnExamFinished = True
                    End If
                    Dim ReferralID As Long = c1Referrals.Item(c1Referrals.RowSel, COL_REFERRAL_ID)
                    Dim TemplateName As String = c1Referrals.Item(c1Referrals.RowSel, COL_TEMPLATENAME)
                    'line comented and modify by dipak 20100907 to replace gnPatientID by local variable
                    'Dim frm As New frmSummaryofVisit(gnPatientID, mgnVisitID, examid, ExamNewDocumentName, ExamProviderId, blnExamFinished, True, "", ReferralID, TemplateName)
                    Dim frm As New frmSummaryofVisit(_PatientID, mgnVisitID, True, examid, "", ExamProviderId, blnExamFinished, True, "", ReferralID, TemplateName)
                    'end modification by dipak 
                    frm.Text = "Patient Referrals"
                    frmExamChild = frm
                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    frm.Close()
                    frm.Dispose()
                    frm = Nothing

                Else
                    MessageBox.Show("Select a referral to modify.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
            End If


        Catch ex As Exception
            MessageBox.Show("Error while trying to modify referral.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DeleteCategory()
        Try
            If c1Referrals.Rows.Count > 1 Then

                If c1Referrals.RowSel <= 0 Then
                    Exit Sub
                End If
                'Shubhangi 20091012
                'Check whether selected record is not having status Finished
                If c1Referrals.Item(c1Referrals.RowSel, Col_IsFinished) = "Yes" Then
                    MsgBox("You are not able to delete the record having status finished.", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    Exit Sub
                End If

                '28-Apr-15 Aniket: Resolving Bug #81000: gloEMR: Referral Msg- Question mark should be present at the end of messag
                If MessageBox.Show("Are you sure you want to delete this record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    Dim ID As Long
                    ID = CType(c1Referrals.Item(c1Referrals.RowSel, 0), Long)
                    ReferralsDBLayer.DeleteData(ID, _PatientID)
                    ''''''Code is Added by Anil 0n 20071102
                    Dim myDataView As DataView = CType(c1Referrals.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = CType(c1Referrals.DataSource, DataView).Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If

                        BindGrid(strcolumnName, strsortorder, strSearchstring)
                        ''''''''''''''''''
                    End If
                End If
            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Try
            Me.Cursor = Cursors.WaitCursor
            BindGrid()
            txtSearch.Text = ""
            ''''''''''''added by Anil on 20071105
            If c1Referrals.Rows.Count > 1 Then
                c1Referrals.RowSel = 1
                c1Referrals.Select(1, 0)
            End If
            ''''''''''''''''''
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                ''          Call AddCategory()
                If (tsb_referal.DropDownItems.Count = 0) Then
                    MessageBox.Show("There are no exam for selected patient. No referral can be created for the patient. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Shubhangi 20091007
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmVWReferrals_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ''HideColumn()
        SetGridStyle()
    End Sub


    Private Sub frmExamChild_ActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Handles frmExamChild.ActivateExamChild

        UpdateVoiceLog("frmExamChild_ActivateExamChild started ")
        Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                CType(Me.MdiParent, MainMenu).MdiExamChildActivate(frmExamChild)
            End If
        Catch ex As Exception
            UpdateVoiceLog("Error - " & ex.ToString)
        End Try
        UpdateVoiceLog("frmExamChild_ActivateExamChild finished ")


    End Sub

    Private Sub frmExamChild_DeActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Handles frmExamChild.DeActivateExamChild

        UpdateVoiceLog("frmExamChild_DeActivateExamChild started ")
        Try

            CType(Me.MdiParent, MainMenu).MdiExamChildDeActivate(frmExamChild)

        Catch ex As Exception
            UpdateVoiceLog("Error - " & ex.ToString)
        End Try
        UpdateVoiceLog("frmExamChild_DeActivateExamChild finished ")


    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub c1Referrals_AfterSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1Referrals.AfterSort  ''added for bugid 72078
        Try
            If ind > -1 Then
                Dim rw As C1.Win.C1FlexGrid.Row
                For Each rw In c1Referrals.Rows
                    Dim cm As CurrencyManager = CType(BindingContext(Me.c1Referrals.DataSource), CurrencyManager)
                    Dim dr As DataRowView = CType(rw.DataSource, DataRowView)
                    If Not dr Is Nothing Then
                        Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)
                        If currIndex = ind Then
                            Dim cr As C1.Win.C1FlexGrid.CellRange = c1Referrals.GetCellRange(rw.Index, 1)
                            ' to scroll the selected row in the visible area
                            c1Referrals.Select(cr, True)
                            cr = c1Referrals.GetCellRange(rw.Index, 0, rw.Index, c1Referrals.Cols.Count - 1)
                            c1Referrals.Select(cr, False)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("View PatientLetter AfterSort " + ex.Message.ToString(), False)
        End Try
        ind = -1
    End Sub

    Private Sub c1Referrals_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Referrals.MouseClick
        Try
            If (Not IsNothing(c1Referrals.DataSource) AndAlso (c1Referrals.Rows.Count > 0)) Then   ''added for bugid 72078
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1Referrals.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(cm.Current, DataRowView)
                ind = dr.Row.Table.Rows.IndexOf(dr.Row)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("View Referrals MouseClick " + ex.Message.ToString(), False)
        End Try
    End Sub

    Private Sub c1Referrals_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Referrals.MouseDoubleClick
        Try

            'SHUBHANGI 20090914 
            'For checking Header name of column
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1Referrals.HitTest(ptPoint)

            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.ColumnHeader Or htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.None Then 'HitTestTypeEnum.ColumnHeader Then


                Exit Sub
            End If

            'End Shubhangi
            'code added by dipak 20100106 to open referral letters from view->referals
            Dim mgnVisitID As Int64 = c1Referrals.Item(htInfo.Row, COL_VISIT_ID) '40181955078867301 ' 983 '
            Dim examid As Int64 = c1Referrals.Item(htInfo.Row, COL_EXAM_ID) '401819677068313301 'dgReferrals.Item(1, 0)
            Dim ExamProviderId As Int64 = c1Referrals.Item(htInfo.Row, COL_EXAM_PROVIDER_ID) '1 'dgReferrals.Item(1, 0)
            Dim blnExamFinished As Boolean = False
            If (c1Referrals.Item(htInfo.Row, COL_ISEXAM_FINISH).ToString() = "YES") Then
                blnExamFinished = True
            End If
            Dim ReferralID As Long = c1Referrals.Item(htInfo.Row, COL_REFERRAL_ID)
            Dim TemplateName As String = c1Referrals.Item(htInfo.Row, COL_TEMPLATENAME)
            Try
                'Line Commented and modified by dipak 20100907 for case UC5070 
                'Dim frm As New frmSummaryofVisit(gnPatientID, mgnVisitID, examid, ExamNewDocumentName, ExamProviderId, blnExamFinished, True, "", ReferralID, TemplateName)
                Dim frm As New frmSummaryofVisit(_PatientID, mgnVisitID, True, examid, "", ExamProviderId, blnExamFinished, True, "", ReferralID, TemplateName)
                frm.Text = "Patient Referrals"
                frmExamChild = frm
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                If Not IsNothing(frm) Then   'Obj Disposed by Mitesh
                    frm.Close()
                End If
                If Not IsNothing(frm) Then
                    frm.Dispose()
                    frm = Nothing
                End If

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            'end code added by dipak 20100106


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        If strstring = "ON" Then
        ElseIf strstring = "OFF" Then
        Else
            For Each frm As Form In Application.OpenForms
                If frm.Name = "frmSummaryofVisit" Then
                    If Not IsNothing(DirectCast(frm, gloEMR.frmSummaryofVisit).oCurDoc) Then
                        Try
                            DirectCast(frm, gloEMR.frmSummaryofVisit).oCurDoc.ActiveWindow.SetFocus()
                            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=DirectCast(frm, gloEMR.frmSummaryofVisit).oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue, Replace:=Microsoft.Office.Interop.Word.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                            Exit For
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Send, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        End Try
                    End If
                End If
            Next
        End If

    End Sub


End Class
