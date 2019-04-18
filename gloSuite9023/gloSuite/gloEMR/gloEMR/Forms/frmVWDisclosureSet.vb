Public Class frmVWDisclosureSet
    Inherits System.Windows.Forms.Form
    Private objDBLayer As New clsDisclosureMgmt  'ClsDBLayer  '
    Private intCatID As Long
    Private strCatDesc As String
    '' To Allow Sreach
    Private _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Dim strsortorder As String
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
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Dim dt As DataTable

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
            Try

                If (IsNothing(dgDisclosureSet) = False) Then
                    dgDisclosureSet.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgDisclosureSet)
                    dgDisclosureSet.Dispose()
                    dgDisclosureSet = Nothing
                End If
            Catch ex As Exception

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
    Friend WithEvents dgDisclosureSet As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWDisclosureSet))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgDisclosureSet = New System.Windows.Forms.DataGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.pnlTopRight.SuspendLayout()
        CType(Me.dgDisclosureSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.panel4)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(661, 24)
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
        Me.btnClear.TabIndex = 45
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = True
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
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(659, 1)
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
        Me.Label3.Location = New System.Drawing.Point(660, 1)
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
        Me.Label4.Size = New System.Drawing.Size(661, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'dgDisclosureSet
        '
        Me.dgDisclosureSet.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgDisclosureSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgDisclosureSet.BackgroundColor = System.Drawing.Color.White
        Me.dgDisclosureSet.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgDisclosureSet.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgDisclosureSet.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgDisclosureSet.CaptionForeColor = System.Drawing.Color.White
        Me.dgDisclosureSet.CaptionVisible = False
        Me.dgDisclosureSet.DataMember = ""
        Me.dgDisclosureSet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgDisclosureSet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgDisclosureSet.ForeColor = System.Drawing.Color.Black
        Me.dgDisclosureSet.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgDisclosureSet.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgDisclosureSet.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgDisclosureSet.HeaderForeColor = System.Drawing.Color.White
        Me.dgDisclosureSet.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgDisclosureSet.Location = New System.Drawing.Point(3, 1)
        Me.dgDisclosureSet.Name = "dgDisclosureSet"
        Me.dgDisclosureSet.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgDisclosureSet.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgDisclosureSet.ReadOnly = True
        Me.dgDisclosureSet.RowHeadersVisible = False
        Me.dgDisclosureSet.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgDisclosureSet.SelectionForeColor = System.Drawing.Color.Black
        Me.dgDisclosureSet.Size = New System.Drawing.Size(660, 431)
        Me.dgDisclosureSet.TabIndex = 2
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(667, 53)
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
        Me.ts_ViewButtons.Size = New System.Drawing.Size(667, 53)
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
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.dgDisclosureSet)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(667, 435)
        Me.Panel1.TabIndex = 12
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 431)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(659, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 431)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(663, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 431)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(661, 1)
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
        Me.Panel2.Size = New System.Drawing.Size(667, 30)
        Me.Panel2.TabIndex = 13
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
        Me.panel4.TabIndex = 47
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
        'frmVWDisclosureSet
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(667, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVWDisclosureSet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Disclosure Set"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        CType(Me.dgDisclosureSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frmVWCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            dgDisclosureSet.AllowSorting = True
            Bindgrid()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub Bindgrid(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try
            dt = New DataTable

            dt = objDBLayer.GetDisclosureSet()
            dgDisclosureSet.DataSource = dt
            dgDisclosureSet.SetDataBinding(objDBLayer.GetDataView, "")
            txtSearch.Text = ""
            txtSearch.Text = strsearchtxt
            'If strcolumnName = "" Then
            '    objDBLayer.SortDataview(objDBLayer.GetDataView.Table.Columns(1).ColumnName)
            'Else
            '    Dim strColumn As String = Replace(strcolumnName, "[", "")

            '    objDBLayer.SortDataview(strColumn, strSortBy)
            'End If

            'Commented by Shubhangi 20091202
            'Called this function on Shown
            'HideColumn()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        'dgCategoryList.DataBindings = objDBLayer.DsDataSet
        'dgCategoryList.DataMember = "Category_Mst"
    End Sub
    Private Sub HideColumn()
        Dim ts As New clsDataGridTableStyle(objDBLayer.GetDataView.Table.TableName)

        Dim dgCategory As New DataGridTextBoxColumn

        With dgCategory
            .MappingName = objDBLayer.GetDataView.Table.Columns(0).ColumnName
            .Alignment = HorizontalAlignment.Center
            .NullText = ""
            .Width = 0
        End With

        Dim dgDescription As New DataGridTextBoxColumn
        With dgDescription
            .MappingName = objDBLayer.GetDataView.Table.Columns(1).ColumnName
            .HeaderText = "Disclosure Set"
            .NullText = ""
            .Width = 0.25 * dgDisclosureSet.Width
        End With

        Dim dgCategoryType As New DataGridTextBoxColumn
        With dgCategoryType
            .MappingName = objDBLayer.GetDataView.Table.Columns(2).ColumnName
            .HeaderText = "Association Type"
            .NullText = ""
            .Width = 0.75 * dgDisclosureSet.Width - 10
        End With
        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgCategory, dgDescription, dgCategoryType})
        dgDisclosureSet.TableStyles.Clear()
        dgDisclosureSet.TableStyles.Add(ts)

    End Sub

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
                Dim dvPatient As DataView
                dvPatient = CType(dgDisclosureSet.DataSource(), DataView)

                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                ' dgCategoryList.DataSource = dvPatient
                Dim strPatientSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''") & ""
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If


                dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' "
                'Shubhangi 20091202
                'Set focus on first row of filtrated recrds
                If dvPatient.Count > 0 Then
                    dgDisclosureSet.Select(0)
                End If

                'COMMENTED BY SHUBHANGI 20091001
                'Select Case Trim(lblSearch.Text)
                '    Case "Disclosure Set"
                '        '''''Code Modified by Anil on 24/09/2007 at 3:15 p.m.
                '        '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
                '        '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
                '        '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

                '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        '' Else
                '        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        ''End If
                '    Case "Association Type"
                '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        ''Else
                '        ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        ''End If
                '        '''''Upto here the changes are made by Anil on 24/09/2007 at 3:15 p.m.
                'End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub



    'Private Sub UpdateCategory()


    'End Sub


    Private Sub frmVWCategory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub


    Private Sub dgCategoryList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgDisclosureSet.MouseDoubleClick
        Try
            ' dgCategoryList.AllowSorting = False
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgDisclosureSet.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                'COMMENTED BY SHUBHANGI 20091001
                'Select Case htInfo.Column ' dgCategoryList.CurrentCell.ColumnNumber
                '    Case 1
                '        lblSearch.Text = "Disclsoure Set"
                '    Case 2
                '        lblSearch.Text = "Association Type"
                'End Select
                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                UpdateCategory()
            End If
            ' dgCategoryList.AllowSorting = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AddCategory()
        Dim frm As New frmMSTDisclosureSet

        Try

            frm.Text = "New Disclosure Set"
            frm.MaximizeBox = False
            frm.IsModify = False
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.Dispose()
            frm = Nothing
            Bindgrid()

            'Dim i As Integer
            'For i = 0 To CType(dgCategoryList.DataSource, DataView).Table.Rows.Count - 1
            '    'If frm._CategoryName = dgCategoryList.Item(i, 1) Then
            '    dgCategoryList.CurrentRowIndex = i
            '    dgCategoryList.Select(i)
            '    Exit For
            '    'End If
            'Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            frm = Nothing
        End Try
    End Sub
    Private Sub UpdateCategory()
        If dgDisclosureSet.VisibleRowCount >= 1 Then
            'strCatDesc = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 1)
            'collCategoryDesc = New Collection
            'collCategoryDesc.Clear()
            'FillCategoryDescription_Collection()
            'For j As Integer = 1 To collCategoryDesc.Count
            '    If collCategoryDesc.Item(j) = strCatDesc Then
            '        MessageBox.Show("System defined categories can not be modified", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Exit Sub
            '    End If
            'Next
            Dim grdIndex As Integer
            intCatID = Int64.Parse(dgDisclosureSet.Item(dgDisclosureSet.CurrentRowIndex, 0))
            grdIndex = dgDisclosureSet.CurrentRowIndex
            Dim myDataView As DataView = CType(dgDisclosureSet.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                sortOrder = CType(dgDisclosureSet.DataSource, DataView).Sort
                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If
            End If
            Dim frm As New frmMSTDisclosureSet(intCatID)
            frm.Text = "Modify Disclosure Set"
            frm.MaximizeBox = False
            frm.txtCategoryName.Text = dgDisclosureSet.Item(dgDisclosureSet.CurrentRowIndex, 1).ToString
            frm.IsModify = True
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            Bindgrid(strcolumnName, strsortorder, strSearchstring)
            Dim myDatagView As DataView = CType(dgDisclosureSet.DataSource, DataView)
            If (IsNothing(myDatagView) = False) Then


                Dim i As Integer
                For i = 0 To myDatagView.Count - 1 ''  Table.Rows.Count - 1
                    If intCatID = dgDisclosureSet.Item(i, 0) Then
                        dgDisclosureSet.CurrentRowIndex = i
                        dgDisclosureSet.Select(i)
                        Exit For
                    End If
                Next
            End If
            frm.Dispose()
            frm = Nothing
        End If
    End Sub
    Private Sub DeleteCategory()
        Try
            If dgDisclosureSet.VisibleRowCount >= 1 Then
                If MessageBox.Show("Are you sure you want to delete this record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    intCatID = Int64.Parse(dgDisclosureSet.Item(dgDisclosureSet.CurrentRowIndex, 0))
                    objDBLayer.DeleteDisclosure(intCatID)
                    Dim myDataView As DataView = CType(dgDisclosureSet.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = CType(dgDisclosureSet.DataSource, DataView).Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        Bindgrid(strcolumnName, strsortorder, strSearchstring)
                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Me.Cursor = Cursors.WaitCursor
        Bindgrid()
        Me.Cursor = Cursors.Default

        '''''This code clears the search textbox and gets the focus on the first row of the grid on click of Refresh button.
        txtSearch.Clear()
        _blnSearch = True
        If Not dgDisclosureSet.VisibleRowCount = 0 Then
            dgDisclosureSet.CurrentRowIndex = 0
            dgDisclosureSet.Select(dgDisclosureSet.CurrentRowIndex)
        End If
        '''''upto here code added
    End Sub
    Private Sub FormClose()
        objDBLayer = Nothing
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

    Private Sub dgCategoryList_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgDisclosureSet.MouseUp
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgDisclosureSet.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                dgDisclosureSet.Select(htInfo.Row)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930
        'Use to clear search text box.
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
    'Shubhangi 20091202
    'Add Hidecolumn function here coz this function is called before load so it will overcome the grid resize after search.
    Private Sub frmVWDisclosureSet_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        HideColumn()
    End Sub
End Class
