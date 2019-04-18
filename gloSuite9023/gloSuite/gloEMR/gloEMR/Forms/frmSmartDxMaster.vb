Imports System.Security.Permissions
Public Class frmSmartDxMaster
    Inherits System.Windows.Forms.Form
    Private COL_To As Integer = 0
    Private COL_Select As Integer = 1
    Private COL_ID As Integer = 2
    Private COL_NAME As Integer = 3
    Private COL_ProviderName As Integer = 4
    Private COL_CreatedDateTime As Integer = 5
    Private COL_ACTIVATED_STATUS As Integer = 6
    Private COL_ProviderID As Integer = 7
    Private COL_Count = 7


    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Merge As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents tsbtn_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Copy As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Act_Deact_Rule As System.Windows.Forms.ToolStripButton
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents lblProvider As System.Windows.Forms.Label
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Private bIsForMerge As Boolean = False
    Friend WithEvents TxtMergeSmartDx As System.Windows.Forms.TextBox
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImgFormIcon As System.Windows.Forms.ImageList
    Public oMainForm As MainMenu

#Region " Windows Form Designer generated code "

    Public Sub New(Optional ByVal bIsFormerge As Boolean = False)
        MyBase.New()
        Me.bIsForMerge = bIsFormerge
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
    Friend WithEvents c1SmartDxList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSmartDxMaster))
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.c1SmartDxList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsbtn_Add = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Merge = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Copy = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Act_Deact_Rule = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.lblProvider = New System.Windows.Forms.Label()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.TxtMergeSmartDx = New System.Windows.Forms.TextBox()
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ImgFormIcon = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.c1SmartDxList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 0
        '
        'c1SmartDxList
        '
        Me.c1SmartDxList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1SmartDxList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1SmartDxList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1SmartDxList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1SmartDxList.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.c1SmartDxList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1SmartDxList.ExtendLastCol = True
        Me.c1SmartDxList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1SmartDxList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1SmartDxList.Location = New System.Drawing.Point(3, 2)
        Me.c1SmartDxList.Name = "c1SmartDxList"
        Me.c1SmartDxList.Rows.DefaultSize = 19
        Me.c1SmartDxList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1SmartDxList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1SmartDxList.ShowCellLabels = True
        Me.c1SmartDxList.Size = New System.Drawing.Size(822, 529)
        Me.c1SmartDxList.StyleInfo = resources.GetString("c1SmartDxList.StyleInfo")
        Me.c1SmartDxList.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(829, 53)
        Me.pnlToolStrip.TabIndex = 0
        Me.pnlToolStrip.TabStop = True
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtn_Add, Me.ts_btnEdit, Me.ts_btnDelete, Me.tsbtn_Merge, Me.tsbtn_Copy, Me.tsbtn_Act_Deact_Rule, Me.ts_btnRefresh, Me.tblSave, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(829, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tsbtn_Add
        '
        Me.tsbtn_Add.Image = CType(resources.GetObject("tsbtn_Add.Image"), System.Drawing.Image)
        Me.tsbtn_Add.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Add.Name = "tsbtn_Add"
        Me.tsbtn_Add.Size = New System.Drawing.Size(37, 50)
        Me.tsbtn_Add.Tag = "New"
        Me.tsbtn_Add.Text = "&New"
        Me.tsbtn_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnEdit
        '
        Me.ts_btnEdit.Image = CType(resources.GetObject("ts_btnEdit.Image"), System.Drawing.Image)
        Me.ts_btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnEdit.Name = "ts_btnEdit"
        Me.ts_btnEdit.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnEdit.Tag = "Edit"
        Me.ts_btnEdit.Text = "&Modify"
        Me.ts_btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'tsbtn_Merge
        '
        Me.tsbtn_Merge.Image = CType(resources.GetObject("tsbtn_Merge.Image"), System.Drawing.Image)
        Me.tsbtn_Merge.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Merge.Name = "tsbtn_Merge"
        Me.tsbtn_Merge.Size = New System.Drawing.Size(49, 50)
        Me.tsbtn_Merge.Tag = "Merge"
        Me.tsbtn_Merge.Text = "Mer&ge"
        Me.tsbtn_Merge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_Merge.ToolTipText = "Merge"
        '
        'tsbtn_Copy
        '
        Me.tsbtn_Copy.Image = CType(resources.GetObject("tsbtn_Copy.Image"), System.Drawing.Image)
        Me.tsbtn_Copy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Copy.Name = "tsbtn_Copy"
        Me.tsbtn_Copy.Size = New System.Drawing.Size(42, 50)
        Me.tsbtn_Copy.Tag = "Copy"
        Me.tsbtn_Copy.Text = "Co&py"
        Me.tsbtn_Copy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbtn_Act_Deact_Rule
        '
        Me.tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Resources.Activate
        Me.tsbtn_Act_Deact_Rule.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Act_Deact_Rule.Name = "tsbtn_Act_Deact_Rule"
        Me.tsbtn_Act_Deact_Rule.Size = New System.Drawing.Size(62, 50)
        Me.tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
        Me.tsbtn_Act_Deact_Rule.Text = "&Activate"
        Me.tsbtn_Act_Deact_Rule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'tblSave
        '
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(66, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save&&Cls"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save and Close"
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
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.c1SmartDxList)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 83)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(829, 534)
        Me.pnl_Base.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 530)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(821, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 2)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 529)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(825, 2)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 529)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(823, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.pnlTopRight)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 53)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSearch.Size = New System.Drawing.Size(829, 30)
        Me.pnlSearch.TabIndex = 0
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.lblProvider)
        Me.pnlTopRight.Controls.Add(Me.cmbProvider)
        Me.pnlTopRight.Controls.Add(Me.panel4)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label5)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(823, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'lblProvider
        '
        Me.lblProvider.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProvider.AutoSize = True
        Me.lblProvider.BackColor = System.Drawing.Color.Transparent
        Me.lblProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProvider.Location = New System.Drawing.Point(578, 5)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(63, 14)
        Me.lblProvider.TabIndex = 50
        Me.lblProvider.Text = "Provider  :"
        Me.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbProvider
        '
        Me.cmbProvider.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(644, 1)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(168, 22)
        Me.cmbProvider.TabIndex = 1
        Me.cmbProvider.DropDownStyle = ComboBoxStyle.DropDownList
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.TxtMergeSmartDx)
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
        Me.panel4.TabIndex = 49
        '
        'TxtMergeSmartDx
        '
        Me.TxtMergeSmartDx.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMergeSmartDx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtMergeSmartDx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMergeSmartDx.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtMergeSmartDx.Location = New System.Drawing.Point(5, 3)
        Me.TxtMergeSmartDx.Name = "TxtMergeSmartDx"
        Me.TxtMergeSmartDx.Size = New System.Drawing.Size(214, 15)
        Me.TxtMergeSmartDx.TabIndex = 0
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
        Me.btnClear.TabIndex = 46
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear Search ")
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
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 22)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(821, 1)
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
        Me.Label7.Location = New System.Drawing.Point(822, 1)
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
        Me.Label8.Size = New System.Drawing.Size(823, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'ImgFormIcon
        '
        Me.ImgFormIcon.ImageStream = CType(resources.GetObject("ImgFormIcon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgFormIcon.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgFormIcon.Images.SetKeyName(0, "MergeSmartDx.ico")
        '
        'frmSmartDxMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(829, 617)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSmartDxMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Smart Diagnosis"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.c1SmartDxList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub DesignGrid(ByVal GridControl As C1.Win.C1FlexGrid.C1FlexGrid)

        With c1SmartDxList
            .SetData(0, COL_To, "Merge To")
            .SetData(0, COL_Select, "Merge From")
            .SetData(0, COL_ID, "ID")
            .SetData(0, COL_NAME, "Name")
            .SetData(0, COL_ProviderName, "Provider")
            .SetData(0, COL_CreatedDateTime, "Created DateTime")
            .SetData(0, COL_ACTIVATED_STATUS, "Status")
            For i As Int16 = COL_ID To .Cols.Count - 1
                .Cols(i).AllowEditing = False
            Next
            If bIsForMerge = False Then
                .Cols(COL_To).Visible = False
                .Cols(COL_Select).Visible = False
                .Cols(COL_ID).Visible = False
                .Cols(COL_ACTIVATED_STATUS).Visible = False
                .Cols(COL_ProviderID).Visible = False
                .Cols(COL_ProviderName).Visible = True
                .Cols(COL_CreatedDateTime).Visible = True
                TxtMergeSmartDx.Visible = False
                txtSearch.Visible = True
                lblSearch.Text = "  Search :"
                tblSave.Visible = False
            Else
                .Cols(COL_To).Visible = True
                .Cols(COL_Select).Visible = True
                .Cols(COL_ID).Visible = False
                .Cols(COL_ACTIVATED_STATUS).Visible = False
                .Cols(COL_ProviderID).Visible = False
                .Cols(COL_ProviderName).Visible = False
                .Cols(COL_CreatedDateTime).Visible = False
                .Cols(COL_To).DataType = GetType(Boolean)
                .Cols(COL_Select).DataType = GetType(Boolean)
                txtSearch.Visible = False
                lblSearch.Text = "  Name :"
                btnClear.Visible = False
                TxtMergeSmartDx.Visible = True
                tblSave.Visible = True
                cmbProvider.Visible = False
                lblProvider.Visible = False
                RemoveHandler c1SmartDxList.MouseDoubleClick, AddressOf c1SmartDxList_MouseDoubleClick
            End If
            Dim _width As Integer = Width
            .Rows.Fixed = 1

        End With
    End Sub

    Private Sub frmSmartDxMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1SmartDxList)

        Try

            Call RefreshCategory()
            Fill_Provider()
            tsbtn_Act_Deact_Rule.Visible = False
            If bIsForMerge = True Then
                tsbtn_Add.Visible = False
                tsbtn_Copy.Visible = False
                tsbtn_Merge.Visible = False
                ts_btnEdit.Visible = False
                ts_btnRefresh.Visible = False
                ts_btnDelete.Visible = False
            End If
            ''Rule_Status()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub frmSmartDxMaster_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If c1SmartDxList.Cols.Count = COL_Count Then
            c1SmartDxList.Cols(COL_NAME).Width = c1SmartDxList.Width - 20
        End If
    End Sub

    Private Sub RefreshGrid()

        Dim dtSmartDx As DataTable = Nothing
        Dim oSmartDx As New smartDx
        Try
            dtSmartDx = oSmartDx.GetSmartDxList
            'c1SmartDxList.Clear()
            c1SmartDxList.DataSource = Nothing
            c1SmartDxList.BeginUpdate()
            c1SmartDxList.DataSource = dtSmartDx.Copy().DefaultView
            If dtSmartDx.Rows.Count <= 0 Then

                c1SmartDxList.Cols(COL_NAME).Width = 760 ''CInt(Width * 0.5)
                c1SmartDxList.Cols(COL_ProviderName).Width = 250 ''CInt(Width * 0.2)
                c1SmartDxList.Cols(COL_CreatedDateTime).Width = 190 ''CInt(Width * 0.1)
                c1SmartDxList.Cols(COL_ProviderID).DataType = GetType(String)
            Else

                c1SmartDxList.Cols(COL_NAME).Width = 760 ''CInt(Width * 0.6)
                c1SmartDxList.Cols(COL_ProviderName).Width = 250 '' CInt(Width * 0.2)
                c1SmartDxList.Cols(COL_CreatedDateTime).Width = 190 ''CInt(Width * 0.15)
                '' c1SmartDxList.Cols(COL_CreatedDateTime).Format = "MM/dd/yyy HH:mm tt"
                c1SmartDxList.Cols(COL_CreatedDateTime).DataType = GetType(DateTime)
                c1SmartDxList.Cols(COL_ProviderID).DataType = GetType(String)
            End If
            c1SmartDxList.EndUpdate()
            cmbProvider.SelectedValue = 0
        Catch ex As Exception
            c1SmartDxList.Cols(COL_NAME).Width = 760 ''CInt(Width * 0.6)
            c1SmartDxList.Cols(COL_ProviderName).Width = 250 '' CInt(Width * 0.2)
            c1SmartDxList.Cols(COL_CreatedDateTime).Width = 190 ''CInt(Width * 0.15) 
        Finally
            If dtSmartDx IsNot Nothing Then
                dtSmartDx.Dispose()
                dtSmartDx = Nothing

            End If
            If oSmartDx IsNot Nothing Then
                oSmartDx.Dispose()
                oSmartDx = Nothing
            End If
            c1SmartDxList.EndUpdate()
        End Try

    End Sub

    Private Sub c1SmartDxList_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1SmartDxList.AfterEdit
        If c1SmartDxList.Rows.Count > 0 Then

            With c1SmartDxList
                If e.Col = COL_To Then
                    For i As Integer = 1 To .Rows.Count - 1
                        If .GetCellCheck(i, COL_To) = C1.Win.C1FlexGrid.CheckEnum.Checked And .RowSel = i Then
                            .SetCellCheck(i, COL_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                            .Rows(i).AllowEditing = False
                            TxtMergeSmartDx.Text = Convert.ToString(.GetData(i, COL_NAME))
                        Else
                            .Rows(i).AllowEditing = True
                            .SetCellCheck(i, COL_To, C1.Win.C1FlexGrid.CheckEnum.Unchecked)

                        End If


                    Next
                End If
            End With


        End If
    End Sub

    Private Sub c1SmartDxList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1SmartDxList.MouseDoubleClick

        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1SmartDxList.HitTest(ptPoint)

        With c1SmartDxList
            If .Rows.Count > 1 And htInfo.Row > 0 Then
                Dim _ID As Long
                Dim _nProviderID As Int64
                Dim _EditName As String


                _ID = .GetData(.Row, COL_ID)
                _EditName = .GetData(.Row, COL_NAME)
                _nProviderID = Convert.ToInt64(.GetData(.Row, COL_ProviderID))
                If _ID > 0 And _EditName.Trim <> "" Then
                    Dim oSmartDx As New smartDx
                    Dim frm As New frmICD9Association
                    Try
                        With frm
                            frmICD9Association.ISICD9AssocOpen = True
                            frmICD9Association.ICD9SelNodeKey = _ID
                            frmICD9Association.ICDSmarDxName = _EditName
                            frmICD9Association.ISCopyICDSmarDx = False
                            frmICD9Association.nProviderID = _nProviderID
                            .WindowState = FormWindowState.Maximized
                            .Owner = Me
                            '  .MdiParent = Me
                            Dim pt As Point = Me.Location
                            pt.X = pt.X + (Me.Width / 2)
                            pt.Y = pt.Y - 200
                            .Location = pt
                            ' 
                            .ShowDialog(frm.Parent)
                            .Close()
                            .Dispose()
                            RefreshCategory()
                        End With
                        frm = Nothing
                    Catch objErr As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        If oSmartDx IsNot Nothing Then
                            oSmartDx.Dispose()
                            oSmartDx = Nothing
                        End If
                        If frm IsNot Nothing Then
                            frm.Dispose()
                            frm = Nothing
                        End If
                    End Try
                Else
                    MessageBox.Show("No Record Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End With
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try

            Dim strSearch As String
            With (txtSearch)
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            Dim dvDisease As DataView

            dvDisease = CType(c1SmartDxList.DataSource(), DataView)

            If (IsNothing(dvDisease) = False AndAlso strSearch <> "") Then
                If (cmbProvider.SelectedValue <= 0) Then
                    dvDisease.RowFilter = dvDisease.Table.Columns(COL_NAME).ColumnName & " Like '%" & strSearch.Trim.Replace("'", "''") & "%' "
                Else
                    dvDisease.RowFilter = dvDisease.Table.Columns(COL_NAME).ColumnName & " Like '%" & strSearch.Trim.Replace("'", "''") & "%' AND " &
                          dvDisease.Table.Columns(COL_ProviderID).ColumnName & "=" & cmbProvider.SelectedValue.ToString().Trim.Replace("'", "''") & " "

                End If
            ElseIf (IsNothing(dvDisease) = False AndAlso strSearch = "" AndAlso cmbProvider.SelectedValue > 0) Then
                dvDisease.RowFilter = dvDisease.Table.Columns(COL_ProviderID).ColumnName & "=" & cmbProvider.SelectedValue.ToString().Trim.Replace("'", "''") & " "
            Else
                dvDisease.RowFilter = ""
            End If
            c1SmartDxList.DataSource = dvDisease

        Catch ex As Exception
            If ex.Message.Contains("Error in Like operator") = True Then
                MessageBox.Show("Invalid search criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show(ex.Message)
            End If

        End Try
    End Sub

    Private Sub AddCategory()

        Dim frm As New frmICD9Association

        Try
            With frm
                frmICD9Association.ISICD9AssocOpen = True
                frmICD9Association.ICD9SelNodeKey = 0
                frmICD9Association.ICDSmarDxName = ""
                frmICD9Association.ISCopyICDSmarDx = False
                frmICD9Association.nProviderID = 0
                .WindowState = FormWindowState.Maximized
                ' .Owner = Me  ''commented for case CAS-07954-Q2T2Z8 
                Dim pt As Point = Me.Location
                pt.X = pt.X + (Me.Width / 2)
                pt.Y = pt.Y - 200
                .Location = pt
                .ShowDialog(Me) ''me added for case CAS-07954-Q2T2Z8 
                .Close()
                .Dispose()
                RefreshCategory()
            End With
            frm = Nothing
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            
            If frm IsNot Nothing Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try

    End Sub

    Private Sub UpdateCategory()
        'oDMSetup delete data of selected criteria
        With c1SmartDxList
            If .Rows.Count > 1 Then
                Dim _ID As Long
                Dim _EditName As String
                Dim _nProviderID As Int64
                If .Row > 0 Then
                    _ID = .GetData(.Row, COL_ID)
                    _EditName = .GetData(.Row, COL_NAME)
                    _nProviderID = .GetData(.Row, COL_ProviderID)

                    If _ID > 0 And _EditName.Trim <> "" Then
                        Dim oSmartDx As New smartDx
                        Dim frm As New frmICD9Association
                        Try
                            With frm
                                frmICD9Association.ISICD9AssocOpen = True
                                frmICD9Association.ICD9SelNodeKey = _ID
                                frmICD9Association.ICDSmarDxName = _EditName
                                frmICD9Association.ISCopyICDSmarDx = False
                                frmICD9Association.nProviderID = _nProviderID
                                .WindowState = FormWindowState.Maximized
                                .Owner = Me
                                '  .MdiParent = Me
                                Dim pt As Point = Me.Location
                                pt.X = pt.X + (Me.Width / 2)
                                pt.Y = pt.Y - 200
                                .Location = pt
                                .ShowDialog(frm.Parent)
                                .Close()
                                .Dispose()
                                RefreshCategory()
                            End With
                            frm = Nothing
                        Catch objErr As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            If oSmartDx IsNot Nothing Then
                                oSmartDx.Dispose()
                                oSmartDx = Nothing
                            End If
                            If frm IsNot Nothing Then
                                frm.Dispose()
                                frm = Nothing
                            End If
                        End Try
                    Else
                        MessageBox.Show("No Record Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub DeleteCategory()
        Dim oSmartDx As New smartDx
        With c1SmartDxList
            If .Rows.Count > 1 Then
                Dim _ID As Long
                Dim _EditName As String
                If .Row > 0 Then
                    _ID = .GetData(.Row, COL_ID)
                    _EditName = .GetData(.Row, COL_NAME)

                    If _ID > 0 And _EditName.Trim <> "" Then

                        If MessageBox.Show("Are you sure you want to delete the current record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                            'If MessageBox.Show("Are You sure to delete current record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            oSmartDx.DeleteSmartDx(_ID, _EditName)
                            RefreshCategory()
                        End If
                    End If
                End If
            End If
        End With
        oSmartDx.Dispose()
        oSmartDx = Nothing
    End Sub

    Private Sub RefreshCategory()

        txtSearch.Focus()
        Try
            RefreshGrid()
            DesignGrid(c1SmartDxList)
            If txtSearch.Text.Trim().Length > 0 Then
                Dim sender As Object = Nothing
                Dim e As System.EventArgs = Nothing
                txtSearch_TextChanged(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormClose()
        Me.Close()
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked

        Try

            Select Case e.ClickedItem.Tag
                Case "New"
                    Me.Cursor = Cursors.WaitCursor
                    Call AddCategory()
                Case "Edit"
                    Me.Cursor = Cursors.WaitCursor
                    Call UpdateCategory()
                Case "Delete"
                    Call DeleteCategory()
                Case "Refresh"
                    Me.Cursor = Cursors.WaitCursor
                    Call RefreshCategory()
                    txtSearch.Clear()
                Case "Close"
                    Call FormClose()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.Close, "'" & gstrLoginName & "' successfully Closed DM_RuleSetup Screen", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End Select
        Catch ex As Exception

        Finally

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub c1SmartDxList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1SmartDxList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub tsbtn_Merge_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_Merge.Click
        Try
            Dim hicon As IntPtr
            RemoveHandler c1SmartDxList.MouseDoubleClick, AddressOf c1SmartDxList_MouseDoubleClick
            Dim oDMModule As New frmSmartDxMaster(True)
            oDMModule.WindowState = FormWindowState.Maximized
            oDMModule.MaximizeBox = True
            oDMModule.ShowInTaskbar = False
            oDMModule.Text = "Merge Smart Diagnosis"
            hicon = DirectCast(ImgFormIcon.Images(0), Bitmap).GetHicon()
            oDMModule.Icon = System.Drawing.Icon.FromHandle(hicon)
            oDMModule.ShowDialog(Me)
            If oDMModule IsNot Nothing Then
                oDMModule.Dispose()
                oDMModule = Nothing
            End If
            RefreshCategory()
            AddHandler c1SmartDxList.MouseDoubleClick, AddressOf c1SmartDxList_MouseDoubleClick
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub tsbtn_Copy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbtn_Copy.Click

        With c1SmartDxList
            If .Rows.Count > 1 Then
                Dim _ID As Long
                Dim _EditName As String
                Dim _nProviderID As Int64
                If .Row > 0 Then
                    _ID = .GetData(.Row, COL_ID)
                    _EditName = .GetData(.Row, COL_NAME)
                    _nProviderID = .GetData(.Row, COL_ProviderID)
                    If _ID > 0 And _EditName.Trim <> "" Then
                        Dim oSmartDx As New smartDx
                        Dim frm As New frmICD9Association
                        Try
                            With frm
                                frmICD9Association.ISICD9AssocOpen = True
                                frmICD9Association.ICD9SelNodeKey = _ID
                                frmICD9Association.ICDSmarDxName = ""
                                frmICD9Association.ISCopyICDSmarDx = True
                                frmICD9Association.nProviderID = _nProviderID
                                .WindowState = FormWindowState.Maximized
                                .Owner = Me
                                '  .MdiParent = Me
                                Dim pt As Point = Me.Location
                                pt.X = pt.X + (Me.Width / 2)
                                pt.Y = pt.Y - 200
                                .Location = pt
                                .ShowDialog(frm.Parent)
                                .Close()
                                .Dispose()
                                RefreshCategory()
                            End With
                            frm = Nothing
                        Catch objErr As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            If oSmartDx IsNot Nothing Then
                                oSmartDx.Dispose()
                                oSmartDx = Nothing
                            End If
                            If frm IsNot Nothing Then
                                frm.Dispose()
                                frm = Nothing
                            End If

                        End Try
                    Else
                        MessageBox.Show("No Record Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub c1SmartDxList_AfterSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1SmartDxList.AfterSort
        If (c1SmartDxList.Rows.Count > 1) Then
            'c1DiseaseList.Select(1, 0)
            'Code changes for maintaining the selected row after sorting 
            Dim _index As Integer = c1SmartDxList.FindRow(_id, 0, COL_ID, False, False, False)
            c1SmartDxList.ShowCell(_index, COL_NAME)
            c1SmartDxList.Row = _index
            c1SmartDxList.Select()
        End If
    End Sub
    'Code changes for maintaining the selected row after sorting 
    Dim _id As Int64
    Private Sub c1SmartDxList_BeforeSort(sender As System.Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1SmartDxList.BeforeSort
        If (c1SmartDxList.Rows.Count > 1) Then
            _id = c1SmartDxList.Rows(c1SmartDxList.RowSel)(COL_ID)
        End If
    End Sub

    Private Sub Fill_Provider()
        Try
            RemoveHandler cmbProvider.SelectedValueChanged, AddressOf cmbProvider_SelectedValueChanged
            Dim dt As DataTable
            Dim oProvider As gloAppointmentBook.Books.Resource = New gloAppointmentBook.Books.Resource(GetConnectionString())
            dt = oProvider.GetProviders()

            If Not dt Is Nothing Then
                Dim dr As DataRow = dt.NewRow()
                dr("nProviderID") = 0
                dr("ProviderName") = "All"
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()

                cmbProvider.DataSource = dt.Copy()
                cmbProvider.ValueMember = dt.Columns("nProviderID").ColumnName
                cmbProvider.DisplayMember = dt.Columns("ProviderName").ColumnName
                cmbProvider.Refresh()

                dt.Dispose()
                dt = Nothing
            End If

            oProvider.Dispose()
            AddHandler cmbProvider.SelectedValueChanged, AddressOf cmbProvider_SelectedValueChanged
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub cmbProvider_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbProvider.SelectedValueChanged
        Try

            If Convert.ToInt64(cmbProvider.SelectedValue) <> 0 Then

                Dim dvDisease As DataView
                Dim strSearch As String
                strSearch = Convert.ToString(cmbProvider.SelectedValue).Trim.Replace("'", "''")
                dvDisease = CType(c1SmartDxList.DataSource(), DataView)
                If (IsNothing(dvDisease) = False) Then
                    dvDisease.RowFilter = dvDisease.Table.Columns(COL_ProviderID).ColumnName & "=" & cmbProvider.SelectedValue.ToString().Trim.Replace("'", "''") & " "
                End If
                c1SmartDxList.BeginUpdate()
                c1SmartDxList.DataSource = dvDisease
                c1SmartDxList.EndUpdate()
                c1SmartDxList.Cols(0).Width = 0
                c1SmartDxList.Cols(COL_ProviderID).Width = 0
                DesignGrid(c1SmartDxList)
            Else
                RefreshCategory()
            End If
        Catch ex As Exception

            If ex.Message.Contains("Error in Like operator") = True Then
                MessageBox.Show("Invalid search criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show(ex.Message)
            End If

        End Try
    End Sub

    Private Sub tblSave_Click(sender As System.Object, e As System.EventArgs) Handles tblSave.Click
        Dim nMergeInToSmartDxId As Int64 = 0
        Dim sMergeFromSmartDxIdss As String = ""
        Dim oSmartDx As New smartDx

        Try
            With c1SmartDxList

                Dim _nMergeintoRow As Integer = 0
                Dim _nMergeFromRow As Integer = 0
                If .Rows.Count > 0 Then
                    _nMergeintoRow = .FindRow(C1.Win.C1FlexGrid.CheckEnum.Checked.GetHashCode(), 1, COL_To, True)
                    _nMergeFromRow = .FindRow(C1.Win.C1FlexGrid.CheckEnum.Checked.GetHashCode(), 1, COL_Select, True)
                End If
                If _nMergeintoRow <= 0 Then
                    MessageBox.Show("Select merge ""To"" SmartDx.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                Else
                    nMergeInToSmartDxId = Convert.ToInt64(.GetData(_nMergeintoRow, COL_ID))
                End If
                If _nMergeFromRow <= 0 Then
                    MessageBox.Show("Select at least one ""From"" SmartDx for merge.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                For i As Integer = 1 To .Rows.Count - 1
                    If .GetCellCheck(i, COL_Select) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        sMergeFromSmartDxIdss = sMergeFromSmartDxIdss + "," + Convert.ToString(.GetData(i, COL_ID))
                    End If

                Next

                If sMergeFromSmartDxIdss <> "" Then
                    Dim Result As Integer = 0
                    sMergeFromSmartDxIdss = sMergeFromSmartDxIdss.Substring(1, sMergeFromSmartDxIdss.Length - 1)
                    Result = oSmartDx.MergeSmartDx(nMergeInToSmartDxId, sMergeFromSmartDxIdss, TxtMergeSmartDx.Text.Trim())

                    If Result > 0 Then
                        Me.Close()
                    End If

                End If

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oSmartDx IsNot Nothing Then
                oSmartDx.Dispose()
                oSmartDx = Nothing
            End If

        End Try


    End Sub

End Class
