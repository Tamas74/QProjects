Public Class frmIM_View
    Inherits System.Windows.Forms.Form

    Private COL_ID As Integer = 0

    Private COL_HOWMANY As Integer = 1
    Private COL_SKU As Integer = 2
    Private COL_Location As Integer = 3
    Private col_Vaccine As Integer = 4
    Private COL_TRADENAME As Integer = 5
    Private COL_Category As Integer = 6
    Private COL_LOtNo As Integer = 7
    Private COL_Mfr As Integer = 8
    Private COL_OnHand As Integer = 9
    Private COL_Status As Integer = 10
    Private COL_Funding As Integer = 11
    Private COL_ExpiryDate As Integer = 12
    Private COL_Comments As Integer = 13

    Dim dtIMDetails As New DataTable
    Dim _isFormLoad As Boolean = False
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim _DefaultLocationID As Long
    Dim _DefaultLocation As String

    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents tls_Duplicate As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_Deactivate As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnClearAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    'line commented and added by dipak 20090910 to increase COL_COUNT to 4
    'Private COL_COUNT As Integer = 4
    Private COL_COUNT As Integer = 14
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Private _isModify As Boolean = False
    'Shubhangi



#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        If appSettings("DefaultLocationID") IsNot Nothing Then
            If appSettings("DefaultLocationID").ToString() <> "" Then
                _DefaultLocationID = Convert.ToInt64(appSettings("DefaultLocationID"))
                _DefaultLocation = Convert.ToString(appSettings("DefaultLocation"))

            End If
        End If
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents C1IMView As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIM_View))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.C1IMView = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.tls_Duplicate = New System.Windows.Forms.ToolStripButton()
        Me.tls_Deactivate = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnSelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnClearAll = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        CType(Me.C1IMView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.C1IMView)
        Me.pnlMain.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlMain.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlMain.Controls.Add(Me.lbl_pnlRight)
        Me.pnlMain.Controls.Add(Me.lbl_pnlTop)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 84)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(927, 544)
        Me.pnlMain.TabIndex = 2
        '
        'C1IMView
        '
        Me.C1IMView.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1IMView.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Columns
        Me.C1IMView.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1IMView.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1IMView.ColumnInfo = resources.GetString("C1IMView.ColumnInfo")
        Me.C1IMView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1IMView.ExtendLastCol = True
        Me.C1IMView.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1IMView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1IMView.Location = New System.Drawing.Point(4, 1)
        Me.C1IMView.Name = "C1IMView"
        Me.C1IMView.Rows.DefaultSize = 19
        Me.C1IMView.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1IMView.Size = New System.Drawing.Size(919, 539)
        Me.C1IMView.StyleInfo = resources.GetString("C1IMView.StyleInfo")
        Me.C1IMView.TabIndex = 0
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 540)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(919, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 540)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(923, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 540)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(921, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(927, 54)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.tls_Duplicate, Me.tls_Deactivate, Me.ts_btnDelete, Me.ts_btnRefresh, Me.tlbbtnSelectAll, Me.tlbbtnClearAll, Me.ts_gloCommunityDownload, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(927, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAdd.Tag = "New"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_Duplicate
        '
        Me.tls_Duplicate.BackColor = System.Drawing.Color.Transparent
        Me.tls_Duplicate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Duplicate.Image = CType(resources.GetObject("tls_Duplicate.Image"), System.Drawing.Image)
        Me.tls_Duplicate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Duplicate.Name = "tls_Duplicate"
        Me.tls_Duplicate.Size = New System.Drawing.Size(68, 50)
        Me.tls_Duplicate.Tag = "Duplicate"
        Me.tls_Duplicate.Text = "&Duplicate"
        Me.tls_Duplicate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_Deactivate
        '
        Me.tls_Deactivate.BackColor = System.Drawing.Color.Transparent
        Me.tls_Deactivate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Deactivate.Image = CType(resources.GetObject("tls_Deactivate.Image"), System.Drawing.Image)
        Me.tls_Deactivate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_Deactivate.Name = "tls_Deactivate"
        Me.tls_Deactivate.Size = New System.Drawing.Size(76, 50)
        Me.tls_Deactivate.Tag = "Deactivate"
        Me.tls_Deactivate.Text = "&Deactivate"
        Me.tls_Deactivate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDelete.Visible = False
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.Visible = False
        '
        'tlbbtnSelectAll
        '
        Me.tlbbtnSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnSelectAll.Image = CType(resources.GetObject("tlbbtnSelectAll.Image"), System.Drawing.Image)
        Me.tlbbtnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnSelectAll.Name = "tlbbtnSelectAll"
        Me.tlbbtnSelectAll.Size = New System.Drawing.Size(67, 50)
        Me.tlbbtnSelectAll.Tag = "SelectAll"
        Me.tlbbtnSelectAll.Text = "&Select All"
        Me.tlbbtnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnSelectAll.Visible = False
        '
        'tlbbtnClearAll
        '
        Me.tlbbtnClearAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClearAll.Image = CType(resources.GetObject("tlbbtnClearAll.Image"), System.Drawing.Image)
        Me.tlbbtnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClearAll.Name = "tlbbtnClearAll"
        Me.tlbbtnClearAll.Size = New System.Drawing.Size(60, 50)
        Me.tlbbtnClearAll.Tag = "ClearAll"
        Me.tlbbtnClearAll.Text = "&Clear All"
        Me.tlbbtnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnClearAll.Visible = False
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
        'ts_btnClose
        '
        Me.ts_btnClose.BackColor = System.Drawing.Color.Transparent
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
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.pnlTopRight)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(927, 30)
        Me.Panel1.TabIndex = 14
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.panel4)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.cmbLocation)
        Me.pnlTopRight.Controls.Add(Me.cmbStatus)
        Me.pnlTopRight.Controls.Add(Me.Label29)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(921, 26)
        Me.pnlTopRight.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(533, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Location :"
        '
        'cmbLocation
        '
        Me.cmbLocation.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(598, 2)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(109, 22)
        Me.cmbLocation.TabIndex = 52
        '
        'cmbStatus
        '
        Me.cmbStatus.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(783, 2)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(109, 22)
        Me.cmbStatus.TabIndex = 51
        '
        'Label29
        '
        Me.Label29.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(729, 5)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(50, 14)
        Me.Label29.TabIndex = 50
        Me.Label29.Text = "Status :"
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
        Me.btnClear.Size = New System.Drawing.Size(21, 24)
        Me.btnClear.TabIndex = 48
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 24)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(919, 1)
        Me.Label2.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 25)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(920, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 25)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(0, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(921, 1)
        Me.Label1.TabIndex = 8
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.txtSearch)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Controls.Add(Me.label21)
        Me.panel4.Controls.Add(Me.label22)
        Me.panel4.Controls.Add(Me.label23)
        Me.panel4.Controls.Add(Me.btnClear)
        Me.panel4.Controls.Add(Me.label24)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.Black
        Me.panel4.Location = New System.Drawing.Point(65, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 24)
        Me.panel4.TabIndex = 47
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.White
        Me.label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label20.Location = New System.Drawing.Point(5, 19)
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
        Me.label22.Size = New System.Drawing.Size(4, 24)
        Me.label22.TabIndex = 38
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.label23.Location = New System.Drawing.Point(0, 0)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(1, 24)
        Me.label23.TabIndex = 39
        Me.label23.Text = "label4"
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.label24.Location = New System.Drawing.Point(240, 0)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(1, 24)
        Me.label24.TabIndex = 40
        Me.label24.Text = "label4"
        '
        'frmIM_View
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(927, 628)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIM_View"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IM Setup [Immunization View]"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        CType(Me.C1IMView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region



    Private Sub RefreshGrid()

        'Commented by Shubhangi B'coz we want to populate IM list by binding datatable to c1Flex Grid
        If (_isFormLoad = False) Then

            'Dim oItemDetails As New gloStream.Immunization.Supporting.ImmunizationItems
            Dim oIM As New gloStream.Immunization.ItemSetup
            ' Dim PatientID As Integer
            Dim dvIM As DataView

            dtIMDetails = oIM.ImmunizationList(cmbStatus.Text.Trim, cmbLocation.Text.Trim)

            dvIM = dtIMDetails.DefaultView

            C1IMView.DataSource = dvIM
            C1IMView.ShowCellLabels = False
            C1IMView.AllowSorting = True
            C1IMView.AllowEditing = False
            C1IMView.Cols(COL_ID).Visible = False
            C1IMView.Cols(COL_HOWMANY).Visible = False

            If C1IMView.Cols.Count = COL_COUNT Then
                Dim _Width As Single
                _Width = C1IMView.Width
                C1IMView.Cols(COL_SKU).Width = _Width * 0.07
                C1IMView.Cols(col_Vaccine).Width = _Width * 0.1
                C1IMView.Cols(COL_TRADENAME).Width = _Width * 0.1
                C1IMView.Cols(COL_Category).Width = _Width * 0.08
                C1IMView.Cols(COL_LOtNo).Width = _Width * 0.05
                C1IMView.Cols(COL_Mfr).Width = _Width * 0.1
                C1IMView.Cols(COL_OnHand).Width = _Width * 0.12
                C1IMView.Cols(COL_Status).Width = _Width * 0.05
                C1IMView.Cols(COL_Status).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                C1IMView.Cols(COL_Funding).Width = _Width * 0.06
                C1IMView.Cols(COL_ExpiryDate).Width = _Width * 0.08
                C1IMView.Cols(COL_Comments).Width = _Width * 0.012
                C1IMView.Cols(COL_OnHand).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            End If

            C1IMView.Cols(COL_OnHand).Caption = "Total Doses in Inventory"

            oIM = Nothing 'obj Disposed by mitesh
        End If
    End Sub

    Private Sub frmIM_View_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''Added for check gloCommunity feature is on as on 20120831
            If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
                ts_gloCommunityDownload.Visible = gblngloCommunity
            End If
            ''End
            gloC1FlexStyle.Style(C1IMView)
            cmbStatus.Items.Add("All")
            cmbStatus.Items.Add("Active")
            cmbStatus.Items.Add("Inactive")
            _isFormLoad = True
            cmbStatus.SelectedIndex = 1

            tlbbtnClearAll.Visible = False
            FillLocation()
            _isFormLoad = False
            RefreshCategory()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            _isFormLoad = False
        End Try
    End Sub

    Private Sub FillLocation()

        Try
            Dim dtVaccineInformation As New DataTable

            cmbLocation.Items.Add("All")
            Dim oclsDM As New gloStream.Immunization.ItemSetup
            dtVaccineInformation = oclsDM.getLocation()
            Dim i As Int16
            If IsNothing(dtVaccineInformation) = False Then
                If dtVaccineInformation.Rows.Count > 0 Then
                    For i = 0 To dtVaccineInformation.Rows.Count - 1
                        cmbLocation.Items.Add(dtVaccineInformation.Rows(i)(1).ToString())
                    Next
                End If
            End If

            cmbLocation.SelectedIndex = cmbLocation.FindStringExact("All")

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

#Region "Operations To Add New Item,Update,Duplicate and Deactivate Status"

    Private Sub NewCategory()
        Dim oIMSetup As New frmIM_Setup
        oIMSetup.MyOwner = Me
        ''
        Dim ReturnId As Long
        If oIMSetup.ShowDialog(IIf(IsNothing(oIMSetup.Parent), Me, oIMSetup.Parent)) = Windows.Forms.DialogResult.OK Then

            ReturnId = oIMSetup._ReturnID
            RefreshCategory()
            Dim nID As Int64
            nID = ReturnId
            RefreshCategory()
            'To refresh list after performing add/update/delete operation
            Dim rowIndex As Int64
            rowIndex = C1IMView.FindRow(nID, 1, COL_ID, False, True, False)
            C1IMView.Select(rowIndex, COL_ID, True)
        End If
        If Not IsNothing(oIMSetup) Then   'obj Disposed by mitesh
            oIMSetup.Dispose()
            oIMSetup = Nothing
        End If
    End Sub

    Private Sub UpdateCategory()
        Dim oIMSetup As New frmIM_Setup
        _isModify = True
        Dim _ID As Long = 0
        With C1IMView



            If .Rows.Count > 1 Then
                If C1IMView.Row > 0 Then


                    _ID = .GetData(.Row, COL_ID)
                    If _ID > 0 Then
                        oIMSetup._EditID = _ID

                        '  oIMSetup._SaveFlag = False
                        If oIMSetup.ShowDialog(IIf(IsNothing(oIMSetup.Parent), Me, oIMSetup.Parent)) = Windows.Forms.DialogResult.OK Then
                            RefreshCategory()
                            txtSearch_TextChanged(Nothing, Nothing)
                            Dim nID As Int64
                            nID = oIMSetup._EditID

                            'To refresh list after performing add/update/delete operation
                            Dim rowIndex As Int64
                            rowIndex = C1IMView.FindRow(nID, 1, COL_ID, False, True, False)
                            C1IMView.Select(rowIndex, COL_ID, True)


                        End If
                    End If
                    If Not IsNothing(oIMSetup) Then
                        oIMSetup.Dispose()
                        oIMSetup = Nothing
                    End If

                End If
            End If

        End With
        'cmbStatus_SelectedIndexChanged(Nothing, Nothing)
        _isModify = False
    End Sub
    Private Sub DuplicateCategory()
        Dim oIMSetup As New frmIM_Setup


        With C1IMView
            If .Rows.Count > 1 Then
                Dim _ID As Long = 0
                If C1IMView.Row > 0 Then


                    _ID = .GetData(.Row, COL_ID)
                    Dim IDSelected As Long
                    If _ID > 0 Then
                        oIMSetup._EditID = _ID


                        oIMSetup._SaveFlag = True
                        If oIMSetup.ShowDialog(IIf(IsNothing(oIMSetup.Parent), Me, oIMSetup.Parent)) = Windows.Forms.DialogResult.OK Then


                            IDSelected = oIMSetup._ReturnID
                            RefreshCategory()
                            Dim nID As Int64
                            nID = oIMSetup._ReturnID
                            RefreshCategory()
                            'To refresh list after performing add/update/delete operation
                            Dim rowIndex As Int64
                            rowIndex = C1IMView.FindRow(nID, 1, COL_ID, False, True, False)
                            C1IMView.Select(rowIndex, COL_ID, True)

                        End If
                    End If
                End If
                If Not IsNothing(oIMSetup) Then   'obj Disposed by mitesh
                    oIMSetup.Dispose()
                    oIMSetup = Nothing
                End If

            End If
        End With
    End Sub
    Private Sub DeactivateStatus()
        Dim oclsDM As New gloStream.Immunization.ItemSetup
        Try

            With C1IMView

                Dim _ID As Long
                If .Rows.Count > 1 Then
                    ' For i As Int16 = 0 To .Rows.Count - 1
                    If C1IMView.Row > 0 Then


                        If .Rows(.Row)(COL_Status) <> "InActive" Then
                            _ID = .GetData(.Row, COL_ID)
                        End If

                        ' Next
                        If _ID > 0 Then


                            If oclsDM.DeactivateStatus(_ID) = True Then

                                Dim nID As Long
                                nID = _ID
                                RefreshCategory()
                                'To refresh list after performing add/update/delete operation
                                Dim rowIndex As Long
                                rowIndex = C1IMView.FindRow(nID, 1, COL_ID, False, True, False)
                                C1IMView.Select(rowIndex, COL_ID, True)
                            End If
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If IsNothing(oclsDM) = False Then
                oclsDM = Nothing
            End If
        End Try

    End Sub
#End Region
    Private Sub DeleteCategory()
        Dim oclsDM As New gloStream.Immunization.ItemSetup
        With C1IMView
            If .Rows.Count > 1 Then
                Dim _ID As Long
                Dim _EditName As String = ""

                _ID = .GetData(.Row, COL_ID)
                If _ID > 0 Then
                    If oclsDM.IsDelete(_EditName) = True Then
                        If MessageBox.Show("Are you sure to remove current record ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                            oclsDM.Delete(_ID, _EditName)
                            RefreshCategory()
                        End If
                    Else
                        MessageBox.Show("You can not delete this criteria because Transaction already made against this criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question)
                    End If
                Else
                    MessageBox.Show("User name does not exist", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End With
        If Not IsNothing(oclsDM) Then   'obj Disposed by mitesh
            oclsDM = Nothing
        End If
    End Sub

    Public Sub RefreshCategory()
        Try
            RefreshGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "New"
                    Call NewCategory()
                    _isModify = True
                    _isModify = False
                Case "Modify"
                    Call UpdateCategory()

                Case "Duplicate"
                    _isModify = True
                    Call DuplicateCategory()
                    _isModify = False

                Case "Deactivate"
                    DeactivateStatus()

                Case "Delete"
                    _isModify = True
                    Call DeleteCategory()
                    _isModify = False

                Case "Refresh"
                    Call RefreshCategory()

                Case "SelectAll"
                    Call SelectClearAll(True)
                    tlbbtnSelectAll.Visible = False
                    tlbbtnClearAll.Visible = True

                Case "ClearAll"
                    Call SelectClearAll(False)
                    tlbbtnSelectAll.Visible = True
                    tlbbtnClearAll.Visible = False

                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        FilterRecord()
    End Sub

    Private Sub FilterRecord()
        Try
            Dim dv As DataView
            'Dim dt As DataTable

            dv = CType(C1IMView.DataSource, DataView)

            If IsNothing(dv) Then
                Exit Sub
            End If

            C1IMView.DataSource = dv
            Dim strsearchArray() As String = Nothing
            Dim strSearch As String = txtSearch.Text.Trim()
            strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "").Replace("/", "")

            Dim sFilter As String = ""
            If strSearch.Trim <> "" Then

                strsearchArray = strSearch.Split(",")

                If strsearchArray.Length = 1 Then
                    strSearch = strsearchArray(0)
                    strSearch = strSearch.Trim()
                    If (strSearch.Length > 1) Then

                        Dim str As String = strSearch.Substring(1).Replace("%", "")
                        strSearch = strSearch.Substring(0, 1) + str
                    End If
                    sFilter += " ( [Trade Name] Like '%" & strSearch & "%' OR SKU Like '%" & strSearch & "%'  OR Location Like '%" & strSearch & "%' OR Vaccine Like '%" & strSearch & "%' OR [Lot#] Like '%" & strSearch & "%' OR [Mfr.] Like '%" & strSearch & "%' OR [On Hand] Like '%" & strSearch & "%' OR Status Like '%" & strSearch & "%' OR Comments Like '%" & strSearch & "%' OR Funding Like '%" & strSearch & "%' OR Category Like '%" & strSearch & "%' ) "

                    dv.RowFilter = sFilter
                Else

                    For i As Int16 = 0 To strsearchArray.Length - 1
                        strSearch = strsearchArray(i)
                        strSearch = strSearch.Trim()
                        If (strSearch.Length > 1) Then

                            Dim str As String = strSearch.Substring(1).Replace("%", "")
                            strSearch = strSearch.Substring(0, 1) + str
                        End If

                        If strSearch.Trim <> "" Then
                            If sFilter = "" Then
                                sFilter = " ( [Trade Name] Like '%" & strSearch & "%' OR SKU Like '%" & strSearch & "%' OR Location Like '%" & strSearch & "%' OR Vaccine Like '%" & strSearch & "%' OR [Lot#] Like '%" & strSearch & "%' OR [Mfr.] Like '" & strSearch & "%' OR [On Hand] Like '%" & strSearch & "%' OR Status Like '%" & strSearch & "%' OR Comments Like '%" & strSearch & "%' OR Funding Like '%" & strSearch & "%' OR Category Like '%" & strSearch & "%' ) "
                            Else
                                sFilter = sFilter + " or ( [Trade Name] Like '%" & strSearch & "%' OR SKU Like '%" & strSearch & "%' OR Location Like '%" & strSearch & "%' OR Vaccine Like '%" & strSearch & "%' OR [Lot#] Like '%" & strSearch & "%' OR [Mfr.] Like '%" & strSearch & "%' OR [On Hand] Like '%" & strSearch & "%' OR Status Like '%" & strSearch & "%' OR Comments Like '%" & strSearch & "%' OR Funding Like '%" & strSearch & "%' OR Category Like '%" & strSearch & "%' ) "
                            End If
                        End If


                    Next
                    dv.RowFilter = sFilter
                End If
            Else

                dv.RowFilter = sFilter
            End If

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Query, "Searched IM", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch Ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Query, Ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Query, "Could not search IM name  having substring  ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)

            MessageBox.Show(Ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1IMView_AfterSort(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles C1IMView.AfterSort
    End Sub

    Private Sub C1IMView_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1IMView.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = C1IMView.HitTest(ptPoint)

        If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
            UpdateCategory()
        End If
    End Sub

    Private Sub C1IMView_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1IMView.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmIM_View_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If C1IMView.Cols.Count = COL_COUNT Then
            Dim _Width As Single
            _Width = C1IMView.Width
            C1IMView.Cols(COL_SKU).Width = _Width * 0.07
            C1IMView.Cols(col_Vaccine).Width = _Width * 0.1
            C1IMView.Cols(COL_TRADENAME).Width = _Width * 0.1
            C1IMView.Cols(COL_Category).Width = _Width * 0.08
            C1IMView.Cols(COL_LOtNo).Width = _Width * 0.05
            C1IMView.Cols(COL_Mfr).Width = _Width * 0.1
            C1IMView.Cols(COL_OnHand).Width = _Width * 0.12
            C1IMView.Cols(COL_Status).Width = _Width * 0.05
            C1IMView.Cols(COL_Status).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            C1IMView.Cols(COL_Funding).Width = _Width * 0.06
            C1IMView.Cols(COL_ExpiryDate).Width = _Width * 0.08
            C1IMView.Cols(COL_Comments).Width = _Width * 0.012
            C1IMView.Cols(COL_OnHand).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
        End If
    End Sub

    Private Sub SelectClearAll(ByVal blnSelect As Boolean)
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        Try
            RefreshGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1IMView_BeforeSort(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.SortColEventArgs) Handles C1IMView.BeforeSort
    End Sub

    Private Sub cmbLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
        Try
            RefreshGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("IMSetup", "Download")
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

End Class
