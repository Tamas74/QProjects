Imports gloEMR.gloEMRWord
Public Class frmVWPTProtocols

    Inherits System.Windows.Forms.Form
    Implements IPatientContext


    Dim _PatientID As Long
    Public Shared blnModify As Boolean
    Dim objclsPTProtocol As New clsPTProtocols
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim Col_Count As Integer = 5
    Dim Col_ProtocolID As Integer = 0
    Dim Col_ProtocolDate As Integer = 1
    Dim Col_TemplateID As Integer = 2
    Dim Col_ProtocolName As Integer = 3
    Dim Col_IsFinished As Integer = 4
    Dim ind As Integer = -1
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Dim _blnAdd As Boolean
    Dim dtWord As DataTable
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
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents c1PTProtocol As C1.Win.C1FlexGrid.C1FlexGrid
    Dim objWord As clsWordDocument

#Region " Windows Form Designer generated code "
    'constructor commnted by dipak 20100907 as not used anywhere 
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
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWPTProtocols))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
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
        Me.c1PTProtocol = New C1.Win.C1FlexGrid.C1FlexGrid()
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
        CType(Me.c1PTProtocol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.pnlSearch)
        Me.pnlTopRight.Controls.Add(Me.Label9)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(842, 23)
        Me.pnlTopRight.TabIndex = 1
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.Label77)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Controls.Add(Me.Label5)
        Me.pnlSearch.Controls.Add(Me.Label6)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(64, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(241, 23)
        Me.pnlSearch.TabIndex = 52
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(5, 1)
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
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(219, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 21)
        Me.btnClear.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(1, 1)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(4, 21)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(240, 1)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(241, 1)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(241, 1)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "label1"
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
        Me.Label9.TabIndex = 51
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(4, 2, 4, 0)
        Me.lblSearch.Size = New System.Drawing.Size(64, 16)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = " Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 22)
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
        Me.Label2.Size = New System.Drawing.Size(1, 22)
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
        Me.Label3.Size = New System.Drawing.Size(1, 22)
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
        Me.Panel1.Controls.Add(Me.c1PTProtocol)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 82)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(848, 436)
        Me.Panel1.TabIndex = 0
        '
        'c1PTProtocol
        '
        Me.c1PTProtocol.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1PTProtocol.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1PTProtocol.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1PTProtocol.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1PTProtocol.ColumnInfo = resources.GetString("c1PTProtocol.ColumnInfo")
        Me.c1PTProtocol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1PTProtocol.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1PTProtocol.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1PTProtocol.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1PTProtocol.Location = New System.Drawing.Point(4, 1)
        Me.c1PTProtocol.Name = "c1PTProtocol"
        Me.c1PTProtocol.Rows.Count = 1
        Me.c1PTProtocol.Rows.DefaultSize = 19
        Me.c1PTProtocol.Rows.Fixed = 0
        Me.c1PTProtocol.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1PTProtocol.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1PTProtocol.ShowCellLabels = True
        Me.c1PTProtocol.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1PTProtocol.Size = New System.Drawing.Size(840, 431)
        Me.c1PTProtocol.StyleInfo = resources.GetString("c1PTProtocol.StyleInfo")
        Me.c1PTProtocol.TabIndex = 15
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 432)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 432)
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
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 432)
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
        Me.Panel2.Size = New System.Drawing.Size(848, 29)
        Me.Panel2.TabIndex = 0
        '
        'frmVWPTProtocols
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(848, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWPTProtocols"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View PT Protocol"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.c1PTProtocol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub frmVWPTProtocol_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Line commented for replace gnPatientID by local variable.
        '_PatientID = gnPatientID
        Try
            c1PTProtocol.Enabled = False
            c1PTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
            c1PTProtocol.Enabled = True
            ''nLetterID, dtLetterDate, nTemplateID, sTemplateName

            SetGridStyle()
            'Sanjog - Added on 2011 May 17 for Patient Safety
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Sanjog - Added on 2011 May 17 for Patient Safety
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    'Private Sub UpdatePTProtocol_Old()

    '    Dim ProtocolID As Long
    '    Dim TemplateID As Long
    '    Dim objfrmPTProtocols As frmPTProtocols

    '    Try
    '        If grdPTProtocol.VisibleRowCount >= 1 Then
    '            blnModify = True
    '            _blnAdd = False
    '            ProtocolID = grdPTProtocol.Item(grdPTProtocol.CurrentRowIndex, 0).ToString
    '            TemplateID = grdPTProtocol.Item(grdPTProtocol.CurrentRowIndex, 2).ToString

    '            ' '' <><><> Record Level Locking <><><><> 
    '            ' '' Mahesh - 20070718 
    '            Dim blnRecordLock As Boolean = False
    '            If gblnRecordLocking = True Then
    '                Dim mydt As New mytable
    '                mydt = Scan_n_Lock_Transaction(TrnType.PTProtocol, ProtocolID, 0, Now)
    '                If mydt.Code <> gstrLoginName Or mydt.Description <> gstrClientMachineName Then
    '                    If MessageBox.Show("This PT Protocol is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '                        blnRecordLock = True
    '                    Else
    '                        'Return False
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


    '            Dim grdIndex As Integer = grdPTProtocol.CurrentRowIndex
    '            If grdPTProtocol.Item(grdPTProtocol.CurrentRowIndex, 4).ToString = "Yes" Then
    '                ''if Letter's Sataus is 'Finished' IsFinished=Yes
    '                objfrmPTProtocols = New frmPTProtocols(ProtocolID, TemplateID, True, blnRecordLock, _PatientID)
    '            Else

    '                If blnRecordLock Then
    '                    ''if Letter's Sataus is 'NOT Finished' IsFinished=No and Record is Lock then send it as finish
    '                    objfrmPTProtocols = New frmPTProtocols(ProtocolID, TemplateID, True, blnRecordLock, _PatientID)
    '                Else
    '                    ''if Letter's Sataus is 'NOT Finished' IsFinished=No and Record is not Lock
    '                    objfrmPTProtocols = New frmPTProtocols(ProtocolID, TemplateID, False, blnRecordLock, _PatientID)
    '                End If

    '            End If

    '            '''''''''''Code is Added by Anil on 20071103
    '            sortOrder = CType(grdPTProtocol.DataSource, DataView).Sort
    '            strSearchstring = txtSearch.Text.Trim
    '            arrcolumnsort = Split(sortOrder, "]")
    '            If arrcolumnsort.Length > 1 Then
    '                strcolumnName = arrcolumnsort.GetValue(0)
    '                strsortorder = arrcolumnsort.GetValue(1)
    '            End If
    '            ''''''''''''''''''''''
    '            'AddHandler objfrmPTProtocols.EvntGenerateCDAFromPTProtocols, AddressOf Raise_EvntGenerateCDAFromVWPTProtocols
    '            With objfrmPTProtocols
    '                ' .MdiParent = Me.ParentForm
    '                .MdiParent = Me.ParentForm
    '                .Ismodify = True
    '                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '                .MyCaller = Me
    '                .Show()
    '                .WindowState = FormWindowState.Maximized
    '                .BringToFront()
    '            End With
    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.View, "PT Protocol viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            If objfrmPTProtocols.CancelClick = False Then
    '                grdPTProtocol.Enabled = False
    '                grdPTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
    '                grdPTProtocol.Enabled = True
    '                'If Not IsNothing(objclsPTProtocol.DsDataview) Then
    '                '    objclsPTProtocol.SortDataview(objclsPTProtocol.GetDataview.Table.Columns(1).ColumnName)
    '                'End If
    '                '''' To Remember the Selection of Row 
    '                Dim i As Integer
    '                For i = 0 To CType(grdPTProtocol.DataSource, DataView).Table.Rows.Count - 1
    '                    '''' when ID Found select that matching Row
    '                    If ProtocolID = grdPTProtocol.Item(i, 0) Then
    '                        grdPTProtocol.CurrentRowIndex = i
    '                        grdPTProtocol.Select(i)
    '                        Exit For
    '                    End If
    '                Next
    '            Else
    '                grdPTProtocol.Select(grdIndex)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        objfrmPTProtocols = Nothing
    '    End Try
    'End Sub


    Private Sub UpdatePTProtocol()

        Dim ProtocolID As Long
        Dim TemplateID As Long
        Dim objfrmPTProtocols As frmPTProtocols

        Try
            If c1PTProtocol.Rows.Count > 1 And c1PTProtocol.RowSel > 0 Then
                blnModify = True
                _blnAdd = False
                ProtocolID = c1PTProtocol.Item(c1PTProtocol.RowSel, 0).ToString
                TemplateID = c1PTProtocol.Item(c1PTProtocol.RowSel, 2).ToString

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.PTProtocol, ProtocolID, 0, Now)
                    If (IsNothing(mydt) = False) Then
           
                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            If MessageBox.Show("This PT Protocol is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                blnRecordLock = True
                            Else
                                'Return False
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


            Dim grdIndex As Integer = c1PTProtocol.RowSel
            If c1PTProtocol.Item(c1PTProtocol.RowSel, 4).ToString = "Yes" Then
                ''if Letter's Sataus is 'Finished' IsFinished=Yes
                objfrmPTProtocols = New frmPTProtocols(ProtocolID, TemplateID, True, blnRecordLock, _PatientID)
            Else

                If blnRecordLock Then
                    ''if Letter's Sataus is 'NOT Finished' IsFinished=No and Record is Lock then send it as finish
                    objfrmPTProtocols = New frmPTProtocols(ProtocolID, TemplateID, False, blnRecordLock, _PatientID)


                Else
                    ''if Letter's Sataus is 'NOT Finished' IsFinished=No and Record is not Lock
                    objfrmPTProtocols = New frmPTProtocols(ProtocolID, TemplateID, False, blnRecordLock, _PatientID)
                End If

            End If
            Dim myDataView As DataView = CType(c1PTProtocol.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                '''''''''''Code is Added by Anil on 20071103
                sortOrder = CType(c1PTProtocol.DataSource, DataView).Sort
                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If
                ''''''''''''''''''''''
            End If
            'AddHandler objfrmPTProtocols.EvntGenerateCDAFromPTProtocols, AddressOf Raise_EvntGenerateCDAFromVWPTProtocols
            With objfrmPTProtocols
                ' .MdiParent = Me.ParentForm
                .MdiParent = Me.ParentForm
                .Ismodify = True
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                .MyCaller = Me
                .Show()
                .WindowState = FormWindowState.Maximized
                .BringToFront()
            End With
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.View, "PT Protocol viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            If objfrmPTProtocols.CancelClick = False Then
                c1PTProtocol.Enabled = False
                c1PTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
                SetGridStyle()
                c1PTProtocol.Enabled = True
                'If Not IsNothing(objclsPTProtocol.DsDataview) Then
                '    objclsPTProtocol.SortDataview(objclsPTProtocol.GetDataview.Table.Columns(1).ColumnName)
                'End If
                '''' To Remember the Selection of Row 
                Dim i As Integer
                For i = 1 To c1PTProtocol.Rows.Count - 1
                    '''' when ID Found select that matching Row
                    If ProtocolID = c1PTProtocol.Item(i, 0) Then
                        c1PTProtocol.RowSel = i
                        c1PTProtocol.Select(i, 0)
                        Exit For
                    End If
                Next
            Else
                c1PTProtocol.Select(grdIndex, 0)
            End If
            End If
        Catch ex As Exception
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmPTProtocols = Nothing
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Public Sub RefreshPTProtocols_Old(ByVal ProtocolID As Long)
    '    grdPTProtocol.Enabled = False
    '    grdPTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
    '    grdPTProtocol.Enabled = True

    '    '''''''''''Code is Added by Anil on 20071103
    '    '''''Flag is checked to find whether we are adding or modifying the record, so according to that the grid will be filled.
    '    If _blnAdd = False Then
    '        sortOrder = CType(grdPTProtocol.DataSource, DataView).Sort
    '        strSearchstring = txtSearch.Text.Trim
    '        arrcolumnsort = Split(sortOrder, "]")
    '        If arrcolumnsort.Length > 1 Then
    '            strcolumnName = arrcolumnsort.GetValue(0)
    '            strsortorder = arrcolumnsort.GetValue(1)
    '        End If
    '        '''''''''''''''''''''''
    '        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
    '    Else
    '        CustomGridStyle()
    '    End If

    '    If ProtocolID <> 0 Then
    '        grdPTProtocol.UnSelect(0)
    '    End If
    '    '''' To Remember the Selection of Row 
    '    Dim i As Integer
    '    For i = 0 To CType(grdPTProtocol.DataSource, DataView).Count - 1
    '        '''' when ID Found select that matching Row
    '        If ProtocolID = grdPTProtocol.Item(i, 0) Then
    '            grdPTProtocol.CurrentRowIndex = i
    '            grdPTProtocol.Select(i)
    '            Exit For
    '        End If
    '    Next
    'End Sub

    Public Sub RefreshPTProtocols(ByVal ProtocolID As Long)
        c1PTProtocol.Enabled = False
        c1PTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
        c1PTProtocol.Enabled = True

        '''''''''''Code is Added by Anil on 20071103
        '''''Flag is checked to find whether we are adding or modifying the record, so according to that the grid will be filled.
        If _blnAdd = False Then
            Dim myDataView As DataView = CType(c1PTProtocol.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                sortOrder = myDataView.Sort
                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If
                '''''''''''''''''''''''
                SetGridStyle(strcolumnName, strsortorder, strSearchstring)
            End If
        Else
            SetGridStyle()
        End If

        If ProtocolID <> 0 Then
            c1PTProtocol.RowSel = -1
        End If
        '''' To Remember the Selection of Row 
        Dim i As Integer
        For i = 1 To c1PTProtocol.Rows.Count - 1
            '''' when ID Found select that matching Row
            If ProtocolID = c1PTProtocol.Item(i, 0) Then
                c1PTProtocol.RowSel = i
                c1PTProtocol.Select(i, 0)
                Exit For
            End If
        Next
    End Sub
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
    '    Dim dt As DataTable
    '    Dim dv As DataView
    '    dt = objclsPTProtocol.GetDataTable
    '    dv = dt.DefaultView
    '    Dim ts As New clsDataGridTableStyle(dt.TableName)

    '    Dim LetterIDCol As New DataGridTextBoxColumn
    '    With LetterIDCol
    '        .Width = 0
    '        .MappingName = dv.Table.Columns(0).ColumnName
    '        .HeaderText = "ProtocolID"
    '    End With

    '    Dim DateCol As New DataGridTextBoxColumn
    '    With DateCol
    '        .Width = 0.3 * grdPTProtocol.Width
    '        .MappingName = dv.Table.Columns(1).ColumnName
    '        .HeaderText = "Protocol Date"
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
    '        .Width = 0.5 * grdPTProtocol.Width
    '        .MappingName = dv.Table.Columns(3).ColumnName
    '        .HeaderText = "Protocol Name"
    '        .NullText = ""
    '    End With

    '    Dim IsFinishedCol As New DataGridTextBoxColumn
    '    With IsFinishedCol
    '        .Width = 0.2 * grdPTProtocol.Width
    '        .MappingName = dv.Table.Columns(4).ColumnName
    '        .HeaderText = "Is Finished"
    '        .NullText = ""
    '    End With
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
    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {LetterIDCol, DateCol, TempIDCol, TempNameCol, IsFinishedCol})
    '    grdPTProtocol.TableStyles.Clear()
    '    grdPTProtocol.TableStyles.Add(ts)

    '    If (dt.Rows.Count >= 1) Then
    '        grdPTProtocol.Select(0)
    '    End If

    'End Sub
    Private Sub SetGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        ''added to solve  sorting issue Bugid 72083
        Try


            Dim dt As DataTable
            Dim dv As DataView
            dt = objclsPTProtocol.GetDataTable
            dv = dt.DefaultView
            c1PTProtocol.DataSource = dv
            With c1PTProtocol
                .AllowSorting = True


                .Redraw = False
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = Screen.PrimaryScreen.WorkingArea.Width - 40
                c1PTProtocol.Width = _TotalWidth
                ' c1Disclosure.Height = Me.Height - 20
                c1PTProtocol.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                .Cols.Count = Col_Count
                .Rows.Fixed = 1
                .Styles.ClearUnused()
                .AllowResizing = True

                .Cols(Col_ProtocolID).Width = _TotalWidth * 0
                .Cols(Col_ProtocolID).AllowEditing = False
                .Cols(Col_ProtocolID).Visible = False
                .Cols(Col_ProtocolID).Caption = "ProtocolID"
                .Cols(Col_ProtocolID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_ProtocolDate).Width = _TotalWidth * 0.33
                .Cols(Col_ProtocolDate).AllowEditing = False
                .Cols(Col_ProtocolDate).Visible = True
                .Cols(Col_ProtocolDate).Caption = "Protocol Date"
                .Cols(Col_ProtocolDate).DataType = GetType(System.DateTime)
                .Cols(Col_ProtocolDate).Format = "MM/dd/yyyy h:mm tt"

                .Cols(Col_ProtocolDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_TemplateID).Width = _TotalWidth * 0
                .Cols(Col_TemplateID).AllowEditing = False
                .Cols(Col_TemplateID).Visible = False
                .Cols(Col_TemplateID).Caption = "TemplateID"
                .Cols(Col_TemplateID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                .Cols(Col_ProtocolName).Width = _TotalWidth * 0.4
                .Cols(Col_ProtocolName).AllowEditing = False
                .Cols(Col_ProtocolName).Visible = True
                .Cols(Col_ProtocolName).Caption = "Protocol Name"
                .Cols(Col_ProtocolName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



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

    'Private Sub grdModifiers_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPTProtocol.CurrentCellChanged
    '    ''Try
    '    ''    Select Case grdPTProtocol.CurrentCell.ColumnNumber
    '    ''        Case 3
    '    ''            lblSearch.Text = "Protocol Name"
    '    ''    End Select
    '    ''Catch objErr As Exception
    '    ''    MessageBox.Show(objErr.ToString, "PTProtocol", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    ''End Try
    'End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView
            dvPatient = CType(c1PTProtocol.DataSource(), DataView)
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            c1PTProtocol.Enabled = False
            c1PTProtocol.DataSource = dvPatient
            c1PTProtocol.Enabled = True
            Dim strPatientSearchDetails As String

            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            ' Select Case Trim(lblSearch.Text)
            'Case "Date"
            '    If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
            '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            '    Else
            '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
            '    End If
            'Case "Protocol Name"
            If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            Else
                'dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                'Shubhangi 20091007
                'Use General search & in string Search
                dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                        & dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%' "
            End If
            ' End Select
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub grdPTProtocol_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPTProtocol.MouseUp
    '    'If grdPTProtocol.CurrentRowIndex >= 0 Then
    '    '    grdPTProtocol.Select(grdPTProtocol.CurrentRowIndex)
    '    'End If
    '    Try
    '        Dim ptPoint As Point = New Point(e.X, e.Y)
    '        Dim htInfo As DataGrid.HitTestInfo = grdPTProtocol.HitTest(ptPoint)
    '        If htInfo.Type = DataGrid.HitTestType.Cell Then
    '            grdPTProtocol.Select(htInfo.Row)
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
                If c1PTProtocol.Rows.Count > 1 Then
                    c1PTProtocol.Select(1, 0)

                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVWPTProtocols_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Try
            'If CType(Me.MdiParent, MainMenu).pnlLeft.Visible = False Then
            '    CType(Me.MdiParent, MainMenu).Splitter1.Visible = False
            'End If
            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            ''Above 2 lines commented by sudhir 20090202

        Catch ex As Exception
        End Try
    End Sub

    Private Sub AddCategory()

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



        dtWord = New DataTable
        objWord = New clsWordDocument

        '20120726:: Bug No 31922;enum value changed from PatientConsent to PTProtocol
        dtWord = objWord.FillTemplates(enumTemplateFlag.PTProtocol)

        If dtWord.Rows.Count = 0 Then
            ''''If not present then exit from sub
            MessageBox.Show("No Template is associated for PT Protocols. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtWord = Nothing
            objWord = Nothing
            Exit Sub
        Else
            Dim objfrmPTProtocols As New frmPTProtocols(_PatientID)

            Try
                '''''<><><><><> Check Patient Status <><><><><><>''''
                _blnAdd = True
                blnModify = False

                With objfrmPTProtocols
                    .MyCaller = Me
                    .MdiParent = Me.ParentForm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .WindowState = FormWindowState.Maximized
                    .Show()

                    .BringToFront()
                End With

                '21-Jan-15 Aniket: Resolving Bug #78687 ( Modified): gloEMR: View PT protocol- Grid displays database based columns name
                'If objfrmPTProtocols.CancelClick = False Then
                '    c1PTProtocol.Enabled = False
                '    c1PTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
                '    c1PTProtocol.Enabled = True
                'End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            Finally
                objfrmPTProtocols = Nothing
            End Try
        End If
    End Sub
    Private Sub UpdateCategory()
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
            If c1PTProtocol.Rows.Count > 1 Then

                If c1PTProtocol.RowSel > 0 Then
                    Call UpdatePTProtocol()
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub
    'Private Sub DeleteCategory_Old()
    '    Dim ProtocolID As Long
    '    Dim ProtocolDate As String
    '    Dim ProtocolHeader As String

    '    Try

    '        If grdPTProtocol.VisibleRowCount >= 1 Then

    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ''''' 20070125 -Mahesh 
    '            'If CheckPatientStatus(_PatientID) = False Then
    '            '    Exit Sub
    '            'End If

    '            If MainMenu.IsAccess(False, _PatientID) = False Then
    '                Exit Sub
    '            End If

    '            If grdPTProtocol.IsSelected(grdPTProtocol.CurrentRowIndex) = False Then
    '                Exit Sub
    '            End If

    '            If grdPTProtocol.Item(grdPTProtocol.CurrentRowIndex, 4) = "Yes" Then
    '                MessageBox.Show("The status of PT Protocol is finished, you cannot delete this PT Protocol.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Exit Sub
    '            End If

    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ProtocolID = grdPTProtocol.Item(grdPTProtocol.CurrentRowIndex, 0).ToString
    '            ProtocolDate = grdPTProtocol.Item(grdPTProtocol.CurrentRowIndex, 1).ToString

    '            ' '' <><><> Record Level Locking <><><><> 
    '            ' '' Mahesh - 20070718 
    '            Dim blnRecordLock As Boolean = False
    '            If gblnRecordLocking = True Then
    '                Dim mydt As New mytable
    '                mydt = Scan_n_Lock_Transaction(TrnType.PTProtocol, ProtocolID, 0, ProtocolDate)
    '                If mydt.Code <> gstrLoginName Or mydt.Description <> gstrClientMachineName Then
    '                    MessageBox.Show("This PT Protocol is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it now.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '            End If
    '            '''' <><><> Record Level Locking <><><><> 


    '            'blnModify = True
    '            If MessageBox.Show("Are you sure to Delete this Protocol?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

    '                ProtocolHeader = grdPTProtocol.Item(grdPTProtocol.CurrentRowIndex, 3).ToString
    '                objclsPTProtocol.DeletePTProtocol(ProtocolID, ProtocolDate, ProtocolHeader, _PatientID)
    '                grdPTProtocol.Enabled = False
    '                grdPTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
    '                grdPTProtocol.Enabled = True
    '                'If Not IsNothing(objclsPTProtocol.GetDataview) Then
    '                '    objclsPTProtocol.SortDataview(objclsPTProtocol.GetDataview.Table.Columns(1).ColumnName)
    '                'End If

    '                '''''''''''Code is Added by Anil on 20071105
    '                sortOrder = CType(grdPTProtocol.DataSource, DataView).Sort
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

    Private Sub DeleteCategory()
        Dim ProtocolID As Long
        Dim ProtocolDate As String
        Dim ProtocolHeader As String

        Try

            If c1PTProtocol.Rows.Count > 1 Then

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                'If CheckPatientStatus(_PatientID) = False Then
                '    Exit Sub
                'End If

                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                End If

                If c1PTProtocol.RowSel < 1 Then
                    Exit Sub
                End If

                If c1PTProtocol.Item(c1PTProtocol.RowSel, 4) = "Yes" Then
                    MessageBox.Show("The status of PT Protocol is finished, you cannot delete this PT Protocol.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ProtocolID = c1PTProtocol.Item(c1PTProtocol.RowSel, 0).ToString
                ProtocolDate = c1PTProtocol.Item(c1PTProtocol.RowSel, 1).ToString

                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable
                    mydt = Scan_n_Lock_Transaction(TrnType.PTProtocol, ProtocolID, 0, ProtocolDate)
                    If (IsNothing(mydt) = False) Then
                        If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                            MessageBox.Show("This PT Protocol is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it now.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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


                'blnModify = True
                If MessageBox.Show("Are you sure to Delete this Protocol?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                    ProtocolHeader = c1PTProtocol.Item(c1PTProtocol.RowSel, 3).ToString
                    objclsPTProtocol.DeletePTProtocol(ProtocolID, ProtocolDate, ProtocolHeader, _PatientID)
                    c1PTProtocol.Enabled = False
                    c1PTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
                    c1PTProtocol.Enabled = True
                    'If Not IsNothing(objclsPTProtocol.GetDataview) Then
                    '    objclsPTProtocol.SortDataview(objclsPTProtocol.GetDataview.Table.Columns(1).ColumnName)
                    'End If
                    Dim myDataView As DataView = CType(c1PTProtocol.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        '''''''''''Code is Added by Anil on 20071105
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
    'Private Sub RefreshCategory_Old()
    '    Try
    '        grdPTProtocol.Enabled = False
    '        grdPTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
    '        grdPTProtocol.Enabled = True

    '        CustomGridStyle()
    '        'Call RefreshLetters()
    '        ''''''Added by Anil on 20071105
    '        If grdPTProtocol.VisibleRowCount > 0 Then
    '            grdPTProtocol.CurrentRowIndex = 0
    '            grdPTProtocol.Select(0)
    '        End If
    '        txtSearch.Text = ""
    '        _blnSearch = True
    '        '''''''''''''''''''''''
    '    Catch ex As Exception
    '        'MessageBox.Show(ex.ToString, "PTProtocol", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub RefreshCategory()
        Try
            c1PTProtocol.Enabled = False
            c1PTProtocol.DataSource = objclsPTProtocol.GetAllPTProtocols(_PatientID)
            c1PTProtocol.Enabled = True

            SetGridStyle()
            'Call RefreshLetters()
            ''''''Added by Anil on 20071105
            If c1PTProtocol.Rows.Count > 1 Then
                c1PTProtocol.RowSel = 1
                c1PTProtocol.Select(1, 0)
            End If
            txtSearch.Text = ""
            _blnSearch = True
            '''''''''''''''''''''''
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "PTProtocol", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Shubhangi 20091007
        'Use to clear search text
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmVWPTProtocols_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
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

    'Public Delegate Sub GenerateCDAFromVWPTProtocols(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromVWPTProtocols(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromVWPTProtocols(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromVWPTProtocols(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub


    Private Sub c1PTProtocol_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1PTProtocol.AfterSort  ''added to resolve selection issue while sorting for bugid Bug #72080
        Try
            If ind > -1 Then
                Dim rw As C1.Win.C1FlexGrid.Row
                For Each rw In c1PTProtocol.Rows
                    Dim cm As CurrencyManager = CType(BindingContext(Me.c1PTProtocol.DataSource), CurrencyManager)
                    Dim dr As DataRowView = CType(rw.DataSource, DataRowView)
                    If Not dr Is Nothing Then
                        Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)
                        If currIndex = ind Then
                            Dim cr As C1.Win.C1FlexGrid.CellRange = c1PTProtocol.GetCellRange(rw.Index, 1)
                            ' to scroll the selected row in the visible area
                            c1PTProtocol.Select(cr, True)
                            cr = c1PTProtocol.GetCellRange(rw.Index, 0, rw.Index, c1PTProtocol.Cols.Count - 1)
                            c1PTProtocol.Select(cr, False)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("View PatientProtocol AfterSort " + ex.Message.ToString(), False)
        End Try
        ind = -1
    End Sub
    Private Sub c1PTProtocol_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PTProtocol.MouseClick   ''added to resolve selection issue while sorting for bugid Bug #72080
        Try
            If (Not IsNothing(c1PTProtocol.DataSource) AndAlso (c1PTProtocol.Rows.Count > 0)) Then
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1PTProtocol.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(cm.Current, DataRowView)
                ind = dr.Row.Table.Rows.IndexOf(dr.Row)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("View PatientProtocol MouseClick " + ex.Message.ToString(), False)
        End Try

    End Sub

    Private Sub c1PTProtocol_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PTProtocol.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1PTProtocol.HitTest(ptPoint)
        ''''''''''''Code is Added by Anil on 20071105
        If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.ColumnHeader Then

            'Select Case htInfo.Column
            '    Case 3
            '        lblSearch.Text = "Protocol Name"
            'End Select

            If txtSearch.Text = "" Then
                _blnSearch = True
            Else
                _blnSearch = False
                txtSearch.Text = ""
                _blnSearch = True
            End If
            '''''''''''''''''''''''''''''''''''''
        ElseIf htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
            ''''<><><><><> Check Patient Status <><><><><><>''''
            '''' 20060914 -Mahesh 
            Dim oclsPatReg As New ClsPatientRegistrationDBLayer
            With oclsPatReg
                Dim PatientStatus As String = ""
                'shweta//PatientStatus = .PatientStatus(gnPatientID)
                PatientStatus = .PatientStatus(_PatientID)
                '' If Patietn Status Is "Legal Pending" or "Decesed" then 
                '' dont Allow any activity against this Patient
                If PatientStatus = gtsrPatientStatus_Deceased Or PatientStatus = gtsrPatientStatus_Pending Then
                    MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "You can not perform any activity on this Patient. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End With
            oclsPatReg.Dispose()
            oclsPatReg = Nothing
            ''''<><><><><><><><><><><>''''
            Call UpdatePTProtocol()
        Else
            Exit Sub
        End If
    End Sub
End Class
