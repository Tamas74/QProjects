Public Class frmDM_GuidlineAssociation
    Inherits System.Windows.Forms.Form

    Private COL_PATID As Int16 = 0
    Private COL_PATCODE As Int16 = 1
    Private COL_PATNAME As Int16 = 2
    Private COL_DATE As Int16 = 3
    Private COL_GUIDLINE As Int16 = 4
    Private COL_TEMPLATEID As Int16 = 5
    Private COL_BUTTON As Int16 = 6
    Private COL_FLAG As Int16 = 7

    Private COL_COUNT As Int16 = 8

    Private strGuideline As String = ""
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Private strDate As String = ""

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
            If Not (components Is Nothing) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

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
    Friend WithEvents c1Templates As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cmnuDelete As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteAssociation As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_GuidlineAssociation))
        Me.c1Templates = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cmnuDelete = New System.Windows.Forms.ContextMenu
        Me.mnuDeleteAssociation = New System.Windows.Forms.MenuItem
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label
        Me.lblSearch = New System.Windows.Forms.Label
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me.c1Templates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.SuspendLayout()
        '
        'c1Templates
        '
        Me.c1Templates.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Templates.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Templates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Templates.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.c1Templates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Templates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Templates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Templates.Location = New System.Drawing.Point(3, 2)
        Me.c1Templates.Name = "c1Templates"
        Me.c1Templates.Rows.DefaultSize = 19
        Me.c1Templates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Templates.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Templates.Size = New System.Drawing.Size(801, 444)
        Me.c1Templates.StyleInfo = resources.GetString("c1Templates.StyleInfo")
        Me.c1Templates.TabIndex = 2
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(92, 7)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(712, 15)
        Me.txtSearch.TabIndex = 1
        '
        'cmnuDelete
        '
        Me.cmnuDelete.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteAssociation})
        '
        'mnuDeleteAssociation
        '
        Me.mnuDeleteAssociation.Index = 0
        Me.mnuDeleteAssociation.Text = "Delete Association"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(808, 54)
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnRefresh, Me.ts_btnDelete, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(808, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
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
        'ts_btnDelete
        '
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnDelete.Tag = "Save"
        Me.ts_btnDelete.Text = "&Save&&Cls"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDelete.ToolTipText = "Save and Close"
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
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.lblSearch)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 54)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSearch.Size = New System.Drawing.Size(808, 31)
        Me.pnlSearch.TabIndex = 16
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(92, 4)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(712, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(92, 22)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(712, 5)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.White
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.Image = CType(resources.GetObject("lblSearch.Image"), System.Drawing.Image)
        Me.lblSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSearch.Location = New System.Drawing.Point(4, 4)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(88, 23)
        Me.lblSearch.TabIndex = 41
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 27)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(800, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 3)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(800, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 25)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(804, 3)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 25)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.c1Templates)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 85)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(808, 449)
        Me.pnl_Base.TabIndex = 17
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 445)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(800, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 444)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(804, 2)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 444)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(802, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmDM_GuidlineAssociation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(808, 534)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDM_GuidlineAssociation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Guideline Association"
        CType(Me.c1Templates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDM_GuidlineDuration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gloC1FlexStyle.Style(c1Templates)

        Try
            DesignGrid()
            Fill_Patients()

            lblSearch.Text = "Patient Code"
            lblSearch.Tag = COL_PATCODE
        Catch ex As Exception
            c1Templates.EndInit()
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignGrid()

        With c1Templates
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0

            .SetData(0, COL_PATID, "Id")
            .SetData(0, COL_PATCODE, "Code")
            .SetData(0, COL_PATNAME, "Name")
            .SetData(0, COL_DATE, "Date")
            .SetData(0, COL_GUIDLINE, "Guideline Template")

            .Rows(0).Height = 23
            Dim _Width As Single = (.Width - 20) / 8
            .Cols(COL_PATID).Width = 0
            .Cols(COL_PATCODE).Width = _Width * 1.5
            .Cols(COL_PATNAME).Width = _Width * 2.5
            .Cols(COL_DATE).Width = _Width * 1
            .Cols(COL_GUIDLINE).Width = _Width * 2.5
            .Cols(COL_BUTTON).Width = _Width * 0.3
            Dim _Templates As String
            _Templates = Get_Templates()

            .Cols(COL_PATID).DataType = GetType(Long)
            .Cols(COL_PATCODE).DataType = GetType(String)
            .Cols(COL_PATNAME).DataType = GetType(String)
            .Cols(COL_DATE).DataType = GetType(Date)
            .Cols(COL_GUIDLINE).DataType = GetType(String)
            '.Cols(COL_GUIDLINE).ComboList = _Templates


            .Cols(COL_PATID).Visible = False
            .Cols(COL_PATCODE).Visible = True
            .Cols(COL_PATNAME).Visible = True
            .Cols(COL_DATE).Visible = True
            .Cols(COL_GUIDLINE).Visible = True
            .Cols(COL_TEMPLATEID).Visible = False
            .Cols(COL_BUTTON).Visible = True
            .Cols(COL_FLAG).Visible = False



            .Cols(COL_PATID).AllowEditing = False
            .Cols(COL_PATCODE).AllowEditing = False
            .Cols(COL_PATNAME).AllowEditing = False
            .Cols(COL_DATE).AllowEditing = True
            .Cols(COL_GUIDLINE).AllowEditing = False
            .Cols(COL_TEMPLATEID).AllowEditing = False
            .Cols(COL_BUTTON).AllowEditing = True

        End With
    End Sub

    Private Function Get_Templates() As String
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim oDataReader As SqlClient.SqlDataReader
        Dim _strSQL As String
        Dim _Templates As String = ""

        _strSQL = "SELECT TemplateGallery_MST.sTemplateName, TemplateGallery_MST.nTemplateID, Category_MST.sDescription " _
        & " FROM TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID WHERE (Category_MST.sDescription = 'Patient Education') OR (Category_MST.sDescription = 'Preventive Services') OR (Category_MST.sDescription = 'Wellness Guidelines') " _
        & " ORDER BY Category_MST.sDescription,TemplateGallery_MST.sTemplateName"

        oDB.Connect(GetConnectionString)
        oDataReader = oDB.ReadQueryRecords(_strSQL)


        If Not oDataReader Is Nothing Then
            If oDataReader.HasRows = True Then
                While oDataReader.Read
                    If IsDBNull(oDataReader.Item("sTemplateName")) = False AndAlso IsDBNull(oDataReader.Item("nTemplateID")) = False AndAlso IsDBNull(oDataReader.Item("sDescription")) = False Then
                        _Templates = _Templates & "|" & oDataReader.Item("sTemplateName") & ""
                    End If
                End While
            End If
        End If

        oDB.Disconnect()
        oDB = Nothing

        Return _Templates
    End Function

    Private Sub Fill_Patients()
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim oDataReader As SqlClient.SqlDataReader
        Dim _strSQL As String
        Dim cStyle As C1.Win.C1FlexGrid.CellStyle

        '_strSQL = "SELECT npatientid,spatientcode,(sFirstName + ' ' + smiddlename + ' ' + slastname) AS PatName from patient"

        _strSQL = " SELECT     Patient.nPatientID, Patient.sPatientCode, Patient.sFirstName + ' ' + Patient.sMiddleName + ' ' + Patient.sLastName AS PatName,  " _
                & " ISNULL(TemplateGallery_MST.sTemplateName, '') AS TemplateName, DM_Association.dm_dtOnSetDate, ISNULL(DM_Association.dm_nTemplateID, 0) AS TemplateID, " _
                & " ISNULL(DM_Association.dm_nFlag, 0) AS Flag " _
                & " FROM         TemplateGallery_MST INNER JOIN " _
                & " DM_Association ON TemplateGallery_MST.nTemplateID = DM_Association.dm_nTemplateID RIGHT OUTER JOIN " _
                & " Patient ON DM_Association.dm_nPatientID = Patient.nPatientID "

        oDB.Connect(GetConnectionString)
        oDataReader = oDB.ReadQueryRecords(_strSQL)

        c1Templates.BeginInit()
        With c1Templates
            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        If IsDBNull(oDataReader.Item("npatientid")) = False AndAlso IsDBNull(oDataReader.Item("spatientcode")) = False AndAlso IsDBNull(oDataReader.Item("PatName")) = False Then
                            c1Templates.Rows.Add()
                            c1Templates.SetData(c1Templates.Rows.Count - 1, COL_PATID, oDataReader.Item("npatientid") & "")
                            c1Templates.SetData(c1Templates.Rows.Count - 1, COL_PATCODE, oDataReader.Item("spatientcode") & "")
                            c1Templates.SetData(c1Templates.Rows.Count - 1, COL_PATNAME, oDataReader.Item("PatName") & "")
                            .SetData(.Rows.Count - 1, COL_GUIDLINE, oDataReader.Item("TemplateName") & "")
                            .SetData(.Rows.Count - 1, COL_TEMPLATEID, oDataReader.Item("TemplateID") & "")

                            If IsDBNull(oDataReader.Item("dm_dtOnSetDate")) = False Then
                                .SetData(.Rows.Count - 1, COL_DATE, oDataReader.Item("dm_dtOnSetDate") & "")
                            End If
                            .SetData(.Rows.Count - 1, COL_FLAG, oDataReader.Item("Flag") & "")

                            'cStyle = .Styles.Add("BubbleValues")
                            Try
                                If (.Styles.Contains("BubbleValues")) Then
                                    cStyle = .Styles("BubbleValues")
                                Else
                                    cStyle = .Styles.Add("BubbleValues")

                                End If
                            Catch ex As Exception
                                cStyle = .Styles.Add("BubbleValues")

                            End Try
                            cStyle.ComboList = "..."
                            Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Rows.Count - 1, COL_BUTTON, .Rows.Count - 1, COL_BUTTON)
                            rgDig.Style = cStyle

                        End If
                    End While
                End If
            End If
            oDB.Disconnect()
            oDB = Nothing
        End With
        c1Templates.EndInit()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub c1Templates_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1Templates.CellButtonClick
        Try
            With c1Templates
                If .ColSel = COL_BUTTON Then
                    Dim frm As New frmDM_SelectGuideline
                    With frm
                        .ShowInTaskbar = False
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    End With
                    If frm._strTemplateName <> "" Then
                        Dim str As String()
                        str = Split(frm._strTemplateName, "|", 2)
                        If (str.Length > 1) Then
                            .SetData(.RowSel, COL_GUIDLINE, str(1))
                        End If

                        .SetData(.RowSel, COL_TEMPLATEID, str(0))
                    Else
                        frm.Dispose()
                        frm = Nothing
                        Exit Sub
                    End If
                    frm.Dispose()
                    frm = Nothing
                End If

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub c1Templates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles c1Templates.Click
        Try
            With c1Templates
                If .ColSel = COL_PATCODE Then
                    lblSearch.Text = "Patient Code"
                    lblSearch.Tag = COL_PATCODE
                ElseIf .ColSel = COL_PATNAME Then
                    lblSearch.Text = "Patient Name"
                    lblSearch.Tag = COL_PATNAME
                End If
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            With c1Templates
                If lblSearch.Tag = COL_PATCODE Then
                    .Row = .FindRow(txtSearch.Text.Trim, 1, COL_PATCODE, False, False, True)
                ElseIf lblSearch.Tag = COL_PATNAME Then
                    .Row = .FindRow(txtSearch.Text.Trim, 1, COL_PATNAME, False, False, True)
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                c1Templates.Select()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub c1Templates_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1Templates.AfterEdit
        Try
            ' If e.Col = COL_DATE Or e.Col = COL_TEMPLATEID Then
            '' If Date Change the Reset the Flag
            With c1Templates
                'c1Templates.SetData(e.Row, COL_FLAG, 0)
                If .GetData(e.Row, COL_GUIDLINE) <> strGuideline Or Format(.GetData(e.Row, COL_DATE), "MM/dd/yyyy").ToString <> strDate Then
                    c1Templates.SetData(e.Row, COL_FLAG, 0)
                End If
            End With
            '  End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1Templates_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1Templates.BeforeEdit
        Try
            'If e.Col = COL_DATE Or e.Col = COL_TEMPLATEID Then
            '' If Date Change the Reset the Flag
            With c1Templates
                strGuideline = .GetData(e.Row, COL_GUIDLINE)
                strDate = Format(.GetData(e.Row, COL_DATE), "MM/dd/yyyy").ToString
            End With
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1Templates_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Templates.MouseDown
        Try
            With c1Templates
                If e.Button = MouseButtons.Right Then
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    .Select(r, True)
                    If r > 0 And .GetData(r, COL_GUIDLINE).ToString <> "" Then
                        'Try
                        '    If (IsNothing(.ContextMenu) = False) Then
                        '        .ContextMenu.Dispose()
                        '        .ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenu = cmnuDelete
                    Else
                        'Try
                        '    If (IsNothing(.ContextMenu) = False) Then
                        '        .ContextMenu.Dispose()
                        '        .ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        .ContextMenu = Nothing
                    End If
                Else
                    'Try
                    '    If (IsNothing(.ContextMenu) = False) Then
                    '        .ContextMenu.Dispose()
                    '        .ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    .ContextMenu = Nothing
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteAssociation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteAssociation.Click
        Try
            With c1Templates
                .SetData(.Row, COL_DATE, Nothing)
                .SetData(.Row, COL_GUIDLINE, "")
                .SetData(.Row, COL_TEMPLATEID, 0)
                .SetData(.Row, COL_FLAG, 0)
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    ''Code Added by Shilpa for adding the new buttons on 13th Nov 2007
    Private Sub RefreshCategory()
        txtSearch.Clear()
        c1Templates.RowSel = 1
    End Sub
    Private Sub SaveCategory()
        Try
            Dim Assocaition As New Collection
            Dim lst As myList
            With c1Templates
                For i As Integer = 1 To .Rows.Count - 1
                    If .GetData(i, COL_DATE) <> Nothing AndAlso .GetData(i, COL_GUIDLINE) <> "" Then
                        lst = New myList
                        lst.ID = .GetData(i, COL_PATID)
                        lst.VisitDate = .GetData(i, COL_DATE)
                        lst.Index = .GetData(i, COL_TEMPLATEID)
                        lst.Value = .GetData(i, COL_FLAG)
                        Assocaition.Add(lst)
                    End If
                Next

            End With

            Dim oDM_Template As New clsDM_Template
            If oDM_Template.Save_GuidelineAssociation(Assocaition) = True Then
                Me.Close()
            End If
            oDM_Template = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Refresh"
                Call RefreshCategory()
            Case "Save"
                Call SaveCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub c1Templates_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Templates.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
