Imports gloEMR.gloEMRWord ''added namespace for  bugid 70606 showing message if no template exist
Public Class frmVWMessages
    Inherits System.Windows.Forms.Form
    Public Shared blnModify As Boolean
    Dim objMessages As New clsMessage
    Dim m_UserID As Long
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String
    Dim _PatientID As Long
    Dim Default_PatientID As Long
    Dim _IsSentMessages As Boolean = True
    'Dim grdmsgTooltip As ToolTip = Nothing
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Dim _blnAdd As Boolean
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
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents rbRecievedMessage As System.Windows.Forms.RadioButton
    Friend WithEvents rbSentMessage As System.Windows.Forms.RadioButton
    Dim ColumnHeader As String = "Patient ID"
    Dim _SelMsgid As String = "0"
    Dim isMessageDeleted As Boolean = False
    Public Shared IsOpen As Boolean = False
    Private Shared frm As frmVWMessages
    Friend WithEvents c1Messages As C1.Win.C1FlexGrid.C1FlexGrid  ''remove datagrid and added c1flexgrid v8022 for tooltip issue 69505
    Private Prevtooltiptext As String = ""
    Dim Col_MessageID As Integer = 0  ''added   for tooltip issue 69505 ,v8022
    Dim Col_MessageDate As Integer = 1
    Dim Col_PatientID As Integer = 2
    Dim Col_PatientName As Integer = 3
    Dim Col_Subject As Integer = 4
    Dim Col_Template As Integer = 5
    Dim Col_Finished As Integer = 6
    Dim Col_Priority As Integer = 7
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Dim Col_Count As Integer = 8

