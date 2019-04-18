Imports gloEMR.gloEMRWord
'Imports gloEMR.gloAuditTrail
Public Class frmVWPatientConsent
    Inherits System.Windows.Forms.Form
    Implements IPatientContext


#Region " Windows Form Designer generated code "
    'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub
    'end modify

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents c1PatientConsent As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWPatientConsent))
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
        Me.c1PatientConsent = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.c1PatientConsent, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlRight.Size = New System.Drawing.Size(637, 24)
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
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(635, 1)
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
        Me.Label3.Location = New System.Drawing.Point(636, 1)
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
        Me.Label4.Size = New System.Drawing.Size(637, 1)
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
        Me.pnlToolStrip.Size = New System.Drawing.Size(643, 53)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(643, 53)
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.c1PatientConsent)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(643, 387)
        Me.Panel1.TabIndex = 0
        '
        'c1PatientConsent
        '
        Me.c1PatientConsent.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1PatientConsent.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1PatientConsent.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1PatientConsent.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1PatientConsent.ColumnInfo = resources.GetString("c1PatientConsent.ColumnInfo")
        Me.c1PatientConsent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1PatientConsent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1PatientConsent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1PatientConsent.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1PatientConsent.Location = New System.Drawing.Point(4, 1)
        Me.c1PatientConsent.Name = "c1PatientConsent"
        Me.c1PatientConsent.Rows.Count = 1
        Me.c1PatientConsent.Rows.DefaultSize = 19
        Me.c1PatientConsent.Rows.Fixed = 0
        Me.c1PatientConsent.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1PatientConsent.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1PatientConsent.ShowCellLabels = True
        Me.c1PatientConsent.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1PatientConsent.Size = New System.Drawing.Size(635, 382)
        Me.c1PatientConsent.StyleInfo = resources.GetString("c1PatientConsent.StyleInfo")
        Me.c1PatientConsent.TabIndex = 13
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 383)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(635, 1)
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
        Me.lbl_RightBrd.Location = New System.Drawing.Point(639, 1)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(637, 1)
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
        Me.Panel2.Size = New System.Drawing.Size(643, 30)
        Me.Panel2.TabIndex = 0
        '
        'frmVWPatientConsent
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(643, 470)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWPatientConsent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Consent"
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
        CType(Me.c1PatientConsent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim _PatientID As Long
    Public Shared blnModify As Boolean
    Dim objclsPatientConsent As New clsPatientConsent
    'Dim dt As DataTable
    Dim dv As DataView
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim _blnAdd As Boolean
    Dim dtWord As DataTable
    Dim objWord As clsWordDocument
    Dim Col_Count = 5
    Dim Col_ConsentID As Integer = 0
    Dim Col_ConsentDate As Integer = 1
    Dim Col_TemplateID As Integer = 2
    Dim Col_ConsentHeader As Integer = 3
    Dim Col_IsFinished As Integer = 4

    Private Sub btnCloseConsent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmVWPatientConsent_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Try
            Me.WindowState = FormWindowState.Maximized
            'If CType(Me.MdiParent, MainMenu).pnlLeft.Visible = False Then
            '    CType(Me.MdiParent, MainMenu).Splitter1.Visible = False
            'End If


            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            '' ABOVE 2 LINES COMMENTED BY SUDHIR 20090202
          

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVWPatientConsent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSearch.Focus()
        'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        '_PatientID = gnPatientID
        'end comment 

        'grdPatientConsent.Enabled = False
        'grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
        'grdPatientConsent.Enabled = True
        ''nLetterID, dtLetterDate, nTemplateID, sTemplateName

        ' CustomGridStyle()

        c1PatientConsent.Enabled = False
        c1PatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
        c1PatientConsent.Enabled = True
        SetGridStyle()
        'Sanjog - Added on 2011 May 17 for Patient Safety
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        'Sanjog - Added on 2011 May 17 for Patient Safety
    End Sub

    'Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
    '    'Dim dt As DataTable
    '    dv = objclsPatientConsent.GetDataView

    '    Dim ts As New clsDataGridTableStyle(dv.Table.TableName)

    '    Dim ConsentIDCol As New DataGridTextBoxColumn
    '    With ConsentIDCol
    '        .Width = 0
    '        .MappingName = dv.Table.Columns(0).ColumnName
    '        .HeaderText = "ConsentID"
    '    End With

    '    Dim DateCol As New DataGridTextBoxColumn
    '    With DateCol
    '        .Width = 0.3 * grdPatientConsent.Width
    '        .MappingName = dv.Table.Columns(1).ColumnName
    '        .HeaderText = "Consent Date"
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
    '        .Width = 0.5 * grdPatientConsent.Width
    '        .MappingName = dv.Table.Columns(3).ColumnName
    '        .HeaderText = "Consent Header"
    '        .NullText = ""
    '    End With

    '    Dim IsFinishedCol As New DataGridTextBoxColumn
    '    With IsFinishedCol
    '        .Width = 0.2 * grdPatientConsent.Width
    '        .MappingName = dv.Table.Columns(4).ColumnName
    '        .HeaderText = "Is Finished"
    '        .NullText = ""
    '    End With

    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {ConsentIDCol, DateCol, TempIDCol, TempNameCol, IsFinishedCol})
    '    grdPatientConsent.TableStyles.Clear()
    '    grdPatientConsent.TableStyles.Add(ts)

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
    '        grdPatientConsent.Select(0)
    '    End If
    'End Sub



    Private Sub SetGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        ''added to solve  sorting issue Bugid 72083
        Try


            dv = objclsPatientConsent.GetDataView

            c1PatientConsent.DataSource = dv
            With c1PatientConsent
                .AllowSorting = True


                .Redraw = False
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = Screen.PrimaryScreen.WorkingArea.Width - 60
                c1PatientConsent.Width = _TotalWidth
                ' c1Disclosure.Height = Me.Height - 20
                c1PatientConsent.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                .Cols.Count = Col_Count
                .Rows.Fixed = 1
                .Styles.ClearUnused()
                .AllowResizing = True

                .Cols(Col_ConsentID).Width = _TotalWidth * 0
                .Cols(Col_ConsentID).AllowEditing = False
                .Cols(Col_ConsentID).Visible = False
                .Cols(Col_ConsentID).Caption = "ConsentID"
                .Cols(Col_ConsentID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_ConsentDate).Width = _TotalWidth * 0.33
                .Cols(Col_ConsentDate).AllowEditing = False
                .Cols(Col_ConsentDate).Visible = True
                .Cols(Col_ConsentDate).Caption = "Consent Date"
                .Cols(Col_ConsentDate).DataType = GetType(System.DateTime)
                .Cols(Col_ConsentDate).Format = "MM/dd/yyyy h:mm tt"

                .Cols(Col_ConsentDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_TemplateID).Width = _TotalWidth * 0
                .Cols(Col_TemplateID).AllowEditing = False
                .Cols(Col_TemplateID).Visible = False
                .Cols(Col_TemplateID).Caption = "TemplateID"
                .Cols(Col_TemplateID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                .Cols(Col_ConsentHeader).Width = _TotalWidth * 0.4
                .Cols(Col_ConsentHeader).AllowEditing = False
                .Cols(Col_ConsentHeader).Visible = True
                .Cols(Col_ConsentHeader).Caption = "Consent Header"
                .Cols(Col_ConsentHeader).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                .Cols(Col_IsFinished).Width = _TotalWidth * 0.27
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


    'Private Sub UpdateLetters_Old()
    '    Dim LetterID As Long
    '    Dim TemplateID As Long
    '    Dim objfrmPatientConsent As frmPatientConsent
    '    Try

    '        If grdPatientConsent.VisibleRowCount >= 1 Then
    '            blnModify = True
    '            _blnAdd = False
    '            LetterID = grdPatientConsent.Item(grdPatientConsent.CurrentRowIndex, 0).ToString
    '            TemplateID = grdPatientConsent.Item(grdPatientConsent.CurrentRowIndex, 2).ToString

    '            ' '' <><><> Record Level Locking <><><><> 
    '            ' '' Mahesh - 20070718 
    '            Dim blnRecordLock As Boolean = False
    '            If gblnRecordLocking = True Then
    '                Dim mydt As New mytable
    '                mydt = Scan_n_Lock_Transaction(TrnType.PatientConsent, LetterID, 0, Now)
    '                If mydt.Description <> gstrClientMachineName Then
    '                    If MessageBox.Show("This Patient Consent is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '                        blnRecordLock = True
    '                    Else
    '                        Exit Sub
    '                    End If
    '                End If

    '            End If
    '            '''' <><><> Record Level Locking <><><><> 

    '            '******Shweta 20090828 *********'
    '            'To check exeception related to word
    '            If CheckWordForException() = False Then
    '                Exit Sub
    '            End If
    '            'End Shweta


    '            Dim grdIndex As Integer = grdPatientConsent.CurrentRowIndex
    '            If grdPatientConsent.Item(grdPatientConsent.CurrentRowIndex, 4).ToString = "Yes" Then
    '                ''if Letter's Sataus is 'Finished' IsFinished=Yes
    '                'Shweta //
    '                'objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, True, blnRecordLock)/ //

    '                objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, True, blnRecordLock, _PatientID)
    '            Else

    '                ''GLO2011-0015182 : Nurse Note 
    '                ''Code is commented only because it has been handled Through isFinished Query.
    '                'If blnRecordLock Then
    '                '    ''if Letter's Sataus is 'NOT Finished' IsFinished=No and record is lock then open is as finished
    '                '    'Shweta //
    '                '    'objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, True, blnRecordLock) //
    '                '    objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, True, blnRecordLock, _PatientID)

    '                'Else
    '                ''if Letter's Sataus is 'NOT Finished' IsFinished=No and record is not lock
    '                'Shweta //
    '                'objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, False, blnRecordLock)
    '                objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, False, blnRecordLock, _PatientID)
    '                'End If

    '            End If

    '            '''''''''''Code is Added by Anil on 20071103
    '            sortOrder = CType(grdPatientConsent.DataSource, DataView).Sort
    '            strSearchstring = txtSearch.Text.Trim
    '            arrcolumnsort = Split(sortOrder, "]")
    '            If arrcolumnsort.Length > 1 Then
    '                strcolumnName = arrcolumnsort.GetValue(0)
    '                strsortorder = arrcolumnsort.GetValue(1)
    '            End If
    '            ''''''''''''''''''''''

    '            With objfrmPatientConsent
    '                .Text = "Modify Patient Consent"
    '                '.MdiParent = Me.ParentForm
    '                .MdiParent = Me.ParentForm
    '                .IsModify = True
    '                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '                .MyCaller = Me
    '                .Show()
    '                .WindowState = FormWindowState.Maximized
    '                .BringToFront()
    '            End With

    '            If objfrmPatientConsent.CancelClick = False Then
    '                'grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
    '                grdPatientConsent.Enabled = False
    '                grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
    '                grdPatientConsent.Enabled = True
    '                'If Not IsNothing(objclsPatientLetters.DsDataview) Then
    '                '    objclsPatientLetters.SortDataview(objclsPatientLetters.GetDataview.Table.Columns(1).ColumnName)
    '                'End If
    '                '''' To Remember the Selection of Row 
    '                Dim i As Integer
    '                For i = 0 To dv.Table.Rows.Count - 1
    '                    '''' when ID Found select that matching Row
    '                    If LetterID = grdPatientConsent.Item(i, 0) Then
    '                        grdPatientConsent.CurrentRowIndex = i
    '                        grdPatientConsent.Select(i)
    '                        Exit For
    '                    End If
    '                Next
    '                'Else
    '                '    grdPatientConsent.Select(grdIndex)
    '            End If
    '        End If
    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.View, "Patient Consent viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

    '    Finally
    '        objfrmPatientConsent = Nothing
    '    End Try
    'End Sub


    Private Sub UpdateLetters()
        Dim LetterID As Long
        Dim TemplateID As Long
        Dim objfrmPatientConsent As frmPatientConsent
        Try

            If c1PatientConsent.Rows.Count > 1 Then
                blnModify = True
                _blnAdd = False
                LetterID = c1PatientConsent.Item(c1PatientConsent.RowSel, 0).ToString
                TemplateID = c1PatientConsent.Item(c1PatientConsent.RowSel, 2).ToString

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.PatientConsent, LetterID, 0, Now)
                    If (IsNothing(mydt) = False) Then
                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            If MessageBox.Show("This Patient Consent is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
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



            If c1PatientConsent.Item(c1PatientConsent.RowSel, 4).ToString = "Yes" Then
                ''if Letter's Sataus is 'Finished' IsFinished=Yes
                'Shweta //
                'objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, True, blnRecordLock)/ //

                objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, True, blnRecordLock, _PatientID)
            Else

                ''GLO2011-0015182 : Nurse Note 
                ''Code is commented only because it has been handled Through isFinished Query.
                'If blnRecordLock Then
                '    ''if Letter's Sataus is 'NOT Finished' IsFinished=No and record is lock then open is as finished
                '    'Shweta //
                '    'objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, True, blnRecordLock) //
                '    objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, True, blnRecordLock, _PatientID)

                'Else
                ''if Letter's Sataus is 'NOT Finished' IsFinished=No and record is not lock
                'Shweta //
                'objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, False, blnRecordLock)
                objfrmPatientConsent = New frmPatientConsent(LetterID, TemplateID, False, blnRecordLock, _PatientID)
                'End If

            End If

            '''''''''''Code is Added by Anil on 20071103
            Dim myDataView As DataView = CType(c1PatientConsent.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                sortOrder = CType(c1PatientConsent.DataSource, DataView).Sort
                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If

                ''  End If''''''''''''''''''''
            End If
            With objfrmPatientConsent
                .Text = "Modify Patient Consent"
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

            If objfrmPatientConsent.CancelClick = False Then
                'grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
                c1PatientConsent.Enabled = False
                c1PatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)

                If (IsNothing(myDataView) = False) Then
                    sortOrder = CType(c1PatientConsent.DataSource, DataView).Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                    SetGridStyle(strcolumnName, strsortorder, strSearchstring)
                End If

                c1PatientConsent.Enabled = True
                'If Not IsNothing(objclsPatientLetters.DsDataview) Then
                '    objclsPatientLetters.SortDataview(objclsPatientLetters.GetDataview.Table.Columns(1).ColumnName)
                'End If
                '''' To Remember the Selection of Row 
                Dim i As Integer
                For i = 1 To c1PatientConsent.Rows.Count - 1
                    '''' when ID Found select that matching Row
                    If LetterID = c1PatientConsent.Item(i, 0) Then
                        c1PatientConsent.RowSel = i
                        c1PatientConsent.Select(i, 0)
                        Exit For
                    End If
                Next
                'Else
                '    grdPatientConsent.Select(grdIndex)
            End If
            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.View, "Patient Consent viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

        Finally
            objfrmPatientConsent = Nothing
        End Try
    End Sub



    'Public Sub RefreshConsent_Old(ByVal ConsentID As Long)

    '    'grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
    '    grdPatientConsent.Enabled = False
    '    grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
    '    grdPatientConsent.Enabled = True
    '    '''''''''''Code is Added by Anil on 20071103
    '    '''''Flag is checked to find whether we are adding or modifying the record, so according to that the grid will be filled.
    '    If _blnAdd = False Then
    '        sortOrder = dv.Sort
    '        strSearchstring = txtSearch.Text.Trim
    '        arrcolumnsort = Split(sortOrder, "]")
    '        If arrcolumnsort.Length > 1 Then
    '            strcolumnName = arrcolumnsort.GetValue(0)
    '            strsortorder = arrcolumnsort.GetValue(1)
    '        End If
    '        '''''''''''''''''''''''
    '        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
    '        '''' To Remember the Selection of Row 
    '        'Dim i As Integer
    '        'For i = 0 To CType(grdPatientConsent.DataSource, DataView).Count - 1
    '        '    '''' when ID Found select that matching Row
    '        '    If ConsentID = grdPatientConsent.Item(i, 0) Then
    '        '        grdPatientConsent.CurrentRowIndex = i
    '        '        grdPatientConsent.Select(i)
    '        '        Exit For
    '        '    End If
    '        'Next
    '    Else
    '        CustomGridStyle()
    '    End If

    '    If ConsentID <> 0 Then
    '        grdPatientConsent.UnSelect(0)
    '    End If
    '    Dim i As Integer
    '    For i = 0 To CType(grdPatientConsent.DataSource, DataView).Count - 1
    '        '''' when ID Found select that matching Row
    '        If ConsentID = grdPatientConsent.Item(i, 0) Then
    '            grdPatientConsent.CurrentRowIndex = i
    '            grdPatientConsent.Select(i)
    '            Exit For
    '        End If
    '    Next
    'End Sub


    Public Sub RefreshConsent(ByVal ConsentID As Long)

        'grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
        c1PatientConsent.Enabled = False
        c1PatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
        c1PatientConsent.Enabled = True
        '''''''''''Code is Added by Anil on 20071103
        '''''Flag is checked to find whether we are adding or modifying the record, so according to that the grid will be filled.
        If _blnAdd = False Then
            sortOrder = dv.Sort
            strSearchstring = txtSearch.Text.Trim
            arrcolumnsort = Split(sortOrder, "]")
            If arrcolumnsort.Length > 1 Then
                strcolumnName = arrcolumnsort.GetValue(0)
                strsortorder = arrcolumnsort.GetValue(1)
            End If
            '''''''''''''''''''''''
            SetGridStyle(strcolumnName, strsortorder, strSearchstring)
            '''' To Remember the Selection of Row 
            'Dim i As Integer
            'For i = 0 To CType(grdPatientConsent.DataSource, DataView).Count - 1
            '    '''' when ID Found select that matching Row
            '    If ConsentID = grdPatientConsent.Item(i, 0) Then
            '        grdPatientConsent.CurrentRowIndex = i
            '        grdPatientConsent.Select(i)
            '        Exit For
            '    End If
            'Next
        Else
            SetGridStyle()
        End If

        If ConsentID <> 0 Then
            c1PatientConsent.RowSel = -1
        End If
        Dim i As Integer
        For i = 1 To c1PatientConsent.Rows.Count - 1
            '''' when ID Found select that matching Row
            If ConsentID = c1PatientConsent.Item(i, 0) Then
                c1PatientConsent.RowSel = i
                c1PatientConsent.Select(i, 0)
                Exit For
            End If
        Next
    End Sub

    Private Sub grdModifiers_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''Try
        ''    Select Case grdPatientConsent.CurrentCell.ColumnNumber
        ''        Case 3
        ''            'lblSearch.Text = "Letter Header"
        ''    End Select
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, "Patient  Consent", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub


    'Private Sub grdPatientConsent_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Try
    '        Dim ptPoint As Point = New Point(e.X, e.Y)
    '        Dim htInfo As DataGrid.HitTestInfo = grdPatientConsent.HitTest(ptPoint)
    '        ''''''''''''Code is Added by Anil on 20071103
    '        If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then


    '            If txtSearch.Text = "" Then
    '                _blnSearch = True
    '            Else
    '                _blnSearch = False
    '                txtSearch.Text = ""
    '                _blnSearch = True
    '            End If
    '            '''''''''''''''''''''''''''''''''''''
    '        ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
    '            'If CheckPatientStatus(_PatientID) = False Then
    '            '    Exit Sub
    '            'End If
    '            If MainMenu.IsAccess(False, _PatientID) = False Then
    '                Exit Sub
    '            End If
    '            UpdateLetters()
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    'Private Sub grdPatientConsent_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    'If grdPatientConsent.CurrentRowIndex >= 0 Then
    '    '    grdPatientConsent.Select(grdPatientConsent.CurrentRowIndex)
    '    'End If
    '    Try
    '        Dim ptPoint As Point = New Point(e.X, e.Y)
    '        Dim htInfo As DataGrid.HitTestInfo = grdPatientConsent.HitTest(ptPoint)
    '        If htInfo.Type = DataGrid.HitTestType.Cell Then
    '            grdPatientConsent.Select(htInfo.Row)
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If c1PatientConsent.Rows.Count > 1 Then
                    c1PatientConsent.Select(1, 0)
                    c1PatientConsent.RowSel = 1
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
       
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(c1PatientConsent.DataSource(), DataView)
                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                c1PatientConsent.Enabled = False
                c1PatientConsent.DataSource = dvPatient
                c1PatientConsent.Enabled = True
                Dim strPatientSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If
                ' Dim strSearch As String = "Consent Header"
                '' Select Case strSearch
                'Case "Date"
                '    If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '    Else
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '    End If
                ' Case "Consent Header"
                If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                    dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                Else
                    'Commented by shubhangi 20091006
                    'Use in string search 
                    'dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                    'We wnat to use instring search
                    'Shubhangi 20091007

                    dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                       & dv.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%' "

                End If
                'End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007
    'Private Sub AddConsent_Old()
    '    ''''' 20070427 - Bipin
    '    'If CheckPatientStatus(_PatientID) = False Then
    '    '    Exit Sub
    '    'End If
    '    If MainMenu.IsAccess(False, _PatientID) = False Then
    '        Exit Sub
    '    End If
    '    '' SUDHIR 20090521 '' CHECK PROVIDER ''
    '    If gblnProviderDisable = True Then
    '        If ShowAssociateProvider(_PatientID, Me) = True Then
    '            CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
    '        End If
    '    End If
    '    '' END SUDHIR

    '    '******Shweta 20090828 *********'
    '    'To check exeception related to word
    '    If CheckWordForException() = False Then
    '        Exit Sub
    '    End If
    '    'End Shweta


    '    dtWord = New DataTable
    '    objWord = New clsWordDocument
    '    dtWord = objWord.FillTemplates(enumTemplateFlag.PatientConsent)
    '    If dtWord.Rows.Count = 0 Then
    '        ''''If not present then exit from sub
    '        MessageBox.Show("No Template is associated for Consent form. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        objWord = Nothing
    '        dtWord = Nothing
    '        Exit Sub
    '    Else
    '        _blnAdd = True
    '        Dim objfrmPatientConsent As New frmPatientConsent(_PatientID)
    '        Try
    '            blnModify = False

    '            With objfrmPatientConsent
    '                .Text = "New Patient Consent"
    '                ' .MdiParent = Me.ParentForm
    '                .MyCaller = Me
    '                .MdiParent = Me.ParentForm
    '                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '                .Show()
    '                .WindowState = FormWindowState.Maximized
    '                .BringToFront()
    '            End With

    '            If objfrmPatientConsent.CancelClick = False Then
    '                grdPatientConsent.Enabled = False
    '                grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
    '                grdPatientConsent.Enabled = True
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

    '        Finally
    '            objfrmPatientConsent = Nothing
    '        End Try
    '    End If
    'End Sub

    Private Sub AddConsent()
        ''''' 20070427 - Bipin
        'If CheckPatientStatus(_PatientID) = False Then
        '    Exit Sub
        'End If
        If MainMenu.IsAccess(False, _PatientID) = False Then
            Exit Sub
        End If
        '' SUDHIR 20090521 '' CHECK PROVIDER ''
        If gblnProviderDisable = True Then
            If ShowAssociateProvider(_PatientID, Me) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If
        '' END SUDHIR

        '******Shweta 20090828 *********'
        'To check exeception related to word
        If CheckWordForException() = False Then
            Exit Sub
        End If
        'End Shweta


        dtWord = New DataTable
        objWord = New clsWordDocument
        dtWord = objWord.FillTemplates(enumTemplateFlag.PatientConsent)
        If dtWord.Rows.Count = 0 Then
            ''''If not present then exit from sub
            MessageBox.Show("No Template is associated for Consent form. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            objWord = Nothing
            dtWord = Nothing
            Exit Sub
        Else
            _blnAdd = True
            Dim objfrmPatientConsent As New frmPatientConsent(_PatientID)
            Try
                blnModify = False

                With objfrmPatientConsent
                    .Text = "New Patient Consent"
                    ' .MdiParent = Me.ParentForm
                    .MyCaller = Me
                    .MdiParent = Me.ParentForm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .Show()
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                End With

                If objfrmPatientConsent.CancelClick = False Then
                    c1PatientConsent.Enabled = False
                    c1PatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
                    c1PatientConsent.Enabled = True
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            Finally
                objfrmPatientConsent = Nothing
            End Try
        End If
    End Sub


    'Private Sub UpdateConsent_Old()
    '    Try

    '        '''''<><><><><> Check Patient Status <><><><><><>''''
    '        ''''' 20070125 -Mahesh 
    '        'If CheckPatientStatus(_PatientID) = False Then
    '        '    Exit Sub
    '        'End If
    '        If MainMenu.IsAccess(False, _PatientID) = False Then
    '            Exit Sub
    '        End If
    '        '''''<><><><><> Check Patient Status <><><><><><>''''
    '        If grdPatientConsent.VisibleRowCount >= 1 Then
    '            If grdPatientConsent.IsSelected(grdPatientConsent.CurrentRowIndex) Then
    '                Call UpdateLetters()
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub


    Private Sub UpdateConsent()
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
            If c1PatientConsent.Rows.Count > 1 Then
                If c1PatientConsent.RowSel > 0 Then
                    Call UpdateLetters()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    'Private Sub DeleteConsent_Old()
    '    Dim ID As Long
    '    Dim ConsentDate As String
    '    Dim ConsentHeader As String

    '    Try
    '        ''''' 20070427 - Bipin 
    '        'If CheckPatientStatus(_PatientID) = False Then
    '        '    Exit Sub
    '        'End If
    '        If MainMenu.IsAccess(False, _PatientID) = False Then
    '            Exit Sub
    '        End If

    '        If grdPatientConsent.VisibleRowCount >= 1 Then

    '            If grdPatientConsent.IsSelected(grdPatientConsent.CurrentRowIndex) = False Then
    '                Exit Sub
    '            End If

    '            If grdPatientConsent.Item(grdPatientConsent.CurrentRowIndex, 4) = "Yes" Then
    '                MessageBox.Show("The status of Patient Consent is finished, you cannot delete this Patient Consent.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Exit Sub
    '            End If
    '            'blnModify = True
    '            ID = grdPatientConsent.Item(grdPatientConsent.CurrentRowIndex, 0).ToString
    '            ConsentDate = grdPatientConsent.Item(grdPatientConsent.CurrentRowIndex, 1).ToString

    '            ' '' <><><> Record Level Locking <><><><> 
    '            ' '' Mahesh - 20070718 
    '            'Dim blnRecordLock As Boolean = False
    '            If gblnRecordLocking = True Then
    '                Dim mydt As New mytable
    '                mydt = Scan_n_Lock_Transaction(TrnType.PatientConsent, ID, 0, ConsentDate)
    '                If mydt.Description <> gstrClientMachineName Then
    '                    MessageBox.Show("This Patient Consent is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            End If
    '            '''' <><><> Record Level Locking <><><><> 

    '            If MessageBox.Show("Do you want to delete selected Patient Consent?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then

    '                ConsentHeader = grdPatientConsent.Item(grdPatientConsent.CurrentRowIndex, 3).ToString
    '                'parameter _PatientID pass for UC5070.003 by dipak.
    '                objclsPatientConsent.DeletePatientConsent(ID, ConsentDate, ConsentHeader, _PatientID)
    '                'end changes made by dipak
    '                grdPatientConsent.Enabled = False
    '                grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
    '                grdPatientConsent.Enabled = True
    '                'If Not IsNothing(objclsPatientLetters.GetDataview) Then
    '                '    objclsPatientLetters.SortDataview(objclsPatientLetters.GetDataview.Table.Columns(1).ColumnName)
    '                'End If

    '                '''''''''''Code is Added by Anil on 20071103
    '                sortOrder = CType(grdPatientConsent.DataSource, DataView).Sort
    '                strSearchstring = txtSearch.Text.Trim
    '                arrcolumnsort = Split(sortOrder, "]")
    '                If arrcolumnsort.Length > 1 Then
    '                    strcolumnName = arrcolumnsort.GetValue(0)
    '                    strsortorder = arrcolumnsort.GetValue(1)
    '                End If

    '                CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
    '                ''''''''''''''''''
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub DeleteConsent()
        Dim ID As Long
        Dim ConsentDate As String
        Dim ConsentHeader As String

        Try
            ''''' 20070427 - Bipin 
            'If CheckPatientStatus(_PatientID) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If

            If c1PatientConsent.Rows.Count > 1 Then

                If c1PatientConsent.RowSel < 1 Then
                    Exit Sub
                End If

                If c1PatientConsent.Item(c1PatientConsent.RowSel, 4) = "Yes" Then
                    MessageBox.Show("The status of Patient Consent is finished, you cannot delete this Patient Consent.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                'blnModify = True
                ID = c1PatientConsent.Item(c1PatientConsent.RowSel, 0).ToString
                ConsentDate = c1PatientConsent.Item(c1PatientConsent.RowSel, 1).ToString

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                'Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.PatientConsent, ID, 0, ConsentDate)
                    If (IsNothing(mydt) = False) Then
                    
                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            MessageBox.Show("This Patient Consent is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

                If MessageBox.Show("Do you want to delete selected Patient Consent?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then

                    ConsentHeader = c1PatientConsent.Item(c1PatientConsent.RowSel, 3).ToString
                    'parameter _PatientID pass for UC5070.003 by dipak.
                    objclsPatientConsent.DeletePatientConsent(ID, ConsentDate, ConsentHeader, _PatientID)
                    'end changes made by dipak
                    c1PatientConsent.Enabled = False
                    c1PatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
                    c1PatientConsent.Enabled = True
                    'If Not IsNothing(objclsPatientLetters.GetDataview) Then
                    '    objclsPatientLetters.SortDataview(objclsPatientLetters.GetDataview.Table.Columns(1).ColumnName)
                    'End If

                    '''''''''''Code is Added by Anil on 20071103
                    Dim myDataView As DataView = CType(c1PatientConsent.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = myDataView.Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If

                        SetGridStyle(strcolumnName, strsortorder, strSearchstring)
                        ''''''''''''''''''
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
   
    'Private Sub RefreshConsent_Old()
    '    Try
    '        grdPatientConsent.Enabled = False
    '        grdPatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
    '        grdPatientConsent.Enabled = True
    '        CustomGridStyle()
    '        If grdPatientConsent.VisibleRowCount > 0 Then
    '            grdPatientConsent.CurrentRowIndex = 0
    '            grdPatientConsent.Select(0)
    '        End If
    '        txtSearch.Text = ""
    '        _blnSearch = True
    '        'Call RefreshConsent()
    '    Catch ex As Exception
    '        'MessageBox.Show(ex.ToString, "Patient Letters", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub RefreshConsent()
        Try
            c1PatientConsent.Enabled = False
            c1PatientConsent.DataSource = objclsPatientConsent.GetAllPatientConsents(_PatientID)
            c1PatientConsent.Enabled = True
            SetGridStyle()
            If c1PatientConsent.Rows.Count > 1 Then
                c1PatientConsent.RowSel = 1
                c1PatientConsent.Select(1, 0)
            End If
            txtSearch.Text = ""
            _blnSearch = True
            'Call RefreshConsent()
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "Patient Letters", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                Call AddConsent()
            Case "Modify"
                Call UpdateConsent()
            Case "Delete"
                Call DeleteConsent()
            Case "Refresh"
                Call RefreshConsent()
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'shubhangi 20091006
        'Use to clear search text box 
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmVWPatientConsent_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ' CustomGridStyle()
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
    Dim ind As Integer = -1
    Private Sub c1PatientConsent_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1PatientConsent.AfterSort ''added to solve sorting issue for bugid 72081
        Try
            If ind > -1 Then
                Dim rw As C1.Win.C1FlexGrid.Row
                For Each rw In c1PatientConsent.Rows
                    Dim cm As CurrencyManager = CType(BindingContext(Me.c1PatientConsent.DataSource), CurrencyManager)
                    Dim dr As DataRowView = CType(rw.DataSource, DataRowView)
                    If Not dr Is Nothing Then
                        Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)
                        If currIndex = ind Then
                            Dim cr As C1.Win.C1FlexGrid.CellRange = c1PatientConsent.GetCellRange(rw.Index, 1)
                            ' to scroll the selected row in the visible area
                            c1PatientConsent.Select(cr, True)
                            cr = c1PatientConsent.GetCellRange(rw.Index, 0, rw.Index, c1PatientConsent.Cols.Count - 1)
                            c1PatientConsent.Select(cr, False)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(" View PatientConsent AfterSort " + ex.Message.ToString(), False)
        End Try
        ind = -1
    End Sub

    Private Sub c1PatientConsent_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PatientConsent.MouseClick ''added to solve sorting issue for bugid 72081
        Try
            If (Not IsNothing(c1PatientConsent.DataSource) AndAlso (c1PatientConsent.Rows.Count > 0)) Then
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1PatientConsent.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(cm.Current, DataRowView)
                ind = dr.Row.Table.Rows.IndexOf(dr.Row)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(" View PatientConsent MouseClick " + ex.Message.ToString(), False)
        End Try

    End Sub

    Private Sub c1PatientConsent_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PatientConsent.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1PatientConsent.HitTest(ptPoint)

            If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.ColumnHeader Then


                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
                '''''''''''''''''''''''''''''''''''''
            ElseIf htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
                'If CheckPatientStatus(_PatientID) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                End If
                UpdateLetters()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class
