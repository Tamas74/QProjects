Imports QSurvey.Models

Public Class frmViewScreenings

    Inherits System.Windows.Forms.Form    


    Dim nPatientID As Long


    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String

    Dim Col_HoosID As Integer = 0
    Dim Col_FormDisplayName As Integer = 1
    Dim Col_ProviderName As Integer = 2
    Dim Col_ScreeningDate As Integer = 3
    Dim Col_FormName As Integer = 4
    Dim Col_Count As Integer = 5

    Dim ind As Integer = -1
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton


    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
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
    Friend WithEvents c1Screenings As C1.Win.C1FlexGrid.C1FlexGrid


#Region " Windows Form Designer generated code "
    'constructor commnted by dipak 20100907 as not used anywhere 
    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal PatientID As Long)
        InitializeComponent()
        nPatientID = PatientID
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewScreenings))
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
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.c1Screenings = New C1.Win.C1FlexGrid.C1FlexGrid()
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
        Me.pnlMain.SuspendLayout()
        CType(Me.c1Screenings, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnDelete, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(848, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
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
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.c1Screenings)
        Me.pnlMain.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMain.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMain.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMain.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 82)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(848, 436)
        Me.pnlMain.TabIndex = 0
        '
        'c1Screenings
        '
        Me.c1Screenings.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Screenings.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1Screenings.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Screenings.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Screenings.ColumnInfo = resources.GetString("c1Screenings.ColumnInfo")
        Me.c1Screenings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Screenings.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Screenings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Screenings.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1Screenings.Location = New System.Drawing.Point(4, 1)
        Me.c1Screenings.Name = "c1Screenings"
        Me.c1Screenings.Rows.Count = 1
        Me.c1Screenings.Rows.DefaultSize = 19
        Me.c1Screenings.Rows.Fixed = 0
        Me.c1Screenings.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Screenings.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Screenings.ShowCellLabels = True
        Me.c1Screenings.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1Screenings.Size = New System.Drawing.Size(840, 431)
        Me.c1Screenings.StyleInfo = resources.GetString("c1Screenings.StyleInfo")
        Me.c1Screenings.TabIndex = 15
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
        'frmViewScreenings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(848, 518)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewScreenings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Screenings"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.c1Screenings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub frmVWPTProtocol_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.LoadScreenings()
    End Sub

    Private Sub LoadScreenings()
        Try
            c1Screenings.Enabled = False
            c1Screenings.DataSource = clsViewScreenings.GetScreenings(nPatientID, gnLoginProviderID, "")
            c1Screenings.Enabled = True

            SetGridStyle()

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, nPatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try

            If TypeOf (c1Screenings.DataSource) Is DataTable Then
                Dim dt As DataTable
                Dim dv As DataView
                dt = c1Screenings.DataSource
                dv = dt.DefaultView

                c1Screenings.DataSource = dv
                With c1Screenings
                    .AllowSorting = True
                    .ExtendLastCol = True

                    .Redraw = False
                    .Dock = DockStyle.Fill
                    Dim _TotalWidth As Single = 0
                    _TotalWidth = Screen.PrimaryScreen.WorkingArea.Width - 40
                    c1Screenings.Width = _TotalWidth

                    c1Screenings.ShowCellLabels = False
                    Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                    .Cols.Count = Col_Count
                    .Rows.Fixed = 1
                    .Styles.ClearUnused()
                    .AllowResizing = True

                    .Cols(Col_HoosID).Width = _TotalWidth * 0
                    .Cols(Col_HoosID).AllowEditing = False
                    .Cols(Col_HoosID).Visible = False
                    .Cols(Col_HoosID).Caption = "ScreeningID"
                    .Cols(Col_HoosID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                    .Cols(Col_FormName).Width = _TotalWidth * 0
                    .Cols(Col_FormName).AllowEditing = False
                    .Cols(Col_FormName).Visible = False
                    .Cols(Col_FormName).Caption = "Screening Name"
                    .Cols(Col_FormName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                    .Cols(Col_ProviderName).Width = _TotalWidth * 0.3
                    .Cols(Col_ProviderName).AllowEditing = False
                    .Cols(Col_ProviderName).Visible = True
                    .Cols(Col_ProviderName).Caption = "Provider Name"
                    .Cols(Col_ProviderName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                    .Cols(Col_FormDisplayName).Width = _TotalWidth * 0.3
                    .Cols(Col_FormDisplayName).AllowEditing = False
                    .Cols(Col_FormDisplayName).Visible = True
                    .Cols(Col_FormDisplayName).Caption = "Screening"
                    .Cols(Col_FormDisplayName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                    .Cols(Col_ScreeningDate).Width = _TotalWidth * 0.33
                    .Cols(Col_ScreeningDate).AllowEditing = False
                    .Cols(Col_ScreeningDate).Visible = True
                    .Cols(Col_ScreeningDate).Caption = "Screening Date"
                    .Cols(Col_ScreeningDate).DataType = GetType(System.DateTime)
                    .Cols(Col_ScreeningDate).Format = "MM/dd/yyyy h:mm tt"

                    .Redraw = True
                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView
            dvPatient = CType(c1Screenings.DataSource(), DataView)
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            c1Screenings.Enabled = False
            c1Screenings.DataSource = dvPatient
            c1Screenings.Enabled = True
            Dim strPatientSearchDetails As String

            If Trim(txtSearch.Text) <> "" Then
                strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            'Case "Form Name"
            If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                dvPatient.RowFilter = dvPatient.Table.Columns("sFormDisplayName").ColumnName & " Like '%" & strPatientSearchDetails & "%'"
            Else
                dvPatient.RowFilter = dvPatient.Table.Columns("sFormDisplayName").ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                        & dvPatient.Table.Columns("ProviderName").ColumnName & " Like '%" & strPatientSearchDetails & "%' "
            End If
            ' End Select
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If c1Screenings.Rows.Count > 1 Then
                    c1Screenings.Select(1, 0)

                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Delete"
                If c1Screenings.RowSel > 0 Then
                    If DeleteScreening() Then
                        LoadScreenings()
                    End If
                End If
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmViewScreenings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        SetGridStyle()
    End Sub

    Private Sub c1PTProtocol_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1Screenings.AfterSort  ''added to resolve selection issue while sorting for bugid Bug #72080
        Try
            If ind > -1 Then
                Dim rw As C1.Win.C1FlexGrid.Row
                For Each rw In c1Screenings.Rows
                    Dim cm As CurrencyManager = CType(BindingContext(Me.c1Screenings.DataSource), CurrencyManager)
                    Dim dr As DataRowView = CType(rw.DataSource, DataRowView)
                    If Not dr Is Nothing Then
                        Dim currIndex As Integer = dr.Row.Table.Rows.IndexOf(dr.Row)
                        If currIndex = ind Then
                            Dim cr As C1.Win.C1FlexGrid.CellRange = c1Screenings.GetCellRange(rw.Index, 1)
                            ' to scroll the selected row in the visible area
                            c1Screenings.Select(cr, True)
                            cr = c1Screenings.GetCellRange(rw.Index, 0, rw.Index, c1Screenings.Cols.Count - 1)
                            c1Screenings.Select(cr, False)
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("View Screenings AfterSort " + ex.Message.ToString(), False)
        End Try
        ind = -1
    End Sub

    Private Sub c1PTProtocol_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1Screenings.MouseClick   ''added to resolve selection issue while sorting for bugid Bug #72080
        Try
            If (Not IsNothing(c1Screenings.DataSource) AndAlso (c1Screenings.Rows.Count > 0)) Then
                Dim cm As CurrencyManager = CType(BindingContext(Me.c1Screenings.DataSource), CurrencyManager)
                Dim dr As DataRowView = CType(cm.Current, DataRowView)
                ind = dr.Row.Table.Rows.IndexOf(dr.Row)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("View Screenings MouseClick " + ex.Message.ToString(), False)
        End Try

    End Sub

    Private Sub c1PTProtocol_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1Screenings.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1Screenings.HitTest(ptPoint)
        ''''''''''''Code is Added by Anil on 20071105
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
            OpenScreening()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub OpenScreening()
        Dim nScreeningID As Int64 = 0
        Dim screeningType As gloMU.ScreeningEnum = gloMU.ScreeningEnum.HoosHip
        Dim screeningModel As QSurvey.Models.Request = Nothing
        Dim sScreeningType As String = String.Empty
        Dim enumScreeningType As gloMU.ScreeningEnum = gloMU.ScreeningEnum.HoosHip
        Try
            nScreeningID = Convert.ToInt64(c1Screenings.Item(c1Screenings.RowSel, Me.Col_HoosID))
            sScreeningType = Convert.ToString(c1Screenings.Item(c1Screenings.RowSel, Me.Col_FormName))

            If [Enum].TryParse(sScreeningType, enumScreeningType) Then

                screeningModel = gloMU.DBOperations.GetScreeningObject(nScreeningID, enumScreeningType, GetConnectionString())

                If screeningModel IsNot Nothing Then
                    Dim frmScreening As New gloMU.frmMUScreeningForm(nPatientID, screeningModel, enumScreeningType, gstrHoosKoosSurveyUrl) With {.SurveyType = gloMU.ScreeningMode.View}
                    frmScreening.MdiParent = Me.ParentForm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    frmScreening.Show()
                    frmScreening.BringToFront()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ScreeningTools, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.View, "Viewed screening", nPatientID, nScreeningID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("View Screening MouseClick " + ex.Message.ToString(), False)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ScreeningTools, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.View, "Viewed screening", nPatientID, nScreeningID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Function DeleteScreening() As Boolean
        Dim bReturned As Boolean = False
        Dim nScreeningID As Int64 = 0

        Try
            If MessageBox.Show("Do you want to delete this screening?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                bReturned = True
                nScreeningID = Convert.ToInt64(c1Screenings.Item(c1Screenings.RowSel, Me.Col_HoosID))
                gloMU.DBOperations.DeleteScreening(nScreeningID, GetConnectionString())
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ScreeningTools, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Delete, "Deleted screening", nPatientID, nScreeningID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Delete Screenings MouseClick " + ex.Message.ToString(), False)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ScreeningTools, gloAuditTrail.ActivityCategory.MUScreeningTools, gloAuditTrail.ActivityType.Delete, "Deleted screening", nPatientID, nScreeningID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return bReturned
    End Function

End Class