#Region " Windows Form DeMessagesner generated code "
#Region "Constructor"
    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form DeMessagesner.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    ''Commented by Dhruv 20100129 
    'Public Sub New(ByVal UserID As Long)
    '    MyBase.New()

    '    m_UserID = UserID

    '    'This call is required by the Windows Form DeMessagesner.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub
    '-------------------------
    ''' <summary>
    ''' Dhruv 20100129 
    ''' it is called from the Dashboard 
    ''' as setting is passed through it 
    ''' recieved and sent
    ''' </summary>
    ''' <param name="ShowSentMessages"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal ShowSentMessages As Boolean, ByVal PatientID As Long)
        MyBase.New()
        _IsSentMessages = ShowSentMessages
        'This call is required by the Windows Form DeMessagesner.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        Default_PatientID = PatientID
    End Sub
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form DeMessagesner.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub
#End Region

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWMessages))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.rbSentMessage = New System.Windows.Forms.RadioButton()
        Me.rbRecievedMessage = New System.Windows.Forms.RadioButton()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
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
        Me.c1Messages = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlTopRight.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.c1Messages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.rbSentMessage)
        Me.pnlTopRight.Controls.Add(Me.rbRecievedMessage)
        Me.pnlTopRight.Controls.Add(Me.btnClear)
        Me.pnlTopRight.Controls.Add(Me.Label9)
        Me.pnlTopRight.Controls.Add(Me.txtSearch)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_RightBrd)
        Me.pnlTopRight.Controls.Add(Me.lbl_TopBrd)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(648, 24)
        Me.pnlTopRight.TabIndex = 0
        '
        'rbSentMessage
        '
        Me.rbSentMessage.AutoSize = True
        Me.rbSentMessage.Location = New System.Drawing.Point(581, 3)
        Me.rbSentMessage.Name = "rbSentMessage"
        Me.rbSentMessage.Size = New System.Drawing.Size(106, 18)
        Me.rbSentMessage.TabIndex = 51
        Me.rbSentMessage.TabStop = True
        Me.rbSentMessage.Text = "Sent Messages"
        Me.rbSentMessage.UseVisualStyleBackColor = True
        '
        'rbRecievedMessage
        '
        Me.rbRecievedMessage.AutoSize = True
        Me.rbRecievedMessage.Location = New System.Drawing.Point(429, 3)
        Me.rbRecievedMessage.Name = "rbRecievedMessage"
        Me.rbRecievedMessage.Size = New System.Drawing.Size(129, 18)
        Me.rbRecievedMessage.TabIndex = 50
        Me.rbRecievedMessage.TabStop = True
        Me.rbRecievedMessage.Text = "Received Messages"
        Me.rbRecievedMessage.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(293, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(289, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label9.Size = New System.Drawing.Size(4, 20)
        Me.Label9.TabIndex = 49
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(69, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(220, 22)
        Me.txtSearch.TabIndex = 3
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(68, 22)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = "   Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(646, 1)
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
        Me.lbl_RightBrd.Location = New System.Drawing.Point(647, 1)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(648, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(654, 53)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(654, 53)
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
        Me.Panel1.AutoScroll = False
        Me.Panel1.Controls.Add(Me.c1Messages)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(654, 426)
        Me.Panel1.TabIndex = 12
        '
        'c1Messages
        '
        Me.c1Messages.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Messages.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1Messages.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Messages.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Messages.ColumnInfo = resources.GetString("c1Messages.ColumnInfo")
        Me.c1Messages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Messages.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Messages.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Messages.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1Messages.Location = New System.Drawing.Point(4, 1)
        Me.c1Messages.Name = "c1Messages"
        Me.c1Messages.Rows.Count = 1
        Me.c1Messages.Rows.DefaultSize = 19
        Me.c1Messages.Rows.Fixed = 0
        Me.c1Messages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.c1Messages.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Messages.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Messages.ShowCellLabels = True
        Me.c1Messages.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1Messages.Size = New System.Drawing.Size(646, 421)
        Me.c1Messages.StyleInfo = resources.GetString("c1Messages.StyleInfo")
        Me.c1Messages.TabIndex = 10

        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 422)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(646, 1)
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
        Me.Label2.Size = New System.Drawing.Size(1, 422)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(650, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 422)
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
        Me.Label4.Size = New System.Drawing.Size(648, 1)
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
        Me.Panel2.Size = New System.Drawing.Size(654, 30)
        Me.Panel2.TabIndex = 13
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmVWMessages
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(654, 509)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWMessages"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Messages"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.c1Messages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Shared Function GetInstance(ByVal ShowSentMessages As Boolean, ByVal PatientID As Long) As frmVWMessages
        '_mu.WaitOne()
        Try

            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmVWMessages" Then
                    If CType(f, frmVWMessages).Default_PatientID = PatientID Then
                        IsOpen = True
                        frm = f
                        ''slr come out of for
                        Exit For
                    End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmVWMessages(ShowSentMessages, PatientID)
            End If

            'If frm Is Nothing Then
            '    frm = New frm_LM_Orders(VisitID, VisitDate, PatientID, OpenfromMainGrid, blnRecordLock)
            'End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function


    Private Sub frmVWMessages_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'If Not IsNothing(grdmsgTooltip) Then
        '    grdmsgTooltip.Dispose()
        '    grdmsgTooltip = Nothing
        'End If
        If (isMessageDeleted) Then
            CType(Me.ParentForm, MainMenu).ShowMessages_New()
        End If
        If (IsNothing(objMessages) = False) Then
            objMessages.Dispose()
            objMessages = Nothing
        End If
    End Sub

    Private Sub frmVwMessages_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
        '_PaitentID = gnPatientID
        'end comment

        Try
            ''Commented By Dhruv 20100128 ----------------------------------------------
            'Dim dt As DataTable
            'Dim dv As DataView
            'm_UserID = objMessages.GetUserID(gstrLoginName)
            'dt = objMessages.GetAllMessagesList("F", m_UserID)
            'grdMessages.Enabled = False
            'dv = dt.DefaultView
            'grdMessages.DataSource = dv
            'grdMessages.Enabled = True
            'CustomGridStyle()
            ''-------------------------------------------------------------------------
            Prevtooltiptext = ""
            If _IsSentMessages = True Then                      ''Dhruv 20100128 When clicked from the view - message
                rbSentMessage.Checked = True                    '' it will show the sent messages
            Else
                rbRecievedMessage.Checked = True                ''other vise clicked directly from the dashboard icon will show recieved messa  
            End If                                              ''dhruv----------------------------------------------end


            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Patient Messages Opened", gloAuditTrail.ActivityOutCome.Success)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Other, "Patient Messages Opened", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Private Sub UpdateMessages2()
    '    Dim MsgID As Long
    '    Dim MsgDate As Date
    '    '  Dim PatientID As Long

    '    Dim objfrmMsg As frmMessages

    '    Try
    '        _blnAdd = False
    '        If grdMessages.VisibleRowCount >= 1 Then
    '            _PatientID = grdMessages.Item(grdMessages.CurrentRowIndex, 2)


    '            If MainMenu.IsAccess(False, _PatientID, , True) = False Then
    '                Exit Sub
    '            End If

    '            '' 20100728 : Statement Added by Mahesh to set Selected patient on Messages globally.
    '            CType(Me.ParentForm, MainMenu).ShowDefaultPatientDetails(_PatientID)
    '            ''

    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ''''' 20070125 -Mahesh 
    '            'If CheckPatientStatus(_PatientID, grdMessages.Item(grdMessages.CurrentRowIndex, 2).ToString) = False Then
    '            '    Exit Sub
    '            'End If
    '            If MainMenu.IsAccess(False, CType(grdMessages.Item(grdMessages.CurrentRowIndex, 2), Long)) = False Then
    '                Exit Sub
    '            End If
    '            '''''<><><><><> Check Patient Status <><><><><><>''''

    '            '******Shweta 20090828 *********'
    '            'To check exeception related to word
    '            If CheckWordForException() = False Then
    '                Exit Sub
    '            End If
    '            'End Shweta

    '            Dim blnisFinished As Boolean = False

    '            If grdMessages.Item(grdMessages.CurrentRowIndex, 5) = "Yes" Then
    '                blnisFinished = True
    '            Else
    '                blnisFinished = False
    '            End If

    '            blnModify = True
    '            MsgID = grdMessages.Item(grdMessages.CurrentRowIndex, 0).ToString
    '            MsgDate = grdMessages.Item(grdMessages.CurrentRowIndex, 1)
    '            'PatientID = grdMessages.Item(grdMessages.CurrentRowIndex, 3)

    '            ' '' <><><> Record Level Locking <><><><> 
    '            ' '' Mahesh - 20070718 
    '            Dim blnRecordLock As Boolean = False

    '            Dim mydt As mytable ''slr new not needed 
    '            If gblnRecordLocking = True Then
    '                mydt = Scan_n_Lock_Transaction(TrnType.Messages, MsgID, 0, MsgDate)
    '                If mydt.Code <> gstrLoginName Or mydt.Description <> gstrClientMachineName Then
    '                    If MessageBox.Show("This message is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
    '                        ''Record open only for view.
    '                        blnRecordLock = True
    '                        '' Word document is only view can't add data to documents.
    '                        blnisFinished = True
    '                    Else
    '                        'Return False
    '                        ''slr free mydt
    '                        If Not IsNothing(mydt) Then
    '                            mydt.Dispose()
    '                            mydt = Nothing
    '                        End If

    '                        Exit Sub
    '                    End If
    '                End If
    '                ''slr free mydt
    '                If Not IsNothing(mydt) Then
    '                    mydt.Dispose()
    '                    mydt = Nothing
    '                End If
    '            End If
    '            '''' <><><> Record Level Locking <><><><> 

    '            objfrmMsg = frmMessages.GetInstance(MsgID, m_UserID, MsgDate, _PatientID, 0, blnisFinished, blnRecordLock)

    '            If IsNothing(objfrmMsg) = True Then
    '                Exit Sub
    '            End If
    '            '''''''''''Code is Added by Anil on 20071103
    '            sortOrder = CType(grdMessages.DataSource, DataView).Sort
    '            strSearchstring = txtSearch.Text.Trim
    '            arrcolumnsort = Split(sortOrder, "]")
    '            If arrcolumnsort.Length > 1 Then
    '                strcolumnName = arrcolumnsort.GetValue(0)
    '                strsortorder = arrcolumnsort.GetValue(1)
    '            End If
    '            ''''''''''''''''''''''
    '            With objfrmMsg
    '                .Text = "Modify Messages"
    '                .myCaller = Me
    '                .IsModify = True
    '                .MdiParent = Me.ParentForm
    '                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '                .WindowState = FormWindowState.Maximized
    '                .BringToFront()
    '                .Show()
    '            End With
    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.View, "Patient Message viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    '            'objfrmMsg.ShowDialog(Me)
    '            'objfrmMsg.BringToFront()
    '            'If objfrmMsg.CancelClick = False Then
    '            'grdMessages.DataSource = objMessages.GetAllMessagesList(m_UserID)  ' to View all Messages
    '            'End If

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

    '    Finally
    '        ' objfrmMsg = Nothing
    '    End Try
    'End Sub

    Private Sub UpdateMessages()
        Dim MsgID As Long
        Dim MsgDate As Date
        '  Dim PatientID As Long

        Dim objfrmMsg As frmMessages

        Try
            _blnAdd = False
            If c1Messages.Rows.Count > 1 Then
                _PatientID = Convert.ToInt64(c1Messages.Item(c1Messages.RowSel, Col_PatientID))       ''grdMessages.Item(grdMessages.CurrentRowIndex, 2)


                If MainMenu.IsAccess(False, _PatientID, , True) = False Then
                    Exit Sub
                End If

                '' 20100728 : Statement Added by Mahesh to set Selected patient on Messages globally.
                CType(Me.ParentForm, MainMenu).ShowDefaultPatientDetails(_PatientID)
                ''

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                'If CheckPatientStatus(_PatientID, grdMessages.Item(grdMessages.CurrentRowIndex, 2).ToString) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, CType(c1Messages.Item(c1Messages.RowSel, Col_PatientID), Long)) = False Then
                    Exit Sub
                End If
                '''''<><><><><> Check Patient Status <><><><><><>''''

                '******Shweta 20090828 *********'
                'To check exeception related to word
                If CheckWordForException() = False Then
                    Exit Sub
                End If
                'End Shweta

                Dim blnisFinished As Boolean = False

                If c1Messages.Item(c1Messages.RowSel, Col_Template) = "Yes" Then
                    blnisFinished = True
                Else
                    blnisFinished = False
                End If

                blnModify = True
                MsgID = c1Messages.Item(c1Messages.RowSel, Col_MessageID).ToString
                MsgDate = c1Messages.Item(c1Messages.RowSel, Col_MessageDate)


                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                Dim blnRecordLock As Boolean = False

                Dim mydt As mytable ''slr new not needed 
                If gblnRecordLocking = True Then
                    mydt = Scan_n_Lock_Transaction(TrnType.Messages, MsgID, 0, MsgDate)
                    If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                        If MessageBox.Show("This message is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
                            ''Record open only for view.
                            blnRecordLock = True
                            '' Word document is only view can't add data to documents.
                            blnisFinished = True
                        Else
                            'Return False
                            ''slr free mydt
                            If Not IsNothing(mydt) Then
                                mydt.Dispose()
                                mydt = Nothing
                            End If

                            Exit Sub
                        End If
                    End If
                    ''slr free mydt
                    If Not IsNothing(mydt) Then
                        mydt.Dispose()
                        mydt = Nothing
                    End If
                End If
                '''' <><><> Record Level Locking <><><><> 

                objfrmMsg = frmMessages.GetInstance(MsgID, m_UserID, MsgDate, _PatientID, 0, blnisFinished, blnRecordLock)

                If IsNothing(objfrmMsg) = True Then
                    Exit Sub
                End If
                '''''''''''Code is Added by Anil on 20071103
                Dim myDataView As DataView = CType(c1Messages.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    sortOrder = myDataView.Sort
                    strSearchstring = txtSearch.Text.Trim
                    arrcolumnsort = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                End If
                ''''''''''''''''''''''
                With objfrmMsg
                    .Text = "Modify Messages"
                    .myCaller = Me
                    .IsModify = True
                    .MdiParent = Me.ParentForm
                    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                    .Show()
                End With
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.View, "Patient Message viewed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                'objfrmMsg.ShowDialog(Me)
                'objfrmMsg.BringToFront()
                'If objfrmMsg.CancelClick = False Then
                'grdMessages.DataSource = objMessages.GetAllMessagesList(m_UserID)  ' to View all Messages
                'End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

        Finally
            ' objfrmMsg = Nothing
        End Try
    End Sub

    'Public Sub RefreshMessages2(ByVal MessageID As Long)

    '    Dim dv As DataView
    '    Dim dtMessages As DataTable ''slr new not needed 
    '    'Dim dtMessages As DataTable = objMessages.GetAllMessagesList("F", m_UserID)
    '    If rbSentMessage.Checked = True Then
    '        dtMessages = objMessages.GetAllMessagesList("F", m_UserID)
    '    Else
    '        dtMessages = objMessages.GetAllMessagesList("T", m_UserID)
    '    End If
    '    dv = dtMessages.DefaultView
    '    grdMessages.Enabled = False
    '    grdMessages.DataSource = dv
    '    grdMessages.Enabled = True
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

    '        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
    '    Else
    '        CustomGridStyle()
    '    End If
    '    If MessageID <> 0 Then
    '        grdMessages.UnSelect(0)
    '    End If
    '    ''''''''''''''''''''''
    '    Dim i As Integer
    '    For i = 0 To dv.Table.Rows.Count - 1
    '        '' when ID Found select that matching Row
    '        If MessageID = grdMessages.Item(i, 0) Then
    '            grdMessages.CurrentRowIndex = i
    '            grdMessages.Select(i)
    '            Exit For
    '        End If
    '    Next

    'End Sub
    Public Sub RefreshMessages(ByVal MessageID As Long)

        Dim dv As DataView = Nothing
        Dim dtMessages As DataTable ''slr new not needed 
        'Dim dtMessages As DataTable = objMessages.GetAllMessagesList("F", m_UserID)
        If rbSentMessage.Checked = True Then
            dtMessages = objMessages.GetAllMessagesList("F", m_UserID)
        Else
            dtMessages = objMessages.GetAllMessagesList("T", m_UserID)
        End If
        If (IsNothing(dtMessages) = False) Then
            dv = dtMessages.Copy().DefaultView
            dtMessages.Dispose()
            dtMessages = Nothing
        Else
            dv = New DataView()
        End If


        c1Messages.Enabled = False
        c1Messages.DataSource = dv
        c1Messages.Enabled = True
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

            SetGridStyle(dtMessages, strcolumnName, strsortorder, strSearchstring)
        Else
            SetGridStyle()
        End If
        If MessageID = 0 Then
            If c1Messages.Rows.Count > 1 Then
                c1Messages.Select(1, 0)
            End If
        End If
        ''''''''''''''''''''''
        Dim i As Integer
        For i = 1 To c1Messages.Rows.Count - 1
            '' when ID Found select that matching Row
            If MessageID = Convert.ToInt64(c1Messages.GetData(i, 0)) Then
                c1Messages.Select(i, 0)

                Exit For
            End If
        Next

    End Sub

    'Public Sub RefreshMessages(ByVal MessageID As Long)
    '    grdMessages.DataSource = objMessages.GetAllMessages("F", m_UserID)
    '    CustomGridStyle()

    '    Dim i As Integer
    '    For i = 0 To CType(grdMessages.DataSource, DataView).Table.Rows.Count - 1
    '        '''' when ID Found select that matching Row
    '        If MessageID = grdMessages.Item(i, 0) Then
    '            grdMessages.CurrentRowIndex = i
    '            grdMessages.Select(i)
    '            Exit For
    '        End If
    '    Next
    'End Sub

    'Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
    '    Dim dt As DataTable
    '    dt = objMessages.GetDataTable
    '    Dim ts As New clsDataGridTableStyle(dt.TableName())
    '    Dim dv As DataView
    '    dv = dt.DefaultView
    '    Dim IDCol As New DataGridTextBoxColumn
    '    IDCol.Width = 0
    '    IDCol.MappingName = dt.Columns("nMessageID").ColumnName
    '    IDCol.HeaderText = "Message ID"


    '    Dim MsgDateCol As New DataGridTextBoxColumn
    '    If rbSentMessage.Checked = True Then                                    ''Dhruv 20100128 Checked which is setted (Sent/recieved)
    '        With MsgDateCol
    '            .Width = 0.12 * grdMessages.Width
    '            .MappingName = dt.Columns("dtMsgDate").ColumnName               ''Sent Means "F"
    '            .HeaderText = "Date"
    '            .NullText = ""
    '        End With
    '    Else                                                                    ''---------------------------------
    '        With MsgDateCol
    '            .Width = 0.12 * grdMessages.Width
    '            .MappingName = dt.Columns("dtMessage").ColumnName               ''Recieved Means "T"
    '            .HeaderText = "Date"
    '            .NullText = ""
    '        End With
    '    End If                                                                  ''Dhruv----------------------------------End

    '    Dim PatCodeCol As New DataGridTextBoxColumn
    '    With PatCodeCol
    '        .Width = 0 * grdMessages.Width
    '        .MappingName = dt.Columns("PatientID").ColumnName
    '        .HeaderText = "Patient ID"
    '        .NullText = ""
    '    End With

    '    Dim FirstNameCol As New DataGridTextBoxColumn
    '    With FirstNameCol
    '        .Width = 0.2 * grdMessages.Width
    '        .MappingName = dt.Columns("PatientName").ColumnName
    '        .HeaderText = "Patient Name"
    '        .NullText = ""
    '    End With

    '    Dim Subject As New DataGridTextBoxColumn  ''added for 8022 message PRD changes
    '    With Subject
    '        .Width = 0.319 * grdMessages.Width
    '        .MappingName = dt.Columns("Subject").ColumnName
    '        .HeaderText = "Subject"
    '        .NullText = ""
    '    End With


    '    Dim Template As New DataGridTextBoxColumn
    '    With Template
    '        .Width = 0.22 * grdMessages.Width
    '        .MappingName = dt.Columns("sTemplateName").ColumnName
    '        .HeaderText = "Template"
    '        .NullText = ""
    '    End With

    '    Dim Finished As New DataGridTextBoxColumn

    '    With Finished
    '        .Width = 0.07 * grdMessages.Width
    '        .MappingName = dt.Columns("Finished").ColumnName
    '        .HeaderText = "Finished"
    '        .NullText = ""
    '    End With


    '    Dim Priority As New DataGridTextBoxColumn
    '    With Priority
    '        .Width = 0.07 * grdMessages.Width
    '        .MappingName = dt.Columns("Priority").ColumnName
    '        .HeaderText = "Priority"
    '        .NullText = ""
    '    End With



    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, MsgDateCol, PatCodeCol, FirstNameCol, Subject, Template, Finished, Priority})
    '    grdMessages.TableStyles.Clear()
    '    grdMessages.TableStyles.Add(ts)

    '    '''''''Code is added by Anil on 20071103
    '    txtSearch.Text = ""
    '    txtSearch.Text = strsearchtxt
    '    If strcolumnName = "" Or IsNothing(strcolumnName) Then
    '        If (strsortorder = "") Or IsNothing(strsortorder) Then
    '            strsortorder = " DESC"
    '            dv.Sort = "[" & dt.Columns(1).ColumnName & "]" & strsortorder
    '            strsortorder = ""
    '        Else
    '            dv.Sort = "[" & dt.Columns(1).ColumnName & "]" & strsortorder
    '        End If
    '    Else
    '        Dim strColumn As String = Replace(strcolumnName, "[", "")
    '        dv.Sort = "[" & strColumn & "]" & strSortBy
    '    End If
    '    ''''''''''''''''''''''''''''''''
    '    'Dim ptPoint As Point = New Point(e.X, e.Y)
    '    'Dim htInfo As DataGrid.HitTestInfo = grdMessages.HitTest(ptPoint)

    '    If (dt.Rows.Count >= 1) Then

    '        grdMessages.Select(0)
    '    End If

    'End Sub





    Private Sub SetGridStyle(Optional ByVal _dt As DataTable = Nothing, Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")

        Try
            ' _isFormLoad = True
            'Dim _strComplaints As String
            'Dim _Comments() As String
            'Dim _nCommentCount As Integer
            With c1Messages
                .AllowSorting = True

                ' Dim i As Int16
                ' .Redraw = False
                ' .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = Me.Width
                c1Messages.Width = _TotalWidth
                c1Messages.Height = Me.Height - 20
                c1Messages.ShowCellLabels = False
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing

                .Cols.Count = Col_Count
                '.Rows.Count = 8
                .Rows.Fixed = 1


                .Styles.ClearUnused()

                .AllowResizing = True

                Dim dt As DataTable
                If IsNothing(_dt) Then
                    dt = objMessages.GetDataTable
                Else
                    dt = _dt
                End If

                '07-Oct-14 Aniket: Cannot dispose the below datatable as it contains data
                'If (IsNothing(dt) = False) Then
                '    dt = New DataTable()
                'End If

                Dim dv As DataView
                dv = dt.DefaultView



                txtSearch.Text = strsearchtxt
                If IsNothing(strcolumnName) OrElse strcolumnName = "" Then
                    If IsNothing(strsortorder) OrElse (strsortorder = "") Then
                        strsortorder = " DESC"
                        dv.Sort = "[" & dt.Columns(1).ColumnName & "]" & strsortorder
                        strsortorder = ""
                    Else
                        dv.Sort = "[" & dt.Columns(1).ColumnName & "]" & strsortorder
                    End If
                Else
                    Dim strColumn As String = Replace(strcolumnName, "[", "")
                    dv.Sort = "[" & strColumn & "]" & strSortBy
                End If


                c1Messages.DataSource = dv



                .Cols(Col_MessageID).Width = _TotalWidth * 0
                .Cols(Col_MessageID).AllowEditing = False
                .Cols(Col_MessageID).Visible = False
                .Cols(Col_MessageID).Caption = "Message ID"
                .Cols(Col_MessageID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_MessageDate).Width = _TotalWidth * 0.118
                .Cols(Col_MessageDate).AllowEditing = False
                .Cols(Col_MessageDate).Visible = True
                '.SetData(0, Col_MessageDate, "Date")
                .Cols(Col_MessageDate).Caption = "Date"
                .Cols(Col_MessageDate).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_MessageDate).DataType = GetType(System.DateTime)
                .Cols(Col_MessageDate).Format = "MM/dd/yyyy h:mm tt"
                .Cols(Col_PatientID).Width = _TotalWidth * 0
                .Cols(Col_PatientID).AllowEditing = False
                .Cols(Col_PatientID).Visible = False
                ''.SetData(0, Col_PatientID, "PatientID")
                .Cols(Col_PatientID).Caption = "PatientID"
                .Cols(Col_PatientID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(Col_PatientName).Width = _TotalWidth * 0.2
                .Cols(Col_PatientName).AllowEditing = False
                .Cols(Col_PatientName).Visible = True
                '.SetData(0, Col_PatientName, "Patient Name")
                .Cols(Col_PatientName).Caption = "Patient Name"
                .Cols(Col_PatientName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                ' .Cols(Col_PatientName).AllowSorting = True

                .Cols(Col_Subject).Width = _TotalWidth * 0.319
                .Cols(Col_Subject).AllowEditing = False
                .Cols(Col_Subject).Visible = True
                ' .SetData(0, Col_Subject, "Subject")
                .Cols(Col_Subject).Caption = "Subject"
                .Cols(Col_Subject).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                ' .Cols(Col_Subject).AllowSorting = True

                .Cols(Col_Template).Width = _TotalWidth * 0.22
                .Cols(Col_Template).AllowEditing = False
                .Cols(Col_Template).Visible = True
                ' .SetData(0, Col_Template, "Template")
                .Cols(Col_Template).Caption = "Template"
                .Cols(Col_Template).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_Template).AllowSorting = True

                .Cols(Col_Finished).Width = _TotalWidth * 0.065
                .Cols(Col_Finished).AllowEditing = False
                .Cols(Col_Finished).Visible = True
                ' .SetData(0, Col_Finished, "Finished")
                .Cols(Col_Finished).Caption = "Finished"
                .Cols(Col_Finished).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                '.Cols(Col_Finished).AllowSorting = True

                .Cols(Col_Priority).Width = _TotalWidth * 0.07
                .Cols(Col_Priority).AllowEditing = False
                .Cols(Col_Priority).Visible = True
                ' .SetData(0, Col_Priority, "Priority")
                .Cols(Col_Priority).Caption = "Priority"
                .Cols(Col_Priority).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                '.Cols(Col_Priority).AllowSorting = True



                ' .Redraw = True
                '  c1ProblemList.SelectionMode = SelectionModeEnum.Row  'swaraj 20100629
                ' .Cols(Col_MessageDate).AllowSorting = True
                ' .Cols(Col_Template).AllowSorting = True
                '.Cols(Col_Priority).AllowSorting = True
                ' .Cols(Col_Finished).AllowSorting = True

            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally

        End Try

    End Sub






    'Private Sub grdMessages_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMessages.CurrentCellChanged
    '    '''''Commented by Anil on 20071103
    '    ''Try
    '    ''    Select Case grdMessages.CurrentCell.ColumnNumber
    '    ''        Case 2
    '    ''            lblSearch.Text = "Patient ID"
    '    ''        Case 3
    '    ''            lblSearch.Text = "Patient Name"
    '    ''        Case 4
    '    ''            lblSearch.Text = "Message"
    '    ''        Case 5
    '    ''            lblSearch.Text = "Finished"
    '    ''    End Select
    '    ''Catch ex As Exception
    '    ''    'MessageBox.Show(ex.ToString, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    ''End Try
    'End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dv As DataView
                Dim dtmsg As DataTable

                dv = CType(c1Messages.DataSource, DataView)
                If IsNothing(dv) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                c1Messages.DataSource = dv
                dtmsg = dv.Table

                Dim strPatientSearchDetails As String
                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If
                'Shubhangi 20091007
                'General & Instring Search
                dtmsg.DefaultView.RowFilter = dtmsg.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                    & dtmsg.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                    & dtmsg.Columns(5).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                    & dtmsg.Columns(7).ColumnName & " Like '%" & strPatientSearchDetails & "%' "



                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    'Private Sub grdMessages_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdMessages.MouseUp
    '    'If grdMessages.CurrentRowIndex >= 0 Then
    '    '    grdMessages.Select(grdMessages.CurrentRowIndex)
    '    'End If
    '    Try
    '        Dim ptPoint As Point = New Point(e.X, e.Y)
    '        Dim htInfo As DataGrid.HitTestInfo = grdMessages.HitTest(ptPoint)
    '        If htInfo.Type = DataGrid.HitTestType.Cell Then
    '            grdMessages.Select(htInfo.Row)
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    Private Sub frmVWMessages_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            'If CType(Me.MdiParent, MainMenu).pnlLeft.Visible = False Then
            '    CType(Me.MdiParent, MainMenu).Splitter1.Visible = False
            'End If
            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmVWMessages_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Form closed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Form closed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        Catch ex As Exception

        End Try
    End Sub

    '''''''''''code/event is added by Anil on 20071103
    'Private Sub grdMessages_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    '    Try
    '        Dim ptPoint As Point = New Point(e.X, e.Y)
    '        Dim htInfo As DataGrid.HitTestInfo = grdMessages.HitTest(ptPoint)
    '        If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

    '            'Commented by Shubhangi 20091007
    '            'Use General Search
    '            'Select Case htInfo.Column
    '            '    Case 2
    '            '        lblSearch.Text = "Patient ID"
    '            '        ColumnHeader = "Patient ID"
    '            '    Case 3
    '            '        lblSearch.Text = "Patient Name"
    '            '        ColumnHeader = "Patient Name"
    '            '    Case 4
    '            '        lblSearch.Text = "Message"
    '            '        ColumnHeader = "Message"
    '            '    Case 5
    '            '        lblSearch.Text = "Finished"
    '            '        ColumnHeader = "Finished"
    '            'End Select


    '            If txtSearch.Text = "" Then
    '                _blnSearch = True
    '            Else
    '                _blnSearch = False
    '                txtSearch.Text = ""
    '                _blnSearch = True
    '            End If

    '        ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then

    '            _blnSearch = True
    '            UpdateMessages()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    ' ''''''''''''''''''''''''''''''''''''
    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007
    Private Sub AddCategory()
        'Dim st As Long
        'Dim ed As Long
        'Dim tt As Long
        'st = DateTime.Now.Ticks

        Try

            '''''<><><><><> Check Patient Status <><><><><><>''''
            ''''' 20070125 -Mahesh 
            'If CheckPatientStatus(Default_PatientID) = False Then
            '    Exit Sub
            'End If
            If MainMenu.IsAccess(False, Default_PatientID) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''

            '' SUDHIR 20090521 '' CHECK PROVIDER ''
            If gblnProviderDisable = True Then
                If ShowAssociateProvider(Default_PatientID, Me) = True Then
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
            Dim dtWord As DataTable = Nothing  ''added for bugid 70606 showing message if no template exist
            Dim objWord As New clsWordDocument
            dtWord = objWord.FillTemplates(enumTemplateFlag.Messages)
            objWord = Nothing
            If (IsNothing(dtWord) = False) Then
                If dtWord.Rows.Count = 0 Then
                    ''''If not present then exit from sub
                    MessageBox.Show("No template is associated for messages. Associate any template first.", "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'If Not IsNothing(dtWord) Then
                    '    dtWord.Dispose()
                    '    dtWord = Nothing
                    'End If
                    Exit Sub
                End If
            End If
            

            'Dim objfrmMsg As New frmMessages(0, m_UserID, Now)
            Dim objfrmMsg As frmMessages
            objfrmMsg = frmMessages.GetInstance(0, m_UserID, Now, Default_PatientID)

            If IsNothing(objfrmMsg) = True Then
                Exit Sub
            End If
            _blnAdd = True
            With objfrmMsg
                blnModify = False
                .Text = "New Messages"
                .dtTemplates = dtWord.Copy
                .MdiParent = Me.MdiParent
                .myCaller = Me
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
                .WindowState = FormWindowState.Maximized
                .BringToFront() ''added against bugid 78219
                .Show()
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)


            End With
            If Not IsNothing(dtWord) Then
                dtWord.Dispose()
                dtWord = Nothing
            End If
            'objfrmMsg.ShowDialog(Me)

            'If objfrmMsg.CancelClick = False Then
            '    grdMessages.DataSource = objMessages.GetAllMessages(m_UserID) ' to View all Messages
            'End If
            'ed = DateTime.Now.Ticks
            'tt = (ed - st) / 1000000
            'MsgBox(tt.ToString)
            objfrmMsg = Nothing
        Catch ex As Exception
            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub
    Private Sub UpdateCategory()
        Try

            'If grdMessages.VisibleRowCount >= 1 Then

            '    If grdMessages.IsSelected(grdMessages.CurrentRowIndex) Then
            '        Call UpdateMessages()
            '    End If
            'End If
            If c1Messages.Rows.Count > 1 Then
                If c1Messages.RowSel >= 1 Then
                    Call UpdateMessages()
                End If
            End If

        Catch ex As Exception

        End Try

        ''Dim MsgID As Long
        ''Dim objfrmMsg As frmMessages
        ''Try
        ''    If grdMessages.VisibleRowCount >= 1 Then
        ''        Dim blnisFinished As Boolean = False

        ''        If grdMessages.Item(grdMessages.CurrentRowIndex, 5) = "Yes" Then
        ''            blnisFinished = True
        ''        Else
        ''            blnisFinished = False
        ''        End If

        ''        blnModify = True
        ''        MsgID = grdMessages.Item(grdMessages.CurrentRowIndex, 0).ToString
        ''        objfrmMsg = New frmMessages(MsgID, m_UserID, 0, blnisFinished)
        ''        With objfrmMsg
        ''            .Text = "Modify Messages"
        ''            .MdiParent = Me.ParentForm
        ''            .myCaller = Me
        ''            .Show()
        ''        End With

        ''        'objfrmMsg.ShowDialog(Me)
        ''        'objfrmMsg.BringToFront()
        ''        'If objfrmMsg.CancelClick = False Then
        ''        'grdMessages.DataSource = objMessages.GetAllMessages(m_UserID)  ' to View all Messages
        ''        'End If
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, "Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    objfrmMsg = Nothing
        ''End Try
    End Sub
    'Private Sub DeleteCategory1()
    '    Dim MsgID As Long
    '    Dim MsgDate As String
    '    Dim strcolumnName As String = Nothing
    '    Dim strsortorder As String = Nothing
    '    Try

    '        If grdMessages.VisibleRowCount >= 1 Then
    '            If grdMessages.IsSelected(grdMessages.CurrentRowIndex) = False Then
    '                Exit Sub
    '            End If
    '            ''<><><><><> Check Patient Status <><><><><><>''
    '            ''''' 20070125 -Mahesh 
    '            'If CheckPatientStatus(0, grdMessages.Item(grdMessages.CurrentRowIndex, 2).ToString) = False Then
    '            '    Exit Sub
    '            'End If
    '            If MainMenu.IsAccess(False, CType(grdMessages.Item(grdMessages.CurrentRowIndex, 2), Long)) = False Then
    '                Exit Sub
    '            End If
    '            If grdMessages.IsSelected(grdMessages.CurrentRowIndex) = False Then
    '                Exit Sub
    '            End If
    '            ''<><><><><> Check Patient Status <><><><><><>''

    '            'blnModify = True
    '            If grdMessages.Item(grdMessages.CurrentRowIndex, 5) = "Yes" Then
    '                MessageBox.Show("The status of message is finished, you cannot delete this message", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Exit Sub
    '            End If

    '            MsgID = grdMessages.Item(grdMessages.CurrentRowIndex, 0).ToString
    '            MsgDate = grdMessages.Item(grdMessages.CurrentRowIndex, 1).ToString
    '            _PatientID = grdMessages.Item(grdMessages.CurrentRowIndex, 2)
    '            ' '' <><><> Record Level Locking <><><><> 
    '            ' '' Mahesh - 20070718 
    '            Dim blnRecordLock As Boolean = False
    '            Dim mydt As mytable ''slr new not needed 
    '            mydt = Scan_n_Lock_Transaction(TrnType.Messages, MsgID, 0, MsgDate)
    '            If mydt.Code <> gstrLoginName Or mydt.Description <> gstrClientMachineName Then
    '                MessageBox.Show("This message is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                blnRecordLock = True
    '                'Return False
    '                ''slr free mydt
    '                If Not IsNothing(mydt) Then
    '                    mydt.Dispose()
    '                    mydt = Nothing
    '                End If
    '                Exit Sub
    '            End If
    '            ''slr free mydt
    '            If Not IsNothing(mydt) Then
    '                mydt.Dispose()
    '                mydt = Nothing
    '            End If
    '            '''' <><><> Record Level Locking <><><><> 

    '            If MessageBox.Show("Do you want to delete selected Patient Message?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
    '                objMessages.DeleteMessages(MsgID, MsgDate, _PatientID)
    '                isMessageDeleted = True
    '                If rbSentMessage.Checked = True Then
    '                    grdMessages.DataSource = objMessages.GetAllMessagesList("F", m_UserID).DefaultView              ''Dhruv 20100128 Checked which is setted (Sent/recieved)
    '                Else                                                                                            ''Sent Means "F"
    '                    grdMessages.DataSource = objMessages.GetAllMessagesList("T", m_UserID).DefaultView              ''Recieved Means "T"
    '                End If                                                                                           ''Dhruv---------------------------------------------End
    '                ''Call RefreshMessages()
    '                '''''''''''Code is Added by Anil on 20071103
    '                sortOrder = CType(grdMessages.DataSource, DataView).Sort
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
        Dim MsgID As Long
        Dim MsgDate As String
        Dim strcolumnName As String = Nothing
        Dim strsortorder As String = Nothing
        Try

            If c1Messages.Rows.Count > 1 Then
                If c1Messages.RowSel < 1 Then
                    Exit Sub
                End If

                If MainMenu.IsAccess(False, CType(c1Messages.Item(c1Messages.RowSel, 2), Long)) = False Then
                    Exit Sub
                End If
                'If grdMessages.IsSelected(grdMessages.CurrentRowIndex) = False Then    to check 
                '    Exit Sub
                'End If
                ''<><><><><> Check Patient Status <><><><><><>''

                'blnModify = True
                If c1Messages.Item(c1Messages.RowSel, "Finished") = "Yes" Then
                    MessageBox.Show("The status of message is finished, you cannot delete this message", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                MsgID = c1Messages.Item(c1Messages.RowSel, 0).ToString
                MsgDate = c1Messages.Item(c1Messages.RowSel, 1).ToString
                _PatientID = c1Messages.Item(c1Messages.RowSel, 2)
                ' '' <><><> Record Level Locking <><><><> 
                ' '' Mahesh - 20070718 
                Dim blnRecordLock As Boolean = False
                Dim mydt As mytable ''slr new not needed 
                mydt = Scan_n_Lock_Transaction(TrnType.Messages, MsgID, 0, MsgDate)
                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    MessageBox.Show("This message is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot delete it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    blnRecordLock = True
                    'Return False
                    ''slr free mydt
                    If Not IsNothing(mydt) Then
                        mydt.Dispose()
                        mydt = Nothing
                    End If
                    Exit Sub
                End If
                ''slr free mydt
                If Not IsNothing(mydt) Then
                    mydt.Dispose()
                    mydt = Nothing
                End If
                '''' <><><> Record Level Locking <><><><> 

                If MessageBox.Show("Do you want to delete selected Patient Message?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    objMessages.DeleteMessages(MsgID, MsgDate, _PatientID)
                    isMessageDeleted = True

                    Dim _dv As DataView = CType(c1Messages.DataSource, DataView)
                    If (IsNothing(_dv) = False) Then


                        Dim _dt As DataTable = _dv.ToTable()
                        _dt.Rows.RemoveAt((c1Messages.RowSel - 1))
                        '  Dim dt As DataTable = Nothing
                        'If rbSentMessage.Checked = True Then
                        '    dt = objMessages.GetAllMessagesList("F", m_UserID)           ''Dhruv 20100128 Checked which is setted (Sent/recieved)
                        'Else                                                                                            ''Sent Means "F"
                        '    dt = objMessages.GetAllMessagesList("T", m_UserID)             ''Recieved Means "T"
                        'End If                                                                                           ''Dhruv---------------------------------------------End
                        Dim myDataView As DataView = CType(c1Messages.DataSource, DataView)
                        If (IsNothing(myDataView) = False) Then


                            sortOrder = myDataView.Sort
                            strSearchstring = txtSearch.Text.Trim
                            arrcolumnsort = Split(sortOrder, "]")
                            If arrcolumnsort.Length > 1 Then
                                strcolumnName = arrcolumnsort.GetValue(0)
                                strsortorder = arrcolumnsort.GetValue(1)
                            End If
                        End If
                        ' CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        ''''''''''''''''''
                        SetGridStyle(_dt, strcolumnName, strsortorder, strSearchstring)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshCategory()
        Try
            Dim dv As DataView
            Dim dt As DataTable
            c1Messages.Enabled = False
            If rbSentMessage.Checked = True Then                                    ''Dhruv 20100128 Checked which is setted (Sent/recieved)
                dt = objMessages.GetAllMessagesList("F", m_UserID)                      ''Sent Means "F"
            Else
                dt = objMessages.GetAllMessagesList("T", m_UserID)                      ''Recieved Means "T"
            End If                                                                  ''Dhruv---------------------------------------------End

            If (IsNothing(dt) = False) Then
                dv = dt.Copy().DefaultView
                dt.Dispose()
                dt = Nothing
            Else
                dv = New DataView()
            End If
            
            c1Messages.DataSource = dv
            c1Messages.Enabled = True
            'CustomGridStyle()
            SetGridStyle(dt)
            txtSearch.Text = ""
            _blnSearch = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Try
            'Dim frm As MainMenu
            'frm = Me.MdiParent
            'frm.Fill_Messages()
            Me.Close()
        Catch ex As Exception
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

        ResetSearchText()
    End Sub

    Private Sub frmVWMessages_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        SetGridStyle()
    End Sub

#Region "Checkbox operation"
    ''' <summary>
    ''' Dhruv 20100128 -------------------------------------------------------------------------------------------------------------------------
    ''' Recieved message
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub rbRecievedMessage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbRecievedMessage.CheckedChanged
        If rbRecievedMessage.Checked Then                                       'checking the radio button is checked or not
            rbRecievedMessage.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)      'setting the font style
            ResetSearchText()                                                   'Resetting the text box    
            ShowMessages(True)                                                  'boolean function sending the message as true
        Else
            rbRecievedMessage.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)   'setting the font style
        End If
    End Sub

    '16-Nov-15 Aniket: 8070 Refresh Tasks/Messages when different user logs in after lock screen
    Public Sub RefreshMessages()
        If rbSentMessage.Checked = True Then
            ShowMessages(False)
        Else
            ShowMessages(True)
        End If
    End Sub


    Private Sub rbSentMessage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSentMessage.CheckedChanged
        If rbSentMessage.Checked Then                                           'checking the radio button is checked or not
            rbSentMessage.Font = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)          'setting the font style
            ShowMessages(False)                                                  'boolean function sending the message as true
            ResetSearchText()                                                    'Resetting the text box  

        Else
            rbSentMessage.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)       'setting the font style
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="isReceived"></param>
    ''' <remarks></remarks>
    Public Sub ShowMessages(ByVal isReceived As Boolean)
        Try
            Dim dt As DataTable
            Dim dv As DataView
            m_UserID = objMessages.GetUserID(gstrLoginName)             'taking the user id 
            If isReceived Then
                dt = objMessages.GetAllMessagesList("T", m_UserID)          'Recieved Messages as "F"
            Else
                dt = objMessages.GetAllMessagesList("F", m_UserID)          'Sent Messages as "T"
            End If
            If (IsNothing(dt) = False) Then
                dv = dt.Copy().DefaultView
                dt.Dispose()
                dt = Nothing
            Else
                dv = New DataView()
            End If
            c1Messages.Enabled = False

            c1Messages.DataSource = dv
            c1Messages.Enabled = True
            '  CustomGridStyle()                                           'Setting the grid style
            SetGridStyle(dt)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''' <summary>
    ''' Resetting the testbox
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResetSearchText()
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
    '----------------------------------------------------------------------------------------------------------------------------
#End Region




    Private Sub grdMessages_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)

        'Dim myHitTest As DataGrid.HitTestInfo  ''added to show tooltip 
        'myHitTest = grdMessages.HitTest(e.X, e.Y)
        'If myHitTest.Row > -1 And myHitTest.Column > -1 Then
        '    If (myHitTest.Type = DataGrid.HitTestType.Cell) Then
        '        Dim tooltiptext As String = Convert.ToString(grdMessages.Item(myHitTest.Row, myHitTest.Column).ToString().Trim())

        '        If Prevtooltiptext <> tooltiptext And tooltiptext.Length > 40 Then

        '            If Not IsNothing(grdmsgTooltip) Then
        '                grdmsgTooltip.Dispose()
        '                grdmsgTooltip = Nothing
        '            End If
        '            grdmsgTooltip = New ToolTip()

        '            grdmsgTooltip.RemoveAll()
        '            grdmsgTooltip.ShowAlways = False
        '            grdmsgTooltip.Show(tooltiptext, Me, e.X, e.Y + 98, 2000)
        '            Prevtooltiptext = tooltiptext


        '    Else
        '        Prevtooltiptext = ""

        '    End If
        '        Else

        '            Prevtooltiptext = ""
        '        End If
        '    End If
    End Sub

    Private Sub c1Messages_AfterSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1Messages.AfterSort ''added for Sorting Issue Bug #72075

        Try


            If (Not IsNothing(c1Messages.DataSource) AndAlso (c1Messages.Rows.Count > 0)) Then
                If (_SelMsgid <> "0") Then
                    Dim dt As DataTable = (CType(c1Messages.DataSource, DataView)).ToTable()
                    Dim dr As DataRow() = dt.Select("nMessageID =  " + _SelMsgid + " ")
                    If (dr.Length > 0) Then
                        Dim ind As Integer = dt.Rows.IndexOf(dr(0))
                        c1Messages.Select(ind + 1, 0)

                    End If
                    dt.Dispose()
                    dt = Nothing
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub c1Messages_BeforeSort(sender As Object, e As C1.Win.C1FlexGrid.SortColEventArgs) Handles c1Messages.BeforeSort  ''added for Sorting Issue Bug #72075
        If (Not IsNothing(c1Messages.DataSource) AndAlso (c1Messages.Rows.Count > 0) AndAlso c1Messages.RowSel > 0) Then
            _SelMsgid = Convert.ToString(c1Messages.GetData(c1Messages.RowSel, Col_MessageID))
        Else
            _SelMsgid = "0"
        End If
    End Sub

    Private Sub c1Messages_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1Messages.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub c1Messages_DoubleClick(sender As System.Object, e As System.EventArgs) Handles c1Messages.DoubleClick
        If (c1Messages.Rows.Count > 0) Then
            If (c1Messages.RowSel > 0) Then
                UpdateMessages()
            End If
        End If
    End Sub
End Class
