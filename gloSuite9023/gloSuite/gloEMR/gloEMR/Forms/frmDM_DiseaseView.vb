Public Class frmDM_DiseaseView
    Inherits System.Windows.Forms.Form

    Private COL_ID As Integer = 0
    Private COL_NAME As Integer = 1
    Private COL_DESC As Integer = 2
    Private COL_ACTIVATED_STATUS As Integer = 3
    Private COL_ACTIVATED_DATE As Integer = 4
    Private COL_ACTIVATED_USER As Integer = 5
    Private COL_NOTE As Integer = 6
    Private COL_COUNT As Integer = 6
    Private COl_OldRule As Integer = 7
    Private COl_OldRuleCount As Integer = 8

    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_ViewRuleHistory As System.Windows.Forms.ToolStripButton
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
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_NewDMSetup As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_CopyRule As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Act_Deact_Rule As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label

    Public oMainForm As MainMenu

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
    Friend WithEvents c1DiseaseList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnDiseaseSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_DiseaseView))
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnDiseaseSearch = New System.Windows.Forms.Button()
        Me.c1DiseaseList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsbtn_NewDMSetup = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_CopyRule = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Act_Deact_Rule = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_ViewRuleHistory = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        CType(Me.c1DiseaseList, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'btnDiseaseSearch
        '
        Me.btnDiseaseSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDiseaseSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDiseaseSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDiseaseSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDiseaseSearch.Location = New System.Drawing.Point(735, 12)
        Me.btnDiseaseSearch.Name = "btnDiseaseSearch"
        Me.btnDiseaseSearch.Size = New System.Drawing.Size(62, 23)
        Me.btnDiseaseSearch.TabIndex = 5
        Me.btnDiseaseSearch.Text = "Search"
        Me.btnDiseaseSearch.Visible = False
        '
        'c1DiseaseList
        '
        Me.c1DiseaseList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1DiseaseList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1DiseaseList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1DiseaseList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1DiseaseList.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.c1DiseaseList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1DiseaseList.ExtendLastCol = True
        Me.c1DiseaseList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1DiseaseList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1DiseaseList.Location = New System.Drawing.Point(3, 2)
        Me.c1DiseaseList.Name = "c1DiseaseList"
        Me.c1DiseaseList.Rows.DefaultSize = 19
        Me.c1DiseaseList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1DiseaseList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1DiseaseList.ShowCellLabels = True
        Me.c1DiseaseList.Size = New System.Drawing.Size(822, 529)
        Me.c1DiseaseList.StyleInfo = resources.GetString("c1DiseaseList.StyleInfo")
        Me.c1DiseaseList.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.btnDiseaseSearch)
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(829, 53)
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtn_NewDMSetup, Me.ts_btnModify, Me.ToolStripButton2, Me.ts_btnDelete, Me.ts_gloCommunityDownload, Me.tsbtn_CopyRule, Me.tsbtn_Act_Deact_Rule, Me.tsbtn_ViewRuleHistory, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(829, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tsbtn_NewDMSetup
        '
        Me.tsbtn_NewDMSetup.Image = CType(resources.GetObject("tsbtn_NewDMSetup.Image"), System.Drawing.Image)
        Me.tsbtn_NewDMSetup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_NewDMSetup.Name = "tsbtn_NewDMSetup"
        Me.tsbtn_NewDMSetup.Size = New System.Drawing.Size(37, 50)
        Me.tsbtn_NewDMSetup.Tag = "DM"
        Me.tsbtn_NewDMSetup.Text = "&New"
        Me.tsbtn_NewDMSetup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(52, 50)
        Me.ToolStripButton2.Tag = "Search"
        Me.ToolStripButton2.Text = "&Search"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton2.Visible = False
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
        'tsbtn_CopyRule
        '
        Me.tsbtn_CopyRule.Image = CType(resources.GetObject("tsbtn_CopyRule.Image"), System.Drawing.Image)
        Me.tsbtn_CopyRule.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_CopyRule.Name = "tsbtn_CopyRule"
        Me.tsbtn_CopyRule.Size = New System.Drawing.Size(73, 50)
        Me.tsbtn_CopyRule.Tag = "DM"
        Me.tsbtn_CopyRule.Text = "C&opy Rule"
        Me.tsbtn_CopyRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'tsbtn_ViewRuleHistory
        '
        Me.tsbtn_ViewRuleHistory.Image = CType(resources.GetObject("tsbtn_ViewRuleHistory.Image"), System.Drawing.Image)
        Me.tsbtn_ViewRuleHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_ViewRuleHistory.Name = "tsbtn_ViewRuleHistory"
        Me.tsbtn_ViewRuleHistory.Size = New System.Drawing.Size(88, 50)
        Me.tsbtn_ViewRuleHistory.Tag = "ViewHistory"
        Me.tsbtn_ViewRuleHistory.Text = "&View History"
        Me.tsbtn_ViewRuleHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_ViewRuleHistory.ToolTipText = "View rule activation/de-activation history"
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
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.c1DiseaseList)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 83)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(829, 534)
        Me.pnl_Base.TabIndex = 2
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
        Me.pnlSearch.TabIndex = 3
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
        Me.pnlTopRight.Size = New System.Drawing.Size(823, 24)
        Me.pnlTopRight.TabIndex = 1
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
        Me.panel4.TabIndex = 49
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
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.White
        Me.label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.label21.Location = New System.Drawing.Point(5, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(214, 3)
        Me.label21.TabIndex = 37
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
        'frmDM_DiseaseView
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
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
        Me.Name = "frmDM_DiseaseView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DM Setup [Clinical Recommendation Rules]"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.c1DiseaseList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub DesignGrid(ByVal GridControl As C1.Win.C1FlexGrid.C1FlexGrid)
        'With GridControl
        '    .Rows.Count = 1
        '    .Cols.Count = COL_COUNT
        '    .Rows.Fixed = 1
        '    .Cols.Fixed = 0
        With c1DiseaseList
            .SetData(0, COL_ID, "ID")
            .SetData(0, COL_NAME, "Name")
            .SetData(0, COL_DESC, "Description")
            .SetData(0, COL_ACTIVATED_STATUS, "Status")
            .SetData(0, COL_ACTIVATED_DATE, "Activation Date")
            .SetData(0, COL_ACTIVATED_USER, "Activation User")
            .SetData(0, COL_NOTE, "Note")


            For i As Int16 = 0 To .Cols.Count - 1
                .Cols(i).AllowEditing = False
            Next

            .Cols(COL_ID).Visible = False
            .Cols(COL_NAME).Visible = True
            .Cols(COL_NOTE).Visible = False
            Dim _width As Integer = Width
            .Rows.Fixed = 1
        End With
    End Sub

    Private Sub frmDM_DiseaseView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1DiseaseList)

        Try
            '
            'Code Start added by kanchan on 20120102 for gloCommunity integration
            If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
                ts_gloCommunityDownload.Visible = gblngloCommunity
            End If
            'Code end added by kanchan on 20120102 for gloCommunity integration
            Call RefreshCategory()
            Rule_Status()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub frmDM_DiseaseView_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If c1DiseaseList.Cols.Count = COL_COUNT Then
            c1DiseaseList.Cols(COL_NAME).Width = c1DiseaseList.Width - 20
        End If
    End Sub

    Private Sub Fill_Diseases()
        'Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
        'Dim oCriteries As New gloStream.DiseaseManagement.Supporting.ItemDetails
        'odm.GetCriteraList 
    End Sub


    Private Sub RefreshGrid()
        Dim dtCriteria As DataTable = Nothing
        ' Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
        Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
        Try


            dtCriteria = oDM.GetCritera

            'Dim rowindex As Integer = 0
            ''For rowindex = 0 To dtCriteria.Rows.Count - 1
            ''    If Not IsDBNull(dtCriteria.Rows(rowindex)("OldRulePresent")) Then
            ''        Exit For
            ''    End If
            ''Next

            'c1DiseaseList.Clear()
            c1DiseaseList.DataSource = Nothing
            c1DiseaseList.BeginUpdate()
            c1DiseaseList.DataSource = dtCriteria.Copy().DefaultView

            If dtCriteria.Rows.Count > 0 And dtCriteria.Rows(0)("IsOldCount") <= 0 Then
                c1DiseaseList.Cols(COl_OldRule).Visible = False
                c1DiseaseList.Cols(COl_OldRuleCount).Visible = False
                c1DiseaseList.Cols(COl_OldRule).Width = 0
                c1DiseaseList.Cols(COl_OldRuleCount).Width = 0

                c1DiseaseList.Cols(COL_NAME).Width = Width * 0.3
                c1DiseaseList.Cols(COL_DESC).Width = Width * 0.36
                c1DiseaseList.Cols(COL_ACTIVATED_STATUS).Width = Width * 0.1
                c1DiseaseList.Cols(COL_ACTIVATED_DATE).Width = Width * 0.15
                c1DiseaseList.Cols(COL_ACTIVATED_USER).Width = Width * 0.1

            ElseIf dtCriteria.Rows.Count <= 0 Then
                c1DiseaseList.Cols(COl_OldRule).Visible = False
                c1DiseaseList.Cols(COl_OldRuleCount).Visible = False
                c1DiseaseList.Cols(COl_OldRule).Width = 0
                c1DiseaseList.Cols(COl_OldRuleCount).Width = 0

                c1DiseaseList.Cols(COL_NAME).Width = Width * 0.36
                c1DiseaseList.Cols(COL_DESC).Width = Width * 0.4
                c1DiseaseList.Cols(COL_ACTIVATED_STATUS).Width = Width * 0.1
                c1DiseaseList.Cols(COL_ACTIVATED_DATE).Width = Width * 0.15
                c1DiseaseList.Cols(COL_ACTIVATED_USER).Width = Width * 0.1
            Else
                c1DiseaseList.SetData(0, COl_OldRule, "Rule Type")
                c1DiseaseList.Cols(COl_OldRuleCount).Visible = False
                '' c1DiseaseList.Width = c1DiseaseList.Width * 0.1
                c1DiseaseList.Cols(COL_NAME).Width = Width * 0.2
                c1DiseaseList.Cols(COL_DESC).Width = Width * 0.36
                c1DiseaseList.Cols(COL_ACTIVATED_STATUS).Width = Width * 0.1
                c1DiseaseList.Cols(COL_ACTIVATED_DATE).Width = Width * 0.15
                c1DiseaseList.Cols(COL_ACTIVATED_USER).Width = Width * 0.1
                c1DiseaseList.Cols(COl_OldRule).DataType = GetType(System.String)
            End If
            c1DiseaseList.EndUpdate()
            Rule_Status()

        Catch ex As Exception
            c1DiseaseList.Cols(COl_OldRule).Visible = False
            c1DiseaseList.Cols(COl_OldRuleCount).Visible = False
            c1DiseaseList.Cols(COl_OldRule).Width = 0
            c1DiseaseList.Cols(COl_OldRuleCount).Width = 0

            c1DiseaseList.Cols(COL_NAME).Width = Width * 0.3
            c1DiseaseList.Cols(COL_DESC).Width = Width * 0.36
            c1DiseaseList.Cols(COL_ACTIVATED_STATUS).Width = Width * 0.1
            c1DiseaseList.Cols(COL_ACTIVATED_DATE).Width = Width * 0.15
            c1DiseaseList.Cols(COL_ACTIVATED_USER).Width = Width * 0.1
            '' MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing

            End If
            If dtCriteria IsNot Nothing Then
                dtCriteria.Dispose()
                dtCriteria = Nothing
            End If
            c1DiseaseList.EndUpdate()
        End Try

    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnDiseaseSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiseaseSearch.Click
        If txtSearch.Text.Trim.Length > 0 Then
            Dim _FindRow As Integer = 0
            _FindRow = c1DiseaseList.FindRow(txtSearch.Text.Trim, 0, COL_NAME, False, False, False)
            c1DiseaseList.ShowCell(_FindRow, COL_NAME)
            c1DiseaseList.Row = _FindRow
            c1DiseaseList.Select()
        End If
    End Sub

    Private Sub c1DiseaseList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1DiseaseList.MouseDoubleClick

        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1DiseaseList.HitTest(ptPoint)
        With c1DiseaseList
            If .Rows.Count > 1 And htInfo.Row > 0 Then
                Dim _ID As Long
                Dim _EditName As String

                _ID = .GetData(.Row, COL_ID)
                _EditName = .GetData(.Row, COL_NAME)

                If _ID > 0 And _EditName.Trim <> "" Then
                    'Shubhangi 20091007
                    'Use wait cursor to open form for modify.
                    Me.Cursor = Cursors.WaitCursor
                    Dim oDMSetup As New frmDM_RulesSetup(True, _ID, _EditName)
                    oDMSetup.WindowState = FormWindowState.Normal
                    oDMSetup.StartPosition = FormStartPosition.CenterScreen
                    oDMSetup.ShowInTaskbar = False
                    oDMSetup.ShowDialog(IIf(IsNothing(oDMSetup.Parent), Me, oDMSetup.Parent))
                    'Shubhangi 20091007
                    Me.Cursor = Cursors.Arrow
                    RefreshCategory()
                    oDMSetup.Dispose()
                Else
                    MessageBox.Show("No Record Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End With
    End Sub

    Private Sub Panel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        Dim dvDisease As DataView = Nothing

        Try

            Dim strSearch As String
            With txtSearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            If IsNothing(c1DiseaseList) = False Then

                dvDisease = CType(c1DiseaseList.DataSource(), DataView)

                If (IsNothing(dvDisease) = False) Then
                    dvDisease = FilterData(dvDisease)

                    If IsNothing(dvDisease) = False Then
                        c1DiseaseList.DataSource = dvDisease
                        SetFlexStyleAfterFilter(dvDisease)
                        DesignGrid(c1DiseaseList)
                    End If

                Else
                    c1DiseaseList.DataSource = dvDisease
                End If

                c1DiseaseList.Cols(0).Width = 0
                Rule_Status()

            End If

        Catch ex As Exception

            If ex.Message.Contains("Error in Like operator") = True Then
                MessageBox.Show("Invalid search criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show(ex.Message)
            End If

        End Try

    End Sub
    Private Sub SetFlexStyleAfterFilter(ByVal dvDisease As DataView)
        c1DiseaseList.BeginUpdate()


        If dvDisease.Table.Rows.Count > 0 And dvDisease.Table.Rows(0)("IsOldCount") <= 0 Then
            c1DiseaseList.Cols(COl_OldRule).Visible = False
            c1DiseaseList.Cols(COl_OldRuleCount).Visible = False
            c1DiseaseList.Cols(COl_OldRule).Width = 0
            c1DiseaseList.Cols(COl_OldRuleCount).Width = 0

            c1DiseaseList.Cols(COL_NAME).Width = Width * 0.3
            c1DiseaseList.Cols(COL_DESC).Width = Width * 0.36
            c1DiseaseList.Cols(COL_ACTIVATED_STATUS).Width = Width * 0.1
            c1DiseaseList.Cols(COL_ACTIVATED_DATE).Width = Width * 0.15
            c1DiseaseList.Cols(COL_ACTIVATED_USER).Width = Width * 0.1

        ElseIf dvDisease.Table.Rows.Count <= 0 Then
            c1DiseaseList.Cols(COl_OldRule).Visible = False
            c1DiseaseList.Cols(COl_OldRuleCount).Visible = False
            c1DiseaseList.Cols(COl_OldRule).Width = 0
            c1DiseaseList.Cols(COl_OldRuleCount).Width = 0

            c1DiseaseList.Cols(COL_NAME).Width = Width * 0.36
            c1DiseaseList.Cols(COL_DESC).Width = Width * 0.4
            c1DiseaseList.Cols(COL_ACTIVATED_STATUS).Width = Width * 0.1
            c1DiseaseList.Cols(COL_ACTIVATED_DATE).Width = Width * 0.15
            c1DiseaseList.Cols(COL_ACTIVATED_USER).Width = Width * 0.1
        Else
            c1DiseaseList.SetData(0, COl_OldRule, "Rule Type")
            c1DiseaseList.Cols(COl_OldRuleCount).Visible = False
            '' c1DiseaseList.Width = c1DiseaseList.Width * 0.1
            c1DiseaseList.Cols(COL_NAME).Width = Width * 0.2
            c1DiseaseList.Cols(COL_DESC).Width = Width * 0.36
            c1DiseaseList.Cols(COL_ACTIVATED_STATUS).Width = Width * 0.1
            c1DiseaseList.Cols(COL_ACTIVATED_DATE).Width = Width * 0.15
            c1DiseaseList.Cols(COL_ACTIVATED_USER).Width = Width * 0.1
            c1DiseaseList.Cols(COl_OldRule).DataType = GetType(System.String)
        End If
        c1DiseaseList.EndUpdate()
    End Sub

    Private Function FilterData(ByVal DvDataView As DataView) As DataView

        Try

            Dim filterrow As EnumerableRowCollection(Of DataRow) = From element As DataRow In DvDataView.Table.AsEnumerable()
                                                                   Where (Convert.ToString(element(1)).ToLower()).Contains(txtSearch.Text.ToLower())
                                                                   Select element


            DvDataView = filterrow.AsDataView()

        Catch ex As Exception
            Return Nothing
        End Try

        Return DvDataView

    End Function


    'Private Sub btnFindPatients_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindPatients.Click
    '    'oDMSetup delete data of selected criteria
    '    Try


    '    Dim _strFind As String = ""
    '    With c1DiseaseList
    '        If .Rows.Count > 1 Then
    '            Dim _ID As Long

    '            _ID = .GetData(.Row, COL_ID)

    '            If _ID > 0 Then
    '                Dim oDMFind As New gloStream.DiseaseManagement.DiseaseManagement
    '                Dim oPatients As New Collection
    '                With oDMFind
    '                    oPatients = .FindGuidelinesForMultiplePatient(_ID)
    '                End With
    '                oDMFind = Nothing

    '                If Not oPatients Is Nothing Then
    '                    '''''<><><><><> Check Patient Status <><><><><><>''''
    '                    ''''' 20070125 -Mahesh 
    '                    '//If CheckPatientStatus(gnPatientID) = False Then
    '                    '//Exit Sub
    '                    '//End If
    '                    '''''<><><><><> Check Patient Status <><><><><><>''''

    '                        'Col_CriteriaID.Add(390970683860170001)
    '                        Dim Col_CriteriaID As New Collection
    '                        Col_CriteriaID.Add(_ID)
    '                        Dim frm As New frmDM_Template(gnPatientID, Col_CriteriaID, oPatients)
    '                        With frm
    '                            .MdiParent = oMainForm
    '                            .ShowInTaskbar = False
    '                            .WindowState = FormWindowState.Maximized
    '                            .Show()
    '                        End With
    '                    End If
    '            Else
    '                MessageBox.Show("Please select criteria.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If
    '        End If
    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show("Sorry, no patient found", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    'End Sub



    ''Code Added by Shilpa for adding the new buttons on 13th Nov 2007
    Private Sub AddCategory()
        Try
            Dim oDMSetup As New frmDM_Setup
            With oDMSetup

                .ShowInTaskbar = False
                .WindowState = FormWindowState.Normal
                .StartPosition = FormStartPosition.CenterScreen
                .BringToFront()
                .ShowDialog(IIf(IsNothing(oDMSetup.Parent), Me, oDMSetup.Parent))
            End With
            'Dim oDMSetup As New frmDM_Setup()

            'oDMSetup.ShowDialog(Me)
            RefreshCategory()
            oMainForm.Fill_DMCriteriaMenu()
            oDMSetup.Dispose()
            oDMSetup = Nothing
            'oDMModule.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub UpdateCategory()
        'oDMSetup delete data of selected criteria
        With c1DiseaseList
            If .Rows.Count > 1 Then
                Dim _ID As Long
                Dim _EditName As String
                If .Row > 0 Then
                    _ID = .GetData(.Row, COL_ID)
                    _EditName = .GetData(.Row, COL_NAME)

                    If _ID > 0 And _EditName.Trim <> "" Then
                        Me.Cursor = Cursors.WaitCursor
                        Dim oDMSetup As New frmDM_RulesSetup(True, _ID, _EditName)
                        oDMSetup.WindowState = FormWindowState.Normal
                        oDMSetup.StartPosition = FormStartPosition.CenterScreen
                        oDMSetup.ShowInTaskbar = False
                        oDMSetup.ShowDialog(IIf(IsNothing(oDMSetup.Parent), Me, oDMSetup.Parent))
                        'Shubhangi 20091007
                        Me.Cursor = Cursors.Arrow
                        RefreshCategory()
                        oDMSetup.Dispose()
                    Else
                        MessageBox.Show("No Record Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        End With
    End Sub
    Private Sub DeleteCategory()
        '   Dim oDMSetup As New frmDM_Setup
        Dim oclsDM As New gloStream.DiseaseManagement.DiseaseManagement
        'oDMSetup fill all data of selected criteria
        With c1DiseaseList
            If .Rows.Count > 1 Then
                Dim _ID As Long
                Dim _EditName As String
                If .Row > 0 Then
                    _ID = .GetData(.Row, COL_ID)
                    _EditName = .GetData(.Row, COL_NAME)

                    If _ID > 0 And _EditName.Trim <> "" Then
                        If oclsDM.IsDelete(_EditName.Replace("'", "''")) = True Then
                            If MessageBox.Show("Are you sure you want to delete the current record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                'If MessageBox.Show("Are You sure to delete current record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                oclsDM.Delete(_ID, _EditName.Replace("'", "''"))
                                RefreshCategory()
                                oMainForm.Fill_DMCriteriaMenu()
                            End If
                        Else
                            MessageBox.Show("Recommendation rule you are trying to delete has generated active recommendations for patient(s). You cannot delete the rule instead mark it in-active.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        MessageBox.Show("User Name Does Not Exist", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If
        End With
        oclsDM.Dispose()
        oclsDM = Nothing
    End Sub
    Private Sub RefreshCategory()

        txtSearch.Focus()
        Try
            RemoveHandler c1DiseaseList.RowColChange, AddressOf c1DiseaseList_RowColChange

            RefreshGrid()
            DesignGrid(c1DiseaseList)

            '''''Code line below is addded by Anil 0n 26/09/07 at 10:00 a.m.
            '''''This Code line clears the search text box on click of Refresh button.

            AddHandler c1DiseaseList.RowColChange, AddressOf Me.c1DiseaseList_RowColChange

            If txtSearch.Text.Trim().Length > 0 Then
                Dim sender As Object = Nothing
                Dim e As System.EventArgs = Nothing
                txtSearch_TextChanged(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            '' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.CDS, gloAuditTrail.ActivityType.RefereshRules, "'" & gstrLoginName & "' unsuccessfully Refresh records", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

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

                Case "Modify"
                    Me.Cursor = Cursors.WaitCursor
                    Call UpdateCategory()


                Case "Delete"
                    Call DeleteCategory()

                Case "Refresh"
                    Me.Cursor = Cursors.WaitCursor
                    Call RefreshCategory()

                    ''  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.CDS, gloAuditTrail.ActivityType.RefereshRules, "'" & gstrLoginName & "' successfully Refresh records", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
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

    Private Sub c1DiseaseList_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1DiseaseList.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20091005 
        'USE CLEAR BUTTON TO CLEAR SEARCH TEXT BOX
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("DmSetUp", "Download")
            'code added by seema on 20120221 to prevent opening of multiple windows
            If gloCommunity.Classes.clsGeneral.getInstance(FrmgloCommunityViewData.Name, FrmgloCommunityViewData.Text) = False Then
                Try

                    With FrmgloCommunityViewData
                        .MdiParent = Application.OpenForms("MainMenu")
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With

                Catch objErr As Exception
                    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
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

    Private Sub tsbtn_NewDMSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtn_NewDMSetup.Click
        Me.Cursor = Cursors.WaitCursor
        Dim oDMSetup As New frmDM_RulesSetup()
        oDMSetup.WindowState = FormWindowState.Normal
        oDMSetup.StartPosition = FormStartPosition.CenterScreen
        oDMSetup.ShowInTaskbar = False
        oDMSetup.ShowDialog(IIf(IsNothing(oDMSetup.Parent), Me, oDMSetup.Parent))
        Me.Cursor = Cursors.Arrow
        oDMSetup.Dispose()
        RefreshCategory()

    End Sub

    Private Sub tsbtn_Act_Deact_Rule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtn_Act_Deact_Rule.Click
        PerformActivateDeActivate()
    End Sub

    Private Sub tsbtn_ViewRuleHistory_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_ViewRuleHistory.Click

        Dim _selectedRuleId As Int64 = 0
        Dim _SelectedRulename As String = ""
        Try

            If Not IsNothing(c1DiseaseList) AndAlso c1DiseaseList.Rows.Count > 0 AndAlso c1DiseaseList.RowSel > 0 Then

                If Not IsNothing(c1DiseaseList.GetData(c1DiseaseList.RowSel, COL_ID)) AndAlso Convert.ToString(c1DiseaseList.GetData(c1DiseaseList.RowSel, COL_ID)).Trim() <> "" Then

                    _selectedRuleId = Convert.ToInt64(c1DiseaseList.GetData(c1DiseaseList.RowSel, COL_ID))
                    _SelectedRulename = Convert.ToString(c1DiseaseList.GetData(c1DiseaseList.RowSel, COL_NAME))
                    If (_selectedRuleId > 0) Then

                        Dim oForm As New frmDM_RuleActivityHistory(_selectedRuleId, _SelectedRulename)
                        oForm.WindowState = FormWindowState.Normal
                        oForm.StartPosition = FormStartPosition.CenterScreen
                        oForm.ShowInTaskbar = False
                        oForm.ShowDialog(IIf(IsNothing(oForm.Parent), Me, oForm.Parent))
                        Me.Cursor = Cursors.Arrow
                        oForm.Dispose()
                        oForm = Nothing
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.View, "'" & _SelectedRulename & "' Successfully Viewed History", 0, _selectedRuleId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.DM_RuleSetup, gloAuditTrail.ActivityType.View, "'" & _SelectedRulename & "' UnSuccessfully Viewed History", 0, _selectedRuleId, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally

        End Try

    End Sub

    Private Sub PerformActivateDeActivate()

        Dim _SelectedRuleId As Int64 = 0
        Dim _nSelectedRowIndex As Integer = -1
        Dim oDiseaseManagement As gloStream.DiseaseManagement.DiseaseManagement = Nothing
        Dim _sActivationDeActivationNote As String = ""

        Try
            If Not IsNothing(c1DiseaseList) AndAlso c1DiseaseList.Rows.Count > 0 AndAlso c1DiseaseList.RowSel > 0 Then

                With c1DiseaseList

                    _SelectedRuleId = .GetData(.RowSel, COL_ID)

                    If _SelectedRuleId > 0 Then

                        If tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE" Then
                            If Not MessageBox.Show("Are you sure you want to de-activate the current record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                Exit Sub
                            End If
                        End If



                        If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then
                            If Not MessageBox.Show("Are you sure you want to activate the current record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                                Exit Sub
                            End If
                        End If



                        'Calling notes form for input
                        Dim ofrmNotes As frmDM_RecommendationNotes = New frmDM_RecommendationNotes(_SelectedRuleId, True)
                        ofrmNotes.StartPosition = FormStartPosition.CenterParent
                        ofrmNotes.ShowDialog(IIf(IsNothing(ofrmNotes.Parent), Me, ofrmNotes.Parent))
                        If ofrmNotes.FormDialogResult = Windows.Forms.DialogResult.OK Then
                            _sActivationDeActivationNote = ofrmNotes.Note

                            'Code to add history and note goes here
                            '----


                        End If
                        ofrmNotes.Dispose()
                        oDiseaseManagement = New gloStream.DiseaseManagement.DiseaseManagement()

                        If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then
                            oDiseaseManagement.UpdateCriteriaStatus(_SelectedRuleId, True, _sActivationDeActivationNote, .GetData(.RowSel, COL_NAME).ToString())
                        Else
                            oDiseaseManagement.UpdateCriteriaStatus(_SelectedRuleId, False, _sActivationDeActivationNote, .GetData(.RowSel, COL_NAME).ToString())
                        End If
                        RefreshCategory()
                        ''_nSelectedRowIndex = .RowSel
                        _nSelectedRowIndex = .FindRow(_SelectedRuleId.ToString(), 0, 0, False, False, False)

                        If (_nSelectedRowIndex <> -1 And _nSelectedRowIndex > 0) Then
                            .RowSel = _nSelectedRowIndex
                            .Select(_nSelectedRowIndex, 0)

                        End If
                    End If

                End With

                'If tsbtn_Act_Deact_Rule.Tag = "ACTIVATE" Then

                '    tsbtn_Act_Deact_Rule.Text = "De-&activate"
                '    tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                '    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate

                'ElseIf tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE" Then

                '    tsbtn_Act_Deact_Rule.Text = "&Activate"
                '    tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                '    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate

                'End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDiseaseManagement = Nothing
        End Try
    End Sub

    Private Sub tsbtn_CopyRule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbtn_CopyRule.Click

        With c1DiseaseList
            If .Rows.Count > 1 Then
                Dim _ID As Long
                'Dim _EditName As String

                _ID = .GetData(.Row, COL_ID)

                If _ID > 0 Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim oDMSetup As New frmDM_RulesSetup(True, _ID)
                    oDMSetup.WindowState = FormWindowState.Normal
                    oDMSetup.StartPosition = FormStartPosition.CenterScreen
                    oDMSetup.ShowInTaskbar = False

                    oDMSetup.ShowDialog(IIf(IsNothing(oDMSetup.Parent), Me, oDMSetup.Parent))
                    'Shubhangi 20091007
                    Me.Cursor = Cursors.Arrow
                    RefreshCategory()
                    oDMSetup.Dispose()


                    '' gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DM_RuleSetup, gloAuditTrail.ActivityCategory.CDS, gloAuditTrail.ActivityType.CopyRule, "'" & gstrLoginName & "' successfully Copy record", 0, _ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                Else
                    MessageBox.Show("No Record Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End With
    End Sub

    Private Sub c1DiseaseList_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1DiseaseList.RowColChange
        Dim _sStatus As String
        If c1DiseaseList.RowSel > 0 Then

            '19-Jan-16 Aniket: Resolving Bug #91222: gloEMR: DM Setup: Application gives exception on search
            If IsNothing(c1DiseaseList.GetData(c1DiseaseList.RowSel, COL_ACTIVATED_STATUS)) = False Then
                _sStatus = c1DiseaseList.GetData(c1DiseaseList.RowSel, COL_ACTIVATED_STATUS).ToString()
                If _sStatus.ToUpper() = "ACTIVE" Then
                    tsbtn_Act_Deact_Rule.Text = "De-&activate"
                    tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate
                Else
                    tsbtn_Act_Deact_Rule.Text = "&Activate"
                    tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                    tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate
                End If
            End If
        End If
    End Sub

    Private Sub Rule_Status()
        Dim _sStatus As String
        If c1DiseaseList.RowSel > 0 Then
            _sStatus = c1DiseaseList.GetData(c1DiseaseList.RowSel, COL_ACTIVATED_STATUS).ToString()
            If _sStatus.ToUpper() = "ACTIVE" Then
                tsbtn_Act_Deact_Rule.Text = "De-&activate"
                tsbtn_Act_Deact_Rule.Tag = "DEACTIVATE"
                tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Deactivate
            Else
                tsbtn_Act_Deact_Rule.Text = "&Activate"
                tsbtn_Act_Deact_Rule.Tag = "ACTIVATE"
                tsbtn_Act_Deact_Rule.Image = Global.gloEMR.My.Resources.Activate
            End If
        End If
    End Sub

    Private Sub c1DiseaseList_AfterSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1DiseaseList.AfterSort
        If (c1DiseaseList.Rows.Count > 1) Then
            'c1DiseaseList.Select(1, 0)
            'Code changes for maintaining the selected row after sorting 
            Dim _index As Integer = c1DiseaseList.FindRow(_id, 0, COL_ID, False, False, False)
            c1DiseaseList.ShowCell(_index, COL_NAME)
            c1DiseaseList.Row = _index
            c1DiseaseList.Select()
            Rule_Status()
        End If
    End Sub
    'Code changes for maintaining the selected row after sorting 
    Dim _id As Int64
    Private Sub c1DiseaseList_BeforeSort(sender As System.Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1DiseaseList.BeforeSort
        If (c1DiseaseList.Rows.Count > 1) Then
            _id = c1DiseaseList.Rows(c1DiseaseList.RowSel)(COL_ID)
        End If
    End Sub
End Class
