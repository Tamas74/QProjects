Imports gloEMR.gloEMRWord

Public Class frmVWPatientLetters

    Inherits System.Windows.Forms.Form
    Implements IPatientContext
    Implements IDisposable

    Dim _PatientID As Long
    Dim objclsPatientLetters As New clsPatientLetters
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim _blnAdd As Boolean
    Dim _blnSearch As Boolean = True


    Dim Col_Count As Integer = 5
    Dim Col_LetterID As Integer = 0
    Dim Col_LetterDate As Integer = 1
    Dim Col_TemplateID As Integer = 2
    Dim Col_LetterHeader As Integer = 3
    Dim Col_Finished As Integer = 4

    Public Shared blnModify As Boolean

    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents c1PatientLetters As C1.Win.C1FlexGrid.C1FlexGrid

    Dim _GridWidth As Int32

#Region " Windows Form Designer generated code "

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWPatientLetters))
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
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.c1PatientLetters = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.c1PatientLetters, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTopRight.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_RightBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_TopBrd)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(875, 24)
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
        Me.panel4.Location = New System.Drawing.Point(61, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 22)
        Me.panel4.TabIndex = 52
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
        Me.btnClear.TabIndex = 2
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
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(60, 20)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = " Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(873, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(874, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(875, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(881, 53)
        Me.pnlToolStrip.TabIndex = 3
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
        Me.ts_ViewButtons.Size = New System.Drawing.Size(881, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.TabStop = True
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
        Me.Panel1.Controls.Add(Me.c1PatientLetters)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(881, 531)
        Me.Panel1.TabIndex = 1
        '
        'c1PatientLetters
        '
        Me.c1PatientLetters.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1PatientLetters.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1PatientLetters.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1PatientLetters.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1PatientLetters.ColumnInfo = resources.GetString("c1PatientLetters.ColumnInfo")
        Me.c1PatientLetters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1PatientLetters.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1PatientLetters.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1PatientLetters.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1PatientLetters.Location = New System.Drawing.Point(4, 1)
        Me.c1PatientLetters.Name = "c1PatientLetters"
        Me.c1PatientLetters.Rows.Count = 1
        Me.c1PatientLetters.Rows.DefaultSize = 19
        Me.c1PatientLetters.Rows.Fixed = 0
        Me.c1PatientLetters.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1PatientLetters.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1PatientLetters.ShowCellLabels = True
        Me.c1PatientLetters.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1PatientLetters.Size = New System.Drawing.Size(873, 526)
        Me.c1PatientLetters.StyleInfo = resources.GetString("c1PatientLetters.StyleInfo")
        Me.c1PatientLetters.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 527)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(873, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 527)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(877, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 527)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(875, 1)
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
        Me.Panel2.Size = New System.Drawing.Size(881, 30)
        Me.Panel2.TabIndex = 0
        '
        'frmVWPatientLetters
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(881, 614)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWPatientLetters"
        Me.Text = "Patient Letters"
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
        CType(Me.c1PatientLetters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmVWPatientLetters_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (IsNothing(objclsPatientLetters) = False) Then
            objclsPatientLetters.Dispose()
            objclsPatientLetters = Nothing
        End If
    End Sub


    Private Sub frmVWPatientLetters_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSearch.Focus()
        Try
       

            c1PatientLetters.Enabled = False
            c1PatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
            c1PatientLetters.Enabled = True

            SetGridStyle()
            'for Patient Safety
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex, gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'for Patient Safety

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub UpdateLetters_Old()
    '    Dim LetterID As Long
    '    Dim TemplateID As Long
    '    Dim objfrmPatientLetter As frmPatientLetter

    '    Try

    '        _blnAdd = False
    '        If grdPatientLetters.VisibleRowCount >= 1 Then

    '            Dim grdIndex As Integer = grdPatientLetters.CurrentRowIndex
    '            blnModify = True
    '            LetterID = grdPatientLetters.Item(grdIndex, 0).ToString
    '            TemplateID = grdPatientLetters.Item(grdIndex, 2).ToString

    '            'Record Level Locking
    '            Dim blnRecordLock As Boolean = False
    '            If gblnRecordLocking = True Then
    '                Dim mydt As New mytable
    '                mydt = Scan_n_Lock_Transaction(TrnType.Letters, LetterID, 0, Now)
    '                If mydt.Description <> gstrClientMachineName Then
    '                    If MessageBox.Show("This Patient Letter is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '                        blnRecordLock = True
    '                    Else
    '                        Exit Sub
    '                    End If
    '                End If
    '            End If
    '            'Record Level Locking

    '            If grdPatientLetters.Item(grdIndex, 4).ToString = "Yes" Then
    '                objfrmPatientLetter = New frmPatientLetter(LetterID, TemplateID, True, blnRecordLock, _PatientID)
    '            Else
    '                If blnRecordLock Then
    '                    objfrmPatientLetter = New frmPatientLetter(LetterID, TemplateID, True, blnRecordLock, _PatientID)
    '                Else
    '                    objfrmPatientLetter = New frmPatientLetter(LetterID, TemplateID, False, blnRecordLock, _PatientID)
    '                End If
    '            End If

    '            sortOrder = CType(grdPatientLetters.DataSource, DataView).Sort
    '            strSearchstring = txtSearch.Text.Trim
    '            arrcolumnsort = Split(sortOrder, "]")
    '            If arrcolumnsort.Length > 1 Then
    '                strcolumnName = arrcolumnsort.GetValue(0)
    '                strsortorder = arrcolumnsort.GetValue(1)
    '            End If

    '            With objfrmPatientLetter
    '                .Text = "Modify Patient Letters"
    '                .MdiParent = Me.ParentForm
    '                .IsModify = True
    '                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '                .MyCaller = Me
    '                .WindowState = FormWindowState.Maximized
    '                .BringToFront()
    '                .Show()
    '            End With

    '            If objfrmPatientLetter.CancelClick = False Then
    '                grdPatientLetters.Enabled = False
    '                grdPatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
    '                grdPatientLetters.Enabled = True
    '                CustomGridStyle()

    '                Dim i As Integer
    '                For i = 0 To CType(grdPatientLetters.DataSource, DataView).Table.Rows.Count - 1
    '                    'When ID Found select that matching Row
    '                    If LetterID = grdPatientLetters.Item(i, 0) Then
    '                        grdPatientLetters.CurrentRowIndex = i
    '                        grdPatientLetters.Select(i)
    '                        Exit For
    '                    End If
    '                Next
    '            End If
    '        End If

    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.View, "Patient Letter viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
    '    Finally
    '        LetterID = Nothing
    '        TemplateID = Nothing
    '    End Try
    'End Sub


    Private Sub UpdateLetters()
        Dim LetterID As Long
        Dim TemplateID As Long
        Dim objfrmPatientLetter As frmPatientLetter

        Try

            _blnAdd = False
            If c1PatientLetters.Rows.Count > 1 And c1PatientLetters.RowSel >= 1 Then

                Dim grdIndex As Integer = c1PatientLetters.RowSel
                blnModify = True
                LetterID = c1PatientLetters.Item(grdIndex, 0).ToString
                TemplateID = c1PatientLetters.Item(grdIndex, 2).ToString

                'Record Level Locking
                Dim blnRecordLock As Boolean = False
                If gblnRecordLocking = True Then
                    Dim mydt As mytable = Nothing
                    mydt = Scan_n_Lock_Transaction(TrnType.Letters, LetterID, 0, Now)
                    If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                        If MessageBox.Show("This Patient Letter is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            blnRecordLock = True
                        Else
                            mydt.Dispose()
                            mydt = Nothing
                            Exit Sub
                        End If
                    End If
                    mydt.Dispose()
                    mydt = Nothing
                End If
                'Record Level Locking

                If c1PatientLetters.Item(grdIndex, 4).ToString = "Yes" Then
                    objfrmPatientLetter = New frmPatientLetter(LetterID, TemplateID, True, blnRecordLock, _PatientID)
                Else
                    If blnRecordLock Then
                        objfrmPatientLetter = New frmPatientLetter(LetterID, TemplateID, True, blnRecordLock, _PatientID)
                    Else
                        objfrmPatientLetter = New frmPatientLetter(LetterID, TemplateID, False, blnRecordLock, _PatientID)
                    End If
                End If
                Dim myDataView As DataView = CType(c1PatientLetters.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    sortOrder = myDataView.Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                End If
                With objfrmPatientLetter
                    .Text = "Modify Patient Letters"
                    .MdiParent = Me.ParentForm
                    .IsModify = True
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .MyCaller = Me
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                    .Show()
                End With

                If objfrmPatientLetter.CancelClick = False Then
                    c1PatientLetters.Enabled = False
                    c1PatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
                    c1PatientLetters.Enabled = True
                    SetGridStyle()

                    Dim i As Integer
                    For i = 1 To c1PatientLetters.Rows.Count - 1
                        'When ID Found select that matching Row
                        If LetterID = c1PatientLetters.Item(i, 0) Then
                            c1PatientLetters.RowSel = i
                            c1PatientLetters.Select(i, 0)
                            Exit For
                        End If
                    Next
                End If
            End If

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ReminderLetter, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.View, "Patient Letter viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
        Finally
            LetterID = Nothing
            TemplateID = Nothing
        End Try
    End Sub

    'Public Sub RefreshLetters_Old(ByVal LetterID As Long)

    '    grdPatientLetters.Enabled = False
    '    grdPatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
    '    grdPatientLetters.Enabled = True

    '    If _blnAdd = False Then
    '        'Flag is checked to find whether we are adding or modifying the record, so according to that the grid will be filled.
    '        sortOrder = CType(grdPatientLetters.DataSource, DataView).Sort
    '        strSearchstring = txtSearch.Text.Trim
    '        arrcolumnsort = Split(sortOrder, "]")
    '        If arrcolumnsort.Length > 1 Then
    '            strcolumnName = arrcolumnsort.GetValue(0)
    '            strsortorder = arrcolumnsort.GetValue(1)
    '        End If
    '        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
    '    Else
    '        CustomGridStyle()
    '    End If

    '    If LetterID <> 0 Then
    '        grdPatientLetters.UnSelect(0)
    '    End If

    '    'To Remember the Selection of Row 
    '    Dim i As Integer
    '    For i = 0 To CType(grdPatientLetters.DataSource, DataView).Table.Rows.Count - 1
    '        'when ID Found select that matching Row
    '        If LetterID = grdPatientLetters.Item(i, 0) Then
    '            grdPatientLetters.CurrentRowIndex = i
    '            grdPatientLetters.Select(i)
    '            Exit For
    '        End If
    '    Next
    'End Sub
    Public Sub RefreshLetters(ByVal LetterID As Long)

        c1PatientLetters.Enabled = False
        c1PatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
        c1PatientLetters.Enabled = True

        If _blnAdd = False Then
            'Flag is checked to find whether we are adding or modifying the record, so according to that the grid will be filled.
            Dim myDataView As DataView = CType(c1PatientLetters.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                sortOrder = CType(c1PatientLetters.DataSource, DataView).Sort
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

        If LetterID <> 0 Then
            c1PatientLetters.RowSel = -1
        End If

        'To Remember the Selection of Row 
        Dim i As Integer
        For i = 1 To c1PatientLetters.Rows.Count - 1
            'when ID Found select that matching Row
            If LetterID = c1PatientLetters.Item(i, 0) Then
                c1PatientLetters.RowSel = i
                c1PatientLetters.Select(i, 0)
                Exit For
            End If
        Next
    End Sub


    'Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
    '    Dim dt As DataTable
    '    Dim dv As DataView
    '    dt = objclsPatientLetters.GetDataTable
    '    dv = dt.DefaultView
    '    Dim ts As New clsDataGridTableStyle(dt.TableName)

    '    Dim LetterIDCol As New DataGridTextBoxColumn
    '    With LetterIDCol
    '        .Width = 0
    '        .MappingName = dv.Table.Columns(0).ColumnName
    '        .HeaderText = "Letter ID"
    '    End With

    '    Dim DateCol As New DataGridTextBoxColumn
    '    With DateCol
    '        .Width = 0.3 * _GridWidth
    '        .MappingName = dv.Table.Columns(1).ColumnName
    '        .HeaderText = "Letter Date"
    '        .NullText = ""
    '    End With

    '    Dim TempIDCol As New DataGridTextBoxColumn
    '    With TempIDCol
    '        .Width = 0
    '        .MappingName = dv.Table.Columns(2).ColumnName
    '        .HeaderText = "Template ID"
    '        .NullText = ""
    '    End With

    '    Dim TempNameCol As New DataGridTextBoxColumn
    '    With TempNameCol
    '        .Width = 0.5 * _GridWidth
    '        .MappingName = dv.Table.Columns(3).ColumnName
    '        .HeaderText = "Letter Header"
    '        .NullText = ""
    '    End With

    '    Dim IsFinishedCol As New DataGridTextBoxColumn
    '    With IsFinishedCol
    '        .Width = 0.2 * _GridWidth
    '        .MappingName = dv.Table.Columns(4).ColumnName
    '        .HeaderText = "Finished"
    '        .NullText = ""
    '    End With

    '    txtSearch.Text = ""
    '    txtSearch.Text = strsearchtxt
    '    If strcolumnName = "" Or IsNothing(strcolumnName) Then
    '        dv.Sort = "[" & dv.Table.Columns(3).ColumnName & "]" & strsortorder
    '    Else
    '        Dim strColumn As String = Replace(strcolumnName, "[", "")
    '        dv.Sort = "[" & strColumn & "]" & strSortBy
    '    End If

    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {LetterIDCol, DateCol, TempIDCol, TempNameCol, IsFinishedCol})
    '    grdPatientLetters.TableStyles.Clear()
    '    grdPatientLetters.TableStyles.Add(ts)

    '    If (dt.Rows.Count >= 1) Then
    '        grdPatientLetters.Select(0)
    '    End If

    '    dt.Dispose()
    '    dt = Nothing

    '    dv.Dispose()
    '    dv = Nothing

    '    ts.Dispose()
    '    ts = Nothing

    '    LetterIDCol.Dispose()
    '    LetterIDCol = Nothing

    '    DateCol.Dispose()
    '    DateCol = Nothing

    '    TempIDCol.Dispose()
    '    TempIDCol = Nothing

    '    TempNameCol.Dispose()
    '    TempNameCol = Nothing

    '    IsFinishedCol.Dispose()
    '    IsFinishedCol = Nothing
    'End Sub


    Private Sub SetGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        ''added to solve  sorting issue Bugid 72083
        Try


            Dim dt As DataTable
            Dim dv As DataView
            dt = objclsPatientLetters.GetDataTable

            '07-Oct-14 Aniket: Cannot dispose the below datatable as it contains data
            'If (IsNothing(dt) = False) Then
            '    dt = New DataTable()
            'End If

            dv = dt.DefaultView

            c1PatientLetters.DataSource = dv
            With c1PatientLetters
                .AllowSorting = True


                .Redraw = False
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = Screen.PrimaryScreen.WorkingArea.Width - 60
                c1PatientLetters.Width = _TotalWidth
                ' c1Disclosure.Height = Me.Height - 20
                c1PatientLetters.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                .Cols.Count = Col_Count
                .Rows.Fixed = 1
                .Styles.ClearUnused()
                .AllowResizing = True

                .Cols(Col_LetterID).Width = _TotalWidth * 0
                .Cols(Col_LetterID).AllowEditing = False
                .Cols(Col_LetterID).Visible = False
                .Cols(Col_LetterID).Caption = "Letter ID"
                .Cols(Col_LetterID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


                .Cols(Col_LetterDate).Width = _TotalWidth * 0.33
                .Cols(Col_LetterDate).AllowEditing = False
                .Cols(Col_LetterDate).Visible = True
                .Cols(Col_LetterDate).Caption = "Letter Date"
                .Cols(Col_LetterDate).DataType = GetType(System.DateTime)
                .Cols(Col_LetterDate).Format = "MM/dd/yyyy h:mm tt"

                .Cols(Col_LetterDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_TemplateID).Width = _TotalWidth * 0
                .Cols(Col_TemplateID).AllowEditing = False
                .Cols(Col_TemplateID).Visible = False
                .Cols(Col_TemplateID).Caption = "TemplateID"
                .Cols(Col_TemplateID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                .Cols(Col_LetterHeader).Width = _TotalWidth * 0.4
                .Cols(Col_LetterHeader).AllowEditing = False
                .Cols(Col_LetterHeader).Visible = True
                .Cols(Col_LetterHeader).Caption = "Letter Header"
                .Cols(Col_LetterHeader).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter



                .Cols(Col_Finished).Width = _TotalWidth * 0.27
                .Cols(Col_Finished).AllowEditing = False
                .Cols(Col_Finished).Visible = True
                .Cols(Col_Finished).Caption = "Finished"
                .Cols(Col_Finished).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter





                .Redraw = True


            End With


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally

        End Try

    End Sub



    'Private Sub grdModifiers_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim dvPatient As DataView
        Dim strPatientSearchDetails As String

        Try
            Me.Cursor = Cursors.WaitCursor
            dvPatient = CType(c1PatientLetters.DataSource(), DataView)

            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            c1PatientLetters.Enabled = False
            c1PatientLetters.DataSource = dvPatient
            c1PatientLetters.Enabled = True

            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                'Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            Else
                dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                   & dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%' "
            End If

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            Me.Cursor = Cursors.Default

            'If Not IsNothing(dvPatient) Then
            '    dvPatient.Dispose()
            '    dvPatient = Nothing
            'End If

            strPatientSearchDetails = Nothing
        End Try

    End Sub

    'Private Sub grdPatientLetters_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Dim ptPoint As Point = New Point(e.X, e.Y)
    '    Try
    '        Dim htInfo As DataGrid.HitTestInfo = grdPatientLetters.HitTest(ptPoint)
    '        If htInfo.Type = DataGrid.HitTestType.Cell Then
    '            grdPatientLetters.Select(htInfo.Row)
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        ptPoint = Nothing
    '    End Try
    'End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If c1PatientLetters.Rows.Count > 1 Then
                    c1PatientLetters.Select(1, 0)

                End If
            End If
            mdlGeneral.ValidateText(txtSearch.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVWPatientLetters_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.PatientLetters, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
        Try
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub grdPatientLetters_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Dim ptPoint As Point = New Point(e.X, e.Y)
    '    Dim htInfo As DataGrid.HitTestInfo = grdPatientLetters.HitTest(ptPoint)

    '    Try
    '        If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

    '            If txtSearch.Text = "" Then
    '                _blnSearch = True
    '            Else
    '                _blnSearch = False
    '                txtSearch.Text = ""
    '                _blnSearch = True
    '            End If

    '        ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
    '            UpdateLetters()
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        ptPoint = Nothing
    '        htInfo = Nothing
    '    End Try
    'End Sub

    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007
    'Private Sub AddCategory_Old()

    '    If MainMenu.IsAccess(False, _PatientID) = False Then
    '        Exit Sub
    '    End If

    '    If gblnProviderDisable = True Then
    '        If ShowAssociateProvider(_PatientID, Me) = True Then
    '            CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
    '        End If
    '    End If


    '    'To check exeception related to word
    '    If CheckWordForException() = False Then
    '        Exit Sub
    '    End If
    '    'End

    '    dtWord = New DataTable
    '    objWord = New clsWordDocument
    '    dtWord = objWord.FillTemplates(enumTemplateFlag.PatientLetters)

    '    If dtWord.Rows.Count = 0 Then
    '        'If not present then exit from sub
    '        MessageBox.Show("No template is associated for Patient Letter. First associate any template.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        objWord = Nothing
    '        dtWord = Nothing
    '        Exit Sub
    '    Else
    '        _blnAdd = True
    '        Dim objfrmPatientLetter As New frmPatientLetter(_PatientID)
    '        Try
    '            blnModify = False

    '            With objfrmPatientLetter
    '                .MyCaller = Me
    '                .MdiParent = Me.ParentForm
    '                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '                .WindowState = FormWindowState.Maximized
    '                .BringToFront()
    '                .Show()
    '            End With

    '            If objfrmPatientLetter.CancelClick = False Then
    '                grdPatientLetters.Enabled = False
    '                grdPatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
    '                grdPatientLetters.Enabled = True
    '            End If

    '        Catch ex As Exception
    '            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

    '        Finally
    '            objfrmPatientLetter = Nothing
    '        End Try
    '    End If
    'End Sub

    Private Sub AddCategory()

        If MainMenu.IsAccess(False, _PatientID) = False Then
            Exit Sub
        End If

        If gblnProviderDisable = True Then
            If ShowAssociateProvider(_PatientID, Me) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If


        'To check exeception related to word
        If CheckWordForException() = False Then
            Exit Sub
        End If
        'End

        '  dtWord = New DataTable
        Dim objWord As clsWordDocument = New clsWordDocument
        Dim dtWord As DataTable = objWord.FillTemplates(enumTemplateFlag.PatientLetters)
        objWord = Nothing
        If dtWord.Rows.Count = 0 Then
            'If not present then exit from sub
            MessageBox.Show("No template is associated for Patient Letter. First associate any template.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            dtWord.Dispose()
            dtWord = Nothing
            Exit Sub
        Else
            dtWord.Dispose()
            dtWord = Nothing
            _blnAdd = True
            Dim objfrmPatientLetter As New frmPatientLetter(_PatientID)
            Try
                blnModify = False

                With objfrmPatientLetter
                    .MyCaller = Me
                    .MdiParent = Me.ParentForm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                    .Show()
                End With

                If objfrmPatientLetter.CancelClick = False Then
                    c1PatientLetters.Enabled = False
                    c1PatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
                    c1PatientLetters.Enabled = True
                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            Finally
                objfrmPatientLetter = Nothing
            End Try
        End If
    End Sub



    'Private Sub UpdateCategory_Old()
    '    Try

    '        If MainMenu.IsAccess(False, _PatientID) = False Then
    '            Exit Sub
    '        End If

    '        'To check exeception related to word
    '        If CheckWordForException() = False Then
    '            Exit Sub
    '        End If
    '        'End

    '        If grdPatientLetters.VisibleRowCount >= 1 Then
    '            If grdPatientLetters.IsSelected(grdPatientLetters.CurrentRowIndex) Then
    '                Call UpdateLetters()
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub UpdateCategory()
        Try

            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If

            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End

            If c1PatientLetters.Rows.Count > 1 Then
                If c1PatientLetters.RowSel >= 1 Then
                    Call UpdateLetters()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub DeleteCategory_Old()
    '    Dim ID As Long
    '    Dim LetterDate As String
    '    Dim LetterHeader As String

    '    Try

    '        If MainMenu.IsAccess(False, _PatientID) = False Then
    '            Exit Sub
    '        End If

    '        If grdPatientLetters.VisibleRowCount >= 1 Then
    '            If grdPatientLetters.IsSelected(grdPatientLetters.CurrentRowIndex) = False Then
    '                Exit Sub
    '            End If
    '            If grdPatientLetters.Item(grdPatientLetters.CurrentRowIndex, 4) = "Yes" Then
    '                MessageBox.Show("The status of Patient Letters is finished, you cannot delete this Patient Letters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Exit Sub
    '            End If
    '            ID = grdPatientLetters.Item(grdPatientLetters.CurrentRowIndex, 0).ToString
    '            LetterDate = grdPatientLetters.Item(grdPatientLetters.CurrentRowIndex, 1).ToString

    '            'Record Level Locking
    '            ' '' Mahesh - 20070718 
    '            If gblnRecordLocking = True Then
    '                Dim mydt As New mytable
    '                mydt = Scan_n_Lock_Transaction(TrnType.Letters, ID, 0, LetterDate)
    '                If mydt.Description <> gstrClientMachineName Then
    '                    MessageBox.Show("This Patient Letter is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it now.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    Exit Sub
    '                End If
    '                mydt = Nothing
    '            End If
    '            'Record Level Locking

    '            'blnModify = True
    '            If MessageBox.Show("Do you want to delete selected Patient Letters?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

    '                LetterHeader = grdPatientLetters.Item(grdPatientLetters.CurrentRowIndex, 3).ToString
    '                objclsPatientLetters.DeletePatientLetter(ID, LetterDate, LetterHeader, _PatientID)
    '                grdPatientLetters.Enabled = False
    '                grdPatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
    '                grdPatientLetters.Enabled = True

    '                sortOrder = CType(grdPatientLetters.DataSource, DataView).Sort
    '                strSearchstring = txtSearch.Text.Trim
    '                arrcolumnsort = Split(sortOrder, "]")
    '                If arrcolumnsort.Length > 1 Then
    '                    strcolumnName = arrcolumnsort.GetValue(0)
    '                    strsortorder = arrcolumnsort.GetValue(1)
    '                End If

    '                CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
    '            End If
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        ID = Nothing
    '        LetterDate = Nothing
    '        LetterHeader = Nothing
    '    End Try
    'End Sub

    Private Sub DeleteCategory()
        Dim ID As Long
        Dim LetterDate As String
        Dim LetterHeader As String

        Try

            If MainMenu.IsAccess(False, _PatientID) = False Then
                Exit Sub
            End If

            If c1PatientLetters.Rows.Count > 1 Then
                If c1PatientLetters.RowSel < 1 Then
                    Exit Sub
                End If
                If c1PatientLetters.Item(c1PatientLetters.RowSel, 4) = "Yes" Then
                    MessageBox.Show("The status of Patient Letters is finished, you cannot delete this Patient Letters", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                ID = c1PatientLetters.Item(c1PatientLetters.RowSel, 0).ToString
                LetterDate = c1PatientLetters.Item(c1PatientLetters.RowSel, 1).ToString

                'Record Level Locking
                ' '' Mahesh - 20070718 
                If gblnRecordLocking = True Then
                    Dim mydt As mytable = Nothing
                    mydt = Scan_n_Lock_Transaction(TrnType.Letters, ID, 0, LetterDate)
                    If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                        MessageBox.Show("This Patient Letter is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it now.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        mydt.Dispose()
                        mydt = Nothing
                        Exit Sub
                    End If
                    mydt.Dispose()
                    mydt = Nothing
                End If
                'Record Level Locking

                'blnModify = True
                If MessageBox.Show("Do you want to delete selected Patient Letters?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then

                    LetterHeader = c1PatientLetters.Item(c1PatientLetters.RowSel, 3).ToString
                    objclsPatientLetters.DeletePatientLetter(ID, LetterDate, LetterHeader, _PatientID)
                    c1PatientLetters.Enabled = False
                    c1PatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
                    c1PatientLetters.Enabled = True
                    Dim myDataView As DataView = CType(c1PatientLetters.DataSource, DataView)
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
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ID = Nothing
            LetterDate = Nothing
            LetterHeader = Nothing
        End Try
    End Sub


    'Private Sub RefreshCategory_Old()
    '    Try

    '        grdPatientLetters.Enabled = False
    '        grdPatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
    '        grdPatientLetters.Enabled = True
    '        CustomGridStyle()

    '        If grdPatientLetters.VisibleRowCount > 0 Then
    '            grdPatientLetters.CurrentRowIndex = 0
    '            grdPatientLetters.Select(0)
    '        End If

    '        txtSearch.Text = ""
    '        _blnSearch = True

    '    Catch ex As Exception
    '        'MessageBox.Show(ex.ToString, "Patient Letters", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub
    Private Sub RefreshCategory()
        Try

            c1PatientLetters.Enabled = False
            c1PatientLetters.DataSource = objclsPatientLetters.GetAllPatientLetters(_PatientID)
            c1PatientLetters.Enabled = True
            SetGridStyle()

            If c1PatientLetters.Rows.Count > 1 Then
                c1PatientLetters.RowSel = 1
                c1PatientLetters.Select(0, 0)
            End If

            txtSearch.Text = ""
            _blnSearch = True

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "Patient Letters", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FormClose()
        Me.Close()
        Try
            'Application.DoEvents()
            Me.Dispose()
        Catch exdispose As Exception

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
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmVWPatientLetters_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        _GridWidth = c1PatientLetters.Width
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
    Private Sub c1PatientLetters_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1PatientLetters.AfterSort '' added to solve sorting issue bugid 72079
        Try
            If ind > -1 Then
                Dim rw As C1.Win.C1FlexGrid.Row
                For Each rw In c1PatientLetters.Rows
                    Dim cm As CurrencyManager = CType(BindingContext(Me.c1PatientLetters.DataSource), CurrencyManager)
                    Dim dr As DataRowView = CType(rw.DataSource, DataRowView)
                    If Not dr Is Nothing Then
                        Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)
                        If currIndex = ind Then
                            Dim cr As C1.Win.C1FlexGrid.CellRange = c1PatientLetters.GetCellRange(rw.Index, 1)
                            ' to scroll the selected row in the visible area
                            c1PatientLetters.Select(cr, True)
                            cr = c1PatientLetters.GetCellRange(rw.Index, 0, rw.Index, c1PatientLetters.Cols.Count - 1)
                            c1PatientLetters.Select(cr, False)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
        ind = -1
    End Sub

    Private Sub c1PatientLetters_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PatientLetters.MouseClick '' added to solve sorting issue for bugid 72079
        Try
            If (Not IsNothing(c1PatientLetters.DataSource) AndAlso (c1PatientLetters.Rows.Count > 0)) Then
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1PatientLetters.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(cm.Current, DataRowView)
                ind = dr.Row.Table.Rows.IndexOf(dr.Row)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        End Try
    End Sub

    Private Sub c1PatientLetters_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PatientLetters.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1PatientLetters.HitTest(ptPoint)

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
            '' MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''  MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        End Try

    End Sub
End Class
