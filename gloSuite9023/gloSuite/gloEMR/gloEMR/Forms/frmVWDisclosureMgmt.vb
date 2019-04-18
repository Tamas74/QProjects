Public Class frmVWDisclosureMgmt
    Inherits System.Windows.Forms.Form
    Implements IPatientContext

#Region " Windows Form Designer generated code "

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        _PatientID = PatientID
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
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
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
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents c1Disclosure As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWDisclosureMgmt))
        Me.pnlRight = New System.Windows.Forms.Panel()
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
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.c1Disclosure = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.c1Disclosure, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRight.Controls.Add(Me.panel4)
        Me.pnlRight.Controls.Add(Me.lblSearch)
        Me.pnlRight.Controls.Add(Me.Label1)
        Me.pnlRight.Controls.Add(Me.Label2)
        Me.pnlRight.Controls.Add(Me.Label3)
        Me.pnlRight.Controls.Add(Me.Label4)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(782, 24)
        Me.pnlRight.TabIndex = 2
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
        Me.panel4.Location = New System.Drawing.Point(61, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 22)
        Me.panel4.TabIndex = 56
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
        Me.btnClear.BackColor = System.Drawing.Color.Transparent
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
        Me.btnClear.TabIndex = 54
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
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(60, 20)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = " Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(780, 1)
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
        Me.Label3.Location = New System.Drawing.Point(781, 1)
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
        Me.Label4.Size = New System.Drawing.Size(782, 1)
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
        Me.pnlToolStrip.Size = New System.Drawing.Size(788, 53)
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
        Me.ts_ViewButtons.Size = New System.Drawing.Size(788, 53)
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
        Me.Panel1.Controls.Add(Me.c1Disclosure)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(788, 387)
        Me.Panel1.TabIndex = 12
        '
        'c1Disclosure
        '
        Me.c1Disclosure.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Disclosure.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1Disclosure.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Disclosure.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Disclosure.ColumnInfo = resources.GetString("c1Disclosure.ColumnInfo")
        Me.c1Disclosure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Disclosure.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Disclosure.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Disclosure.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1Disclosure.Location = New System.Drawing.Point(4, 1)
        Me.c1Disclosure.Name = "c1Disclosure"
        Me.c1Disclosure.Rows.Count = 1
        Me.c1Disclosure.Rows.DefaultSize = 19
        Me.c1Disclosure.Rows.Fixed = 0
        Me.c1Disclosure.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Disclosure.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Disclosure.ShowCellLabels = True
        Me.c1Disclosure.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1Disclosure.Size = New System.Drawing.Size(780, 382)
        Me.c1Disclosure.StyleInfo = resources.GetString("c1Disclosure.StyleInfo")
        Me.c1Disclosure.TabIndex = 12
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 383)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(780, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 383)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(784, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 383)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(782, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(788, 30)
        Me.Panel2.TabIndex = 13
        '
        'frmVWDisclosureMgmt
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(788, 470)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWDisclosureMgmt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Disclosure Management"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlRight.ResumeLayout(False)
        Me.pnlRight.PerformLayout()
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.c1Disclosure, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim _PatientID As Long
    Public Shared blnModify As Boolean
    Dim objclsDisclosure As New clsDisclosureMgmt
    'Dim dt As DataTable
    Dim dv As DataView
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim _blnAdd As Boolean
    Dim Col_Count As Integer = 5
    Dim Col_DisclosureID As Integer = 0
    Dim Col_DisclosureDate As Integer = 1
    Dim Col_TemplateID As Integer = 2
    Dim Col_DisclosureHeader As Integer = 3
    Dim Col_IsFinished As Integer = 4

    Private Sub btnCloseDisclosure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmVWDisclosure_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            Me.WindowState = FormWindowState.Maximized
            'If CType(Me.MdiParent, MainMenu).pnlLeft.Visible = False Then
            '    CType(Me.MdiParent, MainMenu).Splitter1.Visible = False
            'End If
            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            ''Above 2 lines commented by sudhir 20090202

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub frmVWDisclosure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        Try

            c1Disclosure.Enabled = False
            c1Disclosure.DataSource = objclsDisclosure.GetAllDisclosures(_PatientID)
            c1Disclosure.Enabled = True
            SetGridStyle()
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
    '    'Dim dt As DataTable
    '    dv = objclsDisclosure.GetDataView

    '    Dim ts As New clsDataGridTableStyle(dv.Table.TableName)

    '    Dim DisclosureIDCol As New DataGridTextBoxColumn
    '    With DisclosureIDCol
    '        .Width = 0
    '        .MappingName = dv.Table.Columns(0).ColumnName
    '        .HeaderText = "DisclosureID"
    '    End With

    '    Dim DateCol As New DataGridTextBoxColumn
    '    With DateCol
    '        .Width = 0.3 * grdDisclosure.Width
    '        .MappingName = dv.Table.Columns(1).ColumnName
    '        .HeaderText = "Disclosure Date"
    '        .NullText = ""
    '    End With

    '    Dim TempIDCol As New DataGridTextBoxColumn
    '    With TempIDCol
    '        .Width = 0
    '        .MappingName = dv.Table.Columns(2).ColumnName
    '        .HeaderText = "TemplateID"
    '        .NullText = ""
    '    End With

    '    Dim TempNameCol As New DataGridTextBoxColumn
    '    With TempNameCol
    '        .Width = 0.5 * grdDisclosure.Width
    '        .MappingName = dv.Table.Columns(3).ColumnName
    '        .HeaderText = "Disclosure Header"
    '        .NullText = ""
    '    End With

    '    Dim IsFinishedCol As New DataGridTextBoxColumn
    '    With IsFinishedCol
    '        .Width = 0.2 * grdDisclosure.Width
    '        .MappingName = dv.Table.Columns(4).ColumnName
    '        .HeaderText = "Is Finished"
    '        .NullText = ""
    '    End With

    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {DisclosureIDCol, DateCol, TempIDCol, TempNameCol, IsFinishedCol})
    '    grdDisclosure.TableStyles.Clear()
    '    grdDisclosure.TableStyles.Add(ts)

    '    '''''''Code is added by Anil on 20071105
    '    txtSearch.Text = ""
    '    txtSearch.Text = strsearchtxt
    '    If strcolumnName = "" Or IsNothing(strcolumnName) Then
    '        dv.Sort = "[" & dv.Table.Columns(3).ColumnName & "]" & strsortorder
    '    Else
    '        Dim strColumn As String = Replace(strcolumnName, "[", "")
    '        dv.Sort = "[" & strColumn & "]" & strSortBy
    '    End If
    '    ''''''''''''''''''''''''''''''''
    '    Dim dt As New DataTable
    '    dt = dv.ToTable()
    '    If (dt.Rows.Count >= 1) Then
    '        grdDisclosure.Select(0)
    '    End If
    'End Sub

    Private Sub SetGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        ''added to solve  sorting issue Bugid 72083
        Try

            Dim dv As DataView
            dv = objclsDisclosure.GetDataView

            c1Disclosure.DataSource = dv
            With c1Disclosure
                .AllowSorting = True


                .Redraw = False
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = Me.Width - 20
                c1Disclosure.Width = _TotalWidth
                ' c1Disclosure.Height = Me.Height - 20
                c1Disclosure.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing


                .Cols.Count = Col_Count
                .Rows.Fixed = 1


                .Styles.ClearUnused()

                .AllowResizing = True

                .Cols(Col_DisclosureID).Width = _TotalWidth * 0
                .Cols(Col_DisclosureID).AllowEditing = False
                .Cols(Col_DisclosureID).Visible = False
                .Cols(Col_DisclosureID).Caption = "DisclosureID"
                .Cols(Col_DisclosureID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_DisclosureDate).Width = _TotalWidth * 0.33
                .Cols(Col_DisclosureDate).AllowEditing = False
                .Cols(Col_DisclosureDate).Visible = True
                .Cols(Col_DisclosureDate).Caption = "Disclosure Date"
                .Cols(Col_DisclosureDate).DataType = GetType(System.DateTime)
                .Cols(Col_DisclosureDate).Format = "MM/dd/yyyy h:mm tt"
                .Cols(Col_DisclosureDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_TemplateID).Width = _TotalWidth * 0
                .Cols(Col_TemplateID).AllowEditing = False
                .Cols(Col_TemplateID).Visible = False
                .Cols(Col_TemplateID).Caption = "TemplateID"
                .Cols(Col_TemplateID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_DisclosureHeader).Width = _TotalWidth * 0.33
                .Cols(Col_DisclosureHeader).AllowEditing = False
                .Cols(Col_DisclosureHeader).Visible = True
                .Cols(Col_DisclosureHeader).Caption = "Disclosure Header"
                .Cols(Col_DisclosureHeader).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_IsFinished).Width = _TotalWidth * 0.33
                .Cols(Col_IsFinished).AllowEditing = False
                .Cols(Col_IsFinished).Visible = True
                .Cols(Col_IsFinished).Caption = "Is Finished"
                .Cols(Col_IsFinished).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                .Redraw = True


            End With


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally

        End Try

    End Sub




    Private Sub UpdateDisclosureDetails()
        Dim DisclosureID As Long
        Dim TemplateID As Long
        Dim objfrmDisclosure As frmDisclosureMgmt
        Try

            If c1Disclosure.Rows.Count > 1 Then
                blnModify = True
                _blnAdd = False
                DisclosureID = c1Disclosure.Item(c1Disclosure.RowSel, 0).ToString
                TemplateID = c1Disclosure.Item(c1Disclosure.RowSel, 2).ToString

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.DisclosureManagement, DisclosureID, 0, Now)
                    If (IsNothing(mydt) = False) Then
                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            If MessageBox.Show("This Disclosure is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                blnRecordLock = True
                            Else
                                If (IsNothing(mydt) = False) Then
                                    mydt.Dispose()
                                    mydt = Nothing
                                End If
                                Exit Sub
                            End If
                        End If
                        If (IsNothing(mydt) = False) Then
                            mydt.Dispose()
                            mydt = Nothing
                        End If
                    End If

                End If
                '''' <><><> Record Level Locking <><><><> 

                '******Shweta 20090828 *********'
                'To check exeception related to word
                If CheckWordForException() = False Then
                    Exit Sub
                End If
                'End Shweta


                ' Dim grdIndex As Integer = grdDisclosure.CurrentRowIndex
                If c1Disclosure.GetData(c1Disclosure.RowSel, 4).ToString = "Yes" Then
                    ''if Disclosure's Sataus is 'Finished' IsFinished=Yes
                    objfrmDisclosure = New frmDisclosureMgmt(DisclosureID, TemplateID, True, blnRecordLock, _PatientID)
                Else

                    ''GLO2011-0015182 : Nurse Note 
                    ''Code is commented only because it has been handled Through isFinished Query.
                    'If blnRecordLock Then
                    '    ''if Disclosure's Sataus is 'NOT Finished' IsFinished=No and record is lock then open is as finished
                    '    objfrmDisclosure = New frmDisclosureMgmt(DisclosureID, TemplateID, True, blnRecordLock, _PatientID)
                    'Else
                    '    ''if Disclosure's Sataus is 'NOT Finished' IsFinished=No and record is not lock
                    objfrmDisclosure = New frmDisclosureMgmt(DisclosureID, TemplateID, False, blnRecordLock, _PatientID)
                    'End If


                End If

                '''''''''''Code is Added by Anil on 20071103
                sortOrder = CType(c1Disclosure.DataSource, DataView).Sort
                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If
                ''''''''''''''''''''''
                'AddHandler objfrmDisclosure.EvntGenerateCDAFromDisclosureManagement, AddressOf Raise_EvntGenerateCDAFromVWDisclosureManagement
                With objfrmDisclosure
                    .Text = "Modify Disclosure"
                    '.MdiParent = Me.ParentForm
                    .MdiParent = Me.ParentForm
                    .IsModify = True
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .MyCaller = Me
                    .Show()
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                End With

                '19-Jan-16 Aniket: Resolving Bug #92438: gloEMR: Disclosure management: Application displays table columns name
                'If objfrmDisclosure.CancelClick = False Then

                '    c1Disclosure.Enabled = False
                '    c1Disclosure.DataSource = objclsDisclosure.GetAllDisclosures(_PatientID)
                '    c1Disclosure.Enabled = True

                '    '''' To Remember the Selection of Row 
                '    Dim i As Integer
                '    For i = 1 To c1Disclosure.Rows.Count - 1
                '        '''' when ID Found select that matching Row
                '        If DisclosureID = c1Disclosure.Item(i, 0) Then
                '            c1Disclosure.RowSel = i
                '            c1Disclosure.Select(i, 0)
                '            Exit For
                '        End If
                '    Next

                'End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

        Finally
            objfrmDisclosure = Nothing
        End Try
    End Sub

    Public Sub RefreshDisclosure(ByVal DisclosureID As Long)

        Dim i As Integer

        Try

            Try



                c1Disclosure.Enabled = False
                c1Disclosure.DataSource = objclsDisclosure.GetAllDisclosures(_PatientID)
                c1Disclosure.Enabled = True

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            End Try

            If _blnAdd = False Then
                Dim myDataView As DataView = CType(c1Disclosure.DataSource, DataView)

                If (IsNothing(myDataView) = False) Then

                    sortOrder = myDataView.Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")

                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If

                    SetGridStyle(strcolumnName, strsortorder, strSearchstring)
                End If

            Else
                SetGridStyle()
            End If

            If DisclosureID <> 0 Then
                c1Disclosure.RowSel = -1
            End If


            If (Not IsNothing(c1Disclosure.DataSource) AndAlso (c1Disclosure.Rows.Count > 1)) Then
                For i = 1 To c1Disclosure.Rows.Count - 1 '' to check from i=0
                    '''' when ID Found select that matching Row
                    If DisclosureID = c1Disclosure.Item(i, 0) Then
                        c1Disclosure.RowSel = i
                        ' c1Disclosure.UnSelect(0)
                        c1Disclosure.Select(i, 0)
                        Exit For
                    Else
                        c1Disclosure.RowSel = 1
                    End If
                Next
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub


  
    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If c1Disclosure.RowSel > 1 Then
                    c1Disclosure.Select(1, 0)
                    c1Disclosure.RowSel = 1
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(c1Disclosure.DataSource(), DataView)
                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                c1Disclosure.Enabled = False
                c1Disclosure.DataSource = dvPatient
                c1Disclosure.Enabled = True
                Dim strPatientSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If
                Dim strSearch As String = "Disclosure Header"
                ' Select Case strSearch
                'Case "Date"
                '    If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '    Else
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                ''    End If
                ' Case "Disclosure Header"
                If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                    dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                Else
                    'dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                    'Shubhangi 20091007
                    dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                            & dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%' "
                End If
                ' End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub


    Private Sub AddDisclosure()
   
        If MainMenu.IsAccess(False, _PatientID) = False Then
            Exit Sub
        End If

        If gblnProviderDisable = True Then
            If ShowAssociateProvider(_PatientID, Me) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If
       
        If CheckWordForException() = False Then
            Exit Sub
        End If


        _blnAdd = True
        '
        'Line commented and modified by dipak 20100907 for case UC5070.003 :replace gnPatientID by loacal variable.
        'Dim objfrmDisclosure As New frmDisclosureMgmt(gnPatientID)
        Dim objfrmDisclosure As New frmDisclosureMgmt(_PatientID)
        'AddHandler objfrmDisclosure.EvntGenerateCDAFromDisclosureManagement, AddressOf Raise_EvntGenerateCDAFromVWDisclosureManagement
        'end modification by dipak
        Dim objWord As gloEMRWord.clsWordDocument
        Dim dtWord As DataTable
        Try
            blnModify = False
            dtWord = New DataTable
            objWord = New gloEMRWord.clsWordDocument
            dtWord = objWord.FillTemplates(gloEMRWord.enumTemplateFlag.DisclosureMangement)
            If dtWord.Rows.Count = 0 Then
                ''''If not present then exit from sub
                MessageBox.Show("No Template is associated for Disclosure Mangement. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else

                With objfrmDisclosure
                    .Text = "New Disclosure Management"
                    ' .MdiParent = Me.ParentForm
                    .MyCaller = Me
                    .MdiParent = Me.ParentForm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                    .Show()

                End With

                
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

        Finally
            objfrmDisclosure = Nothing
        End Try
    End Sub
    Private Sub UpdateDisclosure()
        Try

            '''''<><><><><> Check Patient Status <><><><><><>''''
            ''''' 20070125 -Mahesh 
            'If CheckPatientStatus(_PatientID) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''

            If c1Disclosure.Rows.Count > 1 Then
                If c1Disclosure.RowSel >= 1 Then  ''to check
                    Call UpdateDisclosureDetails()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
   
    Private Sub DeleteDisclosure()
        Dim ID As Long
        Dim DisclosureDate As String
        Dim DisclosureHeader As String

        Try
            ''''' 20070427 - Bipin 
            'If CheckPatientStatus(_PatientID) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If

            If c1Disclosure.Rows.Count > 1 Then

                If c1Disclosure.RowSel < 1 Then
                    Exit Sub
                End If

                If c1Disclosure.Item(c1Disclosure.RowSel, 4) = "Yes" Then
                    MessageBox.Show("The status of Disclosure is finished, you cannot delete this Disclosure.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                'blnModify = True
                ID = c1Disclosure.Item(c1Disclosure.RowSel, 0).ToString
                DisclosureDate = c1Disclosure.Item(c1Disclosure.RowSel, 1).ToString

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                'Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.DisclosureManagement, ID, 0, DisclosureDate)
                    If (IsNothing(mydt) = False) Then
                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            MessageBox.Show("This Disclosure Management is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            If (IsNothing(mydt) = False) Then
                                mydt.Dispose()
                                mydt = Nothing
                            End If
                            Exit Sub
                        End If
                        If (IsNothing(mydt) = False) Then
                            mydt.Dispose()
                            mydt = Nothing
                        End If
                    End If

                End If
                '''' <><><> Record Level Locking <><><><> 

                If MessageBox.Show("Do you want to delete the selected Disclosure?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then

                    DisclosureHeader = c1Disclosure.Item(c1Disclosure.RowSel, 3).ToString
                    objclsDisclosure.DeleteDisclosureMgmt(ID, _PatientID) ', DisclosureDate, DisclosureHeader)
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Management Deleted", gloAuditTrail.ActivityOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Management Deleted", _PatientID, ID, 0, gloAuditTrail.ActivityOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Disclosure Set Details Deleted", _PatientID, ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    c1Disclosure.Enabled = False
                    c1Disclosure.DataSource = objclsDisclosure.GetAllDisclosures(_PatientID)
                    c1Disclosure.Enabled = True
                    'If Not IsNothing(objclsPatientDisclosures.GetDataview) Then
                    '    objclsPatientDisclosures.SortDataview(objclsPatientDisclosures.GetDataview.Table.Columns(1).ColumnName)
                    'End If
                    Dim myDataView As DataView = CType(c1Disclosure.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        '''''''''''Code is Added by Anil on 20071103
                        sortOrder = CType(c1Disclosure.DataSource, DataView).Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If

                        SetGridStyle(strcolumnName, strsortorder, strSearchstring)
                    End If


                    ''''''''''''''''''
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    
    Private Sub RefreshDisclosure()
        Try
            c1Disclosure.Enabled = False
            c1Disclosure.DataSource = objclsDisclosure.GetAllDisclosures(_PatientID)
            c1Disclosure.Enabled = True
            SetGridStyle()
            If c1Disclosure.Rows.Count > 1 Then
                c1Disclosure.RowSel = 1
                c1Disclosure.Select(1, 0)
            End If
            txtSearch.Text = ""
            _blnSearch = True

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "Disclosure Managements", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked

        Select Case e.ClickedItem.Tag
            Case "Add"
                Call AddDisclosure()
            Case "Modify"
                Call UpdateDisclosure()
            Case "Delete"
                Call DeleteDisclosure()
            Case "Refresh"
                Call RefreshDisclosure()
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

    Private Sub frmVWDisclosureMgmt_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ''  CustomGridStyle()
        SetGridStyle()
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

#Region "Call Generate CCDA from Dashboard"
    'Public Delegate Sub GenerateCDAFromVWDisclosureManagement(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromVWDisclosureManagement(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromVWDisclosureManagement(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromVWDisclosureManagement(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub
#End Region
    Dim ind As Integer = -1

    Private Sub c1Disclosure_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1Disclosure.AfterSort   ''added to solve  sorting issue Bugid 72083
        Try
            If ind > -1 Then
                Dim rw As C1.Win.C1FlexGrid.Row
                For Each rw In c1Disclosure.Rows
                    Dim cm As CurrencyManager = CType(BindingContext(Me.c1Disclosure.DataSource), CurrencyManager)
                    Dim dr As DataRowView = CType(rw.DataSource, DataRowView)
                    If Not dr Is Nothing Then
                        Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)
                        If currIndex = ind Then
                            Dim cr As C1.Win.C1FlexGrid.CellRange = c1Disclosure.GetCellRange(rw.Index, 1)
                            ' to scroll the selected row in the visible area
                            c1Disclosure.Select(cr, True)
                            cr = c1Disclosure.GetCellRange(rw.Index, 0, rw.Index, c1Disclosure.Cols.Count - 1)
                            c1Disclosure.Select(cr, False)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(" InterfaceMessageQueue AfterSort " + ex.Message.ToString(), False)
        End Try
        ind = -1
    End Sub
    Private Sub c1Disclosure_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1Disclosure.MouseClick   ''added to solve  sorting issue Bugid 72083

        Try
            If (Not IsNothing(c1Disclosure.DataSource) AndAlso (c1Disclosure.Rows.Count > 0)) Then
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1Disclosure.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(cm.Current, DataRowView)
                ind = dr.Row.Table.Rows.IndexOf(dr.Row)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(" InterfaceMessageQueue MouseClick " + ex.Message.ToString(), False)
        End Try

    End Sub

    Private Sub c1Disclosure_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1Disclosure.MouseDoubleClick
        Try




            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1Disclosure.HitTest(ptPoint)
            ''''''''''''Code is Added by Anil on 20071103
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
                '''''''''''''''''''''''''''''''''''''
            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                'If CheckPatientStatus(_PatientID) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                End If
                UpdateDisclosureDetails()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.DisclosureManagement, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
End Class
