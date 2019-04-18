Imports C1.Win.C1FlexGrid

Public Class frmVWFormGallery
    Inherits System.Windows.Forms.Form
    Implements IPatientContext

    Public Shared blnModify As Boolean
    '' To keep track of Add / Modify
    Dim objclsCPTAssociation As New clsCPTAssociation
    Dim _PatientID As Long
    'Dim m_UserID As Long
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim _blnSearch As Boolean = True
    Dim colPatientID As Integer = 0
    Dim colVisitID As Integer = 1
    Dim colVisitDate As Integer = 2
    Dim colCPTID As Integer = 3
    Dim colCPTName As Integer = 4
    Dim colTempalteID As Integer = 5
    Dim colTemplateName As Integer = 6
    Dim colFormID As Integer = 7
    Dim Col_Count As Integer = 8
    Dim _Dv As DataView = Nothing
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
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
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents c1TemplateGallery As C1.Win.C1FlexGrid.C1FlexGrid
    Dim _blnAdd As Boolean

#Region " Windows Form DeMessagesner generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        _PatientID = PatientID
        'This call is required by the Windows Form DeMessagesner.
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

    'Required by the Windows Form DeMessagesner
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form DeMessagesner
    'It can be modified using the Windows Form DeMessagesner.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWFormGallery))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
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
        Me.c1TemplateGallery = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTopRight.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.c1TemplateGallery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.pnlSearch)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(1178, 24)
        Me.pnlTopRight.TabIndex = 0
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.White
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.Label77)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(63, 1)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(241, 22)
        Me.pnlSearch.TabIndex = 54
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 3
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(5, 0)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(214, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
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
        Me.btnClear.TabIndex = 52
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(1, 0)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(4, 22)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(240, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(62, 22)
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
        Me.Label1.Size = New System.Drawing.Size(1176, 1)
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
        Me.Label3.Location = New System.Drawing.Point(1177, 1)
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
        Me.Label4.Size = New System.Drawing.Size(1178, 1)
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
        Me.pnlToolStrip.Size = New System.Drawing.Size(1184, 54)
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
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1184, 54)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 51)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 51)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 51)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 51)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 51)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.c1TemplateGallery)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(1184, 678)
        Me.Panel1.TabIndex = 12
        '
        'c1TemplateGallery
        '
        Me.c1TemplateGallery.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1TemplateGallery.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1TemplateGallery.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1TemplateGallery.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1TemplateGallery.ColumnInfo = resources.GetString("c1TemplateGallery.ColumnInfo")
        Me.c1TemplateGallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1TemplateGallery.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1TemplateGallery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1TemplateGallery.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1TemplateGallery.Location = New System.Drawing.Point(4, 1)
        Me.c1TemplateGallery.Name = "c1TemplateGallery"
        Me.c1TemplateGallery.Rows.Count = 1
        Me.c1TemplateGallery.Rows.DefaultSize = 19
        Me.c1TemplateGallery.Rows.Fixed = 0
        Me.c1TemplateGallery.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1TemplateGallery.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1TemplateGallery.ShowCellLabels = True
        Me.c1TemplateGallery.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1TemplateGallery.Size = New System.Drawing.Size(1176, 673)
        Me.c1TemplateGallery.StyleInfo = resources.GetString("c1TemplateGallery.StyleInfo")
        Me.c1TemplateGallery.TabIndex = 11
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 674)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(1176, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 674)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(1180, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 674)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(1178, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1184, 30)
        Me.Panel2.TabIndex = 13
        '
        'frmVWFormGallery
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1184, 762)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWFormGallery"
        Me.Text = "Form Gallery"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.c1TemplateGallery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmVWFormGallery_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        blnModify = False
    End Sub



    Private Sub frmVWFormGallery_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'line commented as _PatientID is set at constructor.
        '_PatientID = gnPatientID
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            '''''***********
            '''''    On FormLoad we have to show Forms Filled by that Patients   
            '''''    Here we are showing it in grdTemplateGallery
            '''''    col(0)=PatientID,
            '''''    col(1)=VisitID,
            '''''    col(2)=VisitDate, 
            '''''    col(3)=CPTID, 
            '''''    col(4)=CPTName, 
            '''''    col(5)=TempalteID, 
            '''''    col(6)=TemplateName
            '''''***********


            'Dim i As Integer

            ''If IsNothing(dt) = False Then
            ''    For i = 0 To dt.Rows.Count - 1
            ''        r = tblTemplate.NewRow()

            ''        'Specify the  col name to add value for the row
            ''        ''''' Refer Sub GridFormat()
            ''        ' GridFormat()
            ''        r("CPTID") = dt.Rows(i)(0)     '--0
            ''        r("CPT") = dt.Rows(i)(1)   '--1
            ''        r("TemplateID") = dt.Rows(i)(2)     '--2
            ''        r("Template") = dt.Rows(i)(3)  '--3
            ''        '''''r("Template") = CType(dt.Rows(0)(3), Image)

            ''        ''''  if there no rows in tblTemplate table then add Template to Template 

            ''        tblTemplate.Rows.Add(r)
            ''    Next
            ''End If
            'grdTemplateGallery.Enabled = False
            'grdTemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(_PatientID)
            'grdTemplateGallery.Enabled = True
            'CustomGridStyle()
            'c1TemplateGallery.Enabled = False
            'c1TemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(_PatientID)
            'c1TemplateGallery.Enabled = True
            '  SetGridStyle()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Private Sub UpdateFormGallery_old()
    '    '''''*************************
    '    '' Open frmCPTTemplate for Editing existing Form for Selected Patient 
    '    '' frmCPTTemplate will be Child Form of MainMenu
    '    '''''*************************

    '    Dim VisitID As Long
    '    Dim VisitDate As Date
    '    Dim CPTID As Long
    '    Dim TemplateID As Long
    '    Dim formID As Long
    '    Dim objfrm As frmCPTTemplate
    '    Try
    '        _blnAdd = False
    '        If grdTemplateGallery.VisibleRowCount >= 1 Then

    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ''''' 20071011 -Mahesh 
    '            'If CheckPatientStatus(_PatientID) = False Then
    '            '    Exit Sub
    '            'End If
    '            If MainMenu.IsAccess(False, _PatientID) = False Then
    '                Exit Sub
    '            End If

    '            '''''<><><><><> Check Patient Status <><><><><><>''''


    '            '******Shweta 20090828 *********'
    '            'To check exeception related to word
    '            If CheckWordForException() = False Then
    '                Exit Sub
    '            End If
    '            'End Shweta


    '            blnModify = True   '''' Open frmCPTTemplate in Edit Mode

    '            VisitID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 1).ToString
    '            VisitDate = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2).ToString
    '            CPTID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 3).ToString
    '            TemplateID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 5).ToString
    '            formID = Convert.ToInt64(Convert.ToString(grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 7)))
    '            objfrm = New frmCPTTemplate(VisitID, CPTID, TemplateID, VisitDate, _PatientID, formID)

    '            With objfrm
    '                '.Panel1.Visible = False
    '                .Text = "Modify Form Gallery"
    '                .MdiParent = Me.ParentForm
    '                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '                .BringToFront()
    '                .WindowState = FormWindowState.Maximized
    '                .Show()
    '            End With
    '            'SHUBHANGI 20110331
    '            Dim i As Integer
    '            For i = 0 To CType(grdTemplateGallery.DataSource, DataView).Table.Rows.Count - 1
    '                '''' when ID Found select that matching Row
    '                If TemplateID = grdTemplateGallery.Item(i, 5) Then
    '                    grdTemplateGallery.CurrentRowIndex = i
    '                    grdTemplateGallery.Select(i)
    '                    Exit For
    '                End If
    '            Next


    '            'objfrmMsg.ShowDialog(Me)
    '            'objfrmMsg.BringToFront()
    '            'If objfrm.CancelClick = False Then
    '            '    grdTemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(gnPatientID)  ' to View all Form Gallery
    '            'End If
    '        End If
    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, "Form Gallery viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

    '    Finally
    '        objfrm = Nothing
    '    End Try
    'End Sub

    Private Sub UpdateFormGallery()
        '''''*************************
        '' Open frmCPTTemplate for Editing existing Form for Selected Patient 
        '' frmCPTTemplate will be Child Form of MainMenu
        '''''*************************

        Dim VisitID As Long
        Dim VisitDate As Date
        Dim CPTID As Long
        Dim TemplateID As Long
        Dim formID As Long
        Dim objfrm As frmCPTTemplate
        Try
            _blnAdd = False
            If c1TemplateGallery.Rows.Count > 1 Then

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20071011 -Mahesh 
                'If CheckPatientStatus(_PatientID) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                End If

                '''''<><><><><> Check Patient Status <><><><><><>''''


                '******Shweta 20090828 *********'
                'To check exeception related to word
                If CheckWordForException() = False Then
                    Exit Sub
                End If
                'End Shweta
                If (c1TemplateGallery.RowSel > 0) Then
                    Dim CurrRowIndex As Integer = c1TemplateGallery.RowSel
                    blnModify = True   '''' Open frmCPTTemplate in Edit Mode

                    VisitID = c1TemplateGallery.Item(CurrRowIndex, 1).ToString
                    VisitDate = c1TemplateGallery.Item(CurrRowIndex, 2).ToString
                    CPTID = c1TemplateGallery.Item(CurrRowIndex, 3).ToString
                    TemplateID = c1TemplateGallery.Item(CurrRowIndex, 5).ToString
                    formID = Convert.ToInt64(Convert.ToString(c1TemplateGallery.Item(CurrRowIndex, 7)))
                    objfrm = New frmCPTTemplate(VisitID, CPTID, TemplateID, VisitDate, _PatientID, formID)

                    With objfrm
                        '.Panel1.Visible = False
                        .Text = "Modify Form Gallery"
                        .MdiParent = Me.ParentForm
                        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                        .BringToFront()
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With
                    'SHUBHANGI 20110331
                    Dim i As Integer
                    For i = 1 To c1TemplateGallery.Rows.Count - 1
                        '''' when ID Found select that matching Row
                        If TemplateID = c1TemplateGallery.Item(i, 5) Then
                            c1TemplateGallery.RowSel = i
                            c1TemplateGallery.Select(i, 0)
                            Exit For
                        End If
                    Next
                End If

                'objfrmMsg.ShowDialog(Me)
                'objfrmMsg.BringToFront()
                'If objfrm.CancelClick = False Then
                '    grdTemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(gnPatientID)  ' to View all Form Gallery
                'End If
            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.TemplateAllocation, gloAuditTrail.ActivityType.View, "Form Gallery viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

        Finally
            objfrm = Nothing
        End Try
    End Sub


    Private Sub frmVWFormGallery_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Try
            Me.WindowState = FormWindowState.Maximized
            If frmCPTTemplate.CancelClick = False Then
                ' grdTemplateGallery.Enabled = False
                'grdTemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(_PatientID)  ' to View all Form Gallery
                'grdTemplateGallery.Enabled = True
                If Not IsNothing(_Dv) Then
                    _Dv.Dispose()
                    _Dv = Nothing
                End If
                _Dv = objclsCPTAssociation.ViewFormGallery(_PatientID)  ' to View all Form Gallery

                c1TemplateGallery.Enabled = False
                '_Dv = CType(grdTemplateGallery.DataSource, DataView)
                If (_Dv.Table.Columns.Contains("sResult")) Then
                    _Dv.Table.Columns.Remove("sResult")
                End If
                c1TemplateGallery.DataSource = _Dv     ''objclsCPTAssociation.ViewFormGallery(_PatientID)  ' to View all Form Gallery
                c1TemplateGallery.Enabled = True
                '''''Flag is checked to find whether we are adding or modifying the record, so according to that the grid will be filled.
                If _blnAdd = False Then
                    ''''''''''''''''Code added by Anil on 20071105
                    Dim myDataView As DataView = CType(c1TemplateGallery.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = myDataView.Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        strcolumnName = arrcolumnsort.GetValue(0)
                        If arrcolumnsort.Length > 1 Then
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        '' CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        SetGridStyle(strcolumnName, strsortorder, strSearchstring)
                    End If

                Else
                    '' CustomGridStyle()
                    SetGridStyle()
                End If
            End If
            'If CType(Me.MdiParent, MainMenu).pnlLeft.Visible = False Then
            '    CType(Me.MdiParent, MainMenu).Splitter1.Visible = False
            'End If

            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            ''Above 2 lines commented by sudhir 20090202

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
    '    'Dim ts As New DataGridTableStyle
    '    'ts.ReadOnly = True
    '    'ts.AlternatingBackColor = System.Drawing.Color.Gainsboro
    '    'ts.BackColor = System.Drawing.Color.WhiteSmoke
    '    'ts.MappingName = tblTemplate.TableName.ToString
    '    'ts.HeaderBackColor = System.Drawing.Color.DimGray
    '    'ts.HeaderFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    'ts.ReadOnly = False
    '    'ts.RowHeadersVisible = False
    '    Dim dt As DataTable
    '    Dim dv As DataView
    '    dv = objclsCPTAssociation.DsDataview
    '    dt = objclsCPTAssociation.GetDataTable
    '    Dim ts As New clsDataGridTableStyle(dt.TableName)

    '    Dim colPatientID As New DataGridTextBoxColumn
    '    With colPatientID
    '        .Width = 0
    '        .MappingName = dt.Columns(0).ColumnName
    '        .HeaderText = "Patient ID"
    '        .NullText = ""
    '    End With

    '    Dim colVisitID As New DataGridTextBoxColumn
    '    With colVisitID
    '        .Width = 0
    '        .MappingName = dt.Columns(1).ColumnName
    '        .HeaderText = "VisitID"
    '        .NullText = ""
    '    End With

    '    Dim colVisitDate As New DataGridTextBoxColumn
    '    With colVisitDate
    '        .Width = grdTemplateGallery.Width / 5
    '        .MappingName = dt.Columns(2).ColumnName
    '        .HeaderText = "Visit Date"
    '        .NullText = ""
    '    End With

    '    Dim colCPTID As New DataGridTextBoxColumn
    '    With colCPTID
    '        .Width = 0
    '        .MappingName = dt.Columns(3).ColumnName
    '        .HeaderText = "CPTID"
    '        .NullText = ""
    '    End With

    '    Dim colCPTName As New DataGridTextBoxColumn
    '    With colCPTName
    '        .Width = grdTemplateGallery.Width * 2 / 5
    '        .MappingName = dt.Columns(4).ColumnName
    '        .HeaderText = "CPT"
    '        .NullText = ""
    '    End With

    '    Dim colTempalteID As New DataGridTextBoxColumn
    '    With colTempalteID
    '        .Width = 0
    '        .MappingName = dt.Columns(5).ColumnName
    '        .HeaderText = "TempalteID"
    '        .NullText = ""
    '    End With

    '    Dim colTemplateName As New DataGridTextBoxColumn
    '    With colTemplateName
    '        .Width = grdTemplateGallery.Width * 2 / 5
    '        .MappingName = dt.Columns(6).ColumnName
    '        .HeaderText = "Template"
    '        .NullText = ""
    '    End With

    '    Dim colFormID As New DataGridTextBoxColumn
    '    With colFormID
    '        .Width = 0
    '        .MappingName = dt.Columns(8).ColumnName
    '        .HeaderText = "FormID"
    '        .NullText = ""
    '    End With

    '    ''Dim colFormat As New DataGridTextBoxColumn
    '    ''With colFormat
    '    ''    .Width = 125
    '    ''    .MappingName = tblTemplate.Columns(2).ColumnName
    '    ''    .HeaderText = "Template"
    '    ''    .NullText = ""
    '    ''End With


    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {colPatientID, colVisitID, colVisitDate, colCPTID, colCPTName, colTempalteID, colTemplateName, colFormID})
    '    grdTemplateGallery.TableStyles.Clear()
    '    grdTemplateGallery.TableStyles.Add(ts)
    '    '''''''Code is added by Anil on 20071105
    '    txtSearch.Text = ""
    '    txtSearch.Text = strsearchtxt
    '    If strcolumnName = "" Or IsNothing(strcolumnName) Then
    '        dv.Sort = "[" & objclsCPTAssociation.DsDataview.Table.Columns(6).ColumnName & "]" & strsortorder
    '    Else
    '        Dim strColumn As String = Replace(strcolumnName, "[", "")
    '        dv.Sort = "[" & strColumn & "]" & strSortBy
    '    End If
    '    ''''''''''''''''''''''''''''''''
    '    If (dt.Rows.Count >= 1) Then
    '        '' grdTemplateGallery.Select(0)
    '    End If

    ' End Sub

    Private Sub SetGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        ''added to solve  sorting issue Bugid 72077
        Try
            Dim dt As DataTable
            Dim dv As DataView
            dv = objclsCPTAssociation.DsDataview
            dt = objclsCPTAssociation.GetDataTable

            With c1TemplateGallery
                .AllowSorting = True

                ' Dim i As Int16
                ' .Redraw = False
                ' .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = Me.Width
                ' c1TemplateGallery.Width = _TotalWidth
                ' c1TemplateGallery.Height = Me.Height - 20
                c1TemplateGallery.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing

                '.Cols.Count = Col_Count
                .Cols.Count = Col_Count
                .Rows.Fixed = 1


                .Styles.ClearUnused()

                .AllowResizing = True

                'Dim dt As DataTable
                'If IsNothing(_dt) Then
                '    dt = objMessages.GetDataTable
                'Else
                '    dt = _dt
                'End If

                'Dim dv As DataView
                'dv = dt.DefaultView






                .Cols(colPatientID).Width = _TotalWidth * 0
                .Cols(colPatientID).AllowEditing = False
                .Cols(colPatientID).Visible = False
                .Cols(colPatientID).Caption = "Patient ID"
                .Cols(colPatientID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(colVisitID).Width = _TotalWidth * 0
                .Cols(colVisitID).AllowEditing = False
                .Cols(colVisitID).Visible = False
                .Cols(colVisitID).Caption = "VisitID"
                .Cols(colVisitID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(colVisitDate).Width = _TotalWidth * 0.33
                .Cols(colVisitDate).AllowEditing = False
                .Cols(colVisitDate).Visible = True
                .Cols(colVisitDate).DataType = GetType(System.DateTime)
                .Cols(colVisitDate).Format = "MM/dd/yyyy h:mm tt"
                .Cols(colVisitDate).Caption = "Visit Date"
                .Cols(colVisitDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(colCPTID).Width = _TotalWidth * 0
                .Cols(colCPTID).AllowEditing = False
                .Cols(colCPTID).Visible = False
                .Cols(colCPTID).Caption = "CPTID"
                .Cols(colCPTID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(colCPTName).Width = _TotalWidth * 0.33
                .Cols(colCPTName).AllowEditing = False
                .Cols(colCPTName).Visible = True
                .Cols(colCPTName).Caption = "CPT"
                .Cols(colCPTName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(colTempalteID).Width = _TotalWidth * 0
                .Cols(colTempalteID).AllowEditing = False
                .Cols(colTempalteID).Visible = False
                .Cols(colTempalteID).Caption = "TempalteID"
                .Cols(colTempalteID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(colTemplateName).Width = _TotalWidth * 0.33
                .Cols(colTemplateName).AllowEditing = False
                .Cols(colTemplateName).Visible = True
                .Cols(colTemplateName).Caption = "Template"
                .Cols(colTemplateName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(colFormID).Width = 0
                .Cols(colFormID).AllowEditing = False
                .Cols(colFormID).Visible = False
                .Cols(colFormID).Caption = "FormID"
                .Cols(colFormID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                '.Cols(Col_MessageDate).Width = _TotalWidth * 0.118
                '.Cols(Col_MessageDate).AllowEditing = False
                '.Cols(Col_MessageDate).Visible = True
                ''.SetData(0, Col_MessageDate, "Date")
                '.Cols(Col_MessageDate).Caption = "Date"
                '.Cols(Col_MessageDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                '.Cols(Col_MessageDate).DataType = GetType(System.DateTime)
                '.Cols(Col_MessageDate).Format = "MM/dd/yyyy h:mm tt"




                .Redraw = True


            End With
            txtSearch.Text = ""
            txtSearch.Text = strsearchtxt
            If IsNothing(strcolumnName) OrElse strcolumnName = "" Then
                dv.Sort = "[" & objclsCPTAssociation.DsDataview.Table.Columns(6).ColumnName & "]" & strsortorder
            Else
                Dim strColumn As String = Replace(strcolumnName, "[", "")
                dv.Sort = "[" & strColumn & "]" & strSortBy
            End If
            ''''''''''''''''''''''''''''''''
            If (dt.Rows.Count >= 1) Then
                c1TemplateGallery.RowSel = 1
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally

        End Try

    End Sub







    'Private Sub grdTemplateGallery_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    ''Try
    '    ''    Select Case grdTemplateGallery.CurrentCell.ColumnNumber
    '    ''        Case 6
    '    ''            lblSearch.Text = "Template Name"
    '    ''            'Case 2
    '    ''            '    lblSearch.Text = "Visit Date"
    '    ''        Case 4
    '    ''            lblSearch.Text = "CPT"
    '    ''    End Select
    '    ''Catch objErr As Exception
    '    ''    MessageBox.Show(objErr.ToString, "Form Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    ''End Try
    'End Sub

    'Private Sub grdTemplateGallery_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Dim ptPoint As Point = New Point(e.X, e.Y)
    '    Dim htInfo As DataGrid.HitTestInfo = grdTemplateGallery.HitTest(ptPoint)
    '    If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

    '        'Commented by Shubhangi 20091007
    '        'Select Case htInfo.Column
    '        '    Case 4
    '        '        lblSearch.Text = "CPT"
    '        '    Case 6
    '        '        lblSearch.Text = "Template Name"
    '        'End Select

    '        If txtSearch.Text = "" Then
    '            _blnSearch = True
    '        Else
    '            _blnSearch = False
    '            txtSearch.Text = ""
    '            _blnSearch = True
    '        End If
    '    ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
    '        UpdateFormGallery()
    '    Else
    '        Exit Sub
    '    End If
    'End Sub

    'Private Sub grdFormGallery_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    'If grdTemplateGallery.CurrentRowIndex >= 0 Then
    '    '    grdTemplateGallery.Select(grdTemplateGallery.CurrentRowIndex)
    '    'End If
    '    Try
    '        Dim ptPoint As Point = New Point(e.X, e.Y)
    '        Dim htInfo As DataGrid.HitTestInfo = grdTemplateGallery.HitTest(ptPoint)

    '        If htInfo.Type = DataGrid.HitTestType.Cell Then
    '            grdTemplateGallery.Select(htInfo.Row)
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub frmVWFormGallery_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            ''Commented by Dhruv''
            ''To Not to show the dashbord directly
            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                'If grdTemplateGallery.CurrentRowIndex >= 0 Then
                '    grdTemplateGallery.Select(0)
                '    grdTemplateGallery.CurrentRowIndex = 0
                'End If
                If c1TemplateGallery.RowSel >= 0 Then
                    c1TemplateGallery.RowSel = -1
                    ''grdTemplateGallery.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvTemplate As DataView
            '  dvTemplate = CType(grdTemplateGallery.DataSource, DataView)
            dvTemplate = CType(c1TemplateGallery.DataSource, DataView)
            If IsNothing(dvTemplate) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            'grdTemplateGallery.Enabled = False
            'grdTemplateGallery.DataSource = dvTemplate
            'grdTemplateGallery.Enabled = True
            c1TemplateGallery.Enabled = False
            c1TemplateGallery.DataSource = dvTemplate
            c1TemplateGallery.Enabled = True
            Dim strPatientSearchDetails As String
            '  Dim strSearch As String = "Template"
            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If
            'Commnted by Shuhangi 20091007
            'Select Case Trim(lblSearch.Text)
            '    'Case "Visit Date"
            '    '    If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '    '        dvTemplate.RowFilter = dvTemplate.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '    '    Else
            '    '        dvTemplate.RowFilter = dvTemplate.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '    '    End If
            '    '''''''****Following code is commented by Anil on 10/10/2007, to implement the In-String Search.
            '    Case "Template Name"
            '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '        dvTemplate.RowFilter = dvTemplate.Table.Columns(6).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '        'Else
            '        'dvTemplate.RowFilter = dvTemplate.Table.Columns(6).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '        'End If
            '    Case "CPT"
            '        'If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '        dvTemplate.RowFilter = dvTemplate.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '        'Else
            '        'dvTemplate.RowFilter = dvTemplate.Table.Columns(4).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '        'End If

            'End Select

            'Shubhangi 20091007 Use General & Instring Search
            dvTemplate.RowFilter = dvTemplate.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                              & dvTemplate.Table.Columns(6).ColumnName & " Like '%" & strPatientSearchDetails & "%' "

            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007
    Private Sub AddCategory()
        '''''*************************
        '' Open frmCPTTemplate for Adding new Form for Selected Patient 
        '' frmCPTTemplate will be Child Form of MainMenu
        '''''*************************
        Dim objfrm As frmCPTTemplate
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


            ''''''Here the modifications are done by Mahesh on 20071004
            ''''''The modifications are done for adding new templates in the form gallery or modifying templates present in the form gallery.

            Dim VisitID As Int64
            Dim VisitDate As DateTime = Now
            Dim CPTID As Int64 = 0
            Dim TemplateID As Int64 = 0
            _blnAdd = True

            '' Assign todays VisitID Of The Patient 
            VisitID = GetVisitID(VisitDate, _PatientID)
            If VisitID = 0 Then
                blnModify = False ' '' Open frmCPTTemplate in Add New Mode
                objfrm = New frmCPTTemplate(_PatientID)
            Else
                blnModify = True   ' '' Open frmCPTTemplate in Edit Mode
                objfrm = New frmCPTTemplate(VisitID, CPTID, TemplateID, VisitDate, _PatientID)
            End If

            'AddHandler objfrm.EvntGenerateCDAFromCPTTemplate, AddressOf Raise_EvntGenerateCDAFromVWFormGallery
            With objfrm
                .Panel1.Visible = True
                .Text = "New Form Gallery"
                .MdiParent = Me.ParentForm
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                .WindowState = FormWindowState.Maximized
                .BringToFront()
                .Show()
            End With

            'If objfrm.CancelClick = False Then
            '    grdTemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(gnPatientID)  ' to View all Form Gallery
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

        Finally
            objfrm = Nothing
        End Try
    End Sub
    Private Sub UpdateCategory()
        'If grdTemplateGallery.VisibleRowCount >= 1 Then
        '    If grdTemplateGallery.IsSelected(grdTemplateGallery.CurrentRowIndex) Then
        '        UpdateFormGallery()
        '    End If

        'End If
        If c1TemplateGallery.Rows.Count > 1 Then
            If c1TemplateGallery.RowSel > 0 Then
                UpdateFormGallery()
            End If
        End If
    End Sub
    'Private Sub DeleteCategoryOld()
    '    Dim VisitID As Long
    '    Dim CPTID As Long
    '    Dim TemplateID As Long
    '    Dim VisitDate As String
    '    Dim TemplateName As String
    '    Dim FormID As Long = 0

    '    Try
    '        If grdTemplateGallery.VisibleRowCount >= 1 Then
    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ''''' 20070125 -Mahesh 
    '            'If CheckPatientStatus(_PatientID) = False Then
    '            '    Exit Sub
    '            'End If
    '            If MainMenu.IsAccess(False, _PatientID) = False Then
    '                Exit Sub
    '            End If
    '            '''''<><><><><> Check Patient Status <><><><><><>''''

    '            If grdTemplateGallery.IsSelected(grdTemplateGallery.CurrentRowIndex) = False Then
    '                Exit Sub
    '            End If

    '            'blnModify = True
    '            If MessageBox.Show("Do you want to delete selected Patient Form Gallery?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
    '                VisitID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 1).ToString
    '                VisitDate = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 2).ToString
    '                CPTID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 3).ToString
    '                TemplateID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 5).ToString
    '                TemplateName = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 6).ToString
    '                FormID = grdTemplateGallery.Item(grdTemplateGallery.CurrentRowIndex, 7).ToString '' Form ID

    '                objclsCPTAssociation.DeleteForm(VisitID, VisitDate, CPTID, TemplateID, TemplateName, _PatientID, FormID)
    '                grdTemplateGallery.Enabled = False
    '                grdTemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(_PatientID)
    '                grdTemplateGallery.Enabled = True
    '                'grdTemplateGallery.Select()

    '                ''''''Code is Added by Anil 0n 20071105
    '                sortOrder = CType(grdTemplateGallery.DataSource, DataView).Sort
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
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub DeleteCategory()
        Dim VisitID As Long
        Dim CPTID As Long
        Dim TemplateID As Long
        Dim VisitDate As String
        Dim TemplateName As String
        Dim FormID As Long = 0

        Try
            If c1TemplateGallery.Rows.Count > 1 Then
                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                'If CheckPatientStatus(_PatientID) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                End If
                '''''<><><><><> Check Patient Status <><><><><><>''''

                If c1TemplateGallery.RowSel <= 0 Then
                    Exit Sub
                End If

                'blnModify = True
                If MessageBox.Show("Do you want to delete selected Patient Form Gallery?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    VisitID = c1TemplateGallery.Item(c1TemplateGallery.RowSel, 1).ToString
                    VisitDate = c1TemplateGallery.Item(c1TemplateGallery.RowSel, 2).ToString
                    CPTID = c1TemplateGallery.Item(c1TemplateGallery.RowSel, 3).ToString
                    TemplateID = c1TemplateGallery.Item(c1TemplateGallery.RowSel, 5).ToString
                    TemplateName = c1TemplateGallery.Item(c1TemplateGallery.RowSel, 6).ToString
                    FormID = c1TemplateGallery.Item(c1TemplateGallery.RowSel, 7).ToString '' Form ID

                    objclsCPTAssociation.DeleteForm(VisitID, VisitDate, CPTID, TemplateID, TemplateName, _PatientID, FormID)
                    'grdTemplateGallery.Enabled = False
                    'grdTemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(_PatientID)
                    'grdTemplateGallery.Enabled = True
                    c1TemplateGallery.Enabled = False
                    If Not IsNothing(_Dv) Then
                        _Dv.Dispose()
                        _Dv = Nothing
                    End If
                    _Dv = objclsCPTAssociation.ViewFormGallery(_PatientID)


                    If (_Dv.Table.Columns.Contains("sResult")) Then
                        _Dv.Table.Columns.Remove("sResult")
                    End If
                    c1TemplateGallery.DataSource = _Dv
                    c1TemplateGallery.Enabled = True
                    'grdTemplateGallery.Select()
                    Dim myDataView As DataView = CType(c1TemplateGallery.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        ''''''Code is Added by Anil 0n 20071105
                        sortOrder = CType(c1TemplateGallery.DataSource, DataView).Sort
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    'Private Sub RefreshCategoryOld()
    '    Try
    '        grdTemplateGallery.Enabled = False
    '        grdTemplateGallery.DataSource = objclsCPTAssociation.ViewFormGallery(_PatientID)
    '        grdTemplateGallery.Enabled = True
    '        CustomGridStyle()
    '        ''''''Added by Anil on 20071105
    '        If grdTemplateGallery.VisibleRowCount > 0 Then
    '            grdTemplateGallery.CurrentRowIndex = 0
    '            grdTemplateGallery.Select(0)
    '        End If
    '        txtSearch.Text = ""
    '        _blnSearch = True
    '        ''''''''''''''''
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub RefreshCategory()
        Try
            c1TemplateGallery.Enabled = False
            If Not IsNothing(_Dv) Then
                _Dv.Dispose()
                _Dv = Nothing
            End If
            _Dv = objclsCPTAssociation.ViewFormGallery(_PatientID)
            If (_Dv.Table.Columns.Contains("sResult")) Then
                _Dv.Table.Columns.Remove("sResult")
            End If
            c1TemplateGallery.DataSource = _Dv
            c1TemplateGallery.Enabled = True
            ''CustomGridStyle()
            SetGridStyle()
            ''''''Added by Anil on 20071105
            If c1TemplateGallery.Rows.Count > 1 Then
                c1TemplateGallery.RowSel = 1
                c1TemplateGallery.Select(1, 0)
            End If
            txtSearch.Text = ""
            _blnSearch = True
            ''''''''''''''''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Try
            'DB Dim frm As MainMenu
            'DB frm = Me.MdiParent
            'DB frm.Fill_Messages()
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
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
        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Shubhangi 20091006
        'Use to clear Search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmVWFormGallery_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'CustomGridStyle()
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

    'Public Delegate Sub GenerateCDAFromVWFormGallery(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromVWFormGallery(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromVWFormGallery(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromVWFormGallery(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Dim rowIndex As Int64 = -1
    Private Sub c1TemplateGallery_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1TemplateGallery.AfterSort
        ''added to solve  sorting issue Bugid 72077
        If rowIndex > -1 Then
            Dim rw As C1.Win.C1FlexGrid.Row
            For Each rw In c1TemplateGallery.Rows
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1TemplateGallery.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(rw.DataSource, DataRowView)
                If Not dr Is Nothing Then
                    Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)
                    If currIndex = rowIndex Then
                        Dim cr As CellRange = c1TemplateGallery.GetCellRange(rw.Index, 1)
                        ' to scroll the selected row in the visible area
                        c1TemplateGallery.Select(cr, True)
                        cr = c1TemplateGallery.GetCellRange(rw.Index, 0, rw.Index, c1TemplateGallery.Cols.Count - 1)
                        c1TemplateGallery.Select(cr, False)
                        Exit For
                    End If
                End If
            Next
        End If



    End Sub
    Private Sub c1TemplateGallery_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1TemplateGallery.MouseClick
        ''added to solve  sorting issue Bugid 72077
        Try

            If (Not IsNothing(c1TemplateGallery.DataSource) AndAlso (c1TemplateGallery.Rows.Count > 0)) Then
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1TemplateGallery.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(cm.Current, DataRowView)
                rowIndex = dr.Row.Table.Rows.IndexOf(dr.Row)

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(" View FormGallery MouseClick " + ex.Message.ToString(), False)
        End Try
    End Sub

    Private Sub c1TemplateGallery_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1TemplateGallery.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1TemplateGallery.HitTest(ptPoint)
        If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.ColumnHeader Then

            'Commented by Shubhangi 20091007
            'Select Case htInfo.Column
            '    Case 4
            '        lblSearch.Text = "CPT"
            '    Case 6
            '        lblSearch.Text = "Template Name"
            'End Select

            If txtSearch.Text = "" Then
                _blnSearch = True
            Else
                _blnSearch = False
                txtSearch.Text = ""
                _blnSearch = True
            End If
        ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
            UpdateFormGallery()
        Else
            Exit Sub
        End If
    End Sub
End Class
